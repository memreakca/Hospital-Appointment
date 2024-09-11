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
    public partial class DoktorPanel : Form
    {
        private OleDbConnection connection1 = new OleDbConnection();
        private OleDbConnection connection2 = new OleDbConnection();
        public DoktorPanel()
        {
            InitializeComponent();
            connection1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\visualrepos\\Proje\\Randevu.accdb;
                                            Persist Security Info=False;";
            connection2.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\visualrepos\\Proje\\Hasta.accdb;
                                            Persist Security Info=False;";
        }
        
        private void Doktor_Load(object sender, EventArgs e)
        {
            
        }

        private void DoktorPanel_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cinsiyet f = new Cinsiyet();
            f.Show();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection1.Open();
                OleDbCommand command1 = new OleDbCommand();
                command1.Connection = connection1;
                command1.CommandText = "SELECT * FROM Randevu WHERE Date='" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "'";

                connection2.Open();
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection2;

                OleDbDataReader reader1 = command1.ExecuteReader();
                listView1.Items.Clear();
                while (reader1.Read())
                {
                    command2.CommandText = "SELECT TC,PatientName,PatientSurname FROM Hasta WHERE TC ='" + reader1["TC"].ToString() + "'";
                    OleDbDataReader reader2 = command2.ExecuteReader();
                    if (reader2.Read())
                    {
                        ListViewItem item = new ListViewItem(reader2["TC"].ToString());
                        item.SubItems.Add(reader2["PatientName"].ToString());
                        item.SubItems.Add(reader2["PatientSurname"].ToString());
                        item.SubItems.Add(reader1["DoctorName"].ToString());
                        item.SubItems.Add(reader1["Date"].ToString());
                        item.SubItems.Add(reader1["Time"].ToString());
                        listView1.Items.Add(item);
                    }
                    reader2.Close();
                }
                reader1.Close();
                connection1.Close();
                connection2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an appointment to delete.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected appointment?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                connection1.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection1;
                command.CommandText = "DELETE FROM Randevu WHERE TC='" + listView1.SelectedItems[0].Text + "' AND Time='" + listView1.SelectedItems[0].SubItems[5].Text + "' AND Date='" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "'";
                command.ExecuteNonQuery();
                connection1.Close();

                // Refresh the ListView
                button2_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoktorOran fr = new DoktorOran();
            fr.Show();
        }
    }
}
