using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ChangeAdmin : Window
    {
        string temp = "";
        string tempS = "";
        string tempF = "";
        int id = 0;
        private DataIO serializer = new DataIO();
        public ChangeAdmin(int ind)
        {
            InitializeComponent();
            id = ind;

            cbFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cbSize.ItemsSource = new List<double>() { 8, 10, 12, 14, 16, 18, 20, 30 };
            cbColor.ItemsSource = new List<string>() { "Black", "White", "Blue", "Red", "Green", "Purple" };
            cbColor.SelectedIndex = 0;
            cbSize.SelectedIndex = 0;
            cbFamily.SelectedIndex = 0;

            Satovi s = Main.GroupaSatova[ind];

            tempS = s.Img;
            txtIme.Text = s.Ime;
            txtID.Text = Convert.ToString(s.Id);
            Uri fileUri = new Uri(s.Img);
            Slika.Source = new BitmapImage(fileUri);

            tempF = s.Rtf;
            TextRange textRange;
            FileStream fileStream;

            if (File.Exists(tempF))
            {
                textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                using (fileStream = new FileStream(tempF, FileMode.OpenOrCreate))
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

                Main.GroupaSatova[id] = new Satovi(txtIme.Text, Convert.ToInt32(txtID.Text), DateTime.Now, temp, tempF);
                //MessageBox.Show("Mrzim C#!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                serializer.SerializeObject<BindingList<Satovi>>(Main.GroupaSatova, "satovi.xml");

                TextRange range;
                FileStream fStream;
                range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                fStream = new FileStream(tempF, FileMode.Open);
                range.Save(fStream, DataFormats.Rtf);
                fStream.Close();

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

        private void cbFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFamily.SelectedItem != null && !rtb.Selection.IsEmpty)
            {
                rtb.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cbFamily.SelectedItem);
            }
        }

        private void cbSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFamily.SelectedItem != null && !rtb.Selection.IsEmpty)
            {
                rtb.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cbSize.SelectedItem);
            }
        }

        private void cbColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbColor.SelectedItem != null && !rtb.Selection.IsEmpty)
            {
                rtb.Selection.ApplyPropertyValue(Inline.ForegroundProperty, cbColor.SelectedItem);
            }
        }

        private void rtb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void rtb_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtb.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

            temp = rtb.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btItal.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

            temp = rtb.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btUnd.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));



            temp = rtb.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cbFamily.SelectedItem = temp;


            temp = rtb.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cbSize.Text = temp.ToString();

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
