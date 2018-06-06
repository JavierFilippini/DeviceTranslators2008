namespace ManagedHandHeldTracker
{
    partial class frmAddTarjetaMaster
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBadge = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbVZone = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbSalida = new System.Windows.Forms.RadioButton();
            this.rdbEntrada = new System.Windows.Forms.RadioButton();
            this.lblBadge = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(238, 141);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 141;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(165, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 23);
            this.btnCancel.TabIndex = 140;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBadge
            // 
            this.txtBadge.Location = new System.Drawing.Point(119, 24);
            this.txtBadge.Name = "txtBadge";
            this.txtBadge.Size = new System.Drawing.Size(194, 20);
            this.txtBadge.TabIndex = 138;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 26);
            this.label1.TabIndex = 136;
            this.label1.Text = "Badge:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 26);
            this.label3.TabIndex = 142;
            this.label3.Text = "Virtual Zone:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbVZone
            // 
            this.cmbVZone.FormattingEnabled = true;
            this.cmbVZone.Location = new System.Drawing.Point(119, 59);
            this.cmbVZone.Name = "cmbVZone";
            this.cmbVZone.Size = new System.Drawing.Size(194, 21);
            this.cmbVZone.TabIndex = 144;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 26);
            this.label2.TabIndex = 137;
            this.label2.Text = "Type:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdbSalida
            // 
            this.rdbSalida.AutoSize = true;
            this.rdbSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbSalida.Location = new System.Drawing.Point(188, 93);
            this.rdbSalida.Name = "rdbSalida";
            this.rdbSalida.Size = new System.Drawing.Size(45, 19);
            this.rdbSalida.TabIndex = 147;
            this.rdbSalida.TabStop = true;
            this.rdbSalida.Text = "Exit";
            this.rdbSalida.UseVisualStyleBackColor = true;
            // 
            // rdbEntrada
            // 
            this.rdbEntrada.AutoSize = true;
            this.rdbEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbEntrada.Location = new System.Drawing.Point(119, 93);
            this.rdbEntrada.Name = "rdbEntrada";
            this.rdbEntrada.Size = new System.Drawing.Size(52, 19);
            this.rdbEntrada.TabIndex = 146;
            this.rdbEntrada.TabStop = true;
            this.rdbEntrada.Text = "Entry";
            this.rdbEntrada.UseVisualStyleBackColor = true;
            // 
            // lblBadge
            // 
            this.lblBadge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBadge.Location = new System.Drawing.Point(119, 24);
            this.lblBadge.Name = "lblBadge";
            this.lblBadge.Size = new System.Drawing.Size(194, 19);
            this.lblBadge.TabIndex = 148;
            this.lblBadge.Text = "label4";
            // 
            // frmAddTarjetaMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 175);
            this.Controls.Add(this.rdbSalida);
            this.Controls.Add(this.rdbEntrada);
            this.Controls.Add(this.cmbVZone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBadge);
            this.Controls.Add(this.lblBadge);
            this.Name = "frmAddTarjetaMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add new Master Badge";
            this.Load += new System.EventHandler(this.frmAddTarjetaMaster_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.RadioButton rdbSalida;
        public System.Windows.Forms.RadioButton rdbEntrada;
        public System.Windows.Forms.TextBox txtBadge;
        public System.Windows.Forms.ComboBox cmbVZone;
        public System.Windows.Forms.Label lblBadge;
    }
}