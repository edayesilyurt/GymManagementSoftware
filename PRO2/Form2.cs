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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            label3.Visible = false; 
            pictureBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String isim = textBox1.Text;
                double fiyat = Convert.ToDouble(textBox2.Text);
                int ay = Convert.ToInt32(numericUpDown1.Value);
                XMLPaket.PaketEkle(isim,ay, fiyat); //Kullanıcıdan alınan veriler metoda gönderilir ve paket XML uzantılı dosyaya yazılır
                label3.Visible = true; // İşlemin başarılı olduğuna dair mesaj visible yapılır
                pictureBox1.Visible = true;
            }
            catch
            {
                MessageBox.Show("Giriş uygun formatta değildi! Lutfen boş alanları doldurunuz..");
            }
        }
    }
}
