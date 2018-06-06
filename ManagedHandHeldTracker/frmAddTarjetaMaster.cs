using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManagedHandHeldTracker
{
    public partial class frmAddTarjetaMaster : Form
    {
        public List<TiposAcceso> tAccesos;
        public Dictionary<int, string> virtualZones;
        ListView listViewMaster;
        public Dictionary<int, string> listaVirtualZones;

        public frmAddTarjetaMaster(ListView v_listView, Dictionary<int, string> virtualZones)
        {
            listViewMaster = v_listView;
            listaVirtualZones = virtualZones;

            InitializeComponent();
            cargarZoneNames();

        }
        private void cargarZoneNames()
        {
            cmbVZone.Items.Clear();

            foreach (string s in listaVirtualZones.Values)
            {
                cmbVZone.Items.Add(s);
            }
        }

        private void frmAddTarjetaMaster_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            long tarjeta;                       // Soporta tarjetas de hasta 18 digitos
            bool check = false;
            if (txtBadge.Visible)
            {
                if (long.TryParse(txtBadge.Text, out tarjeta))
                    if ((rdbEntrada.Checked || rdbSalida.Checked) && !String.IsNullOrEmpty(cmbVZone.Text))
                        check = true;

            }
            else
                check = true;

            if (check)
            {
                bool yaEsta = false;
                foreach (ListViewItem l in listViewMaster.Items)
                {
                    if (txtBadge.Text == l.Text)
                        yaEsta = true;
                }

                if (yaEsta)
                    MessageBox.Show(txtBadge.Text + " already in the collection", "Error");
                else
                {
                    this.Tag = true.ToString();
                    this.Close();
                }
            }
            else
                MessageBox.Show("Invalid definition", "Error");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Tag = false.ToString();
            this.Close();
        }
    }
}
