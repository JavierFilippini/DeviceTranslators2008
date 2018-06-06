using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Management;
using System.Text.RegularExpressions;

namespace ManagedAccessControlTranslator
{
    public enum StatusCode
    {
        //SUCCESS
        OK = 200,
        CREATED=201,
        //CLIENT ERROR
        UNAUTHORIZED = 401,
        NOT_FOUND = 404,
        //SERVER ERROR
        INTERNAL_ERROR = 500,
        NOT_IMPLEMENTED = 501,
    }

    public class Helpers
    {
        public static string OnGuardPath = "";                  // Path de instalacion de OnGuard.
        string LogPath = "";                                    // Path de Log de Lenel: ProgramData/lnl/Logs o OnGuard/Logs
        public int MainOrgID {get; set; }                       // OrgID para enviar en las llamadas del translator.
        public string IPOnGuardAPI { get; set; }
        public string IPDataConduitAPI { get; set; }
        public string IPLicenseAPI{ get; set; }
        public string Token { get; set; }                       // Token del usuario que utiliza el WebService

        public int DEFAULT_BADGETYPE { get; set; }              // Para poder definir el tipo de la badge que se crea automaticamente asociada a un device.
        public int DEFAULT_BADGESTATUS { get; set; }            // Para poder definir el status de la badge que se crea automaticamente asociada a un device.

       #region Singleton
        static Helpers _instance;

        public static Helpers GetInstance()
        {
            if (_instance == null) _instance = new Helpers();
            return _instance;
        }

        Helpers()
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

                if (Directory.Exists(OnGuardPath +@"\\logs"))
                    LogPath = OnGuardPath + @"\\logs";
                else
                    if (Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),@"Lnl\Logs")))
                        LogPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),@"Lnl\Logs");
                    else
                        LogPath =System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),"");

                if (warningLog)
                    DoLog("ATENCION: OnGuardPath dio null en la busqueda por CLSID");

                DoLog("OnGuardpath=" + OnGuardPath + " LogPath=" + LogPath);

                updateConfigurationVariables(false);
                
                DoLog("LEVANTANDO EL HH TRANSLATOR");
            }
            catch (Exception ex)
            {
                IPDataConduitAPI = "";
                IPOnGuardAPI = "";
                IPLicenseAPI = "";
                Token = "";
                MainOrgID = 0;
                DoLog("Excepcion al cargar AlutelConfig.xml. Los parametros de conexion pueden NO haber sido cargados. " + ex.Message);
            }

        }
        ~Helpers()
        {
        }
     
        #endregion

        /// <summary>
        /// Usa la CLSID de AlutelHandHeldTranslator.dll para buscar en el registro el path donde esta instalada.
        /// Esta CLSID esta hardcodeada y es {1E76211B-BE63-4B0C-BA75-A85B44AC2DD0}
        /// </summary>
        private string BuscarInstallationPath()
        {
            string res ="";
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

        public  DateTime generarDateTime(string v_Fechahora)
        {
            DateTime res = DateTime.MinValue;

            Regex FECHAHORA = new Regex(@"(.*)-(.*)-(.*) (.*):(.*):(.*)");

            Match MatchFecha = FECHAHORA.Match(v_Fechahora);
            if (MatchFecha.Success)
            {

                string Año = getMatchData(MatchFecha, 1);
                string Mes = getMatchData(MatchFecha, 2);
                string Dia = getMatchData(MatchFecha, 3);

                string Hora = getMatchData(MatchFecha, 4);
                string Minuto = getMatchData(MatchFecha, 5);
                string Segundo = getMatchData(MatchFecha, 6);

                res = new DateTime(int.Parse(Año), int.Parse(Mes), int.Parse(Dia), int.Parse(Hora), int.Parse(Minuto), int.Parse(Segundo));

            }
            return res;
        }

        /// <summary>
        /// Extrae un campo de una expresion regular reconocida
        /// </summary>
        /// <param name="resultMatch"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private  string getMatchData(Match resultMatch, int index)
        {
            return resultMatch.Groups[index].Value;
        }

        /// <summary>
        /// Devuelve la lista de AccessLevelID asociado a un PanelID
        /// </summary>
        /// <param name="v_panelID"></param>
        /// <returns></returns>
        public  List<int> obtenerAccessLevelPanel(int v_panelID)
        {
            int errCode = -1;
            string errDesc ="";

            List<int> res = WebServiceAPI.GetInstance().ObtenerAccessLevelsDeUnPanel(v_panelID, out errDesc, out errCode);

            return res;
        }

        public List<string> obtenerBadgesAlutelMobility(string orgID)
        {
            int errCode = -1;
            string errDesc = "";

            List<string> res = WebServiceAPI.GetInstance().ObtenerBadgesAlutelmobility(orgID,out errDesc, out errCode);

            return res;
        }


        /// <summary>
        /// Devuelve una lista de ints con los numeros que estan en v_listaVieja y no en v_listaNueva
        /// </summary>
        /// <param name="v_listaVieja"></param>
        /// <param name="v_listaNueva"></param>
        /// <returns></returns>
        public  List<int> listaDiferencia(string v_listaVieja, string v_listaNueva)
        {

            List<int> res = new List<int>();

            string[] numsViejos = v_listaVieja.Split(',');
            string[] numsNuevos = v_listaNueva.Split(',');

            List<string> lst_numsViejos = new List<string>(numsViejos);
            List<string> lst_numsNuevos = new List<string>(numsNuevos);

            foreach (string s in lst_numsViejos)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    if (!lst_numsNuevos.Contains(s))
                        res.Add(Convert.ToInt32(s));
                }
            }
            return res;
        }

        public  bool isBadgeActiveByAL(string prevAccessLevels, List<int> AccessLevelsDefinitions, int deletedAccessLevel)
        {
            bool res = false;
            string[] alPrevios = prevAccessLevels.Split(',');

            foreach (string i in alPrevios)
            {
                if (!String.IsNullOrEmpty(i))
                {
                    if (AccessLevelsDefinitions.Contains(Convert.ToInt32(i)) || (Convert.ToInt32(i) == deletedAccessLevel))
                    {
                        res = true;
                        break;
                    }
                }
            }
            return res;
        }
        
        public  bool chequearRangoActivo(string v_fechaActivacion, string v_fechaDesactivacion)
        {
            DateTime activationDate_D = String.IsNullOrEmpty(v_fechaActivacion) ? new DateTime(1900, 1, 1) : Helpers.GetInstance().generarDateTime(v_fechaActivacion);
            DateTime deactivationDate_D = String.IsNullOrEmpty(v_fechaDesactivacion) ? new DateTime(2100, 1, 1) : Helpers.GetInstance().generarDateTime(v_fechaDesactivacion);
            DateTime ahora = DateTime.Now;   // Para la verificacion del activation/deactivation date

            return ((activationDate_D < ahora) && (deactivationDate_D > ahora));
        }
      
        /// <summary>
        /// Actualiza cuando se modifica el archivo.
        /// </summary>
        public  void updateConfigurationVariables(bool forceUpdate)
        {
            DoLog("Entro a updateConfigurationVariables");

            try
            {
                XmlDocument xDoc = new XmlDocument();
                string configFile =OnGuardPath  + @"\AlutelConfig.xml";
               
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
                            Token =elem.Attributes["value"].Value;
                            break;
                        case "IDOrganizacion":
                            MainOrgID =int.Parse(elem.Attributes["value"].Value);
                            break;
                        case "DefaultBadgeType":
                            DEFAULT_BADGETYPE =int.Parse(elem.Attributes["value"].Value);
                            break;
                        case "DefaultBadgeStatus":
                            DEFAULT_BADGESTATUS =int.Parse(elem.Attributes["value"].Value);
                            break;
                        case "IPLicenseAPI":
                            IPLicenseAPI = elem.Attributes["value"].Value;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
               DoLog("Warning: Error loading configuration parameters " + ex.Message);
               IPDataConduitAPI = "";
               IPOnGuardAPI = "";
               Token = "";
               MainOrgID = 0;
            }
            DoLog("IPwsAlutelOGAPI: " + IPDataConduitAPI + " IPwsAlutelMobilityAPI: " + IPOnGuardAPI +" IDOrganizacion:" + MainOrgID);
        }

        public  void DoLog(string v_stringToLoG)
        {
            try
            {
                //string LogFile =  Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\\OnGuard\\Logs\\HandheldLOG.log";
                string LogFile = LogPath + @"\\HandHeldLOG.log";
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
