namespace ManagedHandHeldTracker
{
    partial class frmLiveTracking
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.chkAutoCenter = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.tmrUpdatePosition = new System.Windows.Forms.Timer(this.components);
            this.lblLocation = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblNodata = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(953, 26);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Live Tracking";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(638, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 21);
            this.label2.TabIndex = 89;
            this.label2.Text = "Last info received:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDateTime
            // 
            this.lblDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.Location = new System.Drawing.Point(807, 52);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(158, 19);
            this.lblDateTime.TabIndex = 90;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 74);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(953, 382);
            this.webBrowser1.TabIndex = 91;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // chkAutoCenter
            // 
            this.chkAutoCenter.AutoSize = true;
            this.chkAutoCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoCenter.Location = new System.Drawing.Point(451, 488);
            this.chkAutoCenter.Name = "chkAutoCenter";
            this.chkAutoCenter.Size = new System.Drawing.Size(93, 20);
            this.chkAutoCenter.TabIndex = 96;
            this.chkAutoCenter.Text = "AutoCenter";
            this.chkAutoCenter.UseVisualStyleBackColor = true;
            this.chkAutoCenter.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(863, 485);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 26);
            this.btnOK.TabIndex = 97;
            this.btnOK.Text = "Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tmrUpdatePosition
            // 
            this.tmrUpdatePosition.Interval = 1500;
            this.tmrUpdatePosition.Tick += new System.EventHandler(this.tmrUpdatePosition_Tick);
            // 
            // lblLocation
            // 
            this.lblLocation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLocation.Location = new System.Drawing.Point(12, 459);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(953, 20);
            this.lblLocation.TabIndex = 120;
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(17, 484);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(136, 27);
            this.btnRefresh.TabIndex = 121;
            this.btnRefresh.Text = "Refresh Map";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblNodata
            // 
            this.lblNodata.AutoSize = true;
            this.lblNodata.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNodata.Location = new System.Drawing.Point(448, 245);
            this.lblNodata.Name = "lblNodata";
            this.lblNodata.Size = new System.Drawing.Size(130, 20);
            this.lblNodata.TabIndex = 122;
            this.lblNodata.Text = "No data available";
            this.lblNodata.Visible = false;
            // 
            // frmLiveTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 523);
            this.Controls.Add(this.lblNodata);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkAutoCenter);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitulo);
            this.KeyPreview = true;
            this.Name = "frmLiveTracking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Live Tracking";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Click += new System.EventHandler(this.Form2_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form2_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.CheckBox chkAutoCenter;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer tmrUpdatePosition;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblNodata;
    }
}