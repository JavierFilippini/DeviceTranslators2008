#define SERVER
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.ComponentModel;
using System.Management;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ManagedAccessControlTranslator
{

    public class ManagedAccessControl
    {
        static ManualResetEvent manualIsDownload = new ManualResetEvent(false);     // Manual reset static para asegurarse que el download DB se hace una sola vez y no muchas por cada instancia del translator

        public static List<int> ListaPanelIDs = new List<int>();                    // Lista de todos los panelIDs que levanta este CommServer..
        public static List<string> ListaPanelNames = new List<string>();            // Lista de todos los PanelNames que levanta este CommServer..

        //string m_Name;
        //int m_PanelID;

        // Constructor de la clase managed asociada al Translator.
        // Hay una instancia por cada Translator, y por lo tanto una por cada Panel.
        public ManagedAccessControl()
        {
            PoolGetConnStatus.GetInstance();                    // Lanza el thread de actualizacion de connStatusgeneral
            PoolGetConnStatus.GetInstance().addRefCount();      // Agrega un conteo de referencia 
            
            PoolGetAcceso.GetInstance();                        // Idem get accesos
            PoolGetAcceso.GetInstance().addRefCount();

            PoolGetAlarm.GetInstance();                         // Idem get Alarmas
            PoolGetAlarm.GetInstance().addRefCount();

            PoolSetAcceso.GetInstance();
            PoolSetAcceso.GetInstance().addRefCount();          // Idem setAccesos

            PoolSetAlarma.GetInstance();                        // Idem SetAlarmas
            PoolSetAlarma.GetInstance().addRefCount();
        }
        public void addPanelID(int panelID)
        {
            lock (ListaPanelIDs)
            {
                if (!ListaPanelIDs.Contains(panelID))
                    ListaPanelIDs.Add(panelID);
            }
        }

        public void addPanelName(string panelName)
        {
            lock (ListaPanelNames)
            {
                if (!ListaPanelNames.Contains(panelName))
                    ListaPanelNames.Add(panelName);
            }
        }

        //public string getName()
        //{
        //    return m_Name;
        //}
        //public void setName(string v_name)
        //{
        //    m_Name = v_name;
        //}

        //public int getPanelID()
        //{
        //    return m_PanelID;
        //}

        //public void setPanelID(int v_ID)
        //{
        //    m_PanelID = v_ID;
        //}


        // Llamada al WS que chequea la disponibilidad del panel en la licencia
        // Devuelve OK o el mensaje a mostrar en la alarma de Lenel
        public string altaPanelWS(string panelName, int panelID)
        {
            string res = WebServiceAPI.GetInstance().AddMobile(panelName, panelID, Helpers.GetInstance().MainOrgID.ToString());

            return res;
        }

        /// <summary>
        /// Recibe el alta de un reader a un Panel desde LENEL
        /// Contruye la expresion para mandarla al server y alli, en funcion del READERNAME, 
        /// se actualiza el LNLREADERENTRANCEID o el LNLREADEREXITID 
        /// </summary>
        public string addReader(string v_panelName, int v_panelID, string v_ReaderName, int v_ReaderID, int v_ReaderEntranceType, int v_OrgID, string v_cardFormats, int m_IsDownloadInProgress)
        {
            //            staticDatamanager.updateConfigurationVariables();
            string res = string.Empty;

            string deviceID = v_panelID.ToString();
            string deviceName = v_panelName;

            string readerID = v_ReaderID.ToString();
            string readerName = v_ReaderName;

            string readerEntranceType = v_ReaderEntranceType.ToString();

            string orgID = Helpers.GetInstance().MainOrgID.ToString();                // No toma la de la llamada

            try
            {
                //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();

                try
                {
                    string errDesc = "";
                    int errCode = -1;
                    string r = WebServiceAPI.GetInstance().AddReaderToPanel(deviceID, deviceName, readerID, readerName, readerEntranceType, orgID, v_cardFormats, out errDesc, out errCode);

                    if (errCode != (int)StatusCode.OK)
                        res = "FAIL";

                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en addReader: " + ex.Message);
                    res = "FAIL"; // Notificar la falla del evento
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en addReader-MUTEX: " + ex.Message);
            }
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}

            return res;
        }

        /// <summary>
        /// Mensaje para notificar la dada de baja de un Reader correspondiente a un Panel
        /// </summary>
        /// <param name="PanelID"></param>
        /// <param name="v_ReaderID"></param>
        public void deleteReader(int PanelID, int v_ReaderID)
        {

            //staticDatamanager.updateConfigurationVariables();

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            string readerID = v_ReaderID.ToString();
            string orgID = Helpers.GetInstance().MainOrgID.ToString();               // El configurado en el registro

            try
            {
                //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();
                try
                {
                    string errDesc = "";
                    int errCode = -1;
                    WebServiceAPI.GetInstance().DeleteReader(PanelID.ToString(), readerID, orgID, out errDesc, out errCode);
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en deleteReader: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en deleteReader-MUTEX: " + ex.Message);
            }
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}
        }

        /// <summary>
        /// Mensaje para notificar la dada de baja de Panel. 
        /// Solo se envia deletePanel si realmente se dio de baja y no existe en Lenel-
        /// Ademas usa DC para eliminar el usuario asociado al panel.
        /// </summary>
        public void deletePanel(int PanelID, string panelName)
        {
            PoolGetConnStatus.GetInstance().subRefCount();                          // Para detener el thread y liberar memoria en caso de ser el ultimo panel que se da de baja.
            PoolGetAlarm.GetInstance().subRefCount();
            PoolGetAcceso.GetInstance().subRefCount();
            PoolSetAlarma.GetInstance().subRefCount();
            PoolSetAcceso.GetInstance().subRefCount();
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            string orgID = Helpers.GetInstance().MainOrgID.ToString();                // El configurado en el registro
            try
            {
                //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();
                try
                {
                    string errDesc = "";
                    int errCode = -1;

                    if (!WebServiceAPI.GetInstance().ExistePanelEnOnGuard(PanelID.ToString(), out errDesc, out errCode))
                    {
                        WebServiceAPI.GetInstance().DeleteDevice(PanelID.ToString(), orgID, out errDesc, out errCode);
                        if (errCode == (int)StatusCode.OK)
                        {
                            Employee emp = WebServiceAPI.GetInstance().ObtenerEmpleadoAsociadoAHH(panelName, out errDesc, out errCode);
                            if (emp != null)
                            {
                                Helpers.GetInstance().DoLog("Va a borrar el empleado " + emp.Nombre + " " + emp.Apellido + " con personid: " + emp.PersonID.ToString() + " asociado al panelID: " + PanelID.ToString() + " llamado:" + panelName);
                                WebServiceAPI.GetInstance().EliminarEmpleado(emp.PersonID, out errDesc, out errCode);
                            }
                            else
                                Helpers.GetInstance().DoLog("emp es NULL en deletePanel. NO borró el empleado asociado al panel " + PanelID.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en deletePanel: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en deletePanelMUTEX: " + ex.Message);
            }
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}
        }

        /// <summary>
        /// getConnStatus: obtener el estado de la conexion del Handheld asociado al v_panelID
        /// YES: Conectado
        /// NO: No conectado
        /// </summary>
        /// <param name="v_PanelID"></param>
        /// <returns></returns>
        public string getConnStatus(int v_PanelID, int m_IsDownloadInProgress)
        {

            if (PoolGetConnStatus.GetInstance().getConnStatusMobile(v_PanelID))
                return "YES";
            else
                return "NO";

            //string res = "";
            //string deviceID = v_PanelID.ToString();
            //string orgID = Helpers.GetInstance().MainOrgID.ToString();

            //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();

            //try
            //{
            //    string errDesc = "";
            //    int errCode = -1;
            //    res = WebServiceAPI.GetInstance().GetConnStatus(deviceID, orgID, out errDesc, out errCode);
            //}
            //catch (Exception ex)
            //{
            //    Helpers.GetInstance().DoLog("EXCEPCION en getConnStatus: " + ex.Message);
            //    res = "FAIL";
            //}
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}


            //Thread.Sleep(500);
            //return res.Trim();
        }


        /// <summary>
        /// Recibe un pedido de eventos desde un Panel LENEL
        /// </summary>
        public string pollAlutrackForEvent(string v_panelName, int v_panelID, int v_serialnum)
        {
            string deviceID = v_panelID.ToString();
            string deviceName = v_panelName;
            string orgID = Helpers.GetInstance().MainOrgID.ToString();
            
            try
            {
                string datosAccesos = PoolGetAcceso.GetInstance().GetAccesos(deviceName);

                if (!String.IsNullOrEmpty(datosAccesos))
                    Helpers.GetInstance().DoLog("GetAccesos de " + v_panelName + "=" + datosAccesos);
                return datosAccesos;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en pollAlutrackForEvent: " + ex.Message);
                return "FAIL";
            }
        }

        public string pollAlutrackForAlarm(int v_panelID, int v_serialnum)
        {
            string LNLPanelID = v_panelID.ToString();
            string orgID = Helpers.GetInstance().MainOrgID.ToString();                

            try
            {
                string datosAlarma = PoolGetAlarm.GetInstance().GetAlarm(int.Parse(LNLPanelID));
                if (!String.IsNullOrEmpty(datosAlarma))
                    Helpers.GetInstance().DoLog("Datos de Alarma de PanelID=" + LNLPanelID + "=" + datosAlarma);
                return datosAlarma;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en pollAlutrackForAlarm: " + ex.Message);
                return "FAIL";
            }

        }


        /// <summary>
        /// Envia las asignaciones de serialnum a ID de accesos.
        /// </summary>
        /// <param name="serialsIDs"></param>
        public void sendIDSerials(string serialsIDs)
        {
            try
            {
                PoolSetAcceso.GetInstance().addSetAcceso(serialsIDs);
                Helpers.GetInstance().DoLog("Hecho addAcceso de: " + serialsIDs);
                if (PoolGetAcceso.GetInstance().isEmpty())
                {
                    //Helpers.GetInstance().DoLog("isEmpty dio True");
                    PoolSetAcceso.GetInstance().ContinuarPool();        // OK darlos de alta en AlutelMobility
                }
                //else
                //    Helpers.GetInstance().DoLog("isEmpty dio False");



            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en SendIDSerials: " + ex.Message);
            }

            Helpers.GetInstance().DoLog("Llamo a sendIDSerials con: " + serialsIDs);
        }

        /// <summary>
        /// Redefinicion del accesslevel para este panel. Viene vacio si se borro el panel del accesslevel.
        /// </summary>
        //public string addAccessLevel(int v_PanelID, int v_OrgID, int v_accessLevelID, string v_strTZReader, int m_IsDownloadInProgress)
        //{
        //    string res = string.Empty;
        //    //staticDatamanager.updateConfigurationVariables();
        //    string alName = "";
        //    string panelID = v_PanelID.ToString();
        //    string organizationID = Helpers.GetInstance().MainOrgID.ToString();                // No toma la de la llamada
        //    string accessLevelID = "";
        //    //Helpers.GetInstance().DoLog("Llama a addAccessLevel. PanelID: " + v_PanelID.ToString() + " v_accessLevelID: " + v_accessLevelID.ToString() + " TZData:" + v_strTZReader);
        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

        //    Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();
        //    try
        //    {


        //        if (!ALInitialized)
        //        {
        //            AccessLevelsDefinitionsDelPanel = Helpers.GetInstance().obtenerAccessLevelPanel(v_PanelID);  // En Lenel por dataconduit
        //            ALInitialized = true;
        //        }
        //        bool actualizar = false;                                                // Para filtrar las llamadas que vengan desde accesslevels que no pertenezcan al panel.
        //        if (String.IsNullOrEmpty(v_strTZReader))
        //        {
        //            if (AccessLevelsDefinitionsDelPanel.Contains(v_accessLevelID))
        //            {
        //                AccessLevelsDefinitionsDelPanel.Remove(v_accessLevelID);
        //                actualizar = true;
        //            }
        //        }
        //        else
        //        {
        //            actualizar = true;  // POr si cambio la info de readerTZ
        //            if (!AccessLevelsDefinitionsDelPanel.Contains(v_accessLevelID))
        //            {
        //                AccessLevelsDefinitionsDelPanel.Add(v_accessLevelID);
        //            }
        //        }

        //        if (actualizar)
        //        {
        //            accessLevelID = v_accessLevelID.ToString();
        //            alName = obtenerAccessLevelName(v_accessLevelID);
        //            string errDesc = "";
        //            int errCode = -1;
        //            WebServiceAPI.GetInstance().AddAccessLevel(alName, panelID, organizationID, accessLevelID, v_strTZReader, m_IsDownloadInProgress.ToString(), out errDesc, out errCode);

        //            if (errCode != (int)StatusCode.OK)
        //                res = "FAIL";
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        Helpers.GetInstance().DoLog("EXCEPCION en addAccessLevel: " + ex.Message);
        //        res = "FAIL";
        //    }

        //    finally
        //    {
        //        Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
        //    }

        //    return res;
        //}

        /// <summary>
        /// LLega la definicion de un tipo de Holiday. Mandarlo al server.
        /// Formato: TYPE:LNL_ADDHOLIDAYS,DEVICEID:(.*),ORGANIZATION:(.*),HOLIDAYSDATA:(.*),ISDOWNLOADINGDB:(.*)
        /// </summary>
        /// <param name="v_panelID"></param>
        /// <param name="v_OrganizationID"></param>
        /// <param name="strHolidayData"></param>
        public string addHolidays(int v_PanelID, int v_OrgID, string v_strHolidayData, int m_IsDownloadInProgress)
        {

            string res = string.Empty;
            //staticDatamanager.updateConfigurationVariables();

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            string holidaysNames = obtenerHolidayNames();

            string panelID = v_PanelID.ToString();
            string organizationID = Helpers.GetInstance().MainOrgID.ToString();                // No toma la de la llamada
            try
            {
                //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();
                try
                {
                    string errDesc = "";
                    int errCode = -1;
                    res = WebServiceAPI.GetInstance().AddHolidays(panelID, organizationID, v_strHolidayData, holidaysNames, m_IsDownloadInProgress.ToString(), out errDesc, out errCode);

                    if (errCode != (int)StatusCode.OK)
                        res = "FAIL";

                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en AddHolidays: " + ex.Message);
                    res = "FAIL";
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en AddHolidaysMUTEX: " + ex.Message);
            }
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}

            return res;
        }

        private string obtenerAccessLevelName(int v_ALid)
        {
            string res = "";

            try
            {
                string errDesc = "";
                int errCode = -1;
                res = WebServiceAPI.GetInstance().ObtenerNombreAccessLevel(v_ALid.ToString(), out errDesc, out errCode);
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en obtenerAccessLevelName: " + ex.Message);
            }

            return res.Replace(',', ' ').Replace('|', ' '); ;

        }

        private string obtenerTimeZoneName(int v_TZNum)
        {
            string res = "";

            try
            {
                string errDesc = "";
                int errCode = -1;
                res = WebServiceAPI.GetInstance().ObtenerNombreTimeZone(v_TZNum.ToString(), out errDesc, out errCode);

                return res.Replace(',', ' ').Replace('|', ' ');
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en obtenerTimeZoneName: " + ex.Message);
            }

            return res.Replace(',', ' ').Replace('|', ' ');
        }

        private string obtenerHolidayNames()
        {
            string res = "";

            try
            {
                string errDesc = "";
                int errCode = -1;
                res = WebServiceAPI.GetInstance().ObtenerNombresHolidays(out errDesc, out errCode);

            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en obtenerHolidayNames: " + ex.Message);
            }

            return res;

        }


        /// <summary>
        /// Llega la definicion de una TimeZone. Mandarla al server.
        /// Formato: TYPE:LNL_ADDTIMEZONE,DEVICEID:(.*),ORGANIZATION:(.*),TZNUMBER:(.*),TIMEZONEDATA:(.*),ISDOWNLOADINGDB:(.*)
        /// </summary>
        /// <param name="v_PanelID"></param>
        /// <param name="v_OrganizationID"></param>
        /// <param name="v_TZNumber"></param>
        /// <param name="strTimezoneData"></param>
        public string addTimezone(int v_PanelID, int v_OrgID, int v_TZNumber, string v_strTimezoneData, int m_IsDownloadInProgress)
        {
            string res = string.Empty;

            string tzName = obtenerTimeZoneName(v_TZNumber);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            string panelID = v_PanelID.ToString();
            string TZNumber = v_TZNumber.ToString();
            //string orgID = v_OrganizationID.ToString();
            string organizationID = Helpers.GetInstance().MainOrgID.ToString();                // No toma la de la llamada
            Helpers.GetInstance().DoLog("addTimeZone, PanelID= " +panelID + " orgID=" + v_OrgID + " TZNumber=" + TZNumber  + " timezoneData=" + v_strTimezoneData);
            try
            {
                //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();
                try
                {

                    string errDesc = "";
                    int errCode = -1;
                    WebServiceAPI.GetInstance().AddTimeZone(tzName, panelID, organizationID, TZNumber, v_strTimezoneData, m_IsDownloadInProgress.ToString(), out errDesc, out errCode);
                    if (errCode != (int)StatusCode.OK)
                        res = "FAIL";
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en addTimeZone: " + ex.Message);
                    res = "FAIL";
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en addTimeZoneMUTEX: " + ex.Message);
            }
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}

            return res;

        }

        // Borrar todos los cardformats de la organizacion
        public void enviarBorrarCF(int panel_id)
        {
            string organizationID = Helpers.GetInstance().MainOrgID.ToString();
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            try
            {
                //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();
                try
                {

                    string errDesc = "";
                    int errCode = -1;
                    WebServiceAPI.GetInstance().DeleteCardFormats(organizationID, panel_id.ToString(), out errDesc, out errCode);
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en enviarBorrarCF: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en enviarBorrarCFMUTEX: " + ex.Message);
            }
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}

        }

        //Envia el serialNum asignado a una alarma LENEL: Mensajes, speed, Panic, etc.
        public void AsignarSerialAAlarma(int v_alarmID, int v_serialNum, string tipoAlarma)
        {
            string organizationID = Helpers.GetInstance().MainOrgID.ToString();
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            try
            {
                try
                {
                    Helpers.GetInstance().DoLog("Hecho addSetAlarma de: alarmID=" + v_alarmID + " tipoAlarma=" + tipoAlarma + " serialNum=" + v_serialNum);
                    PoolSetAlarma.GetInstance().addSetAlarma(v_alarmID.ToString(), tipoAlarma, v_serialNum.ToString());
                    if (PoolGetAlarm.GetInstance().isEmpty())
                    {
//                        Helpers.GetInstance().DoLog("isEmpty de Alarma dio True");
                        PoolSetAlarma.GetInstance().ContinuarPoolSet();
                    }
                    //else
                    //    Helpers.GetInstance().DoLog("isEmpty dio False");
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en AsignarSerialAAlarma: ALARMID: " + v_alarmID.ToString() + ", SERIALNUM:" + v_serialNum + " - " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en AsignarSerialAAlarmaMUTEX: " + ex.Message);
            }

        }

        /// <summary>
        /// Obtiene los datos relevantes al badge especificado utilizando DataConduit, incluyendo la foto
        /// y los transmite al server con la expresion: 
        /// Nueva version: pasaje de Badge a string para soporte de numeros de 64 bits.
        /// </summary>
        /// <param name="v_badge"></param>
        /// <param name="v_panelID"></param>
        /// <returns></returns>
        //public string addEmployee(string v_badge, int v_panelID, string v_newAccesslevels, string v_fechaActivacion, string v_fechaDesactivacion, string v_PIN, int m_IsDownloadInProgress)
        //{

        //    string res = "OK";
        //    try
        //    {
        //        // Nota: solo interesan las fechas. NO las horas
        //        string v_newDates = v_fechaActivacion.Split(' ')[0] + "," + v_fechaDesactivacion.Split(' ')[0]; // Solo las fechas, no las horas.

        //        Helpers.GetInstance().DoLog("Panel: " + v_panelID.ToString() + "- AddEmployee, badge=" + v_badge + " accesslevels=" + v_newAccesslevels + " activationDates=" + v_newDates + " IsDownloadInProgress=" + m_IsDownloadInProgress);

        //        // Primero le saco la coma de más que viene desde C++.
        //        v_newAccesslevels = v_newAccesslevels.TrimEnd(',');

        //        string errDesc = "";
        //        int errCode =(int)StatusCode.NOT_FOUND;
        //        string desdeLenel = WebServiceAPI.GetInstance().ObtenerAccessLevelsDesdeLenel(v_badge,out errDesc, out errCode);
        //        if (errCode != (int)StatusCode.OK)
        //        {
        //            Helpers.GetInstance().DoLog("Error al obtenerAccessLevelsDesdeLenel para la tarjeta " + v_badge + " errDesc=" + errDesc + " errCode=" + errCode.ToString());
        //        }
        //        else
        //        {
        //            v_newAccesslevels = desdeLenel.TrimEnd(',');
        //            Helpers.GetInstance().DoLog("AccessLevels de la tarjeta " + v_badge + ":obtenidos desde Lenel:" + desdeLenel);
        //        }


        //        string v_prevAccessLevels = "";
        //        string v_prevDates = "";

        //        //Helpers.GetInstance().DoLog("Panel: " + v_panelID.ToString() + "- Cantidad de BadgeAccessLevels: " + BadgeAccessLevels.Count.ToString());
        //        if (m_IsDownloadInProgress == 0)   // Si esta haciendo DownloadDB, genero todos los Add
        //        {
        //            if (!BadgeAccessLevels.ContainsKey(v_badge))
        //            {
        //                errDesc = "";
        //                errCode = (int)StatusCode.OK;
        //                // Va a la base AlutelMobility para obtener los ids de accessLevels Lenel que tiene la tarjeta en la base.
        //                v_prevAccessLevels = WebServiceAPI.GetInstance().ObtenerAccessLevelsLenelDesdeAlutelMobility(v_badge, Helpers.GetInstance().MainOrgID, out errDesc, out errCode);

        //                if (errCode != (int)StatusCode.OK)
        //                    Helpers.GetInstance().DoLog("ERROR en addEmployee al obtenerBadgeAccessLevelsLenel de la tarjeta:" + v_badge + " :" + errDesc);

        //                v_prevDates = WebServiceAPI.GetInstance().ObtenerBadgeActivationDates(v_badge, Helpers.GetInstance().MainOrgID, out errDesc, out errCode);
        //                if (errCode != (int)StatusCode.OK)
        //                    Helpers.GetInstance().DoLog("ERROR en addEmployee al ObtenerBadgeActivationDates de la tarjeta:" + v_badge + " :" + errDesc);


        //                BadgeAccessLevels.Add(v_badge, v_prevAccessLevels + "|" + v_prevDates);
        //            }
        //            else
        //            {
        //                try { v_prevAccessLevels = BadgeAccessLevels[v_badge].Split('|')[0]; }
        //                catch (Exception) { }

        //                try { v_prevDates = BadgeAccessLevels[v_badge].Split('|')[1]; }
        //                catch (Exception) { }
        //            }
        //        }

        //        Helpers.GetInstance().DoLog("PrevAccessLevels de " + v_badge + "=" + v_prevAccessLevels);
        //        Helpers.GetInstance().DoLog("PrevDates de " + v_badge + "=" + v_prevDates);

        //        BadgeAccessLevels[v_badge] = v_newAccesslevels + "|" + v_newDates;          // Actualiza los AL y las fechas actuales.

        //        if (!ALInitialized)
        //        {
        //            AccessLevelsDefinitionsDelPanel = Helpers.GetInstance().obtenerAccessLevelPanel(v_panelID);
        //            ALInitialized = true;
        //        }

        //        bool doAdd = false;
        //        bool doErase = false;

        //        string[] newAccessLevels = v_newAccesslevels.Split(',');
        //        string[] prevAccesslevels = v_prevAccessLevels.Split(',');

        //        List<string> prevAccessLevelsDelPanel = new List<string>();         // Solo interesan los de este panel
        //        List<string> newAccessLevelsDelPanel = new List<string>();          // Solo interesan los de este panel

        //        bool tiene = false;                                                 // bool para determinar si en esta definicion tiene accesslevels del panel
        //        foreach (string s in newAccessLevels)
        //        {
        //            if (!String.IsNullOrEmpty(s))
        //            {
        //                if (AccessLevelsDefinitionsDelPanel.Contains(Convert.ToInt32(s)))
        //                {
        //                    tiene = true;                                           // Tiene un accessLevel del panel
        //                    newAccessLevelsDelPanel.Add(s);
        //                }
        //            }
        //        }

        //        bool tenia = false;                                                 // bool para determinar si ya tenía accesslevels del panel

        //        foreach (string s in prevAccesslevels)
        //        {
        //            if (!String.IsNullOrEmpty(s))
        //            {
        //                if (AccessLevelsDefinitionsDelPanel.Contains(Convert.ToInt32(s)))
        //                {
        //                    tenia = true;                  // Tenía un accessLevel del panel
        //                    prevAccessLevelsDelPanel.Add(s);
        //                }
        //            }
        //        }

        //        // Si tenía accessLevel del Panel y ahora no tiene, mando el LNL_DelEmployee
        //        if (tenia && !tiene)
        //            doErase = true;


        //        // Chequeo del add: Solo si cambio algun accessLeveldel panel: alguno nuevo o alguno se borro.
        //        // Elimino de la lista de nuevos los viejos
        //        // Elimino de la lista de viejos los nuevos
        //        // Si ambas estan vacias entonces no hago add
        //        List<string> AlErased = new List<string>();
        //        foreach (string s in newAccessLevelsDelPanel)
        //        {
        //            if (prevAccessLevelsDelPanel.Contains(s))
        //            {
        //                prevAccessLevelsDelPanel.Remove(s);
        //                AlErased.Add(s);
        //            }
        //        }

        //        foreach (string s in AlErased)
        //        {
        //            if (newAccessLevelsDelPanel.Contains(s))
        //            {
        //                newAccessLevelsDelPanel.Remove(s);
        //            }
        //        }

        //        if (!doErase)           // si no voy a borrarlo por accesslevels, y los accesslevels o las fechas difieren: hago un add
        //        {
        //            if ((v_prevDates != v_newDates) && (!String.IsNullOrEmpty(v_prevDates))) // Solo comparo la parte de los dias.
        //                doAdd = true;

        //            if ((newAccessLevelsDelPanel.Count > 0) || (prevAccessLevelsDelPanel.Count > 0))
        //                doAdd = true;
        //        }

        //        // Paso final: Enviar los add o erase.
        //        if (doAdd)
        //        {

        //            res = enviarAddEmployee(v_badge, v_panelID, v_newAccesslevels, v_fechaActivacion, v_fechaDesactivacion,v_PIN, m_IsDownloadInProgress);
        //            if (!ListaBadgesAlutelMobility.Contains(v_badge))
        //                ListaBadgesAlutelMobility.Add(v_badge);
        //        }

        //        if (doErase)
        //        {
        //            enviarEraseEmployee(v_badge, v_panelID, v_newAccesslevels, v_fechaActivacion, v_fechaDesactivacion);
        //        }

        //        if ((!doAdd) && (!doErase))
        //            Helpers.GetInstance().DoLog("AddEmployee de la tarjeta: " + v_badge + " no genero ni Add ni Erase. v_newAccesslevels= " + v_newAccesslevels + " v_prevAccessLevels = " + v_prevAccessLevels + " accesslevelsDelpanel=" + string.Join<int>(",",AccessLevelsDefinitionsDelPanel.ToArray())); 
                    
        //    }
        //    catch (Exception ex)
        //    {
        //        Helpers.GetInstance().DoLog("EXCEPCION en addEmployee: " + ex.Message);
        //        res = "FAIL";
        //    }

        //    return res;
        //}


        /// <summary>
        /// Envia LNL_ERASEEMPLOYEE al server para que se envie directamente al HH
        /// </summary>
        /// <param name="v_badge"></param>
        /// <param name="v_panelID"></param>
        /// <param name="v_newAccesslevels"></param>
        /// <param name="v_fechaActivacion"></param>
        /// <param name="v_fechaDesactivacion"></param>
        private void enviarEraseEmployee(string v_badge, int v_panelID, string v_newAccesslevels, string v_fechaActivacion, string v_fechaDesactivacion)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            try
            {
                //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();

                int errCode = 0;
                string errDesc = "";

                WebServiceAPI.GetInstance().EraseEmployee(v_badge, v_panelID.ToString(), Helpers.GetInstance().MainOrgID.ToString(), v_newAccesslevels, v_fechaActivacion, v_fechaDesactivacion, out errDesc, out errCode);

            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en enviarEraseEmployee: " + ex.Message);
            }
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}
        }

        private string enviarAddEmployee(string v_badge, int v_panelID, string v_newAccesslevels, string v_fechaActivacion, string v_fechaDesactivacion, string v_PIN, int m_IsDownloadInProgress)
        {
            string res = "FAIL";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string errDesc = "";

                KeyValuePair<Employee, Tarjeta> t = WebServiceAPI.GetInstance().ObtenerDatosEmpleadoYTarjeta(v_badge, out errDesc, out errCode);

                if (errCode != (int)StatusCode.OK)
                    Helpers.GetInstance().DoLog("ObtenerDatosEmpleadoYTarjeta devolvio el error: " + errDesc);

                if ((t.Key != null) && (t.Value != null))
                {
                    t.Value.accessLevels = v_newAccesslevels;
                    t.Value.PIN = v_PIN;

                    //Helpers.GetInstance().DoLog("Va a llamar a AddEmployee con tarjeta=" + t.Value.tarjeta + " badgekey=" + t.Value.lnlbadgekey);

                    WebServiceAPI.GetInstance().AddEmployee(t.Key, t.Value, v_panelID, out errDesc, out errCode);
                    if (errCode != (int)StatusCode.OK)
                        Helpers.GetInstance().DoLog("ERROR en enviarAddEmployee: " + errDesc);
                    else
                        res = "OK";

                    if (t.Key.isVisitor)
                    {
                        sendScheduledVisit(t.Key.PersonID.ToString(), t.Key.Nombre, t.Key.Apellido, t.Key.NumeroDocumento, v_badge, Helpers.GetInstance().MainOrgID.ToString());
                    }
                }
                else
                    Helpers.GetInstance().DoLog("No envia addEmployee por ser Empleado o tarjeta NULL" + errDesc);
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en enviarAddEmployee: " + ex.Message);
                res = "FAIL";
            }
            return res;
        }

        /// <summary>
        /// Usa DataConduit para obtener los datos correspondientes a la ultima visita asociada a v_personID (que es un visitor)
        /// Luego manda toda la info al Server.
        /// </summary>
        /// <param name="v_personID"></param>
        private void sendScheduledVisit(string v_personID, string visitorName, string visitorLastname, string visitorSSNO, string v_badge, string v_organizationID)
        {

            string hostID = "";
            DateTime scheduled_TimeIN = new DateTime();
            DateTime scheduled_TimeOUT = new DateTime();
            DateTime Time_IN = new DateTime();

            string errDesc = "";
            int errCode = -1;

            bool res = WebServiceAPI.GetInstance().ObtenerDatosUltimaVisita(v_personID, out hostID, out scheduled_TimeIN, out scheduled_TimeOUT, out Time_IN, out errDesc, out errCode);

            string hostName = "";
            string hostLastname = "";
            string hostSSNO = "";

            bool res2 = obtenerDatosEmpleado(hostID, ref  hostLastname, ref  hostLastname, ref  hostSSNO);

            if (res && res2)
            {
                //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();
                try
                {

                    errCode = -1;
                    errDesc = "";

                    WebServiceAPI.GetInstance().AddScheduledVisit(v_organizationID, v_badge, hostName, hostLastname, hostSSNO, visitorName, visitorLastname, visitorSSNO, scheduled_TimeIN.ToString("yyyy-MM-dd HH:mm:ss"), scheduled_TimeOUT.ToString("yyyy-MM-dd HH:mm:ss"), Time_IN.ToString("yyyy-MM-dd HH:mm:ss"), out errDesc, out errCode);

                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en sendScheduledVisit: " + ex.Message);
                }
                //finally
                //{
                //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
                //}
            }
        }

        bool obtenerDatosEmpleado(string PersonID, ref string name, ref string lastname, ref string SSNO)
        {
            bool res = false;

            try
            {
                int errCode = -1;
                string errDesc = "";
                Employee e = WebServiceAPI.GetInstance().ObtenerDatosEmpleado(PersonID, out errDesc, out errCode);
                if (e != null)
                {
                    name = e.Nombre;
                    lastname = e.Apellido;
                    SSNO = e.NumeroDocumento;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en obtenerDatosEmpleado: " + ex.Message);
            }
            return res;
        }

        // Envio de definicion de un cardFormat al Server.
        public void addCardFormat(int FormatID, int m_PanelID, int BitSize, int FC, int Offset, int BitsFC, int PositionStartFC, int BitsCardNum, int PositionStartCN, int BitsIssueCode, int PositionStartIC, int m_IsDownloadInProgress)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            string orgID = Helpers.GetInstance().MainOrgID.ToString();
            //string nomCF = obtenerNombreCardFormat(FormatID);
            if (BitsFC == 0) BitsFC++;
            if (BitsCardNum == 0) BitsCardNum++;
            if (BitsIssueCode == 0) BitsIssueCode++;

            //Helpers.GetInstance().mutexTCP_ACCESS.WaitOne();

            try
            {
                int errCode = -1;
                string errDesc = "";

                string nombreCardformat = WebServiceAPI.GetInstance().ObtenerNombreCardformat(FormatID.ToString(), out errDesc, out errCode);

                if (String.IsNullOrEmpty(nombreCardformat))
                {
                    Helpers.GetInstance().DoLog("ERROR en addCardFormat: No pudo obtener el Nombre del Cardformat " + FormatID.ToString());
                }

                errCode = -1;
                errDesc = "";

                WebServiceAPI.GetInstance().AddCardFormat(m_PanelID.ToString(), orgID, FormatID.ToString(), nombreCardformat,
                                                         FC.ToString(), Offset.ToString(), BitSize.ToString(),
                                                         PositionStartFC.ToString(), (PositionStartFC + BitsFC - 1).ToString(),
                                                         PositionStartCN.ToString(), (PositionStartCN + BitsCardNum - 1).ToString(),
                                                         PositionStartIC.ToString(), (PositionStartIC + BitsIssueCode - 1).ToString(),
                                                         out errDesc, out errCode);

            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en addCardFormat: " + ex.Message);
            }
            //finally
            //{
            //    Helpers.GetInstance().mutexTCP_ACCESS.ReleaseMutex();
            //}
        }

        public bool isDownloadSent()
        {
            bool res = manualIsDownload.WaitOne(0, false);

            //Helpers.GetInstance().DoLog("IsDownloadSent() devuelve: " + res.ToString());

            return res;
        }


        public void setIsDownloadSent()
        {
            manualIsDownload.Set();

            Helpers.GetInstance().DoLog("Hizo manualIsDownload.Set()");

        }

        public void resetIsDownloadSent()
        {
            manualIsDownload.Reset();

            Helpers.GetInstance().DoLog("Hizo manualIsDownload.Reset()");

        }

        public void TestCall(string texto)
        {
            Helpers.GetInstance().DoLog("Llama a TestCall con: " + texto);
        }



    }
}
