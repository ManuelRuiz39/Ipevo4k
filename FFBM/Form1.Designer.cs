
namespace FFBM
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.preview = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.DeviceMessageTextBlock = new System.Windows.Forms.Label();
            this.stop = new System.Windows.Forms.Button();
            this.brBar = new System.Windows.Forms.TrackBar();
            this.br = new System.Windows.Forms.Label();
            this.contrast = new System.Windows.Forms.Label();
            this.contrastBar = new System.Windows.Forms.TrackBar();
            this.gamma = new System.Windows.Forms.Label();
            this.gammaBar = new System.Windows.Forms.TrackBar();
            this.saturation = new System.Windows.Forms.Label();
            this.saturationBar = new System.Windows.Forms.TrackBar();
            this.mfocusBar = new System.Windows.Forms.TrackBar();
            this.mfocus = new System.Windows.Forms.Label();
            this.FocusChanged = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mfocusBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(919, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose device";
            // 
            // pic
            // 
            this.pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic.Location = new System.Drawing.Point(24, 40);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(873, 530);
            this.pic.TabIndex = 2;
            this.pic.TabStop = false;
            this.pic.Click += new System.EventHandler(this.pic_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(922, 547);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(121, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Take Picture";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(922, 59);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(264, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(922, 116);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(264, 21);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(922, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Chose resolution";
            // 
            // preview
            // 
            this.preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.preview.Location = new System.Drawing.Point(1065, 547);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(121, 23);
            this.preview.TabIndex = 7;
            this.preview.Text = "Preview";
            this.preview.UseVisualStyleBackColor = true;
            this.preview.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(922, 426);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(264, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "AutoFocus";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(922, 464);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(264, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // DeviceMessageTextBlock
            // 
            this.DeviceMessageTextBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceMessageTextBlock.AutoSize = true;
            this.DeviceMessageTextBlock.Location = new System.Drawing.Point(848, 575);
            this.DeviceMessageTextBlock.Name = "DeviceMessageTextBlock";
            this.DeviceMessageTextBlock.Size = new System.Drawing.Size(35, 13);
            this.DeviceMessageTextBlock.TabIndex = 10;
            this.DeviceMessageTextBlock.Text = "label3";
            // 
            // stop
            // 
            this.stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stop.Location = new System.Drawing.Point(922, 504);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(264, 23);
            this.stop.TabIndex = 11;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // brBar
            // 
            this.brBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.brBar.Location = new System.Drawing.Point(922, 167);
            this.brBar.Name = "brBar";
            this.brBar.Size = new System.Drawing.Size(264, 45);
            this.brBar.TabIndex = 12;
            this.brBar.Scroll += new System.EventHandler(this.brBar_Scroll);
            // 
            // br
            // 
            this.br.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.br.AutoSize = true;
            this.br.Location = new System.Drawing.Point(922, 151);
            this.br.Name = "br";
            this.br.Size = new System.Drawing.Size(56, 13);
            this.br.TabIndex = 13;
            this.br.Text = "Brightness";
            // 
            // contrast
            // 
            this.contrast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contrast.AutoSize = true;
            this.contrast.Location = new System.Drawing.Point(922, 199);
            this.contrast.Name = "contrast";
            this.contrast.Size = new System.Drawing.Size(46, 13);
            this.contrast.TabIndex = 14;
            this.contrast.Text = "Contrast";
            // 
            // contrastBar
            // 
            this.contrastBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.contrastBar.Location = new System.Drawing.Point(922, 215);
            this.contrastBar.Name = "contrastBar";
            this.contrastBar.Size = new System.Drawing.Size(264, 45);
            this.contrastBar.TabIndex = 15;
            // 
            // gamma
            // 
            this.gamma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gamma.AutoSize = true;
            this.gamma.Location = new System.Drawing.Point(922, 247);
            this.gamma.Name = "gamma";
            this.gamma.Size = new System.Drawing.Size(43, 13);
            this.gamma.TabIndex = 16;
            this.gamma.Text = "Gamma";
            // 
            // gammaBar
            // 
            this.gammaBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gammaBar.Location = new System.Drawing.Point(922, 266);
            this.gammaBar.Name = "gammaBar";
            this.gammaBar.Size = new System.Drawing.Size(264, 45);
            this.gammaBar.TabIndex = 17;
            // 
            // saturation
            // 
            this.saturation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saturation.AutoSize = true;
            this.saturation.Location = new System.Drawing.Point(922, 298);
            this.saturation.Name = "saturation";
            this.saturation.Size = new System.Drawing.Size(55, 13);
            this.saturation.TabIndex = 18;
            this.saturation.Text = "Saturation";
            // 
            // saturationBar
            // 
            this.saturationBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saturationBar.Location = new System.Drawing.Point(922, 317);
            this.saturationBar.Name = "saturationBar";
            this.saturationBar.Size = new System.Drawing.Size(264, 45);
            this.saturationBar.TabIndex = 19;
            // 
            // mfocusBar
            // 
            this.mfocusBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mfocusBar.Location = new System.Drawing.Point(925, 368);
            this.mfocusBar.Name = "mfocusBar";
            this.mfocusBar.Size = new System.Drawing.Size(261, 45);
            this.mfocusBar.TabIndex = 20;
            this.mfocusBar.Scroll += new System.EventHandler(this.mfocusBar_Scroll);
            // 
            // mfocus
            // 
            this.mfocus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mfocus.AutoSize = true;
            this.mfocus.Location = new System.Drawing.Point(925, 348);
            this.mfocus.Name = "mfocus";
            this.mfocus.Size = new System.Drawing.Size(74, 13);
            this.mfocus.TabIndex = 21;
            this.mfocus.Text = "Manual Focus";
            // 
            // FocusChanged
            // 
            this.FocusChanged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FocusChanged.AutoSize = true;
            this.FocusChanged.Location = new System.Drawing.Point(679, 577);
            this.FocusChanged.Name = "FocusChanged";
            this.FocusChanged.Size = new System.Drawing.Size(33, 13);
            this.FocusChanged.TabIndex = 22;
            this.FocusChanged.Text = "focus";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 597);
            this.Controls.Add(this.FocusChanged);
            this.Controls.Add(this.mfocus);
            this.Controls.Add(this.mfocusBar);
            this.Controls.Add(this.saturationBar);
            this.Controls.Add(this.saturation);
            this.Controls.Add(this.gammaBar);
            this.Controls.Add(this.gamma);
            this.Controls.Add(this.contrastBar);
            this.Controls.Add(this.contrast);
            this.Controls.Add(this.br);
            this.Controls.Add(this.brBar);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.DeviceMessageTextBlock);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "GDLI40-FFBM";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mfocusBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button preview;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label DeviceMessageTextBlock;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.TrackBar brBar;
        private System.Windows.Forms.Label br;
        private System.Windows.Forms.Label contrast;
        private System.Windows.Forms.TrackBar contrastBar;
        private System.Windows.Forms.Label gamma;
        private System.Windows.Forms.TrackBar gammaBar;
        private System.Windows.Forms.Label saturation;
        private System.Windows.Forms.TrackBar saturationBar;
        private System.Windows.Forms.TrackBar mfocusBar;
        private System.Windows.Forms.Label mfocus;
        private System.Windows.Forms.Label FocusChanged;
    }
}

