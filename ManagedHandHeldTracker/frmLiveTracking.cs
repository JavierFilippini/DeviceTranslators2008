using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Reflection;

namespace ManagedHandHeldTracker
{
    public partial class frmLiveTracking : Form
    {
        private bool salir = false;

        public string DEVICEID;
        public int ORGID;
        public string SERIALNUM;

        bool autoCenter = false;             // Indica si luego de actualizar la posicion en el mapa, éste se debe centrar en ella.

        
       // Regex Pos_Data = new Regex(@"LATITUDE:(.*),LONGITUDE:(.*),DATETIME:(.*),HHID:(.*)");
        Regex Pos_Data = new Regex(@"(.*),(.*),(.*),(.*)");
        Regex generalHeaderData = new Regex(@"LENGTH:(.*)");        // Header usado para los pedidos de datos 

        string HTMLMapa = "";
        bool isMapLoaded = false;
        public frmLiveTracking()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //pedirYCargarMapa();
            tmrUpdatePosition.Enabled = true;
        }


        /// <summary>
        /// Extrae un campo de una expresion regular reconocida 
        /// </summary>
        /// <param name="resultMatch"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string getMatchData(Match resultMatch, int index)
        {
            return resultMatch.Groups[index].Value;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            salir = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }
        // Devuelve: latitud,longitud,fechahora,HHID
        private string getLastPositionDateTime()
        {
            string res = "";

            try
            {
                string errDesc = "";
                int errCode = -1;
                string encodedGPS = WebServiceAPI.GetInstance().GetPosition(DEVICEID, ORGID.ToString(), out errDesc, out errCode);

                Match matchRespuesta = Pos_Data.Match(encodedGPS);         // Si no hay match es porque se devolvio NO_LOCATION. Seguir de largo.
                if (matchRespuesta.Success)
                {
                    string latitud = getMatchData(matchRespuesta, 1);
                    string longitud = getMatchData(matchRespuesta, 2);
                    string dateTime = getMatchData(matchRespuesta, 3);
                    string HHID = getMatchData(matchRespuesta, 4);

                    res = latitud + "," + longitud + "," + dateTime + "," + HHID;
                }
            }
            catch (Exception ex) 
            { 
                Tools.GetInstance().DoLog("Excepcion en getLastPositionDateTime: " + ex.Message); 
            }

            return res;
        }


        private void actualizeLivePositionV2(string coords, string fechaHora, string HHID)
        {

            if ((!String.IsNullOrEmpty(coords)) && (!String.IsNullOrEmpty(fechaHora)))
            {
                string latitud = coords.Split(',')[0];
                string longitud = coords.Split(',')[1];


                double lat = Convert.ToDouble(latitud, CultureInfo.InvariantCulture.NumberFormat);
                string latSex = Tools.GetInstance().convertToSexagesimal(lat);
                double longit = Convert.ToDouble(longitud, CultureInfo.InvariantCulture.NumberFormat);
                string longSex = Tools.GetInstance().convertToSexagesimal(longit);

                lblLocation.Text = latSex + ((lat > 0) ? "N" : "S") + " - " + longSex + ((longit > 0) ? "E" : "W");


                lblDateTime.Text = fechaHora;
                lblTitulo.Text = "Live Tracking: " + HHID.Trim();
                string jsArray = coords;
                jsArray += "," + autoCenter.ToString();                 // Serializa los datos..
                object[] args2 = { jsArray };

                webBrowser1.Document.InvokeScript("setPos", args2);
            }
        }

        private void actualizeLivePosition()
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            try
            {
                string errDesc = "";
                int errCode = -1;

                string encodedGPS = WebServiceAPI.GetInstance().GetPosition(DEVICEID, ORGID.ToString(), out errDesc, out errCode);


                Match matchRespuesta = Pos_Data.Match(encodedGPS);         // Si no hay match es porque se devolvio NO_LOCATION. Seguir de largo.
                if (matchRespuesta.Success)
                {
                    string latitud = getMatchData(matchRespuesta, 1);
                    string longitud = getMatchData(matchRespuesta, 2);
                    string dateTime = getMatchData(matchRespuesta, 3);
                    string HHID = getMatchData(matchRespuesta, 4);

                    if ((!String.IsNullOrEmpty(latitud)) && (!String.IsNullOrEmpty(longitud)))
                    {
                        double lat = Convert.ToDouble(latitud, CultureInfo.InvariantCulture.NumberFormat);
                        string latSex = Tools.GetInstance().convertToSexagesimal(lat);
                        double longit = Convert.ToDouble(longitud, CultureInfo.InvariantCulture.NumberFormat);
                        string longSex = Tools.GetInstance().convertToSexagesimal(longit);

                        lblLocation.Text = latSex + ((lat > 0) ? "N" : "S") + " - " + longSex + ((longit > 0) ? "E" : "W");
                    }

                    lblDateTime.Text = dateTime;
                    lblTitulo.Text = "Live Tracking: " + HHID.Trim();
                    string jsArray = latitud + "," + longitud;
                    jsArray += "," + autoCenter.ToString();                 // Serializa los datos..
                    object[] args2 = { jsArray };

                    webBrowser1.Document.InvokeScript("setPos", args2);
                    
                }
            }
            catch (Exception ex) { Tools.GetInstance().DoLog("Excepcion en actualizaLivePosition: " + ex.Message);}
            finally
            {
                tmrUpdatePosition.Enabled = true;
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            autoCenter = chkAutoCenter.Checked;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string coordArray = "-34.89542,-56.18259,-34.89596,-56.18391,#000000,-34.89596,-56.18391,-34.89719,-56.18319,#000000,-34.89719,-56.18319,-34.89623,-56.18121,#000000,-34.89623,-56.18121,-34.89542,-56.18259,#00FF00";

            coordArray = coordArray.Substring(0, coordArray.Length - 1);

            // LLama al javascript() para dibujar las zonas en el mapa.
            object[] args = { coordArray };
            string ret = (string)webBrowser1.Document.InvokeScript("verZonas", args);

        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
  
        }

        private void Form2_Click(object sender, EventArgs e)
        {
          
        }

        private void tmrUpdatePosition_Tick(object sender, EventArgs e)
        {
//            StaticCustomOptionsManager.DoLog("Entro al timer");


            if (salir)
                this.Close();       // Para salir 

            tmrUpdatePosition.Enabled = false;
            try
            {

                if (!isMapLoaded)
                {
                    string errDesc = "";
                    int errCode = -1;

                    string latlongdatetime = getLastPositionDateTime();
                    //Dictionary<int, empInfo> listaEmp = WebServiceAPI.GetInstance().getListaEmpleadosABordo(DEVICEID, ORGID.ToString(), out errDesc, out errCode);
                  
                    if (!String.IsNullOrEmpty(latlongdatetime))
                    {
                        string lati = latlongdatetime.Split(',')[0];
                        string longi = latlongdatetime.Split(',')[1];
                        string fechahora = latlongdatetime.Split(',')[2];
                        string HHID = latlongdatetime.Split(',')[3];


                        //string infoBubble = Helpers.GetInstance().construirHTMLInfoBubble(listaEmp,HHID);

                        //Helpers.GetInstance().DoLog("infoBubble=" + infoBubble);
                    


                        HTMLMapa = Tools.GetInstance().construirMapa(lati, longi, "16", webBrowser1.Version.Major);
                        isMapLoaded = true;

                        //HTMLMapa = HTMLMapa.Replace("[LAT]", lati);
                        //HTMLMapa = HTMLMapa.Replace("[LONG]", longi);

                        webBrowser1.DocumentText = HTMLMapa;
                        waitToRefresh();

                        actualizeLivePositionV2(lati + "," + longi, fechahora, HHID);
                    }
                    else
                    {
                        //string infoBubble = Helpers.GetInstance().construirHTMLInfoBubble(listaEmp, "");
                        //HTMLMapa = Helpers.GetInstance().construirMapa("", "", "16", infoBubble, webBrowser1.Version.Major);
                        HTMLMapa = Tools.GetInstance().construirMapa("", "", "16",  webBrowser1.Version.Major);
                      
                        lblNodata.Visible = true;

                    }
                    isMapLoaded = true;
                }
                else
                {
                      string latlongdatetime = getLastPositionDateTime();

                      if (!String.IsNullOrEmpty(latlongdatetime))
                      {
                          string lati = latlongdatetime.Split(',')[0];
                          string longi = latlongdatetime.Split(',')[1];
                          string fechahora = latlongdatetime.Split(',')[2];
                          string HHID = latlongdatetime.Split(',')[3];
                          HTMLMapa = HTMLMapa.Replace("[LAT]", lati);
                          HTMLMapa = HTMLMapa.Replace("[LONG]", longi);
                          if (lblNodata.Visible)
                          {
                              lblNodata.Visible = false;    // Si estaba el mensaje de NoDataAvailable y llegaron datos GPS
                              isMapLoaded = false;          // Para forzar la recarga el mapa
                          }
                          else
                            actualizeLivePositionV2(lati + "," + longi, fechahora, HHID);
                      }
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en timerUpdatePosition: " +ex.Message);
            }
            finally
            {
                tmrUpdatePosition.Enabled = true;
            }
        }

        /// <summary>
        /// Espera a que el webBrowser cargue el HTML y lo refresque
        /// </summary>
        private void waitToRefresh()
        {
            webBrowser1.Refresh();
            int contToExit = 60;   // 6 segundos maxima espera.
            while ((contToExit > 0) && (webBrowser1.ReadyState != WebBrowserReadyState.Complete))
            {
                Application.DoEvents();
                Thread.Sleep(100);
                contToExit--;

            }
        }

        //private void btnrefresh_Click(object sender, EventArgs e)
        //{
        //    isMapLoaded = false;
        //    tmrUpdatePosition.Enabled = true;
        //    webBrowser1.DocumentText = "";

        //}
       
        private void btnRefresh_Click(object sender, EventArgs e)
        {
              string latlongdatetime = getLastPositionDateTime();
              string errDesc = "";
              int errCode = -1;
              //Dictionary<int, empInfo> listaEmp = WebServiceAPI.GetInstance().getListaEmpleadosABordo(DEVICEID, ORGID.ToString(), out errDesc, out errCode);


              if (!String.IsNullOrEmpty(latlongdatetime))
              {
                  string lati = latlongdatetime.Split(',')[0];
                  string longi = latlongdatetime.Split(',')[1];
                  string fechahora = latlongdatetime.Split(',')[2];
                  string HHID = latlongdatetime.Split(',')[3];
                  //string infoBubble = Helpers.GetInstance().construirHTMLInfoBubble(listaEmp, HHID);

                  //HTMLMapa = Helpers.GetInstance().construirMapa(lati, longi, "16", infoBubble, webBrowser1.Version.Major);
                  HTMLMapa = Tools.GetInstance().construirMapa(lati, longi, "16",  webBrowser1.Version.Major);
              }
              else
              {
                  //string infoBubble = Helpers.GetInstance().construirHTMLInfoBubble(listaEmp, "");
                  //HTMLMapa = Helpers.GetInstance().construirMapa("", "", "16", infoBubble, webBrowser1.Version.Major);
                  HTMLMapa = Tools.GetInstance().construirMapa("", "", "16",  webBrowser1.Version.Major);
              }

            webBrowser1.DocumentText = HTMLMapa;
        }
    }
}
