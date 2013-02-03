using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace PhysOfficeProj
{
    public partial class chart : Form
    {

        
//        string msg1 = "";
//        OdbcParameter p1; //LBE15

        public chart()
        {
            InitializeComponent();

        }

      



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

  

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

  

        private void patCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chart_Load(object sender, EventArgs e)
        {
            // on load of window make database connections 
            try
            {
                OdbcConnection conn = new OdbcConnection();
                OdbcCommand cmd = new OdbcCommand();
                OdbcDataReader readr;

                conn.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
                conn.Open();
                cmd.Connection = conn;

                string patSql = "select * from patient";
                cmd.CommandText = patSql;

                readr = cmd.ExecuteReader();

                // get resultset 

                while (readr.Read())
                {
                    int personId = Decimal.ToInt32(readr.GetDecimal(0));
                    int patId = Decimal.ToInt32(readr.GetDecimal(1));
                    string insurNum = readr.GetString(2);
                    string insurCode = readr.GetString(3);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
            

        }

     
    }
}
