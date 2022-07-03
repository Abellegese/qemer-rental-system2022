using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class CustomerStatement : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    BindBrandsRptr2(); dateasof.InnerText = DateTime.Now.ToString("MMM dd, yyyy");
                    BindBrandsRptr6();
                    BindBrandsRptr45(); bindcompany();
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
        private void BindBrandsRptr2()
        {
            if (Request.QueryString["ref"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref"]);
                CustomerID.InnerText = PID;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "SELECT  CSID,Date,Trans,Details,InvAmount, Payment, 0 + SUM(InvAmount-Payment) OVER (ORDER BY CSID ROWS UNBOUNDED PRECEDING) AS Balance FROM dbo.tblCustomerStatement where Customer='" + PID + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "ID");
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 100;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
            }
        }
        public int CurrentPage
        {
            get
            {
                object s1 = this.ViewState["CurrentPage"];
                if (s1 == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(s1);
                }
            }

            set { this.ViewState["CurrentPage"] = value; }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindBrandsRptr2();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindBrandsRptr2();
        }
        protected void Save(object sender, EventArgs e)
        {
            if (txtDatefrom.Text == "" || txtDateTo.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                if (Request.QueryString["ref"] != null)
                {
                    Date1.InnerText = txtDatefrom.Text; Span1.Visible = true;
                    Span1.InnerText = txtDateTo.Text; Date1.Visible = true;
                    middleto.Visible = true; asof.Visible = false; dateasof.Visible = false;
                    String PID = Convert.ToString(Request.QueryString["ref"]);
                    Sp.InnerText = PID;
                    SqlConnection con = new SqlConnection(strConnString);
                    con.Open();
                    str = "SELECT  CSID,Date,Trans,Details,InvAmount, Payment, 0 + SUM(InvAmount-Payment) OVER (ORDER BY CSID ROWS UNBOUNDED PRECEDING) AS Balance FROM dbo.tblCustomerStatement where Customer='" + PID + "' and Date between CONVERT(datetime, '" + txtDatefrom.Text + "') AND CONVERT(datetime, '" + txtDateTo.Text + "')";
                    com = new SqlCommand(str, con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(com))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();
                        SqlCommand cmd2 = new SqlCommand("select SUM(InvAmount) InvAmount, SUM(Payment) Payment from tblCustomerStatement where Customer='" + PID + "' and Date between CONVERT(datetime, '" + txtDatefrom.Text + "') AND CONVERT(datetime, '" + txtDateTo.Text + "')", con);
                        SqlDataReader reader = cmd2.ExecuteReader();
                        if (reader.Read())
                        {
                            string kc; kc = reader["InvAmount"].ToString();
                            string lc; lc = reader["Payment"].ToString();
                            if (kc == "" || kc == null && lc == "" || lc == null)
                            {

                            }
                            else
                            {
                                Ship2.InnerText = Convert.ToDouble(kc).ToString("#,##0.00");
                                Ship3.InnerText = Convert.ToDouble(lc).ToString("#,##0.00");
                            }

                            reader.Close();
                            con.Close();
                            con.Open();
                            SqlCommand cmd = new SqlCommand("SELECT  CSID,Date,Trans,Details,InvAmount, Payment, 0 + SUM(InvAmount-Payment) OVER (ORDER BY CSID ROWS UNBOUNDED PRECEDING) AS Balance FROM dbo.tblCustomerStatement where Customer='" + PID + "' and Date between CONVERT(datetime, '" + txtDatefrom.Text + "') AND CONVERT(datetime, '" + txtDateTo.Text + "')", con);
                            SqlDataReader reader1 = cmd.ExecuteReader();
                            if (reader1.Read())
                            {
                                string kc1; kc1 = reader1["Balance"].ToString();
                                if (kc1 == "" || kc1 == null)
                                {

                                }
                                else
                                {
                                    Ship.InnerText = Convert.ToDouble(kc1).ToString("#,##0.00");
                                    Span6.InnerText = (Convert.ToDouble(kc1) + Convert.ToDouble(kc) - Convert.ToDouble(lc)).ToString("#,##0.00");
                                    if (Convert.ToDouble(kc1) < 0)
                                    {
                                        Ship.InnerText = "+" + Convert.ToDouble(kc1).ToString("#,##0.00");
                                        Span6.Attributes.Add("class", "text-success");
                                    }
                                }
                                reader1.Close();
                                con.Close();
                            }
                        }
                    }
                }
            }
        }
        private void BindBrandsRptr6()
        {
            if (Request.QueryString["ref"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref"]);
                buttonback.HRef = "CustomerDetails.aspx?ref2=" + PID;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand cmd2 = new SqlCommand("select SUM(InvAmount) InvAmount, SUM(Payment) Payment from tblCustomerStatement where Customer='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; kc = reader["InvAmount"].ToString();
                        string lc; lc = reader["Payment"].ToString();
                        if (kc == "" || kc == null)
                        {

                        }
                        else
                        {
                            Ship2.InnerText = Convert.ToDouble(kc).ToString("#,##0.00");
                            Ship3.InnerText = Convert.ToDouble(lc).ToString("#,##0.00");
                            Double balance = Convert.ToDouble(kc) - Convert.ToDouble(lc);
                        }


                        reader.Close();
                        con.Close();
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT  CSID,Date,Trans,Details,InvAmount, Payment, 0 + SUM(InvAmount-Payment) OVER (ORDER BY CSID ROWS UNBOUNDED PRECEDING) AS Balance FROM dbo.tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);
                        SqlDataReader reader1 = cmd.ExecuteReader();
                        if (reader1.Read())
                        {
                            string kc1; kc1 = reader1["Balance"].ToString();
                            if (kc1 == "" || kc1 == null)
                            {

                            }
                            else
                            {

                                if (Convert.ToDouble(kc1) < 0)
                                {
                                    Span6.InnerText = "+" + Convert.ToDouble(-Convert.ToDouble(kc1)).ToString("#,##0.00");
                                    Span6.Attributes.Add("class", "text-success");
                                }
                                else
                                {
                                    Span6.InnerText = Convert.ToDouble(kc1).ToString("#,##0.00");
                                }
                            }


                            reader1.Close();
                            con.Close();
                        }
                    }
                }
            }
        }
        private void BindBrandsRptr45()
        {
            if (Request.QueryString["ref"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref"]);
                Sp.InnerText = PID;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                SqlCommand cmd2 = new SqlCommand("SELECT  CSID,Date,Trans,Details,InvAmount, Payment, 0 + SUM(InvAmount-Payment) OVER (ORDER BY CSID ROWS UNBOUNDED PRECEDING) AS Balance FROM dbo.tblCustomerStatement where Customer='" + PID + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    string kc;

                    kc = reader["Balance"].ToString();
                    Ship.InnerText = "  " + Convert.ToDouble(kc).ToString("#,##0.00");

                }
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("Label2") as Label;
                    double balance = Convert.ToDouble(lbl.Text);
                    if (balance < 0)
                    {
                        double g = -balance;
                        lbl.Attributes.Add("class", "text-success");
                        lbl.Text = "+" + g.ToString("#,##0.00");
                    }
                }
            }

        }
    }
}