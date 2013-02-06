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
    public partial class login : Form
    {
       
        public login()
        {
            InitializeComponent();
        }

        private void loginBut_Click(object sender, EventArgs e)
        {
            try
            {
                OdbcConnection conn = new OdbcConnection();
                OdbcCommand cmd = new OdbcCommand();
                OdbcDataReader readr;

                conn.ConnectionString = "dsn=Physician;" + "Pwd=shnake24;";
                conn.Open();
                cmd.Connection = conn;

                string loginSql = "select * from authentication a where a.name ='" + unameTxt.Text +"' and a.auth_code = '"+passTxt.Text+"'";

                cmd.CommandText = loginSql;

                readr = cmd.ExecuteReader();

                if (readr.FieldCount > 1)
                {
                    string uname = readr.GetString(0);
                    string pass = readr.GetString(1);

                    
                    this.Hide();
                    Application.Run(new chart());
                }
                else
                {
                    MessageBox.Show("Invalid Credentials");
                }
            }
            catch(Exception ex)
            {
                 
            }
        }
    }
}
