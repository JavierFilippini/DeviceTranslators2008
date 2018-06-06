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
    public partial class frmAddDeviceUser : Form
    {
        public frmManageDeviceUsers mainForm;
        public frmAddDeviceUser()
        {
            InitializeComponent();
        }

        public KeyValuePair<Employee, Tarjeta> datosEmpleado = new KeyValuePair<Employee, Tarjeta>(null, null);

        private void btnBadgeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBadgeSearch.Text.Trim()))
                {
                    string errDesc = "";
                    int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                    datosEmpleado = WebServiceAPI.GetInstance().ObtenerDatosEmpleadoYTarjeta(txtBadgeSearch.Text.Trim(), out errDesc, out errCode);
                    if ((datosEmpleado.Key != null) && (datosEmpleado.Value != null))
                    {
                        lblNombre.Text = datosEmpleado.Key.Nombre;
                        lblApellido.Text = datosEmpleado.Key.Apellido;
                        lblCI.Text = datosEmpleado.Key.NumeroDocumento;
                        lblTarjeta.Text = datosEmpleado.Value.tarjeta;
                        btnOk.Enabled = true;
                        //searchedEmployee = datosEmpleado.Key;
                        //searchedBadge = datosEmpleado.Value;

                        if (datosEmpleado.Key.imageDataBytes != null)
                        {
                            picSearchedEmp.Image = Tools.GetInstance().ByteToImage(datosEmpleado.Key.imageDataBytes, datosEmpleado.Key.imageDataBytes.Length);
                        }
                        else
                            picSearchedEmp.Image = null;

                    }
                    else
                    {
                        MessageBox.Show("Cardholder associated to the badge: " + txtBadgeSearch.Text + " not found.");
                        btnOk.Enabled = false;
                    }
                }
                else
                    MessageBox.Show("Please insert a valid badge");
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion al buscar empleado: " + ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string errDesc = "";
            int errCode = -1;

            // Da de alta el Device User en AlutelMobility antes 
            if ((datosEmpleado.Key != null) && (datosEmpleado.Value != null))
            {
                WebServiceAPI.GetInstance().AddDeviceUser(datosEmpleado.Key, datosEmpleado.Value, out errDesc, out errCode);
                mainForm.isLoaded = false;
                this.Close();
            }
            else
            {
                Tools.GetInstance().DoLog("No se envia AddDeviceUser al pulsar OK porque el empleado o la tarjeta son NULL");
            }
        }

        private void frmAddDeviceUser_Load(object sender, EventArgs e)
        {

        }

        private void frmUpdateIMEI_Activated(object sender, EventArgs e)
        {
            txtBadgeSearch.Select();
            txtBadgeSearch.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
