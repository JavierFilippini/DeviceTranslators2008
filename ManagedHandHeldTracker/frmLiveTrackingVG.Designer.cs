namespace ManagedHandHeldTracker
{
    partial class frmLiveTrackingVG
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
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.lblVirtualZone = new System.Windows.Forms.Label();
            this.chkAutoCenter = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tmrInicializacion = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cmbZones = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnZoomToFit = new System.Windows.Forms.Button();
            this.btnZoomMenos = new System.Windows.Forms.Button();
            this.btnZoomMas = new System.Windows.Forms.Button();
            this.chkTrigger = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.listViewDevices = new System.Windows.Forms.ListView();
            this.button3 = new System.Windows.Forms.Button();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnMustering = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 78);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(723, 405);
            this.webBrowser.TabIndex = 92;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // lblVirtualZone
            // 
            this.lblVirtualZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVirtualZone.Location = new System.Drawing.Point(12, 7);
            this.lblVirtualZone.Name = "lblVirtualZone";
            this.lblVirtualZone.Size = new System.Drawing.Size(956, 26);
            this.lblVirtualZone.TabIndex = 94;
            this.lblVirtualZone.Text = "Live tracking Virtual Zone";
            this.lblVirtualZone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkAutoCenter
            // 
            this.chkAutoCenter.AutoSize = true;
            this.chkAutoCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoCenter.Location = new System.Drawing.Point(642, 489);
            this.chkAutoCenter.Name = "chkAutoCenter";
            this.chkAutoCenter.Size = new System.Drawing.Size(93, 20);
            this.chkAutoCenter.TabIndex = 97;
            this.chkAutoCenter.Text = "AutoCenter";
            this.chkAutoCenter.UseVisualStyleBackColor = true;
            this.chkAutoCenter.CheckedChanged += new System.EventHandler(this.chkAutoCenter_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(902, 489);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 24);
            this.btnClose.TabIndex = 98;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tmrInicializacion
            // 
            this.tmrInicializacion.Interval = 1500;
            this.tmrInicializacion.Tick += new System.EventHandler(this.tmrUpdatePositions_Tick);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 21);
            this.label2.TabIndex = 99;
            this.label2.Text = "Zone:";
            // 
            // cmbZones
            // 
            this.cmbZones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbZones.FormattingEnabled = true;
            this.cmbZones.Location = new System.Drawing.Point(60, 49);
            this.cmbZones.Name = "cmbZones";
            this.cmbZones.Size = new System.Drawing.Size(437, 23);
            this.cmbZones.TabIndex = 100;
            this.cmbZones.SelectedIndexChanged += new System.EventHandler(this.cmbZones_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(748, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 21);
            this.label1.TabIndex = 101;
            this.label1.Text = "Assigned Devices";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnZoomToFit
            // 
            this.btnZoomToFit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomToFit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomToFit.Location = new System.Drawing.Point(362, 489);
            this.btnZoomToFit.Name = "btnZoomToFit";
            this.btnZoomToFit.Size = new System.Drawing.Size(90, 24);
            this.btnZoomToFit.TabIndex = 152;
            this.btnZoomToFit.Text = "FIT";
            this.btnZoomToFit.UseVisualStyleBackColor = false;
            this.btnZoomToFit.Click += new System.EventHandler(this.btnZoomToFit_Click_1);
            // 
            // btnZoomMenos
            // 
            this.btnZoomMenos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomMenos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomMenos.Location = new System.Drawing.Point(458, 489);
            this.btnZoomMenos.Name = "btnZoomMenos";
            this.btnZoomMenos.Size = new System.Drawing.Size(28, 24);
            this.btnZoomMenos.TabIndex = 151;
            this.btnZoomMenos.Text = "-";
            this.btnZoomMenos.UseVisualStyleBackColor = false;
            this.btnZoomMenos.Click += new System.EventHandler(this.btnZoomMenos_Click_1);
            // 
            // btnZoomMas
            // 
            this.btnZoomMas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomMas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomMas.Location = new System.Drawing.Point(327, 489);
            this.btnZoomMas.Name = "btnZoomMas";
            this.btnZoomMas.Size = new System.Drawing.Size(29, 24);
            this.btnZoomMas.TabIndex = 150;
            this.btnZoomMas.Text = "+";
            this.btnZoomMas.UseVisualStyleBackColor = false;
            this.btnZoomMas.Click += new System.EventHandler(this.btnZoomMas_Click);
            // 
            // chkTrigger
            // 
            this.chkTrigger.AutoSize = true;
            this.chkTrigger.Enabled = false;
            this.chkTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrigger.Location = new System.Drawing.Point(512, 53);
            this.chkTrigger.Name = "chkTrigger";
            this.chkTrigger.Size = new System.Drawing.Size(15, 14);
            this.chkTrigger.TabIndex = 153;
            this.chkTrigger.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(526, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 21);
            this.label3.TabIndex = 154;
            this.label3.Text = "Trigger events";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnReload
            // 
            this.btnReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.Location = new System.Drawing.Point(12, 489);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(88, 23);
            this.btnReload.TabIndex = 155;
            this.btnReload.Text = "Refresh";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // listViewDevices
            // 
            this.listViewDevices.BackColor = System.Drawing.SystemColors.Window;
            this.listViewDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewDevices.FullRowSelect = true;
            this.listViewDevices.GridLines = true;
            this.listViewDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDevices.Location = new System.Drawing.Point(748, 78);
            this.listViewDevices.Margin = new System.Windows.Forms.Padding(0);
            this.listViewDevices.MultiSelect = false;
            this.listViewDevices.Name = "listViewDevices";
            this.listViewDevices.Size = new System.Drawing.Size(227, 379);
            this.listViewDevices.TabIndex = 156;
            this.listViewDevices.TileSize = new System.Drawing.Size(227, 18);
            this.listViewDevices.UseCompatibleStateImageBehavior = false;
            this.listViewDevices.View = System.Windows.Forms.View.Tile;
            this.listViewDevices.SelectedIndexChanged += new System.EventHandler(this.listViewDevices_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(951, 460);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 23);
            this.button3.TabIndex = 159;
            this.button3.Text = "X";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtFiltro
            // 
            this.txtFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltro.Location = new System.Drawing.Point(798, 460);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(147, 23);
            this.txtFiltro.TabIndex = 158;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(741, 466);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 157;
            this.label4.Text = "Search:";
            // 
            // btnMustering
            // 
            this.btnMustering.Enabled = false;
            this.btnMustering.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMustering.Location = new System.Drawing.Point(634, 47);
            this.btnMustering.Name = "btnMustering";
            this.btnMustering.Size = new System.Drawing.Size(103, 24);
            this.btnMustering.TabIndex = 160;
            this.btnMustering.Text = "Mustering";
            this.btnMustering.UseVisualStyleBackColor = true;
            this.btnMustering.Click += new System.EventHandler(this.btnMustering_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.Location = new System.Drawing.Point(337, 238);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(66, 16);
            this.lblLoading.TabIndex = 161;
            this.lblLoading.Text = "Loading...";
            // 
            // frmLiveTrackingVG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 523);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.btnMustering);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtFiltro);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listViewDevices);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkTrigger);
            this.Controls.Add(this.btnZoomToFit);
            this.Controls.Add(this.btnZoomMenos);
            this.Controls.Add(this.btnZoomMas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbZones);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chkAutoCenter);
            this.Controls.Add(this.lblVirtualZone);
            this.Controls.Add(this.webBrowser);
            this.Name = "frmLiveTrackingVG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Live tracking Virtual Zone";
            this.Activated += new System.EventHandler(this.fromLiveTrackingVG_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fromLiveTrackingVG_FormClosing);
            this.Load += new System.EventHandler(this.fromLiveTrackingVG_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVirtualZone;
        private System.Windows.Forms.CheckBox chkAutoCenter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer tmrInicializacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbZones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnZoomToFit;
        private System.Windows.Forms.Button btnZoomMenos;
        private System.Windows.Forms.Button btnZoomMas;
        private System.Windows.Forms.CheckBox chkTrigger;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReload;
        internal System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ListView listViewDevices;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnMustering;
        private System.Windows.Forms.Label lblLoading;
    }
}