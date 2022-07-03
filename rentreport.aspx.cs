using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class rentreport : System.Web.UI.Page
    {
#pragma warning disable CS0414 // The field 'rentreport.ntsal' is assigned but its value is never used
        double ntsal = 0;
#pragma warning restore CS0414 // The field 'rentreport.ntsal' is assigned but its value is never used
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        SqlCommand com2;
        SqlDataAdapter sqlda2;
#pragma warning disable CS0169 // The field 'rentreport.ds2' is never used
        DataSet ds2;
#pragma warning restore CS0169 // The field 'rentreport.ds2' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    mont.InnerText = DateTime.Now.ToString("MMMM dd, yyyy");
                    bindstatus(); bindcompany(); bindtotal(); bindstatuscustomer();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }
        private void bindstatuscustomer()
        {

            String PID = Convert.ToString(Request.QueryString["ref2"]);
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrent where status='Active' and DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=15";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            sqlda.Fill(ds); int i = ds.Rows.Count;
            counter.InnerText = i.ToString();

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
                    string company;
                    company = reader["Oname"].ToString();
                    oname.InnerText = company;
                }
            }
        }
        private void bindtotal()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select  SUM(currentperiodue) as duetotal from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=15 and status='Active'", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;

                }
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string duetotal;
                    duetotal = reader["duetotal"].ToString();
                    if (duetotal == "" || duetotal == null)
                    {
                        VatTable.Visible = false;
                    }
                    else
                    {
                        double vatfree = Convert.ToDouble(duetotal) / 1.15;
                        double vat = Convert.ToDouble(duetotal) - vatfree;
                        txtTotal.InnerText = "ETB " + Convert.ToDouble(duetotal).ToString("#,##0.00");
                    }

                }
            }
        }
        private void bindstatus()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=15  and status='Active'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();

            string str2 = "select * from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=15  and status='Active'";
            com2 = new SqlCommand(str2, con);
            sqlda2 = new SqlDataAdapter(com2);
            DataTable dt = new DataTable(); sqlda2.Fill(ds); sqlda2.Fill(dt); int i = dt.Rows.Count;
            if (i != 0)
            {
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 80;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close(); main2.Visible = false;
            }
            else
            {
                main2.Visible = true; btnNext.Enabled = false; btnPrevious.Enabled = false;
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
            bindstatus();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            bindstatus();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        Label lblcustomer = item.FindControl("Label2") as Label;
                        Label lbl = item.FindControl("lblDueDate") as Label;
                        Label lbl2 = item.FindControl("lblDueStatus") as Label;
                        Label lblfine = item.FindControl("lblFine") as Label;
                        Label lblpassed = item.FindControl("lblpassed") as Label;
                        Label lblVAT = item.FindControl("lblVAT") as Label;
                        Label lblTotal = item.FindControl("lblTotal") as Label;
                        Label lblServicecharge = item.FindControl("lblServiceCharge") as Label;
                        Label lblPrice = item.FindControl("lblPrice") as Label;
                        SqlCommand cmdpp = new SqlCommand("select*from tblCustomers where FllName='" + lblcustomer.Text + "' and Status='Active'", con);

                        SqlDataReader readerpp = cmdpp.ExecuteReader();
                        if (readerpp.Read())
                        {
                            string payperiod;
                            payperiod = readerpp["PaymentDuePeriod"].ToString();

                            if (payperiod == "Monthly")
                            {
                                double VAT = Convert.ToDouble(lblServicecharge.Text) * 0.15 + Convert.ToDouble(lblPrice.Text) * 0.15;
                                lblVAT.Text = VAT.ToString("#,##0.00");
                            }
                            else if (payperiod == "Every Three Month")
                            {
                                double VAT = Convert.ToDouble(lblServicecharge.Text) * 0.15 * 3 + Convert.ToDouble(lblPrice.Text) * 0.15 * 3;
                                lblVAT.Text = VAT.ToString("#,##0.00");
                            }
                            else
                            {
                                double VAT = Convert.ToDouble(lblServicecharge.Text) * 0.15 * 12 + Convert.ToDouble(lblPrice.Text) * 0.15 * 12;
                                lblVAT.Text = VAT.ToString("#,##0.00");
                            }
                        }
                        readerpp.Close();
                        SqlCommand cmd = new SqlCommand("select*from tblrent where customer='" + lblcustomer.Text + "' and status='Active'", con);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string duedate1;
                            duedate1 = reader["duedate"].ToString();

                            DateTime today = DateTime.Now.Date;
                            DateTime duedate = Convert.ToDateTime(duedate1);
                            TimeSpan t = duedate - today;
                            string dayleft = t.TotalDays.ToString();

                            if (Convert.ToInt32(dayleft) <= 15 || Convert.ToInt32(dayleft) > 0)
                            {
                                lbl2.Visible = true; lbl2.Text = "Due Remain" + " " + dayleft + " " + "Days";
                                lblfine.Visible = false;
                            }
                            if (Convert.ToInt32(dayleft) > 15)
                            {
                                lbl2.Visible = false; lblfine.Text = "Not Yet.";
                                lblfine.Visible = true;
                            }
                            if (Convert.ToInt32(dayleft) < 0)
                            {
                                int d = -Convert.ToInt32(dayleft); lbl2.Visible = false; lblfine.Visible = false;
                                lblpassed.Text = "due passed by" + " " + d + " " + "Days";
                            }
                        }
                        reader.Close();
                    }
                }
            }
        }
    }
}