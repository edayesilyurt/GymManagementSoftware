using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
///using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
       

        public void populateListView(string sqlcommand, ListView listView)
        {
            using (var db = new SporSalonuContext())
            {
                listView.Items.Clear();
                var odemeList = db.ODEME.SqlQuery(sqlcommand).ToList<ODEME>(); //sqlcommand işletilir ve elde edilen sonuç odemeLıst isimli listeye atılır

                foreach(ODEME x in odemeList) //odemeList içindeki veriler satır satır ListViewe yazılır
                {
                    ListViewItem newItem = new ListViewItem(x.OdeyenID.ToString());
                    newItem.SubItems.Add(x.OdenenMiktar.ToString());
                    newItem.SubItems.Add(x.OdemeTarihi.ToShortDateString());
                    newItem.SubItems.Add(x.UYE.Ad);
                    newItem.SubItems.Add(x.UYE.Soyad);
                    listView.Items.Add(newItem);
                }
            }
        }
        public double listViewOku()
        {
            double aylikCiro = 0;
            for (int i = 0; i < listView2.Items.Count; i++) //Listviewdeki tüm değerleri alabilmek için for kullandık
            {
                aylikCiro += Convert.ToDouble(listView2.Items[i].SubItems[1].Text.ToString());//SubItems değeri 3 sütun olduğu için 1. sütun ise ödeme miktarını göstermekte
            }
            return aylikCiro;
        }
            
        private void button1_Click(object sender, EventArgs e)
        {
            populateListView("SELECT * FROM ODEME", listView1); // Listelenmesi istenen sorgu populateListView metoduna geçirilir       
        }

        public void button2_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now; //Bulunduğumuz tarih now değişkenine atanır
            populateListView("SELECT * FROM ODEME WHERE (DATEPART(mm, OdemeTarihi)="+now.Month+")", listView2); //Bulunduğumuz ay içinde yapılan ödemelerin sorgusu metoda geçirilir

            label4.Text += listViewOku().ToString()  + "TL";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
        
            using (var db = new SporSalonuContext())
            {
                var odemelerList = db.ODEME.SqlQuery("SELECT * FROM ODEME WHERE(DATEPART(mm, OdemeTarihi) = "+now.Month+")").ToList();
                var tumUyelerList= db.UYE.SqlQuery("SELECT * FROM UYE").ToList(); 

                 foreach(UYE kisi in tumUyelerList)
                 {
                     int i = 0;

                    if(odemelerList.Count!=0)
                    {
                        foreach (ODEME kisi2 in odemelerList)
                        {
                            if (kisi.UyeID == kisi2.OdeyenID) //Üye kişisi ödeme yapanların bulundugu listede var mı diye bakılır
                            {
                                i++;
                            }
                        }

                        if (i == 0) // kisi bu ay içinde ödeme yapmamışsa
                        {
                            ListViewItem newItem = new ListViewItem(kisi.Ad.ToString());
                            newItem.SubItems.Add(kisi.Soyad.ToString());
                            newItem.SubItems.Add(kisi.Tel_no.ToString());
                            listView3.Items.Add(newItem); //listview3e eklenir
                        }
                    }
                    else if(odemelerList.Count==0) // ödeme yapan kisilerin listesi bossa
                    {
                        ListViewItem newItem = new ListViewItem(kisi.Ad.ToString()); 
                        newItem.SubItems.Add(kisi.Soyad.ToString());
                        newItem.SubItems.Add(kisi.Tel_no.ToString());
                        listView3.Items.Add(newItem); // bütün üyeler listview3e eklenir
                    }
                 }
            }
        }


        private void button4_Click(object sender, EventArgs e) //Uyeliği bu ay içinde sonlanacak, üyeliklerini yenilemesi gerek üyeleri bulmak için
        {
            List<UyelikPaket> liste = new List<UyelikPaket>();
            liste = XMLPaket.XmlOku();

            using (var db = new SporSalonuContext())
            {
                var tumUyelerList = db.UYE.SqlQuery("SELECT * FROM UYE").ToList(); //Veritabanındaki tüm üyeler listeye atılır

                foreach (UYE kisi in tumUyelerList)
                {
                    int ay=0;
                    for(int i=0;i<liste.Count;i++)
                    {
                        if (kisi.Paket_secimi == liste[i].paketIsmi)
                        {
                            ay = liste[i].uyelikSuresi; // Kişinin üyelik süresi bulunur, ay isimli değişkene atanır
                            break;
                        }        
                    }
                    DateTime today = DateTime.Today;
                    DateTime uyelikBaslangicTarihi = kisi.Kayit_tarihi;
                    DateTime uyelikSonlanmaTarihi = uyelikBaslangicTarihi.AddMonths(+ay); //Kişinin üyelik başlangıç tarihine, üyelik süresi eklenir

                    if (today.Month == uyelikSonlanmaTarihi.Month) // Eğer hesaplanan tarih değerindeki ay, içinde bulunduğumuz aya eşitse
                    {
                        ListViewItem newItem = new ListViewItem(kisi.Ad.ToString());
                        newItem.SubItems.Add(kisi.Soyad.ToString());
                        newItem.SubItems.Add(kisi.Tel_no.ToString());
                        listView4.Items.Add(newItem); // Kişinin bilgileri listView4'e eklenir
                    }                        
                }
                label2.Text = "Üyelik yenilemesi gereken üye sayısı : " + listView4.Items.Count;
            }
        }

    }
}
