using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace advtech.Finance.Accounta
{
    public partial class orgprofileview : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
#pragma warning disable CS0169 // The field 'orgprofileview.str' is never used
        string str;
#pragma warning restore CS0169 // The field 'orgprofileview.str' is never used
#pragma warning disable CS0169 // The field 'orgprofileview.com' is never used
        SqlCommand com;
#pragma warning restore CS0169 // The field 'orgprofileview.com' is never used
#pragma warning disable CS0169 // The field 'orgprofileview.sqlda' is never used
        SqlDataAdapter sqlda;
#pragma warning restore CS0169 // The field 'orgprofileview.sqlda' is never used
#pragma warning disable CS0169 // The field 'orgprofileview.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'orgprofileview.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindBrandsRptr3();

            }
        }
        protected void BindBrandsRptr3()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblOrganization", con);
                SqlDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    string kc; string kc1; string kc2;
                    string kc3; string kc4;
                    string kc6; string kc7;
                    kc = reader["Oname"].ToString();
                    kc1 = reader["OAdress"].ToString();
                    kc2 = reader["City"].ToString();
                    kc3 = reader["Email"].ToString();
                    kc4 = reader["Fax"].ToString();
                    kc6 = reader["Contact"].ToString();
                    string TIN = reader["TIN"].ToString();
                    txtMobile.Text = kc6;
                    txtEmail.Text = kc3;
                    txtTIN.Text = TIN;
                    txtAddress.Text = kc1;
                    kc7 = reader["BuissnessLocation"].ToString();
                    company.InnerText = kc;
                    address.InnerText = kc1;
                    city.InnerText = kc2;
                    email.InnerText = kc3;
                    txtVatRegNumber.Text = reader["vatregnumber"].ToString();
                    VatRegNumber.InnerText = reader["vatregnumber"].ToString();
                    mobile.InnerText = kc6;

                    reader.Close();
                    con.Close();
                }
            }
        }

        protected void Button18_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Update tblOrganization set  Contact='" + txtMobile.Text + "',Email='" + txtEmail.Text + "',OAdress='" + txtAddress.Text + "', TIN='" + txtTIN.Text + "', vatregnumber='" + txtVatRegNumber.Text + "'", con);
                cmd2.ExecuteNonQuery();
                Response.Redirect("orgprofileview.aspx");
            }
        }
    }
}
