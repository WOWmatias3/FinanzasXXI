using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using BLL;
using Tulpep.NotificationWindow;

namespace Finanzas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btningresar_Click_1(object sender, RoutedEventArgs e)
        {

            UsuarioBLL usrbll = new UsuarioBLL();
            bool check = usrbll.getLogin(txtnomb.Text, txtcontra.Password);
            if (check)
            {

                int rut = usrbll.Getrut(txtnomb.Text, txtcontra.Password);
                string nombre = usrbll.Get_nombrecompleto(rut);

                PopupNotifier popup = new PopupNotifier();
                popup.TitleText = "Aviso";
                popup.Image = Properties.Resources.add;
                popup.ContentText = "Bienvenido/a" + nombre;
                popup.AnimationDuration = 500;
                popup.Delay = 3500;
                popup.Popup();
                Window1 adm = new Window1();
                adm.Owner = this;
                adm.lb1.Content = nombre;
                adm.ShowDialog();
                Close();
                





               
            }
            else
            {
                MessageBox.Show("Credenciales o Rol Incorrectos \n Intente nuevamente");
            }
        }
    }
    

    }


