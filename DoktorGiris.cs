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

namespace Proje
{
    public partial class DoktorGiris : Form
    {
        public DoktorGiris()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\visualrepos\Proje\Doktor.accdb;
            Persist Security Info=False;";
        }
        private OleDbConnection connection = new OleDbConnection();
        private void button2_Click(object sender, EventArgs e)
        {
            {
                Giris frm = new Giris();
                frm.Show();
                this.Hide();
            }
        }

        private void txtTC_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "select * from Doctor where TC='" + txtTC.Text + "' and Password='" + txtPassword.Text + "'";

                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    MessageBox.Show("TC and password are correct You are going to Doctor Panel");
                    DoktorPanel frm = new DoktorPanel();
                    frm.Show();
                    this.Hide();

                }
                else if (count > 1)
                {
                    MessageBox.Show("Duplicate username and password");
                }
                else
                {
                    MessageBox.Show("TC and password are not match please try again ");
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
