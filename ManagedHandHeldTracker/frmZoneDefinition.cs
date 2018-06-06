using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace ManagedHandHeldTracker
{
    public partial class frmZoneDefinition : Form
    {

        public string DEVICEID;             // Es el PanelID del LENEL
        public int ORGANIZATIONID;

        public const int DELTAZOOM = 1;

        string zonaDef = "";        // ARRAY DE PUNTOS DE DEFINICION DE ZONA RECIBIDO DESDE EL SERVER
        int triggerMode = 0;        // Indicador si la zona triggerea accesos o no

        public string ZoneName="";
        string actualGate = "";     // Variable temporal para la seleccion de la gate.
        Zone actualZone;
        Dictionary<int, KeyValuePair<Zone.GateAccessType,string>> listaReadersDescriptions; // Lista de descripciones de los readers LENEL

        //StreamWriter writerFile;

        private bool flagAddmarker = false;
        int actualPointID = 0;

        // Diccionario temporal de puntos (nombre y ubicacion) usados para definir una zona.
        private Dictionary<string, Zone.GeoCoord> markerPoints = new Dictionary<string, Zone.GeoCoord>();

        Zone.GeoCoord actualPoint = new Zone.GeoCoord();    // Punto actualmente seleccionado.

        public frmZoneDefinition()
        {
            InitializeComponent();
        }

        private void frmZoneDefinition_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //string personalFolder = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //writerFile = new StreamWriter(@personalFolder + @"\\logZoneDefinition.txt");
            //writerFile.AutoFlush = true;

            string datosMapa = "";
            string datosZona = "";

            //StaticCustomOptionsManager.cargarDatosLicencia();
            Tools.GetInstance().cargarMapaYDatosZona(DEVICEID.ToString(), ORGANIZATIONID.ToString(), ref  datosMapa, ref datosZona, ref triggerMode, webBrowser.Version.Major);
            
            zonaDef = datosZona;
            
            //MessageBox.Show("DatosZona: " + datosZona);
            webBrowser.DocumentText = datosMapa;

            listaReadersDescriptions = Tools.GetInstance().cargarReadersList(DEVICEID, ORGANIZATIONID.ToString());
            //MessageBox.Show("Cantidad de gates: " + listaReadersDescriptions.Count.ToString());
            cargarDatosReaders(listaReadersDescriptions);
          
        }

        private void cargarDatosReaders(Dictionary<int,KeyValuePair<Zone.GateAccessType,string>> v_datosReaders)
        {
            if (v_datosReaders.Count > 0)
            {
                cmbEntrance.Items.Clear();
                cmbExit.Items.Clear();

                cmbEntrance.Items.Add(Tools.GetInstance().NONESTRING);
                cmbExit.Items.Add(Tools.GetInstance().NONESTRING);

                foreach (KeyValuePair<int, KeyValuePair<Zone.GateAccessType,string>> lector in v_datosReaders)
                {
                    //MessageBox.Show ("Va a agregar el reader: " + lector.Value.Value + " del tipo: "+ lector.Value.Key.ToString());
                    if (lector.Value.Key == Zone.GateAccessType.Entrance)
                    {
                        cmbEntrance.Items.Add(lector.Value.Value);
                    }

                    if (lector.Value.Key == Zone.GateAccessType.Exit)
                    {
                        cmbExit.Items.Add(lector.Value.Value);
                    }
                }
                cmbEntrance.Tag = null;
                cmbEntrance.SelectedIndex = 0;

                cmbExit.Tag = null;
                cmbExit.SelectedIndex = 0;

                cmbEntrance.Tag = true;
                cmbExit.Tag = true;

            }
        }

        private void btnAddPoints_Click(object sender, EventArgs e)
        {

            flagAddmarker = true;
            btnAddPoints.BackColor = Color.Red;
            deleteAllZones();
            webBrowser.Document.InvokeScript("flagAgregarTrue");
            btnCreate.Visible = true;
            btnCancelNew.Visible = true;
            btnCancelNew.Text = "Cancel";
            btnAddPoints.Enabled = false;

        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            deleteAllMarkers();
            actualZone.listaPuertas.Clear();
            actualizarListViewGates();
            webBrowser.Document.InvokeScript("borrarZonas");
        }


        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //HtmlDocument Document = webBrowser.Document;

            //HtmlElementEventHandler mouseDownEvent = new HtmlElementEventHandler(EventoClickMouse);
            //Document.Body.MouseDown += mouseDownEvent;

            tmrLoadZone.Enabled = true;         
            tmrLoadZone.Interval = 2000;
        }


        private void crearMarkers(string listaCoords)
        {
            string[] coordArray = listaCoords.Split(',');

			markerPoints.Clear();
            if (coordArray.Length>2)
            {
                actualPointID = 0;
                for (int i = 0; i < coordArray.Length; i = i + 2)
                {
                    actualPointID++;

                    string idNuevoPunto = "P" + actualPointID.ToString();
                    Zone.GeoCoord coordNuevoPunto = new Zone.GeoCoord(coordArray[i], coordArray[i+1]);
                    markerPoints.Add(idNuevoPunto, coordNuevoPunto);
                }
            }
        }

        // Evento general de atencion de eventos de click sobre el mapa. 
        // Las distintas funciones a cumplir vienen definidas por flags externos: flagAddMarker, etc.
        public void EventoClickMouse(object sender, HtmlElementEventArgs e)
        {
            

            object[] args = { };

            string res = (string)webBrowser.Document.InvokeScript("actualCoords", args);

            this.Text = "La posicion es: " + res;

            // Hizo un click para agregar un punto
            if (flagAddmarker)
            {
              
                btnCreate.Visible = true;
                actualPointID++;

                string idNuevoPunto = "P" + actualPointID.ToString();
                string nuevoPunto = (string)webBrowser.Document.InvokeScript("actualCoords");

                Zone.GeoCoord coordNuevoPunto = stringCoordToGeoCoord(nuevoPunto);

                markerPoints.Add(idNuevoPunto, coordNuevoPunto);
                actualPoint = coordNuevoPunto;

                // Actualiza el ListView de los puntos de la zona.
                //actualizarListaPuntos();

                actualizarMarkersEnMapa(markerPoints);
            }
        }

        // Convesion de string a Coordenadas Geometricas.
        private Zone.GeoCoord stringCoordToGeoCoord(string v_punto)
        {
            Zone.GeoCoord result = new Zone.GeoCoord();

            string[] coords = v_punto.Split(',');

            result.latitude = coords[0].Substring(1);
            result.longitude = coords[1].Substring(0, coords[1].Length - 1);
            return result;
        }


        private void actualizarMarkersEnMapa(Dictionary<string, Zone.GeoCoord> markers)
        {
            string coordArray = "";

            foreach (KeyValuePair<string, Zone.GeoCoord> par in markers)
            {
                coordArray = coordArray + par.Value.latitude + "," + par.Value.longitude + ",";
            }
            object[] args = { coordArray,"" };          // Sin bubbles

            string ret = (string)webBrowser.Document.InvokeScript("verMarkers", args);
        }
        private void deleteAllZones()
        {
            deleteAllMarkers();
            if (actualZone !=null)
                actualZone.listaPuertas.Clear();
            if(actualZone!=null)
                actualizarListViewGates();
            webBrowser.Document.InvokeScript("deleteAllGates");

        }

        private void deleteAllMarkers()
        {
            webBrowser.Document.InvokeScript("deleteAllMarkers");
            markerPoints.Clear();
            actualPointID = 0;
           // actualizarListaPuntos();
            webBrowser.Document.InvokeScript("noResaltarPunto");
        }

        // Actualiza la lista de puntos actuales: tempPoints
        //public void actualizarListaPuntos()
        //{
        //    listViewPoints.View = View.Details;
        //    listViewPoints.GridLines = true;
        //    listViewPoints.Columns.Clear();
        //    listViewPoints.Columns.Add("ID", 40, HorizontalAlignment.Left);
        //    listViewPoints.Columns.Add("Latitude", 80, HorizontalAlignment.Left);
        //    listViewPoints.Columns.Add("Longitude", 80, HorizontalAlignment.Left);
        //    listViewPoints.Items.Clear();

        //    foreach (KeyValuePair<string, Zone.GeoCoord> pair in markerPoints)
        //    {
        //        ListViewItem list;
        //        list = listViewPoints.Items.Add(pair.Key);
        //        list.SubItems.Add(pair.Value.latitude);
        //        list.SubItems.Add(pair.Value.longitude);
        //    }
        //    this.listViewPoints.MultiSelect = true;
        //    this.listViewPoints.HideSelection = false;
        //    this.listViewPoints.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        //}

        private void btnDeletePoint_Click(object sender, EventArgs e)
        {
            //string idPunto = "P" + actualPointID.ToString();
            ////object[] args2 = { idPunto };
            ////string res = (string)webBrowser2.Document.InvokeScript("borrarMarker", args2);
            ////Form.ActiveForm.Text = res;
            //markerPoints.Remove(idPunto);
            //actualizarListaPuntos();
            //actualizarMarkersEnMapa(markerPoints);
            //webBrowser.Document.InvokeScript("noResaltarPunto");

            //crearZonaPorMarkers();
            //ZoomToFit();

        }

        //private void listViewPoints_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //ListView.SelectedListViewItemCollection itemCollection = listViewPoints.SelectedItems;

        //    //if (itemCollection.Count > 0)
        //    //{
        //    //    ListViewItem item = itemCollection[0];

        //    //    actualPointID = int.Parse(item.SubItems[0].Text.Substring(1));

        //    //    string lat = item.SubItems[1].Text;
        //    //    string lng = item.SubItems[2].Text;

        //    //    string jsArray = lat + "," + lng;           // Serializa los datos..

        //    //    object[] args2 = { jsArray };

        //    //    string ret = (string)webBrowser.Document.InvokeScript("resaltarPunto", args2);

                
        //    //}
        //}

        private void frmZoneDefinition_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (actualZone != null)
            {
                if (actualZone.listaPuertas != null)
                {
                    if (markerPoints != null)
                    {
                        if (markerPoints.Count >= 2)
                        {
                            sendZoneToServer();
                             Tools.GetInstance().DoLog("Zona enviada al server...");
                            

                            sendAllGatesToServer();
                            Tools.GetInstance().DoLog("Todas las zonas enviadas al server...");
                        }
                    }
                }
            }

            this.Close();
        }


        /// <summary>
        /// Envia todas las definiciones de gates al server.
        /// </summary>
        private void sendAllGatesToServer()
        {
            foreach (string nombre in actualZone.listaPuertas.Keys)
            {
                sendGateToServer(nombre);
                Thread.Sleep(100);          // para no saturar...
            }
        }

        /// <summary>
        /// Envia la definicion de un segmento al sever
        /// </summary>
        /// <param name="v_gate"></param>
        private void sendGateToServer(string v_gate)
        {
            try
            {
                Zone.GateAccessType tipoPuerta = actualZone.listaPuertas[v_gate].type;

                string Ordinal1 = actualZone.listaPuertas[v_gate].from.Ordinal.ToString();
                string Ordinal2 = actualZone.listaPuertas[v_gate].to.Ordinal.ToString();
                int lnlEntranceReaderID = actualZone.listaPuertas[v_gate].LNLEntranceReaderID;
                int lnlExitReaderID = actualZone.listaPuertas[v_gate].LNLExitReaderID;

                string errDesc = "";
                int errCode = -1;

                WebServiceAPI.GetInstance().DefineGate(DEVICEID, lnlEntranceReaderID.ToString(), lnlExitReaderID.ToString(), ORGANIZATIONID.ToString(), tipoPuerta.ToString(), Ordinal1, Ordinal2, out errDesc, out errCode);
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en sendGateToServer: " + ex.Message);
            }
        }

        private void sendZoneToServer()
        {
            string listaPuntos = "";
            foreach (KeyValuePair<string, Zone.GeoCoord> pair in markerPoints)
            {
                listaPuntos = listaPuntos + pair.Value.latitude + "," + pair.Value.longitude + ",";
            }

            string modeStr = chkTrigger.Checked ? "1" : "0";

            int errCode = -1;
            string errDesc = "";
            WebServiceAPI.GetInstance().DefineZone(DEVICEID, modeStr, listaPuntos, ORGANIZATIONID.ToString(), out errDesc, out errCode);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (zonaDef != "")
            {
                crearZonaPordef(zonaDef);
            }
        }
        
        // Utiliza el diccionario de markerPoints para crear la Zona
        // El orden de creacion de las gates se corresponde al orden de los markerPoints por su clave(string)
        private void crearZonaPorMarkers()
        {
            actualZone = new Zone(Tools.GetInstance().ZoneName);
            Zone.GateDefinition nuevaGate = new Zone.GateDefinition();
            string firstLat = "";
            string firstLong = "";
            string firstID = "";
            bool primerPunto = true;
            int ordPunto = 1;            // Lista de Ordinales
            foreach (KeyValuePair<string, Zone.GeoCoord> pair in markerPoints)
            {
                if (primerPunto)
                {
                    nuevaGate.from = new Zone.ZonePoint("Desde", pair.Value.latitude, pair.Value.longitude, ordPunto);
                    nuevaGate.type = Zone.GateAccessType.Forbidden;
                    //nuevaGate.ID = pair.Key;
                    firstLat = pair.Value.latitude;
                    firstLong = pair.Value.longitude;
                    firstID = pair.Key;

                    ordPunto++;
                    primerPunto = false;
                }
                else
                {
                    nuevaGate.to = new Zone.ZonePoint("Hacia", pair.Value.latitude, pair.Value.longitude,ordPunto);
                    nuevaGate.ID = Tools.GetInstance().ZoneName + "- Gate " + (actualZone.listaPuertas.Count + 1).ToString();
                    actualZone.listaPuertas.Add(nuevaGate.ID, nuevaGate);

                    nuevaGate = new Zone.GateDefinition();
                    nuevaGate.from = new Zone.ZonePoint("Desde", pair.Value.latitude, pair.Value.longitude, ordPunto);
                    nuevaGate.type = Zone.GateAccessType.Forbidden;
                    nuevaGate.ID = pair.Key;
                    ordPunto++;
                }
            }

            // La ultima puerta se genera entre el ultimo punto y el primero.
            nuevaGate.to = nuevaGate.to = new Zone.ZonePoint("Hacia", firstLat, firstLong,1);   // El ordinal del primer punto es 1
            nuevaGate.ID = Tools.GetInstance().ZoneName +"- Gate " + (actualZone.listaPuertas.Count + 1).ToString();

            actualZone.listaPuertas.Add(nuevaGate.ID, nuevaGate);

            actualizarZonaEnMapa();
            actualizarListViewGates();

        }

        //strZona: Lista de: Lat1,Long1,ORD1,Lat2,Long2,ORD2,TIPOACCESO, LNLEntranceReaderID, LNLExitReaderID: 9 items
        private void crearZonaPordef(string v_strZona)
        {
            actualZone = new Zone(Tools.GetInstance().ZoneName);
            actualPointID = 0;
            markerPoints = new Dictionary<string, Zone.GeoCoord>();


            string[] listaDatos = v_strZona.Split(',');
            //MessageBox.Show(v_strZona + ", listaDatos es de largo: " + listaDatos.Length.ToString());
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

                    //nuevaGate.type = getAccessType(gateType);

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

                }
                Tools.GetInstance().DoLog("Zona definida: " + actualZone.IDZona + " con: " + actualZone.listaPuertas.Count + " gates");
            }
            actualizarZonaEnMapa();
            actualizarListViewGates();

            Tools.GetInstance().DoLog("Zona actualizada en el mapa");

        }


        private Zone.GateAccessType getAccessType(string v_accesstypeChar)
        {
            switch (v_accesstypeChar)
            {
                case "E":
                    return Zone.GateAccessType.Entrance;
                case "X":
                    return Zone.GateAccessType.Exit;
                case "G":
                    return Zone.GateAccessType.Granted;
                case "F":
                    return Zone.GateAccessType.Forbidden;
                default:
                    return Zone.GateAccessType.Forbidden;
            }

                
        }

        private void addPointToMarkers(Zone.ZonePoint v_ZP)
        {
            actualPointID++;

            string idNuevoPunto = "P" + actualPointID.ToString();

            markerPoints.Add(idNuevoPunto, v_ZP.position);

            // Actualiza el ListView de los puntos de la zona.
            //actualizarListaPuntos();
            actualizarMarkersEnMapa(markerPoints);
        }

        private void  actualizarListViewGateUnValor(string segment, string access, int LnlEntranceReaderID, int LNLExitReaderID)
        {

            string entranceDesc = "None";
            string exitDesc = "None";
            if (LnlEntranceReaderID > 0)
                entranceDesc = listaReadersDescriptions[LnlEntranceReaderID].Value;
            if (LNLExitReaderID>0)
                exitDesc = listaReadersDescriptions[LNLExitReaderID].Value;

            foreach (ListViewItem item in listViewGates.Items)
            {
                if (item.Text == segment)
                {
                    item.SubItems[1].Text = access;
                    item.SubItems[2].Text = entranceDesc;
                    item.SubItems[3].Text = exitDesc;
                }
            }
        }
        private void actualizarListViewGates()
        {
          
            listViewGates.Items.Clear();
           
            Zone zona = actualZone;

            /*
            listViewPersonas.Columns.Add("Employee", (int)((listViewWidth - 20)*0.5f), HorizontalAlignment.Left);
            listViewPersonas.Columns.Add("Badge", (int)((listViewWidth - 20) * 0.2f), HorizontalAlignment.Left);
            listViewPersonas.Columns.Add("Entrance Date", (int)((listViewWidth - 20) * 0.3f), HorizontalAlignment.Left);
            listViewPersonas.OwnerDraw = true;*/

            int listViewWidth = listViewGates.Size.Width;

            listViewGates.View = View.Details;
            listViewGates.GridLines = true;
            listViewGates.Columns.Clear();
            //listViewGates.Columns.Add("Segment", 120, HorizontalAlignment.Left);
            //listViewGates.Columns.Add("Type", 75, HorizontalAlignment.Left);
            //listViewGates.Columns.Add("Entrance Reader", 120, HorizontalAlignment.Left);
            //listViewGates.Columns.Add("Exit Reader", 120, HorizontalAlignment.Left);

            listViewGates.Columns.Add("Segment", (int)((listViewWidth - 20) * 0.28f), HorizontalAlignment.Left);
            listViewGates.Columns.Add("Type", (int)((listViewWidth - 20) * 0.2f), HorizontalAlignment.Left);
            listViewGates.Columns.Add("Entrance Reader", (int)((listViewWidth - 20) * 0.26f), HorizontalAlignment.Left);
            listViewGates.Columns.Add("Exit Reader", (int)((listViewWidth - 20) * 0.26f), HorizontalAlignment.Left);


            listViewGates.Items.Clear();

            foreach (KeyValuePair<string, Zone.GateDefinition> puerta in zona.listaPuertas)
            {
                ListViewItem list;
                list = listViewGates.Items.Add(puerta.Value.ID);
                list.SubItems.Add(puerta.Value.type.ToString());
                if (puerta.Value.LNLEntranceReaderID > 0)
                {
                    //MessageBox.Show("Cantidad de readers: " + listaReadersDescriptions.Count.ToString());
                    //MessageBox.Show("puerta.Value.LNLEntranceReaderID: " + puerta.Value.LNLEntranceReaderID.ToString());
                    //MessageBox.Show("listaReadersDescriptions[puerta.Value.LNLEntranceReaderID]: " + listaReadersDescriptions[puerta.Value.LNLEntranceReaderID].ToString());
                    list.SubItems.Add(listaReadersDescriptions[puerta.Value.LNLEntranceReaderID].Value);
                }
                else
                    list.SubItems.Add("None");
                if(puerta.Value.LNLExitReaderID>0)
                    list.SubItems.Add(listaReadersDescriptions[puerta.Value.LNLExitReaderID].Value);
                else
                    list.SubItems.Add("None");
            }

            this.listViewGates.MultiSelect = true;
            this.listViewGates.HideSelection = false;
            this.listViewGates.HeaderStyle = ColumnHeaderStyle.Nonclickable;

           
        }

        /// <summary>
        /// Usa los datos de la ActualZone para resetear el mapa y dibujar la zona.
        /// </summary>
        private void actualizarZonaEnMapa()
        {
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
                string ret = (string)webBrowser.Document.InvokeScript("verZonas", args);
            }
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
                    res =  Zone.GateColorAccessForbidden;
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

        private void listViewGates_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbEntrance.Tag = null;         // Para que no se haga nada al disparar el evento selectedIndexChange
            cmbExit.Tag = null;             // Para que no se haga nada al disparar el evento selectedIndexChange

            ListView.SelectedListViewItemCollection itemCollection = listViewGates.SelectedItems;
            if (itemCollection.Count > 0)
            {
                ListViewItem item = itemCollection[0];
                actualGate = item.SubItems[0].Text;
               
                Zone.GateDefinition gate = actualZone.getGate(actualGate);
                if (gate != null)
                {
                    int EntranceReaderID = gate.LNLEntranceReaderID;
                    int ExitReaderID = gate.LNLExitReaderID;

                    if (cmbEntrance.Items.Count > 0)
                    {
                        if (EntranceReaderID == -1)
                        {
                            cmbEntrance.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbEntrance.SelectedItem = buscarReaderDescFromReaderID(EntranceReaderID);
                        }
                    }

                    if (cmbExit.Items.Count > 0)
                    {
                        if (ExitReaderID == -1)
                        {
                            cmbExit.SelectedIndex = 0;
                        }
                        else
                        {
                            cmbExit.SelectedItem = buscarReaderDescFromReaderID(ExitReaderID);
                        }
                    }
                }
                
                cmbEntrance.Enabled = true;
                cmbExit.Enabled = true;

                actualizarGateEnMapa(actualGate);
            }
            else
            {
                actualGate = "";
                cmbEntrance.Enabled = false;
                cmbExit.Enabled = false;
            }

            cmbEntrance.Tag = true;
            cmbExit.Tag = true;
        }

        private void actualizarGateEnMapa( string v_gate)
        {
         
            Zone.GateDefinition selectedGate =actualZone.listaPuertas[v_gate];

            Dictionary<string, Zone.GeoCoord> pointsInGate = new Dictionary<string, Zone.GeoCoord>();
            pointsInGate.Add(selectedGate.ID + "1", new Zone.GeoCoord(selectedGate.from.position.latitude, selectedGate.from.position.longitude));
            pointsInGate.Add(selectedGate.ID + "2", new Zone.GeoCoord(selectedGate.to.position.latitude, selectedGate.to.position.longitude));
            actualizarMarkersEnMapa(pointsInGate);
            
        }

        private void btnZoomMas_Click(object sender, EventArgs e)
        {
            object[] args = { DELTAZOOM };

            //// Centrar el mapa
            webBrowser.Document.InvokeScript("ZoomIn", args);
        }

        private void btnZoomMenos_Click(object sender, EventArgs e)
        {
            object[] args = { DELTAZOOM };

            //// Centrar el mapa
            webBrowser.Document.InvokeScript("ZoomOut", args);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            object[] args = {  };
            //// Centrar el mapa
            string res = webBrowser.Document.InvokeScript("obtenerMarkers", args).ToString();

            res = res.TrimEnd(',');
            string[] puntos = res.Split(',');

            if (puntos.Length < 6)
            {
                MessageBox.Show("You must define at least 3 points");
                return;
            }
            crearMarkers(res);
            
            crearZonaPorMarkers();

            // Esto es para desactivar el agregado de puntos
            flagAddmarker = false;
            btnAddPoints.BackColor = Color.LightGray;
            btnCreate.Visible = false;
            btnAddPoints.Enabled = true;
            btnCancelNew.Text = "Revert";

            ZoomToFit();
            webBrowser.Document.InvokeScript("flagAgregarFalse");


        }

        private void tmrLoadZone_Tick(object sender, EventArgs e)
        {
            tmrLoadZone.Enabled = false;
            if (zonaDef != "")
            {
                chkTrigger.Checked = (triggerMode == 1);

                crearZonaPordef(zonaDef);
                if (markerPoints.Count > 2)
                {
                        ZoomToFit();
                }
            }
            else
            {
                string jsArray = "-31,-54,4";           // Posicion y Zoom por defecto
                object[] args2 = { jsArray };
                    webBrowser.Document.InvokeScript("setPos", args2);
            }
        }

        /// <summary>
        /// Devuelve la cantidad de VGates con readers asociados: cuantan para la licencia
        /// </summary>
        /// <returns></returns>
        private int  contarVGates()
        {
            int c = 0;
            try
            {
                foreach (KeyValuePair<string, Zone.GateDefinition> d in actualZone.listaPuertas)
                {
                    if (d.Value.type != Zone.GateAccessType.Forbidden)
                        c++;
                }
            }
            catch (Exception) { }

            return c;
        }

        // Ubica el mapa dentro de la zona definida por los markers.
        private void  ZoomToFit()
        {
            string res = (string)webBrowser.Document.InvokeScript("zoomToFitZone");

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            deleteAllMarkers();
            actualZone.listaPuertas.Clear();
            actualizarListViewGates();
            webBrowser.Document.InvokeScript("borrarZonas");
        }

        private void btnZoomToFit_Click(object sender, EventArgs e)
        {
            ZoomToFit();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            object[] args = { 1 };

            //// Centrar el mapa
            webBrowser.Document.InvokeScript("zoomMapa", args);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object[] args = { 5 };

            //// Centrar el mapa
            webBrowser.Document.InvokeScript("zoomMapa", args);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            object[] args = { 10 };

            //// Centrar el mapa
            webBrowser.Document.InvokeScript("zoomMapa", args);
        }

        private void frmZoneDefinition_Activated(object sender, EventArgs e)
        {
            lblZoneName.Text = ZoneName;
        }

        private void cmbEntrance_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEntrance.Tag == null)
                return;
            int index = cmbEntrance.SelectedIndex;
            if (index >= 0)
            {
                string idVGate = cmbEntrance.Items[index].ToString();
                if (!String.IsNullOrEmpty(idVGate)&& !String.IsNullOrEmpty(actualGate))
                {
                    int ReaderID = buscarReaderIDFromReaderDescription(idVGate);

                    if (index > 0)
                    {
                        if (actualZone.listaPuertas[actualGate].LNLExitReaderID == -1) 
                        {
                            actualZone.listaPuertas[actualGate].LNLEntranceReaderID = ReaderID;
                        }
                        else
                            actualZone.listaPuertas[actualGate].LNLEntranceReaderID = ReaderID;
                    }
                    else
                    {
                        actualZone.listaPuertas[actualGate].LNLEntranceReaderID = -1;      // -1 es NONE
                        cmbEntrance.SelectedIndex = 0;
                    }

                    actualizeAccessType(actualZone.listaPuertas[actualGate]);

                    actualizarListViewGateUnValor(actualGate, actualZone.listaPuertas[actualGate].type.ToString(), actualZone.listaPuertas[actualGate].LNLEntranceReaderID, actualZone.listaPuertas[actualGate].LNLExitReaderID);
                    actualizarZonaEnMapa();
                }
            }
        }

        /// <summary>
        /// True si quedan licencias de VG disponibles.
        /// </summary>
        /// <returns></returns>
        //private bool checkAvailability()
        //{
        //    int VGDefinidas = contarVGates();
        //    bool res = (StaticCustomOptionsManager.ActualVG - cantVGatesOriginalesZona + VGDefinidas) < StaticCustomOptionsManager.CantVG;
        //    return res;
        //}

        private void actualizeAccessType(Zone.GateDefinition v_gate)
        {
            if ((v_gate.LNLEntranceReaderID > 0) && (v_gate.LNLExitReaderID > 0))
                v_gate.type = Zone.GateAccessType.Granted;

            if ((v_gate.LNLEntranceReaderID > 0) && (v_gate.LNLExitReaderID < 0))
                v_gate.type = Zone.GateAccessType.Entrance;

            if ((v_gate.LNLEntranceReaderID < 0) && (v_gate.LNLExitReaderID > 0))
                v_gate.type = Zone.GateAccessType.Exit;

            if ((v_gate.LNLEntranceReaderID < 0) && (v_gate.LNLExitReaderID < 0))
                v_gate.type = Zone.GateAccessType.Forbidden;

        }
        private string buscarReaderDescFromReaderID(int v_ID)
        {
            string res = "";
            foreach (KeyValuePair<int, KeyValuePair<Zone.GateAccessType, string>> par in listaReadersDescriptions)
            {
                if (par.Key == v_ID)
                {
                    res = par.Value.Value;
                    break;
                }
            }
            return res;
        }

        private int buscarReaderIDFromReaderDescription(string v_readerDesc)
        {
            int res = -1;

            foreach (KeyValuePair<int, KeyValuePair<Zone.GateAccessType,string>> par in listaReadersDescriptions)
            {
                if (par.Value.Value == v_readerDesc)
                {
                    res = par.Key;
                    break;
                }
            }

            return res;
        }

        private void cmbExit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbExit.Tag == null)
                return;
            int index = cmbExit.SelectedIndex;
            if (index >= 0)
            {
                string idVGate = cmbExit.Items[index].ToString();
                if (!String.IsNullOrEmpty(idVGate) && !String.IsNullOrEmpty(actualGate))
                {
                    int ReaderID = buscarReaderIDFromReaderDescription(idVGate);
                    if (index > 0)
                    {
                        if (actualZone.listaPuertas[actualGate].LNLEntranceReaderID == -1)
                        {
                            actualZone.listaPuertas[actualGate].LNLExitReaderID = ReaderID;
                        }
                        else
                            actualZone.listaPuertas[actualGate].LNLExitReaderID = ReaderID;
                    }
                    else
                    {
                        actualZone.listaPuertas[actualGate].LNLExitReaderID = -1;      // -1 es NONE
                        cmbExit.SelectedIndex = 0;
                    }

                    actualizeAccessType(actualZone.listaPuertas[actualGate]);

                    actualizarListViewGateUnValor(actualGate, actualZone.listaPuertas[actualGate].type.ToString(), actualZone.listaPuertas[actualGate].LNLEntranceReaderID, actualZone.listaPuertas[actualGate].LNLExitReaderID);
                    actualizarZonaEnMapa();
                }
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            crearZonaPordef(zonaDef);
        }

        private void lblZoneName_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tmrLoadZone.Enabled = true;
        }

        private void btnCancelNew_Click(object sender, EventArgs e)
        {
            flagAddmarker = false;
            btnAddPoints.BackColor = Color.LightGray;
            btnCreate.Visible = false;
            btnAddPoints.Enabled = true;
            webBrowser.Document.InvokeScript("flagAgregarFalse");


            if (actualZone != null)
            {
                actualZone.listaPuertas.Clear();
                actualizarListViewGates();
            }
            cmbEntrance.Items.Clear();
            cmbExit.Items.Clear();
            cmbEntrance.Enabled = false;
            cmbExit.Enabled = false;

            frmZoneDefinition_Load(sender, e);
            btnCancelNew.Visible = false;

        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            
        }

        private void btnGeoCodeSearch_click(object sender, EventArgs e)
        {
            string latitud = "";
            string longitud = "";

            string errDesc = "";
            int errCode = 1;
            string posDesc = txtPosition.Text;

            if (!String.IsNullOrEmpty(posDesc.Trim()))
            {
                WebServiceAPI.GetInstance().obtenerPosicion(posDesc, out latitud, out longitud, out errDesc, out errCode);

                if (!String.IsNullOrEmpty(latitud) && !String.IsNullOrEmpty(longitud))
                {
                    string jsArray = latitud + "," + longitud + ",15";                     // Posicion y Zoom por defecto
                    object[] args2 = { jsArray };

                    webBrowser.Document.InvokeScript("setPos", args2);
                }
                else
                    MessageBox.Show("Could not get the coordinates of " + posDesc);
            }
            else
                MessageBox.Show("You have to describe a place using the following syntax: City, Country");
           

        }

        private void txtPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGeoCodeSearch_click(sender, e);
            }
        }
    }
}
