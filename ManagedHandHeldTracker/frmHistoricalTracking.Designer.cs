namespace ManagedHandHeldTracker
{
    partial class frmHistoricalTracking
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
            this.components = new System.ComponentModel.Container();
            this.endTime = new System.Windows.Forms.DateTimePicker();
            this.startTime = new System.Windows.Forms.DateTimePicker();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDevice = new System.Windows.Forms.ComboBox();
            this.btnStartTracking = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.trackingBar = new System.Windows.Forms.TrackBar();
            this.btnPauseContinue = new System.Windows.Forms.Button();
            this.tmrTracking = new System.Windows.Forms.Timer(this.components);
            this.btnStop = new System.Windows.Forms.Button();
            this.chkAutoCenter = new System.Windows.Forms.CheckBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnPor2 = new System.Windows.Forms.Button();
            this.btnPor4 = new System.Windows.Forms.Button();
            this.btnPor8 = new System.Windows.Forms.Button();
            this.btnPor1 = new System.Windows.Forms.Button();
            this.lblTool = new System.Windows.Forms.Label();
            this.btnPor16 = new System.Windows.Forms.Button();
            this.btnPor32 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackingBar)).BeginInit();
            this.SuspendLayout();
            // 
            // endTime
            // 
            this.endTime.CustomFormat = "HH:mm:ss";
            this.endTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endTime.Location = new System.Drawing.Point(310, 54);
            this.endTime.Name = "endTime";
            this.endTime.ShowUpDown = true;
            this.endTime.Size = new System.Drawing.Size(92, 22);
            this.endTime.TabIndex = 150;
            this.endTime.Value = new System.DateTime(2015, 1, 19, 17, 53, 0, 0);
            // 
            // startTime
            // 
            this.startTime.CustomFormat = "HH:mm:ss";
            this.startTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTime.Location = new System.Drawing.Point(310, 18);
            this.startTime.Name = "startTime";
            this.startTime.ShowUpDown = true;
            this.startTime.Size = new System.Drawing.Size(92, 22);
            this.startTime.TabIndex = 149;
            this.startTime.Value = new System.DateTime(2015, 1, 19, 17, 51, 0, 0);
            // 
            // endDate
            // 
            this.endDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endDate.Location = new System.Drawing.Point(55, 54);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(249, 22);
            this.endDate.TabIndex = 148;
            this.endDate.Value = new System.DateTime(2010, 3, 22, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(9, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 26);
            this.label8.TabIndex = 147;
            this.label8.Text = "End:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(9, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 26);
            this.label6.TabIndex = 146;
            this.label6.Text = "Start:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startDate
            // 
            this.startDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startDate.Location = new System.Drawing.Point(55, 18);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(249, 22);
            this.startDate.TabIndex = 145;
            this.startDate.Value = new System.DateTime(2010, 3, 22, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(729, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 151;
            this.label1.Text = "Device:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDevice
            // 
            this.cmbDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDevice.FormattingEnabled = true;
            this.cmbDevice.Location = new System.Drawing.Point(794, 14);
            this.cmbDevice.Name = "cmbDevice";
            this.cmbDevice.Size = new System.Drawing.Size(188, 24);
            this.cmbDevice.TabIndex = 152;
            this.cmbDevice.SelectedIndexChanged += new System.EventHandler(this.cmbDevice_SelectedIndexChanged);
            // 
            // btnStartTracking
            // 
            this.btnStartTracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartTracking.Location = new System.Drawing.Point(794, 41);
            this.btnStartTracking.Name = "btnStartTracking";
            this.btnStartTracking.Size = new System.Drawing.Size(188, 33);
            this.btnStartTracking.TabIndex = 153;
            this.btnStartTracking.Text = "Start Tracking";
            this.btnStartTracking.UseVisualStyleBackColor = true;
            this.btnStartTracking.Click += new System.EventHandler(this.btnStartTracking_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 80);
            this.webBrowser.MinimumSize = new System.Drawing.Size(10, 10);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(970, 347);
            this.webBrowser.TabIndex = 154;
            // 
            // trackingBar
            // 
            this.trackingBar.Enabled = false;
            this.trackingBar.Location = new System.Drawing.Point(8, 476);
            this.trackingBar.Margin = new System.Windows.Forms.Padding(0);
            this.trackingBar.Name = "trackingBar";
            this.trackingBar.Size = new System.Drawing.Size(831, 45);
            this.trackingBar.TabIndex = 155;
            this.trackingBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackingBar.Scroll += new System.EventHandler(this.trackingBar_Scroll);
            this.trackingBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackingBar_MouseUp);
            // 
            // btnPauseContinue
            // 
            this.btnPauseContinue.Enabled = false;
            this.btnPauseContinue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPauseContinue.Location = new System.Drawing.Point(845, 453);
            this.btnPauseContinue.Name = "btnPauseContinue";
            this.btnPauseContinue.Size = new System.Drawing.Size(73, 33);
            this.btnPauseContinue.TabIndex = 156;
            this.btnPauseContinue.Text = "Pause";
            this.btnPauseContinue.UseVisualStyleBackColor = true;
            this.btnPauseContinue.Click += new System.EventHandler(this.btnPauseContinue_Click);
            // 
            // tmrTracking
            // 
            this.tmrTracking.Interval = 1000;
            this.tmrTracking.Tick += new System.EventHandler(this.tmrTracking_Tick);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(924, 453);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(57, 33);
            this.btnStop.TabIndex = 157;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // chkAutoCenter
            // 
            this.chkAutoCenter.Checked = true;
            this.chkAutoCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCenter.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.chkAutoCenter.Location = new System.Drawing.Point(450, 430);
            this.chkAutoCenter.Margin = new System.Windows.Forms.Padding(0);
            this.chkAutoCenter.Name = "chkAutoCenter";
            this.chkAutoCenter.Size = new System.Drawing.Size(94, 23);
            this.chkAutoCenter.TabIndex = 158;
            this.chkAutoCenter.Text = "Auto Center";
            this.chkAutoCenter.UseVisualStyleBackColor = true;
            // 
            // lblLocation
            // 
            this.lblLocation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLocation.Location = new System.Drawing.Point(12, 453);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(827, 20);
            this.lblLocation.TabIndex = 159;
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(845, 494);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(136, 32);
            this.btnExit.TabIndex = 160;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(22, 401);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnrefresh.Size = new System.Drawing.Size(98, 16);
            this.btnrefresh.TabIndex = 161;
            this.btnrefresh.Text = "REFRESH MAP";
            this.btnrefresh.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnPor2
            // 
            this.btnPor2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPor2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPor2.Location = new System.Drawing.Point(303, 506);
            this.btnPor2.Name = "btnPor2";
            this.btnPor2.Size = new System.Drawing.Size(58, 20);
            this.btnPor2.TabIndex = 162;
            this.btnPor2.Text = "X2";
            this.btnPor2.UseVisualStyleBackColor = true;
            this.btnPor2.Click += new System.EventHandler(this.btnPor2_Click);
            // 
            // btnPor4
            // 
            this.btnPor4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPor4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPor4.Location = new System.Drawing.Point(367, 506);
            this.btnPor4.Name = "btnPor4";
            this.btnPor4.Size = new System.Drawing.Size(58, 20);
            this.btnPor4.TabIndex = 163;
            this.btnPor4.Text = "X4";
            this.btnPor4.UseVisualStyleBackColor = true;
            this.btnPor4.Click += new System.EventHandler(this.btnPor4_Click);
            // 
            // btnPor8
            // 
            this.btnPor8.BackColor = System.Drawing.SystemColors.Control;
            this.btnPor8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPor8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPor8.Location = new System.Drawing.Point(431, 506);
            this.btnPor8.Name = "btnPor8";
            this.btnPor8.Size = new System.Drawing.Size(58, 20);
            this.btnPor8.TabIndex = 164;
            this.btnPor8.Text = "X8";
            this.btnPor8.UseVisualStyleBackColor = false;
            this.btnPor8.Click += new System.EventHandler(this.btnPor8_Click);
            // 
            // btnPor1
            // 
            this.btnPor1.BackColor = System.Drawing.Color.LightGreen;
            this.btnPor1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPor1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPor1.Location = new System.Drawing.Point(239, 506);
            this.btnPor1.Name = "btnPor1";
            this.btnPor1.Size = new System.Drawing.Size(58, 20);
            this.btnPor1.TabIndex = 165;
            this.btnPor1.Text = "X1";
            this.btnPor1.UseVisualStyleBackColor = false;
            this.btnPor1.Click += new System.EventHandler(this.btnPor1_Click);
            // 
            // lblTool
            // 
            this.lblTool.AutoSize = true;
            this.lblTool.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTool.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTool.Location = new System.Drawing.Point(748, 61);
            this.lblTool.Name = "lblTool";
            this.lblTool.Size = new System.Drawing.Size(40, 15);
            this.lblTool.TabIndex = 166;
            this.lblTool.Text = "lblTool";
            this.lblTool.Visible = false;
            // 
            // btnPor16
            // 
            this.btnPor16.BackColor = System.Drawing.SystemColors.Control;
            this.btnPor16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPor16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPor16.Location = new System.Drawing.Point(495, 506);
            this.btnPor16.Name = "btnPor16";
            this.btnPor16.Size = new System.Drawing.Size(58, 20);
            this.btnPor16.TabIndex = 168;
            this.btnPor16.Text = "X16";
            this.btnPor16.UseVisualStyleBackColor = false;
            this.btnPor16.Click += new System.EventHandler(this.btnPor16_Click);
            // 
            // btnPor32
            // 
            this.btnPor32.BackColor = System.Drawing.SystemColors.Control;
            this.btnPor32.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPor32.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPor32.Location = new System.Drawing.Point(559, 506);
            this.btnPor32.Name = "btnPor32";
            this.btnPor32.Size = new System.Drawing.Size(58, 20);
            this.btnPor32.TabIndex = 169;
            this.btnPor32.Text = "X32";
            this.btnPor32.UseVisualStyleBackColor = false;
            this.btnPor32.Click += new System.EventHandler(this.btnPor32_Click);
            // 
            // frmHistoricalTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 533);
            this.Controls.Add(this.btnPor32);
            this.Controls.Add(this.btnPor16);
            this.Controls.Add(this.lblTool);
            this.Controls.Add(this.btnPor1);
            this.Controls.Add(this.btnPor8);
            this.Controls.Add(this.btnPor4);
            this.Controls.Add(this.btnPor2);
            this.Controls.Add(this.btnrefresh);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.chkAutoCenter);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPauseContinue);
            this.Controls.Add(this.trackingBar);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.btnStartTracking);
            this.Controls.Add(this.cmbDevice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endTime);
            this.Controls.Add(this.startTime);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.startDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmHistoricalTracking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historical Tracking";
            this.Activated += new System.EventHandler(this.frmHistoricalTracking_Activated);
            this.Load += new System.EventHandler(this.frmHistoricalTracking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackingBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker endTime;
        private System.Windows.Forms.DateTimePicker startTime;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDevice;
        private System.Windows.Forms.Button btnStartTracking;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TrackBar trackingBar;
        private System.Windows.Forms.Button btnPauseContinue;
        private System.Windows.Forms.Timer tmrTracking;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox chkAutoCenter;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnPor2;
        private System.Windows.Forms.Button btnPor4;
        private System.Windows.Forms.Button btnPor8;
        private System.Windows.Forms.Button btnPor1;
        private System.Windows.Forms.Label lblTool;
        private System.Windows.Forms.Button btnPor16;
        private System.Windows.Forms.Button btnPor32;
    }
}