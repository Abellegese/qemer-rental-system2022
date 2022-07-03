using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace advtech.Finance.Accounta
{
    public partial class EmployeeReport : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
#pragma warning disable CS0169 // The field 'EmployeeReport.sqlda' is never used
        SqlDataAdapter sqlda;
#pragma warning restore CS0169 // The field 'EmployeeReport.sqlda' is never used
#pragma warning disable CS0169 // The field 'EmployeeReport.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'EmployeeReport.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    BindBrandsRptr2(); bindinfo();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }
        private void BindBrandsRptr2()
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID2 = Convert.ToString(Request.QueryString["fname"]);
                string fname = PID2;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblEmpPayrol where Name LIKE '%" + fname + "%'";
                com = new SqlCommand(str, con);

                using (SqlDataAdapter sda = new SqlDataAdapter(com))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                    Repeater1.DataSource = dtBrands;
                    Repeater1.DataBind();
                }
            }
        }
        protected void bindinfo()
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["fname"]);

                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from  tblPersonalInformation where FullName  LIKE '%" + PID + "%'", con);
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    if (reader1.Read())
                    {
                        string AddressResidential;
                        AddressResidential = reader1["ResidentialAddress"].ToString();
                        String Mobile = reader1["MobileNumber"].ToString();
                        if (Mobile != "")
                        {
                            Contact.InnerText = Mobile;
                        }
                        if (AddressResidential != "")
                        {
                            Address.InnerText = AddressResidential;
                        }
                        reader1.Close();
                        SqlCommand cmd2 = new SqlCommand("select * from tblEmployeeBasic where FullName  LIKE '%" + PID + "%'", con);
                        SqlDataReader reader = cmd2.ExecuteReader();
                        if (reader.Read())
                        {
                            string kc; string Pos; string kc3;
                            kc = reader["FullName"].ToString();

                            kc3 = reader["EmployeeID"].ToString();

                            //Basic
                            Pos = reader["Position"].ToString();
                            string workemail = reader["WorkEmail"].ToString();
                            string dateofj = reader["DateofJoining"].ToString();
                            string department2 = reader["Department"].ToString();
                            Name.InnerText = kc;
                            if (workemail != "")
                            {
                                Email.InnerText = workemail;
                            }
                            DateofJoining.InnerText = Convert.ToDateTime(dateofj).ToString("MMMM dd, yyyy");
                            Department.InnerText = department2;
                            Position.InnerText = Pos;
                            reader.Close();
                            con.Close();
                        }
                    }
                }
            }
        }
    }
}