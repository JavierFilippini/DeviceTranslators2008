using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ManagedHandHeldTracker
{
    public partial class frmExtendedFeatures : Form
    {
        public int ORGID;       // OrgID para identificar los mensajes
        public string DEVICEID; // DeviceID sobre el que se hizo el click 

        public frmExtendedFeatures()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHistTracking_Click(object sender, EventArgs e)
        {
            string listaHH_GPS = "";
            int cantRety = 3;

            while (String.IsNullOrEmpty(listaHH_GPS) && cantRety >= 0)
            {
                listaHH_GPS = Tools.GetInstance().cargarHHGPS(ORGID.ToString());

                if (String.IsNullOrEmpty(listaHH_GPS))
                {
                    Tools.GetInstance().DoLog("Lista de HHGPS vacia. Retrying..." + cantRety.ToString());

                }
                cantRety--;
                Application.DoEvents();
                Thread.Sleep(100);
                Application.DoEvents();
                Thread.Sleep(100);
            }

            if (!String.IsNullOrEmpty(listaHH_GPS))
            {
                //MessageBox.Show("listaHH_GPS: " + listaHH_GPS);

                frmHistoricalTracking ventana = new frmHistoricalTracking();
                //ventana.IP = StaticCustomOptionsManager.IP;
                //ventana.PORT = StaticCustomOptionsManager.PORT1;
                //ventana.ORGID = StaticCustomOptionsManager.MainOrgID; 
                ventana.DEVICEID = DEVICEID;
                ventana.listaDevices = listaHH_GPS;

                ventana.ShowDialog();
                ventana.Dispose();
            }
            else
            {
                MessageBox.Show("Server not available. Retry in a few seconds.");
            }

        }

        private void btnDeviceConfig_Click(object sender, EventArgs e)
        {
            string listaDeviceConfig = "";

            listaDeviceConfig = Tools.GetInstance().cargarDeviceConfig(ORGID.ToString());
            if (String.IsNullOrEmpty(listaDeviceConfig))
            {
                Tools.GetInstance().DoLog("ATENCION: Lista de HHGPS_MAXSPEED vacia");

            }

            if (!String.IsNullOrEmpty(listaDeviceConfig))
            {
                frmDefineConfig ventana = new frmDefineConfig();
                ventana.ORGID = Tools.GetInstance().MainOrgID;
                ventana.DEVICEID = DEVICEID;
                ventana.listaDevicesMaxSpeed = listaDeviceConfig;
                //Tools.GetInstance().DoLog("ListaHHGPS: " + listaHH_GPS_MAXSpeed);
                ventana.ShowDialog();
                ventana.Dispose();
            }
            else
            {
                MessageBox.Show("Server not available. Retry in a few seconds.");
            }
        }

        private void btnManageIMEI_Click(object sender, EventArgs e)
        {
            frmManageIMEI ventana = new frmManageIMEI();
            ventana.ORGID = Tools.GetInstance().MainOrgID;
            ventana.ShowDialog();
            ventana.Dispose();
        }

        private void btnManageDeviceUsers_Click(object sender, EventArgs e)
        {
            frmManageDeviceUsers ventana = new frmManageDeviceUsers();
            //ventana.ORGID = Tools.GetInstance().MainOrgID;
            ventana.ShowDialog();
            ventana.Dispose();
        }

        private void btnMasterBadges_Click(object sender, EventArgs e)
        {

            frmTarjetasMaster ventana = new frmTarjetasMaster();
            ventana.OrgID= Tools.GetInstance().MainOrgID;
            ventana.ShowDialog();
            ventana.Dispose();
        }
    }
}
