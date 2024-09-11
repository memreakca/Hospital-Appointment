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
    public partial class Cinsiyet : Form
    {
        public Cinsiyet()
        {
            InitializeComponent();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =D:\\visualrepos\\Proje\\Hasta.accdb";
            OleDbConnection connection = new OleDbConnection(connectionString);

            try
            {
                // Open the connection
                connection.Open();

                // Execute a SQL query to retrieve the data for the males and females
                string sql = "SELECT Gender, COUNT(*) FROM Hasta GROUP BY Gender";
                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                // Add a series to the chart for each gender
                while (reader.Read())
                {
                    string gender = reader.GetString(0);
                    int count = reader.GetInt32(1);

                    Series series = chart1.Series.Add(gender);
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

        private void Cinsiyet_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
