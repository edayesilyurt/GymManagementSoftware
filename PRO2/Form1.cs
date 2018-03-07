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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();          
        }

        private void childForm(Form _childForm)
        {
            bool durum = false;// ekranda açık olan formu bulmak için

            foreach (var frm in this.MdiChildren)
            {
                if (frm.Text == _childForm.Text)
                {
                    durum = true;
                    _childForm.Activate();
                }
                else
                {
                    frm.Close();
                }
            }

            if (durum == false)
            {
                _childForm.MdiParent = this;
                _childForm.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Tarih :" + DateTime.Today.ToShortDateString();
        }

        private void eğitmenYetkileriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void çıkişYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void paketEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm(new Form2());
        }

        private void paketSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm(new Form3());
        }

        private void üyeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm(new Form6());
        }

        private void eğitmenEkleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            childForm(new Form5());
        }

        private void uyeBilgisiGüncellemeVeSilmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm(new UyeEkleGuncelle7());
        }

        private void ödemeAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm(new OdemeAl8());
        }

        private void ciroGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm(new Form4());
        }

        private void bigiGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm(new Form9());
        }

        private void üyelikGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm(new Form10());
        }
    }
}

