using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO2
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            List<UyelikPaket> liste = new List<UyelikPaket>();
            liste = XMLPaket.XmlOku(); //XmlOku metodu xml dosyasından verileri listeye atıp döndürür

            for (int i = 0; i < liste.Count; i++)
            {
                comboBox1.Items.Add(liste[i].paketIsmi);//xml dosyasından okuyup döndürülen değerler comboboxa eklenir
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UYE personToUpdate = new UYE();
            int id = Convert.ToInt32(textBox1.Text);

            using (var db = new SporSalonuContext())
            {
                personToUpdate = db.UYE.Where(s => s.UyeID == id).FirstOrDefault<UYE>();

                if (personToUpdate != null)
                {
                    personToUpdate.Kayit_tarihi = DateTime.Today;
                    personToUpdate.Paket_secimi = comboBox1.SelectedItem.ToString();
                    db.SaveChanges();
                    MessageBox.Show(DateTime.Now.ToLongDateString() + " tarihinde kayıt GÜNCELLENDİ");
                }
            }
        }
    }
}
