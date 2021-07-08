using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Threading;
using AForge.Video;
using AForge.Video.DirectShow;
using com.ipevo.windows.CameraKit;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using com.ipevo.windows.ToolKit;
using System.Drawing.Imaging;



namespace FFBM
{

    public partial class Form1 : Form
    {
        private WriteableBitmap captureWriteableBitmap;
        private ICCamera previousSelectedCamera = null;
        List<Dictionary<ICCamera.FormatKey, object>> supportFormats = new List<Dictionary<ICCamera.FormatKey, object>>();

        private ICCameraStreamProxy.StreamObserver streamObserver;
        public Form1()
        {
            InitializeComponent();
        }


        private ICCamera ActionDevice
        {
            get
            {
                ICCamera camera = null;
                if (this.comboBox1.SelectedItem != null)
                {
                    foreach (ICCamera value in ICCameraManager.sharedManager.cameras)
                    {
                        if (value.CameraInstanceName == this.comboBox1.SelectedItem.ToString())
                        {
                            camera = value;
                            break;
                        }
                    }
                }
                return camera;
            }
        }

        private void UpdateDevice(string notificationName, object sender, object userInfo)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(new Action(() =>
            {
                switch (notificationName)
                {
                    case ICCameraManager.ICNotification.DeviceAttached:
                        {
                            if (!comboBox1.Items.Contains((userInfo as ICCamera).CameraInstanceName))
                                comboBox1.Items.Add((userInfo as ICCamera).CameraInstanceName);
                        }
                        break;
                    case ICCameraManager.ICNotification.DeviceDetached:
                        {
                            if (comboBox1.Items.Contains((userInfo as ICCamera).CameraInstanceName))
                                comboBox1.Items.Remove((userInfo as ICCamera).CameraInstanceName);
                        }
                        break;
                    default:
                        break;
                }
            }));
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            ICCameraManager.sharedManager.startMonitor();
            comboBox1.DataSource = ICCameraManager.sharedManager.cameras;
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var camera_selected = this.comboBox1.SelectedIndex.ToString();
            ICCamera camera = comboBox1.SelectedItem as ICCamera;

            foreach (var item in camera.supportedFormats())
            {
                comboBox2.Items.Add(item[ICCamera.FormatKey.FormatInfo]);
            }
            comboBox2.SelectedIndex = 0;
        }

        private void Window_Close(object sender, EventArgs e)
        {
            ICCameraManager.sharedManager.stopMonitor();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            /*Show a preview of taken photos*/
            Form2 win2 = new Form2();
            win2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ICCamera camera = this.ActionDevice;
            if (camera != null)
            {
                camera.getAutoFocus(out bool isAuto);
                if (!isAuto)
                {
                    camera.setAutoFocus(true);
                }
                camera.startFocus();
            }
        }

        private void pic_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*Start streaming aftar that you choose the camera*/
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            bool isHasCapabilityAutoExposure = camera.hasCapability(ICCamera.Capability.AutoExposure);
            NSNotificationCenter.defaultCenter.addNotificationObserver(ICCameraManager.ICNotification.DeviceFocusBegin, new
            NSNotificationCenter.NotificationObserver(cameraFocusStatus));

            List<Dictionary<ICCamera.FormatKey, object>> supportFormats = camera.supportedFormats();
            Dictionary<ICCamera.FormatKey, object> setFormat = supportFormats[comboBox2.SelectedIndex];
            camera.setFormat(setFormat);

            ICCameraStreamProxy.StreamObserver streamObserver;
            streamObserver = new ICCameraStreamProxy.StreamObserver(UpdateStream);
            ICCameraStreamProxy.sharedProxy.addStreamObserver(camera, streamObserver);
        }

        private void cameraFocusStatus(string notificationName, object sender, object userInfo)
        {
            if (userInfo != null)
            {
                System.Collections.Hashtable data = userInfo as System.Collections.Hashtable;
                ICCamera camera = data[ICCameraManager.ICCommonString.Camera] as ICCamera;
                string focusStatus = notificationName ==
                ICCameraManager.ICNotification.DeviceFocusBegin ? "Focus Start" : "FocusFinish";

                Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                {
                    if (camera == comboBox1.SelectedItem as ICCamera)
                        DeviceMessageTextBlock.Text = string.Format("{0} {1}",
                        camera.CameraInstanceName, focusStatus);
                }));

            }
        }


        private void UpdateStream(ICCamera camera, IntPtr buffer, int bufferLength, int frameWidth = 0, int frameHeight = 0)
        {
            Dispatcher.CurrentDispatcher.Invoke(new System.Threading.ThreadStart(() =>
            {
                StreamBufferToWriteableBitmap(camera, buffer, bufferLength);
            }));
        }

        [System.Runtime.InteropServices.DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);
        object lockObject = new object();
        private void StreamBufferToWriteableBitmap(ICCamera camera, IntPtr buffer, int bufferLength)
        {
            lock (lockObject)
            {
                if (captureWriteableBitmap == null)
                {
                    camera.getFormat(out Dictionary<ICCamera.FormatKey, object> formatInfo);
                    if (formatInfo != null
                        && formatInfo.Count > 0)
                        this.CreateWriteableBitmap(Convert.ToInt32(formatInfo[ICCamera.FormatKey.Width]), Convert.ToInt32(formatInfo[ICCamera.FormatKey.Height]));
                }
                else
                {
                    int writeableBitmapSize = (int)captureWriteableBitmap.Width * (int)captureWriteableBitmap.Height * 4;
                    if (writeableBitmapSize != bufferLength)
                    {
                        captureWriteableBitmap = null;

                        camera.getFormat(out Dictionary<ICCamera.FormatKey, object> formatInfo);
                        if (formatInfo != null
                            && formatInfo.Count > 0)
                            this.CreateWriteableBitmap(Convert.ToInt32(formatInfo[ICCamera.FormatKey.Width]), Convert.ToInt32(formatInfo[ICCamera.FormatKey.Height]));
                    }

                    if (captureWriteableBitmap != null
                        && buffer != IntPtr.Zero)
                    {
                        try
                        {
                            captureWriteableBitmap.Lock();
                            System.Windows.Int32Rect rect = new System.Windows.Int32Rect(0, 0, (int)captureWriteableBitmap.Width, (int)captureWriteableBitmap.Height);
                            IntPtr backbuffer = captureWriteableBitmap.BackBuffer;
                            CopyMemory(backbuffer, buffer, bufferLength);
                            captureWriteableBitmap.AddDirtyRect(rect);
                            captureWriteableBitmap.Unlock();
                            pic.Image = BitmapFromWriteableBitmap(captureWriteableBitmap);
                            
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());

                            captureWriteableBitmap = null;
                        }
                    }
                }
            }
        }

        private void CreateWriteableBitmap(int sourceWidth, int sourceHeight)
        {
            try
            {
                captureWriteableBitmap = new WriteableBitmap(sourceWidth, sourceHeight, 96d, 96d, PixelFormats.Bgr32, null);
                pic.Image = BitmapFromWriteableBitmap(captureWriteableBitmap);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private Bitmap BitmapFromWriteableBitmap(WriteableBitmap source)
        {
            Bitmap bmp;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)source));
                enc.Save(outStream);
                bmp = new Bitmap(outStream);
            }
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            return bmp;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ICCamera camera = ActionDevice;

            if (camera != null && supportFormats.Count > 0)
            {
                ICCameraStreamProxy.sharedProxy.stopStreamObserver(camera);
                Dictionary<ICCamera.FormatKey, object> setFormat = supportFormats[comboBox2.SelectedIndex];
                camera.setFormat(setFormat);

                ICCameraStreamProxy.sharedProxy.startStreamObserver(camera);
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            //Console.WriteLine(comboBox1.SelectedItem.ToString());
            ICCameraStreamProxy.sharedProxy.stopStreamObserver(camera);
            ICCameraStreamProxy.StreamObserver streamObserver;
            streamObserver = new ICCameraStreamProxy.StreamObserver(UpdateStream);
            ICCameraStreamProxy.sharedProxy.removeStreamObserver(camera, streamObserver);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.SaveCameraImage();
        }



        private void SaveCameraImage()
        {
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            camera.getFormat(out Dictionary<ICCamera.FormatKey, object> formatInfo);
            if (formatInfo != null && formatInfo.Count > 0)
            {
                var sourceWidth = Convert.ToInt32(formatInfo[ICCamera.FormatKey.Width]);
                var sourceHeight = Convert.ToInt32(formatInfo[ICCamera.FormatKey.Height]);
                captureWriteableBitmap = new WriteableBitmap(sourceWidth, sourceHeight, 96d, 96d, PixelFormats.Bgr32, null);
                var frame = BitmapFromWriteableBitmap(captureWriteableBitmap);
                frame.Save("test.png", System.Drawing.Imaging.ImageFormat.Png);

            }
                
          

            /*System.Drawing.Bitmap captureBitmap = BitmapSourceToBitmap((BitmapSource)this.captureWriteableBitmap);
            string fileName = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("MM-dd-yyyy_HHmmss") + ".jpg";
            captureBitmap.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipX);
            captureBitmap.Save(fileName);
            captureBitmap?.Dispose();
            captureBitmap = null;

            //open current folder
            System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory);*/
        }
    }




}
