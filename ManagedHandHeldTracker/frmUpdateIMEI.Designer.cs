namespace ManagedHandHeldTracker
{
    partial class frmUpdateIMEI
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblTituloUpdate = new System.Windows.Forms.Label();
            this.txtIMEI = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(41, 105);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(292, 105);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Confirm";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblTituloUpdate
            // 
            this.lblTituloUpdate.Location = new System.Drawing.Point(12, 9);
            this.lblTituloUpdate.Name = "lblTituloUpdate";
            this.lblTituloUpdate.Size = new System.Drawing.Size(409, 25);
            this.lblTituloUpdate.TabIndex = 2;
            this.lblTituloUpdate.Text = "label1";
            this.lblTituloUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtIMEI
            // 
            this.txtIMEI.Location = new System.Drawing.Point(41, 56);
            this.txtIMEI.Name = "txtIMEI";
            this.txtIMEI.Size = new System.Drawing.Size(351, 22);
            this.txtIMEI.TabIndex = 3;
            this.txtIMEI.TextChanged += new System.EventHandler(this.txtIMEI_TextChanged);
            this.txtIMEI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIMEI_KeyDown);
            // 
            // frmUpdateIMEI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 146);
            this.ControlBox = false;
            this.Controls.Add(this.txtIMEI);
            this.Controls.Add(this.lblTituloUpdate);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmUpdateIMEI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update IMEI";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.frmUpdateIMEI_Activated);
            this.Load += new System.EventHandler(this.frmUpdateIMEI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.TextBox txtIMEI;
        public System.Windows.Forms.Label lblTituloUpdate;
    }
}