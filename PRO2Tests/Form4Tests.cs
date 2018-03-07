using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRO2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRO2.Tests
{
    [TestClass()]
    public class Form4Tests
    {
        
        [TestMethod()]
        public void button2_ClickTest()
        {
            double aylikCiro = 0;
            
            /*for (int i = 0; i < listView2.Items.Count; i++) //Listviewdeki tüm değerleri alabilmek için for kullandık
            {
                aylikCiro += Convert.ToDouble(listView2.Items[i].SubItems[1].Text.ToString());//SubItems değeri 3 sütun olduğu için 1. sütun ise ödeme miktarını göstermekte
            }*/

            

            aylikCiro += aylikCiro;
            Assert.AreNotEqual(0, aylikCiro);
            Assert.Fail();
        }
    }
}