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

namespace HCI_Sat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string userProv;


        private void btn_Izlaz_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Prijava_Click(object sender, RoutedEventArgs e)
        {
            string korUser = "kor";
            string korPass = "kor";

            string adminUser = "admin";
            string adminPass = "admin";

            if ((txtUser.Text == korUser) && (txtPass.Text == korPass))
            {
                userProv = txtUser.Text;
                Main window2 = new Main(userProv);
                this.Close();
                window2.ShowDialog();
            }
            else if ((txtUser.Text == adminUser) && (txtPass.Text == adminPass))
            {
                userProv = txtUser.Text;
                Main window2 = new Main(userProv);
                this.Close();
                window2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pogresan login!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
