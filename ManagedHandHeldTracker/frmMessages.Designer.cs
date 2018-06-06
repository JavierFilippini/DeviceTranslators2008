namespace ManagedHandHeldTracker
{
    partial class frmMessages
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
            this.lblBanner = new System.Windows.Forms.Label();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMens = new System.Windows.Forms.TextBox();
            this.btnMore = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBanner
            // 
            this.lblBanner.BackColor = System.Drawing.Color.Black;
            this.lblBanner.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.lblBanner.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanner.ForeColor = System.Drawing.Color.White;
            this.lblBanner.Location = new System.Drawing.Point(2, 5);
            this.lblBanner.Name = "lblBanner";
            this.lblBanner.Size = new System.Drawing.Size(243, 21);
            this.lblBanner.TabIndex = 131;
            this.lblBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMessages
            // 
            this.txtMessages.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessages.Location = new System.Drawing.Point(3, 29);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ReadOnly = true;
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(335, 259);
            this.txtMessages.TabIndex = 132;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(225, 294);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(54, 35);
            this.btnSend.TabIndex = 133;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMens
            // 
            this.txtMens.Location = new System.Drawing.Point(3, 294);
            this.txtMens.Multiline = true;
            this.txtMens.Name = "txtMens";
            this.txtMens.Size = new System.Drawing.Size(216, 35);
            this.txtMens.TabIndex = 134;
            this.txtMens.TextChanged += new System.EventHandler(this.txtMens_TextChanged);
            // 
            // btnMore
            // 
            this.btnMore.Location = new System.Drawing.Point(251, 6);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(87, 20);
            this.btnMore.TabIndex = 135;
            this.btnMore.Text = "Previous";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(280, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 35);
            this.button1.TabIndex = 136;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 330);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.txtMens);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.lblBanner);
            this.Name = "frmMessages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Messages";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMessages_FormClosing);
            this.Load += new System.EventHandler(this.frmMessages_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBanner;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMens;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.Button button1;
    }
}