namespace ManagedHandHeldTracker
{
    partial class frmZoneDefinition
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
            this.lblZoneName = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.btnAddPoints = new System.Windows.Forms.Button();
            this.listViewGates = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnZoomMas = new System.Windows.Forms.Button();
            this.btnZoomMenos = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.tmrLoadZone = new System.Windows.Forms.Timer(this.components);
            this.btnZoomToFit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbExit = new System.Windows.Forms.ComboBox();
            this.cmbEntrance = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkTrigger = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCancelNew = new System.Windows.Forms.Button();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.btnGeocodeSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblZoneName
            // 
            this.lblZoneName.BackColor = System.Drawing.SystemColors.Control;
            this.lblZoneName.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.lblZoneName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoneName.Location = new System.Drawing.Point(217, 11);
            this.lblZoneName.Margin = new System.Windows.Forms.Padding(0);
            this.lblZoneName.Name = "lblZoneName";
            this.lblZoneName.Size = new System.Drawing.Size(418, 26);
            this.lblZoneName.TabIndex = 134;
            this.lblZoneName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblZoneName.Click += new System.EventHandler(this.lblZoneName_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOK.Location = new System.Drawing.Point(924, 494);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 26);
            this.btnOK.TabIndex = 130;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.Location = new System.Drawing.Point(854, 494);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64, 26);
            this.btnCancel.TabIndex = 129;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 26);
            this.label1.TabIndex = 128;
            this.label1.Text = "Virtual Zone definition:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 115);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(611, 325);
            this.webBrowser.TabIndex = 127;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.DocumentCompleted);
            // 
            // btnAddPoints
            // 
            this.btnAddPoints.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAddPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPoints.Location = new System.Drawing.Point(12, 58);
            this.btnAddPoints.Name = "btnAddPoints";
            this.btnAddPoints.Size = new System.Drawing.Size(59, 26);
            this.btnAddPoints.TabIndex = 125;
            this.btnAddPoints.Text = "New";
            this.btnAddPoints.UseVisualStyleBackColor = false;
            this.btnAddPoints.Click += new System.EventHandler(this.btnAddPoints_Click);
            // 
            // listViewGates
            // 
            this.listViewGates.FullRowSelect = true;
            this.listViewGates.GridLines = true;
            this.listViewGates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewGates.Location = new System.Drawing.Point(642, 85);
            this.listViewGates.Name = "listViewGates";
            this.listViewGates.Size = new System.Drawing.Size(346, 359);
            this.listViewGates.TabIndex = 140;
            this.listViewGates.UseCompatibleStateImageBehavior = false;
            this.listViewGates.SelectedIndexChanged += new System.EventHandler(this.listViewGates_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(638, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 139;
            this.label3.Text = "Segments";
            // 
            // btnZoomMas
            // 
            this.btnZoomMas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomMas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomMas.Location = new System.Drawing.Point(272, 446);
            this.btnZoomMas.Name = "btnZoomMas";
            this.btnZoomMas.Size = new System.Drawing.Size(24, 22);
            this.btnZoomMas.TabIndex = 141;
            this.btnZoomMas.Text = "+";
            this.btnZoomMas.UseVisualStyleBackColor = false;
            this.btnZoomMas.Click += new System.EventHandler(this.btnZoomMas_Click);
            // 
            // btnZoomMenos
            // 
            this.btnZoomMenos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomMenos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomMenos.Location = new System.Drawing.Point(400, 446);
            this.btnZoomMenos.Name = "btnZoomMenos";
            this.btnZoomMenos.Size = new System.Drawing.Size(24, 22);
            this.btnZoomMenos.TabIndex = 142;
            this.btnZoomMenos.Text = "-";
            this.btnZoomMenos.UseVisualStyleBackColor = false;
            this.btnZoomMenos.Click += new System.EventHandler(this.btnZoomMenos_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(149, 58);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(130, 26);
            this.btnCreate.TabIndex = 143;
            this.btnCreate.Text = "Confirm Definition";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Visible = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // tmrLoadZone
            // 
            this.tmrLoadZone.Interval = 2000;
            this.tmrLoadZone.Tick += new System.EventHandler(this.tmrLoadZone_Tick);
            // 
            // btnZoomToFit
            // 
            this.btnZoomToFit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomToFit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomToFit.Location = new System.Drawing.Point(302, 446);
            this.btnZoomToFit.Name = "btnZoomToFit";
            this.btnZoomToFit.Size = new System.Drawing.Size(92, 22);
            this.btnZoomToFit.TabIndex = 145;
            this.btnZoomToFit.Text = "Zoom to Zone";
            this.btnZoomToFit.UseVisualStyleBackColor = false;
            this.btnZoomToFit.Click += new System.EventHandler(this.btnZoomToFit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(638, 467);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 146;
            this.label2.Text = "Readers:";
            // 
            // cmbExit
            // 
            this.cmbExit.Enabled = false;
            this.cmbExit.FormattingEnabled = true;
            this.cmbExit.Location = new System.Drawing.Point(854, 467);
            this.cmbExit.Name = "cmbExit";
            this.cmbExit.Size = new System.Drawing.Size(132, 21);
            this.cmbExit.TabIndex = 147;
            this.cmbExit.SelectedIndexChanged += new System.EventHandler(this.cmbExit_SelectedIndexChanged);
            // 
            // cmbEntrance
            // 
            this.cmbEntrance.Enabled = false;
            this.cmbEntrance.FormattingEnabled = true;
            this.cmbEntrance.Location = new System.Drawing.Point(708, 467);
            this.cmbEntrance.Name = "cmbEntrance";
            this.cmbEntrance.Size = new System.Drawing.Size(142, 21);
            this.cmbEntrance.TabIndex = 148;
            this.cmbEntrance.SelectedIndexChanged += new System.EventHandler(this.cmbEntrance_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(705, 447);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 149;
            this.label5.Text = "Entrance:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(851, 447);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 17);
            this.label6.TabIndex = 150;
            this.label6.Text = "Exit:";
            // 
            // chkTrigger
            // 
            this.chkTrigger.AutoSize = true;
            this.chkTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrigger.Location = new System.Drawing.Point(508, 58);
            this.chkTrigger.Name = "chkTrigger";
            this.chkTrigger.Size = new System.Drawing.Size(115, 20);
            this.chkTrigger.TabIndex = 151;
            this.chkTrigger.Text = "Trigger Events";
            this.chkTrigger.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(14, 445);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(59, 22);
            this.btnRefresh.TabIndex = 152;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCancelNew
            // 
            this.btnCancelNew.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelNew.Location = new System.Drawing.Point(80, 58);
            this.btnCancelNew.Name = "btnCancelNew";
            this.btnCancelNew.Size = new System.Drawing.Size(59, 26);
            this.btnCancelNew.TabIndex = 153;
            this.btnCancelNew.Text = "Cancel";
            this.btnCancelNew.UseVisualStyleBackColor = false;
            this.btnCancelNew.Visible = false;
            this.btnCancelNew.Click += new System.EventHandler(this.btnCancelNew_Click);
            // 
            // txtPosition
            // 
            this.txtPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPosition.Location = new System.Drawing.Point(12, 87);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(494, 21);
            this.txtPosition.TabIndex = 154;
            this.txtPosition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPosition_KeyDown);
            // 
            // btnGeocodeSearch
            // 
            this.btnGeocodeSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnGeocodeSearch.Location = new System.Drawing.Point(512, 85);
            this.btnGeocodeSearch.Name = "btnGeocodeSearch";
            this.btnGeocodeSearch.Size = new System.Drawing.Size(111, 26);
            this.btnGeocodeSearch.TabIndex = 129;
            this.btnGeocodeSearch.Text = "Search Location";
            this.btnGeocodeSearch.UseVisualStyleBackColor = false;
            this.btnGeocodeSearch.Click += new System.EventHandler(this.btnGeoCodeSearch_click);
            // 
            // frmZoneDefinition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 526);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.btnCancelNew);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.chkTrigger);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbEntrance);
            this.Controls.Add(this.cmbExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnZoomToFit);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnZoomMenos);
            this.Controls.Add(this.btnZoomMas);
            this.Controls.Add(this.listViewGates);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblZoneName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnGeocodeSearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.btnAddPoints);
            this.Name = "frmZoneDefinition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zone definition";
            this.Activated += new System.EventHandler(this.frmZoneDefinition_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmZoneDefinition_FormClosing);
            this.Load += new System.EventHandler(this.frmZoneDefinition_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblZoneName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button btnAddPoints;
        private System.Windows.Forms.ListView listViewGates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnZoomMas;
        private System.Windows.Forms.Button btnZoomMenos;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Timer tmrLoadZone;
        private System.Windows.Forms.Button btnZoomToFit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbExit;
        private System.Windows.Forms.ComboBox cmbEntrance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkTrigger;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCancelNew;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Button btnGeocodeSearch;
    }
}