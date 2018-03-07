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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            label2.Visible = false;
            pictureBox1.Visible = false;
            xmlOkuChekboxaYaz();
        }

        private void xmlOkuChekboxaYaz()
        {
            List<UyelikPaket> liste = new List<UyelikPaket>();

            liste = XMLPaket.XmlOku();

            for (int i = 0; i < liste.Count; i++)
            {
                checkedListBox1.Items.Add(liste[i].paketIsmi + " - " + liste[i].aylikUcret + " TL" ); // xml dosyasından okuyup döndürülen değerler checkboxa eklenir
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (string itemChecked in checkedListBox1.CheckedItems) //CneckedListBoxta seçilen itemler tek tek dolaşılır
                XMLPaket.PaketSil(itemChecked); // Silinecek item silme metoduna parametre olarak geçirilir
            if (checkedListBox1.CheckedItems.Count == 0)
                MessageBox.Show("Lütfen seçim yapınız!");
            else
            {
                label2.Visible = true;
                pictureBox1.Visible = true;
                checkedListBox1.Items.Clear();
                xmlOkuChekboxaYaz(); //Silinen paketlerin görünmeye devam etmemsi için, xml dosyasından veriler yeniden okunur ve checkedlistboxa eklenir
            }                          
        }
    }
}
