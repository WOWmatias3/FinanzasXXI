using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using BLL;
using System.Data;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextSharp;
using Border = iText.Layout.Borders.Border;
using System.IO;

namespace Finanzas
{
    /// <summary>
    /// Lógica de interacción para ReportesPedidos.xaml
    /// </summary>
    public partial class ReportesPedidos : MetroWindow
    {
        PedidoBLL pb = new PedidoBLL();
        DataTable dt = new DataTable();
        public ReportesPedidos()
        {
            InitializeComponent();
        }

        private void Btn_reporteMensual_Click(object sender, RoutedEventArgs e)
        {
            date_desde.IsEnabled = false;
            date_hasta.IsEnabled = false;
            lb1.Content = "";
            lb2.Content = "";
            btn_filtrarRango.Visibility = Visibility.Hidden;
            dt.Clear();
            dt = pb.Reporte_PedidoMensual();
            dtg_reportepedido.ItemsSource = dt.DefaultView;
            txt_total.Text= Total(dt).ToString();
            
        }


        private int Total(DataTable datos)
        {

            int suma = 0;
            for (int i = 0; i < datos.Rows.Count; i++)
            {
                int monto = Int32.Parse(dt.Rows[i]["MONTO"].ToString());

                suma = suma + monto;

            }
            return suma;
        }

        private void Btn_ReporteHoy_Click(object sender, RoutedEventArgs e)
        {
            date_desde.IsEnabled = false;
            date_hasta.IsEnabled = false;
            lb1.Content = "";
            lb2.Content = "";
            btn_filtrarRango.Visibility = Visibility.Hidden;
            dt.Clear();
            dt = pb.Reporte_pedidosHoy();
            dtg_reportepedido.ItemsSource = dt.DefaultView;
            txt_total.Text = Total(dt).ToString();
        }

        private void Btn_semanal_Click(object sender, RoutedEventArgs e)
        {
            date_desde.IsEnabled = false;
            date_hasta.IsEnabled = false;
            lb1.Content = "";
            lb2.Content = "";
            btn_filtrarRango.Visibility = Visibility.Hidden;
            dt.Clear();
            dt = pb.Reporte_PedidoSemanal();
            dtg_reportepedido.ItemsSource = dt.DefaultView;
            txt_total.Text = Total(dt).ToString();
        }

        private void Btn_rango_Click(object sender, RoutedEventArgs e)
        {
            date_desde.IsEnabled = true;
            date_hasta.IsEnabled = true;
            lb1.Content = "";
            lb2.Content = "";
            btn_filtrarRango.Visibility = Visibility.Visible;
            
        }

        private void Btn_filtrarRango_Click(object sender, RoutedEventArgs e)
        {
            bool fechadesde = true;
            bool fechahasta = true;

            if (date_desde.SelectedDate.ToString() == "")
            {
                fechadesde = false;
                lb1.Content = "Seleccione una fecha";
                
            }

            if (date_hasta.SelectedDate.ToString() == "")
            {
                fechahasta = false;
                lb2.Content = "Seleccione una fecha";
            }

            if (fechadesde && fechahasta)
            {
                DateTime fd = date_desde.SelectedDate.Value;
                DateTime fh = date_hasta.SelectedDate.Value;
                fd = fd.AddDays(-1);
                fh = fh.AddDays(+1);
                dt.Clear();
                dt = pb.Reporte_PedidoRango(fd,fh);
                dtg_reportepedido.ItemsSource = dt.DefaultView;
                txt_total.Text = Total(dt).ToString();
            }
           

        }

        private void Date_desde_GotFocus(object sender, RoutedEventArgs e)
        {
            lb1.Content = "";
            lb2.Content = "";
        }

        private void Date_hasta_GotFocus(object sender, RoutedEventArgs e)
        {
            lb1.Content = "";
            lb2.Content = "";
        }

        private void Btn_generar_Click(object sender, RoutedEventArgs e)
        {
            string ruta = @"C:\Reportes Restaurante\Reportes Pedidos";


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
            //DataTable dt = usrBLL.Getdatoplato();



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

            Table tb2 = new Table(7).UseAllAvailableWidth();
            Cell cell2 = new Cell().Add(new Paragraph("#"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Proveedor"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Insumo"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Cantidad"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Monto"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Estado"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Fecha"));
            tb2.AddCell(cell2.AddStyle(stylecell));

            int total = Total(dt);
            int filas = dt.Rows.Count;
            int contador = 0;

            foreach (DataRow row in dt.Rows)
            {
                cell2 = new Cell().Add(new Paragraph(row["ID PEDIDO"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["PROVEEDOR"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["INGREDIENTE"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["CANTIDAD"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["MONTO"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["ESTADO"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["FECHA"].ToString()));
                tb2.AddCell(cell2);
                
                int aux = dt.Rows.Count;
                if (contador == aux-1)
                {
                    cell2 = new Cell().Add(new Paragraph(""));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph(""));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph(""));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph("TOTAL"));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph("" + total));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph(""));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph(""));
                    tb2.AddCell(cell2);
                }
                else
                {

                }
                contador++;
               

            }
            doc.Add(tb2);
            doc.Close();
            MessageBox.Show("creado");
        }

        private void Btn_atras_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}

