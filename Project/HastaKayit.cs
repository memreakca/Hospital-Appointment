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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Proje
{
    public partial class HastaKayit : Form
    {
        public HastaKayit()
        {
            InitializeComponent();
        }
        
            OleDbConnection Aconnection = new
            OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source =D:\\visualrepos\\Proje\\Hasta.accdb");
        private void HastaKayit_Load(object sender, EventArgs e)
        {

        }

       

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Giris frm = new Giris();
            frm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        


        private void button1_Click(object sender, EventArgs e)
        {
            string TC = textTC.Text;
            string checkUsernameSql = "SELECT COUNT(*) FROM Hasta WHERE TC = @tc";
            OleDbCommand checkUsernameCommand = new OleDbCommand(checkUsernameSql, Aconnection);
            checkUsernameCommand.Parameters.AddWithValue("@tc", TC);

            try
            {
                Aconnection.Open();
                int count = (int)checkUsernameCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("This TC is already been Signed Up.");
                    return;
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Aconnection.Close();
            }
            Aconnection.Open();
            OleDbCommand AccessCommand = new OleDbCommand();
            AccessCommand.Connection = Aconnection;
            AccessCommand.CommandText = "insert into [Hasta]([TC],[Password],[PatientName],[PatientSurname],[Gender],[Email]) VALUES " +
                "(@TC,@Password,@PatientName,@PatientSurname,@Gender,@Email)";
            AccessCommand.Parameters.Add("@TC", textTC.Text);
            AccessCommand.Parameters.Add("@Password", textPassword.Text);
            AccessCommand.Parameters.Add("@PatientName", textName.Text);
            AccessCommand.Parameters.Add("@PatientSurname", textSurname.Text);
            AccessCommand.Parameters.Add("@Gender", comboBox1.SelectedItem.ToString());
            AccessCommand.Parameters.Add("@Email", textEmail.Text);

           


            AccessCommand.ExecuteNonQuery();
            Aconnection.Close();
            MessageBox.Show("You have been signed up , now you are going to Home page ","PATIENT APPOINTMENT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Giris frm = new Giris();
            frm.Show();
            this.Hide();
        }
    }
}
