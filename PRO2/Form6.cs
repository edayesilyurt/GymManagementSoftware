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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();

            List<UyelikPaket> liste = new List<UyelikPaket>(); 
            liste = XMLPaket.XmlOku(); //XmlOku metodu xml dosyasından verileri listeye atıp döndürür

            for (int i = 0; i < liste.Count; i++)
            {
                comboBox1.Items.Add(liste[i].paketIsmi);//xml dosyasından okuyup döndürülen değerler comboboxa eklenir
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str="";
            if (radioButton1.Checked)
                str = "K"; //K, KADIN anlamına gelir, databasedeki tabloya harf olarak kaydedilir
            else if (radioButton2.Checked)
                str = "E"; //E, ERKEK anlamına gelir
            using (var db = new SporSalonuContext())//sporsalonuentities
            {
                try
                {
                    var yeniUye = new UYE() // Database'e eklenecek UYE tipinden bir nesne yaratılır
                    {
                        UyeID = Convert.ToInt32(textBox5.Text),
                        Ad = textBox1.Text,
                        Soyad = textBox2.Text,
                        Cinsiyet = str,
                        Tel_no = textBox6.Text,
                        Dogum_tarihi = dateTimePicker1.Value.Date,
                        Kayit_tarihi = DateTime.Today,
                        Paket_secimi = comboBox1.SelectedItem.ToString(),
                        Boy = Convert.ToInt32(textBox3.Text),
                        Kilo = Convert.ToSingle(textBox4.Text.Replace(".", ",")),
                        GuncelKilo = null,
                        HedefKilo = Convert.ToSingle(textBox7.Text, CultureInfo.InvariantCulture),
                    };
                    db.UYE.Add(yeniUye); //Databasedeki tabloya yeniUye nesnesi eklenir
                    db.SaveChanges(); //Değişiklikler kaydedilir
                    var userRole = new ROL() //Şifrenin kime ait olduğunu anlamak için kullanılan ROL tanlosu için bir nesne yaratılır
                    {
                        KullanıcıID = Convert.ToInt32(textBox5.Text),
                        KullanıcıRolü = "Üye"
                    };
                    db.ROL.Add(userRole); //Databasedeki tabloya nesne eklenir
                    db.SaveChanges(); //Değişiklikler kaydedilir
                    MessageBox.Show(DateTime.Now.ToLongDateString() + " tarihinde kayıt eklendi");
                }
                catch
                {
                    MessageBox.Show("Giriş uygun formatta değildi! Lutfen boş alanları doldurunuz..");
                }
            }
         }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new SporSalonuContext())
            {
                try
                {
                    var yeniUye = new VUCUTOLC()
                    {
                        UyeID = Convert.ToInt32(textBox5.Text),
                        BasBiceps = Convert.ToSingle(textBox8.Text.Replace(".", ",")),
                        BasBel = Convert.ToSingle(textBox9.Text.Replace(".", ",")),
                        BasGogus = Convert.ToSingle(textBox10.Text.Replace(".", ",")),
                        BasOmuz = Convert.ToSingle(textBox11.Text.Replace(".", ",")),
                        BasBacak = Convert.ToSingle(textBox12.Text.Replace(".", ","))
                    };

                    db.VUCUTOLC.Add(yeniUye);
                    db.SaveChanges();
                    MessageBox.Show(DateTime.Now.ToLongDateString() + " tarihinde vücut ölçüleri kaydedildi!");
                }
                catch
                {
                    MessageBox.Show("Giriş uygun formatta değildi! Lutfen boş alanları doldurunuz..");
                }
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            Random rastgele = new Random();
            int sayi = rastgele.Next(200, 999); //Random olarak 200-999 arası üye Id'si belirlenir

            using (var db = new SporSalonuContext())
            {
                var kisi = db.UYE.Find(sayi); 
                while (kisi != null) //Databasede eğer bu ID'ye sahip üye varsa
                {
                    sayi = rastgele.Next(200,999); // yeniden random olarak bir ID belirlenir
                    kisi = db.UYE.Find(sayi);
                }
                textBox5.Text = Convert.ToString(sayi); //unique bir değer olan ID ekrana yazılır
            }
        }
    }
}
