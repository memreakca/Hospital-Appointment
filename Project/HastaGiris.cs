using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\visualrepos\Proje\Hasta.accdb;
            Persist Security Info=False;";
        }
        private OleDbConnection connection = new OleDbConnection();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            Giris frm = new Giris();
            frm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "select * from Hasta where TC='" + txtTC.Text + "' and Password='" + txtPassword.Text + "'";

                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    MessageBox.Show("TC and password are correct You are going to Appointment Panel");
                    RandevuPanel frm = new RandevuPanel();
                    frm.Show();
                    frm.setTC(txtTC.Text);
                    this.Hide();

                }
                else if (count > 1)
                {
                    MessageBox.Show("Duplicate username and password");
                }
                else
                {
                    MessageBox.Show("TC and password are not match please try again or if you dont have account please create");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
        }
    }
}
//OleDbConnection db_conn;
//OleDbCommand db_comn;
//db_conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source =D:\\visualrepos\\Proje\\Hasta.accdb");
//db_conn.Open();
//string que = "Select COUNT (*) FROM Hasta WHERE TC-@utc and Password-@pa";
//db_comn = new OleDbCommand(que, db_conn);
//db_comn.Parameters.Add("@utc", OleDbType.VarChar).Value = txtTC.Text;
//db_comn.Parameters.Add("@pa", OleDbType.VarChar).Value = txtPassword.Text;
//int count = (int)db_comn.ExecuteScalar();
//db_conn.Close();
//db_conn = null;
//if (count <= 0)
//{
//    MessageBox.Show("User not exists");
//}
//else
//{
//    MessageBox.Show("You have been logged in");
//}
