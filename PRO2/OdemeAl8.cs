using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO2
{
    public partial class OdemeAl8 : Form
    {
        public OdemeAl8()
        {
            InitializeComponent();
            label6.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UYE person = new UYE();
            using (var db = new SporSalonuContext())
            {
                 person = db.UYE.Find(Convert.ToInt32(textBox1.Text));
            }

            if (person != null)
            {
                label4.Text = "Paket Seçimi : " + person.Paket_secimi;
            }
            else
            {
                MessageBox.Show("Bu isimli kayıtlı üye yoktur!");
                textBox1.Clear();
            }
                
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            List<UyelikPaket> liste2 = new List<UyelikPaket>();
            liste2 = XMLPaket.XmlOku();

            UYE person2 = new UYE();

            try
            {
                using (var db = new SporSalonuContext())
                {
                    person2 = db.UYE.Find(Convert.ToInt32(textBox1.Text));
                }
                if (person2 != null)
                {
                    for (int i = 0; i < liste2.Count; i++)
                    {
                        double alinacak;
                        if (liste2[i].paketIsmi == person2.Paket_secimi)
                        {
                            alinacak = liste2[i].aylikUcret;

                            if (alinacak != Convert.ToDouble(textBox3.Text, CultureInfo.GetCultureInfo("en-us")))
                                MessageBox.Show("Yanliş tutar girdiniz !");
                            else
                            {
                                using (var db = new SporSalonuContext())
                                {
                                    var odemeKaydi = new ODEME()
                                    {
                                        OdemeTarihi = (DateTime.Today),
                                        OdenenMiktar = Convert.ToSingle(textBox3.Text.Replace(".", ",")),
                                        OdeyenID = person2.UyeID
                                    };
                                    db.ODEME.Add(odemeKaydi);
                                    db.SaveChanges();
                                    MessageBox.Show(DateTime.Now.ToLongDateString() + " tarihinde ödeme başarıyla kayıtlandı!");
                                }
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("Kişi bulunamadı !");
            }
            catch
            {
                MessageBox.Show("Giriş uygun formatta değildi! Lutfen boş alanları doldurunuz..");
            }
        }
    }
}
