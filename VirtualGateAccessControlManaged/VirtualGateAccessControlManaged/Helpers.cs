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

namespace VirtualGateManaged
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

    public class Helpers
    {
        public static string OnGuardPath = "";                  // Path de instalacion de OnGuard.
        string LogPath = "";                                    // Path de Log de Lenel: ProgramData/lnl/Logs o OnGuard/Logs
        public int MainOrgID { get; set; }                          // OrgID para enviar en las llamadas del translator.
        public string IPOnguardAPI { get; set; }
        public string IPDataConduitAPI { get; set; }
        public string IPLicenseAPI { get; set; }
        public string Token { get; set; }                           // Token del usuario que utiliza el WebService

        #region Singleton
        static Helpers _instance;

        public static Helpers GetInstance()
        {
            if (_instance == null) _instance = new Helpers();
            return _instance;
        }

        Helpers()
        {
            // Registra el evento para actuzlizar las variables de configuracion si cambia el contenido del archivo AlutelConfig.xml
            try
            {
                bool warningLog = false;        // Para que cuando tenga el path del log loguee que no encotro la CLSID

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

                updateConfigurationVariables(true);

                DoLog("LEVANTANDO EL VG TRANSLATOR");
            }
            catch (Exception ex)
            {
                IPDataConduitAPI = "";
                IPOnguardAPI = "";
                IPLicenseAPI = "";
                Token = "";
                MainOrgID = 0;
                DoLog("Excepcion al cargar AlutelConfig.xml. Los parametros de conexion no han sido cargados. " + ex.Message);
            }

        }
        ~Helpers()
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
            string errDesc = "";

            List<int> res = WebServiceAPI.GetInstance().ObtenerAccessLevelsDeUnPanel(v_panelID, out errDesc, out errCode);

            return res;
        }
        public List<string> obtenerBadgesAlutelMobility(string orgID)
        {
            int errCode = -1;
            string errDesc = "";

            List<string> res = WebServiceAPI.GetInstance().ObtenerBadgesAlutelmobility(orgID, out errDesc, out errCode);

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
        /// La actualizacion de las variables no se hace en cada llamada sino cada TIMES_TO_RECONFIG veces 
        /// excepto si forceUpdate es true
        /// </summary>
        public  void updateConfigurationVariables(bool forceUpdate)
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
                            IPOnguardAPI = elem.Attributes["value"].Value;
                            break;
                        case "Token":
                            Token = elem.Attributes["value"].Value;
                            break;
                        case "IDOrganizacion":
                            MainOrgID = int.Parse(elem.Attributes["value"].Value);
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
            }
            DoLog("IPwsAlutelOGAPI: " + IPDataConduitAPI + " IPwsAlutelMobilityAPI: " + IPOnguardAPI + " Token:" + Token + " IDOrganizacion:" + MainOrgID);
        }


        private  string desencriptar(string cadena, string clave)
        {
            // Convierto la cadena y la clave en arreglos de bytes
            // para poder usarlas en las funciones de encriptacion
            // En este caso la cadena la convierta usando base 64
            // que es la codificacion usada en el metodo encriptar
            byte[] cadenaBytes = Convert.FromBase64String(cadena);
            byte[] claveBytes = Encoding.UTF8.GetBytes(clave);

            // Creo un objeto de la clase Rijndael
            RijndaelManaged rij = new RijndaelManaged();

            // Configuro para que utilice el modo ECB
            rij.Mode = CipherMode.ECB;

            // Configuro para que use encriptacion de 256 bits.
            rij.BlockSize = 256;

            // Declaro que si necesitara mas bytes agregue ceros.
            rij.Padding = PaddingMode.Zeros;

            // Declaro un desencriptador que use mi clave secreta y un vector
            // de inicializacion aleatorio
            ICryptoTransform desencriptador;
            desencriptador = rij.CreateDecryptor(claveBytes, rij.IV);

            // Declaro un stream de memoria para que guarde los datos
            // encriptados
            MemoryStream memStream = new MemoryStream(cadenaBytes);

            // Declaro un stream de cifrado para que pueda leer de aqui
            // la cadena a desencriptar. Esta clase utiliza el desencriptador
            // y el stream de memoria para realizar la desencriptacion
            CryptoStream cifradoStream;
            cifradoStream = new CryptoStream(memStream, desencriptador, CryptoStreamMode.Read);

            // Declaro un lector para que lea desde el stream de cifrado.
            // A medida que vaya leyendo se ira desencriptando.
            StreamReader lectorStream = new StreamReader(cifradoStream);

            // Leo todos los bytes y lo almaceno en una cadena
            string resultado = lectorStream.ReadToEnd();

            // Cierro los dos streams creados
            memStream.Close();
            cifradoStream.Close();

            // Quito los '\0' que completan la cadena
            //if (resultado.Contains(@"\0")) resultado = resultado.Substring(0, resultado.IndexOf(@"\0"));


            // Devuelvo la cadena
            return resultado.Replace("\0", "");
        }

        public void DoLog(string v_stringToLoG)
        {
            try
            {
                //string LogFile = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\\OnGuard\\logs\\VirtualZoneLOG.log";
                string LogFile = LogPath + @"\\VirtualZoneLOG.log";

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
