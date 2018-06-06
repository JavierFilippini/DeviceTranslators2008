using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ManagedHandHeldTracker
{
    public partial class frmTarjetasMaster : Form
    {
        public int OrgID;                   // OrgID para identificar los mensajes

        Dictionary<int, string> listaVirtualZones;     // Coleccion de PanelID,VirtualZoneName
        bool somethingChanged = false;

         bool isLoaded = false;

        public frmTarjetasMaster()
        {
           
            InitializeComponent();
        }

        private void frmTarjetasMaster_Load(object sender, EventArgs e)
        {

        }

        private void frmTarjetasMaster_Activated(object sender, EventArgs e)
        {
            cargaInicial();
        }
        private void cargaInicial()
        {
            try
            {
                if (!isLoaded)
                {
                    isLoaded = true;            // esta antes para no lanzar muchas veces el thread: caso de multiples clicks fuera y dentro.
            
                    listViewMasterBadges.View = View.Details;
                    listViewMasterBadges.GridLines = true;
                    listViewMasterBadges.Columns.Clear();
                    listViewMasterBadges.Columns.Add("Badge", 140, HorizontalAlignment.Left);
                    listViewMasterBadges.Columns.Add("Type", 50, HorizontalAlignment.Left);
                    listViewMasterBadges.Columns.Add("Virtual Zone", 90, HorizontalAlignment.Left);

                    this.listViewMasterBadges.MultiSelect = true;
                    this.listViewMasterBadges.HideSelection = false;
                    this.listViewMasterBadges.HeaderStyle = ColumnHeaderStyle.Clickable;
                    
                    Thread t = new Thread(LoadAllData);
                    t.Start();
                }
            }
            catch (Exception ex) 
            {
                Tools.GetInstance().DoLog("Excepcion en cargaInicial" + ex.Message);
            }
        }

        private void LoadAllData()
        {

            listaVirtualZones = WebServiceAPI.GetInstance().ObtenerListaVirtualZones(OrgID);
            if (listaVirtualZones == null)
            {
                Tools.GetInstance().DoLog("ERROR: no pudo obtener la lista de zonas virtuales.");
                listaVirtualZones = new Dictionary<int, string>();
            }
            
            List<TarjetaMaster> listaTarjetas = WebServiceAPI.GetInstance().ObtenerListaTarjetasMaster(OrgID);
            
            if (listaTarjetas!= null)
            {
                try
                {
                    foreach (TarjetaMaster t in listaTarjetas)
                    {
                        ListViewItem list = listViewMasterBadges.Items.Add(t.Numero);
                        list.UseItemStyleForSubItems = false;

                        string NameTipo = (t.Tipo == TiposAcceso.Entrada ? "Entry" : "Exit");

                        list.SubItems.Add(NameTipo);
                        string nombreVZ = listaVirtualZones[t.PanelID];
                        list.SubItems.Add(nombreVZ);
                    }
                }
                catch ( Exception ex)
                {
                    Tools.GetInstance().DoLog("Excepcion en updateListViewTarjetas: " + ex.Message);
                }

            }
            else
            {
                Tools.GetInstance().DoLog("Error al cargar la lista de tarjetas Master");
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            frmAddTarjetaMaster ventanaSetbadge = new frmAddTarjetaMaster(listViewMasterBadges, listaVirtualZones);
            ventanaSetbadge.txtBadge.Visible = true;

            ventanaSetbadge.lblBadge.Visible = false;
            ventanaSetbadge.txtBadge.Text = "";
            ventanaSetbadge.txtBadge.Select();


            ventanaSetbadge.ShowDialog();
            if (ventanaSetbadge.Tag.ToString() == "True")
            {
                ListViewItem list = listViewMasterBadges.Items.Add(ventanaSetbadge.txtBadge.Text);
                list.UseItemStyleForSubItems = false;
                list.SubItems.Add(ventanaSetbadge.rdbEntrada.Checked ? TiposAcceso.Entrada.ToString() : TiposAcceso.Salida.ToString());
                list.SubItems.Add(ventanaSetbadge.cmbVZone.Text);
            }
            ventanaSetbadge.Dispose();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmAddTarjetaMaster ventanaSetbadge = new frmAddTarjetaMaster(listViewMasterBadges, listaVirtualZones);
            if (listViewMasterBadges.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Please select at least one Badge", "Warning");
                return;
            }
            ventanaSetbadge.txtBadge.Visible = false;
            ventanaSetbadge.lblBadge.Visible = true;

            if (listViewMasterBadges.SelectedIndices.Count == 1)
            {
                ventanaSetbadge.lblBadge.Text = listViewMasterBadges.SelectedItems[0].Text;
                if (listViewMasterBadges.SelectedItems[0].SubItems[1].Text == TiposAcceso.Entrada.ToString())
                    ventanaSetbadge.rdbEntrada.Checked = true;
                else
                    ventanaSetbadge.rdbSalida.Checked = true;

                ventanaSetbadge.cmbVZone.Text = listViewMasterBadges.SelectedItems[0].SubItems[2].Text;
            }
            else
            {
                ventanaSetbadge.lblBadge.Text = listViewMasterBadges.SelectedItems.Count + " selected badges";

                // Si todas los Tipos de los seleccionados coinciden, se la precargo a al formulario
                string tipoAccesoRef = listViewMasterBadges.SelectedItems[0].SubItems[1].Text;
                bool cambio = false;
                foreach (ListViewItem l in listViewMasterBadges.SelectedItems)
                {
                    if (!l.SubItems[1].Text.Equals(tipoAccesoRef))
                    {
                        cambio = true;
                        break;
                    }
                }

                if (!cambio)
                {
                    if (tipoAccesoRef == TiposAcceso.Entrada.ToString())
                        ventanaSetbadge.rdbEntrada.Checked = true;
                    else
                        ventanaSetbadge.rdbSalida.Checked = true;
                }
                ventanaSetbadge.cmbVZone.Text = listViewMasterBadges.SelectedItems[0].SubItems[2].Text;
            }
            ventanaSetbadge.ShowDialog();

            if (bool.Parse(ventanaSetbadge.Tag.ToString()))      // Click en OK. Actualizar el listBox.
            {
                foreach (ListViewItem l in listViewMasterBadges.SelectedItems)
                {
                    string tipo = ventanaSetbadge.rdbEntrada.Checked ? TiposAcceso.Entrada.ToString() : TiposAcceso.Salida.ToString();
                    l.SubItems[1].Text = tipo;
                    l.SubItems[2].Text = ventanaSetbadge.cmbVZone.Text;
                }
                somethingChanged = true;
            }
            ventanaSetbadge.Dispose();
        }

        private void listViewMasterBadges_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewMasterBadges_DoubleClick(object sender, EventArgs e)
        {
            btnModify_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!somethingChanged)
                this.Dispose();
            else
            {
                DialogResult res = MessageBox.Show("You will loose all your chages. Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    this.Dispose();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (listViewMasterBadges.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Please select at least one Badge", "Warning");
                return;
            }

            if (listViewMasterBadges.SelectedIndices.Count == 1)
            {
                DialogResult res = MessageBox.Show("You are about to delete the definition of the badge " + listViewMasterBadges.SelectedItems[0].Text + ". Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    borrarListViewSelected(listViewMasterBadges);
                }
            }
            else
            {
                DialogResult res = MessageBox.Show("You are about to delete the definition of " + listViewMasterBadges.SelectedItems.Count.ToString() + " badges. Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    borrarListViewSelected(listViewMasterBadges);
                }
            }
        }
        private void borrarListViewSelected(ListView lview)
        {
            foreach (ListViewItem l in lview.SelectedItems)
            {
                lview.Items.Remove(l);
            }
            somethingChanged = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (updateMasterBadges())
            {
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Database Error. Can't update Master Badges Definition.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private bool updateMasterBadges()
        {
            bool res = false;
            try
            {
                List<TarjetaMaster> nuevasTarjetas = construirListaTarjetasMaster(OrgID);

                
                res = WebServiceAPI.GetInstance().ActualizarTarjetasMaster(nuevasTarjetas,OrgID);

            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en updateMasterBadges(): " + ex.Message);
            }

            return res;
        }

        private List<TarjetaMaster> construirListaTarjetasMaster(int OrgID)
        {
            List<TarjetaMaster> nuevasTarjetas = new List<TarjetaMaster>();
            try
            {
                foreach (ListViewItem item in listViewMasterBadges.Items)
                {
                    TarjetaMaster t = new TarjetaMaster()
                    {
                        Numero = item.Text,
                        Tipo = (TiposAcceso)Enum.Parse(typeof(TiposAcceso), item.SubItems[1].Text),
                        PanelID = listaVirtualZones.FirstOrDefault(x => x.Value == item.SubItems[2].Text).Key

                    };
                    nuevasTarjetas.Add(t);
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en construirListaTarjetasMaster: " + ex.Message);
            }

            return nuevasTarjetas;
        }

    }
}
