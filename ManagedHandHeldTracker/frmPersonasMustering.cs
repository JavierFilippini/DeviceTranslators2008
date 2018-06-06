using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace ManagedHandHeldTracker
{
    public partial class frmPersonasMustering : Form
    {
        public List<empInfo> listaPersonas;
        public string ZoneName;
        public string ISOLanguajeName; 
        public frmPersonasMustering()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonasMustering_Activated(object sender, EventArgs e)
        {


        }

        private void frmPersonasMustering_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "Employees on site: " + ZoneName;
            inicializarListView();
            if (listaPersonas != null)
                if (listaPersonas.Count > 0)
                    actualizarListView(listaPersonas);

        }

        private void inicializarListView()
        {
            listViewPersonas.View = View.Details;
            listViewPersonas.GridLines = true;
            listViewPersonas.Columns.Clear();

            int listViewWidth = listViewPersonas.Size.Width;

            listViewPersonas.Columns.Add("Employee", (int)((listViewWidth - 20)*0.5f), HorizontalAlignment.Left);
            listViewPersonas.Columns.Add("Badge", (int)((listViewWidth - 20) * 0.2f), HorizontalAlignment.Left);
            listViewPersonas.Columns.Add("Entrance Date", (int)((listViewWidth - 20) * 0.3f), HorizontalAlignment.Left);
            listViewPersonas.OwnerDraw = true;

            listViewPersonas.FullRowSelect = true;
            this.listViewPersonas.MultiSelect = false;
            this.listViewPersonas.HideSelection = false;
            this.listViewPersonas.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        }


        private void actualizarListView(List<empInfo> listaPersonas)
        {
            Tools.GetInstance().DoLog("entra a actualizarlistview");
            Tools.GetInstance().DoLog("listaPersonas.count=" + listaPersonas.Count);

            listaPersonas.Sort(new empInfoComparer());

            // POr si se decide llamar a este metodo desde un Task...
            Invoke((MethodInvoker)delegate
            {
            
                foreach (empInfo emp in listaPersonas)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = emp.Name + " " + emp.Lastname;
                    item.SubItems.Add(emp.Badge);

                    string dateTimeFormat = (ISOLanguajeName == "es") ? "dd/MM/yyyy hh:mm" : "MM/dd/yyyy hh:mm";

                    item.SubItems.Add(emp.LastAccess.ToString(@dateTimeFormat) + " " + emp.LastAccess.ToString("tt", CultureInfo.InvariantCulture));
                    listViewPersonas.Items.Add(item);
                }
            });
        }


        private void listViewPersonas_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;

                using (var headerFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold))
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }
            }
        }

        private void listViewPersonas_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            using (var sf = new StringFormat())
            {
                using (var headerFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular))
                {
                    if (e.ItemIndex == selectedindex)
                        e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                    e.Graphics.DrawString(e.Item.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }
            }
        }

        private void listViewPersonas_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            using (var sf = new StringFormat())
            {
                using (var headerFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular))
                {
                    if (e.ItemIndex == selectedindex)
                        e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                    e.Graphics.DrawString(e.SubItem.Text, headerFont, Brushes.Black, e.Bounds, sf);
                }
            }
        }
        private int selectedindex = -1;

        private void listViewPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPersonas.SelectedItems.Count > 0)
            {

                selectedindex = listViewPersonas.SelectedIndices[0];
            }
            else
            {
                selectedindex = -1;
            }
        }




    }
}
