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
                MySqlDataAdapter sqlDataAdapt = null;
                DataSet aptData = null;
                DataTable aptDataTab = null;

                try 
                {
                    MYSQLconn = new MySqlConnection(mySqlConn);
                    MYSQLconn.Open();

                      string stm = "select patient_id,appt_begin,appt_end,appt_reason from appointment";   
                      //MySqlCommand cmd = new MySqlCommand(stm, MYSQLconn);

                      sqlDataAdapt = new MySqlDataAdapter(stm, MYSQLconn);
                      aptData = new DataSet();

                      aptDataTab = new DataTable();
                      sqlDataAdapt.Fill(aptDataTab);
                      
                     // sqlDataAdapt.Fill(aptData,"Appointments");
                      

                    //  dataGridView1.DataSource = aptData.Tables["Appointments"];
                      dataGridView1.DataSource = aptDataTab;
                      

                      //mysqlRead =  cmd.ExecuteReader();

                    //while(mysqlRead.Read())
                    //{ 

                    //}

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
      


           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            int row = dataGridView1.SelectedCells[0].RowIndex;
            string curRow = dataGridView1.Rows[row].Cells[0].Value.ToString();
            MessageBox.Show("row selected "+curRow);
            load_Patient(curRow);
        }

        /*
         *  Method to load a given patient record and 
         *  populate the appropriate fields in the chart 
         * 
         * 
         *  @ author Kwesi Amable 
         * */

        private void load_Patient(string patID)
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

                string patSql = "select * from patient join person on person.person_id = patient.patient_id where patient.patient_id =" + patID; // select patient related data
                cmd.CommandText = patSql;

                readr = cmd.ExecuteReader();

                // get resultset 

                while (readr.Read())
                {
                    int personId = Decimal.ToInt32(readr.GetDecimal(0));
                    int patId = Decimal.ToInt32(readr.GetDecimal(1));
                    string insurNum = readr.GetString(2);
                    string insurCode = readr.GetString(3);
                    int personId2 = Decimal.ToInt32(readr.GetDecimal(4));
                    string fname = readr.GetString(5);
                    string lname = readr.GetString(6);
                    string mname = readr.GetString(7);
                    DateTime dob = readr.GetDateTime(8);



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }
  



     
    }
}
