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
    public partial class Login : Form
    {
        public static ROL me = new ROL();
        public Login()
        {
            InitializeComponent();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var isNumeric = Int32.TryParse(textBox2.Text, out var userpassword);
            if (!isNumeric)
                MessageBox.Show("Başarısız giriş! ID'niz sadece sayısal karakter içermelidir!");

            ROL user = new ROL();
     
            using (var db = new SporSalonuContext())
            {
                user = db.ROL.Where(s => s.KullanıcıID == userpassword).FirstOrDefault<ROL>();
            }

            if (user != null)
            {
                this.Hide();
                Form1 myform = new Form1();
                myform.label2.Text = "ID : " + Convert.ToString(user.KullanıcıID);
               
                me.KullanıcıID = user.KullanıcıID;
                me.KullanıcıRolü = user.KullanıcıRolü;
   
                if (user.KullanıcıRolü == "Üye")
                {
                    myform.Show();
                    myform.yöneticiYetkileriToolStripMenuItem.Visible = false;
                    myform.eğitmenYetkileriToolStripMenuItem.Visible = false;
                    MessageBox.Show("Üye girişi yapıldı! Hoşgeldiniz!");
                }
                else if (user.KullanıcıRolü == "Eğitmen")
                {
                    myform.Show();
                    myform.yöneticiYetkileriToolStripMenuItem.Visible = false;
                    MessageBox.Show("Eğitmen girişi yapıldı! Hoşgeldiniz!");
                }
                else if (user.KullanıcıRolü == "Yönetici")
                {
                    myform.Show();
                    MessageBox.Show("Yönetici girişi yapıldı! Hoşgeldiniz!");
                }
            }
            else if(user==null)
            {
                MessageBox.Show("Böyle bir kayıt bulunamadı.Lütfen tekrar deneyiniz!","Uyarı", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);  
                textBox2.Clear();
            }
        }
    }
}
