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
    public partial class frmManageDeviceUsers : Form
    {
        internal bool isLoaded = false;
        List<empInfo> listaDeviceUsers = new List<empInfo>();
        Font fontListView = new Font("Arial", 10);

        public frmManageDeviceUsers()
        {
            InitializeComponent();
        }

        private void frmManageDeviceUsers_Load(object sender, EventArgs e)
        {

        }

        private void frmManageDeviceUsers_Activated(object sender, EventArgs e)
        {
            if (!isLoaded)
            {
                inicializarListViewDeviceUsers();

                Thread t = new Thread(startActualizarListaDeviceUsers);
                t.Start();

                //Task.Factory.StartNew(() => startActualizarListaDeviceUsers());
                
                isLoaded = true;
            }
           
        }


        private void inicializarListViewDeviceUsers()
        {
            Tools.GetInstance().DoLog("Entró a inicializar ListView.");

            listViewDeviceUsers.View = View.Details;
            listViewDeviceUsers.GridLines = true;
            listViewDeviceUsers.Columns.Clear();
            //listViewDevices.Columns.Add("Type", 70, HorizontalAlignment.Left);
            //listViewDevices.Columns.Add("Name", 125, HorizontalAlignment.Left);
            listViewDeviceUsers.Columns.Add("Badge Number", 140, HorizontalAlignment.Left);
            listViewDeviceUsers.Columns.Add("Name", 70, HorizontalAlignment.Left);
            listViewDeviceUsers.Columns.Add("LastName", 70, HorizontalAlignment.Left);

            this.listViewDeviceUsers.MultiSelect = true;
            this.listViewDeviceUsers.HideSelection = false;
            this.listViewDeviceUsers.HeaderStyle = ColumnHeaderStyle.Clickable;
        }


        internal void startActualizarListaDeviceUsers()
        {
            try
            {
                Tools.GetInstance().DoLog("Entro a StartActualizarListaDeviceUsers");

                listaDeviceUsers = WebServiceAPI.GetInstance().CargarListaDeviceUsers();
                if (listaDeviceUsers != null)
                {
                    EmpInfoComparer comparer = new EmpInfoComparer();
                    listaDeviceUsers.Sort(comparer);
                    actualizarListaDeviceUsers(listaDeviceUsers);
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
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en StartActualizarListaDeviceUsers. Ex: " + ex.ToString());
            }
          
        }

        private void actualizarListaDeviceUsers(List<empInfo> listaDeviceUsers)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    listViewDeviceUsers.Items.Clear();
                });

                Tools.GetInstance().DoLog("La lista de DeviceUsers devuelta por OnGuardAPI dio -> " + listaDeviceUsers.Count);

                foreach (empInfo empInfo in listaDeviceUsers)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        agregarItem(empInfo);
                    });
                }
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en actualizarListaDeviceUsers. Ex: " + ex.ToString());
            }
        }

        private void agregarItem(empInfo empInfo)
        {
            try
            {
                ListViewItem list = listViewDeviceUsers.Items.Add(empInfo.Badge);
                list.UseItemStyleForSubItems = false;
                //list.SubItems.Add(s.Split(',')[0]);
                list.SubItems.Add(empInfo.Name);
                list.SubItems.Add(empInfo.Lastname);

                //n.Font = fontListView;
                //listViewDeviceUsers.Items.Add(n);
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en agregarItem. Ex: " + ex.ToString());
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddDeviceUser ventanaAdd = new frmAddDeviceUser();
            ventanaAdd.mainForm = this;
            ventanaAdd.ShowDialog();
            ventanaAdd.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string errDesc = "";
            int errCode = -1;

            if (listViewDeviceUsers.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Please select one user", "Warning");
                return;
            }

            int cantSelected = listViewDeviceUsers.SelectedIndices.Count;

            bool okDelete = false;
            if (cantSelected == 1)
            {
                DialogResult res = MessageBox.Show("Delete Device User " + listViewDeviceUsers.SelectedItems[0].Text + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    okDelete = true;
            }
            else
            {
                DialogResult res = MessageBox.Show("Do you really want to delete " + cantSelected + " items?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    okDelete = true;
            }

            if (okDelete)
            {
                //somethingChanged = true;
                List<ListViewItem> listaItems = new List<ListViewItem>();

                for (int i = 0; i < listViewDeviceUsers.SelectedIndices.Count; i++)
                {
                    ListViewItem item = listViewDeviceUsers.Items[listViewDeviceUsers.SelectedIndices[i]];
                    listaItems.Add(item);
                }

                foreach (ListViewItem i in listaItems)
                {
                    listViewDeviceUsers.Items.Remove(i);
                    var respuesta = WebServiceAPI.GetInstance().DeleteDeviceUser(i.Text, out errDesc, out errCode); 
                }

                listaDeviceUsers = WebServiceAPI.GetInstance().CargarListaDeviceUsers();
                actualizarListaDeviceUsers(listaDeviceUsers);

                clearSelected();
            }
        }

        private void clearSelected()
        {
            for (int i = 0; i < listViewDeviceUsers.Items.Count; i++)
            {
                listViewDeviceUsers.Items[i].Selected = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
