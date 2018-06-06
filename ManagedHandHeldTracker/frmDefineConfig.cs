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
    public partial class frmDefineConfig : Form
    {
        public int ORGID;                   // OrgID para identificar los mensajes
        public string DEVICEID;             // DeviceID sobre el que se hizo el click 

        public string listaDevicesMaxSpeed = "";    //Parametro con la info de todos los HH y GPS
        public string contenidoMapa = "";   // El mapa del browser

        List<string> lstHH = null;          // Lista de nombres de HH
        List<string> lstGPS = null;         // lista de nombre,id de GPS


        bool isLoaded = false;
        bool somethingChanged =false;       // Fara el confirm del Cancel
        List<string> devicesCambiados = new List<string>();

        public frmDefineConfig()
        {
            InitializeComponent();
        }

        private void btnModifyConfig_Click(object sender, EventArgs e)
        {
            frmSetConfig ventanaConfig = new frmSetConfig();
            if (listViewDevices.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Please select at least one Device", "Warning");
                return;
            }
            if (listViewDevices.SelectedIndices.Count == 1)
            {
                ventanaConfig.lblTitle.Text = listViewDevices.SelectedItems[0].Text;
                ventanaConfig.txtmaxSpeed.Text = listViewDevices.SelectedItems[0].SubItems[1].Text;
                ventanaConfig.txtGPSUpdate.Text = listViewDevices.SelectedItems[0].SubItems[2].Text;

            }
            else
            {
                ventanaConfig.lblTitle.Text = listViewDevices.SelectedItems.Count + " selected devices";
                
                // Si todas las speed y todos los GPS de los seleccionados coinciden, se la precargo a la textbox
                string speedRef = listViewDevices.SelectedItems[0].SubItems[1].Text;
                string GPSRef = listViewDevices.SelectedItems[0].SubItems[2].Text;
                bool cambio = false;
                foreach (ListViewItem l in listViewDevices.SelectedItems)
                {
                    if ((!l.SubItems[1].Text.Equals(speedRef)) || (!l.SubItems[2].Text.Equals(GPSRef)))
                    {
                        cambio = true;
                        break;
                    }
                }

                if (!cambio)
                    ventanaConfig.txtmaxSpeed.Text = speedRef;

            }

            ventanaConfig.ShowDialog();

            if ((bool)ventanaConfig.Tag == true)
            {
                definirDeviceConfig(ventanaConfig.txtmaxSpeed.Text, ventanaConfig.txtGPSUpdate.Text);
            }

            ventanaConfig.Dispose();
        }

        private void definirDeviceConfig(string maxSpeed, string GPSUpdate)
        {
            foreach (ListViewItem item in listViewDevices.SelectedItems)
            {
                string deviceName = item.Text;
                if (!devicesCambiados.Contains(deviceName))
                    devicesCambiados.Add(deviceName);

                item.SubItems[1].Text = maxSpeed;
                item.SubItems[2].Text = GPSUpdate;
            }

            somethingChanged = true;
            btnSelectNone_Click(null,null);


        }


        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewDevices.Items.Count; i++)
            {
                listViewDevices.Items[i].Selected = true;
            }
            listViewDevices.Select();
        }

        private void frmMaxSpeed_Load(object sender, EventArgs e)
        {

        }

        private void frmMaxSpeed_Activated(object sender, EventArgs e)
        {
            cargaInicial();
        }



        private void cargaInicial()
        {
            try
            {
                if (!isLoaded)
                {
                    isLoaded = true;
                    updateListViewDevices();
                }
            }
            catch (Exception) { /*MessageBox.Show("Error en cargaInicial"); */}
        }

        private void updateListViewDevices()
        {
            try
            {
                listViewDevices.View = View.Details;
                listViewDevices.GridLines = true;
                listViewDevices.Columns.Clear();
                //listViewDevices.Columns.Add("Type", 70, HorizontalAlignment.Left);
                //listViewDevices.Columns.Add("Name", 125, HorizontalAlignment.Left);
                listViewDevices.Columns.Add("Name", 140, HorizontalAlignment.Left);
                listViewDevices.Columns.Add("Speed Limit", 70, HorizontalAlignment.Left);
                listViewDevices.Columns.Add("GPS Time", 70, HorizontalAlignment.Left);

                this.listViewDevices.MultiSelect = true;
                this.listViewDevices.HideSelection = false;
                this.listViewDevices.HeaderStyle = ColumnHeaderStyle.Clickable;

                //Tools.GetInstance().DoLog("listaDevicesMaxSpeed=" + listaDevicesMaxSpeed);
                string[] listaDevTypes = listaDevicesMaxSpeed.Split(';');       // Dos grupos : HH y GPS. Los carga antes de invocar a esta ventana

                string listaHHMaxSpeed = listaDevTypes[0];
                //string listaGPSMaxSpeed = listaDevTypes[1];

                string[] arrHH = listaHHMaxSpeed.Split(',');                // NombreHH,maxSpeed,gpsUpdateTime
                lstHH = new List<string>();

                if (arrHH.Length > 1)
                {
                    for (int i = 0; i < arrHH.Length; i = i + 3)
                    {
                        lstHH.Add(arrHH[i] + "," + arrHH[i + 1] + "," + arrHH[i+2]);  // En cada elemento queda: NombreHH,maxSpeed,gpsUpdateTime, en ese orden
                    }

                    lstHH.Sort();       // Queda ordenado por Nombre
                }
                //string[] arrGPS = listaGPSMaxSpeed.Split(',');

                //lstGPS = new List<string>();
                //if (arrGPS.Length > 2)
                //{
                //    for (int i = 0; i < arrGPS.Length; i = i + 3)             // movilID,Nombre,maxSpeed
                //    {
                //        lstGPS.Add(arrGPS[i + 1] + "," + arrGPS[i] + "," + arrGPS[i + 2]);   // En cada elemento queda: NombreGPS,idMovil,maxSpeed. En ese orden
                //    }

                //    lstGPS.Sort();                                            // Lista de Nombre,idMovil,maxSpeed ordenada por nombre.
                //}

                foreach (string s in lstHH)
                {
                    if (!String.IsNullOrEmpty(s))
                    {
                        //ListViewItem list = listViewDevices.Items.Add(TiposDevices.DEVICE.ToString());
                        ListViewItem list = listViewDevices.Items.Add(s.Split(',')[0]);
                        list.UseItemStyleForSubItems = false;
                        //list.SubItems.Add(s.Split(',')[0]);
                        list.SubItems.Add(s.Split(',')[1]);
                        list.SubItems.Add(s.Split(',')[2]);
                    }
                }

            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en updateListViewDevices: " + ex.Message);

            }

        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewDevices.Items.Count; i++)
            {
                listViewDevices.Items[i].Selected = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!somethingChanged)
                this.Dispose();
            else
            {
                DialogResult res = MessageBox.Show("You will loose all your chages. Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    this.Dispose();
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (updateDeviceConfig())
                this.Dispose();
            else
            {
                MessageBox.Show("Can't update data on server. Please Retry operation", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Envia los datos de configracion del device al Server: Device, maxSpeed, GPSUpdateTime,....
        /// </summary>
        /// <returns></returns>
        private bool updateDeviceConfig()
        {
            string strConfigData = "";
            
            foreach (ListViewItem item in listViewDevices.Items)
            {
                //string tipo = item.Text;
                string nombredevice = item.Text;
                if (devicesCambiados.Contains(nombredevice))
                {
                    string maxSpeed = item.SubItems[1].Text;
                    string GPSUpdate = item.SubItems[2].Text;
                    strConfigData += nombredevice + "," + maxSpeed + "," + GPSUpdate + ",";
                }
            }

            strConfigData = strConfigData.TrimEnd(',');
 
            // Listo. Ultimo paso: enviar los datos al server, con retry

            int cantRetry = 3;
            bool resSend = false;
            while (!resSend && cantRetry >= 0)
            {
                resSend = Tools.GetInstance().enviarHHConfig(strConfigData);

                if (!resSend)
                {
                    Tools.GetInstance().DoLog("Error al enviar UpdateServerData data. Retrying..." + cantRetry.ToString());
                }
                cantRetry--;
                Application.DoEvents();
                Thread.Sleep(100);
                Application.DoEvents();
                Thread.Sleep(100);
            }

            return resSend;

        }

        private void listViewDevices_DoubleClick(object sender, EventArgs e)
        {
            btnModifyConfig_Click(null, null);
        }

        private void listViewDevices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
