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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proje
{
    public partial class RandevuPanel : Form
    {
        public void setTC(string tc)
        {
            textTC.Text = tc;
        }
        public RandevuPanel()
        {
            InitializeComponent();
            connection1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\visualrepos\\Proje\\Randevu.accdb;
                                            Persist Security Info=False;";
            connection2.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\visualrepos\\Proje\\Hasta.accdb;
                                            Persist Security Info=False;";

        }
        private OleDbConnection connection1 = new OleDbConnection();
        private OleDbConnection connection2 = new OleDbConnection();


        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection2.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection2;
                command.CommandText = "SELECT TC FROM Hasta WHERE TC='" + textTC.Text + "'";

                DateTime currentDateTime = DateTime.Now;
                DateTime appointmentDate = dateTimePicker1.Value;

                if (appointmentDate < currentDateTime)
                {
                    MessageBox.Show("Cannot add an appointment for a date that has already passed.");   
                    return;
                }
                DialogResult result = MessageBox.Show("Are you sure ?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }


                OleDbDataReader reader = command.ExecuteReader();
                bool nameExists = false;
                while (reader.Read())
                {
                    nameExists = true;
                }
                reader.Close();
                connection2.Close();

                try
                {
                    connection1.Open();
                    OleDbCommand command1 = new OleDbCommand();
                    command.Connection = connection1;
                    command.CommandText = "SELECT * FROM Randevu WHERE Date='" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' AND Time='" + comboBox1.SelectedItem.ToString() + "' AND DoctorName='" + DNComboBox.SelectedItem.ToString() + "'";
                    OleDbDataReader reader1 = command.ExecuteReader();
                    if (reader1.Read())
                    {
                        MessageBox.Show("An appointment already exists at the selected date, time, and with the selected doctor.");
                        connection1.Close();
                        return;
                    }
                    reader1.Close();
                    connection1.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                }

            if (nameExists)
                {
                    // Add the appointment
                    connection1.Open();
                    OleDbCommand command2 = new OleDbCommand();
                    command2.Connection = connection1;
                    command2.CommandText = "INSERT INTO [Randevu] ([TC], [Date], [Time], [DoctorName]) values (@tc,@date,@time,@dname)";
                    command2.Parameters.AddWithValue("@tc", textTC.Text);
                    command2.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                    command2.Parameters.AddWithValue("@time", comboBox1.SelectedItem.ToString());
                    command2.Parameters.AddWithValue("@dname", DNComboBox.SelectedItem.ToString());
                    command2.ExecuteNonQuery();
                    MessageBox.Show("Appointment Added!");
                    connection1.Close();
                }
                else
                {
                    MessageBox.Show("TC not found in database.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DNComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void showButton_Click(object sender, EventArgs e)
        {
            try
            {
                connection1.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection1;
                command.CommandText = "SELECT * FROM Randevu WHERE Date='" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "'";
                OleDbDataReader reader = command.ExecuteReader();
                listView1.Items.Clear();
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["Date"].ToString());
                    item.SubItems.Add(reader["Time"].ToString());
                    item.SubItems.Add(reader["DoctorName"].ToString());
                    listView1.Items.Add(item);
                }
                reader.Close();
                connection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void RandevuPanel_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Randevularim frm1 = new Randevularim();
            frm1.Show();
            frm1.getTC(textTC.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out ?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            Giris frm2 = new Giris();
            frm2.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit ?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }
            this.Close();
        }
    }
}
