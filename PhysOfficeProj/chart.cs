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
            popLabPat();

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

                    string stm = "select patient_id as 'PATIENT',FROM_UNIXTIME(appt_begin) as 'APPT_BEGIN',FROM_UNIXTIME(appt_end) as 'APPT_END',appt_reason as 'CHIEF COMPLAINT' from appointment";   
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


      /*  private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            int row = dataGridView1.SelectedCells[0].RowIndex;
            string curRow = dataGridView1.Rows[row].Cells[0].Value.ToString();
           // MessageBox.Show("row selected "+curRow);
            load_Patient(curRow);
        }*/

        private void getMeds(int visit)
        {
           
            try
            {
                OdbcConnection conn = new OdbcConnection();
                OdbcCommand cmd = new OdbcCommand();
                OdbcDataAdapter medAdapt;
                DataSet medData = null;
                DataTable medDataTab = null;

                conn.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
                conn.Open();
                cmd.Connection = conn;

                string medSql = "select m.med_name, m.med_dose,m.med_dose_unit, m.med_frequency, m.units, m.med_route from medication m " +
                                 "join physical_exam p on p.phy_ex# = m.phy_ex# join visit v on v.visit# = p.visit# where p.visit# = "+ visit;


                medAdapt = new OdbcDataAdapter(medSql, conn);
                medData = new DataSet();

                medDataTab = new DataTable();
                medAdapt.Fill(medDataTab);

                // sqlDataAdapt.Fill(aptData,"Appointments");


                //  dataGridView1.DataSource = aptData.Tables["Appointments"];
              medDataGrid.DataSource = medDataTab;
               

       

                conn.Close();

                getHistory(visit);

            }
            catch (Exception ex)
            {

            }
            
                
        }


        private void getHistory(int visit)
        {
            OdbcConnection conn = new OdbcConnection();
            OdbcCommand cmd = new OdbcCommand();
            OdbcDataReader readr;

            conn.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
            conn.Open();
            cmd.Connection = conn;

            string vitalSql = " select phy_hist from physical_exam where visit# = " + visit;

            cmd.CommandText = vitalSql;

            readr = cmd.ExecuteReader();

            while (readr.Read())
            {
                string history = readr.GetString(0);
                histTxtArea.Text = history;
            }

            
        }

        private void  getVitals(int person)
        {
            try
            {
                OdbcConnection conn = new OdbcConnection();
                OdbcCommand cmd = new OdbcCommand();
                OdbcDataReader readr;

                conn.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
                conn.Open();
                cmd.Connection = conn;


                string vitalSql = "select s.blood_pressure, s.pulse,s.respiration, s.weight, s.temperature,s.visit# from vital_signal s " +
                                   "join visit v on v.visit#  = s.visit# " +
                                    "join  person p on p.person_id = v.person_id where p.person_id =" + person;

                cmd.CommandText = vitalSql;

                readr = cmd.ExecuteReader();

                int CURRVIST = 0;

                while (readr.Read())
                {
                    string blodPres = readr.GetString(0);
                    int pulse = Decimal.ToInt32(readr.GetDecimal(1));
                    int respire = Decimal.ToInt32(readr.GetDecimal(2));
                    int weight = Decimal.ToInt32(readr.GetDecimal(3));
                    int heat = Decimal.ToInt32(readr.GetDecimal(4));
                    CURRVIST = Decimal.ToInt32(readr.GetDecimal(5));

                    //populate feilds iu chart 
                    bp1.Text = blodPres;
                    plseTxt.Text = pulse.ToString();
                    resprTxt.Text = respire.ToString();
                    heavTxt.Text = weight.ToString();
                    hotTxt.Text = heat.ToString();
 
                    

                }

                conn.Close();
                getMeds(CURRVIST);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }

        }





        private void getAllergies(int person)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
            

           
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
                    dobTxt.Text = dob.ToShortDateString();

                    //patCombo.Items.Add(fname + "  " + mname + "  " + lname);

                    // track the current patient 
                    CURRENT_PAT = patId;
                    CURRENT_PER = personId;

                    //MessageBox.Show(fname + mname + lname);       

                }

                conn.Close();

                getAllergies(CURRENT_PER);// get allergy information 
                getVitals(CURRENT_PAT);   // get vital signs 

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }

        /*
         *  Method to populate lab order field with 
         *  exsisting patients 
         * 
         * */

       private void popLabPat()
       {


           try
           {
               OdbcConnection conn = new OdbcConnection();
               OdbcCommand cmd = new OdbcCommand();
               OdbcDataReader readr;

               conn.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
               conn.Open();
               cmd.Connection = conn;

               string patCombSql = "Select person_id,fname, midname, lname from person ";

               cmd.CommandText = patCombSql;

               readr = cmd.ExecuteReader();

               while (readr.Read())
               {

                   int Id = Decimal.ToInt32(readr.GetDecimal(0));
                   string fname = readr.GetString(1);
                   string lname = readr.GetString(2);
                   string mname = readr.GetString(3);

                   patCombo.Items.Add(fname+" "+mname+" "+lname);
               }

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


        private void patCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(patCombo.SelectedIndex.ToString());


        }


        private void labOrderBut_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(patCombo.SelectedIndex.ToString());

                    OdbcConnection conn = new OdbcConnection();
                    OdbcCommand cmd = new OdbcCommand();
                    OdbcDataReader readr;

                    OdbcConnection connIns = new OdbcConnection();
                    OdbcCommand cmdIns = new OdbcCommand();
                   


                    OdbcParameter p1;
                    OdbcParameter p2;
                    //OdbcParameter p3;


           int lastVisit=0;

            if (patCombo.SelectedIndex < 0 || testCombo.SelectedIndex < 0 || labDatePick.Text == " " || statCombo.SelectedIndex < 0)
            {
                MessageBox.Show("Please select Patient,Test and Date");
            }
            else
            {
                        try 
                        {


                           conn.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
                           conn.Open();
                           cmd.Connection = conn;

                            string lastVisitSql = "select max(visit#) from visit ";

                            cmd.CommandText = lastVisitSql;

                           readr = cmd.ExecuteReader();

                           while (readr.Read())
                           {
                               lastVisit = readr.GetInt16(0);
                           }

                           conn.Close();
                           readr.Close();

                                                      
                           
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("error" + ex.Message); 
                    }


                        try
                        {


                            /// insert query 

                            connIns.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
                            connIns.Open();
                            cmdIns.Connection = connIns;

                            cmdIns.CommandText = "INSERT INTO PHYS_ADMIN.VISIT (VISIT#,PERSON_ID,ADMIT_DATE) VALUES (:VISIT#,:PERSON_ID,:ADMIT_DATE)";
                            cmdIns.Prepare();

                            lastVisit = lastVisit + 1;
                            cmdIns.Parameters.AddWithValue("VISIT#",lastVisit);
                            cmdIns.Parameters.AddWithValue("PERSON_ID",patCombo.SelectedIndex+1);
                            cmdIns.Parameters.AddWithValue("ADMIT_DATE",labDatePick.Value);
                                                                                

                            cmdIns.ExecuteNonQuery();
                            cmdIns.Parameters.Clear();

                            cmdIns.CommandText = "Insert into PHYS_ADMIN.LAB_ORDER(LAB_ORDER#,VISIT#,COLLECT_DATE,STATUS,NOTES,TEST_NAME) values (:LAB_ORDER#,:VISIT#,:COLLECT_DATE,:STATUS,:NOTES,:TEST_NAME)";

                            cmdIns.Parameters.AddWithValue("VISIT#", lastVisit);
                            cmdIns.Parameters.AddWithValue("PERSON_ID", patCombo.SelectedIndex + 1);
                            cmdIns.Parameters.AddWithValue("COLLECT_DATE", labDatePick.Value);
                            cmdIns.Parameters.AddWithValue("STATUS",statCombo.SelectedValue );
                            cmdIns.Parameters.AddWithValue("NOTES", testNotArea.Text);
                            cmdIns.Parameters.AddWithValue("TEST_NAME",testCombo.SelectedValue);
                            
                            

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("error" + ex.Message); 
                        }

               


                
            }
        }

     

    

     
  



     
    }
}
