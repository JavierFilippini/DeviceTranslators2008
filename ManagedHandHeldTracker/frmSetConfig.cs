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
    public partial class frmSetConfig : Form
    {
        public frmSetConfig()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Tag = false;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int speed = 0;
            int GPSTime = 0;

            if (int.TryParse(txtmaxSpeed.Text,out speed))
                if (speed > 0)
                    if (int.TryParse(txtGPSUpdate.Text, out GPSTime))
                        if(GPSTime>0)
                        {
                            this.Tag = true;
                            this.Close();
                            return;
                        }

            MessageBox.Show("Some invalid inputs, rewrite and try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void frmSetMaxSpeed_Load(object sender, EventArgs e)
        {
            Tag = false;
        }

        private void txtmaxSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnOK_Click(null, null);
        }
    }
}
