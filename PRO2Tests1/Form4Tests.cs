using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRO2;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace PRO2.Tests
{
    [TestClass()]
    public class Form4Tests
    {
        [TestMethod()]
        public void listViewOkuTest()
        {
            float toplam = 0;
            double ciro = 0;
            string str1;
            
            Form4 myform = new Form4();
            ciro = myform.listViewOku();
            str1 = Convert.ToString(toplam);
            DateTime now = DateTime.Now;
           
            SqlConnection Conn = new SqlConnection("Data Source=DESKTOP-1NG718J;Initial Catalog=SporSalonu;Integrated Security=True");
            SqlCommand Comm = new SqlCommand(("SELECT OdenenMiktar FROM ODEME WHERE (DATEPART(mm, OdemeTarihi)=" + now.Month + ")"), Conn);
            Conn.Open();
            SqlDataReader DR = Comm.ExecuteReader();

            while (DR.Read())
            {
                toplam += DR.GetFloat(0);
            }

            Assert.AreEqual(str1, ciro.ToString());
        }

        [TestMethod()]
        public void XmlOkuTest()
        {
            string KlasorYolu = @"C:\Users\edaye\source\repos\PRO2\PRO2";
            string XmlYolu = Path.Combine(KlasorYolu, "XmlFile1.xml");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlYolu);

            string root = xmlDoc.DocumentElement.Name;
            
            Assert.AreEqual("Paketler", root);

        }
    }
}