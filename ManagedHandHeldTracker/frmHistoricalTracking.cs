using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ManagedHandHeldTracker
{
    public partial class frmHistoricalTracking : Form
    {

        public int ORGID;                   // OrgID para identificar los mensajes
        public string DEVICEID;             // DeviceID sobre el que se hizo el click 

        public string listaDevices = "";    //Parametro con la info de todos los HH y GPS
        public string contenidoMapa = "";   // El mapa del browser

        List<string> lstHH = null;          // Lista de nombres de HH
        List<string> lstGPS = null;         // lista de nombre,id de GPS
        
        bool isLoaded = false;              // flag que indica si los recursos del form se cargaron (mapa y devices)

        // VARIABLES DE LIVETRACKING
        int indice = 0;
        int indProximo = 1;
        int indBuffer = -1;
        int cantBuffers = 2;
        int cantPuntos = 10;
        bool cambio = false;

        bool bufferLoaded = false;
        bool abortHistTrack = false;

        string selectedDeviceID = "";
        string selectedDeviceType = "";

        DateTime iniRangoTracking;
        DateTime finRangoTracking;
        DateTime actualDateTime;
        DateTime lastDatetime;              // Ultimo ddatetime de un punto cargado. 

        string pauseText = "Pause";         // Para el boton pause y continue...
        string continueText = "Continue";   

        public int speedDivider = 1;            // Timer: 1000/speedDivider

        private struct PuntoGPS
        {
            public string latitud;
            public string longitud;
            public string velocidad;
            public string heading;
            public string odometer;
            public string hora;
        }

        private struct listaPuntos
        {
            public PuntoGPS[] buffer;
        }

        listaPuntos[] bufferPuntos;


        public frmHistoricalTracking()
        {
            InitializeComponent();

            //InicializarBufferPuntos();
           
        }


        private void InicializarBufferPuntos()
        {
            //Inicializacion buffers de livetracking
            bufferPuntos = new listaPuntos[cantBuffers];

            for (int i = 0; i < cantBuffers; i++)
            {
                bufferPuntos[i].buffer = new PuntoGPS[cantPuntos];
            }
            // NOTA: la forma de acceder es: bufferPuntos[1].buffer[2].latitud = "2";

        }


        private void btnStartTracking_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(selectedDeviceID))
            {
                MessageBox.Show("Please select a device to track", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            iniRangoTracking = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, startTime.Value.Hour, startTime.Value.Minute, startTime.Value.Second);

            finRangoTracking = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, endTime.Value.Hour, endTime.Value.Minute, startTime.Value.Second);
            //iniRangoTracking = startDate.Value.Add(new TimeSpan(startTime.Value.Hour,startTime.Value.Minute,startTime.Value.Second));
            //finRangoTracking = endDate.Value.Add(new TimeSpan(endTime.Value.Hour, endTime.Value.Minute, endTime.Value.Second));

            if (iniRangoTracking >= finRangoTracking)
            {
                MessageBox.Show("Start Date must be before End Date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Start: " + iniRangoTracking.ToString() + " End:" + finRangoTracking.ToString());

                return;
            }
            else
            {
              
                
                TimeSpan diff = finRangoTracking - iniRangoTracking;
                if (diff.TotalDays > 3)
                {
                    MessageBox.Show("The time period between the beginning and the end must be less than three days", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                InicializarBufferPuntos();          // Limpia los puntos del tracking anterior...

                btnStartTracking.Enabled = false;
                cmbDevice.Enabled = false;

                // TODO: Usar delegados aca....
                //lblLocation.Text = "Buffering....";
                //this.Refresh();
                //Application.DoEvents(); // Para actualizar el lbl

                Invoke((MethodInvoker)delegate
                {
                    lblLocation.Text = "Buffering...";
                });


                trackingBar.Maximum = (int)diff.TotalSeconds;

                trackingBar.TickFrequency = trackingBar.Maximum / 20;

                Tools.GetInstance().DoLog("Tracking desde: " + iniRangoTracking.ToString() + " hasta: " + finRangoTracking.ToString());
                actualDateTime = iniRangoTracking;
                lastDatetime = iniRangoTracking;
                abortHistTrack = false;
              
                btnStop.Enabled = true;
                btnPauseContinue.Enabled = true;
                trackingBar.Enabled = false;
                btnPauseContinue.Text = pauseText;
                indice = 0;
                indProximo = 1;
                indBuffer = -1;
                cambio = false;

                cargarProximoBuffer(indice);  // La primera vez que lo pide lanza el thread

                // TODO: Usar manualResetEvents aca...
                while ((!bufferLoaded) && (!abortHistTrack))
                {
                    Thread.Sleep(100);
                    Application.DoEvents();     // para que pueda apretar Abort...
                }
                if (!abortHistTrack)
                {
                    tmrTracking.Interval = 1000 / speedDivider;
                    tmrTracking.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No data available");
                    lblLocation.Text = "";
                }

            }

        }

        private void btnPauseContinue_Click(object sender, EventArgs e)
        {
            if (btnPauseContinue.Text == pauseText)
            {
                abortHistTrack = true;
                tmrTracking.Enabled = false;
                btnPauseContinue.Text = continueText;
                trackingBar.Enabled = true;
            }
            else
            {
                btnPauseContinue.Text = pauseText;

                actualDateTime = iniRangoTracking.AddSeconds(trackingBar.Value);
                lastDatetime = actualDateTime;
                abortHistTrack = false;
                btnStartTracking.Enabled = false;
                btnStop.Enabled = true;
                btnPauseContinue.Enabled = true;
                trackingBar.Enabled = false;
                btnPauseContinue.Text = pauseText;
                indice = 0;
                indProximo = 1;
                indBuffer = -1;
                cambio = false;

                cargarProximoBuffer(indice);

                while ((!bufferLoaded) && (!abortHistTrack))
                {
                    Thread.Sleep(100);
                    Application.DoEvents();     // para que pueda apretar Abort...
                }
                if (!abortHistTrack)
                {
                    tmrTracking.Interval = 1000 / speedDivider;
                    tmrTracking.Enabled = true;
                }

            }
            
        }

        private void cargaInicial()
        {
            try
            {
                if (!isLoaded)
                {
                    updateDevices();
                    updateMap(true);
                    isLoaded = true;

                    startDate.Value = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
                    endDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    startTime.Value = new DateTime(1900, 1, 1, 0, 0, 0);
                    endTime.Value = new DateTime(1900, 1, 1, 23, 59,59);


                }
            }
            catch (Exception) { /*MessageBox.Show("Error en cargaInicial"); */} 

        }

        // Pide el mapa y lo muestra en el WebBrowser.
        private void updateMap(bool load)
        {
            if (load)
            {
                //contenidoMapa = StaticCustomOptionsManager.cargarMapaHisttracking();
                contenidoMapa = Tools.GetInstance().construirMapa("-34.89636", "-56.1822", "16", webBrowser.Version.Major);
            }

            webBrowser.DocumentText = contenidoMapa;

        }


        private void updateDevices()
        {
            cmbDevice.Items.Clear();

            string[] listaDevTypes = listaDevices.Split(';');       // Dos grupos : HH y GPS. Los carga antes de invocar a esta ventana

            string listaHH = listaDevTypes[0];
            string listaGPS = listaDevTypes[1];

            lstHH = new List<string>(listaHH.Split(','));

            lstHH.Sort();       // Lista de nombres de devices ordenada.

            string[] arrGPS = listaGPS.Split(',');

            lstGPS = new List<string>();
            if (arrGPS.Length >= 2)
            {
                for (int i = 0; i < arrGPS.Length; i = i + 2)
                {
                    lstGPS.Add(arrGPS[i + 1] + "," + arrGPS[i]);          // En cada elemento queda: NombreGPS,idMovil. En ese orden
                }
            }
            lstGPS.Sort();      // Lista de Nombre,idMovil ordenada por nombre.


            foreach (string s in lstHH)
            {
                if (!String.IsNullOrEmpty(s))
                    cmbDevice.Items.Add(s);
            }

            foreach (string s in lstGPS)
            {
                if (!String.IsNullOrEmpty(s))
                    cmbDevice.Items.Add(s.Split(',')[0]);
                
            }

            string errDesc = "";
            int errCode = 0;
            if (!String.IsNullOrEmpty(DEVICEID))
            {
                string deviceSeleccionado = WebServiceAPI.GetInstance().ObtenerNombrePanelID(int.Parse(DEVICEID), out errDesc, out errCode);
                if (!String.IsNullOrEmpty(deviceSeleccionado))
                    cmbDevice.Text = deviceSeleccionado;
            }

        }

        private void frmHistoricalTracking_Activated(object sender, EventArgs e)
        {
            cargaInicial();
        }

        private void tmrTracking_Tick(object sender, EventArgs e)
        {
            tmrTracking.Enabled = false;            // para que no vuelva a entrar hasta terminar el proceso
            try
            {
                indBuffer++;        // Su valor inicial es -1, asi que arranca en cero.

                if (indBuffer > (int)(cantPuntos / 2) && (!cambio))
                {
                    //actualDateTime = lastDatetime;
                    if (actualDateTime > finRangoTracking)
                    {
                        tmrTracking.Enabled = false;
                        cmbDevice.Enabled = true;
                        btnStartTracking.Enabled = true;
                        btnPauseContinue.Enabled = false;
                        btnStop.Enabled = false;
                        trackingBar.Enabled = false;
                        abortHistTrack = true;
                        trackingBar.Value = 0;
                        lblLocation.Text = "End of data";
                        return;
                    }
                    else
                    {
                        //this.Text = "Pidiendo datos de : " + actualDateTime.ToString();
                        cargarProximoBuffer(indProximo);                            // Thread..
                        cambio = true;                                              // Solo pedir el nuevo buffer una vez
                    }
                }

                if (indBuffer >= cantPuntos)                // Se acabaron los puntos de este buffer...
                {
                    while ((!bufferLoaded) && (!abortHistTrack))
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }

                    indBuffer = 0;
                    indice = indProximo;
                    indProximo = (indProximo + 1) % cantBuffers;

                    //MessageBox.Show("CAMBIO!. BUFFER: " + indice + " BUFFER PROXIMO: " + indProximo);
                    cambio = false;                     // cuando se llegue a la mitad, pedir el nuevo buffer
                }
                //MessageBox.Show("Va a actualizar el punto");

                string latitud = bufferPuntos[indice].buffer[indBuffer].latitud;
                string longitud =  bufferPuntos[indice].buffer[indBuffer].longitud;
                string fechaHora = bufferPuntos[indice].buffer[indBuffer].hora;
                string velocidad = bufferPuntos[indice].buffer[indBuffer].velocidad;

                //StaticCustomOptionsManager.DoLog("latitud:" + latitud + " longitud: " + longitud + " fechahora: " + fechaHora);

                if (!String.IsNullOrEmpty(fechaHora))
                    actualDateTime = DateTime.ParseExact(fechaHora, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                else
                    actualDateTime = finRangoTracking;      // Para que finalice...

                if (actualDateTime >= finRangoTracking)
                {
                    abortHistTrack = true;
                    tmrTracking.Enabled = false;
                    btnStartTracking.Enabled = true;
                    cmbDevice.Enabled = true;
                    btnPauseContinue.Enabled = false;
                    btnStop.Enabled = false;
                    trackingBar.Enabled = false;
                    trackingBar.Value = 0;
                    return;
                }
                else
                {

                    actualizarPuntoEnMapa(latitud, longitud);

                    //MessageBox.Show("latitud: " + bufferPuntos[indice].buffer[indBuffer].latitud + " longitud: " + bufferPuntos[indice].buffer[indBuffer].longitud);

                    if ((!String.IsNullOrEmpty(latitud)) && (!String.IsNullOrEmpty(longitud)))
                    {
                        double lat = Convert.ToDouble(latitud, CultureInfo.InvariantCulture.NumberFormat);
                        string latSex = Tools.GetInstance().convertToSexagesimal(lat);
                        double longit = Convert.ToDouble(longitud, CultureInfo.InvariantCulture.NumberFormat);
                        string longSex = Tools.GetInstance().convertToSexagesimal(longit);

                        //lblLocation.Text = "Time: " + fechaHora + " - (" + latSex + ((lat > 0) ? "N" : "S") + " - " + longSex + ((longit > 0) ? "E" : "W") + ")";

                        string strVel="";
                        try
                        {
                            if (!String.IsNullOrEmpty(velocidad))
                            {
                                double p = double.Parse(velocidad, CultureInfo.InvariantCulture.NumberFormat);

                                strVel = p.ToString("0.00", CultureInfo.InvariantCulture.NumberFormat);
                            }

                        }
                        catch (Exception ex) { }


                        lblLocation.Text = "Time: " + fechaHora + " - Speed: " + strVel;
                    }
                }
                DateTime fechaHoraActual = DateTime.ParseExact(fechaHora, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                TimeSpan diff = fechaHoraActual - iniRangoTracking;

                trackingBar.Value = (int)diff.TotalSeconds;
                
                tmrTracking.Enabled = true; // Ok proximo punto
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en tmrTracking_Tick:" + ex.Message);
            }
        }

        void actualizarPuntoEnMapa(string latitud, string longitud)
        {
            string jsArray = latitud + "," + longitud;
            jsArray += "," + chkAutoCenter.Checked.ToString();                 // Serializa los datos..
            object[] args2 = { jsArray };

            webBrowser.Document.InvokeScript("setPos", args2);
        }

        private void cargarProximoBuffer(int indiceEnBuffer)
        {
            bufferLoaded = false;

            Thread t = new Thread(()=>pedirBuffer(indiceEnBuffer));     // Thread con parametro...
            t.Start();
        }


        private void pedirBuffer(int indice)
        {
            int cantRetry = 3;
            bool res = false;
            while ((!res) && cantRetry >= 0)
            {
                res = doPedirBuffer(indice);

                if (!res)
                {
                    Tools.GetInstance().DoLog("Retrying... pedirBuffer:" + cantRetry.ToString());
                }
                cantRetry--;
                Application.DoEvents();
                Thread.Sleep(100);
                Application.DoEvents();
                Thread.Sleep(100);
            }
            if (!res)
            {
                abortHistTrack = true;
                tmrTracking.Enabled = false;
                btnStartTracking.Enabled = true;
                cmbDevice.Enabled = true;
                btnPauseContinue.Enabled = false;
                btnStop.Enabled = false;
                trackingBar.Enabled = false;
                trackingBar.Value = 0;
            }

        }

        /// <summary>
        /// Se conceta al server para pedirle cantPuntos del device selecteddevice.
        /// </summary>
        /// <param name="indice"></param>
        private bool doPedirBuffer(int indice)
        {
            
            if (String.IsNullOrEmpty(selectedDeviceID))
                return false;
            try
            {

                string actualDateTimeStr = lastDatetime.Year + "-" + lastDatetime.Month.ToString("00") + "-" + lastDatetime.Day.ToString("00") + " " + lastDatetime.Hour.ToString("00") + ":" + lastDatetime.Minute.ToString("00") + ":" + lastDatetime.Second.ToString("00");

                string errDesc = "";
                int errCode = -1;
                Tools.GetInstance().DoLog("Va a pedir puntos desde " + actualDateTimeStr + " cantPuntos=" + cantPuntos.ToString());
                string datos = WebServiceAPI.GetInstance().GetPuntosGPS(Tools.GetInstance().MainOrgID.ToString(), selectedDeviceID, actualDateTimeStr, selectedDeviceType, cantPuntos.ToString(), out errDesc, out errCode);
                
                Tools.GetInstance().DoLog("Puntos Obtenidos=" + datos);

                string[] puntos = datos.Split(';');

                if (puntos.Length != cantPuntos)
                    Tools.GetInstance().DoLog("Atencion: PedirBuffer pidio: " + cantPuntos + " y recibio " + puntos.Length.ToString());

                // Estructura de datos: hora,latitud,longitud,heading,velocidad,odometro;
                int i = 0;      // Indice en bufferPuntos
                bool porlomenosUno = false;
                foreach (string s in puntos)
                {
                    if (!String.IsNullOrEmpty(s))
                    {
                        string[] infoPunto = s.Split(',');          // 1: latitud, 2: longitud
                        bufferPuntos[indice].buffer[i].hora = infoPunto[0];
                        bufferPuntos[indice].buffer[i].latitud = infoPunto[1];
                        bufferPuntos[indice].buffer[i].longitud = infoPunto[2];
                        bufferPuntos[indice].buffer[i].velocidad = infoPunto[4];

                        // Ultima hora cargada de un dato valido.
                        lastDatetime = DateTime.ParseExact(infoPunto[0], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        if (lastDatetime <= finRangoTracking)
                            porlomenosUno = true;

                        lastDatetime = lastDatetime.AddSeconds(1);  // Para no repetir el ultimo punto en el proximo pedido. 

                        i++;
                        if (i >= cantPuntos)
                            break;
                    }
                }
                Tools.GetInstance().DoLog("PorLoMenosUno es: " + porlomenosUno.ToString());

                bufferLoaded = porlomenosUno;
                return porlomenosUno;       // Se necesita por lo menos un punto para desencadenar el tracking
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("EXCEPCION en pedirBuffer(): " + ex.Message);
                return false;
            }
        }

        private void cmbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = cmbDevice.Text;
            if (!String.IsNullOrEmpty(selected))
            {
                if (lstHH.Contains(selected))
                {
                    selectedDeviceID = selected;
                    selectedDeviceType = TiposDevices.DEVICE.ToString();
                }
                else
                {
                    foreach (string s in lstGPS)
                    {
                        string GPSName = s.Split(',')[0];
                        if (GPSName == selected)
                        {
                            selectedDeviceID = s.Split(',')[1];
                            selectedDeviceType = TiposDevices.GPS.ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            abortHistTrack = true;
            btnStartTracking.Enabled = true;
            cmbDevice.Enabled = true;
            btnPauseContinue.Enabled = false;
            btnStop.Enabled = false;
            tmrTracking.Enabled = false;
            trackingBar.Value = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            abortHistTrack = true;
            tmrTracking.Enabled = false;
            this.Close();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            updateMap(true);
        }

        private void btnPor1_Click(object sender, EventArgs e)
        {
            speedDivider = 1;
            tmrTracking.Interval = 1000;
            updateSpeedButtons();
        }


        private void updateSpeedButtons()
        {
            switch (speedDivider)
            {
                case 1:
                    btnPor1.BackColor = Color.LightGreen;
                    btnPor2.BackColor = Color.FromName("Control");
                    btnPor4.BackColor = Color.FromName("Control");
                    btnPor8.BackColor = Color.FromName("Control");
                    btnPor16.BackColor = Color.FromName("Control");
                    btnPor32.BackColor = Color.FromName("Control");
                    break;
                case 2:
                    btnPor1.BackColor = Color.FromName("Control");
                    btnPor2.BackColor = Color.LightGreen;
                    btnPor4.BackColor = Color.FromName("Control");
                    btnPor8.BackColor = Color.FromName("Control");
                    btnPor16.BackColor = Color.FromName("Control");
                    btnPor32.BackColor = Color.FromName("Control");
                    break;
                case 4:
                    btnPor1.BackColor =Color.FromName("Control");
                    btnPor2.BackColor = Color.FromName("Control");
                    btnPor4.BackColor = Color.LightGreen;
                    btnPor8.BackColor =Color.FromName("Control");
                    btnPor16.BackColor = Color.FromName("Control");
                    btnPor32.BackColor = Color.FromName("Control");
                    break;
                case 8:
                    btnPor1.BackColor = Color.FromName("Control");
                    btnPor2.BackColor = Color.FromName("Control");
                    btnPor4.BackColor = Color.FromName("Control");
                    btnPor8.BackColor = Color.LightGreen;
                    btnPor16.BackColor = Color.FromName("Control");
                    btnPor32.BackColor = Color.FromName("Control");
                    break;
                case 16:
                    btnPor1.BackColor = Color.FromName("Control");
                    btnPor2.BackColor = Color.FromName("Control");
                    btnPor4.BackColor = Color.FromName("Control");
                    btnPor8.BackColor = Color.FromName("Control");
                    btnPor16.BackColor = Color.LightGreen;
                    btnPor32.BackColor = Color.FromName("Control");
                    break;
                case 32:
                    btnPor1.BackColor = Color.FromName("Control");
                    btnPor2.BackColor = Color.FromName("Control");
                    btnPor4.BackColor = Color.FromName("Control");
                    btnPor8.BackColor = Color.FromName("Control");
                    btnPor16.BackColor = Color.FromName("Control");
                    btnPor32.BackColor = Color.LightGreen;
                    break;






            }
            
        }

        private void btnPor2_Click(object sender, EventArgs e)
        {
            speedDivider = 2;
            tmrTracking.Interval = 1000/2;
            updateSpeedButtons();
        }

        private void btnPor4_Click(object sender, EventArgs e)
        {
            speedDivider = 4;
            tmrTracking.Interval = 1000/4;
            updateSpeedButtons();
        }

        private void btnPor8_Click(object sender, EventArgs e)
        {
            speedDivider = 8;
            tmrTracking.Interval = 1000/8;
            updateSpeedButtons();
        }

        private void trackingBar_Scroll(object sender, EventArgs e)
        {
            DateTime fechaTrack = iniRangoTracking.AddSeconds(trackingBar.Value);

            lblTool.Text = fechaTrack.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            var relativePoint = this.PointToClient(Cursor.Position);
            //lblTool.Left = relativePoint.X - (lblTool.Width / 2);
            lblTool.Left = relativePoint.X;
            lblTool.Top = trackingBar.Top - lblTool.Height;
            lblTool.BackColor = Color.White;
            lblTool.ForeColor = Color.Black;
            lblTool.Visible = true;
        }

        private void trackingBar_MouseUp(object sender, MouseEventArgs e)
        {
            lblTool.Visible = false;
        }

       

        private void btnPor16_Click(object sender, EventArgs e)
        {
            speedDivider = 16;
            tmrTracking.Interval = 1000 / 16;
            updateSpeedButtons();
        }

        private void btnPor32_Click(object sender, EventArgs e)
        {
            speedDivider = 32;
            tmrTracking.Interval = 1000 / 32;
            updateSpeedButtons();
        }

        private void frmHistoricalTracking_Load(object sender, EventArgs e)
        {

        }

     
    }
}
