using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextSharp;
using Border = iText.Layout.Borders.Border;
using MahApps.Metro.Controls;

namespace Finanzas
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : MetroWindow
    {
        public string username = null;
        public Window1()
        {
            InitializeComponent();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*string path = @"c:\pdf";
            if (Directory.Exists(path))
            {
                Console.WriteLine("That path exists already.");
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));*/


            string ruta = @"C:\Reportes Restaurante\Reportes Platos";


            if (!Directory.Exists(ruta))
            {
                Console.WriteLine("Creando el directorio: {0}", ruta);
                DirectoryInfo direc = Directory.CreateDirectory(ruta);
            }
            else
            {
                Console.WriteLine("El directorio ya existe, no se ha realizado ninguna acción", ruta);
            }
            //var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var exportfile = System.IO.Path.Combine(ruta, "reporte.pdf");
            UsuarioBLL usrBLL = new UsuarioBLL();
            DataTable dt = usrBLL.Getdatoplato();



            PdfWriter pw = new PdfWriter(exportfile);
            PdfDocument pdfDoc = new PdfDocument(pw);
            Document doc = new Document(pdfDoc, PageSize.LETTER);
            doc.SetMargins(75, 30, 75, 30);
            

            Table tb = new Table(1).UseAllAvailableWidth();
            Cell cell = new Cell().Add(new Paragraph("Reporte").SetFontSize(14)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER)
                .SetFontColor(ColorConstants.BLUE));
            tb.AddCell(cell);

            doc.Add(tb);

            iText.Layout.Style stylecell = new iText.Layout.Style().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            Table tb2 = new Table(5).UseAllAvailableWidth();
            Cell cell2 = new Cell().Add(new Paragraph("#"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Nombre"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Precio"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Categoria"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("habilitado"));
            tb2.AddCell(cell2.AddStyle(stylecell));



            foreach (DataRow row in dt.Rows)
            {
                cell2 = new Cell().Add(new Paragraph(row["ID_PLATO"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["NOMBRE_PLATO"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["PRECIO"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["CATEGORIA"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["HABILITADO"].ToString()));
                tb2.AddCell(cell2);

            }
            doc.Add(tb2);
            doc.Close();
            MessageBox.Show("creado");
        }

        private void btnSalir_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            Close();
            mainwindow.ShowDialog();
        }

        private void finanzas_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Tile_reportes_Click(object sender, RoutedEventArgs e)
        {
            Fly_Reportes.IsOpen = true;
        }

        private void Tile_reportesPedidos_Click(object sender, RoutedEventArgs e)
        {
            ReportesPedidos rp = new ReportesPedidos();
            rp.Owner = this;
            rp.ShowDialog();
           
            
        }

        private void Tile_reporteboletas_Click(object sender, RoutedEventArgs e)
        {
            ReporteBoletas rb = new ReporteBoletas();
            rb.Owner = this;
            rb.ShowDialog();

        }
    }
}

