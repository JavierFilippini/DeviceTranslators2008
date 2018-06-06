using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Timers;

namespace ManagedHandHeldTracker
{
    public partial class frmEventinfoValidAccess : Form
    {

        public delegate void actualizarEtiquetaDelegate(string msg);
        public delegate void ActualizarBrowserHTMLGDelegate(string data);

        private bool isActivated = false;

        public string DEVICEID;
        public string SERIALNUM;
        //public string RegexToLoad;  // Expresion regular con los datos para cargar imagenes, mapa, etc.

        public byte[] fotoEmp = null;
        public byte[] fotoEv = null;
        public string datosEvento = "";

        public System.Net.Sockets.NetworkStream stream;             // Stream para cargar los datos
        public bool tipoMapa;       // True->con Internet->HTML->WebBrowser
                                    // False->sin Internet->JPG->PictureBox

        Regex datosAccesoValido = new Regex(@"VALIDO:(.*),(.*),(.*)");
        Regex Event_Data = new Regex(@"BADGE:(.*),NAME:(.*),SURNAME:(.*),SSNO:(.*),COMPANY:(.*),HHID:(.*),ACCESSTYPE:(.*),DATETIME:(.*),LATITUDE:(.*),LONGITUDE:(.*),IDIMAGENEMPLEADO:(.*),IDIMAGENACCESO:(.*),READERNAME:(.*),TEXTOVENTANA:(.*)");
        Regex generalHeaderData = new Regex(@"LENGTH:(.*)");        // Header usado para los pedidos de datos 

        public static string HTMLMapa = "";

        System.Timers.Timer aTimer = new System.Timers.Timer();


        public frmEventinfoValidAccess()
        {
            InitializeComponent();
            lblInfo.BackColor = Color.Transparent;
        }


        private void frmEventinfoValidAccess_Activated(object sender, EventArgs e)
        {
            if (!isActivated)
            {
                isActivated = true;
                Thread t = new Thread(actualizarVentana);
                //t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
        }

        private void actualizarVentana()
        {
            try
            {
                string respuesta = datosEvento;

                    if (fotoEmp != null)
                    {
                        picEmp.Image = ByteToImage(fotoEmp, fotoEmp.Length);
                    }

                    if (fotoEv != null)
                    {
                        picEvent.Image = ByteToImage(fotoEv, fotoEv.Length);
                    }

                    Match matchRespuesta = Event_Data.Match(respuesta);

                    if (matchRespuesta.Success)
                    {

                        string badge = getMatchData(matchRespuesta, 1);
                        string name = getMatchData(matchRespuesta, 2);
                        string surname = getMatchData(matchRespuesta, 3);
                        string SSNO = getMatchData(matchRespuesta, 4);
                        string empresa = getMatchData(matchRespuesta, 5);
                        string HHID = getMatchData(matchRespuesta, 6);
                        string accessType = getMatchData(matchRespuesta, 7);
                        string fechaHora = getMatchData(matchRespuesta, 8);
                        string latitude = getMatchData(matchRespuesta, 9);
                        string longitude = getMatchData(matchRespuesta, 10);
                        string readerName = getMatchData(matchRespuesta, 13);
                        string textoVentana = getMatchData(matchRespuesta, 14);

                        if (!String.IsNullOrEmpty(textoVentana))
                            lblTitulo.Text = textoVentana;

                        lblNombre.Text = name;
                        lblApellido.Text = surname;

                        actualizarBadge(badge);

                        lblDocumento.Text = SSNO;
                        lblEmpresa.Text = empresa;
                        lblHHID.Text = HHID;
                        lblAccesstype.Text = Tools.GetInstance().translateToEnglish(accessType);
                        if ((!String.IsNullOrEmpty(latitude)) && (!String.IsNullOrEmpty(longitude)))
                        {
                            double lat = Convert.ToDouble(latitude, CultureInfo.InvariantCulture.NumberFormat);
                            string latSex = Tools.GetInstance().convertToSexagesimal(lat);
                            double longit = Convert.ToDouble(longitude, CultureInfo.InvariantCulture.NumberFormat);
                            string longSex = Tools.GetInstance().convertToSexagesimal(longit);

                            lblLocation.Text = latSex + ((lat > 0) ? "N" : "S") + " - " + longSex + ((longit > 0) ? "E" : "W");
                        }

                        lblReader.Text = readerName;
                        lblDateTime.Text = fechaHora;

                        
                        if ((!String.IsNullOrEmpty(latitude)) && (!String.IsNullOrEmpty(longitude)))
                        {
                            //HTMLMapa = StaticCustomOptionsManager.cargarMapaDesdeResources(webBrowser.Version.Major);
                            HTMLMapa = Tools.GetInstance().construirMapa(latitude, longitude, "10", webBrowser.Version.Major);
                            
                            //HTMLMapa = HTMLMapa.Replace("[LAT]", latitude);
                            //HTMLMapa = HTMLMapa.Replace("[LONG]", longitude);

                            webBrowser.Visible = true;

                            lblMask.Visible = false;
                            lblInfo.Visible = false;
                                
                            aTimer.Enabled = true;
                            aTimer.Start();
                        }
                        else
                        {
                            lblInfo.Text = "No GPS Information";
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data available", "Information");
                        lblInfo.Visible = false;
                    }
                
            }
            catch (Exception ex) 
            {
                Tools.GetInstance().DoLog("Excepcion en actualizarVentana de EventInfoValidAccess: " + ex.Message);
            }
            
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            aTimer.Stop();
            ActualizarBrowser(HTMLMapa);

        }

        private void ActualizarBrowser(string dato)
        {

            if (webBrowser.InvokeRequired)
            {
                webBrowser.Invoke(new ActualizarBrowserHTMLGDelegate(updateBrowser), dato);
            }
            else
                webBrowser.DocumentText = dato;

        }

        private void updateBrowser(string v_dato)
        {
            webBrowser.DocumentText = v_dato;
        }

        //public void cargarMapaDesdeResources(int v_browserVersion)
        //{
        //    try
        //    {
        //        string mapstractionCode = "";
        //        string encodedIcon = "";

        //        var assembly = Assembly.GetExecutingAssembly();

        //        var resourceName = "";
        //        resourceName = "ManagedHandHeldTracker.MapLENELV3.html";

        //        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            HTMLMapa = reader.ReadToEnd();
        //        }

        //        resourceName = "ManagedHandHeldTracker.mapstractionCode.js";
        //        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            mapstractionCode = reader.ReadToEnd();

        //        }

        //        resourceName = "ManagedHandHeldTracker.encodedIcon.txt";
        //        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            encodedIcon = reader.ReadToEnd();
        //        }

        //        HTMLMapa = HTMLMapa.Replace("[MAPSTRACTIONCODE]", mapstractionCode);

        //        if (v_browserVersion < 9)
        //            HTMLMapa = HTMLMapa.Replace("[ENCODEDICON]", @"https://dl.dropboxusercontent.com/u/79785316/iconoMapStraction.png");
        //        else
        //            HTMLMapa = HTMLMapa.Replace("[ENCODEDICON]", encodedIcon);

        //    }
        //    catch (Exception ex)
        //    {
        //        StaticCustomOptionsManager.DoLog("Excepcion en cargarMapaDesdeRecursos: " + ex.Message);
        //    }

        //}


        public void actualizarBadge(string texto)
        {
            if (lblbadge.InvokeRequired)
            {
                lblbadge.Invoke(new actualizarEtiquetaDelegate(updateBadge), texto);
            }
            else
            {
                lblbadge.Text = texto;
            }

        }

        private void updateBadge(string texto)
        {
            lblbadge.Text = texto;

        }
      

        private Image ByteToImage(byte[] imageBytes, int cantbytes)
        {
            // Convert byte[] to Image
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes, 0, cantbytes);
            ms.Write(imageBytes, 0, cantbytes);
            Image image = new Bitmap(ms);
            return image;
        }

        public string getMatchData(Match resultMatch, int index)
        {
            return resultMatch.Groups[index].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void frmEventinfoValidAccess_Load(object sender, EventArgs e)
        {
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to 1 second.
            aTimer.Interval = 1000;
        }

      


        private void picEvent_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void grupoMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
          
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            webBrowser.DocumentText = HTMLMapa;
        }

        //private void tmrOneUpdate_Tick(object sender, EventArgs e)
        //{
        //    //tmrOneUpdate.Enabled = false;
        //    //try
        //    //{
        //    //    if (webBrowser.ReadyState != WebBrowserReadyState.Complete)
        //    //        webBrowser.DocumentText = HTMLMapa;
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    MessageBox.Show("Error en tmrOneUpdate: " + ex.Message);
        //    //}

        //}

    }
}
