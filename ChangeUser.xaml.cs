using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace HCI_Sat
{
    /// <summary>
    /// Interaction logic for AddWin.xaml
    /// </summary>
    public partial class ChangeUser : Window
    {
        string temp = "";
        string tempS = "";
        string tempF = "";
        int id = 0;

        public ChangeUser(int ind)
        {
            InitializeComponent();
            id = ind;
            Satovi s = Main.GroupaSatova[ind];

            tempS = s.Img;
            txtIme.Text = s.Ime;
            txtID.Text = Convert.ToString(s.Id);


            Uri fileUri = new Uri(s.Img);
            Slika.Source = new BitmapImage(fileUri);

            TextRange textRange;
            FileStream fileStream;

            if (File.Exists(s.Rtf))
            {
                textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                using (fileStream = new FileStream(s.Rtf, FileMode.OpenOrCreate))
                {
                    textRange.Load(fileStream, DataFormats.Rtf);
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (validate() == true)
            {
                if(temp == "")
                {
                    temp = tempS;
                }
                //Main m = new Main();
                this.Close();
                //m.ShowDialog();
            }
        }

        private void tbSlika_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                temp = openFileDialog.FileName;
                Uri fileUri = new Uri(temp);
                Slika.Source = new BitmapImage(fileUri);
            }
        }

        private bool validate()
        {

            bool isNumeric = int.TryParse(txtID.Text, out _);
            bool result = true;
            if (txtIme.Text.Trim().Equals(""))
            {
                result = false;
                MessageBox.Show("Greska kod imena!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (txtID.Text.Trim().Equals("") || !isNumeric)
            {
                result = false;
                MessageBox.Show("Greska kod ID-a!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (Slika.Source == null)
            {
                result = false;
                MessageBox.Show("Greska kod slike!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            return result;
        }

        private void rtb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btBold_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btUnd_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btItal_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
