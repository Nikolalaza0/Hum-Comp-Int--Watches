using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCI_Sat
{
    [Serializable]
    public class Satovi
    {
        string ime;
        int id;
        DateTime date;
        string img;
        string rtf;

        public Satovi(string ime, int id, DateTime date, string img, string rtf)
        {
            this.ime = ime;
            this.id = id;
            this.date = date;
            this.img = img;
            this.rtf = rtf;
        }

        public Satovi()
        {

        }

        public string Ime { get => ime; set => ime = value; }
        public int Id { get => id; set => id = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Img { get => img; set => img = value; }
        public string Rtf { get => rtf; set => rtf = value; }
    }
}
