using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace advtech.Finance.Accounta
{
    public partial class rentstatus1 : System.Web.UI.Page
    {
#pragma warning disable CS0414 // The field 'rentstatus1.ntsal' is assigned but its value is never used
        double ntsal = 0;
#pragma warning restore CS0414 // The field 'rentstatus1.ntsal' is assigned but its value is never used
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
                    bindpay(); bindstatus(); bindtotal(); bindcustomerno(); bindduepassedtotal();
                    bindAutomation(); BindBrandsRptr8(); bindbankaccount();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private long bindFSnumber()
        {
            long fsno = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcrd1 = new SqlCommand("select TOP 1* from tblrentreceipt order by id desc", con);
                SqlDataReader readercrd1 = cmdcrd1.ExecuteReader();
                if (readercrd1.Read())
                {
                    string FSNumber = readercrd1["fsno"].ToString();
                    readercrd1.Close();
                    if (FSNumber == "" || FSNumber == null)
                    {
                    }
                    else
                    {
                        fsno = Convert.ToInt64(FSNumber) + 1;
                    }
                }
            }
            return fsno;
        }
        private readonly Random _random = new Random();
        public long RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(4, false));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(4));
            return passwordBuilder.ToString();
        }
        private void bindAutomation()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select status from tblautomation", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company;
                    company = reader["status"].ToString(); reader.Close();
                    if (company == "stoped")
                    {
                        automationdiv.Visible = false;
                        btnAlert.Visible = true;
                    }
                    else
                    {
                        automationdiv.Visible = true;
                        btnAlert.Visible = false;
                    }
                }
            }
        }
        private void bindbankaccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblBankAccounting", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    DropDownList1.DataSource = dt;
                    DropDownList1.DataTextField = "AccountName";
                    DropDownList1.DataValueField = "AC";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void bindcustomerno()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=15 and DATEDIFF(day, '" + DateTime.Now.Date + "', duedate)>=0 and status='Active'", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); long i2c3 = dtBrands232c3.Rows.Count;
                    customerno.InnerText = i2c3.ToString();
                }
            }
        }
        private void bindstatus1()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblrent where status='Active'", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater2.DataSource = dtBrands;
                        Repeater2.DataBind();

                    }
                }
            }
        }
        private void bindstatus()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrent where status='Active'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 40;
            Pds1.CurrentPageIndex = CurrentPage;
            Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
            btnPrevious.Enabled = !Pds1.IsFirstPage;
            btnNext.Enabled = !Pds1.IsLastPage;
            Repeater1.DataSource = Pds1;
            Repeater1.DataBind();
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
        private void bindpay()
        {
            if (Request.QueryString["createservice"] != null)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from tblCustomers where Status='Active'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][10].ToString() == "Every Three Month")
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 3 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active')", con);
                            cmdin.ExecuteNonQuery();
                        }
                        else if (dt.Rows[i][10].ToString() == "Every Six Month")
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 6 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active')", con);
                            cmdin.ExecuteNonQuery();
                        }
                        else if (dt.Rows[i][10].ToString() == "Monthly")
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 1 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active')", con);
                            cmdin.ExecuteNonQuery();
                        }
                        else
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "','" + dt.Rows[i][2].ToString() + "','" + dt.Rows[i][1].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 12 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active')", con);
                            cmdin.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("rentstatus.aspx");
                }
            }
        }
        private void bindtotal()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select  SUM(currentperiodue) as duetotal from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=15 and status='Active' and  DATEDIFF(day, '" + DateTime.Now.Date + "', duedate)>=0", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); long i2c3 = dtBrands232c3.Rows.Count;

                }
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string duetotal;
                    duetotal = reader["duetotal"].ToString();
                    if (duetotal == "" || duetotal == null)
                    {
                        cost.InnerText = "ETB 0.00";
                    }
                    else
                    {
                        cost.InnerText = "ETB " + Convert.ToDouble(duetotal).ToString("#,##0.00");
                    }

                }
            }
        }
        private void bindduepassedtotal()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select  SUM(currentperiodue) as duetotal from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) < 0 and status='Active'", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); long i2c3 = dtBrands232c3.Rows.Count;

                }
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string duetotal;
                    duetotal = reader["duetotal"].ToString();
                    if (duetotal == "" || duetotal == null)
                    {
                        H1.InnerText = "ETB 0.00";
                    }
                    else
                    {
                        H1.InnerText = "ETB " + Convert.ToDouble(duetotal).ToString("#,##0.00");
                    }

                }
            }
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
                        Label lblTerms = item.FindControl("Label3") as Label;
                        SqlCommand cmdpp = new SqlCommand("select*from tblCustomers where FllName='" + lblcustomer.Text + "' and Status='Active'", con);

                        SqlDataReader readerpp = cmdpp.ExecuteReader();
                        if (readerpp.Read())
                        {
                            string payperiod; string agreementdate1;
                            payperiod = readerpp["PaymentDuePeriod"].ToString();
                            agreementdate1 = readerpp["agreementdate"].ToString();
                            DateTime today = DateTime.Now.Date;
                            DateTime duedate = Convert.ToDateTime(agreementdate1);
                            TimeSpan t = duedate - today;
                            if (t.TotalDays < 0)
                            {
                                string dayleft = (-t).TotalDays.ToString() + " days passed";
                                lblTerms.Text = dayleft;
                                lblTerms.Attributes.Add("class", "text-danger");
                            }
                            else
                            {
                                string dayleft = (t).TotalDays.ToString() + " days remain";
                                lblTerms.Text = dayleft;
                                lblTerms.Attributes.Add("class", "text-success");
                            }

                            if (payperiod == "Monthly")
                            {
                                double VAT = Convert.ToDouble(lblPrice.Text) * 0.15;
                                lblVAT.Text = VAT.ToString("#,##0.00");
                            }
                            else if (payperiod == "Every Three Month")
                            {
                                double VAT = Convert.ToDouble(lblPrice.Text) * 0.15 * 3;
                                lblVAT.Text = VAT.ToString("#,##0.00");
                            }
                            else if (payperiod == "Every Six Month")
                            {
                                double VAT = Convert.ToDouble(lblPrice.Text) * 0.15 * 3;
                                lblVAT.Text = VAT.ToString("#,##0.00");
                            }
                            else
                            {
                                double VAT = Convert.ToDouble(lblPrice.Text) * 0.15 * 12;
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
                                long d = -Convert.ToInt32(dayleft); lbl2.Visible = false; lblfine.Visible = false;
                                lblpassed.Text = "due passed by" + " " + d + " " + "Days";
                            }
                        }
                        reader.Close();
                    }
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblrent where customer LIKE '%" + name + "%' and status='Active'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
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
        }
        private void BindBrandsRptr8()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd16g = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where balance > 0", con);
                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd16g))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); long i2c3 = dtBrands232c3.Rows.Count;
                    if (i2c3 != 0)
                    {
                        SqlDataReader reader6g = cmd16g.ExecuteReader();

                        if (reader6g.Read())
                        {
                            string ah7g;
                            ah7g = reader6g["Balance"].ToString();
                            reader6g.Close();
                            con.Close();
                            if (ah7g == null || ah7g == "")
                            {
                                H4.InnerText = "0.00";
                            }
                            else
                            {
                                H4.InnerText = "ETB " + Convert.ToDouble(ah7g).ToString("#,##0.00");
                            }


                        }

                    }
                    else
                    {
                        H4.InnerText = "ETB 0.00";
                    }
                }
            }
        }
        protected void btnAlert_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <= 15", con);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); long i = dt.Rows.Count;
                    if (i != 0)
                    {
                        try
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[j][6].ToString() != "")
                                {
                                    SqlCommand cmd41 = new SqlCommand("select * from tblCustomers where FllName='" + dt.Rows[j][2].ToString() + "'", con);
                                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                                    DataTable dt1 = new DataTable();
                                    sda1.Fill(dt1); long i1 = dt1.Rows.Count;
                                    DateTime today = DateTime.Now.Date;
                                    DateTime duedate = Convert.ToDateTime(dt.Rows[j][9].ToString());
                                    TimeSpan t = duedate - today;
                                    string dayleft = t.TotalDays.ToString();
                                    if (Convert.ToInt32(dayleft) <= 15 || Convert.ToInt32(dayleft) > 0)
                                    {
                                        string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                                        string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                                        TwilioClient.Init(accountSid, authToken);

                                        var message = MessageResource.Create(
                                            body: "Dear customer, your payment remains " + dayleft + " days with due amount of ETB " + Convert.ToDouble(dt.Rows[j][7].ToString()).ToString("#,##0.00"),
                                            from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                            to: new Twilio.Types.PhoneNumber("+251" + dt1.Rows[0][6].ToString())
                                        );
                                    }
                                    else
                                    {
                                        long d = -Convert.ToInt32(dayleft);
                                        string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                                        string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                                        TwilioClient.Init(accountSid, authToken);

                                        var message = MessageResource.Create(
                                            body: "Dear customer, your payment passed " + d + " days with due amount of ETB " + Convert.ToDouble(dt.Rows[j][7].ToString()).ToString("#,##0.00"),
                                            from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                            to: new Twilio.Types.PhoneNumber("+251" + dt1.Rows[0][6].ToString())
                                        );
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            string id = "No internet connection";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + id + "');", true);
                        }
                    }
                }
            }
        }
        protected void btnPassed_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) < 0 and status='Active'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
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
        }
        protected void btnUnpassed_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) >= 0 and status='Active' and DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <15 and status='Active'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
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
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrent where status='Active'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
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
        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    CheckBox CheckRow = item.FindControl("chkRow3") as CheckBox;
                    Label PID = item.FindControl("Label2") as Label;
                    if (CheckRow.Checked == true)
                    {
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {

                            SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID.Text + "' and status='Active'", con);
                            SqlDataReader reader = cmd2.ExecuteReader();
                            if (reader.Read())
                            {
                                string servicecharge; servicecharge = reader["servicecharge"].ToString();
                                string kc; string duedates = reader["duedate"].ToString();
                                kc = reader["currentperiodue"].ToString();
                                reader.Close();

                                double due = Convert.ToDouble(kc);

                                {
                                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
                                    using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                    {
                                        DataTable dtBrands2322 = new DataTable();
                                        sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
                                        //
                                        if (i2 != 0)
                                        {
                                            SqlDataReader reader6679034 = cmd19012.ExecuteReader();

                                            if (reader6679034.Read())
                                            {
                                                string ah12893;
                                                ah12893 = reader6679034["Balance"].ToString();
                                                reader6679034.Close();
                                                con.Close();
                                                con.Open();
                                                Double M1 = Convert.ToDouble(ah12893);

                                                Double bl1 = M1 + due; ;
                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                cmd45.ExecuteNonQuery();
                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','" + due + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                cmd1974.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    //Selecting from account prefernce
                                    SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                    using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                    {
                                        DataTable dtBrandss = new DataTable();
                                        sdas.Fill(dtBrandss); long iss = dtBrandss.Rows.Count;
                                        //Selecting from Income account
                                        SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                        using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                        {
                                            DataTable dtBrandss2 = new DataTable();
                                            sdas2.Fill(dtBrandss2); long iss2 = dtBrandss2.Rows.Count;
                                            //
                                            if (iss2 != 0)
                                            {
                                                SqlDataReader readers = cmds2.ExecuteReader();
                                                if (readers.Read())
                                                {
                                                    string ah1289;
                                                    ah1289 = readers["Balance"].ToString();
                                                    readers.Close();
                                                    con.Close();
                                                    con.Open();
                                                    SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

                                                    SqlDataReader reader66 = cmd166.ExecuteReader();

                                                    if (reader66.Read())
                                                    {
                                                        string ah11;
                                                        string ah1258;
                                                        ah11 = reader66["No"].ToString();
                                                        ah1258 = reader66["AccountType"].ToString();
                                                        reader66.Close();
                                                        con.Close();
                                                        con.Open();
                                                        Double M1 = Convert.ToDouble(ah1289);
                                                        double income = due - 0.15 * due;
                                                        Double bl1 = income + M1;
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                        cmd1974.ExecuteNonQuery();

                                                    }
                                                }
                                            }
                                        }
                                        //Selecting from tax acccount
                                        SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                        using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                                        {
                                            DataTable dttax = new DataTable();
                                            sdatax.Fill(dttax); long iss2 = dttax.Rows.Count;
                                            //
                                            if (iss2 != 0)
                                            {
                                                SqlDataReader readers = cmdintax.ExecuteReader();
                                                if (readers.Read())
                                                {
                                                    string ah1289;
                                                    ah1289 = readers["Balance"].ToString();
                                                    readers.Close();
                                                    con.Close();
                                                    con.Open();
                                                    SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                                    SqlDataReader reader66 = cmd166.ExecuteReader();

                                                    if (reader66.Read())
                                                    {
                                                        string ah11;
                                                        string ah1258;
                                                        ah11 = reader66["No"].ToString();
                                                        ah1258 = reader66["AccountType"].ToString();
                                                        reader66.Close();
                                                        con.Close();
                                                        con.Open();
                                                        Double M1 = Convert.ToDouble(ah1289);
                                                        Double bl1 = M1 + due * 0.15;
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + due * 0.15 + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','0','" + due * 0.15 + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                        cmd1974.ExecuteNonQuery();

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //Inserting to customer statement
                                    SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID.Text + "' ORDER BY CSID DESC", con);

                                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                    if (readerbcustb.Read())
                                    {
                                        string ah11;

                                        ah11 = readerbcustb["Balance"].ToString();
                                        readerbcustb.Close();
                                        double payment = Convert.ToDouble(due);
                                        double balancedue = Convert.ToDouble(ah11);
                                        double remain = balancedue + payment;
                                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','Credit issued','','" + due + "','0','" + remain + "','" + PID.Text + "')", con);
                                        custcmd.ExecuteNonQuery();
                                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                        if (readerAC.Read())
                                        {
                                            String FN = readerAC["Name"].ToString();
                                            readerAC.Close();
                                            con.Close();
                                            //Activity
                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Credit Issued','Credit Issued for','" + PID.Text + "','Credit Issued for'+' '+'<b>" + PID.Text + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                            con.Open();
                                            cmdAc.ExecuteNonQuery();
                                            string money = "ETB";
                                            SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                            SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                            DataTable dtcni = new DataTable();
                                            sdacni.Fill(dtcni); 
                                            long nbcni = 0;
                                            if (dtcni.Rows.Count > 0) { nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString()) + 1; }
                                            else { nbcni += 1; }
                                            ///
                                            string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID.Text;
                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Issued as a Credit','" + FN + "','" + PID.Text + "','Unseen','fas fa-dollar-sign text-white','icon-circle bg bg-warning','"+ crediturl + "','MN')", con);
                                            cmd197h.ExecuteNonQuery();

                                            //Updating the Due Date

                                            SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID.Text + "' and Status='Active'", con);
                                            SqlDataReader readerup = cmdup.ExecuteReader();

                                            if (readerup.Read())
                                            {
                                                String terms = readerup["PaymentDuePeriod"].ToString();
                                                readerup.Close();
                                                if (terms == "Monthly")
                                                {
                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                    DateTime newduedate = duedate.AddDays(30);
                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                    cmdrent.ExecuteNonQuery();
                                                }
                                                else if (terms == "Every Three Month")
                                                {
                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                    DateTime newduedate = duedate.AddDays(90);
                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                    cmdrent.ExecuteNonQuery();
                                                }
                                                else if (terms == "Every Six Month")
                                                {
                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                    DateTime newduedate = duedate.AddDays(180);
                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                    cmdrent.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                    DateTime newduedate = duedate.AddDays(365);
                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                    cmdrent.ExecuteNonQuery();
                                                }
                                                SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID.Text + "','" + DateTime.Now + "','" + due + "','" + due + "','Credit for rent','" + DateTime.Now.AddDays(30) + "','CN-NO-REF')", con);
                                                cmdcrn.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                            reader.Close();
                        }
                    }
                }
                Response.Redirect("creditnote.aspx");
            }
        }
        protected void btnBulkCashPayment_Click(object sender, EventArgs e)
        {
            if (txtDateCash.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Select the date of transaction')", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    foreach (RepeaterItem item in Repeater1.Items)
                    {
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {
                            CheckBox CheckRow = item.FindControl("chkRow3") as CheckBox;
                            Label PID = item.FindControl("Label2") as Label;
                            if (CheckRow.Checked == true)
                            {
                                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID.Text + "'", con);
                                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                                if (readercrd.Read())
                                {
                                    double SC = 0;
                                    string pp = readercrd["PaymentDuePeriod"].ToString();
                                    string limit = readercrd["CreditLimit"].ToString();
                                    readercrd.Close();
                                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID.Text + "'", con);
                                    SqlDataReader reader = cmd2.ExecuteReader();

                                    if (reader.Read())
                                    {
                                        string kc; string duedates = reader["duedate"].ToString();
                                        string servicecharge; servicecharge = reader["servicecharge"].ToString();
                                        kc = reader["currentperiodue"].ToString();
                                        if (pp == "Every Three Month") { SC = Convert.ToDouble(servicecharge) * 3; }
                                        else if (pp == "Every Six Month") { SC = Convert.ToDouble(servicecharge) * 6; }
                                        else if (pp == "Monthly") { SC = Convert.ToDouble(servicecharge) * 1; }
                                        else { SC = Convert.ToDouble(servicecharge) * 12; }
                                        double totalpay = Convert.ToDouble(kc);
                                        double due = Convert.ToDouble(kc);
                                        double balance = 0;
                                        reader.Close();

                                        SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash on Hand'", con);
                                        using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                        {
                                            DataTable dtBrands2322 = new DataTable();
                                            sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
                                            //
                                            if (i2 != 0)
                                            {
                                                SqlDataReader reader6679034 = cmd19012.ExecuteReader();

                                                if (reader6679034.Read())
                                                {
                                                    string ah12893;
                                                    ah12893 = reader6679034["Balance"].ToString();
                                                    reader6679034.Close();
                                                    con.Close();
                                                    con.Open();
                                                    double deb = Convert.ToDouble(kc);
                                                    Double M1 = Convert.ToDouble(ah12893);
                                                    Double bl1 = M1 + deb;
                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash on Hand'", con);
                                                    cmd45.ExecuteNonQuery();
                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','" + deb + "','0','" + bl1 + "','" + txtDateCash.Text + "','Cash on Hand','','Cash')", con);
                                                    cmd1974.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                        //Selecting from account prefernce
                                        SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                        using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                        {
                                            DataTable dtBrandss = new DataTable();
                                            sdas.Fill(dtBrandss); long iss = dtBrandss.Rows.Count;
                                            //Selecting from Income account
                                            SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                            using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                            {
                                                DataTable dtBrandss2 = new DataTable();
                                                sdas2.Fill(dtBrandss2); long iss2 = dtBrandss2.Rows.Count;
                                                //
                                                if (iss2 != 0)
                                                {
                                                    SqlDataReader readers = cmds2.ExecuteReader();
                                                    if (readers.Read())
                                                    {
                                                        string ah1289;
                                                        ah1289 = readers["Balance"].ToString();
                                                        readers.Close();
                                                        con.Close();
                                                        con.Open();
                                                        SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

                                                        SqlDataReader reader66 = cmd166.ExecuteReader();

                                                        if (reader66.Read())
                                                        {
                                                            string ah11;
                                                            string ah1258;
                                                            ah11 = reader66["No"].ToString();
                                                            ah1258 = reader66["AccountType"].ToString();
                                                            reader66.Close();
                                                            con.Close();
                                                            con.Open();
                                                            Double M1 = Convert.ToDouble(ah1289);
                                                            double income = (totalpay - SC) / 1.15;
                                                            income = income + SC;
                                                            Double bl1 = income + M1;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + txtDateCash.Text + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','0','" + income + "','" + bl1 + "','" + txtDateCash.Text + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                            cmd1974.ExecuteNonQuery();

                                                        }
                                                    }
                                                }
                                            }
                                            //Selecting from cash acccount
                                            SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                            using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                                            {
                                                DataTable dttax = new DataTable();
                                                sdatax.Fill(dttax); long iss2 = dttax.Rows.Count;
                                                //
                                                if (iss2 != 0)
                                                {
                                                    SqlDataReader readers = cmdintax.ExecuteReader();
                                                    if (readers.Read())
                                                    {
                                                        string ah1289;
                                                        ah1289 = readers["Balance"].ToString();
                                                        readers.Close();
                                                        con.Close();
                                                        con.Open();
                                                        SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                                        SqlDataReader reader66 = cmd166.ExecuteReader();

                                                        if (reader66.Read())
                                                        {
                                                            string ah11;
                                                            string ah1258;
                                                            ah11 = reader66["No"].ToString();
                                                            ah1258 = reader66["AccountType"].ToString();
                                                            reader66.Close();
                                                            con.Close();
                                                            con.Open();
                                                            Double M1 = Convert.ToDouble(ah1289);
                                                            double vatfree = (totalpay - SC) / 1.15;
                                                            double vat = (totalpay - SC) - vatfree;
                                                            Double bl1 = M1 + vat;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','0','" + vat + "','" + bl1 + "','" + txtDateCash.Text + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                            cmd1974.ExecuteNonQuery();

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //Inserting to customer statement
                                        SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID.Text + "' ORDER BY CSID DESC", con);

                                        SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                        if (readerbcustb.Read())
                                        {
                                            string ah11;

                                            ah11 = readerbcustb["Balance"].ToString();
                                            readerbcustb.Close();

                                            SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + txtDateCash.Text + "','Bulk Cash Payment','','" + totalpay + "','" + kc + "','" + ah11 + "','" + PID.Text + "')", con);
                                            custcmd.ExecuteNonQuery();
                                            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                            SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                            if (readerAC.Read())
                                            {
                                                String FN = readerAC["Name"].ToString();
                                                readerAC.Close();
                                                con.Close();
                                                //Activity

                                                string money = "ETB";

                                                con.Open();


                                                //Updating the Due Date
                                                SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID.Text + "'", con);
                                                SqlDataReader readerup = cmdup.ExecuteReader();

                                                if (readerup.Read())
                                                {
                                                    String terms = readerup["PaymentDuePeriod"].ToString();
                                                    readerup.Close();
                                                    if (CashDueDate.Checked == false)
                                                    {
                                                        if (terms == "Monthly")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(30);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        if (terms == "Every Three Month")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(90);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        if (terms == "Every Six Month")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(180);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        if (terms == "Yearly")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(365);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                    }
                                                    //Insert into cash receipt journal
                                                    double payment = Convert.ToDouble(kc);
                                                    double credit = due - payment;
                                                    double balancedue = Convert.ToDouble(ah11);
                                                    double unpaid = -balance;
                                                    double remain = balancedue + unpaid;

                                                    SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                    SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                    DataTable dtdf = new DataTable();
                                                    sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                                    double vatfree = due - 0.15 * due;
                                                    string ref2 = "RAKS-" + RandomPassword();
                                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID.Text + "','" + ref2 + "','" + txtDateCash.Text + "','" + 0.ToString() + "','" + (due - SC) + "','" + ah11 + "','" + nb + "','" + bindFSnumber() + "','Cash')", con);
                                                    cmdri.ExecuteNonQuery();
                                                    string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID.Text;
                                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + txtDateCash.Text + "','" + money + "'+' '+'" + totalpay.ToString("#,##0.00") + "'+' '+'Deposited into Cash on Hand account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                    cmd197h.ExecuteNonQuery();
                                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + txtDateCash.Text + "','Payment Received','Payment received from customer','" + PID.Text + "','Payment received from'+' '+'<b>" + PID.Text + "</b>'+' '+'Was Recorded','" + FN + "','" + url + "')", con);

                                                    cmdAc.ExecuteNonQuery();


                                                }
                                            }
                                        }
                                    }
                           
                                }
                           
                            }
                        }
                    }
                    Response.Redirect("rentstatus1.aspx");
                }
            }
        }
        protected void btnBankPayment_Click(object sender, EventArgs e)
        {
            if (txtDateBank.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Select the date of transaction')", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    foreach (RepeaterItem item in Repeater1.Items)
                    {
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {
                            CheckBox CheckRow = item.FindControl("chkRow3") as CheckBox;
                            Label PID = item.FindControl("Label2") as Label;
                            if (CheckRow.Checked == true)
                            {
                                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID.Text + "'", con);
                                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                                if (readercrd.Read())
                                {
                                    double SC = 0;
                                    string pp = readercrd["PaymentDuePeriod"].ToString();
                                    string limit = readercrd["CreditLimit"].ToString();
                                    readercrd.Close();
                                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID.Text + "'", con);
                                    SqlDataReader reader = cmd2.ExecuteReader();

                                    if (reader.Read())
                                    {
                                        string servicecharge; servicecharge = reader["servicecharge"].ToString();
                                        string kc; string duedates = reader["duedate"].ToString();
                                        kc = reader["currentperiodue"].ToString();
                                        if (pp == "Every Three Month") { SC = Convert.ToDouble(servicecharge) * 3; }
                                        else if (pp == "Every Six Month") { SC = Convert.ToDouble(servicecharge) * 6; }
                                        else if (pp == "Monthly") { SC = Convert.ToDouble(servicecharge) * 1; }
                                        else { SC = Convert.ToDouble(servicecharge) * 12; }
                                        double totalpay = Convert.ToDouble(kc);
                                        reader.Close();
                                        SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                        SqlDataReader readerbank = cmdbank.ExecuteReader();

                                        if (readerbank.Read())
                                        {
                                            string bankno;
                                            bankno = readerbank["AccountNumber"].ToString();
                                            readerbank.Close(); string totalvb = "Bulk bank payment-" + PID.Text;
                                            SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                            using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                            {
                                                DataTable dt1 = new DataTable();
                                                sda221.Fill(dt1); long j = dt1.Rows.Count;
                                                //
                                                if (j != 0)
                                                {
                                                    double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(kc);
                                                    SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                    cmdday.ExecuteNonQuery();

                                                    SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('-','-','" + kc + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalvb + "','" + txtDateBank.Text + "')", con);
                                                    cvb.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    double t = Convert.ToDouble(kc);
                                                    SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('Bulk Bank Payment','Bulk Bank Payment','" + kc + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalvb + "','" + txtDateBank.Text + "')", con);
                                                    cvb1.ExecuteNonQuery();
                                                    SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('Bulk Bank Payment','Bulk Bank Payment','" + kc + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalvb + "','" + txtDateBank.Text + "')", con);
                                                    b.ExecuteNonQuery();

                                                }
                                            }
                                            //selecting from cash at bank
                                            SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash at Bank'", con);
                                            using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                            {
                                                DataTable dtBrands2322 = new DataTable();
                                                sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
                                                //
                                                if (i2 != 0)
                                                {
                                                    SqlDataReader reader6679034 = cmd19012.ExecuteReader();

                                                    if (reader6679034.Read())
                                                    {
                                                        string ah12893;
                                                        ah12893 = reader6679034["Balance"].ToString();
                                                        reader6679034.Close();
                                                        con.Close();
                                                        con.Open();
                                                        double deb = Convert.ToDouble(kc);
                                                        Double M1 = Convert.ToDouble(ah12893);
                                                        Double bl1 = M1 + deb;
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','" + deb + "','0','" + bl1 + "','" + txtDateBank.Text + "','Cash at Bank','','Cash')", con);
                                                        cmd1974.ExecuteNonQuery();
                                                    }
                                                }
                                            }
                                            //Selecting from account prefernce
                                            SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                            using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                            {
                                                DataTable dtBrandss = new DataTable();
                                                sdas.Fill(dtBrandss); long iss = dtBrandss.Rows.Count;
                                                //Selecting from Income account
                                                SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                {
                                                    DataTable dtBrandss2 = new DataTable();
                                                    sdas2.Fill(dtBrandss2); long iss2 = dtBrandss2.Rows.Count;
                                                    //
                                                    if (iss2 != 0)
                                                    {
                                                        SqlDataReader readers = cmds2.ExecuteReader();
                                                        if (readers.Read())
                                                        {
                                                            string ah1289;
                                                            ah1289 = readers["Balance"].ToString();
                                                            readers.Close();
                                                            con.Close();
                                                            con.Open();
                                                            SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

                                                            SqlDataReader reader66 = cmd166.ExecuteReader();

                                                            if (reader66.Read())
                                                            {
                                                                string ah11;
                                                                string ah1258;
                                                                ah11 = reader66["No"].ToString();
                                                                ah1258 = reader66["AccountType"].ToString();
                                                                reader66.Close();
                                                                con.Close();
                                                                con.Open();
                                                                Double M1 = Convert.ToDouble(ah1289);
                                                                double income = (totalpay - SC) / 1.15;
                                                                income += SC;
                                                                Double bl1 = income + M1;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + txtDateBank.Text + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','0','" + income + "','" + bl1 + "','" + txtDateBank.Text + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                cmd1974.ExecuteNonQuery();

                                                            }
                                                        }
                                                    }
                                                }
                                                //Selecting from cash acccount
                                                SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                                                {
                                                    DataTable dttax = new DataTable();
                                                    sdatax.Fill(dttax); long iss2 = dttax.Rows.Count;
                                                    //
                                                    if (iss2 != 0)
                                                    {
                                                        SqlDataReader readers = cmdintax.ExecuteReader();
                                                        if (readers.Read())
                                                        {
                                                            string ah1289;
                                                            ah1289 = readers["Balance"].ToString();
                                                            readers.Close();
                                                            con.Close();
                                                            con.Open();
                                                            SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                                            SqlDataReader reader66 = cmd166.ExecuteReader();

                                                            if (reader66.Read())
                                                            {
                                                                string ah11;
                                                                string ah1258;
                                                                ah11 = reader66["No"].ToString();
                                                                ah1258 = reader66["AccountType"].ToString();
                                                                reader66.Close();
                                                                con.Close();
                                                                con.Open();
                                                                Double M1 = Convert.ToDouble(ah1289);
                                                                double vatfree = (totalpay - SC) / 1.15;
                                                                double vat = (totalpay - SC) - vatfree;
                                                                Double bl1 = M1 + vat;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID.Text + "','','0','" + vat + "','" + bl1 + "','" + txtDateBank.Text + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                cmd1974.ExecuteNonQuery();

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            //Inserting to customer statement
                                            SqlCommand cmdreadb = new SqlCommand("select TOP 1* from tblCustomerStatement where Customer='" + PID.Text + "'  ORDER BY CSID DESC", con);

                                            SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                            if (readerbcustb.Read())
                                            {
                                                string ah11;

                                                ah11 = readerbcustb["Balance"].ToString();
                                                readerbcustb.Close();

                                                SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + txtDateBank.Text + "','Bulk Bank Payment','','" + totalpay + "','" + kc + "','" + ah11 + "','" + PID.Text + "')", con);
                                                custcmd.ExecuteNonQuery();
                                                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                if (readerAC.Read())
                                                {
                                                    String FN = readerAC["Name"].ToString();
                                                    readerAC.Close();
                                                    con.Close();
                                                    //Activity
                                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + txtDateBank.Text + "','Payment Received','Payment received from customer','" + PID.Text + "','Payment received from'+' '+'<b>" + PID.Text + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus.aspx')", con);
                                                    con.Open();
                                                    cmdAc.ExecuteNonQuery();
                                                    string money = "ETB";


                                                    //Updating the Due Date
                                                    SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID.Text + "'", con);
                                                    SqlDataReader readerup = cmdup.ExecuteReader();

                                                    if (readerup.Read())
                                                    {
                                                        String terms = readerup["PaymentDuePeriod"].ToString();
                                                        readerup.Close();
                                                        if (BankDueDate.Checked == false)
                                                        {
                                                            if (terms == "Monthly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(30);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            if (terms == "Every Three Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(90);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            if (terms == "Every Six Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(180);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            if (terms == "Yearly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(365);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID.Text + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                        }
                                                        if (CheckGene.Checked == true)
                                                        {


                                                            double payment = Convert.ToDouble(kc);

                                                            SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                            SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                            DataTable dtdf = new DataTable();
                                                            sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;

                                                            string ref2 = "RAKS-" + RandomPassword();
                                                            SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID.Text + "','" + ref2 + "','" + txtDateBank.Text + "','" + 0.ToString() + "','" + (payment - SC) + "','" + ah11 + "','" + nb + "','" + bindFSnumber() + "','Bank')", con);
                                                            cmdri.ExecuteNonQuery();
                                                            string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID.Text;
                                                            SqlCommand cmd197h1 = new SqlCommand("insert into tblNotification values('" + txtDateBank.Text + "','" + money + "'+' '+'" + totalpay.ToString("#,##0.00") + "'+' '+'Deposited into Cash at Bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                            cmd197h1.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + txtDateBank.Text + "','" + money + "'+' '+'" + Convert.ToDouble(kc).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID.Text + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','MN')", con);
                                                            cmd197h.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Response.Redirect("rentstatus1.aspx");
                }
            }
        }
        protected void btnDueDateUpdate_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox CheckRow = item.FindControl("chkRow3") as CheckBox;
                        Label PID = item.FindControl("Label2") as Label;
                        if (CheckRow.Checked == true)
                        {
                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + txtDueDate.Text + "' where customer='" + PID.Text + "'", con);
                            cmdrent.ExecuteNonQuery();
                            SqlCommand cmdcust = new SqlCommand("Update tblCustomers set duedate='" + txtDueDate.Text + "' where FllName='" + PID.Text + "'", con);
                            cmdcust.ExecuteNonQuery();
                        }

                    }
                }
            }
            Response.Redirect("rentstatus1.aspx");
        }
        protected void btnBulkSMS_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox CheckRow = item.FindControl("chkRow3") as CheckBox;
                        Label PID = item.FindControl("Label2") as Label;
                        if (CheckRow.Checked == true)
                        {
                            SqlCommand cmd41 = new SqlCommand("select * from tblCustomers where FllName='" + PID.Text + "'", con);
                            SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                            DataTable dt1 = new DataTable();
                            sda1.Fill(dt1);
                            if (dt1.Rows[0][6].ToString() != "")
                            {

                                string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                                string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                                TwilioClient.Init(accountSid, authToken);

                                var message = MessageResource.Create(
                                    body: txtBulkmessage.Text,
                                    from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                    to: new Twilio.Types.PhoneNumber("+251" + dt1.Rows[0][6].ToString())
                                );
                            }
                        }
                    }
                }
            }
        }
        protected void btnAmountCondition_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                str = "select * from tblrent where currentperiodue > '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            else if (less.Checked == true)
            {
                str = "select * from tblrent where currentperiodue < '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            else
            {
                str = "select * from tblrent where currentperiodue = '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtShopNo.Text);
            str = "select * from tblrent where shopno LIKE '%" + name + "%'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btnAgreeUpdate_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox CheckRow = item.FindControl("chkRow3") as CheckBox;
                        Label PID = item.FindControl("Label2") as Label;
                        if (CheckRow.Checked == true)
                        {
                            SqlCommand cmdcust = new SqlCommand("Update tblCustomers set agreementdate='" + txtAgreeDate.Text + "' where FllName='" + PID.Text + "'", con);
                            cmdcust.ExecuteNonQuery();
                        }

                    }
                }
            }
            Response.Redirect("rentstatus1.aspx");
        }
        protected void btnUncollected_Click1(object sender, EventArgs e)
        {
            bindstatus1();
            con.Visible = true;
            String PID = "report";
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "rentstatus_" + PID + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/x-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            con.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
    }
}
