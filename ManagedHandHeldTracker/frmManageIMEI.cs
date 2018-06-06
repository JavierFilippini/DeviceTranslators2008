using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
//using System.Threading.Tasks;

namespace ManagedHandHeldTracker
{
    public partial class frmManageIMEI : Form
    {
        public int ORGID;                   // OrgID para identificar los mensajes
        bool isLoaded = false;

        Font fontListView = new Font("Arial", 10);

        List<string> listaIMEI = new List<string>();


        private bool somethingChanged = false;
        public frmManageIMEI()
        {
            InitializeComponent();
        }


        private void btnModifyConfig_Click(object sender, EventArgs e)
        {

        }

        private void frmManageIMEI_Load(object sender, EventArgs e)
        {

        }

        private void frmManageIMEI_Activated(object sender, EventArgs e)
        {
            if (!isLoaded)
            {
                inicializarListViewIMEI();
                Thread t = new Thread(startActualizarListaIMEI);
                t.Start();

                //Task.Factory.StartNew(() => startActualizarListaIMEI());
                isLoaded = true;
            }

            txtFiltro.Select();
            txtFiltro.Focus();
        }

        private void startActualizarListaIMEI()
        {

            listaIMEI = WebServiceAPI.GetInstance().CargarListaIMEI(ORGID);
            if (listaIMEI != null)
            {
                listaIMEI.Sort();
                actualizarListaItems(listaIMEI);
            }
            else
            {
                MessageBox.Show("Server not available. Retry in a few seconds.");
                Invoke((MethodInvoker)delegate
                {
                    this.Close();
                });
            }
        }


        private void actualizarListaItems(List<string> listaItems)
        {
            Invoke((MethodInvoker)delegate
            {
                listViewIMEI.Items.Clear();
            });

            foreach (string IMEI in listaItems)
            {
                Invoke((MethodInvoker)delegate
                {
                    agregarItem(IMEI);
                });
            }
        }


        private void agregarItem(string texto)
        {
            ListViewItem n = new ListViewItem(texto);
            n.Name = texto;

            n.Font = fontListView;
            listViewIMEI.Items.Add(n);
        }


        private void inicializarListViewIMEI()
        {
            listViewIMEI.View = View.Details;    // Tile
            listViewIMEI.GridLines = true;
            listViewIMEI.Columns.Clear();

            int listViewWidth = listViewIMEI.Size.Width;

            listViewIMEI.Columns.Add("IMEI", (int)((listViewWidth - 10) * 0.9f), HorizontalAlignment.Left);

            listViewIMEI.FullRowSelect = true;
            listViewIMEI.MultiSelect = true;
            listViewIMEI.HideSelection = false;
            listViewIMEI.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmUpdateIMEI dialog = new frmUpdateIMEI();
            dialog.frmMain = this;
            dialog.lblTituloUpdate.Text = "Add new IMEI";
            dialog.prevIMEI = "";
            dialog.txtIMEI.Text = "";

            dialog.ShowDialog();
            if ((bool)dialog.Tag ==true)
            {
                agregarItem(dialog.txtIMEI.Text);
                listaIMEI.Add(dialog.txtIMEI.Text);

                listaIMEI.Sort();                           // Ordena toda la lista antes de actualizar el listview
                actualizarListaItems(listaIMEI);

                int indH = listaIMEI.IndexOf(dialog.txtIMEI.Text);  // El index dentro del listview corresponde con el del array

                listViewIMEI.Focus();
                listViewIMEI.Items[indH].Selected = true;
                listViewIMEI.Items[indH].EnsureVisible();
            
                somethingChanged = true;

            }
            dialog.Dispose();
        }


      



        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!somethingChanged) this.Dispose();

            if (updateIMEI())
                this.Dispose();
            else
            {
                MessageBox.Show("Can't update data on server. Please Retry operation", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public bool updateIMEI()
        {
            try
            {
                return WebServiceAPI.GetInstance().UpdateListaIMEI(listaIMEI, Tools.GetInstance().MainOrgID);
            }
            catch (Exception)
            {
                return false;
            }


        }


        public bool ExisteIMEI(string IMEI)
        {
            bool res = false;
            foreach (ListViewItem item in listViewIMEI.Items)
            {
                if (item.Text.ToUpper().Trim().Equals(IMEI.ToUpper().Trim()))
                {
                    res = true;
                    break;
                }
            }

            return res;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int indexEdited = -1;
            frmUpdateIMEI ventanaConfig = new frmUpdateIMEI();
            ventanaConfig.frmMain = this;
            if (listViewIMEI.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Please select one IMEI", "Warning");
                return;
            }
            if (listViewIMEI.SelectedIndices.Count == 1)
            {
                ventanaConfig.lblTituloUpdate.Text = "Modify IMEI";
                ventanaConfig.txtIMEI.Text = listViewIMEI.SelectedItems[0].Text;

                indexEdited = listViewIMEI.SelectedIndices[0];
                ventanaConfig.ShowDialog();
            }

            if ((bool)ventanaConfig.Tag == true)
            {
                int i = listaIMEI.IndexOf(listViewIMEI.Items[indexEdited].Text);
                listViewIMEI.Items[indexEdited].Text = ventanaConfig.txtIMEI.Text;
                listaIMEI[i] = ventanaConfig.txtIMEI.Text;
                somethingChanged = true;
            }

            ventanaConfig.Dispose();
            clearSelected();
        }

        private void clearSelected()
        {
            for (int i = 0; i < listViewIMEI.Items.Count; i++)
            {
                listViewIMEI.Items[i].Selected = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewIMEI.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Please select one IMEI", "Warning");
                return;
            }

            int cantSelected = listViewIMEI.SelectedIndices.Count;

            bool okDelete = false;
            if (cantSelected ==1)
            {
                DialogResult res = MessageBox.Show("Delete IMEI " + listViewIMEI.SelectedItems[0].Text +"?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                   okDelete = true;
            }
            else
            {   DialogResult res = MessageBox.Show("Do you really want to delete " + cantSelected +" items?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                   okDelete = true;
            }

            if (okDelete)
            {
                somethingChanged = true;
                List<ListViewItem> listaItems = new List<ListViewItem>();

                for (int i = 0; i < listViewIMEI.SelectedIndices.Count; i++)
                {
                    ListViewItem item = listViewIMEI.Items[listViewIMEI.SelectedIndices[i]];
                    listaItems.Add(item);
                }

                foreach(ListViewItem i in listaItems)
                {
                    listViewIMEI.Items.Remove(i);
                    listaIMEI.Remove(i.Text);
                }

                clearSelected();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (somethingChanged)
            {
                DialogResult res = MessageBox.Show("You will loose some changes. Are you sure you want to exit?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    this.Tag = false;
                    this.Close();
                }
            }
            else
            {
                this.Tag = false;
                this.Close();
            }
        }

        private void listViewIMEI_DoubleClick(object sender, EventArgs e)
        {
            btnModify_Click(null, null);
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            actualizarListaItems(txtFiltro.Text);
        }

        private void actualizarListaItems(string filtro)
        {
            // Devuelve la lista de nombres que cumplen con el filtro
            List<string> listaFiltro = filtrarNombre(filtro);

            //// Primero saco los que no cumplan 
            List<ListViewItem> aBorrar = new List<ListViewItem>();
            foreach (ListViewItem item in listViewIMEI.Items)
            {
                if (!listaFiltro.Contains(item.Name))
                    aBorrar.Add(item);
            }
            foreach (ListViewItem i in aBorrar)
            {
                i.Remove();
            }

            // Ahora agrego los de la lista que NO esten.
            foreach (string s in listaFiltro)
            {
                if (!listViewIMEI.Items.ContainsKey(s))
                {
                    ListViewItem n = new ListViewItem(s.Split('|')[0]);
                    n.Name = s;

                    Font f = new Font("Arial", 10);

                    n.Font = f;
                    ListViewItem l = listViewIMEI.Items.Add(n);
                }
            }
        }

        private List<string> filtrarNombre(string filtro)
        {
            List<string> res = new List<string>();

            foreach (string s in listaIMEI)
                if (s.ToUpper().IndexOf(filtro.ToUpper()) >= 0)
                    res.Add(s);


            return res;
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
        }

    }
}
