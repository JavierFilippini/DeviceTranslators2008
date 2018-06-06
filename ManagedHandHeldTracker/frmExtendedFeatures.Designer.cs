namespace ManagedHandHeldTracker
{
    partial class frmExtendedFeatures
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
            this.btnHistTracking = new System.Windows.Forms.Button();
            this.btnDeviceConfig = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnManageDeviceUsers = new System.Windows.Forms.Button();
            this.btnManageIMEI = new System.Windows.Forms.Button();
            this.btnMasterBadges = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHistTracking
            // 
            this.btnHistTracking.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHistTracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistTracking.Location = new System.Drawing.Point(38, 26);
            this.btnHistTracking.Name = "btnHistTracking";
            this.btnHistTracking.Size = new System.Drawing.Size(184, 29);
            this.btnHistTracking.TabIndex = 0;
            this.btnHistTracking.Text = "Historical Tracking";
            this.btnHistTracking.UseVisualStyleBackColor = true;
            this.btnHistTracking.Click += new System.EventHandler(this.btnHistTracking_Click);
            // 
            // btnDeviceConfig
            // 
            this.btnDeviceConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeviceConfig.Location = new System.Drawing.Point(38, 82);
            this.btnDeviceConfig.Name = "btnDeviceConfig";
            this.btnDeviceConfig.Size = new System.Drawing.Size(184, 29);
            this.btnDeviceConfig.TabIndex = 1;
            this.btnDeviceConfig.Text = "Bulk Configuration";
            this.btnDeviceConfig.UseVisualStyleBackColor = true;
            this.btnDeviceConfig.Click += new System.EventHandler(this.btnDeviceConfig_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(195, 340);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(83, 27);
            this.button5.TabIndex = 5;
            this.button5.Text = "Close";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(9, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(272, 327);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnMasterBadges);
            this.tabPage1.Controls.Add(this.btnManageDeviceUsers);
            this.tabPage1.Controls.Add(this.btnManageIMEI);
            this.tabPage1.Controls.Add(this.btnHistTracking);
            this.tabPage1.Controls.Add(this.btnDeviceConfig);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(264, 301);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnManageDeviceUsers
            // 
            this.btnManageDeviceUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageDeviceUsers.Location = new System.Drawing.Point(38, 194);
            this.btnManageDeviceUsers.Name = "btnManageDeviceUsers";
            this.btnManageDeviceUsers.Size = new System.Drawing.Size(184, 29);
            this.btnManageDeviceUsers.TabIndex = 3;
            this.btnManageDeviceUsers.Text = "Manage Device Users";
            this.btnManageDeviceUsers.UseVisualStyleBackColor = true;
            this.btnManageDeviceUsers.Click += new System.EventHandler(this.btnManageDeviceUsers_Click);
            // 
            // btnManageIMEI
            // 
            this.btnManageIMEI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageIMEI.Location = new System.Drawing.Point(38, 138);
            this.btnManageIMEI.Name = "btnManageIMEI";
            this.btnManageIMEI.Size = new System.Drawing.Size(184, 29);
            this.btnManageIMEI.TabIndex = 2;
            this.btnManageIMEI.Text = "Manage IMEI";
            this.btnManageIMEI.UseVisualStyleBackColor = true;
            this.btnManageIMEI.Click += new System.EventHandler(this.btnManageIMEI_Click);
            // 
            // btnMasterBadges
            // 
            this.btnMasterBadges.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMasterBadges.Location = new System.Drawing.Point(38, 250);
            this.btnMasterBadges.Name = "btnMasterBadges";
            this.btnMasterBadges.Size = new System.Drawing.Size(184, 29);
            this.btnMasterBadges.TabIndex = 4;
            this.btnMasterBadges.Text = "Manage Master Badges";
            this.btnMasterBadges.UseVisualStyleBackColor = true;
            this.btnMasterBadges.Click += new System.EventHandler(this.btnMasterBadges_Click);
            // 
            // frmExtendedFeatures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 371);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExtendedFeatures";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extended features";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHistTracking;
        private System.Windows.Forms.Button btnDeviceConfig;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnManageIMEI;
        private System.Windows.Forms.Button btnManageDeviceUsers;
        private System.Windows.Forms.Button btnMasterBadges;
    }
}