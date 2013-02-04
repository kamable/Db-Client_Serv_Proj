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
using MySql.Data.MySqlClient;

namespace PhysOfficeProj
{
    public partial class chart : Form
    {

        
//        string msg1 = "";
//        OdbcParameter p1; //LBE15

        public chart()
        {
            InitializeComponent();
            getAppointmentData();

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



            /*
             * Method to get appointment data from mysql database 
             * poulate data grid  
             * 
             **/


        public void getAppointmentData()
        {
            string mySqlConn =  @"server=localhost;userid=root;
            password=;database=ereceptionist_mysql";

           

                MySqlConnection MYSQLconn = null;
                MySqlDataReader mysqlRead = null;

                try 
                {
                    MYSQLconn = new MySqlConnection(mySqlConn);
                    MYSQLconn.Open();

                      string stm = "select * from appointment";   
                      MySqlCommand cmd = new MySqlCommand(stm, MYSQLconn);

                      mysqlRead =  cmd.ExecuteReader();

                    while(mysqlRead.Read())
                    { 

                    }

                   // MessageBox.Show("MySQL version : {1}", version);

                } catch (MySqlException ex) 
                {
                    MessageBox.Show("Error: {0}",  ex.ToString());

                } finally 
                {          
                    if (MYSQLconn != null) 
                    {
                        MYSQLconn.Close();
                    }
                }
            

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
