namespace ManagedHandHeldTracker
{
    partial class frmMustering
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
            this.listviewZonas = new System.Windows.Forms.ListView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDetails = new System.Windows.Forms.Button();
            this.rdbEN = new System.Windows.Forms.RadioButton();
            this.rdbES = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listviewZonas
            // 
            this.listviewZonas.Location = new System.Drawing.Point(12, 48);
            this.listviewZonas.Name = "listviewZonas";
            this.listviewZonas.Size = new System.Drawing.Size(328, 416);
            this.listviewZonas.TabIndex = 0;
            this.listviewZonas.UseCompatibleStateImageBehavior = false;
            this.listviewZonas.View = System.Windows.Forms.View.Details;
            this.listviewZonas.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listviewZonas_DrawColumnHeader);
            this.listviewZonas.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listviewZonas_DrawItem);
            this.listviewZonas.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listviewZonas_DrawSubItem);
            this.listviewZonas.SelectedIndexChanged += new System.EventHandler(this.listviewZonas_SelectedIndexChanged);
            this.listviewZonas.DoubleClick += new System.EventHandler(this.listviewZonas_DoubleClick);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(265, 495);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(12, 495);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(89, 28);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export PDF";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.Location = new System.Drawing.Point(123, 434);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(118, 23);
            this.lblLoading.TabIndex = 3;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoading.Click += new System.EventHandler(this.lblLoading_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "General Mustering Information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Enabled = false;
            this.btnDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetails.Location = new System.Drawing.Point(136, 495);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(89, 28);
            this.btnDetails.TabIndex = 5;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // rdbEN
            // 
            this.rdbEN.AutoSize = true;
            this.rdbEN.Location = new System.Drawing.Point(127, 469);
            this.rdbEN.Name = "rdbEN";
            this.rdbEN.Size = new System.Drawing.Size(40, 17);
            this.rdbEN.TabIndex = 6;
            this.rdbEN.TabStop = true;
            this.rdbEN.Text = "EN";
            this.rdbEN.UseVisualStyleBackColor = true;
            this.rdbEN.CheckedChanged += new System.EventHandler(this.rdbEN_CheckedChanged);
            // 
            // rdbES
            // 
            this.rdbES.AutoSize = true;
            this.rdbES.Location = new System.Drawing.Point(174, 469);
            this.rdbES.Name = "rdbES";
            this.rdbES.Size = new System.Drawing.Size(39, 17);
            this.rdbES.TabIndex = 7;
            this.rdbES.TabStop = true;
            this.rdbES.Text = "ES";
            this.rdbES.UseVisualStyleBackColor = true;
            this.rdbES.CheckedChanged += new System.EventHandler(this.rdbES_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 467);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Datetime Format :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMustering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 530);
            this.Controls.Add(this.rdbES);
            this.Controls.Add(this.rdbEN);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.listviewZonas);
            this.Controls.Add(this.label2);
            this.Name = "frmMustering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mustering";
            this.Load += new System.EventHandler(this.frmMustering_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listviewZonas;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.RadioButton rdbEN;
        private System.Windows.Forms.RadioButton rdbES;
        private System.Windows.Forms.Label label2;
    }
}