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
    public partial class frmLogin : Form
    {
// Accessors para leer y modificar controles internos.
        
        public string txtUsuario
        {
            get { return txtUser.Text; }
            set { txtUser.Text = value; }
        }

        public string txtPassword
        {
            get { return txtPwd.Text; }
            set { txtPwd.Text = value; }
        }


        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Tag = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Tag = false;
            this.Close();
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(sender, e);

        }
    }
}
