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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new SporSalonuContext())
            {
                try
                {
                    var yeniEgitmen = new EGITMEN() // Database'e eklenmek üzere EGITMEN tipinden bir nesne yaratılır
                    {
                        EgitmenID = Convert.ToInt32(textBox5.Text),
                        Ad = textBox1.Text,
                        Soyad = textBox2.Text,
                        Tel_no = textBox3.Text,
                        Maas = Convert.ToSingle(textBox4.Text.Replace(".", ","))
                    };
                    db.EGITMEN.Add(yeniEgitmen);
                    db.SaveChanges();
                    var userRole = new ROL() // Kullanıcının idsine göre hangi kullanıcı rolüne sahip olduğunu bulan tabloya kayıt eklemek için nesne oluşturulur
                    {
                        KullanıcıID = Convert.ToInt32(textBox5.Text),
                        KullanıcıRolü = "Eğitmen"
                    };
                    db.ROL.Add(userRole);
                    db.SaveChanges();
                    MessageBox.Show(DateTime.Now.ToLongDateString() + " tarihinde kayıt EKLENDİ");
                }
                catch
                {
                    MessageBox.Show("Giriş uygun formatta değildi! Lutfen boş alanları doldurunuz..");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                removeMember(Convert.ToInt32(textBox6.Text));
            }
            catch
            {
                MessageBox.Show("Lütfen istenilen formatta giriş yapınız!");
            }           
        }

        public void removeMember(int pass)
        {
            EGITMEN memberToDelete = new EGITMEN();
            ROL memberToDelete2 = new ROL();

            using (var ctx = new SporSalonuContext())
            {
                memberToDelete = ctx.EGITMEN.Where(s => s.EgitmenID == pass).FirstOrDefault<EGITMEN>(); //Kullanıcıdan alınan ID'ye sahip eğitmen bulunup bulunmadığı aranır
                memberToDelete2 = ctx.ROL.Where(s => s.KullanıcıID == pass).FirstOrDefault<ROL>(); //Kullanıcıdan alınan ID'ye sahip eğitmen bulunup bulunmadığı ROL tablosunda aranır
            }

            if (memberToDelete != null && memberToDelete2 != null) //
            {  
                using (var newContext = new SporSalonuContext()) //Disconnected olarak yeni context yaratılır
                {
                    newContext.Entry(memberToDelete).State = System.Data.Entity.EntityState.Deleted;  //EGITMEN tablosundaki state, deleted durumuna set edilir
                    newContext.Entry(memberToDelete2).State = System.Data.Entity.EntityState.Deleted; //ROL tablosundaki state, deleted durumuna set edilir
                    newContext.SaveChanges();
                }
                MessageBox.Show("Silme işlemi başarılı!");
            }
            else
                MessageBox.Show("Bu ID'ye sahip eğitmen bulunamadı! İşlem başarısız!");
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Random rastgele = new Random();
            int sayi = rastgele.Next(100, 199); //Random olarak 100-199 arası üye Id'si belirlenir

            using (var db = new SporSalonuContext())
            {
                var kisi = db.EGITMEN.Find(sayi);
                while (kisi != null)  //Databasede eğer bu ID'ye sahip üye varsa
                {
                    sayi = rastgele.Next(100, 199);
                    kisi = db.EGITMEN.Find(sayi); // yeniden random olarak bir ID belirlenir
                }
                textBox5.Text = Convert.ToString(sayi);
            }
        }
    }
}
