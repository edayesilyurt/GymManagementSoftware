using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO2
{
    public partial class UyeEkleGuncelle7 : Form
    {
        public UyeEkleGuncelle7()
        {
            InitializeComponent();
            UseWaitCursor = false;

            List<UyelikPaket> liste = new List<UyelikPaket>();
            liste = XMLPaket.XmlOku();

            for (int i = 0; i < liste.Count; i++)
            {
                comboBox1.Items.Add(liste[i].paketIsmi);//xml dosyasından okuyup döndürülen değerler comboboxa eklenir
            }
        }
        public void getData(string sqlcommand, DataGridView dataGridView1)
        {
            string connectionString = "Data Source=DESKTOP-1NG718J;Initial Catalog=SporSalonu;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand, connection);

            DataTable t = new DataTable();
            dataAdapter.Fill(t); //datatable'ı doldurmak için dataAdapter kullanılır
            dataGridView1.DataSource = t;
        }
        public void removeMember(int pass)
        {
            UYE memberToDelete = new UYE();
            ROL memberToDelete2 = new ROL();
            VUCUTOLC memberToDelete3 = new VUCUTOLC();

            using (var ctx = new SporSalonuContext())
            {
                memberToDelete = ctx.UYE.Where(s => s.UyeID == pass).FirstOrDefault<UYE>();
                memberToDelete2 = ctx.ROL.Where(s => s.KullanıcıID == pass).FirstOrDefault<ROL>();
                memberToDelete3 = ctx.VUCUTOLC.Where(s => s.UyeID == pass).FirstOrDefault<VUCUTOLC>();
            }
 
            using (var newContext = new SporSalonuContext()) //disconnected olarak yeni context yaratılır
            {
                newContext.Entry(memberToDelete).State = System.Data.Entity.EntityState.Deleted;
                newContext.Entry(memberToDelete2).State = System.Data.Entity.EntityState.Deleted;//The DbSet<T>.Remove method results in the entity's EntityState being set to Deleted
                newContext.Entry(memberToDelete3).State = System.Data.Entity.EntityState.Deleted;
                newContext.SaveChanges();
                //ODEME tablosu ile UYE arasında ilişkı vardır.ODEME tablosundaki kayıtlar databaseden cascade ozelligi sayesinde silinir
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getData("SELECT * FROM UYE", dataGridView1);
            getData("SELECT * FROM VUCUTOLC", dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UYE personToUpdate = new UYE();
            int id;

            string str = "SELECT * FROM UYE WHERE UyeID='" + textBox1.Text + "'";
            string str2 = "SELECT * FROM VUCUTOLC WHERE UyeID='" + textBox1.Text + "'";
            getData(str, dataGridView1);
            getData(str2, dataGridView2);

            id = Convert.ToInt32(textBox1.Text);
            using (var ctx = new SporSalonuContext())
            {
                personToUpdate = ctx.UYE.Where(s => s.UyeID == id).FirstOrDefault<UYE>();

                if (personToUpdate != null)
                {
                    textBox2.Text = personToUpdate.Ad;
                    textBox3.Text = personToUpdate.Soyad;
                    if (personToUpdate.Cinsiyet == "K")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;

                    dateTimePicker1.Text = Convert.ToString(personToUpdate.Dogum_tarihi);
                    comboBox1.SelectedItem = personToUpdate.Paket_secimi;
                    textBox4.Text = personToUpdate.Tel_no;
                    textBox5.Text = Convert.ToString(personToUpdate.Boy);
                    textBox6.Text = Convert.ToString(personToUpdate.GuncelKilo);
                    textBox7.Text = Convert.ToString(personToUpdate.HedefKilo);
                    textBox8.Text = Convert.ToString(personToUpdate.UyeID);
                }
                else
                {
                    MessageBox.Show("Bu kullanıcı idsine sahip üye bulunamadı!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           foreach (DataGridViewRow row in dataGridView1.SelectedRows)  //Datagridde seçili satırlar tek tek dolaşılır
            {
                int id = Convert.ToInt32(row.Cells[0].Value); // satırdaki PK olan UyeID alınıp, id değişkenine atanır
                removeMember(id); //Uyeyi silmek için olan metoda silinecek üyenin idsi parametre olarak geçirilir
            }
            //removeMember(Convert.ToInt32(textBox6.Text));
            MessageBox.Show("Kayıt başarıyla silindi!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UYE personToUpdate = new UYE();
            int id = Convert.ToInt32(textBox1.Text);

            using (var db = new SporSalonuContext())
            {
                personToUpdate = db.UYE.Where(s => s.UyeID == id).FirstOrDefault<UYE>();

                if(personToUpdate!=null)
                {
                    string str = "";
                    if (radioButton1.Checked)
                        str = "K";
                    else if (radioButton2.Checked)
                        str = "E";

                    personToUpdate.Ad = textBox2.Text;
                    personToUpdate.Soyad = textBox3.Text;
                    personToUpdate.Cinsiyet = str;
                    personToUpdate.Tel_no = textBox4.Text;
                    personToUpdate.Dogum_tarihi = dateTimePicker1.Value.Date;
                    personToUpdate.Paket_secimi = comboBox1.SelectedItem.ToString();
                    personToUpdate.Boy = Convert.ToInt32(textBox5.Text);
                    personToUpdate.GuncelKilo = Convert.ToSingle(textBox6.Text.Replace(".", ","));
                    personToUpdate.HedefKilo = Convert.ToSingle(textBox7.Text.Replace(".", ","));
                    personToUpdate.UyeID = Convert.ToInt32(textBox8.Text);

                    db.SaveChanges();
                    MessageBox.Show(DateTime.Now.ToLongDateString() + " tarihinde kayıt GÜNCELLENDİ");
                }
            }

            MessageBox.Show(DateTime.Now.ToLongDateString() + " tarihinde kayıt GÜNCELLENDİ");
            }

        private void button5_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            var person2 = new VUCUTOLC();
            using (var db = new SporSalonuContext())
            {
                person2 = db.VUCUTOLC.Where(s => s.UyeID == id).FirstOrDefault<VUCUTOLC>();
                person2.GunBiceps = Convert.ToSingle(textBox9.Text.Replace(".", ","));
                person2.GunBel = Convert.ToSingle(textBox10.Text.Replace(".", ","));
                person2.GunGogus = Convert.ToSingle(textBox11.Text.Replace(".", ","));
                person2.GunOmuz = Convert.ToSingle(textBox12.Text.Replace(".", ","));
                person2.GunBacak = Convert.ToSingle(textBox13.Text.Replace(".", ","));

                db.SaveChanges();
            };
        }
    }
 }
  
   

