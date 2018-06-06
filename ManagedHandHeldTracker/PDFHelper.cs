using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;
using iTextSharp.text.pdf.draw;

namespace ManagedHandHeldTracker
{
    class PDFHelper
    {
        #region Singleton
        private static PDFHelper _reference;

        public static PDFHelper getInstance()
        {
            if (_reference == null)
                _reference = new PDFHelper();
            return _reference;
        }
        #endregion

        // Crea en el filestream especificado un PDF con la lista de personas dentro de la zona.
        // Usa la biblioteca itextSharp v 4.1.6 que es free (LGPL)
        public bool exportEmpInZone(string zoneName, List<empInfo> listaEmpleados, FileStream fs, string ISOLanguajeName, ref string errDesc)
        {
            string dateTimeFormat = (ISOLanguajeName == "es") ? "dd/MM/yyyy hh:mm" : "MM/dd/yyyy hh:mm";

            bool res = false;

            try
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                doc.Add(new Paragraph(new Phrase("Detailed Mustering Information", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 20f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK))));
                //doc.Add(new Paragraph(new Phrase("Virtual Zone: " + zoneName, new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 15f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK))));

                Phrase frase1 = new Phrase("Virtual Zone: " + zoneName, new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 15f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK));
                Phrase frase2 = new Phrase(DateTime.Now.ToString(@dateTimeFormat) + " " + DateTime.Now.ToString("tt", CultureInfo.InvariantCulture), new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK));

                // Generar una linea con texto justificado derecha y otro justificado izquierda con diferente font size
                Chunk glue = new Chunk(new VerticalPositionMark());
                Paragraph par = new Paragraph(frase1);
                par.Add(glue);
                par.Add(frase2);

                doc.Add(par);
                doc.Add(new Paragraph(" "));

                doc.Add(new Paragraph(new Phrase("Number of employees in the area: " + listaEmpleados.Count.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK))));

                doc.Add(new Paragraph(" "));

                PdfPTable tabla = new PdfPTable(3);
                PdfPCell enc1 = new PdfPCell(new Paragraph(new Phrase("Full Name", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.WHITE))));
                PdfPCell enc2 = new PdfPCell(new Paragraph(new Phrase("Badge", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.WHITE))));
                PdfPCell enc3 = new PdfPCell(new Paragraph(new Phrase("Entrance Date", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.WHITE))));

                enc1.BackgroundColor = iTextSharp.text.Color.BLACK;
                enc2.BackgroundColor = iTextSharp.text.Color.BLACK;
                enc3.BackgroundColor = iTextSharp.text.Color.BLACK;

                enc1.BorderColor = iTextSharp.text.Color.WHITE;
                enc2.BorderColor = iTextSharp.text.Color.WHITE;
                enc3.BorderColor = iTextSharp.text.Color.WHITE;

                tabla.AddCell(enc1);
                tabla.AddCell(enc2);
                tabla.AddCell(enc3);
                // La lleno con la info de los empleados
                foreach (empInfo emp in listaEmpleados)
                {
                    tabla.AddCell(emp.Name);
                    tabla.AddCell(emp.Badge);
                    tabla.AddCell(emp.LastAccess.ToString(@dateTimeFormat) + " " + emp.LastAccess.ToString("tt", CultureInfo.InvariantCulture));
                }

                doc.Add(tabla);

                doc.Close();

                fs.Close();

                res = true;
            }
            catch (Exception ex)
            {
                Tools.GetInstance().DoLog("Excepcion en exportZoneEmpToPDF: " + ex.Message);
            }
            return res;
        }
    }
}
