using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO2
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            if (Login.me.KullanıcıRolü == "Üye")
            {
                button1.Visible = false;
                textBox1.Text = Login.me.KullanıcıID.ToString();
                textBox1.ReadOnly = true;
                string str = "SELECT * FROM UYE WHERE UyeID='" + textBox1.Text + "'";
                getData(str, dataGridView1);
                string str2 = "SELECT * FROM VUCUTOLC WHERE UyeID='" + textBox1.Text + "'";
                getData(str2, dataGridView2);
            }
            else
                button1.Visible = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "SELECT * FROM UYE WHERE UyeID='" + textBox1.Text + "'";
            getData(str, dataGridView1);
            string str2 = "SELECT * FROM VUCUTOLC WHERE UyeID='" + textBox1.Text + "'";
            getData(str2, dataGridView2);
        }
    }
}
