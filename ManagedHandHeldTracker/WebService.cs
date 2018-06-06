using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ManagedHandHeldTracker
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
        //Ejemplo: http://192.168.0.103/AlutelOnGuardAPI/OnGuardAPI?ObtenerSiguienteNumeroTarjeta

        public KeyValuePair<Employee,Tarjeta> GetLinkedEmployee(int deviceID, int orgID, out string errDesc, out int errCode)
        {
            KeyValuePair<Employee, Tarjeta> returnData = new KeyValuePair<Employee, Tarjeta>(null,null);

            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/LinkedEmployee&deviceid=" + deviceID.ToString() + "&orgid=" + orgID.ToString();

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    var objetoAuxiliar = new { emp = new Employee(), badge = new Tarjeta()};

                    var respuesta = JsonConvert.DeserializeAnonymousType((string)gr.Result, objetoAuxiliar);

                    
                    returnData = new KeyValuePair<Employee, Tarjeta>(respuesta.emp, respuesta.badge);
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en WebServiceAPI.GetLinkedEmployee: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return returnData;
        }
        public string GetHHGPS(string v_orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/List&orgid=" + v_orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if ( errCode ==(int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    returnData = (string)gr.Result;
                 }
             }
            catch (WebException ex)
            {
                errDesc = "Excepcion en WebServiceAPI.GetHHGPS: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData= "";
            }
            return returnData;
        }

        public string GetProperties(string paneID, string v_orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/Properties&deviceid=" + paneID + "&orgid=" + v_orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetProperties: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        public string GetReadersFromVZone(string zoneID, string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Zone/Readers&deviceid=" + zoneID + "&orgid=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetReadersFromVZone: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        //public string GetHistTrackMap(out string errDesc, out int errCode)
        //{
        //    string returnData = "";
        //    errDesc = "";
        //    errCode = -1;
        //    try
        //    {
        //        string strURL = @"http://" + StaticCustomOptionsManager.IPwsAlutelMobilityAPI + "/AlutelOnGuardAPI/OnGuardAPI?getHistTrackMap";

        //        string res = GETRequest(strURL, StaticCustomOptionsManager.Token, out errCode, out errDesc);
        //        if (errCode == 0)
        //        {
        //            var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
        //            var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);
        //            returnData = respuesta.resultado;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        StaticCustomOptionsManager.DoLog("Excepcion en WebServiceAPI.GetHistTrackMap: " + ex.ToString());
        //        errDesc = "Excepcion en WebServiceAPI.GetHistTrackMap: " + ex.ToString();
        //        errCode = -1;            // -1: Excepcion

        //        returnData = "";
        //    }

        //    return returnData;
        //}

        public string GetDeviceConfig(string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/Config/List&orgid=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode ==(int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    returnData = (string)gr.Result;
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en GetDeviceConfig: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;

        }

        public PanelType GetDeviceType(string panelID, string orgID, out string errDesc, out int errCode)
        {
            PanelType returnData = PanelType.NODEFINIDO;
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/Type&deviceid=" + panelID + "&orgid=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    returnData = (PanelType)Enum.Parse(typeof(PanelType), (string)gr.Result);
                    //returnData = (string)gr.Result;
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en WebServiceAPI.GetDeviceType: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return returnData;
        }

        //public string GetParameters(out string errDesc, out int errCode)
        //{
        //    string returnData = "";
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPwsAlutelMobilityAPI + "/AlutelOnGuardAPI/OnGuardAPI?getParameters";

        //        string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == 0)
        //        {
        //            GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
        //            errDesc = gr.ErrorDescription;
        //            errCode = gr.ErrorCode;
        //            returnData = (string)gr.Result;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        errDesc = "Excepcion en WebServiceAPI.GetParameters: " + ex.ToString();
        //        Helpers.GetInstance().DoLog(errDesc);

        //        errCode = (int)StatusCode.INTERNAL_ERROR;
        //        returnData = "";
        //    }

        //    Helpers.GetInstance().DoLog("getParameters obtuvo: "  + returnData + " errCode: " + errCode.ToString());

        //    return returnData;
        //}

        public string GetOnlyZone(string panelID, string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
           
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Zone&deviceid=" + panelID + "&orgid=" + orgID;
                Tools.GetInstance().DoLog(strURL);

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetOnlyZone: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;


        }

        public string GetNewMessages(string panelID, string orgID, string lastReceived, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;

            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Message/ByDeviceID&deviceid=" + panelID + "&orgid=" + orgID + "&lastreceived=" + lastReceived;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetNewMessages: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        public string GetPrevMessages(string panelID, string orgID, string firstReceived, string cant, out string errDesc, out int errCode)
        {

            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;

            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Message/Previous&deviceid=" + panelID + "&orgid=" + orgID + "&lastreceived=" + firstReceived + "&quantity=" + cant;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetPrevMessages: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        public string GetEventData(string panelID, string serialNum, string conInternet, out string tipoEvento, out byte[] fotoEmp, out byte[] fotoEvento, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            tipoEvento = "";
            fotoEmp = null;
            fotoEvento = null;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?EventData&deviceid=" + panelID + "&serialnum=" + serialNum + "&hasinternet=" + conInternet;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;

                    var objetoAuxiliar = new { tipoEvento = "", eventData = "", datosFotoEmp = new byte[0], datosFotoEv = new byte[0]};

                    var respuesta = JsonConvert.DeserializeAnonymousType((string)gr.Result, objetoAuxiliar);

                    returnData = respuesta.eventData;
                    tipoEvento = respuesta.tipoEvento;
                    fotoEvento = respuesta.datosFotoEv;
                    fotoEmp = respuesta.datosFotoEmp;
                }
            }
            catch (WebException ex)
            {
                errDesc = "Excepcion en " + System.Reflection.MethodBase.GetCurrentMethod().Name +": " +  ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;

                returnData = "";
            }
            return returnData;
        }

        public string GetPosition(string panelID, string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;

            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Position&deviceid=" + panelID + "&orgid=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetPosition: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        public string GetLiveTrackingZoneInfo(string panelID, string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;

            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Zone/Info&deviceid=" + panelID + "&orgid=" + orgID;
             
                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetLiveTrackingZoneInfo: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        public string GetMultiplePositionsFromZone(string zoneID, string orgID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;

            try
            {
                //string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?LiveTracking/Position/AllDevices&deviceid=" + zoneID + "&orgid=" + orgID;

                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?LiveTracking/VirtualZone&deviceid=" + zoneID + "&orgid=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetMultiplePositionsFromZone: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        public string GetPuntosGPS(string orgID, string DeviceID, string startTime, string DeviceType, string cantPuntos, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;

            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/Position/List&deviceid=" + DeviceID + "&orgid=" + orgID + "&starttime=" + startTime + "&devicetype=" + DeviceType + "&quantity=" + cantPuntos;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetPuntosGPS: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }

            return returnData;
        }

        //public byte[] GetReporte(string tipoReporte, string panelID, string badge, string fechaIni, string fechaFin, string name, string lastName, string orgID, out string errDesc, out int errCode)
        //{
        //    byte[] returnData = new byte[0];
        //    errDesc = "";
        //    errCode = -1;

        //    try
        //    {
        //        string strURL = @"http://" + StaticCustomOptionsManager.IPwsAlutelMobilityAPI + "/AlutelOnGuardAPI/OnGuardAPI?getReporte&panelid=" + panelID + 
        //        "&badge=" + badge + "&fechaIni=" + fechaIni + "&fechaFin=" + fechaFin + "&name=" + name + 
        //        "&lastName=" + lastName + "&orgID=" + orgID + "&tipoReporte=" + tipoReporte;

        //        string res = GETRequest(strURL, StaticCustomOptionsManager.Token, out errCode, out errDesc);
        //        if (errCode == 0)
        //        {
        //            var objetoAuxiliar = new { resultado = new byte[0], errCode = 0, errDesc = "" };
        //            var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);
        //            returnData = respuesta.resultado;

        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        StaticCustomOptionsManager.DoLog("Excepcion en WebServiceAPI.GetReporte: " + ex.ToString());
        //        errDesc = "Excepcion en WebServiceAPI.GetReporte: " + ex.ToString();
        //        errCode = -1;            // -1: Excepcion
        //    }

        //    return returnData;
        //}
        //hhdata = "", gpsdata = "", orgid = ""

        /// <summary>
        /// Manda Device, maxSpeed, GPSUpdateTime,Device,
        /// </summary>
        public string UpdateDeviceConfig(string HHData,string orgID, out string errDesc, out int errCode)
        {

            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Devices/DeviceConfig&orgid=" + orgID;

                string jsonParaEnviar = JsonConvert.SerializeObject(
                new
                {
                    DeviceData = HHData
                });


                string res = PUTRequest(strURL, jsonParaEnviar, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.UpdateDeviceConfig: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
            }
            return returnData;

        }

        public string UpdateLinkedEmployee(string panelID, string orgID, string badge, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/LinkedEmployee&deviceid=" + panelID + "&orgid=" + orgID + "&badge=" + badge;

                string res = PUTRequest(strURL, "", Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.UpdateLinkedEmployee: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;

                returnData = "";
            }
            return returnData;
        }

        public int UpdateLogInStatus(string panelID, bool logIn, string orgId, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/LogInStatus&deviceid=" + panelID + "&orgid=" + orgId;

                string res = PUTRequest(strURL, "", Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.UpdateLogInStatus: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;

                returnData = "";
            }
            return errCode;
        }

        //public string UpdateIMEI(string panelID, string orgID, string IMEI, out string errDesc, out int errCode)
        //{
        //    string returnData = "";
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    try
        //    {
        //        string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/Imei&deviceid=" + panelID + "&orgid=" + orgID + "&imei=" + IMEI;

        //        string res = PUTRequest(strURL, "", Tools.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == (int)StatusCode.OK)
        //        {
        //            GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
        //            errDesc = gr.ErrorDescription;
        //            errCode = gr.ErrorCode;
        //            returnData = (string)gr.Result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errDesc = "Excepcion en WebServiceAPI.UpdateIMEI: " + ex.ToString();
        //        Tools.GetInstance().DoLog(errDesc);
        //        errCode = (int)StatusCode.INTERNAL_ERROR;

        //        returnData = "";
        //    }
        //    return returnData;
        //}

        public bool UpdateListaIMEI(List<string> listaIMEI, int orgID)
        {
            string returnData = "";
            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            bool devolucion = false;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?IMEI/List&orgid=" + orgID;

                string jsonParaEnviar = JsonConvert.SerializeObject(
                new
                {
                    IMEIData= listaIMEI
                });


                string res = PUTRequest(strURL, jsonParaEnviar, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    returnData = (string)gr.Result;
                    devolucion = true;
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.UpdateListaIMEI: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion
                returnData = "";
                devolucion = false;
            }

            return devolucion;
        }

        
        public string GetAccessLevelsLenel(string badge, string orgID,out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Badge/AccesslevelsLenel/List&badge=" + badge + "&orgid=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.GetAccessLevelsLenel: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;

                returnData = "";
            }
            return returnData;

        }


        public string SetMode(string strPanelID , string HHMode ,string strOrgID , out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            Tools.GetInstance().DoLog("SetProperties de: " + strPanelID + " MODE: " + HHMode);
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Device/Mode&deviceid=" + strPanelID + "&orgid=" + strOrgID + "&mode=" + HHMode;

                string res = PUTRequest(strURL, "", Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.SetProperties: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;

                returnData = "";
            }
            return returnData;
        }
        
        public string SendMSG(string strPanelID, string strOrgID, string textoMensaje , out string errDesc, out int errCode)
        {

            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Message&deviceid=" + strPanelID + "&orgid=" + strOrgID + "&text=" + textoMensaje;

                string res = POSTRequest(strURL, "", Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.SendMSG: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }
            return returnData;

        }

        //public string ExportHistTracking(string DeviceID, string DeviceType, string start, string end, out string errDesc, out int errCode)
        //{
        //    string returnData = "";
        //    errDesc = "";
        //    errCode = -1;
        //    try
        //    {
        //        string strURL = @"http://" + StaticCustomOptionsManager.IPwsAlutelMobilityAPI + "/AlutelOnGuardAPI/OnGuardAPI?exportHistTracking";

        //        string jsonParaEnviar = JsonConvert.SerializeObject(
        //        new
        //        {
        //            DeviceID = DeviceID,
        //            DeviceType = DeviceType,
        //            start = start,
        //            end = end
        //        });

        //        string res = POSTRequest(strURL, jsonParaEnviar, StaticCustomOptionsManager.Token, out errCode, out errDesc);
        //        if (errCode == 0)
        //        {
        //            var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
        //            var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);
        //            errDesc = respuesta.errDesc;
        //            errCode = respuesta.errCode;

        //            returnData = respuesta.resultado;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        StaticCustomOptionsManager.DoLog("Excepcion en WebServiceAPI.ExportHistTracking: " + ex.ToString());

        //        errDesc = "Excepcion en WebServiceAPI.ExportHistTracking: " + ex.ToString();
        //        errCode = -1;            // -1: Excepcion

        //        returnData = "";
        //    }
        //    return returnData;

        //}

        public string DefineGate(string LNL_PanelID, string LNL_EntranceReaderID, string LNL_ExitReaderID, string idOrganization, string accessType, string Punto1, string Punto2, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Gate&deviceid=" + LNL_PanelID + "&orgid=" + idOrganization + "&entrancereaderid=" + LNL_EntranceReaderID + "&exitreaderid="+LNL_ExitReaderID + "&accesstype=" + accessType + "&startpoint=" + Punto1 + "&endpoint=" + Punto2;
             
                string res = POSTRequest(strURL, "", Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.DefineGate: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }
            return returnData;
        }

        public string DefineZone(string LNL_PanelID, string triggerMode, string listaPuntos, string idOrganization , out string errDesc, out int errCode)
        {

            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?VirtualZone/Define&deviceid=" + LNL_PanelID +"&orgid=" + idOrganization + "&triggermode=" + triggerMode + "&geometricdata=" + listaPuntos;

                string res = POSTRequest(strURL, "", Tools.GetInstance().Token, out errCode, out errDesc);
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
                errDesc = "Excepcion en WebServiceAPI.DefineZone: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
                returnData = "";
            }
            return returnData;


        }

        //public Dictionary<int, empInfo> getListaEmpleadosABordo(string LNL_PanelID, string idOrganizacion, out string errDesc, out int errCode)
        //{
        //    Dictionary<int, empInfo> retValue = new Dictionary<int, empInfo>();

        //    string returnData = "";
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    try
        //    {

        //        string strURL = @"http://" + Helpers.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Employee/Load/List&deviceid=" + LNL_PanelID + "&orgid=" + idOrganizacion;

        //        string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if (errCode == (int)StatusCode.OK)
        //        {
        //            GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
        //            errDesc = gr.ErrorDescription;
        //            errCode = gr.ErrorCode;
        //            returnData = (string)gr.Result;
        //            retValue = JsonConvert.DeserializeObject<Dictionary<int, empInfo>>(returnData);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errDesc = "Excepcion en WebServiceAPI.DefineZone: " + ex.ToString();
        //        Helpers.GetInstance().DoLog(errDesc);

        //        errCode = (int)StatusCode.INTERNAL_ERROR;
        //        returnData = "";
        //    }
        //    return retValue;
        //}


        //public bool CheckRunning(out string errDesc, out int errCode)
        //{
        //    bool returnData = false;
        //    errDesc = "";
        //    errCode = (int)StatusCode.NOT_IMPLEMENTED;
        //    try
        //    {
        //        string strURL = @"http://" + Helpers.GetInstance().IPwsAlutelMobilityAPI + "/AlutelOnGuardAPI/OnGuardAPI?checkRunning";

        //        string res = GETRequest(strURL, Helpers.GetInstance().Token, out errCode, out errDesc);
        //        if ( errCode ==0)
        //        {
        //            GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
        //            errDesc = gr.ErrorDescription;
        //            errCode = gr.ErrorCode;
        //            returnData = (bool)gr.Result;
        //         }
        //     }
        //    catch (WebException ex)
        //    {
        //        errDesc = "Excepcion en WebServiceAPI.checkRunning: " + ex.ToString();
        //        Helpers.GetInstance().DoLog(errDesc);

        //        errCode = (int)StatusCode.INTERNAL_ERROR;
        //        returnData = false;
        //    }

        //    return returnData;
        //}

        public List<empInfo> ObtenerListaEmpleadosEnZona(string zoneName, ref string errDesc, ref int errCode)
        {
            List<empInfo> returnData = new List<empInfo>();

            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try  
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Person/List/OnSite&entrycontrollers=" + zoneName + "&exitcontrollers=" + zoneName + "&companylist=";

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    returnData = JsonConvert.DeserializeObject<List<empInfo>>(gr.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerListaEmpleadosEnZona: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;

            }
            return returnData;  
        }

        public int ObtenerCantidadEmpleadosEnZona(string zoneName)
        {
            int number = -1;

            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Person/Number/OnSite&entrycontrollers=" + zoneName + "&exitcontrollers=" + zoneName + "&companylist=";
                int errCode = -1;
                string errDesc = "";
                
                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                Tools.GetInstance().DoLog("URL=" + strURL);
                Tools.GetInstance().DoLog("res=" + res);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    number = int.Parse(gr.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en WebServiceAPI.ObtenerCantidadEmpleadosEnZona: " + ex.ToString());
            }
            return number;
        }

        public List<string> CargarListaIMEI(int orgID)
        {
            List<string> listaRes = new List<string>();

            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?IMEI/List&orgid=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    listaRes = JsonConvert.DeserializeObject<List<string>>(gr.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.CargarListaIMEI: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
                listaRes = null;

            }
            return listaRes;  
        }

        public List<empInfo> CargarListaDeviceUsers()
        {
            List<empInfo> listaRes = new List<empInfo>();

            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?DeviceUsers/List";

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    listaRes = JsonConvert.DeserializeObject<List<empInfo>>(gr.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.CargarListaDeviceUsers: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
                listaRes = null;

            }
            return listaRes;
        }

        public List<TarjetaMaster> ObtenerListaTarjetasMaster(int orgID)
        {
            List<TarjetaMaster> listaRes = new List<TarjetaMaster>();

            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?TarjetasMaster/List&OrgID=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    listaRes = JsonConvert.DeserializeObject<List<TarjetaMaster>>(gr.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerListaTarjetasMaster: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
                listaRes = null;

            }
            return listaRes;
        }

        public Dictionary<int, string> ObtenerListaVirtualZones(int orgID)
        {
            Dictionary<int, string> listaRes = new Dictionary<int, string>();

            string errDesc = "";
            int errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?VirtualZone/List&OrgID=" + orgID;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    listaRes = JsonConvert.DeserializeObject<Dictionary<int, string>>(gr.Result.ToString());
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerListaVirtualZones: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
                listaRes = null;

            }
            return listaRes;
        }

        public bool ActualizarTarjetasMaster(List<TarjetaMaster> ListaTarjetas, int OrgID)
        {
            bool returnData = false;
            int errCode = (int)StatusCode.INTERNAL_ERROR;
            string errDesc = "";

            string jsonParaEnviar = JsonConvert.SerializeObject(
            new
            {
                TarjetasMaster = ListaTarjetas
            });

            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?ActualizarTarjetasMaster&orgid=" + OrgID;
              
                string res = PUTRequest(strURL, jsonParaEnviar, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    GenericResponse gr = JsonConvert.DeserializeObject<GenericResponse>(res);
                    errDesc = gr.ErrorDescription;
                    errCode = gr.ErrorCode;
                    returnData = (bool)gr.Result;
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ActualizarTarjetasMaster: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;
                                
            }
            return returnData;
        }



        #endregion
        
        #region AlutelDataConduitAPI

        public Dictionary<int, Device> ObtenerListaPanels(ref string errDesc, ref int errCode)
        {
            errDesc = "";
            errCode = -1;
            Dictionary<int, Device> res = new Dictionary<int, Device>();
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ListaPanels";

                string data = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == 200)
                {
                    var objetoAuxiliar = new { resultado = new Dictionary<int, Device>(), errCode = 0, errDesc = "" };
                    var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                    return respuesta.resultado;
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en WebServiceAPI.ObtenerListaPanels " + ex.ToString());
            }

            return res;
        }

        public bool ExistePanelEnOnGuard(string deviceHHID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            bool res = true;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ExistePanelEnOnGuard&deviceHHID=" + deviceHHID;

                string data = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                    if (errCode == 0)
                    {
                        var objetoAuxiliar = new { resultado = false, errCode = 0, errDesc = "" };
                        var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                        return respuesta.resultado;
                    }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en WebServiceAPI.ExistePanelEnOnGuard " + ex.ToString());
            }

            return res;
        }

        public string ObtenerNombreCardformat(string cardfrmatID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            string res = "";
            try
            {

                string strURL = @"http://" + Tools.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerNombreCardFormat&cardFormatID=" + cardfrmatID;
                string data = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res= respuesta.resultado;
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en WebServiceAPI.ObtenerNombreCardfomat " + ex.ToString());
            }

            return res;

        }



        public string ObtenerNombrePanelID(int panelID, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            string res = "";
            try
            {
                // Nota: PANELTYPEID=234 es fijo y asignado por Lenel para Alutel.
                string strURL = @"http://" + Tools.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerPanelNamePorPanelID&lnlpanelid=" + panelID + "&paneltypeid=234";
                string data = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                var objetoAuxiliar = new { resultado = "", errCode = 0, errDesc = "" };
                var respuesta = JsonConvert.DeserializeAnonymousType(data, objetoAuxiliar);
                res = respuesta.resultado;
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en WebServiceAPI.obtenerNombrePanelID " + ex.ToString());
            }

            return res;
        }



        public KeyValuePair<Employee, Tarjeta> ObtenerDatosEmpleadoYTarjeta(string badge, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            KeyValuePair<Employee, Tarjeta> resultData = new KeyValuePair<Employee, Tarjeta>(null, null);

            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPDataConduitAPI + "/AlutelDataConduitAPI/DataConduitAPI?ObtenerDatosEmpleadoYTarjeta&badge=" + badge;

                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);
                if (errCode == (int)StatusCode.OK)
                {
                    var objetoAuxiliar = new { Empleado = new Employee(), Tarjeta = new Tarjeta(), errCode = 0, errDesc = "" };
                    var respuesta = JsonConvert.DeserializeAnonymousType(res, objetoAuxiliar);
                    respuesta.Tarjeta.OrgID = Tools.GetInstance().MainOrgID;

                    resultData = new KeyValuePair<Employee, Tarjeta>(respuesta.Empleado, respuesta.Tarjeta);
                }
            }
            catch (Exception ex)
            {
                errDesc = "Excepcion en WebServiceAPI.ObtenerDatosEmpleadoYTarjeta " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);

                errCode = (int)StatusCode.INTERNAL_ERROR;
            }

            return resultData;
        }

        /// <summary>
        /// Envia addEmployee usando AlutelMobilityAPI para que se de de alta en la base GPS
        /// </summary>
        public string AddEmployee(Employee emp, Tarjeta tarj, int v_panelID, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?Employee";

                string jsonParaEnviar = JsonConvert.SerializeObject(
                    new
                    {
                        deviceid = v_panelID,
                        employee = emp,
                        badge = tarj
                    });

                string res = POSTRequest(strURL, jsonParaEnviar, Tools.GetInstance().Token, out errCode, out errDesc);

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
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }


        /// <summary>
        /// Envia el nuevo Device User para que se de de alta en la base AlutelMobility
        /// </summary>
        public string AddDeviceUser(Employee emp, Tarjeta tarj, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?DeviceUser";

                string jsonParaEnviar = JsonConvert.SerializeObject(
                    new
                    {
                        employee = emp,
                        badge = tarj
                    });

                string res = POSTRequest(strURL, jsonParaEnviar, Tools.GetInstance().Token, out errCode, out errDesc);

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
                errDesc = "Excepcion en WebServiceAPI.AddDeviceUser: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }


        /// <summary>
        /// Borra Device 
        /// </summary>
        public string DeleteDeviceUser(string tarjeta, out string errDesc, out int errCode)
        {
            string returnData = "";
            errDesc = "";
            errCode = (int)StatusCode.NOT_IMPLEMENTED;
            try
            {
                string strURL = @"http://" + Tools.GetInstance().IPOnGuardAPI + "/AlutelOnGuardAPI/OnGuardAPI?DeviceUser&badge=" + tarjeta;
               
                string res = DELETERequest(strURL, "", Tools.GetInstance().Token, out errCode, out errDesc);

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
                errDesc = "Excepcion en WebServiceAPI.DeleteDeviceUser: " + ex.ToString();
                Tools.GetInstance().DoLog(errDesc);
                errCode = (int)StatusCode.INTERNAL_ERROR;            // -1: Excepcion

                returnData = "";
            }

            return returnData;
        }




        #endregion


        #region wsMapQuest
        /// <summary>
        /// Obtiene Latitud y longitud a partir de la descripcion de un lugar en el mundo.
        /// Usa MapQUest API con la key de uso infinito asociado a javierfil@gmail.com
        /// </summary>
        /// <param name="posdesc"></param>
        /// <param name="latitud"></param>
        /// <param name="longitud"></param>
        /// <param name="errDesc"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public void obtenerPosicion(string posdesc, out string latitud, out string longitud, out string errDesc, out int errCode)
        {
            errDesc = "";
            errCode = -1;
            latitud = "";
            longitud = "";
            try
            {
                string strURL = @"http://www.mapquestapi.com/geocoding/v1/address?key=unBby27lcjB3VN6rAroYlnvsDEjU9PVF&location=" + @posdesc + @"&callback=renderGeocode";
                string res = GETRequest(strURL, Tools.GetInstance().Token, out errCode, out errDesc);

                res = res.Replace("renderGeocode", "");
                res = res.TrimEnd(')').TrimStart('(');

                var obj = JObject.Parse(@res);

                latitud = (string)obj["results"][0]["locations"][0]["latLng"]["lat"];
                latitud = latitud.Replace(',', '.');

                longitud = (string)obj["results"][0]["locations"][0]["latLng"]["lng"];
                longitud = longitud.Replace(',', '.');
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en WebServiceAPI.obtenerPosicion " + ex.ToString());
            }

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
                errCode = (int)StatusCode.OK;

            }
            catch (WebException ex)
            {
                Tools.GetInstance().DoLog("Excepcion en GETRequest " + ex.ToString());
                errDesc = "Excepcion en GETRequest " + ex.ToString();
                res = "";
            }

            return res;

        }

        internal string POSTRequest(string v_url, string v_jsonToSend, string v_token, out int errCode, out string errDesc)
        {
            byte[] dataByte = null;
            dataByte = UTF8Encoding.UTF8.GetBytes(v_jsonToSend);
            errCode = (int)StatusCode.INTERNAL_ERROR;
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
            }
            catch (WebException ex)
            {
                Tools.GetInstance().DoLog("Excepcion en POSTRequest " + ex.ToString());
                errDesc = "Excepcion en POSTRequest " + ex.ToString();
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
                Tools.GetInstance().DoLog("Excepcion en UPDATERequest " + ex.ToString());
                errDesc = "Excepcion en UPDATERequest " + ex.ToString();
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
                Tools.GetInstance().DoLog("Excepcion en DELRequest " + ex.ToString());
                errDesc = "Excepcion en DELRequest " + ex.ToString();
                result = "";
            }
            return result;
        }

        #endregion
    }

    #region Clases para Serializar datos
    public class empInfo
    {
        public string idEmpleado { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Badge { get; set; }
        public DateTime LastAccess { get; set; }
    }
    public class empInfoComparer : IComparer<empInfo>
    {
        public int Compare(empInfo x, empInfo y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
                return (x.Name + " " + x.Lastname).CompareTo(y.Name + " " + y.Lastname);

        }
    }

   
    public class GenericResponse
    {
        public object Result { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
     #endregion
}
