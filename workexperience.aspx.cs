using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class workexperience : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
#pragma warning disable CS0169 // The field 'workexperience.str' is never used
#pragma warning disable CS0169 // The field 'workexperience.str2' is never used
        string str; string str2;
#pragma warning restore CS0169 // The field 'workexperience.str2' is never used
#pragma warning restore CS0169 // The field 'workexperience.str' is never used
#pragma warning disable CS0169 // The field 'workexperience.com' is never used
        SqlCommand com;
#pragma warning restore CS0169 // The field 'workexperience.com' is never used
#pragma warning disable CS0169 // The field 'workexperience.sqlda' is never used
        SqlDataAdapter sqlda;
#pragma warning restore CS0169 // The field 'workexperience.sqlda' is never used
#pragma warning disable CS0169 // The field 'workexperience.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'workexperience.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    bindcompany(); bindinfo(); bindgratitude();

                    mont.InnerText = DateTime.Now.ToString("MMM/d/yyyy");
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindgratitude()
        {
            String PID = Convert.ToString(Request.QueryString["markas"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblEmpWorkExperience where FullName='" + PID + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    string kc; kc = reader["Description"].ToString();
                    gratitude.InnerText = kc;
                    txtGratitude.Text = kc;
                }
            }
        }
        protected void bindinfo()
        {
            if (Request.QueryString["markas"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["markas"]);
                link.HRef = "employeeinfo.aspx?fname=" + PID;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from tblEmployeeBasic where FullName='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; string gender; string kc2; string kc3;
                        kc = reader["FullName"].ToString();

                        kc3 = reader["EmployeeID"].ToString();
                        kc2 = reader["Position"].ToString();
                        gender = reader["Gender"].ToString();
                        //Basic
                        string workemail = reader["WorkEmail"].ToString();
                        if (gender == "Male")
                        {
                            gensg1.InnerText = "him";
                            gen1.InnerText = "his";
                            gen2.InnerText = "his";
                            gen3.InnerText = "his";
                            gen4.InnerText = "his";
                            gen6.InnerText = "his";
                            gen7.InnerText = "his";



                            sir4.InnerText = "Mrs.";
                            sir6.InnerText = "Mrs.";

                        }
                        else
                        {
                            gensg1.InnerText = "her";
                            gen1.InnerText = "her";
                            gen2.InnerText = "her";
                            gen3.InnerText = "her";
                            gen4.InnerText = "her";
                            gen6.InnerText = "her";
                            gen7.InnerText = "her";


                            sir4.InnerText = "Ms.";
                            sir6.InnerText = "Ms.";
                        }
                        string dateofj = reader["DateofJoining"].ToString();
                        datebegining.InnerText = Convert.ToDateTime(dateofj).ToString("MM/dd/yyyy");
                        string department2 = reader["Department"].ToString();
                        position.InnerText = kc2;
                        reader.Close();
                        SqlCommand cmdmn = new SqlCommand("select * from tblEmployeeBasic where Position='Manager'", con);
                        SqlDataReader readermn = cmdmn.ExecuteReader();
                        if (readermn.Read())
                        {
                            kc = readermn["FullName"].ToString(); readermn.Close();
                            manager.InnerText = kc;
                        }
                    }
                }
            }
        }
        private void bindcompany()
        {
            String PID2 = Convert.ToString(Request.QueryString["markas"]);
            empname.InnerText = PID2;
            emp2.InnerText = PID2;
            emp3.InnerText = PID2;
            emp4.InnerText = PID2;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Oname,OAdress,BuissnessLocation,Contact from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company; string addr;
                    addr = reader["OAdress"].ToString();
                    company = reader["Oname"].ToString();
                    comp5.InnerText = company;
                    compaddress.InnerText = addr;
                    oname.InnerText = company;
                    compName.InnerText = company;
                    comp2.InnerText = company;
                    comp3.InnerText = company;
                    dateending.InnerText = DateTime.Now.Date.ToString("MM/dd/yyyy");
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["markas"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update tblEmpWorkExperience set  Description='" + txtGratitude.Text + "' where FullName='" + PID + "'", con);
                cmd.ExecuteNonQuery();
                bindgratitude();
            }
        }
    }
}