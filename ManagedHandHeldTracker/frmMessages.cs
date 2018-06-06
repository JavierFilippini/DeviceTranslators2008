using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;

namespace ManagedHandHeldTracker
{
    public partial class frmMessages : Form
    {
        public delegate void agregaraTexto(string texto, string fechahora, string posicion, bool alignDer);
        public delegate void updateBanner(string texto);

        public int DEVICEID;                            // Es el PanelID del LENEL
        public string deviceName;
     
        public int ORGANIZATIONID;
        private int MAX_MSG_LENGTH = 255;               // LENGTH:xxxxxxxxxxxxx: Espacio para 1000000000
        //private int GENERAL_HEADER_LENGTH = 20;       // LENGTH:xxxxxxxxxxxxx: Espacio para 1000000000
        private int CANT_MSG = 5;
        private Thread thread_actualizar = null;
        private Thread thread_init = null;
        bool salir = false;

        // Rango de fechas de mensajes mostrados.
        private DateTime g_lastReceived = new DateTime(1900, 1, 1);
        private DateTime firstReceived = new DateTime(2100, 1, 1);

        Regex msgReg = new Regex(@"TEXT:(.*),DATETIME:(.*),SOURCE:(.*),DEST:(.*)");

        Regex generalHeaderData = new Regex(@"LENGTH:(.*)");        // Header usado para los pedidos de datos que pueden crecer indefinidamente en funcion de la cantidad de devices/GPSs

        public frmMessages()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string textToSend = txtMens.Text;

            addTextoToView(textToSend, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), "FIN",true);

            enviarMensaje(textToSend);

            txtMens.Text = "";
           
        }

        public void actualizeBanner(string v_text)
        {

            if (lblBanner.InvokeRequired)
            {
                Invoke(new updateBanner(actTexto), v_text);
            }

        }
        private void actTexto(string v_t)
        {
            lblBanner.Text = v_t;
        }



        /// <summary>
        /// posicion = "FIN": agrega al final. INI, al principio
        /// 
        /// </summary>
        /// <param name="textToAdd"></param>
        /// <param name="fechaHora"></param>
        /// <param name="posicion"></param>
        public void addTextoToView(string textToAdd, string fechaHora, string posicion, bool alignDer)
        {
            if (txtMessages.InvokeRequired)
            {
                Invoke(new agregaraTexto(updateTXTMsg), textToAdd, fechaHora, posicion, alignDer);
            }
            else
            {
                updateTXTMsg(textToAdd, fechaHora, posicion,alignDer);
            }

        }
        private void updateTXTMsg(string v_txt, string fechaHora, string posicion, bool alignDer)
        {
            string totalText = "";

            String textoAlign, fechaHoraAlign;

            if (alignDer)
            {
                textoAlign = String.Format("{0,0} {1,43}", "", v_txt);
                fechaHoraAlign = String.Format("{0,0} {1,43}", "", fechaHora);
            }
            else
            {
                textoAlign = v_txt;
                fechaHoraAlign = fechaHora;
            }

            if (posicion == "FIN")
            {
                totalText = txtMessages.Text + Environment.NewLine + Environment.NewLine + fechaHoraAlign + Environment.NewLine + textoAlign;
                txtMessages.Text = totalText;

                //Scroll al final para ver siempre el ultimo mensaje.
                txtMessages.SelectionStart = txtMessages.TextLength;
                txtMessages.ScrollToCaret();
            }
            else
            {


                totalText = fechaHoraAlign + Environment.NewLine + textoAlign + Environment.NewLine + Environment.NewLine + txtMessages.Text;
                txtMessages.Text = totalText;
                txtMessages.SelectionStart = 1;
                txtMessages.ScrollToCaret();
            }
        }

        private void enviarMensaje(string v_texto)
        {
            if (!String.IsNullOrEmpty(v_texto.Trim()))
            {
               
                try
                {
                    int errCode = -1;
                    string errDesc = "";
                    WebServiceAPI.GetInstance().SendMSG(DEVICEID.ToString(), ORGANIZATIONID.ToString(), v_texto.Trim(), out errDesc, out errCode);

                }
                catch (Exception ex)
                {
                    Tools.GetInstance().DoLog("Excepcion en enviarMensaje: " + ex.Message);
                }
            }
        }

        private void frmMessages_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread_actualizar!=null)
            {
                salir = true;
                int i = 10;
                while ((thread_actualizar.IsAlive) && (i > 0))
                {
                    Thread.Sleep(200);
                    i--;
                }
                if (i == 0)
                {
                    try
                    {
                        thread_actualizar.Abort();
                    }
                    catch (Exception) { }
                }
            }
        }

        private void frmMessages_Load(object sender, EventArgs e)
        {
          
            thread_init = new Thread(inicializar);
            thread_init.Start();
        }

        private void inicializar()
        {
            try
            {
                actualizeBanner("Actualizing...");
                string mensajes = cargarMensajesPrevios(CANT_MSG);
                actualizarViewMensajesPrevios(mensajes);
                thread_actualizar = new Thread(startActualizar);
                thread_actualizar.Start();
                actualizeBanner(deviceName);

            }
            catch (Exception) { }
        }

        private void actualizarViewMensajesPrevios(string v_mensajes)
        {
            string[] textos = v_mensajes.Split('|');

            foreach (string msg in textos)
            {
                
                Match textR =   msgReg.Match(msg);
                if (textR.Success)
                {
                    string texto = getMatchData(textR, 1);
                    string fecha = getMatchData(textR, 2);
                    string source = getMatchData(textR, 3);
                    string dest = getMatchData(textR, 4);

                    addTextoToView(texto, fecha, "INI",(source == "SERVER"));

                    DateTime fechaDT = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss",null);
                    if (fechaDT < firstReceived)
                        firstReceived = fechaDT;  // Actualiza el puntero de la fecha del primer mensaje

                    if (fechaDT > g_lastReceived)
                        g_lastReceived = fechaDT;   // Actualiza el puntero de la fecha del ultimo mensaje
                }
            }


        }


        private void startActualizar()
        {
           
            try
            {
                while (!salir)
                {
                    string nuevoMensaje = pollForNew(DEVICEID, ORGANIZATIONID, g_lastReceived);
                    if (!String.IsNullOrEmpty(nuevoMensaje))
                        addTextoToView(nuevoMensaje,g_lastReceived.ToString("yyyy-MM-dd HH:mm:ss"),"FIN",false);
                     if(!salir)
                        Thread.Sleep(1000);
                }
            }
            catch (Exception)
            {
            }

        }

        // Manda un pedido al server de los ulnimos n mensajes previos a firstReceived 
        private string cargarMensajesPrevios(int v_cantMsg)
        {
            string res ="";
            try
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();


                string errDesc = "";
                int errCode = -1;
                res = WebServiceAPI.GetInstance().GetPrevMessages(DEVICEID.ToString(), ORGANIZATIONID.ToString(), firstReceived.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), v_cantMsg.ToString(), out errDesc, out errCode);

            }
            catch (Exception ex ) 
            {
                Tools.GetInstance().DoLog("Excepcion en cargarMensajesPrevios: " + ex.Message);
            }
            

            return res;
        }


        private string pollForNew(int v_deviceID, int v_orgID, DateTime v_lastReceived)
        {
            string res = "";
            try
            {
                string errDesc = "";
                int errCode = -1;
                res = WebServiceAPI.GetInstance().GetNewMessages(v_deviceID.ToString(), ORGANIZATIONID.ToString(), v_lastReceived.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), out errDesc, out errCode);

                Match headerResp = msgReg.Match(res);

                if (headerResp.Success)
                {
                    res = getMatchData(headerResp, 1);
                    string fecha = getMatchData(headerResp, 2);
                    g_lastReceived = Convert.ToDateTime(fecha);       // Actualiza la variable global con la fecha del mensaje recibido
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en pollForNew: " + ex.Message);
            }

            return res;
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

        private void btnMore_Click(object sender, EventArgs e)
        {
            string mensajes = cargarMensajesPrevios(CANT_MSG);
            actualizarViewMensajesPrevios(mensajes);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMens_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
