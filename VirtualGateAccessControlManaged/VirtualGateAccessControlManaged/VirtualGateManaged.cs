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
using System.Text.RegularExpressions;

namespace VirtualGateManaged
{

    public class VirtualGateManagedTranslator
    {
        //string m_Name;
        //int m_PanelID;
        public static List<int> ListaPanelIDs = new List<int>();                    // Lista de todos los panelIDs que levanta este CommServer..
        public static List<string> ListaPanelNames = new List<string>();            // Lista de todos los PanelNames que levanta este CommServer..

        public VirtualGateManagedTranslator()
        {
            PoolGetConnStatus.GetInstance();                    // Lanza el thread de actualizacion de ConnStatus
            PoolGetConnStatus.GetInstance().addRefCount();

            PoolGetAcceso.GetInstance();                        // Lanza el thread de actualizacion de Alarmas
            PoolGetAcceso.GetInstance().addRefCount();

            //PoolGetAlarm.GetInstance();                         // Lanza el thread de actualizacion de Accesos
            //PoolGetAlarm.GetInstance().addRefCount();

            PoolSetAcceso.GetInstance();
            PoolSetAcceso.GetInstance().addRefCount();

            //PoolSetAlarma.GetInstance();

            //PoolSetAlarma.GetInstance().addRefCount();

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

        // Llamada al WS que da de alta paneles Lenel en AlutelMobility con chequeo de disponibilidad
        // Devuelve OK o el mensaje a mostrar en la alarma de Lenel
        public string altaPanelWS(string zoneName, int panelID)
        {
            string res = WebServiceAPI.GetInstance().AddZoneLicense(zoneName, panelID, Helpers.GetInstance().MainOrgID.ToString());

            return res;
        }




//        /// <summary>
//        /// Recibe el alta de un nuevo Panel desde LENEL y lo manda al server
//        /// para que se de de alta o actualice su panelID.
//        /// </summary>
//        public string addZoneFromLenel(string v_panelName, int v_panelID, int m_IsDownloadInProgress)
//        {
//            Helpers.GetInstance().DoLog("Llama a addZoneFromLenel: " + v_panelName);
//            string res = "FAIL";
////            Helpers.GetInstance().updateConfigurationVariables(true);

//            //if (!Helpers.GetInstance().isWebServiceRunning())
//            //    return "FAIL";

//            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

//            string deviceID = v_panelID.ToString();
//            string deviceName = v_panelName;
//            string orgID = Helpers.GetInstance().MainOrgID.ToString();
//            try
//            {
//                Helpers.GetInstance().mutexTCP.WaitOne();
//                try
//                {
//                    string errDesc = "";
//                    int errCode = (int)StatusCode.NOT_IMPLEMENTED;
//                    // Contruyo la lista de BadgeAccessLevels desde el server para optimizar luego las llamadas add y del.
//                    string tarjetasActivas = WebServiceAPI.GetInstance().AddZone(deviceID,deviceName,orgID,out errDesc, out errCode);
//                    if (errCode != (int)StatusCode.OK)
//                    {
//                        Helpers.GetInstance().DoLog("Error al AddDevice(). errcode=" + errCode.ToString() + " errDesc:" + errDesc);
//                        return "FAIL";

//                    }

//                    //string[] badgesAL = tarjetasActivas.Split('|');
//                    ////                                MessageBox.Show("Badges Activas de " + deviceName + ":" + badges.Length.ToString());
//                    //if (badgesAL.Length >= 2)
//                    //{
//                    //    for (int i = 0; i < badgesAL.Length; i = i + 2)
//                    //    {
//                    //        string badge = badgesAL[i];
//                    //        string accessLevels = badgesAL[i + 1];

//                    //        if (!BadgeAccessLevels.ContainsKey(badge))
//                    //            BadgeAccessLevels.Add(badge, accessLevels);
//                    //        else
//                    //            BadgeAccessLevels[badge] = accessLevels;
//                    //    }
//                    //}
//                    res = "OK";
//                }
//                catch (Exception  ex)
//                {
//                    Helpers.GetInstance().DoLog("EXCEPCION en addZoneFromLenel: " + ex.Message);
//                    res = "FAIL";
//                }
//            }
//            catch (Exception ex) 
//            {
//                Helpers.GetInstance().DoLog("EXCEPCION en addZoneFromLenel-MUTEX: " + ex.Message);
//            }
//            finally
//            {
//                Helpers.GetInstance().mutexTCP.ReleaseMutex();
//            } 
            
//            return res;
//        }

        /// <summary>
        /// Recibe el alta de un reader a un Panel desde LENEL
        /// Contruye la expresion para mandarla al server y alli, en funcion del READERNAME, 
        /// se actualiza el LNLREADERENTRANCEID o el LNLREADEREXITID ( ESTO VA A CAMBIAR CUANDO PUEDA GENERAR Y DETECTAR LOS READERTYPES desde el driver)
        /// </summary>
        public string addGate(string v_panelName, int v_panelID, string v_ReaderName, int v_ReaderID, int v_ReaderEntranceType,  int m_IsDownloadInProgress)
        {
            string res = string.Empty;

            string deviceID = v_panelID.ToString();
            string deviceName = v_panelName;

            string readerID = v_ReaderID.ToString();
            string readerName = v_ReaderName;

            string readerEntranceType = v_ReaderEntranceType.ToString();
            string orgID = Helpers.GetInstance().MainOrgID.ToString();            // No toma la de la llamada.
            try
            {
                //Helpers.GetInstance().mutexTCP.WaitOne();
                try
                {

                    string errDesc = "";
                    int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                    string r = WebServiceAPI.GetInstance().AddVirtualGate(deviceID, deviceName, readerID, readerName, readerEntranceType, orgID, out errDesc, out errCode);

                    if (errCode != (int)StatusCode.OK)
                        res = "FAIL";

                }
                catch (Exception ex )
                {
                    Helpers.GetInstance().DoLog("Excepcion en addGate: " + ex.Message);
                    res = "FAIL";
                }
            }
            catch (Exception) { }
         
            return res;
        }

        /// <summary>
        /// Mensaje para notificar la dada de baja de un Reader(Gate) de una Zona
        /// </summary>
        /// <param name="PanelID"></param>
        /// <param name="v_ReaderID"></param>
        public void deleteGate(int PanelID, int v_ReaderID)
        {
            //Helpers.GetInstance().updateConfigurationVariables();

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            string readerID = v_ReaderID.ToString();
            string orgID = Helpers.GetInstance().MainOrgID.ToString();
            try
            {
                //Helpers.GetInstance().mutexTCP.WaitOne();
                try
                {
                    string errDesc = "";
                    int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                    WebServiceAPI.GetInstance().DeleteGate(PanelID.ToString(), readerID, orgID, out errDesc, out errCode);
                }
                catch (Exception) { }
            }
            catch (Exception) { }
            
        }

        // Se llama en el destructor del Panel de la zona.
        public void deleteZone(int PanelID)
        {
            PoolGetConnStatus.GetInstance().subRefCount();
            //PoolGetAlarm.GetInstance().subRefCount();
            PoolGetAcceso.GetInstance().subRefCount();

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            string orgID = Helpers.GetInstance().MainOrgID.ToString();               // El configurado en el registro
            try
            {
                //Helpers.GetInstance().mutexTCP.WaitOne();
                try
                {

                    string errDesc = "";
                    int errCode = (int)StatusCode.NOT_IMPLEMENTED;

                    if (!WebServiceAPI.GetInstance().ExistePanelEnOnGuard(PanelID.ToString(), out errDesc, out errCode))
                    {
                        WebServiceAPI.GetInstance().DeleteZone(PanelID.ToString(), orgID, out errDesc, out errCode);
                    }

                }
                catch (Exception) { }
            }
            catch (Exception) { }
            
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
            string res = "";
            //Helpers.GetInstance().updateConfigurationVariables();

            string deviceID = v_PanelID.ToString();
            string orgID = Helpers.GetInstance().MainOrgID.ToString();                // No toma la de la llamada

            try
            {
                //string errDesc = "";
                //int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                //res = WebServiceAPI.GetInstance().GetConnStatus(deviceID, orgID, out errDesc, out errCode);

                res = PoolGetConnStatus.GetInstance().getConnStatusZone()? "YES": "NO";


            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en getConnStatus: " + ex.Message);
                res = "FAIL";
            }

            Thread.Sleep(500);
            return res.Trim();
        }

        /// <summary>
        /// Recibe un pedido de eventos desde un Panel LENEL
        /// </summary>
        public string pollAlutrackForEvent(string v_panelName, int v_panelID, int v_OrgID)
        {
            //Helpers.GetInstance().updateConfigurationVariables();
               
            //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            string deviceID = v_panelID.ToString();
            string deviceName = v_panelName;
            string orgID = Helpers.GetInstance().MainOrgID.ToString();            // No toma la de la llamada

               
            try
            {
                string datosAccesos = PoolGetAcceso.GetInstance().GetAccesos(deviceName);
                if (!String.IsNullOrEmpty(datosAccesos))
                    Helpers.GetInstance().DoLog("GetAccesos de " + v_panelName + "=" + datosAccesos);

                return datosAccesos;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en pollAlutrackForEvent de VZone: " + ex.Message);
                return "FAIL"; 
            }
            
        }

        public string pollAlutrackForAlarm(int v_panelID, int v_serialnum)
        {
            string LNLPanelID = v_panelID.ToString();
            string orgID = Helpers.GetInstance().MainOrgID.ToString();



            try
            {
                //string errDesc = "";
                //int errCode = -1;
                //string datosAccesos = WebServiceAPI.GetInstance().GetAlarm(LNLPanelID, orgID, out errDesc, out errCode);

                //string datosAlarma = PoolGetAlarm.GetInstance().GetAlarm(int.Parse(LNLPanelID));
                string datosAlarma = "";
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
        }

        /// <summary>
        /// Borra un empleado de la asociacion a un dispositivo. Para eso, el server desasocia el empleado de la tarjeta especificada
        /// Aqui solo se envia la expresion para dar de baja esta asociacion y se envia al server.
        /// Nueva version: pasaje de Badge a string para soporte de numeros de 64 bits.
        /// </summary>
        /// <param name="v_badge"></param>
        //public void delEmployee(string v_badge, int v_PanelID)
        //{

        //    Helpers.GetInstance().DoLog("Llamo a delEmployee con: " + v_badge + " v_panelID=" + v_PanelID);
        //    try
        //    {
        //        if (!ALInitialized)
        //        {
        //            AccessLevelsDefinitionsDelPanel = Helpers.GetInstance().obtenerAccessLevelPanel(v_PanelID);
        //            ALInitialized = true;
        //        }

        //        if (ListaBadgesAlutelMobility.Contains(v_badge))
        //        {
        //            enviarDelEmployee(v_badge, v_PanelID);
        //            ListaBadgesAlutelMobility.Remove(v_badge);
        //        }

        //        if (BadgeAccessLevels.ContainsKey(v_badge))
        //            BadgeAccessLevels[v_badge] = "";
        //        else
        //            BadgeAccessLevels.Add(v_badge, "");

        //    }
        //    catch (Exception ex)
        //    {
        //        Helpers.GetInstance().DoLog("EXCEPCION en delEmployee: " + ex.Message);
        //    }




            //Helpers.GetInstance().updateConfigurationVariables();

            //if (!ALInitialized)
            //{
            //    AccessLevelsDefinitionsDelPanel = Helpers.GetInstance().obtenerAccessLevelPanel(v_PanelID);
            //    ALInitialized = true;
            //}

            //if (BadgeAccessLevels.ContainsKey(v_badge))
            //{
            //    // Si estaba activa para este panel y llego a delEmployee, significa que se le sacaron todos los AL. Mando DEL
            //    // BadgeAccessLevels contiene los AL de la tarjeta ANTES de esta llamada.
            //    if (Helpers.GetInstance().isBadgeActiveByAL(BadgeAccessLevels[v_badge], AccessLevelsDefinitionsDelPanel,deletedAccessLevel))
            //    {
            //        enviarDelEmployee(v_badge, v_PanelID);
            //        BadgeAccessLevels[v_badge] = "";
            //    }
            //}
        //}

        //private void enviarDelEmployee(string v_badge, int v_PanelID)
        //{
        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

        //    string badge = v_badge;
        //    string panelID = v_PanelID.ToString();
        //    try
        //    {
        //        Helpers.GetInstance().mutexTCP.WaitOne();
        //        try
        //        {
        //            string ahoraStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        //            string errDesc = "";
        //            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //            WebServiceAPI.GetInstance().DeleteEmployee(badge, panelID, Helpers.GetInstance().MainOrgID.ToString(), ahoraStr, out errDesc, out errCode);

        //            // Limpia los accesslevels del panel para que cualquier add siguiente se acepte.
        //            if (BadgeAccessLevels.ContainsKey(v_badge))
        //                BadgeAccessLevels[v_badge] = "";
        //        }
        //        catch (Exception) { }
        //    }
        //    catch (Exception) { }
        //    finally
        //    {
        //        Helpers.GetInstance().mutexTCP.ReleaseMutex();
        //    } 
        //}

        /// <summary>
        /// LLega la definicion de un accessLevel. Mandarlo al server.
        /// Formato: 
        /// TYPE:LNL_ADDACCESSLEVEL,DEVICEID:(.*),ORGANIZATION:(.*),ACCESSLEVELID:(.*),TZREADERDATA:(.*),ISDOWNLOADINGDB:(.*)")
        /// TZREADERDATA tiene el formato ReaderID_1,TZ_1,ReaderID_2,TZ_2..
        /// </summary>
        /// <param name="v_panelID"></param>
        /// <param name="v_OrganizationID"></param>
        /// <param name="v_accessLevelID"></param>
        /// <param name="v_strTZReader"></param>
        //public string addAccessLevel(int v_PanelID,int v_OrgID,int v_accessLevelID,string v_strTZReader, int m_IsDownloadInProgress )
        //{

        //    string res = string.Empty;
        //    //staticDatamanager.updateConfigurationVariables();
        //    string alName = "";
        //    string panelID = v_PanelID.ToString();
        //    string organizationID = Helpers.GetInstance().MainOrgID.ToString();                // No toma la de la llamada
        //    string accessLevelID = "";
        //    //Helpers.GetInstance().DoLog("Llama a addAccessLevel. PanelID: " + v_PanelID.ToString() + " v_accessLevelID: " + v_accessLevelID.ToString() + " TZData:" + v_strTZReader);
        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

        //    Helpers.GetInstance().mutexTCP.WaitOne();
        //    try
        //    {

        //        if (!ALInitialized)
        //        {
        //            AccessLevelsDefinitionsDelPanel = Helpers.GetInstance().obtenerAccessLevelPanel(v_PanelID);
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
        //         if (actualizar)
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
        //        Helpers.GetInstance().mutexTCP.ReleaseMutex();
        //    }

        //    return res;

        //    //string res = string.Empty;
        //    ////Helpers.GetInstance().updateConfigurationVariables();

        //    //string alName = obtenerAccessLevelName(v_accessLevelID);

        //    //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

        //    //string panelID = v_PanelID.ToString();
        //    //string organizationID = Helpers.GetInstance().MainOrgID.ToString();               // no toma la de la llamada.

        //    //if (!ALInitialized)
        //    //{
        //    //    AccessLevelsDefinitionsDelPanel = Helpers.GetInstance().obtenerAccessLevelPanel(v_PanelID);
        //    //    ALInitialized = true;
        //    //}

        //    //deletedAccessLevel = -1;                                // TODO nuevo accesslevel que se agregue resetea el ultimo borrado.
        //    //if (String.IsNullOrEmpty(v_strTZReader))
        //    //{
        //    //    if (AccessLevelsDefinitionsDelPanel.Contains(v_accessLevelID))
        //    //    {
        //    //        AccessLevelsDefinitionsDelPanel.Remove(v_accessLevelID);
        //    //        deletedAccessLevel = v_accessLevelID;                   // Para chequear en por el eraseEmployee hasta que se agregue un nuevo AccessLevel
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (!AccessLevelsDefinitionsDelPanel.Contains(v_accessLevelID))
        //    //        AccessLevelsDefinitionsDelPanel.Add(v_accessLevelID);
        //    //}

        //    //string accessLevelID = v_accessLevelID.ToString();
        //    //try
        //    //{
        //    //    Helpers.GetInstance().mutexTCP.WaitOne();
        //    //    try
        //    //    {
        //    //        string errDesc = "";
        //    //        int errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    //        WebServiceAPI.GetInstance().AddAccessLevel(alName, panelID, organizationID, accessLevelID, v_strTZReader, m_IsDownloadInProgress.ToString(), out errDesc, out errCode);

        //    //        if (errCode != (int)StatusCode.OK)
        //    //            res = "FAIL";

        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        Helpers.GetInstance().DoLog("EXCEPCION en addAccessLevel de VirtualZone: " + ex.Message);
        //    //        res = "FAIL";
        //    //    }
        //    //}
        //    //catch (Exception) { }
        //    //finally
        //    //{
        //    //    Helpers.GetInstance().mutexTCP.ReleaseMutex();
        //    //} 
            
        //    //return res;
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
           
            //Helpers.GetInstance().updateConfigurationVariables();

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            string holidaysNames = obtenerHolidayNames();

            string panelID = v_PanelID.ToString();
            //string organizationID = v_OrganizationID.ToString();
            string organizationID = Helpers.GetInstance().MainOrgID.ToString();               // No toma la de la llamada.

            try
            {
              
                try
                {
                    string errDesc = "";
                    int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                    res = WebServiceAPI.GetInstance().AddHolidays(panelID, organizationID, v_strHolidayData, holidaysNames, m_IsDownloadInProgress.ToString(), out errDesc, out errCode);

                    if (errCode != (int)StatusCode.OK)
                        res = "FAIL";
                       
                }
                catch (Exception)
                {
                    res = "FAIL";
                }
            }
            catch (Exception) { }
           
           
            return res;
        }


        private string obtenerAccessLevelName(int v_ALid)
        {
            string res = "";

            try
            {
                string errDesc = "";
                int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                res = WebServiceAPI.GetInstance().ObtenerNombreAccessLevel(v_ALid.ToString(), out errDesc, out errCode);
            }
            catch (Exception ex) 
            {
                Helpers.GetInstance().DoLog("Excepcion en obtenerAccessLevelName de VZ: " + ex.Message);
            }

            return res.Replace(',', ' ').Replace('|', ' '); ;
        }



        private string obtenerTimeZoneName(int v_TZNum)
        {
            string res = "";
           
            try
            {
                string errDesc = "";
                int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                res = WebServiceAPI.GetInstance().ObtenerNombreTimeZone(v_TZNum.ToString(), out errDesc, out errCode);

                return res.Replace(',', ' ').Replace('|', ' ');
            }
            catch (Exception ex) 
            {
                Helpers.GetInstance().DoLog("Excepcion en obtenerTimeZoneName de VZ: " + ex.Message);
            }

            return res.Replace(',', ' ').Replace('|', ' ');
        }

        private string obtenerHolidayNames()
        {
            string res = "";

            try
            {
                string errDesc = "";
                int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                res = WebServiceAPI.GetInstance().ObtenerNombresHolidays(out errDesc, out errCode);

            }
            catch (Exception ex) 
            {
                Helpers.GetInstance().DoLog("Excepcion en obtenerHolidayNames de VZ: " + ex.Message);
            }

            return res.Replace('|', ' ');
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

            //Helpers.GetInstance().updateConfigurationVariables();

            string tzName = obtenerTimeZoneName(v_TZNumber);

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            string panelID = v_PanelID.ToString();
            string TZNumber = v_TZNumber.ToString();
            //string organizationID = v_OrganizationID.ToString();
            string organizationID = Helpers.GetInstance().MainOrgID.ToString();               // No toma la de la llamada.
            try
            {
              
                try
                {

                    string errDesc = "";
                    int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                    WebServiceAPI.GetInstance().AddTimeZone(tzName, panelID, organizationID, TZNumber, v_strTimezoneData, m_IsDownloadInProgress.ToString(), out errDesc, out errCode);
                    if (errCode != (int)StatusCode.OK)
                        res = "FAIL";
                        
                }
                catch (Exception)
                {
                    res = "FAIL";
                }
            }
            catch (Exception) { }
                       
            return res;
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
                   // PoolSetAlarma.GetInstance().addSetAlarma(v_alarmID.ToString(), tipoAlarma, v_serialNum.ToString());
                }
                catch (Exception ex)
                {
                    Helpers.GetInstance().DoLog("EXCEPCION en AsignarSerialAAlarma VG: ALARMID: " + v_alarmID.ToString() + ", SERIALNUM:" + v_serialNum + " - " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("EXCEPCION en AsignarSerialAAlarmaMUTEX VG: " + ex.Message);
            }
          

        }

        
        /// <summary>
        /// Envia LNL_ERASEEMPLOYEE al server para que se envie directamente al HH
        /// </summary>
        private void enviarEraseEmployee(string v_badge, int v_panelID,string v_newAccesslevels, string v_fechaActivacion, string v_fechaDesactivacion)
        {
            try
            {
                int errCode = (int)StatusCode.NOT_IMPLEMENTED;
                string errDesc = "";

                WebServiceAPI.GetInstance().EraseEmployee(v_badge, v_panelID.ToString(), Helpers.GetInstance().MainOrgID.ToString(), v_newAccesslevels, v_fechaActivacion, v_fechaDesactivacion, out errDesc, out errCode);
            }
            catch (Exception) { }
           
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
                    Helpers.GetInstance().DoLog("ERROR en ObtenerDatosEmpleadoYTarjeta de VG: " + errDesc);

                if ((t.Key != null) && (t.Value != null))
                {
                    t.Value.accessLevels = v_newAccesslevels;
                    t.Value.PIN = v_PIN;

                    //Helpers.GetInstance().DoLog("Va a llamar a AddEmployee con tarjeta=" + t.Value.tarjeta + " badgekey=" + t.Value.lnlbadgekey);

                    WebServiceAPI.GetInstance().AddEmployee(t.Key, t.Value, v_panelID, out errDesc, out errCode);
                    if (errCode != (int)StatusCode.OK)
                        Helpers.GetInstance().DoLog("ERROR en enviarAddEmployee de VG: " + errDesc);
                }
                else
                    Helpers.GetInstance().DoLog("En VG no envia addEmployee por ser Empleado o tarjeta NULL" + errDesc);

            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en enviarAddEmployee: " + ex.Message);
                res = "FAIL";
            }
            return res;
        }
        
              
    }
}
