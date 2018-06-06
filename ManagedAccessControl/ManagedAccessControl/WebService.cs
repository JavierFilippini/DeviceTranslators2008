using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace ManagedAccessControlTranslator
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
        /// Devuelve el Empleado y la Tarjeta asociado al deviceID.
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="orgID"></param>
        /// <param name="errDesc"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public KeyValuePair<Employee, Tarjeta> GetLinkedEmployee(int deviceID, int orgID, out string errDesc, out int errCode)
        {
            KeyValuePair<Employee, Tarjeta> returnData = new KeyValuePair<Employee, Tarjeta>(null, null);

            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/LinkedEmployee&deviceid=" + deviceID.ToString() + "&orgid=" + orgID.ToString();

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    var objetoAuxiliar = new { emp = new Employee(), badge = new Tarjeta() };

                    var respuesta = JsonConvert.DeserializeAnonymousType((string)gr.Result, objetoAuxiliar);
                    
                    Employee emp = respuesta.emp;
                    Tarjeta tarj = respuesta.badge;

                    if (emp == null) emp = new Employee();
                    if (tarj == null) tarj = new Tarjeta();

                    returnData = new KeyValuePair<Employee, Tarjeta>(emp, tarj);
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en WebServiceAPI.GetLinkedEmployee: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return returnData;
        }


        /// <summary>
        /// Envia addEmployee usando AlutelMobilityAPI para que se de de alta en la base GPS
        /// </summary>
        public string AddEmployee(Employee emp, Tarjeta tarj, int v_panelID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Employee";

                string jsonParaEnviar = JsonConvert.SerializeObject(
                    new
                    {
                        deviceid = v_panelID,
                        employee = emp,
                        badge = tarj,
                        orgid= Helpers.GetInstance().MainOrgID,
                        updatemobilequeue = true
                    });

                //Helpers.GetInstance().DoLog("JSON en AddEmployee:" + jsonParaEnviar);


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
                errDesc = "Excepcion en WebServiceAPI.AddEmployee: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }
        /// <summary>
        /// LLamada desde el translator al dar de alta un panel
        /// </summary>
        public string AddMobile(string panelName, int panelID, string orgID)
        {
            string returnData = "OK";
            string strURL = @"http://" + Helpers.GetInstance().IPLicenseAPI + "/api/Panel/Mobiles";
            try
            {
                Helpers.GetInstance().DoLog("Levantando panel " + panelName + " con IDLenel=" + panelID);
                lock (ManagedAccessControl.ListaPanelIDs)
                {
                    if (!ManagedAccessControl.ListaPanelIDs.Contains(panelID))
                        ManagedAccessControl.ListaPanelIDs.Add(panelID);
                }

                lock (ManagedAccessControl.ListaPanelNames)
                {
                    if (!ManagedAccessControl.ListaPanelNames.Contains(panelName))
                        ManagedAccessControl.ListaPanelNames.Add(panelName);
                }

                string jsonParaEnviar = JsonConvert.SerializeObject(
                new
                {
                    Name = panelName,
                    LNLID= panelID,
                    Organization= orgID
                });

                int errCode = -1;
                string errDesc = "";
                int HTTPStatus = (int)HttpStatusCode.BadRequest;
                string res = POSTRequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);
                if (HTTPStatus == (int)HttpStatusCode.BadRequest)
                {
                    returnData = errDesc;  // En errDesc viene la descripcion de la alarma que se va a mostrar en Lenel en caso de error 404
                    Helpers.GetInstance().DoLog("Panelname=" + panelName + " con IDLenel=" + panelID + " NO pasó el chequeo de Licencia");
                    Thread.Sleep(1000);    // 1 segundo de espera antes del retry...
                }
                else
                {
                    Helpers.GetInstance().DoLog("Panelname=" + panelName + " con IDLenel=" + panelID + " pasó el chequeo de Licencia");
                    AddDevice(panelID.ToString(), panelName, orgID, null, null, out errDesc, out errCode);

                    Helpers.GetInstance().DoLog("Va a actualizar el LNLPanelID=" + panelID + " en AlutelMobility para el panel " + panelName);
                    List<string> listaReaders = WebServiceAPI.GetInstance().ObtenerReadersDePanel(panelID.ToString());

                    Helpers.GetInstance().DoLog("Readers asociados: " + string.Join(",", listaReaders.ToArray()));

                    List<int> listaCF = WebServiceAPI.GetInstance().ObtenerCardformatsDePanel(panelID);

                    WebServiceAPI.GetInstance().ActualizarPanelIDEnEntidades(panelID, listaReaders, listaCF);
                }
            }
            catch (Exception ex)
            {
                returnData = "Error loading license data for panel " + panelName; 
                string errDesc = "Excepcion en AddMobile(). PanelName=" + panelName + " PanelID=" + panelID + " URL=" + strURL + " " + ex.ToString();
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
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/UpdateLNLPanelID";

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

        public string AddDevice(string v_deviceID, string v_deviceName, string v_orgID, Employee emp, Tarjeta tarjeta, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device";

                string jsonParaEnviar = JsonConvert.SerializeObject(
                new
                {
                    deviceid = v_deviceID,
                    devicename = v_deviceName,
                    orgid = v_orgID,
                    employee = emp,
                    badge = tarjeta
                });

                int HTTPStatus = (int)HttpStatusCode.BadRequest;

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
                errDesc = "Excepcion en WebServiceAPI.addDevice: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;
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

        
        /// <summary>
        /// Devuelve la lista de IDs de Cardformats asociados a un panel.
        /// </summary>
        public List<int> ObtenerCardformatsDePanel(int LNLpanelID)
        {
            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            List<int> listaCF= new List<int>();
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

        public string AddReaderToPanel(string LNLPanelID, string DeviceName, string readerID, string readerName, string readerEntranceType, string organizationID, string cardFormats, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;

            List<CardFormats> newCF = new List<CardFormats>();
            string[] newCFids = cardFormats.Split(',');

            foreach (string s in newCFids)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    CardFormats c = WebServiceAPI.GetInstance().ObtenerCardFormatDesdeLenel(s, out errDesc, out errCode);
                    if (c != null)
                    {
                        c.IdOrganizacion = int.Parse(organizationID);
                        c.Desfasaje = "0";                      // Hardcodeo en branch feature_offsetCeroEnCF
                        newCF.Add(c);
                    }
                    else
                        Helpers.GetInstance().DoLog("El Cardformat con ID: " + s + " dio NULL en AddReaderToPanel()");
                }
            }

            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/Reader";

                string jsonParaEnviar = JsonConvert.SerializeObject(
                    new
                    {
                        deviceid = LNLPanelID,
                        devicename = DeviceName,
                        readerid = readerID,
                        readername = readerName,
                        readerentrancetype = readerEntranceType,
                        orgid = organizationID,
                        cardformats = newCF,
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
                errDesc = "Excepcion en WebServiceAPI.AddReaderToPanel: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;
        }

        public string AddAccessLevel(string ALName, string LNLpanelID, string orgID, string accessLevelID, string TZReaderData, string isDownloadingDB, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?AccessLevel&deviceid=" + LNLpanelID + "&orgid=" + orgID + "&name=" + ALName + "&id=" + accessLevelID + "&tzreaderdata=" + TZReaderData + "&updatemobilequeue=true";

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
                errDesc = "Excepcion en WebServiceAPI.AddAccessLevel: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        public string AddHolidays( string LNLpanelID ,string idOrganization ,string holidaysData ,string holidaysNames ,string isDownloadingDB, out string errDesc, out int errCode)
        {
           string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Holiday&deviceid=" + LNLpanelID + "&orgid=" + idOrganization + "&holidaysdata=" + holidaysData + "&holidaysnames=" + holidaysNames;

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
                errDesc = "Excepcion en WebServiceAPI.AddHolidays: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;

        }

        public string AddTimeZone(string TZName, string LNL_PanelID, string organizationID, string TZNumber, string intervalData, string isDownloadingDB, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?TimeZone&deviceid=" + LNL_PanelID + "&orgid=" + organizationID + "&name=" + TZName + "&id=" + TZNumber + "&intervaldata=" + intervalData + "&integrador=0&updatemobilequeue=true";

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
                errDesc = "Excepcion en WebServiceAPI.AddTimeZone: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;
        }

        public string AssignSerialNums(string idSerials, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Event/SerialNum/List";

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
                errDesc = "Excepcion en WebServiceAPI.AssignSerialNums: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }
            return returnData;
        }

        public string AssignSerial(string alarmID, string tipoAlarm, string serialNum, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Event/SerialNum&eventid="+ alarmID + "&type=" + tipoAlarm + "&serialnum=" + serialNum;

                string res = PUTRequest(strURL, "", Helpers.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.AssignSerial: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }
            return returnData;
        }


        public void AssignSerialnumsAlarmas(List<AlarmaIDSerial> listaToSend, out string  errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Event/SerialNum/Alarm/List";

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
        
        public string AddScheduledVisit(string orgID, string badge, string hostName, string hostLastname, string hostSSNO, string visitorName, string visitorLastname, string visitorSSNO, string scheduledTimeIN, string sheduledTimeOUT, string timeIN, out string errDesc, out int errCode)
        {

            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Visit&orgid=" + orgID + "&badge=" + badge + "&hostname=" + hostName + "&hostssno=" + hostSSNO + "&visitorname=" + visitorName + "&visitorlastname=" + visitorLastname + "&visitorssno=" + visitorSSNO +"&scheduledtimein=" + scheduledTimeIN + "&scheduledtimeout" + sheduledTimeOUT + "&timein=" + timeIN;

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
                errDesc = "Excepcion en WebServiceAPI.AddScheduledVisit: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }
            return returnData;


        }
        // panelid = "", orgid = "", cardFormatid = "", name = "", fcode = "", offset = "", numberofbits = "", fcstart = "", fcend = "", cardnumberstart = "", cardnumberend = "", issuecodestart = "", issuecodeend="" };
        public string AddCardFormat(string panelID, string orgID, string cardFormatID, string Name, string FCode, string Offset, string NumberOfBits, string FCStart, string FCEnd, string CardNumberStart, string CardNumberEnd, string IssueCodeStart, string IssueCodeEnd, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            try
            {
                Offset = "0";           // Hadrcodeo en branch feature_offsetCeroEnCF

                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?CardFormat&deviceid=" + panelID + "&orgid=" + orgID + "&cardformatid=" + cardFormatID +"&name=" +Name + "&fcode=" +  FCode + "&offset=" + Offset + "&numberofbits=" + NumberOfBits + "&fcstart=" + FCStart + "&fcend=" + FCEnd + "&cardnumberstart=" + CardNumberStart + "&cardnumberend=" + CardNumberEnd + "&issuecodestart=" + IssueCodeStart + "&issuecodeend=" + IssueCodeEnd;

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
                errDesc = "Excepcion en WebServiceAPI.addCardFormat: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }
            return returnData;

        }

       
        public string GetConnStatus(string DeviceID, string organizationID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?ConnStatus&deviceid=" + DeviceID + "&orgid=" + organizationID;

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
                errCode = (int)StatusCode.NOT_IMPLEMENTED;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }

        // En una sola llamada a OnGuardAPI obtiene todos los connStatus de todos los devices Lenel dados de alta.
        public Dictionary<int, bool> GetConnStatusMobileGeneral()
        {
            Dictionary<int, bool> returnData = null;
            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?ConnStatusMobileGeneral";

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    Dictionary<int,bool> gr = JsonConvert.DeserializeObject<Dictionary<int,bool>>(res);
                    returnData = gr;
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en GetConnStatusGeneral:" + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.NOT_IMPLEMENTED;            // -1: Excepcion

            }

            return returnData;
        }

        // En una sola llamada a OnGuardAPI obtiene todas las alarmas disponibles para todos los devices.
        public Dictionary<int, List<AlarmaAlutel>> GetAlarmasGeneral(List<int> panelesActuales)
        {
            Dictionary<int, List<AlarmaAlutel>> alarmData = null;
            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?AlarmasGeneral";

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    Dictionary<int, List<AlarmaAlutel>> gr = JsonConvert.DeserializeObject<Dictionary<int, List<AlarmaAlutel>>>(res);
                    alarmData = gr;
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en GetAlarmasGeneral:" + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.NOT_IMPLEMENTED;            // -1: Excepcion

            }

            try
            {
                if (alarmData != null)
                {
                    Dictionary<int, List<AlarmaAlutel>> returnData = new Dictionary<int, List<AlarmaAlutel>>();
                    foreach (KeyValuePair<int, List<AlarmaAlutel>> par in alarmData)
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
                Helpers.GetInstance().DoLog("Excepcion en Filtrado en GetAlarmasGeneral: " + ex.ToString());
                return null;
            }
        }

        public string GetAcceso(string DeviceID, string DeviceName, string organizationID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Access&deviceid=" + DeviceID + "&devicename=" + DeviceName + "&orgid=" + organizationID;
               
                string res = GETRequest(strURL,  Helpers.GetInstance().Token, out errCode, out errDesc   );
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
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString());
                errDesc = "Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData= "";
            }

            return returnData;
        }

        public Dictionary<string, List<Acceso>> GetAccesosGeneral(List<string> panelesActuales,string orgID )
        {
            string errDesc = "";
            int errCode = 0;
            Dictionary<string, List<Acceso>> accessData = null; 
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?AccesosGeneral&orgid=" + orgID;

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
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.GetAccesosGeneral: " + ex.ToString());
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



        public string GetAlarm(string DeviceID,  string organizationID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Alarm&deviceid=" + DeviceID + "&orgid=" + organizationID;

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
                Helpers.GetInstance().DoLog("Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString());
                errDesc = "Excepcion en WebServiceAPI.GetAcceso: " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }


        public string DeleteReader(string PanelID, string ReaderID, string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Reader&deviceid=" + PanelID + "&orgid=" + orgID + "&readerid=" + ReaderID;

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
                errDesc = "Excepcion en WebServiceAPI.DeleteReader: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }

        public string DeleteDevice(string PanelID, string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device&deviceid="+ PanelID + "&orgid=" + orgID;

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
                errDesc = "Excepcion en WebServiceAPI.DeleteDevice: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;
        }

        public string DeleteEmployee(string Badge, string LNLpanelID, string LNLOrgID, string timeOUT, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Employee&deviceid=" + LNLpanelID + "&orgid=" + LNLOrgID + "&badge=" + Badge + "&timeout=" + timeOUT;

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
                errDesc = "Excepcion en WebServiceAPI.DeleteEmployee: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;
        }

        public string EraseEmployee(string badge, string LNLpanelID, string OrgID, string accessLevels, string activationDate, string deactivationDate, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Employee/AccessLevels&deviceid=" + LNLpanelID + "&orgid=" + OrgID + "&badge=" + badge + "&accesslevels=" + accessLevels + "&activationdate=" + activationDate + "&deactivationdate=" + deactivationDate;

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
                errDesc = "Excepcion en WebServiceAPI.EraseEmployee: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;

        }

        public string DeleteCardFormats(string orgID, string panelID,out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?CardFormat/All&deviceid=" + panelID + "&orgid=" + orgID;

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
                errDesc = "Excepcion en WebServiceAPI.deleteCardFormats: " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }

            return returnData;
        }

        public List<string> ObtenerBadgesAlutelmobility(string orgID,out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            List<string> res = new List<string>();
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Badge/Number/List&orgid=" + orgID;

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


        


        public string ObtenerAccessLevelsLenelDesdeAlutelMobility( string badge,int orgID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
           string res = "";
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Badge/AccesslevelsLenel/List&badge=" + badge + "&orgid=" + orgID.ToString();

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
                string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Badge/ActivationDates&badge=" + badge + "&orgid=" + orgID.ToString();

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

        public KeyValuePair<Employee, Tarjeta> ObtenerDatosEmpleadoYTarjeta(string badge,out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            KeyValuePair<Employee,Tarjeta> resultData = new KeyValuePair<Employee,Tarjeta>(null,null);

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
                errDesc = "Excepcion en WebServiceAPI.ObtenerDatosEmpleadoYTarjeta " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return resultData;
        }

        public Employee ObtenerEmpleadoAsociadoAHH(string deviceName, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            Employee resultData = null;

            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerEmpleadoAsociadoAHH&deviceName=" + deviceName;

                string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    var objetoAuxiliar = new { resultado = new Employee(), errCode = 0, errDesc = "" };
                    var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);

                    resultData = respuesta.resultado;
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerEmpleadoAsociadoAHHPorPanelID " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return resultData;
        }

        public bool ExistePanelEnOnGuard(string deviceHHID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            bool res = true;
            try
            {
                    string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI+ "/AlutelDataConduitAPI/DataConduitAPI?ExistePanelEnOnGuard&deviceHHID=" + deviceHHID;

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
                errDesc = "Excepcion en WebServiceAPI.ExistePanelEnOnGuard " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;
        }

        //public bool ExisteTarjeta(string tarjeta, out string errDesc, out int errCode)
        //{
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    bool res = false;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPwsAlutelOGAPI + "/AlutelDataConduitAPI/DataConduitAPI?existeTarjeta&tarjeta=" + tarjeta;
               
        //        string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == (int)StatusCode.OK)
        //        {
        //            var objetoAuxiliar = new { resultado = false, errCode = 0, errDesc = "" };
        //            var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
        //            res= respuesta.resultado;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errDesc = "Excepcion en WebServiceAPI.ExisteTarjeta " + ex.ToString();
        //        errCode = (int)StatusCode.INTERNAL_ERROR;

        //        Helpers.GetInstance().DoLog(errDesc);
        //        res= false;
        //    }

        //    return res;
        //}

        //public Employee ObtenerDatosEmpleadoPorLastName(string lastname, out string errDesc, out int errCode)
        //{
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    Employee res = null;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPwsAlutelOGAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerDatosEmpleadoPorLastName&lastName=" + lastname;

        //        string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == (int)StatusCode.OK)
        //        {

        //            var objetoAuxiliar = new { resultado = new Employee(), errCode = 0, errDesc = "" };
        //            var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
        //            res = respuesta.resultado;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errDesc = "Excepcion en WebServiceAPI.ObtenrDatosEmpleadoPorLastName " + ex.ToString();
        //        Helpers.GetInstance().DoLog(errDesc);
                
        //        errCode = (int)StatusCode.INTERNAL_ERROR;
        //    }
        //    return res;
        //}

        //public long ObtenerNumeroTarjetaAsociadaAEmpleado(int personID, out string errDesc, out int errCode)
        //{
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    long res = -1;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPwsAlutelOGAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerBadge&personID=" + personID.ToString();

        //        string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == (int)StatusCode.OK)
        //        {

        //            var objetoAuxiliar = new { resultado = 0, errCode = 0, errDesc = "" };
        //            var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
        //            res = respuesta.resultado;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errDesc = "Excepcion en WebServiceAPI.ObtenerNumeroTarjetaAsociadaAEmpleado " + ex.ToString();
        //        Helpers.GetInstance().DoLog(errDesc);

        //        errCode = (int)StatusCode.INTERNAL_ERROR;
        //    }
        //    return res;
        //}


        public Employee ObtenerDatosEmpleado(string PersonID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            Employee res = null;
            try
            {

                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerDatosEmpleado&personID=" + PersonID;

                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);

                var objetoAuxiliar = new { Empleado = new Employee(), errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res = respuesta.Empleado;
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerDatosEmpleado " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;
        }

        public string ObtenerNombreCardformat(string cardfrmatID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            string res = "";
            try
            {
             
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerNombreCardFormat&cardFormatID=" + cardfrmatID;
                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res= respuesta.resultado;
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerNombreCardfomat " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;

        }

        public CardFormats ObtenerCardFormatDesdeLenel(string v_cardFormatID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;

            string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerCardFormatDesdeLenel&idCF=" + v_cardFormatID;
            try
            {
                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = new CardFormats(), errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                //Helpers.GetInstance().DoLog("NOmbre del CF obtenido para el ID: " + v_cardFormatID  + " : " + respuesta.resultado.Nombre);
                return respuesta.resultado;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en wsDCManager.ObtenerCardFormatDesdeLenel " + ex.ToString());
                return null;
            }
        }

        public string ObtenerNombreAccessLevel(string ALid, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            string res = "";
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerNombreAccessLevel&accessLevelID=" + ALid;
                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res= respuesta.resultado;
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerNombreAccessLevel " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;

        }

        public string ObtenerNombreTimeZone(string TZid, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
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
                errDesc = "Excepcion en WebServiceAPI.ObtenerNombreTimeZone " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;
        }

        public string ObtenerNombresHolidays(out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
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
                errDesc = "Excepcion en WebServiceAPI.ObtenerNombresHolidays " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return res;
        }

        public bool ObtenerDatosUltimaVisita(string personID, out string hostID, out DateTime scheduled_TimeIN, out DateTime scheduled_TimeOUT, out DateTime Time_IN, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            bool res = false;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerDatosUltimaVisita&personID=" + personID;
                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { hostID = "",scheduled_TimeIN = new DateTime(), scheduled_TimeOUT = new DateTime(), Time_IN = new DateTime(), errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                hostID = respuesta.hostID;
                scheduled_TimeIN = respuesta.scheduled_TimeIN;
                scheduled_TimeOUT = respuesta.scheduled_TimeOUT;
                Time_IN = respuesta.Time_IN;
                res = true;
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerNombreTimeZone " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                hostID = "";
                scheduled_TimeIN = new DateTime();
                scheduled_TimeOUT = new DateTime();
                Time_IN = new DateTime();
                errCode = (int)StatusCode.INTERNAL_ERROR;
                res = false;
            }
            return res;
        }

        public List<int> ObtenerAccessLevelsDeUnPanel(int panelID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            List<int> res = new List<int>();
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerAccessLevelsDeUnPanel&panelID=" + panelID.ToString();

                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);

                var objetoAuxiliar = new { resultado = new List<int>(), errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en ObtenerAccessLevelsDeUnPanel " + ex.ToString());
                errDesc = "Excepcion en ObtenerAccessLevelsDeUnPanel " + ex.ToString();
                errCode = (int)StatusCode.INTERNAL_ERROR;
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



       

        public int AgregarEmpleado(string Nombre, string Apellido, string CI, string Empresa, byte[] imagen, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            int res = -1;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?AgregarEmpleado";

                //Armo parametros para enviar
                string jsonParaEnviar = JsonConvert.SerializeObject(
                    new
                    {
                        Nombre = Nombre,
                        Apellido = Apellido,
                        CI = CI,
                        Empresa = Empresa,
                        Imagen = imagen
                    }
                );

                string data = POSTRequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);

                var objetoAuxiliar = new { resultado = 0, errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en AgregarEmpleado " + ex.ToString());
                errDesc = "Excepcion en AgregarEmpleado " + ex.ToString();
              

                errCode = (int)StatusCode.NOT_IMPLEMENTED;
                res = -1;
            }

            return res;
        }

        public int AgregarTarjeta(int type, int status,string tarjeta, string personID , out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            int HTTPStatus = (int)HttpStatusCode.BadRequest;
            int res = -1;
            if (type == 0) type = 1;
            if (status == 0) status = 1;

            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?AgregarTarjeta";

                //Armo parametros para enviar
                string jsonParaEnviar = JsonConvert.SerializeObject(
                    new
                    {
                        type = type,
                        status = status,
                        tarjeta = tarjeta,
                        personID = personID
                    }
                );

                string data = POSTRequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc, out HTTPStatus);

                var objetoAuxiliar = new { resultado = 0, errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                if (respuesta.errCode != 0)
                {
                    Helpers.GetInstance().DoLog("AgregarTarjeta devolvio error: " + respuesta.errDesc);
                }
                errCode = respuesta.errCode;
                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en AgregarTarjeta " + ex.ToString();
                Helpers.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.NOT_IMPLEMENTED;
                res = -1;
            }

            return res;
        }


        public Tarjeta ObtenerTarjeta(string tarjeta, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            Tarjeta res = null;

            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerTarjeta&badge=" + tarjeta;

                string data = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);

                var objetoAuxiliar = new { resultado =new Tarjeta(), errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en ObtenerTarjeta " + ex.ToString());
                errDesc = "Excepcion en ObtenerTarjeta " + ex.ToString();
                errCode = (int)StatusCode.NOT_IMPLEMENTED;
            }

            return res;
        }
        public void EliminarEmpleado(int personID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Helpers.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?EliminarEmpleado";

                //Armo parametros para enviar
                string jsonParaEnviar = JsonConvert.SerializeObject(
                    new
                    {
                        Empid = personID
                    }
                );

                string data = DELETERequest(strURL, jsonParaEnviar, Helpers.GetInstance().Token, out errCode, out errDesc);

                var objetoAuxiliar = new { resultado = 0, errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);

            }
            catch (Exception ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en EliminarEmpleado " + ex.ToString());
                errDesc = "Excepcion en EliminarEmpleado " + ex.ToString();
                errCode = (int)StatusCode.NOT_IMPLEMENTED;
            }
        }


        //public string ObtenerSiguienteNumeroTarjetaDisponible( out string errDesc, out int errCode)
        //{
        //    string returnData = "";
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPwsAlutelOGAPI + "/AlutelDataConduitAPI/DataConduitAPI?SiguienteNumeroTarjeta";

        //        string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);

        //        var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
        //        var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);
        //        returnData = respuesta.resultado;
        //    }
        //    catch (WebException ex)
        //    {
        //        errDesc = "Excepcion en WebServiceAPI.ObtenerSiguienteNumeroTarjetaDisponible: " + ex.ToString();
        //        Helpers.GetInstance().DoLog(errDesc);

        //        errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
        //        returnData = "";
        //    }

        //    return returnData;
        //}



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
                errCode = (int)StatusCode.OK;

            }
            catch (WebException ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en GETRequest " + ex.Message +" URL=" + v_url);
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
                            errDesc = reader.ReadToEnd();           // El mensaje de error de la respuesta
                        }
                        HTTPStatus = (int)errorResponse.StatusCode;
                    }
                }
                Helpers.GetInstance().DoLog("Excepcion en POSTRequest " + errDesc + " URL=" + v_url + " MESSAGE="  + wex.Message);
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

                Stream POSTstream = request.GetRequestStream();

                POSTstream.Write(dataByte, 0, dataByte.Length);

                //Obtengo la respuesta del webService
                HttpWebResponse UPDATEResponse = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(UPDATEResponse.GetResponseStream(), Encoding.UTF8);

                result = reader.ReadToEnd().ToString();
                errCode = (int)StatusCode.OK;
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en PUTRequest " + ex.Message + " URL=" + v_url;
                Helpers.GetInstance().DoLog(errDesc);
                result = "";
            }
            return result;
        }

        internal string DELETERequest(string v_url, string v_jsonToSend, string v_token, out int errCode, out string errDesc)
        {
            byte[] dataByte = null;
            dataByte = UTF8Encoding.UTF8.GetBytes(v_jsonToSend);
            errCode = (int)StatusCode.INTERNAL_ERROR;
            errDesc = "";
            string result = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(v_url);
                request.Method = "DELETE";
                request.ContentType = "application/json";
                request.ContentLength = dataByte.Length;
                request.Headers["Authorization"] = v_token;

                Stream POSTstream = request.GetRequestStream();

                POSTstream.Write(dataByte, 0, dataByte.Length);

                //Obtengo la respuesta del webService
                HttpWebResponse DELETEResponse = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(DELETEResponse.GetResponseStream(), Encoding.UTF8);

                result = reader.ReadToEnd().ToString();
                errCode = (int)StatusCode.OK;
            }
            catch (WebException ex)
            {
                Helpers.GetInstance().DoLog("Excepcion en DELRequest " + ex.Message + " URL=" + v_url);
                errDesc = "Excepcion en DELRequest " + ex.Message;
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
