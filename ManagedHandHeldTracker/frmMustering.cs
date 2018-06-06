using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
//using System.Threading.Tasks;
using System.Globalization;
using System.Threading;


namespace ManagedHandHeldTracker
{
    public partial class frmMustering : Form
    {
        public Dictionary<long, string> listaZonas;             // <PanelID, nombreVZone>
        private Dictionary<string, int> CantEmpZonas;           // <nombreVZone,cantEmpleados>

        private string ISOLanguajeName = "";                    // Nomenclatura ISO de 2 letras: es=Español, en=Ingles.

        public frmMustering()
        {
            CantEmpZonas = new Dictionary<string, int>();
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (listviewZonas.SelectedItems.Count>0)
            {
                string zoneName = listviewZonas.SelectedItems[0].Text;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "PDF file|*.pdf";
                saveFileDialog1.Title = "Save a PDF File";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    if (fs!=null)
                    {
                        string errDesc = "";
                        int errCode = 0;
                        Tools.GetInstance().DoLog("Va a llamar a ObtenerListaEmpleadosEnZona");

                        List<empInfo> listaEmp = WebServiceAPI.GetInstance().ObtenerListaEmpleadosEnZona(zoneName, ref errDesc, ref errCode);
                        Tools.GetInstance().DoLog("Llamo a ObtenerListaEmpleadosEnZona. errCode="  +errCode);
                        
                        listaEmp.Sort(new empInfoComparer());           // Orden alfabetico

                        if (errCode == (int)StatusCode.OK)
                        {
                            bool res = PDFHelper.getInstance().exportEmpInZone(zoneName, listaEmp, fs, ISOLanguajeName, ref errDesc);  // Genera el PDF
                            if (res)
                                MessageBox.Show(saveFileDialog1.FileName + " successfully created");
                            else
                                MessageBox.Show("Error saving " + saveFileDialog1.FileName + ": " + errDesc);
                        }
                        else
                            MessageBox.Show("Error retrieving data for " + saveFileDialog1.FileName + ": " + errDesc);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMustering_Load(object sender, EventArgs e)
        {
            CultureInfo ci = CultureInfo.CurrentUICulture;
            ISOLanguajeName = ci.TwoLetterISOLanguageName;
            cargaInicial();
        }

        private void cargaInicial()
        {
            lblLoading.Visible = true;
            inicializarListView();
            Thread t = new Thread(cargarDatosZonas);
            t.Start();

            //Task.Factory.StartNew(() => cargarDatosZonas());

            if (ISOLanguajeName == "es")
            {
                rdbES.Checked = true;
                rdbEN.Checked = false;
            }
            else
            {
                rdbES.Checked = false;
                rdbEN.Checked = true;
            }


        }

        private void cargarDatosZonas()
        {
            try
            {
                foreach (KeyValuePair<long, string> zona in listaZonas)
                {
                    int cantEmp = WebServiceAPI.GetInstance().ObtenerCantidadEmpleadosEnZona(zona.Value);
                    Tools.GetInstance().DoLog("Cantidad empleados en la " + zona.Value + " " + cantEmp.ToString());
                    if (cantEmp >= 0)
                    {
                        Tools.GetInstance().DoLog("1");
                        if (!CantEmpZonas.ContainsKey(zona.Value))
                            CantEmpZonas.Add(zona.Value, cantEmp);
                        else
                            CantEmpZonas[zona.Value] = cantEmp;

                        blinkLoading();

                    }
                    Tools.GetInstance().DoLog("loop");
                }
                actualizarListView();
                Tools.GetInstance().DoLog("Fin");
                updateLoadingVisible(false);

            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en cargarDatosZonas():" + ex.Message);
            }
        }

        private void blinkLoading()
        {
            Invoke((MethodInvoker)delegate
            {
                lblLoading.ForeColor = lblLoading.ForeColor == Color.Red ? Color.Blue: Color.Red;

            });
        }

        private void updateLoadingVisible(bool visible)
        {
            Invoke((MethodInvoker)delegate
            {
                lblLoading.Visible = visible;

            });
        }



        // Recorre el diccionario CantEmpZonas y actualiza completamente el listview: si ya existia el elemeto, actualiza su cantidad 
        // y si no existia, lo agrega. Metodo Invioke para poder llamarlo desde un thread

        private void actualizarListView()
        {
            Tools.GetInstance().DoLog("entra a actualizarlistview");
            Tools.GetInstance().DoLog("CantEmpZonas.count=" + CantEmpZonas.Count);

            Invoke((MethodInvoker)delegate
            {
                foreach (KeyValuePair<string, int> par in CantEmpZonas)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = par.Key;
                    item.SubItems.Add(par.Value.ToString());

                    listviewZonas.Items.Add(item);

                    //if (listviewZonas.Items.ContainsKey(par.Key))
                    //{
                    //    listviewZonas.Items[par.Key].SubItems[0].Text = par.Value.ToString();
                    //}
                    //else
                    //{
                    //    listviewZonas.Items.Add(par.Key);
                    //    listviewZonas.Items[par.Key].SubItems[0].Text = par.Value.ToString();
                    //}
                }

            });
        }


        private void inicializarListView()
        {
            listviewZonas.View = View.Details;
            listviewZonas.GridLines = true;
            listviewZonas.Columns.Clear();
            int listViewWidth = listviewZonas.Size.Width;

            listviewZonas.Columns.Add("Virtual Zone", (listViewWidth-10)/2, HorizontalAlignment.Left);
            listviewZonas.Columns.Add("Employees on site", (listViewWidth - 10) / 2, HorizontalAlignment.Left);
            listviewZonas.OwnerDraw = true;

            listviewZonas.FullRowSelect = true;
            this.listviewZonas.MultiSelect = false;
            this.listviewZonas.HideSelection = false;
            this.listviewZonas.HeaderStyle = ColumnHeaderStyle.Nonclickable;

        }

        private void lblLoading_Click(object sender, EventArgs e)
        {

        }

        private int selectedindex = -1;

        private void listviewZonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listviewZonas.SelectedItems.Count > 0)
            {
                btnExport.Enabled = true;
                btnDetails.Enabled = true;
                selectedindex = listviewZonas.SelectedIndices[0];
            }
            else
            {
                selectedindex = -1;
                btnDetails.Enabled = false;
                btnExport.Enabled = false;
            }
        }

        // Mostrar una previsualizacion con las personas en sitio de la zona seleccionada
        private void listviewZonas_DoubleClick(object sender, EventArgs e)
        {
            if (listviewZonas.SelectedItems.Count > 0)
            {

                btnExport.Enabled = true;
                btnDetails.Enabled = true;
                string zoneName = listviewZonas.SelectedItems[0].Text;
                MostrarDetails(zoneName);
            }

        }


        private void listviewZonas_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
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

        private void listviewZonas_DrawItem(object sender, DrawListViewItemEventArgs e)
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

        private void listviewZonas_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
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

     
        private void MostrarDetails(string ZoneName)
        {
            string errDesc = "";
            int errCode = -1;
            List<empInfo> listaEmp = WebServiceAPI.GetInstance().ObtenerListaEmpleadosEnZona(ZoneName, ref errDesc, ref errCode);

            if (listaEmp != null)
                if (listaEmp.Count > 0)
                {
                    frmPersonasMustering ventana = new frmPersonasMustering();
                    ventana.ZoneName = ZoneName;
                    ventana.listaPersonas = listaEmp;
                    ventana.ISOLanguajeName = ISOLanguajeName;
                    ventana.ShowDialog();

                }
                else
                    MessageBox.Show("No persons in zone " + ZoneName);

        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (listviewZonas.SelectedItems.Count > 0)
            {
                string zoneName = listviewZonas.SelectedItems[0].Text;
                MostrarDetails(zoneName);
            }
        }

        private void rdbEN_CheckedChanged(object sender, EventArgs e)
        {
            ISOLanguajeName = "en";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rdbES_CheckedChanged(object sender, EventArgs e)
        {
            ISOLanguajeName = "es";
          
        }
    }
}
