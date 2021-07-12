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
using System.Windows;

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

        private void InitializeManualFocus(ICCamera camera)
        {
            /*Initialize values of focus regarding to capabilities camera*/
            short min = 0, max = 0, delta = 0,value=0;
            if (camera.hasCapability(ICCamera.Capability.ManualFocus))
            {
                //mfocusBar.Visible = true;
                camera.getFocus(out min, ICCamera.PropertyValueType.Minimum);
                camera.getFocus(out max, ICCamera.PropertyValueType.Maximum);
                camera.getFocus(out delta, ICCamera.PropertyValueType.Delta);
                camera.getFocus(out value, ICCamera.PropertyValueType.Current);
                mfocusBar.Minimum = min;
                mfocusBar.Maximum = max;
                mfocusBar.TickFrequency = delta;
                Console.WriteLine(mfocusBar.Maximum);
                mfocusBar.ValueChanged +=
                new System.EventHandler(mfocusBar_ValueChanged);
                this.Controls.Add(this.mfocusBar);
            }
            //setting brightness
            camera.getBrightness(out min, ICCamera.PropertyValueType.Minimum);
            camera.getBrightness(out max, ICCamera.PropertyValueType.Maximum);
            camera.getBrightness(out delta, ICCamera.PropertyValueType.Delta);
            camera.getBrightness(out value, ICCamera.PropertyValueType.Current);

            brBar.Minimum = min;
            brBar.Maximum = max;
            brBar.TickFrequency = delta;
            brBar.Value = value;
            brBar.ValueChanged +=
                new System.EventHandler(brBar_ValueChanged);
            this.Controls.Add(this.brBar);

            //setting contrast
            camera.getContrast(out min, ICCamera.PropertyValueType.Minimum);
            camera.getContrast(out max, ICCamera.PropertyValueType.Maximum);
            camera.getContrast(out delta, ICCamera.PropertyValueType.Delta);
            camera.getContrast(out value, ICCamera.PropertyValueType.Current);

            contrastBar.Minimum = min;
            contrastBar.Maximum = max;
            contrastBar.TickFrequency = delta;
            contrastBar.ValueChanged +=
                new System.EventHandler(contrastBar_ValueChanged);
            this.Controls.Add(this.contrastBar);
            //setting gamma
            camera.getGamma(out min, ICCamera.PropertyValueType.Minimum);
            camera.getGamma(out max, ICCamera.PropertyValueType.Maximum);
            camera.getGamma(out delta, ICCamera.PropertyValueType.Delta);
            camera.getGamma(out value, ICCamera.PropertyValueType.Current);

            gammaBar.Minimum = min;
            gammaBar.Maximum = max;
            gammaBar.TickFrequency = delta;
            gammaBar.ValueChanged +=
                new System.EventHandler(gammaBar_ValueChanged);
            this.Controls.Add(this.gammaBar);

            //setting saturation
            camera.getSaturation(out min, ICCamera.PropertyValueType.Minimum);
            camera.getSaturation(out max, ICCamera.PropertyValueType.Maximum);
            camera.getSaturation(out delta, ICCamera.PropertyValueType.Delta);
            camera.getSaturation(out value, ICCamera.PropertyValueType.Current);

            saturationBar.Minimum = min;
            saturationBar.Maximum = max;
            saturationBar.TickFrequency = delta;
            saturationBar.ValueChanged +=
                new System.EventHandler(saturationBar_ValueChanged);
            this.Controls.Add(this.saturationBar);
        }





        private void Form1_Load_1(object sender, EventArgs e)
        {
            //mfocusBar.Visible = false;
            ICCameraManager.sharedManager.startMonitor();
            comboBox1.DataSource = ICCameraManager.sharedManager.cameras;
            CheckForIllegalCrossThreadCalls = false;
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            //this.previousSelectedCamera = camera;
            InitializeManualFocus(camera);
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var camera_selected = this.comboBox1.SelectedIndex.ToString();
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            if (comboBox1.SelectedIndex == 0)
            {
                comboBox2.Items.Clear();
                this.previousSelectedCamera = camera;
            }
            if (comboBox1.SelectedIndex != 0)
            {
                Console.WriteLine(this.previousSelectedCamera);
                Console.WriteLine(camera);
                ICCameraStreamProxy.sharedProxy.stopStreamObserver(this.previousSelectedCamera);
                ICCameraStreamProxy.sharedProxy.removeStreamObserver(this.previousSelectedCamera, this.streamObserver);
                comboBox2.Items.Clear();
                ICCameraStreamProxy.sharedProxy.addStreamObserver(camera, this.streamObserver);
                this.previousSelectedCamera = camera;
            }
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
            string path_pro = this.path;
            Form2 win2 = new Form2();
            win2.MyProperty = path_pro;
            win2.ListImages = ls;
            win2.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            camera.setAutoFocus(true);
            camera.startFocus();
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
            NSNotificationCenter.defaultCenter.addNotificationObserver(ICCameraManager.ICNotification.FocusChanged, 
            new NSNotificationCenter.NotificationObserver(this.CameraFocusValue));

            List<Dictionary<ICCamera.FormatKey, object>> supportFormats = camera.supportedFormats();
            Dictionary<ICCamera.FormatKey, object> setFormat = supportFormats[comboBox2.SelectedIndex];
            camera.setFormat(setFormat);

            ICCameraStreamProxy.StreamObserver streamObserver;
            streamObserver = new ICCameraStreamProxy.StreamObserver(UpdateStream);
            ICCameraStreamProxy.sharedProxy.addStreamObserver(camera, streamObserver);
        }

        private void CameraFocusValue(string notificationName, object sender, object userInfo)
        {
            if (userInfo != null)
            {
                this.isReceiveCameraFocusValue = true;
                System.Collections.Hashtable data = userInfo as System.Collections.Hashtable;
                ICCamera camera = data[ICCameraManager.ICCommonString.Camera] as ICCamera;
                int focusValue = Convert.ToInt32(data[ICCameraManager.ICCommonString.FocusValue]);
                Dispatcher.CurrentDispatcher.Invoke(new Action(() =>
                {
                    ICCamera selectedCamera = comboBox1.SelectedItem as ICCamera;
                    mfocusBar.Value = focusValue;
                }));
                this.isReceiveCameraFocusValue = false;
            }
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

            //ICCamera camera = comboBox1.SelectedItem as ICCamera;

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
            //ICCameraStreamProxy.sharedProxy.ReleaseStreamProxy();
            pic.Image = null;
            captureWriteableBitmap = null;
        }
        //public string path = @"c:\Windows\Temp\" + DateTime.Now.ToString("MM-dd-yyyy_HHmmss") + "\\";
        public string path = @"c:\Windows\Temp\" + "FFBM\\" ;
        public int counter = 0;
        List<string> ls = new List<string>();
        private void btnStart_Click(object sender, EventArgs e)
        {
            Dispatcher.CurrentDispatcher.Invoke(new ThreadStart(()=>{
                if(counter == 5)
                {
                    
                    counter = 0;
                    Console.WriteLine("Entro al llegar a 6 e inicializo");
                }
                Console.WriteLine(counter);
                if (counter == 0)
                {
                    
                    DirectoryInfo directory = Directory.CreateDirectory(this.path);
                }
                string fileName = this.path + DateTime.Now.ToString("MM-dd-yyyy_HHmmss") + ".png";
                ls.Add(fileName);
                ls.ForEach(Console.WriteLine);
                pic.Image.Save(fileName, ImageFormat.Png);
                counter++;
            }));
        }

        private void mfocusBar_Scroll(object sender, EventArgs e)
        {
            
        }

        private void mfocusBar_ValueChanged(object sender, EventArgs e)
        {
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            if (camera != null && !this.isReceiveCameraFocusValue)
            {
                double val = (sender as TrackBar).Value;
                camera.setFocus((short)mfocusBar.Value);
                Console.WriteLine(val);
            }
        }

        private void brBar_ValueChanged(object sender, EventArgs e)
        {
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            if(camera != null)
            {
                //brightness change
                //Console.WriteLine(brBar.Value);
                camera.setBrightness((short)brBar.Value);
            }
        }

        private void contrastBar_ValueChanged(object sender , EventArgs e)
        {
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            if (camera != null)
            {
                //contrast change
                //Console.WriteLine(contrastBar.Value);
                camera.setBrightness((short)contrastBar.Value);
            }
        }

        private void gammaBar_ValueChanged(object sender, EventArgs e)
        {
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            if (camera != null)
            {
                //gamma change
                //Console.WriteLine(gammaBar.Value);
                camera.setGamma((short)gammaBar.Value);
            }
        }

        private void saturationBar_ValueChanged(object sender, EventArgs e)
        {
            ICCamera camera = comboBox1.SelectedItem as ICCamera;
            if (camera != null)
            {
                //saturation change
                //Console.WriteLine(saturationBar.Value);
                camera.setSaturation((short)saturationBar.Value);
            }
        }

        bool isReceiveCameraFocusValue = false;

        private void brBar_Scroll(object sender, EventArgs e)
        {

        }
    }

}
