using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class CustomerWelcomeLetter : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
#pragma warning disable CS0169 // The field 'CustomerWelcomeLetter.str' is never used
        string str;
#pragma warning restore CS0169 // The field 'CustomerWelcomeLetter.str' is never used
#pragma warning disable CS0169 // The field 'CustomerWelcomeLetter.com' is never used
        SqlCommand com;
#pragma warning restore CS0169 // The field 'CustomerWelcomeLetter.com' is never used
#pragma warning disable CS0169 // The field 'CustomerWelcomeLetter.sqlda' is never used
        SqlDataAdapter sqlda;
#pragma warning restore CS0169 // The field 'CustomerWelcomeLetter.sqlda' is never used
#pragma warning disable CS0169 // The field 'CustomerWelcomeLetter.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'CustomerWelcomeLetter.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    BindBrandsRptr2(); BindBrandsRptr4();
                    BindShopNo(); BindArea(); bindcompany();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindcompany()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Oname,OAdress,Contact,TIN from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company; string bl; string contact1;
                    company = reader["Oname"].ToString();
                    bl = reader["OAdress"].ToString();
                    contact1 = reader["Contact"].ToString();

                    campName.InnerText = company;
                    CompAddress.InnerText = bl;
                    Contact.InnerText = contact1;


                }
            }
        }
        protected void BindArea()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);

                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand cmd2 = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string shop; string locat; string price; string status; string area;
                        shop = reader["shop"].ToString(); shopNumber.InnerText = shop;
                        locat = reader["location"].ToString();
                        price = reader["price"].ToString(); rate.InnerText = (Convert.ToDouble(price) + Convert.ToDouble(price) * 0.15).ToString("#,##0.00");
                        status = reader["Status"].ToString();
                        area = reader["area"].ToString(); areaSpan.InnerText = Convert.ToDouble(area).ToString("#,##0.00");
                        ServiceCharge.InnerText = Convert.ToDouble(reader["servicesharge"].ToString()).ToString("#,##0.00");
                        period.InnerText = reader["PaymentDuePeriod"].ToString();
                    }
                }
            }
        }
        private void BindBrandsRptr2()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            if (Request.QueryString["ref2"] != null)
            {

                buttonback.HRef = "CustomerDetails.aspx?ref2=" + PID;
                Name.InnerText = PID;
            }
            else
            {
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        private void BindShopNo()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    string shopno = reader["shopno"].ToString();
                    ShopNo.InnerText = shopno;
                }
            }
        }
        protected void BindBrandsRptr4()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select currentperiodue from tblrent where customer='" + PID + "'", con);

                    using (SqlDataAdapter sd = new SqlDataAdapter(cmd2))
                    {
                        DataTable dt = new DataTable();
                        sd.Fill(dt); int i2c = dt.Rows.Count;
                        SqlDataReader reader = cmd2.ExecuteReader();
                        if (i2c != 0)
                        {

                            if (reader.Read())
                            {
                                string kc;

                                kc = reader["currentperiodue"].ToString();
                                if (kc == "" || kc == null)
                                {
                                    TotalReceivable.InnerText = "0.00";
                                }
                                else
                                {
                                    TotalReceivable.InnerText = "ETB " + Convert.ToDouble(kc).ToString("#,##0.00");
                                }

                                reader.Close();
                                con.Close();
                            }
                        }
                        else
                        {
                            TotalReceivable.InnerText = "No Transaction";
                        }
                    }
                }
            }
        }
    }
}