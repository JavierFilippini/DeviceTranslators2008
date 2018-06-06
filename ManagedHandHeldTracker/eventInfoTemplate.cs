using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Timers;
using System.Threading;

namespace ManagedHandHeldTracker
{
    public partial class eventInfoTemplate : Form
    {

        public delegate void ActualizarBrowserHTMLGDelegate(string data);
        public delegate void ActualizarPanelMessage();


        private bool isActivated = false;
        public string DEVICEID;
        public string SERIALNUM;
        public NetworkStream stream;        // Stream para leer los datos
        public bool tipoMapa;               // True->con Internet->HTML->WebBrowser
                                            // False->sin Internet->JPG->PictureBox

        Regex datosAccesoAlarma = new Regex(@"ALARMA:(.*)");
        Regex Event_Data = new Regex(@"DEVICE:(.*),TIME:(.*),MAINTEXT:(.*),SUBTEXT:(.*),LATITUDE:(.*),LONGITUDE:(.*)");

        public string loadedData = "";      // Los datos del evento

        System.Timers.Timer aTimer = new System.Timers.Timer();

        public string HTMLMapa = "";

        public eventInfoTemplate()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eventInfoTemplate_Load(object sender, EventArgs e)
        {
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to 1 second.
            aTimer.Interval = 1000;
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

        private void eventInfoTemplate_Activated(object sender, EventArgs e)
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
               
                    Match matchRespuesta = Event_Data.Match(loadedData);

                    if (matchRespuesta.Success)
                    {
                        string device = Tools.GetInstance().getMatchData(matchRespuesta, 1);
                        string fechaHora = Tools.GetInstance().getMatchData(matchRespuesta, 2);
                        string mainText = Tools.GetInstance().getMatchData(matchRespuesta, 3);
                        string subText = Tools.GetInstance().getMatchData(matchRespuesta, 4);
                        string latitud = Tools.GetInstance().getMatchData(matchRespuesta, 5);
                        string longitud = Tools.GetInstance().getMatchData(matchRespuesta, 6);

                        lblTitle.Text = mainText;
                        lblDevice.Text = device;
                        if (mainText.Trim() == "MESSAGE")
                        {
                            lblInfoMessage.Text = subText;
                            Invoke ( new ActualizarPanelMessage(setPanelMessageVisible));
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(subText))
                            {
                                lblInfo.Text = subText;
                                Invoke(new ActualizarPanelMessage(setPanelInfoVisible));
                            }
                            else
                            {
                                Invoke(new ActualizarPanelMessage(setMinForm));
                            }
                        }
                       
                        lblTime.Text = fechaHora;

                        if ((!String.IsNullOrEmpty(latitud)) && (!String.IsNullOrEmpty(longitud)))
                        {
                            HTMLMapa = Tools.GetInstance().construirMapa(latitud, longitud, "16", webBrowser.Version.Major);
        
                            webBrowser.Visible = true;
                            pictureMap.Visible = false;
                            lblMask.Visible = false;
                            lblStatus.Visible = false;

                            aTimer.Enabled = true;
                            aTimer.Start();
                         }
                        else
                        {
                            lblStatus.Text = "No GPS Information";
                        }
                        stream.Close();
                    }
                    else
                    {
                        this.Text = "Invalid Data";
                    }
                
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en ActualizarVentana de eventInfoTemplate: " + ex.Message);
            }

        }

        private void setPanelMessageVisible()
        {
            panelMessage.Visible = true;
        }

        private void setPanelInfoVisible()
        {
            panelInfo.Visible = true;
            btnOK.Location = new Point (btnOK.Location.X,btnOK.Location.Y - btnOK.Height);
            this.Height = this.Height - btnOK.Height;
        }

        private void setMinForm()
        {
            this.Height = this.Height - 2*btnOK.Height;
            btnOK.Location = new Point(btnOK.Location.X, btnOK.Location.Y - 2*btnOK.Height);
        }


    }
}
