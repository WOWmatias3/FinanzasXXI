using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para Reporte_Boleta.xaml
    /// </summary>
    public partial class ReporteBoletas : MetroWindow
    {
        string tipo = "";
        DataTable dt = new DataTable();
        PedidoBLL pb = new PedidoBLL();
        public ReporteBoletas()
        {
            InitializeComponent();
        }

        private void Btn_hoy_Click(object sender, RoutedEventArgs e)
        {
            desde.IsEnabled = false;
            hasta.IsEnabled = false;
            btn_listarrango.Visibility = Visibility.Hidden;
            bool veri=verificacion();
            dt.Clear();
            if (veri == true)
            {

                if (tipo == "trs")
                {
                    dt = pb.Reporte_Boletas_trs_hoy();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                } else if (tipo=="efe")
                {
                    dt = pb.Reporte_Boletas_efe_hoy();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo=="todos")
                {
                    dt = pb.Reporte_Boletas_todos_hoy();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                
            }
        }


        private bool verificacion()
        {
            bool tipo = false;
            if (rdb_trs.IsChecked == false && rdb_efe.IsChecked == false && rdb_todos.IsChecked == false)
            {
                tipo = false;
                if (tipo == false)
                {
                    lb1.Content = "Debe seleccionar un tipo de pago para poder filtrar";
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return true;
            }
           
           



        }
        private void Rdb_trs_Checked(object sender, RoutedEventArgs e)
        {
            tipo = "trs";
        }

        private void Rdb_efe_Checked(object sender, RoutedEventArgs e)
        {
            tipo = "efe";
        }

        private void Rdb_todos_Checked(object sender, RoutedEventArgs e)
        {
            tipo="todos";
        }

        private void Btn_semana_Click(object sender, RoutedEventArgs e)
        {

            desde.IsEnabled = false;
            hasta.IsEnabled = false;
            btn_listarrango.Visibility = Visibility.Hidden;
            bool veri = verificacion();
            dt.Clear();
            if (veri == true)
            {

                if (tipo == "trs")
                {
                    dt = pb.Reporte_Boletas_trs_semana();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo=="efe")
                {
                    dt = pb.Reporte_Boletas_efe_semana();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo == "todos")
                {
                    dt = pb.Reporte_Boletas_todos_semana();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }

            }
        }

        private void Btn_mes_Click(object sender, RoutedEventArgs e)
        {

            desde.IsEnabled = false;
            hasta.IsEnabled = false;
            btn_listarrango.Visibility = Visibility.Hidden;
            bool veri = verificacion();
            dt.Clear();
            if (veri == true)
            {

                if (tipo == "trs")
                {
                    dt = pb.Reporte_Boletas_trs_mes();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo == "efe")
                {
                    dt = pb.Reporte_Boletas_efe_mes();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo == "todos")
                {
                    dt = pb.Reporte_Boletas_todos_mes();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }

            }
        }

        private void Btn_año_actual_Click(object sender, RoutedEventArgs e)
        {
            desde.IsEnabled = false;
            hasta.IsEnabled = false;
            btn_listarrango.Visibility = Visibility.Hidden;
            bool veri = verificacion();
            dt.Clear();
            if (veri == true)
            {

                if (tipo == "trs")
                {
                    dt = pb.Reporte_Boletas_trs_anio();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo == "efe")
                {
                    dt = pb.Reporte_Boletas_efe_anio();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo == "todos")
                {
                    dt = pb.Reporte_Boletas_todos_anio();
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }

            }
        }

        private void Btn_listarrango_Click(object sender, RoutedEventArgs e)
        {
            bool fechas = verificarfechas();
            if (fechas == false)
            {

            }
            else
            {
                DateTime des = desde.SelectedDate.Value;
                DateTime has = hasta.SelectedDate.Value;

                des = des.AddDays(-1);
                has = has.AddDays(+1);

                if (tipo == "trs")
                {
                    dt = pb.Reporte_Boletas_trs_rango(des,has);
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo == "efe")
                {
                    dt = pb.Reporte_Boletas_efe_rango(des, has);
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
                else if (tipo == "todos")
                {
                    dt = pb.Reporte_Boletas_todos_rango(des, has);
                    dtg_reporte_boleta.ItemsSource = dt.DefaultView;
                }
            }
        }


        private bool verificarfechas()
        {
            bool fechasd = true;
            bool fechash = true;
            if (desde.SelectedDate.ToString() == "")
            {
                fechasd = false;
                lb2.Content = "Debe seleccionar una fecha";
            }
            if (hasta.SelectedDate.ToString()=="")
            {
                fechash = false;
                lb3.Content = "Debe seleccionar una fecha";
            }
            if (fechasd && fechash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Btn_rango_Click(object sender, RoutedEventArgs e)
        {
            desde.IsEnabled = true;
            hasta.IsEnabled = true;
            btn_listarrango.Visibility = Visibility.Visible;


        }

        private void Btn_atras_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_reporte_Click(object sender, RoutedEventArgs e)
        {
            string ruta = @"C:\Reportes Restaurante\Reportes Boletas";


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

            Table tb2 = new Table(8).UseAllAvailableWidth();
            Cell cell2 = new Cell().Add(new Paragraph("#"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Fecha"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Medio de Pago"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Sub Total"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Descuentos"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Total"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Mesa"));
            tb2.AddCell(cell2.AddStyle(stylecell));
            cell2 = new Cell().Add(new Paragraph("Rut Cliente"));
            tb2.AddCell(cell2.AddStyle(stylecell));

            int tot = Total(dt);
            int sub = subtotal(dt);
            int desc = descuentos(dt);
            int filas = dt.Rows.Count;
            int contador = 0;

            foreach (DataRow row in dt.Rows)
            {
                cell2 = new Cell().Add(new Paragraph(row["ID"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["FECHA"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["MEDIO DE PAGO"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["SUB TOTAL"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["DESCUENTOS"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["TOTAL"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["MESA"].ToString()));
                tb2.AddCell(cell2);
                cell2 = new Cell().Add(new Paragraph(row["CLIENTE"].ToString()));
                tb2.AddCell(cell2);

                int aux = dt.Rows.Count;
                if (contador == aux - 1)
                {
                    cell2 = new Cell().Add(new Paragraph(""));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph(""));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph(""));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph("" + sub));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph("" + desc));
                    tb2.AddCell(cell2);
                    cell2 = new Cell().Add(new Paragraph("" + tot));
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


        private int Total(DataTable datos)
        {

            int total = 0;
            for (int i = 0; i < datos.Rows.Count; i++)
            {
                int monto = Int32.Parse(dt.Rows[i]["TOTAL"].ToString());
                total = total + monto;

            }
            return total;
        }
        private int subtotal(DataTable datos)
        {

            int subtotal = 0;
            for (int i = 0; i < datos.Rows.Count; i++)
            {
                int monto = Int32.Parse(dt.Rows[i]["SUB TOTAL"].ToString());
                subtotal = subtotal + monto;

            }
            return subtotal;
        }
        private int descuentos(DataTable datos)
        {

            int descuentos = 0;
            for (int i = 0; i < datos.Rows.Count; i++)
            {
                int monto = Int32.Parse(dt.Rows[i]["DESCUENTOS"].ToString());
                descuentos = descuentos + monto;

            }
            return descuentos;
        }
    }
}
