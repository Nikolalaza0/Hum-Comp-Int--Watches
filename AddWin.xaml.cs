using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class AddWin : Window
    {

        private string tempS = "";
        private DataIO serializer = new DataIO();

        public AddWin()
        {
            InitializeComponent();
            DataContext = this; //okidac Data Bindinga

            cbFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cbSize.ItemsSource = new List<double>() { 8, 10, 12, 14, 16, 18, 20, 30 };
            cbColor.ItemsSource = new List<string>() { "Black", "White", "Blue", "Red", "Green", "Purple" };
            cbColor.SelectedIndex = 0;
            cbSize.SelectedIndex = 0;
            cbFamily.SelectedIndex = 0;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (validate() == true)
            {
                string name = "";
                name = txtIme.Text + ".rtf";

                Main.GroupaSatova.Add(new Satovi(txtIme.Text, Convert.ToInt32(txtID.Text), DateTime.Now, tempS, name));
                serializer.SerializeObject<BindingList<Satovi>>(Main.GroupaSatova, "satovi.xml");

                TextRange range;
                FileStream fStream;
                range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                fStream = new FileStream(name, FileMode.Create);
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
                tempS = openFileDialog.FileName;
                Uri fileUri = new Uri(tempS);
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
            CountWord();
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

        private void CountWord()
        {
            string richText = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;

            int wordCount = Regex.Matches(richText, @"\b[A-Za-z0-9]+\b").Count;

            brojac.Text = wordCount.ToString();

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
