using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Drawing;

namespace ManagedHandHeldTracker
{
    public enum StatusCode
    {
        //SUCCESS
        OK = 200,

        //CLIENT ERROR
        UNAUTHORIZED = 401,
        NOT_FOUND = 404,

        //SERVER ERROR
        INTERNAL_ERROR = 500,
        NOT_IMPLEMENTED = 501,
    }
    public enum TiposAcceso             // Es el tipo de evento que viene desde el HH
    {
        NoDefinido,
        Entrada,
        Salida,
        EINVALIDO,
        SINVALIDO,
        INVALIDO,
        Alarma,
        Diferido,
        Bloqueo,                         // Viene desde el HH cuando esta en modo BLOQUEO
    }
    public enum PanelType        // Tipos de dispositivos creados desde LENEL
    {
        NODEFINIDO,
        DEVICE,
        VIRTUALZONE,
        GPS
    }


    public class EmpInfoComparer : IComparer<empInfo>
    {
        public int Compare(empInfo a, empInfo b)
        {
            if ((a.Badge == b.Badge))
                return 0;
            if ((int.Parse(a.Badge) < int.Parse(b.Badge)))
                return -1;

            return 1;
        }
    }


    class Tools
    {
        public static string OnGuardPath = "";                  // Path de instalacion de OnGuard.
        string LogPath = "";                                    // Path de Log de Lenel: ProgramData/lnl/Logs o OnGuard/Logs

        public int MainOrgID { get; set; }                      // OrgID para enviar en las llamadas del translator.
        public string IPOnGuardAPI { get; set; }       // IP del web service OnGuardAPI
        public string IPDataConduitAPI { get; set; }             // IP del web service DataConduitAPI
        public string Token { get; set; }                       // Token del usuario que utiliza el WebService

        public  string NONESTRING = "None";                     // Para los comboboxes de la definicion de zonas.
        public  string ZoneName = "";                           // Nombre de la zona

        public  bool hasHandHeldTranslator = false;             // Para indicar si esta instalado el ALUTELHandHeldTranslator
        public  bool hasVZoneTranslator = false;                // Para indicar si esta instalado el ALUTELVirtualGateTranslator

        #region Singleton
        static Tools _instance;

        // Parametros de las Bubbles de los markers:
        private const int BUBBLE_WIDTH = 350;
        private const int BUBBLE_HEIGHT = 150;

        public static Tools GetInstance()
        {
            if (_instance == null) _instance = new Tools();
            return _instance;
        }

        Tools()
        {
            bool warningLog = false;            // Para que cuando tenga el path del log loguee que no encotro la CLSID
            try
            {

                OnGuardPath = BuscarInstallationPath();

                if (string.IsNullOrEmpty(OnGuardPath))
                {
                    warningLog = true;          // Para que cuando tenga el path del log loguee que no encotro la CLSID
                    OnGuardPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "OnGuard");
                }

                if (Directory.Exists(OnGuardPath + @"\\logs"))
                    LogPath = OnGuardPath + @"\\logs";
                else
                    if (Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Lnl\Logs")))
                        LogPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Lnl\Logs");
                    else
                        LogPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),"");

                if (warningLog)
                    DoLog("ATENCION: OnGuardPath dio null en la busqueda por CLSID");

                DoLog("OnGuardpath=" + OnGuardPath + " LogPath=" + LogPath);


                updateConfigurationVariables();

                DoLog("LEVANTANDO EL CUSTOM TRANSLATOR");
            }
            catch (Exception ex)
            {
                IPDataConduitAPI ="";
                IPOnGuardAPI ="";
                Token="";
                MainOrgID=0;
                DoLog("Excepcion al cargar AlutelConfig.xml. Los parametros de conexion no han sido cargados. " + ex.Message);
            }
        }
        ~Tools()
        {
           
        }

        #endregion

        /// <summary>
        /// Usa la CLSID de AlutelHandHeldTranslator.dll para buscar en el registro el path donde esta instalada.
        /// </summary>
        private string BuscarInstallationPath()
        {
            string res = "";
            try
            {
                RegistryKey key = Registry.LocalMachine;

                RegistryKey clases = key.OpenSubKey(@"SOFTWARE\WOW6432Node\Classes\CLSID\{1E76211B-BE63-4B0C-BA75-A85B44AC2DD0}\InprocServer32", false);
                res = Path.GetDirectoryName((string)clases.GetValue(""));

            }
            catch (Exception)   // NO logueo porque no tengo el path para loguear aun.
            {
                res = "";
            }
            return res;
        }



        // Traduce los tiposAccesos al ingles
        public  string translateToEnglish(string text)
        {
            string res = "";
            try
            {
                TiposAcceso t = (TiposAcceso)Enum.Parse(typeof(TiposAcceso), text);
                if (t == TiposAcceso.Entrada) res = "Entry";
                if (t == TiposAcceso.Salida) res = "Exit";
                if (t == TiposAcceso.EINVALIDO) res = "Invalid Entry";
                if (t == TiposAcceso.SINVALIDO) res = "Invalid Exit";
                if (t == TiposAcceso.INVALIDO) res = "Invalid";
                if (t == TiposAcceso.Alarma) res = "Alarm";
                if (t == TiposAcceso.Diferido) res = "Deferred";
            }
            catch (Exception)
            {
                res = text;
            }

            return res;
        }


        /// <summary>
        /// Usada para saber si pedir el mapa o la foto
        /// </summary>
        /// <returns></returns>
        public  bool hasInternetConnection()
        {
            bool res = false;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://www.google.com");
                myReq.Timeout = 10 * 1000;
                WebResponse wr = myReq.GetResponse();
                wr.Close();
                res = true;
            }
            catch {  }     // NO HAY INTERNET
            
            return res;
        }

        /// <summary>
        /// Actualiza las variables generales de configuracion: IP, Port, etc. en funcion de la
        /// info registrada.
        /// </summary>
        public  void updateConfigurationVariables()
        {
            DoLog("Entro a updateConfigurationVariables");

            try
            {
                XmlDocument xDoc = new XmlDocument();
                string configFile = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\\OnGuard\\AlutelConfig.xml";

                xDoc.Load(configFile);

                foreach (XmlElement elem in xDoc.SelectNodes(@"/ConfigParameters/ConfigParameter"))
                {
                    string parameterID = elem.Attributes["id"].Value;

                    switch (parameterID)
                    {
                        case "IPwsAlutelOGAPI":
                            IPDataConduitAPI = elem.Attributes["value"].Value;
                            break;
                        case "IPwsAlutelMobilityAPI":
                            IPOnGuardAPI = elem.Attributes["value"].Value;
                            break;
                        case "Token":
                            Token = elem.Attributes["value"].Value;
                            break;
                        case "IDOrganizacion":
                            MainOrgID = int.Parse(elem.Attributes["value"].Value);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DoLog("ATENCION: ERROR al cargar parametros de configuracion:" + ex.Message);
                IPDataConduitAPI="";
                IPOnGuardAPI="";
                Token="";
                MainOrgID=0;

            }
            DoLog("IPDataConduitAPI: " + IPDataConduitAPI + " IPOnGuardAPI: " + IPOnGuardAPI +" IDOrganizacion:" + MainOrgID);

            hasVZoneTranslator = true;
            hasHandHeldTranslator = true;
        }

        public  Dictionary<int, KeyValuePair<Zone.GateAccessType, string>> cargarReadersList(string DeviceID, string orgID)
        {

            Dictionary<int, KeyValuePair<Zone.GateAccessType, string>> listaReaders = new Dictionary<int, KeyValuePair<Zone.GateAccessType, string>>();
           
            try
            {
                int errCode = -1;
                string errDesc = "";

                string datosReaders = WebServiceAPI.GetInstance().GetReadersFromVZone(DeviceID, orgID, out errDesc, out errCode);

                Regex regexDatos = new Regex(@"READERLIST:(.*)");
                Match matchHeader = regexDatos.Match(datosReaders);

                if (matchHeader.Success)
                {
                    string[] dReaders = getMatchData(matchHeader, 1).Split(',');

                    for (int i = 0; i < dReaders.Length; i = i + 3)
                    {
                        int readerID = int.Parse(dReaders[i]);
                        string readername = dReaders[i + 1];
                        string tipoGateStr = dReaders[i + 2];

                        Zone.GateAccessType GateType = (Zone.GateAccessType)Enum.Parse(typeof(Zone.GateAccessType), tipoGateStr);

                        if (!listaReaders.ContainsKey(readerID))
                        {
                            listaReaders.Add(readerID, new KeyValuePair<Zone.GateAccessType, string>(GateType, readername));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en cargarReadersList: " + ex.Message);
            }
           
            return listaReaders;
        }

        public  bool enviarHHConfig(string HHData)
        {
            bool res = false;
            try
            {
                int errCode = -1;
                string errDesc = "";
                WebServiceAPI.GetInstance().UpdateDeviceConfig(HHData,Tools.GetInstance().MainOrgID.ToString(), out errDesc, out errCode);

                res = (errCode == (int)StatusCode.OK);

            }
            catch (Exception ex)
            {
                DoLog("EXCEPCION en enviarHHConfig(): " + ex.Message);
                res = false;
            }
            return res;
        }

        /// <summary>
        /// Devuelve una lista deDeviceName, maxSpeed, GPSUpdateTime
        /// </summary>
        public string cargarDeviceConfig(string v_OrgID)
        {
            string res = "";

            try
            {
                string errDesc ="";
                int errCode = -1;
                res = WebServiceAPI.GetInstance().GetDeviceConfig(Tools.GetInstance().MainOrgID.ToString(), out errDesc, out errCode);
                if (errCode != (int)StatusCode.OK)
                {
                    DoLog("ERROR al cargar la configuracion de los devices: " + errDesc);
                }
            }
            catch (Exception ex)
            {
                DoLog("EXCEPCION en cargarDeviceConfig(): " + ex.Message);
                res = "";
            }
            return res;
        }

        /// <summary>
        /// Devuelve una lista de HH,HH,HH;movilID,GPSName,movilID,GPSName
        /// o sea: los IDs de los HH(que son iguales a sus nombres) y los movilID,GPSName de los GPSs
        /// </summary>
        /// <param name="v_OrgID"></param>
        /// <returns></returns>
        public  string cargarHHGPS(string v_OrgID)
        {
            string res = "";

            try
            {
                int errCode = -1;
                string errDesc = "";
                string datosGPS = WebServiceAPI.GetInstance().GetHHGPS(v_OrgID, out errDesc, out errCode);

                Regex datosDevices = new Regex(@"HH:(.*);GPS:(.*)");

                Match matchHeader = datosDevices.Match(datosGPS);
                if (matchHeader.Success)
                {
                    string HH = getMatchData(matchHeader, 1);
                    string GPS = getMatchData(matchHeader, 2);
                    res = HH + ";" + GPS;
                }
                
            }
            catch (Exception ex)
            {
                DoLog("EXCEPCION en cargarHHGPS(): " + ex.Message);
                res = "";
            }

            return res;
        }
        public Image ByteToImage(byte[] imageBytes, int cantbytes)
        {
            // Convert byte[] to Image
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes, 0, cantbytes);
            ms.Write(imageBytes, 0, cantbytes);
            Image image = new Bitmap(ms);
            return image;
        }

        public  void cargarPropiedadesDevice(string v_DeviceID, string v_OrgID, ref  string v_deviceName, ref string v_deviceType,  ref string v_mode,
                                             ref string v_speedLimit, ref string v_gpsUpdateTime)
        {
                try
                {
                    Regex datosProperties = new Regex(@"NAME:(.*),TYPE:(.*),UPDATETIME:(.*),SPEEDLIMIT:(.*),MODE:(.*),DEVICES:(.*)");

                    int errCode = -1;
                    string errDesc = "";
                    string datosProp = WebServiceAPI.GetInstance().GetProperties(v_DeviceID, v_OrgID, out errDesc, out errCode);


                    //Tools.GetInstance().DoLog("datosProp: " + datosProp);

                    Match matchHeader = datosProperties.Match(datosProp);
                    if (matchHeader.Success)
                    {
                        v_deviceName = getMatchData(matchHeader, 1);
                        v_deviceType = getMatchData(matchHeader, 2);
                        v_gpsUpdateTime = getMatchData(matchHeader, 3);
                        v_speedLimit = getMatchData(matchHeader, 4);         
                        v_mode = getMatchData(matchHeader, 5);
                        //v_listaDevices = getMatchData(matchHeader, 6);            // No se usa mas.-
                        //v_IMEI = getMatchData(matchHeader, 7);                    // No se usa mas.-
                    }
                } 
                catch (Exception ex)
                {
                    
                    Tools.GetInstance().DoLog("EXCEPCION en cargarPropiedadesDevice(): " + ex.Message);
                }
        }

        public  PanelType getDeviceType(int v_devid)
        {
            PanelType res = PanelType.NODEFINIDO;
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
          
            try
                {
                    string errDesc = "";
                    int errCode = -1;

                    res = WebServiceAPI.GetInstance().GetDeviceType(v_devid.ToString(), Tools.GetInstance().MainOrgID.ToString(), out errDesc, out errCode);
                    if (errCode != (int)StatusCode.OK)
                    {
                        Tools.GetInstance().DoLog("Error en GetDeviceType(): " + errDesc);

                    }
                }
                catch (Exception ex)
                {
                    Tools.GetInstance().DoLog("Excepcion en GetDeviceType: " + ex.Message);
                }
            

            return res;
        }

        public  void cargarMapaYDatosZona( string DeviceID, string OrgID, ref string v_datosMapa, ref string v_datosZonas, ref int triggerMode, int browserVersion)
        {

            v_datosMapa = Tools.GetInstance().cargarMapaDesdeResources(browserVersion);

            Tools.GetInstance().cargarDatosUnaZona(ref Tools.GetInstance().ZoneName, ref v_datosZonas, ref triggerMode, DeviceID, int.Parse(OrgID));

        }

        /// <summary>
        /// Carga la Zona y actualiza las variables globales ZonaName y triggerMode, pasadas por parametro.
        /// espera la info y muestra la zona en el mapa.
        /// </summary>
        /// <param name="v_PanelID"></param>
        public  void cargarDatosUnaZona(ref string zName, ref string zonaDef, ref int triggerMode, string v_PanelID, int orgID)
        {

            try
            {
                int errCode = -1;
                string errDesc  = "";
                DoLog("Va a cargar la zona " + zName);
                zonaDef = WebServiceAPI.GetInstance().GetOnlyZone(v_PanelID, orgID.ToString(), out errDesc, out errCode);

                DoLog("Zona Cargada. Datos: " + zonaDef);

                triggerMode = int.Parse(zonaDef.Substring(0, 1));               // El primer dato es el TriggerMode.

                // Le saco el triggerMode a los datos geometricos de la zona
                if (zonaDef.Length >= 2)
                    zonaDef = zonaDef.Substring(2, zonaDef.Length - 2);
                else
                    zonaDef = "";
            }
            catch (Exception ex)
            {
                DoLog("EXCEPCION en cargarDatosUnaZona(): " + ex.Message);
            }
        }

        /// <summary>
        /// Envia el mensaje LNL_EXPORTHTRACKING,DEVICEID:(.*),DEVICETYPE:(.*),START:(.*),END:(.*).
        /// Espera la respuesta del server con el nombre del archivo .db que el server va a generar.
        /// </summary>
        public  string enviarGenerarReporte(string v_device, string v_deviceType, DateTime v_startDateTime, DateTime v_endDateTime)
        {
            string nomFile ="";
            //try
            //{
            //    string startDateTime = v_startDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            //    string endDateTime = v_endDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //    string errDesc = "";
            //    int errCode = -1;
            //    nomFile = WebServiceAPI.GetInstance().ExportHistTracking(v_device, v_deviceType, startDateTime, endDateTime, out errDesc, out errCode);
            //    DoLog("Archivo de historical Tracking: " + nomFile);

            //}
            //catch (Exception ex)
            //{
            //    DoLog("EXCEPCION en enviarGenerarReporte(): " + ex.Message);
            //}
            return nomFile;
        }


        /// <summary>
        /// Carga el mapa desde Resources y construye el HTML final sustituyendo la latitud, longitud, zoom y la infoBubble
        /// </summary>
        /// <param name="v_lat"></param>
        /// <param name="v_long"></param>
        /// <param name="v_zoom"></param>
        /// <param name="v_browserVersion"></param>
        /// <returns></returns>
        //public  string construirMapa(string v_lat, string v_long, string v_zoom,string v_infoBubble, int v_browserVersion)
        public string construirMapa(string v_lat, string v_long, string v_zoom, int v_browserVersion)
        {
           
            string HTMLMapa = "";
            try
            {

                string mapstractionCode = "";
                string encodedIcon = "";

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "";
                resourceName = "ManagedHandHeldTracker.MapLENELV3.html";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    HTMLMapa = reader.ReadToEnd();
                }

                resourceName = "ManagedHandHeldTracker.mapstractionCode.js";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    mapstractionCode = reader.ReadToEnd();

                }

                // Mas directo en vez que desde recursos
                encodedIcon = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAmCAMAAADp2asXAAAACXBIWXMAAATAAAAEwAHDMVORAAAABGdBTUEAALGOfPtRkwAAACBjSFJNAAB6JQAAgIMAAPn/AACA6QAAdTAAAOpgAAA6mAAAF2+SX8VGAAACi1BMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgQAAAAAAAAAAgMAAgQAAAAAAgUAAwYABAcAAgUAAAEABQkABAgABQoABAgAAwYACREAAAAABQsACREAChUADhoACBEADBgACxUACxcADBgADBkADRkADRkAEiMADx4AFiwAECIAFCgAFCgAGzUAFiwAFiwAFy0AGjUAHjsAECAAGjQAGjQAHz8AJEcAJEgAJ00AGzcAJ04AK1YAJUoAM2UAAAAAAQEAAgMAAgQAAwYABw8ACRMADhsAECEAFCgAFy8AGTIAGjQAHDcAHToAHjwALVoAL10AMF8AMGAAMWAAMWIAMWMAMmIANWkANm4AOG0AOXEAO3QAO3UAPHcAPnoAPn0AQH8ARIcAR48ASZEATJgATZkATZsAU6MAVKQAV6wAWKwAW7IAXLQAXbcAX7oAYL4AYMEAYcAAYcIAYsIAY8QAY8YAZMQAZccAZcsAZsgAZ8wAZ84AZ9AAadAAadQAatEAatUAa9MAa9YAb9oAb9wAcNwAcuAAcuEAdOgAdeYAdekAduoAd+oAd+8AeOwAePAAefAAefEAefIAevEAevMAevQAe/cAfPEAfPQAfPcAfPgAfPkAfPoAffUAffgAffsAfvkAfvsAfvwAfv0Af/gAf/8AgP8Agf4Agf8Agv4Agv8Ag/8AhP8Ahf8Ahv8Ah/+QcqiGAAAAaHRSTlMAAgQICQ0OFBkiIygqLTAyMzQ3PD5DRElKTE1TVVZZX2FpbG1yc3R1eX1/gomMjZKWoKCkqKqrra6vsbK1t7m5vL7AxMXKy83P0NHY2dra29vd4OTl5+fr7e3t7fD09fb4+Pj5+vr9/UOHdzUAAAHgSURBVBgZbcGFW1NRHAbg33BiF3YjFjZ2d3d397W7QEHF7u7Z3d2Kith+x+Mp/hzPvdszx7b3JQoq2bDTsAnjh3SsV4IilW478+Cdd7mf39zYP6NdGQpLmXZa5EtuyXxxdnZ9Cmmy/q1hCGHmS2Zz8tTN/CsRQcotqWQlTf0g4WJSCbjE7+nliajzGQOX+n7r0kvNYOkLPX2UNFcxWOrU6kULlu39wwAwnV6JGh82sOT1pY5rn4aljzWj7vcEACYzHM/y5xyAeNSLRr7nAHjOCsez8LwCwL+Ooom5HADPW+V4Fl+TANivyTQ2h8PSux3P2h8MAPs2jvo+5bD4q3WOteScgsVf96e0kxou+WzXmpUZVxVc6nIrqrrVwCP0zzwl4TE7apF/4AOJIM4RJB8OTiRK3qwRxexMIaKEHhc1ClB3+/nJqjBHMERgelNN8qQdMIhgjrSnoKJD70uEiRdjilNI9Q2KIYTp7DoU1vKQQYg53sVHYcVGPxbw8I+TSlGE5GwDj9mTSpF83QIalro9oDAVUHGeYAB0Vg2K0vqEBtSVrhSt7CzBmEqvTDE6BJS82SeBYlTbqMz22hTLP+jJpxGJFEejo4EWFE+5+duqUDyFhk8pQnG16U3xNWhK//0DkuV2gVDgdmsAAAAASUVORK5CYII=";

                //resourceName = "ManagedHandHeldTracker.encodedIcon.txt";
                //using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                //using (StreamReader reader = new StreamReader(stream))
                //{
                //    encodedIcon = reader.ReadToEnd();
                //}

                HTMLMapa = HTMLMapa.Replace("[MAPSTRACTIONCODE]", mapstractionCode);

                if (v_browserVersion < 9) // Si el Iexplorer es anterior a la version 9 NO soporta inline images.
                    HTMLMapa = HTMLMapa.Replace("[ENCODEDICON]", @"https://dl.dropboxusercontent.com/u/79785316/iconoMapStraction.png");
                else
                    HTMLMapa = HTMLMapa.Replace("[ENCODEDICON]", encodedIcon);


                // Paso final: LAT,LONG,ZOOM en Pos ALUTEL
                HTMLMapa = HTMLMapa.Replace("[ZOOM]", v_zoom);
                HTMLMapa = HTMLMapa.Replace("[LAT]", v_lat);
                HTMLMapa = HTMLMapa.Replace("[LONG]", v_long);
                //HTMLMapa = HTMLMapa.Replace("[INFOBUBBLE]", v_infoBubble);

            }
            catch (Exception ex)
            {
                DoLog("EXCEPCION en construirMapa(): " + ex.Message);
            }

            return HTMLMapa;
        }

        public string cargarArchivoDesdeResources(string resourceName)
        {
            string res = "";
            try
            {

                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    res = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                DoLog("EXCEPCION en cargarArchivoDesdeResources(): " + ex.Message);
            }

            return res;
        }

        public  string  cargarMapaDesdeResources(int v_browserVersion)
        {
            string HTMLMapa = "";

            string mapstractionCode = "";
            string encodedIcon = "";

            var resourceName = "";
            resourceName = "ManagedHandHeldTracker.MapZoneDefV3.html";

            var assembly = Assembly.GetExecutingAssembly();


            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                HTMLMapa = reader.ReadToEnd();
            }

            resourceName = "ManagedHandHeldTracker.mapstractionCode.js";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                mapstractionCode = reader.ReadToEnd();
            }
            // Mas directo en vez que desde recursos
            //encodedIcon = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAmCAMAAADp2asXAAAACXBIWXMAAATAAAAEwAHDMVORAAAABGdBTUEAALGOfPtRkwAAACBjSFJNAAB6JQAAgIMAAPn/AACA6QAAdTAAAOpgAAA6mAAAF2+SX8VGAAACi1BMVEUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgQAAAAAAAAAAgMAAgQAAAAAAgUAAwYABAcAAgUAAAEABQkABAgABQoABAgAAwYACREAAAAABQsACREAChUADhoACBEADBgACxUACxcADBgADBkADRkADRkAEiMADx4AFiwAECIAFCgAFCgAGzUAFiwAFiwAFy0AGjUAHjsAECAAGjQAGjQAHz8AJEcAJEgAJ00AGzcAJ04AK1YAJUoAM2UAAAAAAQEAAgMAAgQAAwYABw8ACRMADhsAECEAFCgAFy8AGTIAGjQAHDcAHToAHjwALVoAL10AMF8AMGAAMWAAMWIAMWMAMmIANWkANm4AOG0AOXEAO3QAO3UAPHcAPnoAPn0AQH8ARIcAR48ASZEATJgATZkATZsAU6MAVKQAV6wAWKwAW7IAXLQAXbcAX7oAYL4AYMEAYcAAYcIAYsIAY8QAY8YAZMQAZccAZcsAZsgAZ8wAZ84AZ9AAadAAadQAatEAatUAa9MAa9YAb9oAb9wAcNwAcuAAcuEAdOgAdeYAdekAduoAd+oAd+8AeOwAePAAefAAefEAefIAevEAevMAevQAe/cAfPEAfPQAfPcAfPgAfPkAfPoAffUAffgAffsAfvkAfvsAfvwAfv0Af/gAf/8AgP8Agf4Agf8Agv4Agv8Ag/8AhP8Ahf8Ahv8Ah/+QcqiGAAAAaHRSTlMAAgQICQ0OFBkiIygqLTAyMzQ3PD5DRElKTE1TVVZZX2FpbG1yc3R1eX1/gomMjZKWoKCkqKqrra6vsbK1t7m5vL7AxMXKy83P0NHY2dra29vd4OTl5+fr7e3t7fD09fb4+Pj5+vr9/UOHdzUAAAHgSURBVBgZbcGFW1NRHAbg33BiF3YjFjZ2d3d397W7QEHF7u7Z3d2Kith+x+Mp/hzPvdszx7b3JQoq2bDTsAnjh3SsV4IilW478+Cdd7mf39zYP6NdGQpLmXZa5EtuyXxxdnZ9Cmmy/q1hCGHmS2Zz8tTN/CsRQcotqWQlTf0g4WJSCbjE7+nliajzGQOX+n7r0kvNYOkLPX2UNFcxWOrU6kULlu39wwAwnV6JGh82sOT1pY5rn4aljzWj7vcEACYzHM/y5xyAeNSLRr7nAHjOCsez8LwCwL+Ooom5HADPW+V4Fl+TANivyTQ2h8PSux3P2h8MAPs2jvo+5bD4q3WOteScgsVf96e0kxou+WzXmpUZVxVc6nIrqrrVwCP0zzwl4TE7apF/4AOJIM4RJB8OTiRK3qwRxexMIaKEHhc1ClB3+/nJqjBHMERgelNN8qQdMIhgjrSnoKJD70uEiRdjilNI9Q2KIYTp7DoU1vKQQYg53sVHYcVGPxbw8I+TSlGE5GwDj9mTSpF83QIalro9oDAVUHGeYAB0Vg2K0vqEBtSVrhSt7CzBmEqvTDE6BJS82SeBYlTbqMz22hTLP+jJpxGJFEejo4EWFE+5+duqUDyFhk8pQnG16U3xNWhK//0DkuV2gVDgdmsAAAAASUVORK5CYII=";

            resourceName = "ManagedHandHeldTracker.encodedIcon.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                encodedIcon = reader.ReadToEnd();
            }


            HTMLMapa = HTMLMapa.Replace("[MAPSTRACTIONCODE]", mapstractionCode);

            if (v_browserVersion < 9)       // Si no es por lo menos IE9 va a buscar el icono a dropbox
                HTMLMapa = HTMLMapa.Replace("[ENCODEDICON]", @"https://dl.dropboxusercontent.com/u/79785316/iconoMapStraction.png");
            else
                HTMLMapa = HTMLMapa.Replace("[ENCODEDICON]", encodedIcon);

            return HTMLMapa;
        }

        public  string getMatchData(Match resultMatch, int index)
        {
            return resultMatch.Groups[index].Value;
        }

        /// <summary>
        /// Convierte una coordenada en decimal a su equivalente sexagesimal: GradosMinutosSegundos
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public  string convertToSexagesimal(double coord)
        {
            string res;

            int degCoord = (int)Math.Truncate(Math.Abs(coord));

            double decimalpart = Math.Abs(coord - (int)coord);

            double minCoordD = 60.0f * decimalpart;

            int minCoord = (int)Math.Truncate(minCoordD);

            decimalpart = Math.Abs(minCoordD - (int)minCoordD);

            double segCoord = Math.Round(60.0f * decimalpart, 2);

            res = degCoord.ToString() + "°" + minCoord + "'" + segCoord + "\"";
            return res;
        }
        // ATENCION: Si EL Alarm Monitoring NO se ejecuta en MODO ADMINISTRADOR 
        // este LOG puede fallar por no tiene permisos para escribir en el carpeta logs de OnGuard.
        public  void DoLog(string v_stringToLoG)
        {
            try
            {
                string LogFile = LogPath + @"\\CustomOptionsLOG.log";

                StreamWriter w = File.AppendText(LogFile);
                w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + v_stringToLoG);
                w.Close();
            }
            catch (Exception)
            {
            }
        }
      



    }
}
