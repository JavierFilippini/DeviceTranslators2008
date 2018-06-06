using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagedHandHeldTracker
{
    public partial class frmUpdateIMEI : Form
    {
        public frmManageIMEI frmMain; 
        public string prevIMEI = "";
        public frmUpdateIMEI()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtIMEI.Text.Trim()))
            {
                MessageBox.Show("Invalid IMEI");
            }
            else
            {
                if (txtIMEI.Text.ToUpper().Trim().Equals(prevIMEI.ToUpper()))
                {
                    this.Tag = true;
                    this.Close();
                }
                if (frmMain.ExisteIMEI(txtIMEI.Text.Trim()))
                {
                    MessageBox.Show("Invalid IMEI: already exists in the system");
                }
                else
                {
                    this.Tag = true;
                    this.Close();
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Tag = false;
            this.Close();
        }

        private void frmUpdateIMEI_Load(object sender, EventArgs e)
        {
            
        }

        private void frmUpdateIMEI_Activated(object sender, EventArgs e)
        {
            txtIMEI.Select();
            txtIMEI.Focus();
        }

        private void txtIMEI_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIMEI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(sender, e);

            if (e.KeyCode == Keys.Escape)
                btnCancel_Click(sender, e);

        }

    }
}
