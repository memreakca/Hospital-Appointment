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
using System.Windows.Forms.DataVisualization.Charting;


namespace Proje
{
    public partial class DoktorOran : Form
    {
        
        public DoktorOran()
        {
            InitializeComponent();
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =D:\\visualrepos\\Proje\\Randevu.accdb";
            OleDbConnection connection = new OleDbConnection(connectionString);

            try
            {
                // Open the connection
                connection.Open();

                // Execute a SQL query to retrieve the data for the males and females
                string sql = "SELECT DoctorName, COUNT(*) FROM Randevu GROUP BY DoctorName";
                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                // Add a series to the chart for each gender
                while (reader.Read())
                {
                    string dname = reader.GetString(0);
                    int count = reader.GetInt32(1);

                    Series series = chart1.Series.Add(dname);
                    series.Points.Add(count);
                }
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                connection.Close();
            }
        }


    

       
        }
    }

