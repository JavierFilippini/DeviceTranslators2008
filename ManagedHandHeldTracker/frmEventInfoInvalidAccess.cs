using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Timers;

namespace ManagedHandHeldTracker
{
    public partial class frmEventInfoInvalidAccess : Form
    {
        public delegate void ActualizarBrowserHTMLGDelegate(string data);

        private bool isActivated = false;
        public string DEVICEID;
        public string SERIALNUM;
        //public string RegexToLoad;          // Expresion regular con los datos para cargar imagenes, mapa, etc.

        public string datosEvento = "";

        public bool tipoMapa;               // True->con Internet->HTML->WebBrowser
                                            // False->sin Internet->JPG->PictureBox

        Regex datosAccesoInvalido = new Regex(@"INVALIDO:(.*)");
        Regex Event_Data = new Regex(@"BADGE:(.*),NAME:(.*),SURNAME:(.*),SSNO:(.*),COMPANY:(.*),HHID:(.*),ACCESSTYPE:(.*),DATETIME:(.*),LATITUDE:(.*),LONGITUDE:(.*),IDIMAGENEMPLEADO:(.*),IDIMAGENACCESO:(.*),READERNAME:(.*),TEXTOVENTANA:(.*)");
        Regex generalHeaderData = new Regex(@"LENGTH:(.*)");        // Header usado para los pedidos de datos 

        public static string HTMLMapa = "";


        System.Timers.Timer aTimer = new System.Timers.Timer();


        public frmEventInfoInvalidAccess()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEventInfoInvalidAccess_Load(object sender, EventArgs e)
        {
          
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to 1 second.
            aTimer.Interval = 1000;

        }

        private void frmEventInfoInvalidAccess_Activated(object sender, EventArgs e)
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
                Match matchRespuesta = Event_Data.Match(datosEvento);

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

                    lblBadge2.Text = badge;
                    lblHHID2.Text = HHID;
                    lbldateTime2.Text = fechaHora;
                    

                    if ((!String.IsNullOrEmpty(latitude)) && (!String.IsNullOrEmpty(longitude)))
                    {
                        latitude = latitude.Replace(',', '.');
                        longitude = longitude.Replace(',', '.');

                        double lat = Convert.ToDouble(latitude, CultureInfo.InvariantCulture.NumberFormat);
                        string latSex =Tools.GetInstance().convertToSexagesimal(lat);
                        double longit = Convert.ToDouble(longitude, CultureInfo.InvariantCulture.NumberFormat);
                        string longSex = Tools.GetInstance().convertToSexagesimal(longit);

                        lblLocation.Text = latSex + ((lat > 0) ? "N" : "S") + " - " + longSex + ((longit > 0) ? "E" : "W");
                    }

                    lblReader2.Text = readerName;
                       
                    if ((!String.IsNullOrEmpty(latitude)) && (!String.IsNullOrEmpty(longitude)))
                    {

                        HTMLMapa = Tools.GetInstance().construirMapa(latitude, longitude, "10", webBrowser2.Version.Major);
                       // StaticCustomOptionsManager.cargarMapaDesdeResources(webBrowser2.Version.Major);

                       // frmEventInfoInvalidAccess.HTMLMapa = frmEventInfoInvalidAccess.HTMLMapa.Replace("[LAT]", latitude);
                       // frmEventInfoInvalidAccess.HTMLMapa = frmEventInfoInvalidAccess.HTMLMapa.Replace("[LONG]", longitude);

                        webBrowser2.Visible = true;

                        pictureMap.Visible = false;
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
                    this.Text = "Invalid Data";
                }
            }
            catch (Exception ex) 
            {
                Tools.GetInstance().DoLog("Excepcion en ActualizarVentana de EventInfoInvalidAccess: " + ex.Message);
            }

        }


        private void OnTimedEvent(object sender, EventArgs e)
        {

            aTimer.Stop();
            ActualizarBrowser(frmEventInfoInvalidAccess.HTMLMapa);
        }

        private void ActualizarBrowser(string dato)
        {

            if (webBrowser2.InvokeRequired)
            {
                webBrowser2.Invoke(new ActualizarBrowserHTMLGDelegate(updateBrowser), dato);
            }
            else
                webBrowser2.DocumentText = dato;

        }

        private void updateBrowser(string v_dato)
        {
            webBrowser2.DocumentText = v_dato;

        }


        //public void cargarMapaDesdeResources(int v_browserVersion)
        //{

        //    string mapstractionCode = "";
        //    string encodedIcon = "";

        //    var assembly = Assembly.GetExecutingAssembly();

        //    var resourceName = "";
          
        //    resourceName = "ManagedHandHeldTracker.MapLENELV3.html";

        //    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        frmEventInfoInvalidAccess.HTMLMapa = reader.ReadToEnd();
        //    }

        //    resourceName = "ManagedHandHeldTracker.mapstractionCode.js";
        //    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        mapstractionCode = reader.ReadToEnd();
        //    }

        //    resourceName = "ManagedHandHeldTracker.encodedIcon.txt";
        //    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        encodedIcon = reader.ReadToEnd();

        //    }

        //    frmEventInfoInvalidAccess.HTMLMapa = HTMLMapa.Replace("[MAPSTRACTIONCODE]", mapstractionCode);

        //    if (v_browserVersion < 9)
        //        frmEventInfoInvalidAccess.HTMLMapa = HTMLMapa.Replace("[ENCODEDICON]", @"https://dl.dropboxusercontent.com/u/79785316/iconoMapStraction.png");
        //    else
        //        frmEventInfoInvalidAccess.HTMLMapa = HTMLMapa.Replace("[ENCODEDICON]", encodedIcon);

        //}

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

      

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            webBrowser2.DocumentText = frmEventInfoInvalidAccess.HTMLMapa;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void tmrOneUpdate_Tick(object sender, EventArgs e)
        {

        }

    }
}
