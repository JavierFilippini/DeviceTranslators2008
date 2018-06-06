
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading;
//using System.Threading.Tasks;

namespace ManagedHandHeldTracker
{
    public partial class frmLiveTrackingVG : Form
    {
        // Para poder llamar a javascript desde un thread diferente al main
        public static System.Threading.SynchronizationContext mainThreadContext;
        public bool  autoCenter = false;
        public  static string DEVICEID;                     // PanelID sobre el que se hizo RightClick
        public  static int ORGID;

        public  int triggerMode = 0;        // indica si la zona triggerea eventos de acceso
        public  string ZoneName = "";
        Zone actualZone;

        Regex Multiple_Pos_Data = new Regex(@"GPS:(.*);HH:(.*)");  // Cada dato es: ID,Lat,Long,Speed,DateTime, ... 5 por cada bloque
 
        int actualPointID = 0;
        public const int DELTAZOOM = 1;
        public bool isDocumentLoaded = false;

        string actualDeviceName = "";       // Para el autoCenter

        // Diccionario temporal de puntos (nombre y ubicacion) usados para definir una zona.
        private Dictionary<string, Zone.GeoCoord> markerPoints = new Dictionary<string, Zone.GeoCoord>();

        Dictionary<long, string> listaZonas;             // <PanelID, nombreVZone>
        Dictionary<long, string> listaGPS;               // <Tarjeta, NombreGPS>
        Dictionary<long, string> listaHH;                // <Tarjeta,NombreHH>
        Dictionary<long, List<long>> DevicesxZona;        // Lista de Ids de GPS o HH agrupadas por idZona.

        Dictionary<string, string> ultimasPosGPS=  new Dictionary<string , string>();          // Ultimas posiciones GPS de los devices

        bool isLoaded = false;      // Carga del mapa y de los datos para las comboboxes

        private bool resetMarkers = false;              // Para controlar el reseteo de los markers en el task de updatePositions
        private bool salir = false;

        public frmLiveTrackingVG()
        {
            InitializeComponent();
            // Para poder llamar a javascript desde un thread diferente al main
            mainThreadContext = System.Threading.SynchronizationContext.Current;
        }

        private void chkAutoCenter_CheckedChanged(object sender, EventArgs e)
        {
            autoCenter = chkAutoCenter.Checked;
        }

        private void fromLiveTrackingVG_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;     // Quita el control de cerrar ventana.
            tmrInicializacion.Enabled = true;
        }

        private void cargaInicial()
        {

                try
                {

                    string v_datosMapa = "";
                    string v_datosZona = "";
                    Tools.GetInstance().cargarMapaYDatosZona(DEVICEID.ToString(), ORGID.ToString(), ref v_datosMapa, ref v_datosZona, ref triggerMode, webBrowser.Version.Major);

                    Invoke((MethodInvoker)delegate
                    {
                        webBrowser.DocumentText = v_datosMapa;
                    });

                    string errDesc = "";
                    int errCode = -1;

                    string datos = WebServiceAPI.GetInstance().GetLiveTrackingZoneInfo(DEVICEID, ORGID.ToString(), out errDesc, out errCode);

                    //Tools.GetInstance().DoLog("datos=" + datos);

                    Regex Zone_Info = new Regex(@"ZONES:(.*);DEVICES:(.*)");

                    Match matchRespuesta;

                    matchRespuesta = Zone_Info.Match(datos);

                    if (matchRespuesta.Success)
                    {
                        string strListaZonas = getMatchData(matchRespuesta, 1);
                        string strListaDevices = getMatchData(matchRespuesta, 2);

                        // Carga las colecciones de zonas y devices
                        cargarZonas(strListaZonas);
                        cargarDevices(strListaDevices);

                      

                        Tools.GetInstance().DoLog("Fin de carga de mapa.");
                   
                    }
                    else
                        Tools.GetInstance().DoLog("MatchRespuesta no reconocida frmLiveTrackingVG: " + datos);
                  
                }
                catch (Exception ex)
                {
                    Tools.GetInstance().DoLog("Excepcion en formLiveTrackingVG-Load(): " + ex.Message);
                }

            isLoaded = true;        // poner esto aca por el if de chkZones_indexChange
            actualizarListaZonas(); // Carga las zonas en el combo y dispara el indexchange para que se actualicen los datos de la zona actual

            Invoke((MethodInvoker)delegate {
            btnMustering.Enabled = true;
            });

            Thread t = new Thread(UpdatePositions);
            t.Start();
            //Task.Factory.StartNew(() => UpdatePositions()); 

        }

        ManualResetEvent esperaActualizar = new ManualResetEvent(false);
        private void actualizarZonaEnMapaDiferido()
        {
           
            Tools.GetInstance().DoLog("Entra a actualizarZonaEnMapaDiferido()");
            esperaActualizar.WaitOne(1000);
            actualizarZonaEnMapa();

            Tools.GetInstance().DoLog("Sale de actualizarZonaEnMapaDiferido()");

        }


        private void actualizarListaZonas()
        {
            cmbZones.Items.Clear();
            foreach (KeyValuePair<long, string> par in listaZonas)
            {
                Invoke((MethodInvoker)delegate {
                    cmbZones.Items.Add(par.Value);
                });
                //cmbZones.Items.Add(par.Value);
            }
            
            // Autoselecciona la zona elegida al hacerle rightClick
            // Esto dispara el evento selectedIndexChange que a su vez actualiza el listView de devices
            List<long> claves = listaZonas.Keys.ToList();

            if (claves.Contains(long.Parse(DEVICEID)))
            {
                int pos = claves.IndexOf(long.Parse(DEVICEID));

                Invoke((MethodInvoker)delegate
                {
                    Thread.Sleep(2000);
                    cmbZones.SelectedIndex = pos;           // Dispara el indexChange para que la zona se actualice.
                });

                //cmbZones.SelectedIndex = pos;
            }
        }
        private void inicializarListViewDevices()
        {

            listViewDevices.View = View.Details;    // Tile
            listViewDevices.GridLines = true;
            listViewDevices.Columns.Clear();

            int listViewWidth = listViewDevices.Size.Width;

            listViewDevices.Columns.Add("Employee", (int)((listViewWidth - 10) * 0.6f), HorizontalAlignment.Left);
            listViewDevices.Columns.Add("Badge", (int)((listViewWidth - 10) * 0.4f), HorizontalAlignment.Left);

            //listViewDevices.Columns.Add("Employee", 140, HorizontalAlignment.Left);
            //listViewDevices.Columns.Add("Badge", 75, HorizontalAlignment.Left);
           
            listViewDevices.FullRowSelect = true;
            listViewDevices.MultiSelect = true;
            listViewDevices.HideSelection = false;
            listViewDevices.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        }

        private void actualizarListaDevices()
        {
            listViewDevices.Items.Clear();
          
            int PanelIDSelected = int.Parse(DEVICEID);

            // Llena la lista de devices con los devices asignados a la zona elegida.
            if (DevicesxZona.ContainsKey(PanelIDSelected))
            {
                List<long> listaActualDevices = DevicesxZona[PanelIDSelected];

                
                // Primero los GPSs
                foreach (KeyValuePair<long, string> par in listaGPS)
                {
                    if (listaActualDevices.Contains(par.Key))
                    {
                        ListViewItem list = listViewDevices.Items.Add(par.Value);
                    }
                }

                // Luego los HH
                foreach (KeyValuePair<long, string> par in listaHH)
                {
                    if (listaActualDevices.Contains(par.Key))
                    {
                        ListViewItem list = listViewDevices.Items.Add(par.Value);
                    }
                }
            }
        }

        private void cargarZonas(string v_strListaZonas)
        {
            listaZonas = new Dictionary<long, string>();
            string[] strZonas = v_strListaZonas.Split(',');

            for (int i =0; i<strZonas.Length; i=i+2)
            {
                int idZona = 0;

                if (int.TryParse(strZonas[i],out idZona))
                {
                    if (!listaZonas.ContainsKey(idZona))
                    {
                        listaZonas.Add(idZona, strZonas[i + 1]);
                    }
                }
            }
        }

        // Formato: Tarjeta,NombeApellido,Tipo,IDZona
        private void cargarDevices(string v_strListaDevices)
        {
            //MessageBox.Show("listaDevices:" + v_strListaDevices);
            listaGPS = new Dictionary<long, string>();
            listaHH = new Dictionary<long, string>();
            DevicesxZona = new Dictionary<long, List<long>>();

            //Tools.GetInstance().DoLog("listadevices=" + v_strListaDevices);

            string[] strDevices = v_strListaDevices.Split(',');

            for (int i = 0; i < strDevices.Length; i = i + 4)
            {
                Int64 tarjeta = 0;
                string devType = "";
                int idZona =0;

                if (Int64.TryParse(strDevices[i], out tarjeta))
                {
                    devType = strDevices[i + 2];
                    
                    // Carga la lista de HH
                    if (devType == "HH")
                    {
                        if (!listaHH.ContainsKey(tarjeta))
                        {
                            listaHH.Add(tarjeta, strDevices[i + 1] + "|" + tarjeta.ToString());
                        }
                    }

                    //// Carga la lista de GPS
                    //if (devType == "GPS")
                    //{
                    //    if (!listaGPS.ContainsKey(idDevice))
                    //    {
                    //        listaGPS.Add(idDevice, strDevices[i + 1]);
                    //    }
                    //}

                    // Carga la asociacion de GPS y HH a ZOnas.
                    if (int.TryParse(strDevices[i+3], out idZona))
                    {
                        if (!DevicesxZona.ContainsKey(idZona))
                        {
                            List<long> nuevaListaDevices = new List<long>();
                            DevicesxZona.Add(idZona, nuevaListaDevices);
                        }
                        List<long> idsDevices = DevicesxZona[idZona];

                        if (!idsDevices.Contains(tarjeta))
                        {
                            idsDevices.Add(tarjeta);
                        }
                    }
                }
            }
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


      
        private void cmbZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tools.GetInstance().DoLog("Entra a cmbZones_SelectedIndexChanged()");
            if (isLoaded)
            {
                try
                {
                    actualDeviceName = "";
                    int elegido = cmbZones.SelectedIndex;
                    Tools.GetInstance().DoLog("Elegido=" + elegido);
                    if (elegido >= 0)
                    {
                        List<long> claves = listaZonas.Keys.ToList();

                        Tools.GetInstance().DoLog("La Cantidad de claves=" + claves.Count);

                        if (elegido < claves.Count)
                        {
                            webBrowser.Document.InvokeScript("deleteAllGates");

                            resetMarkers = true;
                            FinalizarUpdatePositions.Set();         // Fuerza una finalizacion del loop principal del task de update positions y posterior reseteo con el nuevo conjunto de markers

                            Thread.Sleep(500);
                            Tools.GetInstance().DoLog("Sigue...");
                            webBrowser.Document.InvokeScript("deleteAllMarkersMap");

                            DEVICEID = claves[elegido].ToString();

                            Tools.GetInstance().DoLog("DEVICEID=" + DEVICEID);

                            string zonaDef = "";
                            Tools.GetInstance().cargarDatosUnaZona(ref  Tools.GetInstance().ZoneName, ref zonaDef, ref triggerMode, DEVICEID, ORGID);
                            Tools.GetInstance().DoLog("ZoneDef=" + zonaDef);

                            crearZonaPordef(zonaDef);

                            actualizarZonaEnMapa();
                            ZoomToFitZone();

                            //actualizarListaDevices();
                            actualizarListaItems("");
                            txtFiltro.Text = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Tools.GetInstance().DoLog("Excepcion en selectedIndexChanged:" + ex.Message);
                }
            }
            else
                Tools.GetInstance().DoLog("isLoaded dio FALSE en cmbZones_SelectedIndexChange()");
        }

        private void fromLiveTrackingVG_FormClosing(object sender, FormClosingEventArgs e)
        {
            resetMarkers = false;
            FinalizarUpdatePositions.Set();
        }

        private void tmrUpdateVZ_Tick(object sender, EventArgs e)
        {
            
        }

        private void tmrLoadZone_Tick(object sender, EventArgs e)
        {
        }

        //strZona: Lista de: Lat1,Long1,ORD1,Lat2,Long2,ORD2,TIPOACCESO: 7 items
        public void crearZonaPordef(string v_strZona)
        {
            actualZone = new Zone("NuevaZona");
            actualPointID = 0;
            markerPoints = new Dictionary<string, Zone.GeoCoord>();


            string[] listaDatos = v_strZona.Split(',');
            if (listaDatos.Length >= 9)
            {
                for (int i = 0; i < listaDatos.Length; i = i + 9)
                {
                    Zone.ZonePoint desde = new Zone.ZonePoint("F" + (1 + (i / 9)).ToString(), listaDatos[i], listaDatos[i + 1], int.Parse(listaDatos[i + 2]));
                    Zone.ZonePoint hacia = new Zone.ZonePoint("T" + (1 + (i / 9)).ToString(), listaDatos[i + 3], listaDatos[i + 4], int.Parse(listaDatos[i + 5]));
                    string gateType = listaDatos[i + 6];
                    string LNLEntranceReaderID = listaDatos[i + 7];
                    string LNLExitReaderID = listaDatos[i + 8];

                    Zone.GateDefinition nuevaGate = new Zone.GateDefinition();

                    nuevaGate.from = desde;
                    nuevaGate.to = hacia;

                    nuevaGate.type = (Zone.GateAccessType)Enum.Parse(typeof(Zone.GateAccessType), gateType);

                    nuevaGate.ID = "Gate" + (1 + (i / 9)).ToString();

                    if (LNLEntranceReaderID != "")
                    {
                        nuevaGate.LNLEntranceReaderID = int.Parse(LNLEntranceReaderID);
                        nuevaGate.LNLExitReaderID = int.Parse(LNLExitReaderID);
                    }
                    actualZone.addGate(nuevaGate.ID, nuevaGate);

                    // Ahora actualizo los markers. 
                    //Como las puertas estan conectadas, la lista de markers es solo con el punto de salida.
                    addPointToMarkers(desde);
                    //actualizarMarkersEnMapa(markerPoints);
                }

                Tools.GetInstance().DoLog("Zona definida en memoria: " + actualZone.IDZona + " con: " + actualZone.listaPuertas.Count + " gates");

            }
         
        }

        public void addPointToMarkers(Zone.ZonePoint v_ZP)
        {
            actualPointID++;

            string idNuevoPunto = "P" + actualPointID.ToString();

            markerPoints.Add(idNuevoPunto, v_ZP.position);

  
        }

        /// <summary>
        /// Usa los datos de la ActualZone para resetear el mapa y dibujar la zona.
        /// </summary>
        private void actualizarZonaEnMapa()
        {
            Tools.GetInstance().DoLog("Va a actualizarZonaEnMapa()");
            string coordArray = "";
            string gateColor = "";
            string latGate = "";
            string longGate = "";
            if (actualZone.listaPuertas.Count > 0)
            {
                foreach (KeyValuePair<string, Zone.GateDefinition> puerta in actualZone.listaPuertas)
                {
                    latGate = puerta.Value.from.position.latitude;
                    longGate = puerta.Value.from.position.longitude;

                    coordArray = coordArray + latGate + "," + longGate + "," + puerta.Value.to.position.latitude + "," + puerta.Value.to.position.longitude;

                    gateColor = obtenerGateColor(puerta.Value.type);

                    coordArray = coordArray + "," + gateColor + ",";
                }
                coordArray = coordArray.Substring(0, coordArray.Length - 1);

                // LLama al javascript() para dibujar las zonas en el mapa.
                
                object[] args = { coordArray };
                Tools.GetInstance().DoLog("va a llamar a verzonas con coordArray=" + coordArray);

                try
                {
                    //// Si no se llama asi, no se tiene acceso al webbrowser1
                    //mainThreadContext.Send(delegate
                    //{
                    //    webBrowser.Document.InvokeScript("verZonas", args);
                    //}, null);

                    Invoke((MethodInvoker)delegate
                    {
                        webBrowser.Document.InvokeScript("verZonas", args);
                    });


                }
                catch (Exception) { }

                //   webBrowser.Document.InvokeScript("verZonas", args);
            }
            else
            {
                Tools.GetInstance().DoLog("No hay puertas definidas");
                latGate = "0";              // Sin no hay nada definido, mostrar el mapa en 0,0
                longGate = "0";
            }

            object[] args2 = { latGate, longGate };

            //// Centrar el mapa
            //webBrowser.Document.InvokeScript("centrarMapa", args2);

            // Actualizar el triggerMode
            Invoke((MethodInvoker)delegate
                 {
                     chkTrigger.Checked = (triggerMode == 1);
                 });
        }

        private string obtenerGateColor(Zone.GateAccessType v_gateType)
        {
            string res = Zone.GateColorAccessForbidden;

            switch (v_gateType)
            {
                case Zone.GateAccessType.Granted:
                    res = Zone.GateColorAccessGranted;
                    break;
                case Zone.GateAccessType.Forbidden:
                    res = Zone.GateColorAccessForbidden;
                    break;
                case Zone.GateAccessType.Entrance:
                    res = Zone.GateColorAccessEntry;
                    break;
                case Zone.GateAccessType.Exit:
                    res = Zone.GateColorAccessExit;
                    break;
            }
            return res;
        }


        //private void actualizarMarkersEnMapa(Dictionary<string, Zone.GeoCoord> markers, Dictionary<string,string> lstBubbles)
        private void actualizarMarkersEnMapa(Dictionary<string, Zone.GeoCoord> markers)
        {
            string coordArray = "";
            //string bubbleArray = "";

            foreach (KeyValuePair<string, Zone.GeoCoord> par in markers)
            {
                coordArray = coordArray + par.Key + "," + par.Value.latitude + "," + par.Value.longitude + ",";
                //coordArray = coordArray + par.Value.latitude + "," + par.Value.longitude + ",";
                //bubbleArray = bubbleArray + lstBubbles[par.Key].Replace("|"," ") + "|";
                if (ultimasPosGPS.ContainsKey(par.Key))
                    ultimasPosGPS[par.Key] = par.Value.latitude + "," + par.Value.longitude;
                else
                    ultimasPosGPS.Add(par.Key, par.Value.latitude + "," + par.Value.longitude);
            }
            coordArray = coordArray.TrimEnd(',');
            //bubbleArray = bubbleArray.TrimEnd('|');

            //object[] args = { coordArray,bubbleArray };
            object[] args = { coordArray };

            //Helpers.GetInstance().DoLog("Llama a verMarkers. coordarray: " + coordArray + " bubbleArray = " + bubbleArray);
            //Tools.GetInstance().DoLog("Llama a verMarkers. coordarray: " + coordArray );

            try
            {
                // Si no se llama asi, no se tiene acceso al webbrowser1
                mainThreadContext.Send(delegate
                {
                    webBrowser.Document.InvokeScript("verMarkers2", args);
                }, null);
            }
            catch (Exception) {  }

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //tmrLoadZone.Enabled = true;
            //tmrLoadZone.Interval = 2000;
            //isDocumentLoaded = true;
        }

        private void btnZoomMas_Click(object sender, EventArgs e)
        {
            object[] args = { DELTAZOOM };
            webBrowser.Document.InvokeScript("ZoomIn", args);       //// Centrar el mapa
        }

        public void ZoomToFitZone()
        {
            string res = (string)webBrowser.Document.InvokeScript("zoomToFitZone");
        }

        // Ubica el mapa dentro de la zona definida por los markers.
        private void ZoomToFitMarkers(bool v_doIt)
        {
            if (v_doIt)
            {
                string res = (string)webBrowser.Document.InvokeScript("zoomToFitMarkers");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            resetMarkers = false;
            FinalizarUpdatePositions.Set();
            this.Close();
        }

        private void btnZoomMenos_Click_1(object sender, EventArgs e)
        {
            object[] args = { DELTAZOOM };
            webBrowser.Document.InvokeScript("ZoomOut", args);            //// Centrar el mapa
        }

        private void btnZoomToFit_Click_1(object sender, EventArgs e)
        {
            actualDeviceName = "";
            ZoomToFitZone();
        }

        private void btnCenterMap_Click(object sender, EventArgs e)
        {

        }

        private void tmrUpdatePositions_Tick(object sender, EventArgs e)
        {
            tmrInicializacion.Enabled = false;     // Solo se ejecuta UNA vez: para cargar los controles.
            inicializarListViewDevices();

            if (salir)
            {
                resetMarkers = false;
                FinalizarUpdatePositions.Set();
                this.Close();
            }
            try
            {
                if (!isLoaded)
                {
                    Tools.GetInstance().DoLog("Va a cargar los datos de la zona");
                    Thread t = new Thread(cargaInicial);
                    t.Start();

//                    Task.Factory.StartNew(() => cargaInicial());        // Actualiza los listboxes y el mapa.
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en tmrUpdatePositions_Tick: " + ex.Message);
            }
          
        }

        ManualResetEvent FinalizarUpdatePositions = new ManualResetEvent(false);
        private void UpdatePositions()
        {
            bool exitAll = false;
            Invoke((MethodInvoker)delegate
            {
                lblLoading.Visible = false;
            });

            try
            {
                Tools.GetInstance().DoLog("*** COMIENZA task de updatePositions");
                while (!exitAll)
                {
                    BufferManager.GetInstance().resetBuffer();
                    while (!FinalizarUpdatePositions.WaitOne(500))
                    {
                        Dictionary<string, Zone.GeoCoord> BufferToDraw = BufferManager.GetInstance().ObtenerSiguienteBuffer();

                        if (BufferToDraw != null)
                        {
                            if (BufferToDraw.Count > 0)
                            {
                                actualizarMarkersEnMapa(BufferToDraw);// Ok, draw it.
                                if (autoCenter)
                                {
                                    if (!String.IsNullOrEmpty(actualDeviceName))
                                        centrarMapaEnDevice(actualDeviceName);
                                }
                            }
                        }
                        else
                            Tools.GetInstance().DoLog("BufferToDraw es NULL en UpdatePositions()");
                    }
                    Tools.GetInstance().DoLog("Sale de updatePositions con resetmarkers = " + resetMarkers);

                    if (resetMarkers)
                    {

                        // Si no se llama asi, no se tiene acceso al webbrowser1
                        mainThreadContext.Send(delegate
                        {
                            webBrowser.Document.InvokeScript("deleteAllMarkersMap");
                        }, null);
                        
                        resetMarkers = false;
                        FinalizarUpdatePositions.Reset();

                    }
                    else
                        exitAll = true;
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en UpdatePostions: " + ex.Message);
            }


            Tools.GetInstance().DoLog("*** FINALIZA task de updatePositions");


        }

        // <summary>
        /// Procedimiento Gral de actualizacion de Markers de devices en el mapa.
        /// Manda el mensaje al server con los IDs de los GPS y HH que se quieren actualizar
        /// Espera la respuesta y hace un  reDraw de las posiciones.
        /// </summary>
        //private void actualizeLivePositions()
        //{
        //    long PanelIDSelected = long.Parse(DEVICEID);

        //    if (DevicesxZona.ContainsKey(PanelIDSelected))
        //    {
        //        string texto = "";
        //        try
        //        {
        //            string errDesc = "";
        //            int errCode = -1;
        //            string datos = WebServiceAPI.GetInstance().GetMultiplePositionsFromZone(PanelIDSelected.ToString(),ORGID.ToString(), out errDesc, out errCode);

        //            Match  matchRespuesta = Multiple_Pos_Data.Match(datos);

        //            if (matchRespuesta.Success)
        //            {
        //                string[] GPSDataPositions = getMatchData(matchRespuesta, 1).Split(',');         // Cada dato es: UNITID,Lat,Long,Speed,DateTime, ... 5 por cada bloque
        //                string[] HHPositions = getMatchData(matchRespuesta, 2).Split(',');              // Cada dato es: PANELID,Lat,Long,Speed,DateTime, ... 5 por cada bloque

        //                markerPoints.Clear();
        //               // Dictionary<string, string> lstBubbles = new Dictionary<string, string>();     // Diccionario con las infoBubble de empleados a bordo de los HH

        //                for (int i = 0; i < GPSDataPositions.Length - 1; i = i + 5)
        //                {
        //                    long ID = Convert.ToInt64(GPSDataPositions[i]);                             // Es el unitID
        //                    string lat = GPSDataPositions[i + 1];
        //                    string lng = GPSDataPositions[i + 2];
        //                    string speed = GPSDataPositions[i + 3];
        //                    string dateTime = GPSDataPositions[i + 4];

        //                    Zone.GeoCoord nuevoGPS = new Zone.GeoCoord(lat, lng);
        //                    markerPoints.Add(ID.ToString(), nuevoGPS);
        //                   // lstBubbles.Add(ID.ToString(), "GPS");                                     // El infoBubble de los GPS dice 'GPS'

        //                    // Update en RAM
        //                    if (!ultimasPosGPS.ContainsKey(ID))
        //                        ultimasPosGPS.Add(ID, lat + "," + lng);
        //                    else
        //                        ultimasPosGPS[ID] = lat + "," + lng;
        //                }

        //                for (int i = 0; i < HHPositions.Length - 1; i = i + 5)
        //                {
        //                    try
        //                    {
        //                        long ID = Convert.ToInt64(HHPositions[i]); // Es el PANELID del HH
        //                        string lat = HHPositions[i + 1];
        //                        string lng = HHPositions[i + 2];
        //                        string speed = HHPositions[i + 3];
        //                        string dateTime = HHPositions[i + 4];

        //                        Zone.GeoCoord nuevoGPS = new Zone.GeoCoord(lat, lng);
        //                        markerPoints.Add(ID.ToString(), nuevoGPS);

        //                        errDesc = "";
        //                        errCode = 0;

        //                        //Dictionary<int, empInfo> listaEmp = WebServiceAPI.GetInstance().getListaEmpleadosABordo(ID.ToString(), ORGID.ToString(), out errDesc, out errCode);

        //                        string deviceName = "";
        //                        if ( listaHH.ContainsKey(ID))
        //                            deviceName = listaHH[ID];

        //                        //string infoBubble = Helpers.GetInstance().construirHTMLInfoBubble(listaEmp, deviceName);

        //                        //lstBubbles.Add(ID.ToString(), infoBubble);      // El infoBubble de los HH tiene la lista de empleados a bordo.

        //                        // Update en RAM
        //                        if (!ultimasPosGPS.ContainsKey(ID))
        //                            ultimasPosGPS.Add(ID, lat + "," + lng);
        //                        else
        //                            ultimasPosGPS[ID] = lat + "," + lng;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        i = i + 1;  // WORKAROUND POR BUG: SE INSERTA UN VALOR EXTRAÑO. NO DETENER LA CARGA
        //                        Tools.GetInstance().DoLog("Excepcion en actualizeLivePositions2() inside LOOP: " + ex.Message);
        //                        Tools.GetInstance().DoLog("texto: " + texto);
        //                        //salirXError = true;
        //                    }
        //                }

        //                // Listo: Mostrarlos...
        //                //actualizarMarkersEnMapa(markerPoints, lstBubbles);
        //                actualizarMarkersEnMapa(markerPoints);
        //            }
                    
        //        }
        //        catch (Exception ex)
        //        {
        //            Tools.GetInstance().DoLog("Excepcion en actualizeLivePositions2(): " + ex.Message);
        //        }
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           // actualizeLivePositions();
        }

        private void listViewDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                ListView.SelectedListViewItemCollection itemCollection = listViewDevices.SelectedItems;
                //Tools.GetInstance().DoLog("itemCollection.count=" + itemCollection.Count);
                if (itemCollection.Count > 0)
                {
                    ListViewItem item = itemCollection[0];
                    string DeviceName = item.Name.Split('|')[1];                // En el name del listItem viene Nombre Apellido|Tarjeta|DeviceName

                    if (!String.IsNullOrEmpty(DeviceName))
                    {
                        actualDeviceName = DeviceName;
                        centrarMapaEnDevice(DeviceName);
                    }

                }
            }
            catch (Exception ex)
            { }
        }
        private void centrarMapaEnDevice(string v_DeviceName)
        {
            Tools.GetInstance().DoLog("Llama a centrarMapaEnDevice con device=" + v_DeviceName);
            
            if (!String.IsNullOrEmpty(v_DeviceName))
            {

                if (ultimasPosGPS.ContainsKey(v_DeviceName))
                {
                    string posGPS = ultimasPosGPS[v_DeviceName];

                    object[] args = posGPS.Split(',');

                    if (args.Length == 2)
                    {
                        Tools.GetInstance().DoLog("Va a centrar el mapa en " + posGPS);
                        //// Centrar el mapa
                        try
                        {
                            // Si no se llama asi, no se tiene acceso al webbrowser1
                            mainThreadContext.Send(delegate
                            {
                                webBrowser.Document.InvokeScript("centrarMapa", args);
                            }, null);
                        }
                        catch (Exception) { }
                      
                    }
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //crearZonaPordef(zonaDef);
            //ZoomToFitMarkers(true);
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            ZoomToFitZone();
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            webBrowser.Document.InvokeScript("deleteAllGates");
            webBrowser.Document.InvokeScript("deleteAllMarkersMap");
        }

        private void fromLiveTrackingVG_Activated(object sender, EventArgs e)
        {
            //if (!isLoaded)
            //{
            //    isLoaded = true;
            //    cargarInicial();
            //}


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            //webBrowser.Refresh();
            //actualizarZonaEnMapa();
            
            cmbZones_SelectedIndexChanged(this, null);



        }

        private void button1_Click_5(object sender, EventArgs e)
        {
            MessageBox.Show(actualZone.listaPuertas.Count().ToString());
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            actualizarListaItems(txtFiltro.Text);
        }


        private void actualizarListaItems(string filtro)
        {
            // Devuelve la lista de nombres que cumplen con el filtro
            List<string> listaFiltro = filtrarNombre(filtro);

            listViewDevices.Items.Clear();
            var items = new ListViewItem[listaFiltro.Count];
            int i = 0;
            foreach (string s in listaFiltro)
            {
                ListViewItem n = new ListViewItem(s.Split('|')[0]);
                n.Name = s;
                n.SubItems.Add(s.Split('|')[2]);        // La tarjeta
                items[i] = n;
                i++;
            }

            listViewDevices.BeginUpdate();
            Invoke((MethodInvoker)delegate
            {
                listViewDevices.Items.AddRange(items);
            });

            listViewDevices.EndUpdate();


           
        }

        private List<string> filtrarNombre(string filtro)
        {
            List<string> res = new List<string>();
            List<string> listaDevices = construirListaDevices();

            foreach (string s in listaDevices)
                if (s.ToUpper().IndexOf(filtro.ToUpper()) >= 0)
                    res.Add(s);


            return res;
        }


        private List<string> construirListaDevices()
        {
            List<string> res = new List<string>();
            int PanelIDSelected = int.Parse(DEVICEID);

            if (DevicesxZona.ContainsKey(PanelIDSelected))
            {
                List<long> listaActualDevices = DevicesxZona[PanelIDSelected];


                // Primero los GPSs
                foreach (KeyValuePair<long, string> par in listaGPS)
                {
                    if (listaActualDevices.Contains(par.Key))
                    {
                        res.Add(par.Value );
                    }
                }

                // Luego los HH
                foreach (KeyValuePair<long, string> par in listaHH)
                {
                    if (listaActualDevices.Contains(par.Key))
                    {
                        res.Add(par.Value + "|" + par.Key.ToString());
                    }
                }
            }

            return res;
        }

        private void button1_Click_6(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            try
            {
                listViewDevices.Items[0].EnsureVisible();
            }
            catch (Exception) { }
        }

        private void listViewDevices_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnMustering_Click(object sender, EventArgs e)
        {
            frmMustering frmM = new frmMustering();
            frmM.listaZonas = listaZonas;

            frmM.ShowDialog();

        }


      

    }
}
