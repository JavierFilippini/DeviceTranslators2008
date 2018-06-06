namespace ManagedHandHeldTracker
{
    partial class frmPersonasMustering
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
            this.listViewPersonas = new System.Windows.Forms.ListView();
            this.btnClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewPersonas
            // 
            this.listViewPersonas.Location = new System.Drawing.Point(12, 70);
            this.listViewPersonas.Name = "listViewPersonas";
            this.listViewPersonas.Size = new System.Drawing.Size(497, 411);
            this.listViewPersonas.TabIndex = 1;
            this.listViewPersonas.UseCompatibleStateImageBehavior = false;
            this.listViewPersonas.View = System.Windows.Forms.View.Details;
            this.listViewPersonas.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listViewPersonas_DrawColumnHeader);
            this.listViewPersonas.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listViewPersonas_DrawItem);
            this.listViewPersonas.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listViewPersonas_DrawSubItem);
            this.listViewPersonas.SelectedIndexChanged += new System.EventHandler(this.listViewPersonas_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(434, 487);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, -1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(318, 40);
            this.label2.TabIndex = 5;
            this.label2.Text = "Detailed Mustering Information";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(8, 39);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(330, 28);
            this.lblTitulo.TabIndex = 6;
            this.lblTitulo.Text = "Employees on site: Zone Default 1";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmPersonasMustering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 527);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.listViewPersonas);
            this.Name = "frmPersonasMustering";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detailed Mustering";
            this.Activated += new System.EventHandler(this.frmPersonasMustering_Activated);
            this.Load += new System.EventHandler(this.frmPersonasMustering_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewPersonas;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitulo;
    }
}