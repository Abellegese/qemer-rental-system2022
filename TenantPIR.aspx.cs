using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class TenantPIR : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'TenantPIR.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'TenantPIR.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {

                    String PID = Convert.ToString(Request.QueryString["ref2"]);

                    BindBrandsRptr3(); bindgurantorandother(); bindcompany();
                    invoiceinfo(); BindBrandsRptr2(); binddelinquency();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void BindBrandsRptr2()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            if (Request.QueryString["ref2"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblcreditnote where customer ='" + PID + "' and balance > 0 ";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable ds = new DataTable();
                sqlda.Fill(ds);
                if (ds.Rows.Count != 0)
                {
                    Repeater1.DataSource = ds;
                    Repeater1.DataBind();
                    con.Close();
                    CreditDivNone.Visible = false;
                }
                else
                {
                    CreditDiv.Visible = false;
                }
            }
            else
            {
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        private void bindgurantorandother()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                buttonback.HRef = "CustomerDetails.aspx?ref2=" + PID;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string gfull; string gaddress; string gcontact;

                        //Shop Details
                        gfull = reader["gurentor"].ToString();
                        gaddress = reader["address"].ToString();
                        gcontact = reader["contact"].ToString();
                        TINNumber.InnerText = reader["TIN"].ToString();
                        custAddress.InnerText = reader["addresscust"].ToString();
                        gurantorfulln.InnerText = gfull;
                        addressgurantor.InnerText = gaddress;
                        contact3.InnerText = gcontact;
                    }
                }
            }

        }
        private void invoiceinfo()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();


                    SqlCommand cmd = new SqlCommand("select Sum(InvAmount) as Total,Sum(Payment) as PAID  from tblCustomerStatement where Customer='" + PID + "'", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string ah7g; string paid;
                        ah7g = reader["Total"].ToString();
                        paid = reader["PAID"].ToString();
                        reader.Close();
                        double invoice = 0; double payment = 0;
                        if (ah7g != "")
                        {
                            invoice = Convert.ToDouble(ah7g);
                            paidinv.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                        }
                        if (paid != "")
                        {
                            payment = Convert.ToDouble(paid);
                            unpaidinv.InnerText = Convert.ToDouble(paid).ToString("#,##0.00");
                        }
                        double balance = invoice - payment;
                        if (balance != 0)
                        {
                            BalancePay.InnerText = balance.ToString("#,##0.00");
                        }

                    }
                }
            }
        }
        protected void BindBrandsRptr3()
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
                        string kc; string kc7;
                        string kc4; string kc5; string kc6;
                        string kc8; string kc11; string kc12;
                        //Shop Details
                        kc = reader["contigency"].ToString();
                        if (kc == "" || kc == null)
                        {

                        }
                        else
                        {
                            contingency.InnerText = Convert.ToDouble(kc).ToString("#,##0.00");
                        }
                        string shop; string locat; string price1; string status; string AR;
                        string dateofJ = reader["joiningdate"].ToString(); dateofj.InnerText = Convert.ToDateTime(dateofJ).ToString("MMMM dd, yyyy");
                        AR = reader["area"].ToString(); area.InnerText = AR;
                        shop = reader["shop"].ToString(); shopno.InnerText = shop;
                        locat = reader["location"].ToString(); location.InnerText = locat;
                        price1 = reader["price"].ToString(); price.InnerText = Convert.ToDouble(price1).ToString("#,##0.00");
                        status = reader["Status"].ToString();
                        if (status == "Active")
                        {
                            Status1.InnerText = status; Status1.Attributes.Add("class", "badge badge-success");
                        }
                        else
                        {
                            Status1.InnerText = status; Status1.Attributes.Add("class", "badge badge-danger");
                        }
                        string agreementdate = reader["agreementdate"].ToString();
                        DateTime today1 = DateTime.Now.Date;
                        DateTime duedate1 = Convert.ToDateTime(agreementdate);
                        TimeSpan t1 = duedate1 - today1; string dayleft1 = t1.TotalDays.ToString();
                        if (Convert.ToInt32(dayleft1) <= 15 || Convert.ToInt32(dayleft1) > 0)
                        {
                            agrredate.InnerText = dayleft1 + " " + "Days" + " Remains(" + duedate1.ToString("MMMM dd, yyyy") + ")";
                            agrredate.Attributes.Add("class", "small text-success border-bottom");

                        }
                        if (Convert.ToInt32(dayleft1) < 0)
                        {
                            int d = -Convert.ToInt32(dayleft1);
                            agrredate.InnerText = d + " Days" + " Passed";
                            agrredate.Attributes.Add("class", "small  text-danger border-bottom");
                        }
                        string comp = reader["CompanyName"].ToString();
                        kc4 = reader["CustomerEmail"].ToString(); kc5 = reader["Website"].ToString();
                        kc6 = reader["Mobile"].ToString(); kc7 = reader["WorkPhone"].ToString();
                        kc8 = reader["CreditLimit"].ToString();
                        kc11 = reader["ContactPerson"].ToString(); kc12 = reader["PaymentDuePeriod"].ToString();
                        comanyname.InnerText = comp; billingterms.InnerText = kc12; website.InnerText = kc5;
                        emailaddress.InnerText = kc4; ; mobile.InnerText = kc6;
                        fullname.InnerText = PID; workphone.InnerText = kc7;
                        reader.Close();
                        con.Close();
                    }
                }
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

                    phone.InnerText = "Contact: " + cont;

                }
            }
        }
        private void binddelinquency()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            str = "select * from tblCustomerDelinquency where customer='" + PID + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            if (dt.Rows.Count != 0)
            {
                Repeater4.DataSource = dt;
                Repeater4.DataBind();
                DelinNone.Visible = false;
            }
            else
            {
                DelinquanceyDiv.Visible = false;
            }
            con.Close();
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