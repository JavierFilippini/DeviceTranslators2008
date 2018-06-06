namespace ManagedHandHeldTracker
{
    partial class frmEventinfoValidAccess
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
            this.grupoMain = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblMask = new System.Windows.Forms.Label();
            this.pictureMap = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.picEmp = new System.Windows.Forms.PictureBox();
            this.picEvent = new System.Windows.Forms.PictureBox();
            this.lblAccesstype = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblReader = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblHHID = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblbadge = new System.Windows.Forms.Label();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.lblApellido = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.grupoMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEvent)).BeginInit();
            this.SuspendLayout();
            // 
            // grupoMain
            // 
            this.grupoMain.Controls.Add(this.lblTitulo);
            this.grupoMain.Controls.Add(this.btnrefresh);
            this.grupoMain.Controls.Add(this.lblInfo);
            this.grupoMain.Controls.Add(this.lblMask);
            this.grupoMain.Controls.Add(this.pictureMap);
            this.grupoMain.Controls.Add(this.button1);
            this.grupoMain.Controls.Add(this.label13);
            this.grupoMain.Controls.Add(this.label12);
            this.grupoMain.Controls.Add(this.picEmp);
            this.grupoMain.Controls.Add(this.picEvent);
            this.grupoMain.Controls.Add(this.lblAccesstype);
            this.grupoMain.Controls.Add(this.label22);
            this.grupoMain.Controls.Add(this.lblReader);
            this.grupoMain.Controls.Add(this.label21);
            this.grupoMain.Controls.Add(this.lblDateTime);
            this.grupoMain.Controls.Add(this.label17);
            this.grupoMain.Controls.Add(this.lblEmpresa);
            this.grupoMain.Controls.Add(this.label14);
            this.grupoMain.Controls.Add(this.lblLocation);
            this.grupoMain.Controls.Add(this.lblHHID);
            this.grupoMain.Controls.Add(this.label16);
            this.grupoMain.Controls.Add(this.lblbadge);
            this.grupoMain.Controls.Add(this.lblDocumento);
            this.grupoMain.Controls.Add(this.lblApellido);
            this.grupoMain.Controls.Add(this.lblNombre);
            this.grupoMain.Controls.Add(this.label11);
            this.grupoMain.Controls.Add(this.label10);
            this.grupoMain.Controls.Add(this.label7);
            this.grupoMain.Controls.Add(this.label9);
            this.grupoMain.Controls.Add(this.webBrowser);
            this.grupoMain.Location = new System.Drawing.Point(-2, -1);
            this.grupoMain.Name = "grupoMain";
            this.grupoMain.Size = new System.Drawing.Size(499, 459);
            this.grupoMain.TabIndex = 0;
            this.grupoMain.Paint += new System.Windows.Forms.PaintEventHandler(this.grupoMain_Paint);
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(6, 3);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(490, 25);
            this.lblTitulo.TabIndex = 163;
            this.lblTitulo.Text = "Valid Access";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnrefresh
            // 
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(194, 396);
            this.btnrefresh.Margin = new System.Windows.Forms.Padding(0);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(89, 22);
            this.btnrefresh.TabIndex = 162;
            this.btnrefresh.Text = "REFRESH MAP";
            this.btnrefresh.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(279, 280);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(100, 32);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Loading...";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMask
            // 
            this.lblMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMask.Location = new System.Drawing.Point(186, 228);
            this.lblMask.Name = "lblMask";
            this.lblMask.Size = new System.Drawing.Size(310, 203);
            this.lblMask.TabIndex = 162;
            // 
            // pictureMap
            // 
            this.pictureMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureMap.Location = new System.Drawing.Point(186, 228);
            this.pictureMap.Name = "pictureMap";
            this.pictureMap.Size = new System.Drawing.Size(310, 203);
            this.pictureMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureMap.TabIndex = 161;
            this.pictureMap.TabStop = false;
            this.pictureMap.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(421, 432);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 160;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(5, 240);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(175, 20);
            this.label13.TabIndex = 159;
            this.label13.Text = "Event Image";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(175, 20);
            this.label12.TabIndex = 158;
            this.label12.Text = "Employee";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picEmp
            // 
            this.picEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEmp.Location = new System.Drawing.Point(5, 52);
            this.picEmp.Name = "picEmp";
            this.picEmp.Size = new System.Drawing.Size(175, 185);
            this.picEmp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEmp.TabIndex = 157;
            this.picEmp.TabStop = false;
            // 
            // picEvent
            // 
            this.picEvent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEvent.Location = new System.Drawing.Point(5, 261);
            this.picEvent.Name = "picEvent";
            this.picEvent.Size = new System.Drawing.Size(175, 187);
            this.picEvent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEvent.TabIndex = 156;
            this.picEvent.TabStop = false;
            // 
            // lblAccesstype
            // 
            this.lblAccesstype.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblAccesstype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAccesstype.Location = new System.Drawing.Point(426, 130);
            this.lblAccesstype.Name = "lblAccesstype";
            this.lblAccesstype.Size = new System.Drawing.Size(70, 20);
            this.lblAccesstype.TabIndex = 155;
            this.lblAccesstype.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(379, 132);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 15);
            this.label22.TabIndex = 154;
            this.label22.Text = "Access:";
            // 
            // lblReader
            // 
            this.lblReader.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblReader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReader.Location = new System.Drawing.Point(278, 177);
            this.lblReader.Name = "lblReader";
            this.lblReader.Size = new System.Drawing.Size(218, 20);
            this.lblReader.TabIndex = 153;
            this.lblReader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(222, 177);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(51, 15);
            this.label21.TabIndex = 152;
            this.label21.Text = "Reader:";
            // 
            // lblDateTime
            // 
            this.lblDateTime.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDateTime.Location = new System.Drawing.Point(278, 200);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(218, 20);
            this.lblDateTime.TabIndex = 151;
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(207, 200);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 15);
            this.label17.TabIndex = 150;
            this.label17.Text = "Date/Time:";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblEmpresa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEmpresa.Location = new System.Drawing.Point(278, 107);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(218, 20);
            this.lblEmpresa.TabIndex = 149;
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(210, 107);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 15);
            this.label14.TabIndex = 148;
            this.label14.Text = "Company:";
            // 
            // lblLocation
            // 
            this.lblLocation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLocation.Location = new System.Drawing.Point(186, 433);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(229, 20);
            this.lblLocation.TabIndex = 147;
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHHID
            // 
            this.lblHHID.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblHHID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHHID.Location = new System.Drawing.Point(278, 154);
            this.lblHHID.Name = "lblHHID";
            this.lblHHID.Size = new System.Drawing.Size(218, 20);
            this.lblHHID.TabIndex = 146;
            this.lblHHID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(231, 157);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 15);
            this.label16.TabIndex = 145;
            this.label16.Text = "Panel:";
            // 
            // lblbadge
            // 
            this.lblbadge.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblbadge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblbadge.Location = new System.Drawing.Point(278, 130);
            this.lblbadge.Name = "lblbadge";
            this.lblbadge.Size = new System.Drawing.Size(101, 20);
            this.lblbadge.TabIndex = 144;
            this.lblbadge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDocumento
            // 
            this.lblDocumento.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblDocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDocumento.Location = new System.Drawing.Point(278, 83);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(218, 20);
            this.lblDocumento.TabIndex = 143;
            this.lblDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblApellido
            // 
            this.lblApellido.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblApellido.Location = new System.Drawing.Point(278, 59);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(218, 20);
            this.lblApellido.TabIndex = 142;
            this.lblApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNombre
            // 
            this.lblNombre.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNombre.Location = new System.Drawing.Point(278, 34);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(218, 20);
            this.lblNombre.TabIndex = 141;
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(227, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 15);
            this.label11.TabIndex = 140;
            this.label11.Text = "Badge:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(225, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 16);
            this.label10.TabIndex = 139;
            this.label10.Text = "SSNO:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(206, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 15);
            this.label7.TabIndex = 137;
            this.label7.Text = "Last name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(203, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 15);
            this.label9.TabIndex = 138;
            this.label9.Text = "First Name:";
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(186, 228);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(310, 202);
            this.webBrowser.TabIndex = 136;
            // 
            // frmEventinfoValidAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 456);
            this.ControlBox = false;
            this.Controls.Add(this.grupoMain);
            this.MinimizeBox = false;
            this.Name = "frmEventinfoValidAccess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEventinfoValidAccess";
            this.Activated += new System.EventHandler(this.frmEventinfoValidAccess_Activated);
            this.Load += new System.EventHandler(this.frmEventinfoValidAccess_Load);
            this.grupoMain.ResumeLayout(false);
            this.grupoMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEvent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel grupoMain;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox picEmp;
        private System.Windows.Forms.PictureBox picEvent;
        private System.Windows.Forms.Label lblAccesstype;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblReader;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblHHID;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblbadge;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.PictureBox pictureMap;
        private System.Windows.Forms.Label lblMask;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Label lblTitulo;

    }
}