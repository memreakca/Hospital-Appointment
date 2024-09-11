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

namespace Proje
{
    public partial class Randevularim : Form
    {
        string Tc;
        public void getTC(string tc)
        {
            Tc = tc;
        }

        public Randevularim()
        {
            InitializeComponent();
            connection1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\visualrepos\\Proje\\Randevu.accdb;
                                            Persist Security Info=False;";
        }
        private OleDbConnection connection1 = new OleDbConnection();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection1.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection1;
                command.CommandText = "SELECT * FROM Randevu WHERE TC='" + Tc + "'";
                OleDbDataReader reader = command.ExecuteReader();
                listView1.Items.Clear();
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["TC"].ToString());
                    item.SubItems.Add(reader["Date"].ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an appointment to cancel.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to cancel selected appointment?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                connection1.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection1;
                command.CommandText = "DELETE FROM Randevu WHERE TC='" + listView1.SelectedItems[0].Text + "' AND Time='" + listView1.SelectedItems[0].SubItems[2].Text +"'";
                command.ExecuteNonQuery();
                connection1.Close();
                MessageBox.Show("Appointment Cancelled!");
                // Refresh the ListView
                button1_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
    }
}
