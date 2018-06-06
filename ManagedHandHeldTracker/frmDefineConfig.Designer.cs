namespace ManagedHandHeldTracker
{
    partial class frmDefineConfig
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
            this.listViewDevices = new System.Windows.Forms.ListView();
            this.btnModifyConfig = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewDevices
            // 
            this.listViewDevices.FullRowSelect = true;
            this.listViewDevices.Location = new System.Drawing.Point(12, 24);
            this.listViewDevices.Name = "listViewDevices";
            this.listViewDevices.Size = new System.Drawing.Size(305, 410);
            this.listViewDevices.TabIndex = 0;
            this.listViewDevices.UseCompatibleStateImageBehavior = false;
            this.listViewDevices.SelectedIndexChanged += new System.EventHandler(this.listViewDevices_SelectedIndexChanged);
            this.listViewDevices.DoubleClick += new System.EventHandler(this.listViewDevices_DoubleClick);
            // 
            // btnModifyConfig
            // 
            this.btnModifyConfig.Location = new System.Drawing.Point(323, 191);
            this.btnModifyConfig.Name = "btnModifyConfig";
            this.btnModifyConfig.Size = new System.Drawing.Size(92, 41);
            this.btnModifyConfig.TabIndex = 1;
            this.btnModifyConfig.Text = "Modify Configuration";
            this.btnModifyConfig.UseVisualStyleBackColor = true;
            this.btnModifyConfig.Click += new System.EventHandler(this.btnModifyConfig_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(323, 382);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Confirm";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(323, 24);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(92, 23);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(323, 65);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(92, 23);
            this.btnSelectNone.TabIndex = 4;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(323, 411);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmDefineConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 446);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnModifyConfig);
            this.Controls.Add(this.listViewDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDefineConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Device configuration";
            this.Activated += new System.EventHandler(this.frmMaxSpeed_Activated);
            this.Load += new System.EventHandler(this.frmMaxSpeed_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewDevices;
        private System.Windows.Forms.Button btnModifyConfig;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnCancel;

    }
}