namespace ManagedHandHeldTracker
{
    partial class frmGateDefinition
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
            this.btnZoomToFit = new System.Windows.Forms.Button();
            this.btnZoomMenos = new System.Windows.Forms.Button();
            this.btnZoomMas = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.listViewGates = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rdbEntrance = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbForbidden = new System.Windows.Forms.RadioButton();
            this.rdbGranted = new System.Windows.Forms.RadioButton();
            this.rdbExit = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrLoadZone = new System.Windows.Forms.Timer(this.components);
            this.chkLockMap = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnZoomToFit
            // 
            this.btnZoomToFit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomToFit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomToFit.Location = new System.Drawing.Point(157, 415);
            this.btnZoomToFit.Name = "btnZoomToFit";
            this.btnZoomToFit.Size = new System.Drawing.Size(52, 22);
            this.btnZoomToFit.TabIndex = 149;
            this.btnZoomToFit.Text = "FIT";
            this.btnZoomToFit.UseVisualStyleBackColor = false;
            this.btnZoomToFit.Click += new System.EventHandler(this.btnZoomToFit_Click);
            // 
            // btnZoomMenos
            // 
            this.btnZoomMenos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomMenos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomMenos.Location = new System.Drawing.Point(234, 415);
            this.btnZoomMenos.Name = "btnZoomMenos";
            this.btnZoomMenos.Size = new System.Drawing.Size(24, 22);
            this.btnZoomMenos.TabIndex = 148;
            this.btnZoomMenos.Text = "-";
            this.btnZoomMenos.UseVisualStyleBackColor = false;
            this.btnZoomMenos.Click += new System.EventHandler(this.btnZoomMenos_Click);
            // 
            // btnZoomMas
            // 
            this.btnZoomMas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnZoomMas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZoomMas.Location = new System.Drawing.Point(108, 415);
            this.btnZoomMas.Name = "btnZoomMas";
            this.btnZoomMas.Size = new System.Drawing.Size(24, 22);
            this.btnZoomMas.TabIndex = 147;
            this.btnZoomMas.Text = "+";
            this.btnZoomMas.UseVisualStyleBackColor = false;
            this.btnZoomMas.Click += new System.EventHandler(this.btnZoomMas_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(23, 50);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(344, 346);
            this.webBrowser.TabIndex = 146;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // listViewGates
            // 
            this.listViewGates.FullRowSelect = true;
            this.listViewGates.GridLines = true;
            this.listViewGates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewGates.Location = new System.Drawing.Point(373, 50);
            this.listViewGates.Name = "listViewGates";
            this.listViewGates.Size = new System.Drawing.Size(207, 241);
            this.listViewGates.TabIndex = 151;
            this.listViewGates.UseCompatibleStateImageBehavior = false;
            this.listViewGates.SelectedIndexChanged += new System.EventHandler(this.listViewGates_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(369, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 150;
            this.label3.Text = "Gates";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOK.Location = new System.Drawing.Point(500, 415);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 26);
            this.btnOK.TabIndex = 153;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.Location = new System.Drawing.Point(414, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 26);
            this.btnCancel.TabIndex = 152;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdbEntrance
            // 
            this.rdbEntrance.AutoSize = true;
            this.rdbEntrance.Location = new System.Drawing.Point(8, 34);
            this.rdbEntrance.Name = "rdbEntrance";
            this.rdbEntrance.Size = new System.Drawing.Size(85, 22);
            this.rdbEntrance.TabIndex = 154;
            this.rdbEntrance.Text = "Entrance";
            this.rdbEntrance.UseVisualStyleBackColor = true;
            this.rdbEntrance.CheckedChanged += new System.EventHandler(this.rdbEntrance_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbForbidden);
            this.groupBox1.Controls.Add(this.rdbGranted);
            this.groupBox1.Controls.Add(this.rdbExit);
            this.groupBox1.Controls.Add(this.rdbEntrance);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(373, 297);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 99);
            this.groupBox1.TabIndex = 155;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gate Type";
            // 
            // rdbForbidden
            // 
            this.rdbForbidden.AutoSize = true;
            this.rdbForbidden.Location = new System.Drawing.Point(109, 62);
            this.rdbForbidden.Name = "rdbForbidden";
            this.rdbForbidden.Size = new System.Drawing.Size(92, 22);
            this.rdbForbidden.TabIndex = 157;
            this.rdbForbidden.Text = "Forbidden";
            this.rdbForbidden.UseVisualStyleBackColor = true;
            this.rdbForbidden.CheckedChanged += new System.EventHandler(this.rdbForbidden_CheckedChanged);
            // 
            // rdbGranted
            // 
            this.rdbGranted.AutoSize = true;
            this.rdbGranted.Location = new System.Drawing.Point(109, 34);
            this.rdbGranted.Name = "rdbGranted";
            this.rdbGranted.Size = new System.Drawing.Size(79, 22);
            this.rdbGranted.TabIndex = 156;
            this.rdbGranted.Text = "Granted";
            this.rdbGranted.UseVisualStyleBackColor = true;
            this.rdbGranted.CheckedChanged += new System.EventHandler(this.rdbGranted_CheckedChanged);
            // 
            // rdbExit
            // 
            this.rdbExit.AutoSize = true;
            this.rdbExit.Location = new System.Drawing.Point(8, 62);
            this.rdbExit.Name = "rdbExit";
            this.rdbExit.Size = new System.Drawing.Size(50, 22);
            this.rdbExit.TabIndex = 155;
            this.rdbExit.Text = "Exit";
            this.rdbExit.UseVisualStyleBackColor = true;
            this.rdbExit.CheckedChanged += new System.EventHandler(this.rdbExit_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 26);
            this.label1.TabIndex = 156;
            this.label1.Text = "Gate Definition";
            // 
            // tmrLoadZone
            // 
            this.tmrLoadZone.Interval = 1000;
            this.tmrLoadZone.Tick += new System.EventHandler(this.tmrLoadZone_Tick);
            // 
            // chkLockMap
            // 
            this.chkLockMap.AutoSize = true;
            this.chkLockMap.Location = new System.Drawing.Point(23, 402);
            this.chkLockMap.Name = "chkLockMap";
            this.chkLockMap.Size = new System.Drawing.Size(74, 17);
            this.chkLockMap.TabIndex = 158;
            this.chkLockMap.Text = "Lock Map";
            this.chkLockMap.UseVisualStyleBackColor = true;
            this.chkLockMap.CheckedChanged += new System.EventHandler(this.chkLockMap_CheckedChanged);
            // 
            // frmGateDefinition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 449);
            this.Controls.Add(this.chkLockMap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listViewGates);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnZoomToFit);
            this.Controls.Add(this.btnZoomMenos);
            this.Controls.Add(this.btnZoomMas);
            this.Controls.Add(this.webBrowser);
            this.Name = "frmGateDefinition";
            this.Text = "Gate Definition";
            this.Activated += new System.EventHandler(this.frmGateDefinition_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGateDefinition_FormClosing);
            this.Load += new System.EventHandler(this.GateDefinition_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnZoomToFit;
        private System.Windows.Forms.Button btnZoomMenos;
        private System.Windows.Forms.Button btnZoomMas;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ListView listViewGates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rdbEntrance;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbForbidden;
        private System.Windows.Forms.RadioButton rdbGranted;
        private System.Windows.Forms.RadioButton rdbExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrLoadZone;
        private System.Windows.Forms.CheckBox chkLockMap;
    }
}