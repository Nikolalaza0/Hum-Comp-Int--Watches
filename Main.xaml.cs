using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    public partial class Main : Window
    {
        private DataIO serializer = new DataIO();
        public static BindingList<Satovi> GroupaSatova { get; set; }

        public List<Satovi> SatoviBrisanje = new List<Satovi>();

        public string danas;

        public Main(string str)
        {
            InitializeComponent();
            danas = str;
            GroupaSatova = serializer.DeSerializeObject<BindingList<Satovi>>("satovi.xml");
            if (GroupaSatova == null) //U slucaju da nista nije ucitano
            {
                GroupaSatova = new BindingList<Satovi>(); //nova lista pa cemo u nju dodavati
            }
            DataContext = this; //okidac Data Bindinga

            if(danas == "kor")
            {
                buttonDodavanje.Visibility = Visibility.Collapsed;
                buttonObrisi.Visibility = Visibility.Collapsed;
            }

            if (danas == "admin")
            {
                buttonDodavanje.Visibility = Visibility.Visible;
                buttonObrisi.Visibility = Visibility.Visible;   
            }
        }

        public Main()
        {
            InitializeComponent();
            GroupaSatova = serializer.DeSerializeObject<BindingList<Satovi>>("satovi.xml");
            if (GroupaSatova == null) //U slucaju da nista nije ucitano
            {
                GroupaSatova = new BindingList<Satovi>(); //nova lista pa cemo u nju dodavati
            }
            DataContext = this; //okidac Data Bindinga
        }

        private void buttonIzlaz_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Close();
            m.ShowDialog();
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            foreach(Satovi s in SatoviBrisanje)
            {
                GroupaSatova.Remove(s);
                if (File.Exists(s.Rtf))
                {
                    File.Delete(s.Rtf);
                }
            }
            serializer.SerializeObject<BindingList<Satovi>>(Main.GroupaSatova, "satovi.xml");
        }
        private void buttonDodavanje_Click(object sender, RoutedEventArgs e)
        {
            AddWin add = new AddWin();
            add.ShowDialog();
        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if(danas == "kor")
            {
                ChangeUser add = new ChangeUser(dataGridSatovi.SelectedIndex);
                add.ShowDialog();
            }
            else
            {
                ChangeAdmin add = new ChangeAdmin(dataGridSatovi.SelectedIndex);
                add.ShowDialog();
            }
        }

        private void oznakaChecked(object sender, RoutedEventArgs e)
        {
            Satovi s = GroupaSatova[dataGridSatovi.SelectedIndex];
            SatoviBrisanje.Add(s);
        }
    }
}
