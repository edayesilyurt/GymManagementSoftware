using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PRO2
{
     class XMLPaket
    {
        private static string KlasorYolu = @"C:\Users\edaye\source\repos\PRO2\PRO2";
        private static string XmlYolu = Path.Combine(KlasorYolu, "XmlFile1.xml");

        public static void KlasorYarat()
        {
            if (!Directory.Exists(KlasorYolu))
                Directory.CreateDirectory(KlasorYolu);
        }

        public static void XmlYarat()
        {
            KlasorYarat();

            if (!File.Exists(XmlYolu))
            {
                var file = File.Create(XmlYolu);
                file.Close();
            }
        }

        public static List<UyelikPaket> XmlOku()
        {
            List<UyelikPaket> paketlerList = new List<UyelikPaket>();
            
            XmlTextReader xmlTR = new XmlTextReader(XmlYolu);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlTR);
            xmlTR.Close(); // Xml dosyası başka yerde kullanılabilsin diye XmlTextReader kapatılır

            XmlNodeList xmlNL = xmlDoc.GetElementsByTagName("Paket");//XML belgesindeki "PAKET" adında ne kadar etiket varsa bu etiketin bilgilerini alıyoruz.

            foreach (XmlNode item in xmlNL)
            {
                UyelikPaket okunanPaket = new UyelikPaket();
                okunanPaket.paketIsmi = item["PaketIsmi"].InnerText;  
                okunanPaket.aylikUcret = Convert.ToDouble((item["AylikFiyat"].InnerText), CultureInfo.GetCultureInfo("en-us"));
                okunanPaket.uyelikSuresi = Convert.ToInt32(item["UyelikSuresi"].InnerText);
                paketlerList.Add(okunanPaket); // "PAKET" etiketinin içinde bulunan yer alan değerleri listeye ekliyoruz  
            }
            return paketlerList;
                
        }

        public static void PaketEkle(string isim, int ay, double fiyat)
        {
            XmlYarat();

            XElement yeni_paket = new XElement("Paket",
                                    new XElement("PaketIsmi", isim),
                                    new XElement("UyelikSuresi", ay),
                                    new XElement("AylikFiyat",fiyat)
                                        );

            FileStream stream = File.Open(XmlYolu, FileMode.Open);
            XDocument xml = XDocument.Load(stream);
            
            stream.Close();
            xml.Root.Add(yeni_paket);
            xml.Save(XmlYolu);
        }


        public static void PaketSil(string silinecek)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlYolu);

            foreach (XmlNode node in doc.SelectNodes("/Paketler/Paket")) //Xpath ile node bulunur
            {
                if (silinecek.Contains(node.SelectSingleNode("PaketIsmi").InnerText)) //Paket ismi silinecek isimli stringe eşit ise
                {
                    node.ParentNode.RemoveChild(node); // childnodeları ile beraber silinir
                }
            }
            doc.Save(XmlYolu);   
        }

    }
}
