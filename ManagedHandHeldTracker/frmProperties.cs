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
using System.Threading;
using System.IO;
using Microsoft.Win32;
//using System.Threading.Tasks;

namespace ManagedHandHeldTracker
{
    public partial class frmProperties : Form
    {
        private ManualResetEvent FinalizarActualizar = new ManualResetEvent(false);
        public enum TiposReportes
        {
            REPORTE_ACCESOS,
            REPORTE_VISITAS,
            REPORTE_ZONAS,
        }

        public enum ModosAcceso
        {
            Normal = 0,
            Diferido = 1,
            Bloqueo = 2,
            TarjetasMaster = 3,
            Mobile = 4
        }

        public delegate void agregaraTexto();
        public delegate void updateLabel(string texto);
        public int DEVICEID;             // Es el PanelID del LENEL
        //public string IMEI;
        public string GPSUpdateTime;
        public string SpeedLimit;
        public bool stopLoading = false;
        public int ORGANIZATIONID;
        private int CANT_MSG = 5;

        public string deviceName = "";
        public string deviceType = "";
        //public string loggedUser = "";        // No se usa mas.-
        public int HHMode = 0;
        //public string lastGPSDate = "";       // No se usa mas.-
        //public string listaDevices = "";      // No se usa mas.-

        public KeyValuePair<Employee, Tarjeta> datosEmpleado = new KeyValuePair<Employee,Tarjeta>(null,null);

        //private Thread thread_actualizar = null;
        //private Thread thread_init = null;
//        private Thread threadLoadPDF = null;


        private bool isUpdated = false;

        private  int linkedPersonID = -1;                         // Es el PersonID de LENEL
        private string linkedBadge = "";                          // Numero de tarjeta asociada al device.
        private Employee searchedEmployee = null;
        private Tarjeta searchedBadge = null;

        // Rango de fechas de mensajes mostrados.
        private DateTime g_lastReceived = new DateTime(1900, 1, 1);
        private DateTime firstReceived = new DateTime(2100, 1, 1);

        Regex msgReg = new Regex(@"TEXT:(.*),DATETIME:(.*),SOURCE:(.*),DEST:(.*)");

        Regex generalHeaderData = new Regex(@"LENGTH:(.*)");        // Header usado para los pedidos de datos que pueden crecer indefinidamente en funcion de la cantidad de devices/GPSs

        List<Mensaje> mensajesEnVentana = new List<Mensaje>();

        public frmProperties()
        {
            InitializeComponent();
        }

        private void frmProperties_Load(object sender, EventArgs e)
        {
            // thread_init = new Thread(inicializar);

            Thread t = new Thread(inicializar);
            t.Start();
            //Task.Factory.StartNew(() => inicializar());
            
            // thread_init.Start(); 
            updatePropertiesData();
            updateLinkedUserView();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                HHMode = (chkTriggerEvents.Checked ? (int)ModosAcceso.Diferido : (int)ModosAcceso.Normal) + (chkBlockingMode.Checked ? (int)ModosAcceso.Diferido : (int)ModosAcceso.Normal); // OJO aca: enum ModosAcceso del server
                if (chkMasterCards.Checked) HHMode = (int)ModosAcceso.TarjetasMaster;

                if (chkMobileController.Checked) HHMode = (int)ModosAcceso.Mobile;

                string errDesc = "";
                int errCode = -1;
                WebServiceAPI.GetInstance().SetMode(DEVICEID.ToString(), HHMode.ToString(), ORGANIZATIONID.ToString(), out errDesc, out errCode);

                //if ((IMEI.Trim() != txtIMEI.Text.Trim()) && (!String.IsNullOrEmpty(txtIMEI.Text.Trim())))
                //    WebServiceAPI.GetInstance().UpdateIMEI(DEVICEID.ToString(),ORGANIZATIONID.ToString(), txtIMEI.Text.Trim(), out errDesc, out errCode);

                // Da de alta el empleado en AlutelMobility antes de hacer el updateLinkedEmployee
                if ((datosEmpleado.Key != null) && (datosEmpleado.Value != null))
                    WebServiceAPI.GetInstance().AddEmployee(datosEmpleado.Key, datosEmpleado.Value, DEVICEID, out errDesc, out errCode);
                else
                    Tools.GetInstance().DoLog("No se envia AddEmployee al pulsar OK porque el empleado o la tarjeta son NULL");

                //if (chkMobileController.Checked)
                    WebServiceAPI.GetInstance().UpdateLinkedEmployee(DEVICEID.ToString(), ORGANIZATIONID.ToString(), linkedBadge, out errDesc, out errCode);


                string HHData = deviceName+"," + txtSpeedLimit.Text + "," + txtGPSUpdateTime.Text;

                WebServiceAPI.GetInstance().UpdateDeviceConfig(HHData, ORGANIZATIONID.ToString(), out errDesc, out errCode);

            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en OK de ventana de Properties: " + ex.Message);
            }

            this.Close();
        }

        private void frmProperties_Activated(object sender, EventArgs e)
        {
            updatePropertiesData();
        }

        private void updateLinkedUserView()
        {
            string errDesc = "";
            int errCode = (int)StatusCode.OK;

            datosEmpleado = WebServiceAPI.GetInstance().GetLinkedEmployee(DEVICEID, ORGANIZATIONID, out errDesc, out errCode);
            if ((datosEmpleado.Key  == null) || (datosEmpleado.Value == null))
                Tools.GetInstance().DoLog("GetLinkedEmployee devolvio null. errDesc=" + errDesc + " errCode=" + errCode);
            else
            {
                lblFirstName.Text = datosEmpleado.Key.Nombre;
                lblLastName.Text = datosEmpleado.Key.Apellido;
                lblSSNO.Text = datosEmpleado.Key.NumeroDocumento;
                lblBadge.Text = datosEmpleado.Value.tarjeta;

                // Le carga los accessLevels de lenel ya que el addEmployee final, que lo va a mandar a AlutelMobility espera accesslevels de Lenel
                datosEmpleado.Value.accessLevels = WebServiceAPI.GetInstance().GetAccessLevelsLenel(datosEmpleado.Value.tarjeta, ORGANIZATIONID.ToString(), out errDesc, out errCode);
                
                linkedBadge = datosEmpleado.Value.tarjeta;

                Tools.GetInstance().DoLog("En updateLinkedUser(). Name=" + datosEmpleado.Key.Nombre + " Apellido=" + datosEmpleado.Key.Apellido + " tarjeta=" + datosEmpleado.Value.tarjeta + " AccessLevels=" + datosEmpleado.Value.accessLevels);

                if (datosEmpleado.Key.imageDataBytes != null)
                {
                    picLinkedEmp.Image = Tools.GetInstance().ByteToImage(datosEmpleado.Key.imageDataBytes, datosEmpleado.Key.imageDataBytes.Length);
                }
            }
        }


        private void updatePropertiesData()
        {
            if (!isUpdated)
            {
                lblName.Text = deviceName;
                //lblType.Text = deviceType;
                //lblLogged.Text = loggedUser;
                //lblLastGPS.Text = lastGPSDate;
                //txtIMEI.Text = IMEI;

                txtSpeedLimit.Text = SpeedLimit;
                txtGPSUpdateTime.Text = GPSUpdateTime;

                chkTriggerEvents.Checked = false;
                chkBlockingMode.Checked = false;
                chkMasterCards.Checked = false;

                chkAccessController.Checked = false;
                chkBlockingMode.Checked = false;
                if (HHMode < 4) chkAccessController.Checked = true;

                if (HHMode == 1) chkTriggerEvents.Checked = true;
                if (HHMode == 2)
                {
                    chkTriggerEvents.Checked = true;
                    chkBlockingMode.Checked = true;
                }

                if (HHMode == 3)
                {
                    chkTriggerEvents.Checked = true;
                    chkMasterCards.Checked = true;
                }

                if (HHMode == 4)
                    chkMobileController.Checked = true;

            }

            isUpdated = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPowerOFF_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("This will power off the device: " + deviceName + ". Are you sure?", "Warning", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

            if (res == DialogResult.Yes)
            {
               // TODO...
            }
        }

        private void btnSend_Click_1(object sender, EventArgs e)
        {
            string textToSend = txtToSend.Text.Replace("\r\n","").Replace("\n","");
            if (!String.IsNullOrEmpty(textToSend.Trim()))
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

                try
                {
                
                //    string comando = "TYPE:LNL_MSG,DEVICEID:" + DEVICEID + ",ORGANIZATION:" + ORGANIZATIONID + ",MSG:" + textToSend.Trim();

                    string errDesc = "";
                    int errCode = -1;
                    WebServiceAPI.GetInstance().SendMSG(DEVICEID.ToString(), ORGANIZATIONID.ToString(), textToSend.Trim(), out errDesc, out errCode);

                    addTextoToView(textToSend, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), true);
                }
                catch (Exception ex)
                {
                    Tools.GetInstance().DoLog("Excepcion en boton Send: " + ex.Message);
                }
            }
            txtToSend.Text = "";
        }

        private void txtToSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtToSend_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (!String.IsNullOrEmpty(txtToSend.Text)))
            {
                btnSend_Click_1(sender, e);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            string mensajes = cargarMensajesPrevios(CANT_MSG);
            actualizarViewMensajesPrevios(mensajes);
        }

        /// <summary>
        /// posicion = "FIN": agrega al final. INI, al principio
        public void addTextoToView(string textToAdd, string fechaHora, bool alignDer)
        {

            textToAdd = textToAdd.Replace("\r", "").Replace("\n", "");
            mensajesEnVentana.Add(new Mensaje(textToAdd , fechaHora ,alignDer));

            if (txtMessages.InvokeRequired)
            {
                Invoke(new agregaraTexto(updateTXTMsgLista),null);
            }
            else
            {
                updateTXTMsgLista();
            }
        }

        private void updateTXTMsgLista()
        {
            try
            {
                string totalText = "";

                mensajesEnVentana.Sort(new mensajesSorter());

                foreach (Mensaje m in mensajesEnVentana)
                {
                    String textoAlign, fechaHoraAlign;

                    if (m.alignDer)
                    {
                        textoAlign = String.Format("{0,0} {1,46}", "", m.texto);
                        fechaHoraAlign = String.Format("{0,0} {1,46}", "", m.fechaHora);
                    }
                    else
                    {
                        textoAlign = m.texto;
                        fechaHoraAlign = m.fechaHora;
                    }

                    totalText = totalText + Environment.NewLine + fechaHoraAlign + Environment.NewLine + textoAlign;
                    
                }

                txtMessages.Text = totalText;

                //Scroll al final para ver siempre el ultimo mensaje.
                txtMessages.SelectionStart = txtMessages.TextLength;
                txtMessages.ScrollToCaret();

            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en updateTXTMsgLista(): " + ex.Message);
            }
        }

        // Logica de sort de mensajes.
        public class mensajesSorter : IComparer<Mensaje>
        {
            public int Compare(Mensaje m1, Mensaje m2)
            {
                string fecha1 = m1.fechaHora;
                string dato1 = m1.texto;

                string fecha2 = m2.fechaHora;
                string dato2 = m2.texto;

                if (fecha1 == fecha2)
                    return dato1.CompareTo(dato2);
                else
                    return fecha1.CompareTo(fecha2);
            }
        }




        private void updateTXTMsg(string v_txt, string fechaHora, string posicion, bool alignDer)
        {
            string totalText = "";
            v_txt = v_txt.Replace("\r", "").Replace("\n", "");


            String textoAlign, fechaHoraAlign;

            if (alignDer)
            {
                textoAlign = String.Format("{0,0} {1,46}", "", v_txt);
                fechaHoraAlign = String.Format("{0,0} {1,46}", "", fechaHora);
            }
            else
            {
                textoAlign = v_txt;
                fechaHoraAlign = fechaHora;
            }

            if (posicion == "FIN")
            {

                totalText = txtMessages.Text + Environment.NewLine + fechaHoraAlign + Environment.NewLine + textoAlign;
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


        private void actualizarViewMensajesPrevios(string v_mensajes)
        {
            string[] textos = v_mensajes.Split('|');

            foreach (string msg in textos)
            {

                Match textR = msgReg.Match(msg);
                if (textR.Success)
                {
                    string texto = getMatchData(textR, 1);
                    string fecha = getMatchData(textR, 2);
                    string source = getMatchData(textR, 3);
                    string dest = getMatchData(textR, 4);

                    addTextoToView(texto, fecha,  (source == "SERVER"));

                    DateTime fechaDT = DateTime.ParseExact(fecha, "yyyy-MM-dd HH:mm:ss", null);
                    if (fechaDT < firstReceived)
                        firstReceived = fechaDT;  // Actualiza el puntero de la fecha del primer mensaje

                    if ((fechaDT > g_lastReceived) && ( source != "SERVER"))
                        g_lastReceived = fechaDT;   // Actualiza el puntero de la fecha del ultimo mensaje recibido desde el device.
                }
            }


        }





        private void inicializar()
        {
            try
            {
                actualizeBanner("Actualizing...");
                string mensajes = cargarMensajesPrevios(CANT_MSG);
                actualizarViewMensajesPrevios(mensajes);
                
                //thread_actualizar = new Thread(startActualizar);
                //thread_actualizar.Start();

                Thread t = new Thread(startActualizar);
                t.Start();

//                Task.Factory.StartNew(() => startActualizar());
                
                actualizeBanner(deviceName);

            }
            catch (Exception) { }
        }


        public void actualizeBanner(string v_text)
        {

            if (lblBanner.InvokeRequired)
            {
                Invoke(new updateLabel(actTexto), v_text);
            }

        }
        private void actTexto(string v_t)
        {
            lblBanner.Text = v_t;
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

        // Thread que se ejecuta constantemente para actualizar la ventana de mensajes
        private void startActualizar()
        {

            try
            {
                while (!FinalizarActualizar.WaitOne(500))
                {
                    string nuevoMensaje = pollForNew(DEVICEID, ORGANIZATIONID, g_lastReceived);
                    if (!String.IsNullOrEmpty(nuevoMensaje))
                        addTextoToView(nuevoMensaje, g_lastReceived.ToString("yyyy-MM-dd HH:mm:ss"), false);
                }
            }
            catch (Exception)
            {
            }
        }


        private string pollForNew(int v_deviceID, int v_orgID, DateTime v_lastReceived)
        {
            string res = "";
                try
                {

                    string errDesc = "";
                    int errCode = -1;
                    string mensaje = WebServiceAPI.GetInstance().GetNewMessages(v_deviceID.ToString(), v_orgID.ToString(), v_lastReceived.ToString("yyyy-MM-dd HH:mm:ss"), out errDesc, out errCode);

                    Match headerResp = msgReg.Match(mensaje);
                            
                    if (headerResp.Success)
                    {
                        res = getMatchData(headerResp, 1);
                        string fecha = getMatchData(headerResp, 2);
                        g_lastReceived = Convert.ToDateTime(fecha);       // Actualiza la variable global con la fecha del mensaje recibido
                    }
                }
                catch (Exception ex) 
                {
                    Tools.GetInstance().DoLog("Excepcion en PollForNew(): " + ex.Message);
                }
            
            return res;

        }

        // Manda un pedido al server de los ulnimos n mensajes previos a firstReceived 
        private string cargarMensajesPrevios(int v_cantMsg)
        {
            string res = "";
           
            try
            {
                string errDesc = "";
                int errCode = -1;

                res = WebServiceAPI.GetInstance().GetPrevMessages(DEVICEID.ToString(), ORGANIZATIONID.ToString(), firstReceived.ToString("yyyy-MM-dd HH:mm:ss"), v_cantMsg.ToString(), out errDesc, out errCode);
            }
            catch (Exception ex) 
            {
                Tools.GetInstance().DoLog("Excepcion en cargarMensajesPrevios: " + ex.Message);
            }
            return res;
        }

      
        


      

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void txtToSend_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtMessages.Clear();
        }

        private void chkTriggerEvents_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTriggerEvents.Checked)
            {
                chkBlockingMode.Enabled = true;
                chkMasterCards.Enabled = true;
                //chkMasterCards.CheckedChanged -= chkMasterCards_CheckedChanged;
                //chkMasterCards.Checked = false;
                //chkMasterCards.CheckedChanged += chkMasterCards_CheckedChanged;
               
            }
            else
            {
                chkBlockingMode.Checked = false;
                chkBlockingMode.Enabled = false;
                chkMasterCards.Enabled = false;
                chkMasterCards.Checked = false;

            }
        }

        private void chkMasterCards_CheckedChanged(object sender, EventArgs e)
        {
            chkBlockingMode.CheckedChanged -= chkBlockingMode_CheckedChanged;
            chkBlockingMode.Checked = false;
            chkBlockingMode.CheckedChanged += chkBlockingMode_CheckedChanged;
        }

        private void chkBlockingMode_CheckedChanged(object sender, EventArgs e)
        {
            chkMasterCards.CheckedChanged -= chkMasterCards_CheckedChanged;
            chkMasterCards.Checked = false;
            chkMasterCards.CheckedChanged += chkMasterCards_CheckedChanged;
        }

        private void chkMobileController_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkAccessController_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {

        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            lblFirstName.Text = lblNombre.Text;
            lblLastName.Text = lblApellido.Text;
            lblSSNO.Text = lblCI.Text;
            lblBadge.Text = lblTarjeta.Text;
            if (searchedEmployee != null)
            {
                linkedPersonID = searchedEmployee.PersonID;

                if (searchedEmployee.imageDataBytes != null)
                {
                    picLinkedEmp.Image = Tools.GetInstance().ByteToImage(searchedEmployee.imageDataBytes, searchedEmployee.imageDataBytes.Length);
                }
                else
                    picLinkedEmp.Image = null;
            }

            if (searchedBadge != null)
                linkedBadge = searchedBadge.tarjeta;
        }

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
                        btnLink.Enabled = true;
                        searchedEmployee = datosEmpleado.Key;
                        searchedBadge = datosEmpleado.Value;

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
                        btnLink.Enabled = false;
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chkTriggerEvents.Enabled = true;
            chkBlockingMode.Enabled = true;
            chkMasterCards.Enabled = true;

            //tabControlProperties.TabPages.Remove(tabLinkCardholder);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chkTriggerEvents.Enabled = false;
            chkBlockingMode.Enabled = false;
            chkMasterCards.Enabled = false;

            if (!tabControlProperties.TabPages.Contains(tabLinkCardholder))
                tabControlProperties.TabPages.Add(tabLinkCardholder);
        }

        private void txtBadgeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBadgeSearch_Click(sender, e);
            }
        }

        private void frmProperties_FormClosing(object sender, FormClosingEventArgs e)
        {
            FinalizarActualizar.Set();      // Para liberar el task de actualizacion de mensajes
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtIMEI_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnManageIMEI_Click(object sender, EventArgs e)
        {
            frmManageIMEI ventana = new frmManageIMEI();
            ventana.ORGID = Tools.GetInstance().MainOrgID;
            ventana.ShowDialog();
            ventana.Dispose();
        }

        private void btnForceLogout_Click(object sender, EventArgs e)
        {
            try
            {
                string res = "";
                DialogResult ans = MessageBox.Show("This will log out the device: " + deviceName + ". Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (ans == DialogResult.Yes)
                {
                    string errDesc = "";
                    int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                    var respuesta = WebServiceAPI.GetInstance().UpdateLogInStatus(DEVICEID.ToString(), false, "7", out errDesc, out errCode);

                    if (respuesta == 200)
                    {
                        MessageBox.Show("Successfully logged out.");
                    }
                    else
                    {
                        MessageBox.Show("Can´t log out the current device.");
                    }
                }

            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en btnForceLogout_Click: " + ex.Message);
            }
        }

    }

    public class Mensaje
    {
        internal string texto { get; set; }
        internal string fechaHora { get; set; }
        internal bool alignDer { get; set; }


        internal Mensaje(string  v_texto, string v_fechaHora, bool v_alignDer)
        {
            this.texto = v_texto;
            this.fechaHora = v_fechaHora;
            this.alignDer = v_alignDer;
        }
    }

    


}
