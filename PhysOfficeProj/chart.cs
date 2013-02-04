﻿using System;
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
           // MessageBox.Show("row selected "+curRow);
            load_Patient(curRow);
        }

        private void getAllergies(int person)
        {
            OdbcConnection conn = new OdbcConnection();
            OdbcCommand cmd = new OdbcCommand();
            OdbcDataReader readr;

            conn.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
            conn.Open();
            cmd.Connection = conn;

            string allergySql = "select allergy_description from allergy where person_id = "+ person; // select patient related data
            cmd.CommandText = allergySql;

            readr = cmd.ExecuteReader();
            
            while(readr.Read())
            {
                String allergy = readr.GetString(0);

                allergyTxt.Text = allergy;
            }

            conn.Close();

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

                int CURRENT_PAT = 0;
                int CURRENT_PER = 0;

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
                    string gender = readr.GetString(9);
                    string address = readr.GetString(10);
                    string state = readr.GetString(11);
                    string city = readr.GetString(12);
                    int zip = Decimal.ToInt32(readr.GetDecimal(13));
                    Int64 wrkPhone = readr.GetInt64(14);
                    Int64 hmePhone = readr.GetInt64(15);
                    Int64 pager = readr.GetInt64(16);
                    string email1 = readr.GetString(17);
                    string email2 = readr.GetString(18);


                    // assign the values to data elements on GUI 
                    nameTxt.Text = fname + "  " + mname + "  " + lname;
                    addrTxt.Text = address;
                    stateTxt.Text = state.ToString();
                    zipTxt.Text = zip.ToString();
                    genderTxt.Text = gender;
                    hmePhoneTxt.Text = hmePhone.ToString();
                    wrkPhoneTxt.Text = wrkPhone.ToString();
                    emailTxt.Text = email1;


                    // track the current patient 
                    CURRENT_PAT = patId;
                    CURRENT_PER = personId;

                    //MessageBox.Show(fname + mname + lname);       

                }

                conn.Close();

                getAllergies(CURRENT_PER);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.SelectedCells[0].RowIndex;
            string curRow = dataGridView1.Rows[row].Cells[0].Value.ToString();
            // MessageBox.Show("row selected "+curRow);
            load_Patient(curRow);
        }
  



     
    }
}
