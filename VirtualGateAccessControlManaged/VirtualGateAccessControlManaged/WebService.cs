using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace VirtualGateManaged
{

    public class WebServiceAPI
    {
        #region singleton
        static WebServiceAPI _instance;
        public static WebServiceAPI GetInstance()
        {
            if (_instance == null) _instance = new WebServiceAPI();
            return _instance;
        }

        WebServiceAPI()
        {
        }
        #endregion

        #region AlutelOnGuardAPI

        /// <summary>
        /// Envia addEmployee a AlutelMobilityAPI para que se de de alta en la base GPS
        /// </summary>
        public string AddEmployee(Employee emp, Tarjeta tarj, int v_panelID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;

            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Employee";

                string jsonParaEnviar = JsonConvert.SerializeObject(
                    new
                    {
                        deviceid = v_panelID,
                        employee = emp,
                        badge = tarj,
                        orgid = Helpers.GetInstance().MainOrgID,
                        updatemobilequeue = true
                    });

                string res = POSTRequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);

                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);

                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.AddEmployee: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.AddEmployee: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }

        /// <summary>
        /// LLamada para dar de alta un panel con chequeo de licencia.
        /// Devuelve OK o el string que se va a usar para levantar una alarma en Lenel
        /// </summary>
        public string AddZoneLicense(string zoneName, int panelID, string orgID)
        {
            string returnData = "OK";
            string strURL = @"http://" + Helpers.GetInstance().IPLicenseAPI + "/api/Panel/VirtualZones";
            try
            {
                Helpers.GetInstance().DoLog("Levantando zona " + zoneName + " con IDLenel=" + panelID);

                lock (VirtualGateManagedTranslator.ListaPanelIDs)
                {
                    if (!VirtualGateManagedTranslator.ListaPanelIDs.Contains(panelID))
                        VirtualGateManagedTranslator.ListaPanelIDs.Add(panelID);
                }

                lock (VirtualGateManagedTranslator.ListaPanelNames)
                {
                    if (!VirtualGateManagedTranslator.ListaPanelNames.Contains(zoneName))
                        VirtualGateManagedTranslator.ListaPanelNames.Add(zoneName);
                }


                string jsonParaEnviar = JsonConvert.SerializeObject(
                new
                {
                    Name = zoneName,
                    LNLID = panelID,
                    Organization = orgID
                });

                int errCode = -1;
                string errDesc = "";
                int HTTPStatus = (int)HttpStatusCode.BadRequest;
                string res = POSTRequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);
                if (HTTPStatus == (int)HttpStatusCode.BadRequest)
                {
                    returnData = errDesc;    // En errDesc viene la descripcion de la alarma que se va a mostrar en Lenel en caso de error 404
                    Helpers.GetInstance().DoLog("Zona=" + zoneName + " con IDLenel=" + panelID + " NO pasó el chequeo de Licencia");
                    Thread.Sleep(1000);    // 1 segundo de espera antes del retry...
                }
                else
                {
                    Helpers.GetInstance().DoLog("Zona=" + zoneName + " con IDLenel=" + panelID + " pasó el chequeo de Licencia");
                    AddZone(panelID.ToString(), zoneName, orgID, out errDesc, out errCode);

                    Helpers.GetInstance().DoLog("Va a actualizar el LNLPanelID=" + panelID + " en AlutelMobility para la Zona " + zoneName);
                    List<string> listaReaders = WebServiceAPI.GetInstance().ObtenerReadersDePanel(panelID.ToString());

                    Helpers.GetInstance().DoLog("Readers asociados: " + string.Join(",", listaReaders.ToArray()));

                    List<int> listaCF = WebServiceAPI.GetInstance().ObtenerCardformatsDePanel(panelID);


                    WebServiceAPI.GetInstance().ActualizarPanelIDEnEntidades(panelID, listaReaders, listaCF);

                }
            }
            catch (Exception ex)
            {
                returnData = "Error loading license data for zone " + zoneName;
                string errDesc = "Excepcion en AddZone(). zoneName=" + zoneName + " PanelID=" + panelID + " " + " URL=" + strURL + " " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                Thread.Sleep(1000);    // 1 segundo de espera antes del retry...
            }
            return returnData;
        }
        public void ActualizarPanelIDEnEntidades(int v_LNLPanelID, List<string> v_readers, List<int> v_CF)
        {
            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;

            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/UpdateLNLPanelID";

                string jsonParaEnviar = JsonConvert.SerializeObject(
                new
                {
                    LNLPanelID = v_LNLPanelID,
                    listaReaders = v_readers,
                    listaCardformats = v_CF
                });

                string res = PUTRequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc);

            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ActualizarPanelIDEnEntidades: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
            }

        }
        /// <summary>
        /// Devuelve la lista de IDs de Cardformats asociados a un panel.
        /// </summary>
        public List<int> ObtenerCardformatsDePanel(int LNLpanelID)
        {
            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            List<int> listaCF = new List<int>();
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerCardformatsDePanel&panelid=" + LNLpanelID;

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    var objetoAuxiliar = new { resultado = new List<int>(), errCode = 0, errDesc = "" };
                    var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);
                    listaCF = respuesta.resultado;
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerCardformatsDePanel " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return listaCF;
        }



        /// <summary>
        /// Devuelve la lista de NOMBRES de readers asociados al panel. Usa DCAPI para obtener esto
        /// NOTA: los Nombres de los readers son unicos en Lenel.
        /// </summary>
        public List<string> ObtenerReadersDePanel(string LNLpanelID)
        {
            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            List<string> listaReaders = new List<string>();
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerReadersDePanel&lnlpanelID=" + LNLpanelID;

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    var objetoAuxiliar = new { resultado = new List<string>(), errCode = 0, errDesc = "" };
                    var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);
                    listaReaders = respuesta.resultado;
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerReadersDePanel " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return listaReaders;
        }




        public string AddZone(string v_deviceID, string v_deviceName, string v_orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?VirtualZone&deviceid=" + v_deviceID + "&name=" + v_deviceName + "&orgid=" + v_orgID;
                int HTTPStatus = (int)HttpStatusCode.BadRequest;
                string res = POSTRequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc,out HTTPStatus);

                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.AddZone: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.AddZone: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                Helpers.GetInstance().DoLog(errDesc);
                returnData = "";
            }

            return returnData;
        }

        public string AddVirtualGate(string LNLPanelID, string DeviceName, string readerID, string readerName, string readerEntranceType, string organizationID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            int HTTPStatus = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?VirtualGate&deviceid=" + LNLPanelID + "&orgid=" + organizationID +"&readerid=" + readerID + "&readername=" + readerName + "&readerentrancetype=" + readerEntranceType;

                string res = POSTRequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;

                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.AddVirtualGate: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.AddVirtualGate: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }

        public string AddAccessLevel(string ALName, string LNLpanelID, string orgID, string accessLevelID, string TZReaderData, string isDownloadingDB, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?AccessLevel&deviceid=" + LNLpanelID + "&orgid=" + orgID + "&name=" + ALName + "&id=" + accessLevelID + "&tzreaderdata=" + TZReaderData + "&updatemobilequeue=true";

                string res = POSTRequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }

            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.AddAccessLevel: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.AddAccessLevel: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;
        }

        public string AddHolidays(string LNLpanelID, string idOrganization, string holidaysData, string holidaysNames, string isDownloadingDB, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Holiday&deviceid=" + LNLpanelID + "&orgid=" + idOrganization + "&holidaysdata=" + holidaysData + "&holidaysnames=" + holidaysNames;

                string res = POSTRequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }

            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.AddHolidays: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.AddHolidays: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;

        }

        public string AddTimeZone(string TZName, string LNL_PanelID, string organizationID, string TZNumber, string intervalData, string isDownloadingDB, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?TimeZone&deviceid=" + LNL_PanelID + "&orgid=" + organizationID + "&name=" + TZName + "&id=" + TZNumber + "&intervaldata=" + intervalData;

                string res = POSTRequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.AddTimeZone: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.AddTimeZone: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;
        }

        public string AssignSerialNums(string idSerials, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Event/SerialNum/List";

                string jsonParaEnviar = JsonConvert.SerializeObject(
                new
                {
                    idSerials = idSerials
                });

                string res = PUTRequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.AssignSerialNums: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.AssignSerialNums: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }
            return returnData;
        }

        public string AssignSerial(string alarmID, string tipoAlarm, string serialNum, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Event/SerialNum&eventid=" + alarmID + "&type=" + tipoAlarm + "&serialnum=" + serialNum;

                string res = POSTRequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.AssignSerial: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.AssignSerial: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }
            return returnData;

        }

        public void AssignSerialnumsAlarmas(List<AlarmaIDSerial> listaToSend, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Event/SerialNum/Alarm/List";

                string jsonParaEnviar = JsonConvert.SerializeObject(
               new
               {
                   AlarmasSerials = listaToSend
               });

                string res = PUTRequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.AssignSerialnumsAlarmas: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
            }
        }


        public string GetConnStatus(string DeviceID, string organizationID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?ConnStatus&deviceid=" + DeviceID + "&orgid=" + organizationID;

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = -1;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }

        //
        public bool GetConnStatusZoneGeneral()
        {
            bool returnData = false;
            string errDesc = "";
            int errCode = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?ConnStatusMobileGeneral";

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = true;
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = -1;            // -1: Excepcion
             
            }

            return returnData;
        }

       

        //public string GetAcceso(string DeviceID, string DeviceName, string organizationID, out string errDesc, out int errCode)
        //{
        //    string returnData = "";
        //    errDesc = "";
        //    errCode = -1;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Access&deviceid=" + DeviceID + "&devicename=" + DeviceName + "&orgid=" + organizationID;

        //        string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == (int)StatusCode.OK)
        //        {
        //            GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
        //            errDesc = gr.ErrorDescription;
        //            errCode = gr.ErrorCode;

        //            returnData = (string)gr.Result;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString());
        //        errDesc = "Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString();
        //        errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

        //        returnData = "";
        //    }

        //    return returnData;
        //}

        public Dictionary<string, List<Acceso>> GetAccesosGeneral(List<string> panelesActuales, string orgID)
        {
            string errDesc = "";
            int errCode = 0;
            Dictionary<string, List<Acceso>> accessData = null;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?AccesosGeneralVZones&orgid=" + orgID;

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {

                    var objetoAuxiliar = new { Result = new Dictionary<string, List<Acceso>>(), ErrorCode = 0, ErrorDescription = "" };
                    var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);

                    accessData = respuesta.Result; 
                }
            }
            catch (WebException ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString());
                errDesc = "Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                accessData = null;
            }

            try
            {
                // Solo se queda con los accesos que corresponden a los paneles que el LnlCommServer esta supervisando actualmente.
                if (accessData != null)
                {
                    Dictionary<string, List<Acceso>> returnData = new Dictionary<string, List<Acceso>>();

                    foreach (KeyValuePair<string, List<Acceso>> par in accessData)
                    {
                        if (panelesActuales.Contains(par.Key))
                        {
                            returnData.Add(par.Key, par.Value);
                        }
                    }
                    return returnData;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en Filtrado en GetAccesosGeneral: " + ex.ToString());
                return null;
            }
        }


        //public string GetAlarm(string DeviceID, string organizationID, out string errDesc, out int errCode)
        //{
        //    string returnData = "";
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Alarm&deviceid=" + DeviceID + "&orgid=" + organizationID;

        //        string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == (int)StatusCode.OK)
        //        {
        //            GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
        //            errDesc = gr.ErrorDescription;
        //            errCode = gr.ErrorCode;

        //            returnData = (string)gr.Result;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString());
        //        errDesc = "Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString();
        //        errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

        //        returnData = "";
        //    }

        //    return returnData;
        //}

        // En una sola llamada a OnGuardAPI obtiene todas las alarmas disponibles para todos las VGates.
        //public Dictionary<int, List<AlarmaAlutel>> GetAlarmasGeneral()
        //{
        //    Dictionary<int, List<AlarmaAlutel>> returnData = null;
        //    string errDesc = "";
        //    int errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?AlarmasGeneral";

        //        string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == (int)StatusCode.OK)
        //        {
        //            Dictionary<int, List<AlarmaAlutel>> gr = JsonConvert.DeserializeObject<Dictionary<int, List<AlarmaAlutel>>>(res);
        //            returnData = gr;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        errDesc = "Excepcion en GetAlarmasGeneral:" + ex.ToString();
        //        Helpers.GetInstance().DoLog(errDesc);
        //        errCode = (int)StatusCode.NOT_IMPLEMENTED;            // -1: Excepcion

        //    }

        //    return returnData;
        //}


        public string DeleteGate(string PanelID, string ReaderID, string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Gate&deviceid=" + PanelID + "&orgid=" + orgID + "&readerid=" + ReaderID;
                string res = DELETERequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc);

                if (errCode == (int)StatusCode.OK)          // 0: OK
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.DeleteGate: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.DeleteGate: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;

                returnData = "";
            }
            return returnData;
        }

        public string DeleteZone(string PanelID, string orgID, out string errDesc, out int errCode)
        {
            PoolGetConnStatus.GetInstance().subRefCount();                          // Para detener el thread y liberar memoria en caso de ser el ultimo panel que se da de baja.
            //PoolGetAlarm.GetInstance().subRefCount();
            PoolGetAcceso.GetInstance().subRefCount();
            PoolSetAcceso.GetInstance().subRefCount();
            //PoolSetAlarma.GetInstance().subRefCount();

            string returnData = "";
            errDesc = "";
            errCode = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Zone&deviceid=" + PanelID + "&orgid=" + orgID;
                string res = DELETERequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc);

                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.DeleteZone: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.DeleteZone: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;

                returnData = "";
            }

            return returnData;
        }

        public string DeleteEmployee(string Badge, string LNLpanelID, string LNLOrgID, string timeOUT, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Employee&deviceid=" + LNLpanelID + "&orgid=" + LNLOrgID + "&badge=" + Badge + "&timeout=" + timeOUT;
                
                string res = DELETERequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc);

                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.DeleteEmployee: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.DeleteEmployee: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }

        public string EraseEmployee(string badge, string LNLpanelID, string OrgID, string accessLevels, string activationDate, string deactivationDate, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Employee/AccessLevels&deviceid=" + LNLpanelID + "&orgid=" + OrgID + "&badge=" + badge + "&accesslevels=" + accessLevels + "&activationdate=" + activationDate + "&deactivationdate=" + deactivationDate;

                string res = DELETERequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc);

                if (errCode == (int)StatusCode.OK)          // 0: OK
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    returnData = (string)gr.Result;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.EraseEmployee: " + ex.ToString());

                errDesc = "Excepcion en WebServiceAPI.EraseEmployee: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;

                returnData = "";
            }

            return returnData;

        }

        public List<string> ObtenerBadgesAlutelmobility(string orgID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            List<string> res = new List<string>();
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Badge/Number/List&orgid=" + orgID;

                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);

                var objetoAuxiliar = new { Result = new List<string>(), ErrorCode = 0, ErrorDescription = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                res = respuesta.Result;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en ObtenerBadgesAlutelmobility " + ex.ToString());
                errDesc = "Excepcion en ObtenerBadgesAlutelmobility " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;
        }

        public string ObtenerAccessLevelsLenelDesdeAlutelMobility(string badge, int orgID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            string res = "";
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Badge/AccesslevelsLenel/List&badge=" + badge + "&orgid=" + orgID.ToString();

                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);

                var objetoAuxiliar = new { Result = "", ErrorCode = 0, ErrorDescription = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                res = respuesta.Result;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en ObtenerBadgeAccessLevelsLenel " + ex.ToString());
                errDesc = "Excepcion en ObtenerBadgeAccessLevelsLenel " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;
        }

        public string ObtenerBadgeActivationDates(string badge, int orgID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            string res = "";
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnguardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Badge/ActivationDates&badge=" + badge + "&orgid=" + orgID.ToString();

                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);

                var objetoAuxiliar = new { Result = "", ErrorCode = 0, ErrorDescription = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                res = respuesta.Result;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en ObtenerBadgeActivationDates " + ex.ToString());
                errDesc = "Excepcion en ObtenerBadgeActivationDates " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;
        }

        #endregion

        #region AlutelDataConduitAPI

        public KeyValuePair<Employee, Tarjeta> ObtenerDatosEmpleadoYTarjeta(string badge, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            KeyValuePair<Employee, Tarjeta> resultData = new KeyValuePair<Employee, Tarjeta>(null, null);

            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerDatosEmpleadoYTarjeta&badge=" + badge ;

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    var objetoAuxiliar = new { Empleado = new Employee(), Tarjeta = new Tarjeta(), errCode = 0, errDesc = "" };
                    var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);
                    respuesta.Tarjeta.OrgID = Helpers.GetInstance().MainOrgID;
                    resultData = new KeyValuePair<Employee, Tarjeta>(respuesta.Empleado, respuesta.Tarjeta);
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.ObtenerDatosEmpleadoYTarjeta " + ex.ToString());
                errDesc = "Excepcion en WebServiceAPI.ObtenerDatosEmpleadoYTarjeta " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return resultData;
        }

        public bool ExistePanelEnOnGuard(string deviceHHID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            bool res = true;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ExistePanelEnOnGuard&deviceHHID=" + deviceHHID;

                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    var objetoAuxiliar = new { resultado = false, errCode = 0, errDesc = "" };
                    var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                    return respuesta.resultado;
                }
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.ExistePanelEnOnGuard " + ex.ToString());
            }

            return res;
        }

        public string ObtenerNombreAccessLevel(string ALid, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            string res = "";
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerNombreAccessLevel&accessLevelID=" + ALid;
                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.ObtenerNombreAccessLevel " + ex.ToString());
            }

            return res;

        }

        public string ObtenerNombreTimeZone(string TZid, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            string res = "";
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerNombreTimeZone&timezoneID=" + TZid;
                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.ObtenerNombreTimeZone " + ex.ToString());
            }

            return res;
        }

        public string ObtenerNombresHolidays(out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            string res = "";
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerNombresHolidays";
                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.ObtenerNombresHolidays " + ex.ToString());
            }

            return res;
        }


        public List<int> ObtenerAccessLevelsDeUnPanel(int accessLevelID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            List<int> res = new List<int>();
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerAccessLevelsDeUnPanel&panelID=" + accessLevelID.ToString();

                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);

                var objetoAuxiliar = new { resultado = new List<int>(), errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en AgregarTarjeta " + ex.ToString());
                errDesc = "Excepcion en AgregarTarjeta " + ex.ToString();
                errCode = -1;
            }

            return res;
        }


        public string ObtenerAccessLevelsDesdeLenel(string badge, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            string res = "";
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerAccessLevelsDeTarjeta&badge=" + badge;
                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerAccessLevelsDesdeLenel " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;
        }




        #endregion

        #region helpers
        internal string GETRequest(string v_url, string v_token, out int errCode, out string errDesc)
        {
            string res = "";
            errCode = (int)StatusCode.INTERNAL_ERROR;
            errDesc = "";
            try
            {
                HttpWebRequest GETrequest = (HttpWebRequest)WebRequest.Create(v_url);
                GETrequest.Method = "GET";
                GETrequest.Headers["Authorization"] = v_token;

                HttpWebResponse GETresponse = (HttpWebResponse)GETrequest.GetResponse();
                Stream GETResponseStream = GETresponse.GetResponseStream();

                StreamReader reader = new StreamReader(GETResponseStream);
                res = reader.ReadToEnd();
                errCode = (int)StatusCode.OK;                     // 0:OK

            }
            catch (WebException ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en GETRequest " + ex.ToString());
                errDesc = "Excepcion en GETRequest " + ex.ToString();
                res = "";
            }

            return res;

        }

        internal string POSTRequest(string v_url, string v_jsonToSend, string v_token, out int errCode, out string errDesc, out int HTTPStatus)
        {
            byte[] dataByte = null;
            dataByte = UTF8Encoding.UTF8.GetBytes(v_jsonToSend);
            errCode = (int)StatusCode.INTERNAL_ERROR;
            HTTPStatus = (int)HttpStatusCode.BadRequest;

            errDesc = "";
            string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(v_url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = dataByte.Length;
                request.Headers["Authorization"] = v_token;

                Stream POSTstream = request.GetRequestStream();

                POSTstream.Write(dataByte, 0, dataByte.Length);

                //Obtengo la respuesta del webService
                HttpWebResponse POSTResponse = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(POSTResponse.GetResponseStream(), Encoding.UTF8);

                result = reader.ReadToEnd().ToString();
                errCode = (int)StatusCode.OK;
                HTTPStatus = (int)POSTResponse.StatusCode;
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            errDesc = reader.ReadToEnd();
                        }
                        HTTPStatus = (int)errorResponse.StatusCode;
                    }
                }
                Helpers.GetInstance().DoLog("Excepcion en POSTRequest " + errDesc + " URL=" + v_url + " MESSAGE=" + wex.Message);
                if (HTTPStatus != (int)HttpStatusCode.BadRequest)
                {
                    HTTPStatus = (int)HttpStatusCode.BadRequest;
                    errDesc = wex.Message;
                }
                result = "";
            }
            return result;
        }

        internal string PUTRequest(string v_url, string v_jsonToSend, string v_token, out int errCode, out string errDesc)
        {
            byte[] dataByte = null;
            dataByte = UTF8Encoding.UTF8.GetBytes(v_jsonToSend);
            errCode = (int)StatusCode.INTERNAL_ERROR;
            errDesc = "";
            string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(v_url);
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.ContentLength = dataByte.Length;
                request.Headers["Authorization"] = v_token;

                Stream PUTstream = request.GetRequestStream();

                PUTstream.Write(dataByte, 0, dataByte.Length);

                //Obtengo la respuesta del webService
                HttpWebResponse UPDATEResponse = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(UPDATEResponse.GetResponseStream(), Encoding.UTF8);

                result = reader.ReadToEnd().ToString();
                errCode = (int)StatusCode.OK;
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en PUTRequest " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                result = "";
            }
            return result;
        }

        internal string DELETERequest(string v_url, string v_jsonToSend, string v_token, out int errCode, out string errDesc)
        {
            byte[] dataByte = null;
            dataByte = UTF8Encoding.UTF8.GetBytes(v_jsonToSend);
            errCode = -1;
            errDesc = "";
            string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(v_url);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                request.ContentLength = dataByte.Length;
                request.Headers["Authorization"] = v_token;

                Stream DELETEstream = request.GetRequestStream();

                DELETEstream.Write(dataByte, 0, dataByte.Length);

                //Obtengo la respuesta del webService
                HttpWebResponse DELETEResponse = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(DELETEResponse.GetResponseStream(), Encoding.UTF8);

                result = reader.ReadToEnd().ToString();
                errCode = (int)StatusCode.OK;
            }
            catch (WebException ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en DELRequest " + ex.ToString());
                errDesc = "Excepcion en DELRequest " + ex.ToString();
                result = "";
            }
            return result;
        }

        #endregion
    }

    public class GenericResponse
    {
        public object Result { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }





}
