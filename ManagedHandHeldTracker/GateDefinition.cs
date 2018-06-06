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

namespace ManagedHandHeldTracker
{
    public partial class frmGateDefinition : Form
    {

        public string IP;
        /// <summary>
        /// Puerto para aceptar las conexiones y leer el primer header.
        /// </summary>
        public int PORT;
        /// <summary>
        /// Después de aceptar las conexiones y leer el primer header usando el puerto PORT, 
        /// todas los demás headers son leidos a través de este puerto.
        /// </summary>
        public int PORT2;
        public string PANELID;
        public string READERID;
        public int ORGANIZATIONID;

        public const int DELTAZOOM = 1;

        string zonaDef = "";        // ARRAY DE PUNTOS DE DEFINICION DE ZONA RECIBIDO DESDE EL SERVER

        string ZoneName = "";
        string actualGate = "";     // Variable temporal para la seleccion de la gate.
        Zone actualZone;

        StreamWriter writerFile;


        int actualPointID = 0;

        // Diccionario temporal de puntos (nombre y ubicacion) usados para definir una zona.
        private Dictionary<string, Zone.GeoCoord> markerPoints = new Dictionary<string, Zone.GeoCoord>();




        public frmGateDefinition()
        {
            InitializeComponent();
        }

        private void GateDefinition_Load(object sender, EventArgs e)
        {
            string personalFolder = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            writerFile = new StreamWriter(@personalFolder + @"\\logGateDefinition.txt");
            writerFile.AutoFlush = true;

            string datosMapa = "";
            string datosZona = "";

            StaticCustomOptionsManager.cargarMapaYDatosZonas(PANELID.ToString(), ORGANIZATIONID.ToString(), ref  datosMapa, ref datosZona);
            zonaDef = datosZona;

            webBrowser.DocumentText = datosMapa;

        }


        private void DoLog(string texto)
        {
            writerFile.WriteLine(texto);
        } 

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tmrLoadZone.Enabled = true;
            tmrLoadZone.Interval = 2000;
        }

        private void tmrLoadZone_Tick(object sender, EventArgs e)
        {
            tmrLoadZone.Enabled = false;
            if (zonaDef != "")
            {
               // MessageBox.Show("Va a crear la zona");
                crearZonaPordef(zonaDef);
                //MessageBox.Show("CREO la zona");
                if (READERID != "")
                {
                    Zone.GateDefinition actualGate = obtenerGatedesdeLNLReaderID(int.Parse(READERID));
                    if (actualGate != null)
                    {
                        markerPoints.Clear();
                        addPointToMarkers(actualGate.from);
                        addPointToMarkers(actualGate.to);
                       // MessageBox.Show(actualGate.ID.ToString());
                        listViewGates.Focus();
                        listViewGates.Items[actualGate.index].Selected = true;
                    }
                }
                ZoomToFit(true);
            }
           
        }

        // Ubica el mapa dentro de la zona definida por los markers.
        private void ZoomToFit(bool v_doIt)
        {
            if (v_doIt)
            {
                string res = (string)webBrowser.Document.InvokeScript("zoomToFitMarkers");
            }
        }
        private void crearZonaPordef(string v_strZona)
        {
            actualZone = new Zone("NewZone");
            actualPointID = 0;
            markerPoints = new Dictionary<string, Zone.GeoCoord>();

            string[] listaDatos = v_strZona.Split(',');
            for (int i = 0; i < listaDatos.Length; i = i + 8)
            {
                Zone.ZonePoint desde = new Zone.ZonePoint("F" + (1 + (i / 8)).ToString(), listaDatos[i], listaDatos[i + 1], int.Parse(listaDatos[i + 2]));
                Zone.ZonePoint hacia = new Zone.ZonePoint("T" + (1 + (i / 8)).ToString(), listaDatos[i + 3], listaDatos[i + 4], int.Parse(listaDatos[i + 5]));

                string gateType = listaDatos[i + 6];
                string LNLReaderID = listaDatos[i + 7];
                Zone.GateDefinition nuevaGate = new Zone.GateDefinition();

                nuevaGate.from = desde;
                nuevaGate.to = hacia;

                nuevaGate.type = (Zone.GateAccessType)Enum.Parse(typeof(Zone.GateAccessType), gateType);

                nuevaGate.ID = "Gate" + (1 + (i / 8)).ToString();
                if (LNLReaderID != "")
                {
                    nuevaGate.LNLEntranceReaderID = int.Parse(LNLReaderID);
                }
               
                actualZone.addGate(nuevaGate.ID, nuevaGate);

                // Ahora actualizo los markers. 
                //Como las puertas estan conectadas, la lista de markers es solo con el punto de salida.
                //addPointToMarkers(desde);
            }
            DoLog("Zona definida: " + actualZone.IDZona + " con: " + actualZone.listaPuertas.Count + " gates");

            actualizarZonaEnMapa();
            actualizarListViewGates();

            DoLog("Zona actualizada en el mapa");
        }

        public Zone.GateDefinition obtenerGatedesdeLNLReaderID(int v_LNLReaderID)
        {
            Zone.GateDefinition res= null;


            foreach (KeyValuePair<string, Zone.GateDefinition> pair in actualZone.listaPuertas)
            {
                if (pair.Value.LNLEntranceReaderID == v_LNLReaderID)
                {
                    res = pair.Value;
                    break;
                }
            }

            return res;
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
       
        private void actualizarMarkersEnMapa(Dictionary<string, Zone.GeoCoord> markers)
        {
            string coordArray = "";

            foreach (KeyValuePair<string, Zone.GeoCoord> par in markers)
            {
                coordArray = coordArray + par.Value.latitude + "," + par.Value.longitude + ",";
            }
            object[] args = { coordArray };

            string ret = (string)webBrowser.Document.InvokeScript("verMarkers", args);
        }
        private void actualizarListViewGates()
        {

            listViewGates.Items.Clear();

            Zone zona = actualZone;

            listViewGates.View = View.Details;
            listViewGates.GridLines = true;
            listViewGates.Columns.Clear();
            listViewGates.Columns.Add("Gate definition", 120, HorizontalAlignment.Left);
            listViewGates.Columns.Add("Access", 60, HorizontalAlignment.Left);
            listViewGates.Items.Clear();

            int index = 0;
            foreach (KeyValuePair<string, Zone.GateDefinition> puerta in zona.listaPuertas)
            {
                ListViewItem list;
                list = listViewGates.Items.Add(puerta.Value.ID);
                list.SubItems.Add(puerta.Value.type.ToString());
                puerta.Value.index = index;
                index++;
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

            object[] args2 = { latGate, longGate };

            //// Centrar el mapa
            webBrowser.Document.InvokeScript("centrarMapa", args2);
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

        private void listViewGates_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itemCollection = listViewGates.SelectedItems;
            if (itemCollection.Count > 0)
            {
                ListViewItem item = itemCollection[0];
                actualGate = item.SubItems[0].Text;

                actualizarGateEnMapa(actualGate);
            }
        }

        private void actualizarGateEnMapa(string v_gate)
        {

            Zone.GateDefinition selectedGate = actualZone.listaPuertas[v_gate];

            markerPoints.Clear();
            addPointToMarkers(selectedGate.from);
            addPointToMarkers(selectedGate.to);
            ZoomToFit(true);
         
           // Dictionary<string, Zone.GeoCoord> pointsInGate = new Dictionary<string, Zone.GeoCoord>();
           // pointsInGate.Add(selectedGate.ID + "1", new Zone.GeoCoord(selectedGate.from.position.latitude, selectedGate.from.position.longitude));
           // pointsInGate.Add(selectedGate.ID + "2", new Zone.GeoCoord(selectedGate.to.position.latitude, selectedGate.to.position.longitude));
           // actualizarMarkersEnMapa(pointsInGate);

       

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            sendGateToServer();
            DoLog("Enviado al server...");
            this.Close();
        }

        private void frmGateDefinition_FormClosing(object sender, FormClosingEventArgs e)
        {
            writerFile.Close();
        }

        private void btnZoomToFit_Click(object sender, EventArgs e)
        {
            ZoomToFit(true);
        }

        private void btnZoomMas_Click(object sender, EventArgs e)
        {
            // LLama al javascript() para dibujar las zonas en el mapa.
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

        private void listViewGates_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itemCollection = listViewGates.SelectedItems;
            if (itemCollection.Count > 0)
            {
                ListViewItem item = itemCollection[0];
                actualGate = item.SubItems[0].Text;

                actualizarGateEnMapa(actualGate);
                actualizarRadioButtons(actualGate);
                
                ZoomToFit(!chkLockMap.Checked);
            }
        }


        private void actualizarRadioButtons(string v_actualGate)
        {
            if (actualZone.listaPuertas.ContainsKey(v_actualGate))
            {
                Zone.GateDefinition gate = actualZone.listaPuertas[v_actualGate];

                switch (gate.type)
                {
                    case Zone.GateAccessType.Granted:
                        rdbGranted.Checked = true;
                        break;
                    case Zone.GateAccessType.Forbidden:
                        rdbForbidden.Checked = true;
                        break;
                    case Zone.GateAccessType.Entrance:
                        rdbEntrance.Checked = true;
                        break;
                    case Zone.GateAccessType.Exit:
                        rdbExit.Checked = true;
                        break;
                }

            }
        }
        private void allForbidden()
        {
            foreach (KeyValuePair<string,Zone.GateDefinition> puerta in actualZone.listaPuertas)
            {
                puerta.Value.type = Zone.GateAccessType.Forbidden;
            }

        }

        private void rdbEntrance_CheckedChanged(object sender, EventArgs e)
        {
            //allForbidden();
            if (actualZone.listaPuertas.ContainsKey(actualGate))
            {
                actualZone.listaPuertas[actualGate].type = Zone.GateAccessType.Entrance;
                actualizarZonaEnMapa();
            }
        }

        private void rdbGranted_CheckedChanged(object sender, EventArgs e)
        {
           // allForbidden();
            if (actualZone.listaPuertas.ContainsKey(actualGate))
            {
                actualZone.listaPuertas[actualGate].type = Zone.GateAccessType.Granted;
                actualizarZonaEnMapa();
            }
        }

        private void rdbExit_CheckedChanged(object sender, EventArgs e)
        {
            //allForbidden();
            if (actualZone.listaPuertas.ContainsKey(actualGate))
            {
                actualZone.listaPuertas[actualGate].type = Zone.GateAccessType.Exit;
                actualizarZonaEnMapa();
            }
        }

        private void rdbForbidden_CheckedChanged(object sender, EventArgs e)
        {
           // allForbidden();
            if (actualZone.listaPuertas.ContainsKey(actualGate))
            {
                actualZone.listaPuertas[actualGate].type = Zone.GateAccessType.Forbidden;
                actualizarZonaEnMapa();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sendGateToServer()
        {

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            TcpClient cliente = new TcpClient(IP, PORT);

            NetworkStream stream = cliente.GetStream();

            foreach (KeyValuePair<string, Zone.GateDefinition> puerta in actualZone.listaPuertas)
            {
                if (puerta.Value.type != Zone.GateAccessType.Forbidden)
                {
                    actualGate = puerta.Key;
                    break;
                }
            }

            Zone.GateAccessType tipoPuerta = actualZone.listaPuertas[actualGate].type;

            string Ordinal1 = actualZone.listaPuertas[actualGate].from.Ordinal.ToString();
            string Ordinal2 = actualZone.listaPuertas[actualGate].to.Ordinal.ToString();

            string comando = "TYPE:LNL_DEFINEGATE,DEVICEID:" + PANELID + ",READERID:" + READERID+",ORGANIZATION:" + ORGANIZATIONID.ToString() + ",ACCESSTYPE:" + tipoPuerta.ToString()+",ORD1:"+Ordinal1+",ORD2:"+Ordinal2;

            byte[] arrayBytesComando = encoding.GetBytes(comando);
            stream.WriteTimeout = 3000;
            stream.Write(arrayBytesComando, 0, arrayBytesComando.Length);
            stream.Flush();
        }

        private void frmGateDefinition_Activated(object sender, EventArgs e)
        {
            
        }

        private void chkLockMap_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
