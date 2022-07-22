using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class CreditStatement : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'CreditStatement.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'CreditStatement.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    BindBrandsRptr2(); BindBrandsRptr4();
                    BindShopNo(); bindcompany();
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
                SqlCommand cmd = new SqlCommand("select Oname,BuissnessLocation,Contact from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company; string address; string cont;
                    company = reader["Oname"].ToString();
                    address = reader["BuissnessLocation"].ToString();
                    cont = reader["Contact"].ToString();
                    addressname.InnerText = "Address: " + address;
                    oname.InnerText = company;
                    WaterMarkOname.InnerText = company;
                    phone.InnerText = "Contact: " + cont;

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
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblcreditnote where customer ='" + PID + "' and balance > 0 ";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable ds = new DataTable();
                sqlda.Fill(ds);
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                con.Close();
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
                    ShopNo.InnerText = shopno; datecurrent.InnerText = DateTime.Now.ToString("MMMM dd, yyyy");
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
                    SqlCommand cmd2 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where customer='" + PID + "' and balance > 0", con);

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

                                kc = reader["Balance"].ToString();
                                if (kc == "" || kc == null)
                                {
                                    TotalReceivable.InnerText = "0.00";
                                }
                                else
                                {
                                    TotalReceivable.InnerText = Convert.ToDouble(kc).ToString("#,##0.00");
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
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblAged = item.FindControl("lblAged") as Label;
                    Label lbl = item.FindControl("lblDueDate") as Label;
                    DateTime today = DateTime.Now.Date;
                    DateTime duedate = Convert.ToDateTime(lbl.Text);
                    TimeSpan t = today - duedate;
                    string dayleft = t.TotalDays.ToString();
                    lblAged.Text = dayleft + " Days";
                }
            }
        }
    }
}