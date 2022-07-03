using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace advtech.Finance.Accounta
{
    public partial class Home : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        public static string pid = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {

                    ShowData(); BindBrandsRptr8(); organization(); BindBrandsRptrd();
                    BindBrandsRptra(); BindBrandsRptrb(); BindBrandsRptrc();
                    cashflowbar(); networth(); invoicebar(); profit(); bindCashDrop();
                    bindTax(); bindcurrentliab(); bindlongtermliab();
                    bindshop(); BindUncollected(); ShowUnpaidDoughnutIncome(); bindcustomerno(); ;
#pragma warning disable CS0472 // The result of the expression is always 'false' since a value of type 'double' is never equal to 'null' of type 'double?'
                    if (Depreciate26().ToString() == "" || Depreciate26() == null)
#pragma warning restore CS0472 // The result of the expression is always 'false' since a value of type 'double' is never equal to 'null' of type 'double?'
                    {
                        deprec.InnerText = "0.00";
                    }
                    else
                    {
                        deprec.InnerText = Depreciate26().ToString("#,##0.00");
                    }
                    ShowData2(); ShowPieAR(); ShowBarAR(); ShowUnpaidDoughnut(); ShowUnpaidDoughnutshop();
                    BalanceSheetDate.InnerText = "As of " + DateTime.Now.ToString("dd MMM yyyy");
                    SystemDate.InnerText = "As of " + DateTime.Now.ToString("MMM dd, yyyy");
                    bindduepassedtotal(); unpaidcounter(); BindBalanceUnrecorded();
                    agrremenetdateCounter(); bindsusshop();

                }

            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindsusshop()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblshop where status='SUSPENDED'", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    SUScounter.InnerText = i.ToString();
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
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;

                }
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string duetotal;
                    duetotal = reader["duetotal"].ToString();
                    if (duetotal == "" || duetotal == null)
                    {
                        Span2.InnerText = "0.00";
                    }
                    else
                    {
                        Span2.InnerText = Convert.ToDouble(duetotal).ToString("#,##0.00");
                    }

                }
            }
        }
        private void BindUncollected()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmds = new SqlCommand("select * from tblcreditnote where balance>0", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dt = new DataTable();
                    sdas.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Repeater2.DataSource = dt;
                        Repeater2.DataBind();
                        con.Close(); main5.Visible = false;
                    }
                    else
                    {
                        DivCustomerDue.Visible = false;
                        CustomerWithCreditSpan.Visible = false;
                    }
                }
            }
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                foreach (RepeaterItem item in Repeater2.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        Label lblPhone = item.FindControl("lblPhone") as Label;
                        Label lblCustomer = item.FindControl("Label2") as Label;
                        Label lblAged = item.FindControl("lblAged") as Label;
                        Label lbl = item.FindControl("lblDueDate") as Label;
                        DateTime today = DateTime.Now.Date;
                        DateTime duedate = Convert.ToDateTime(lbl.Text);
                        TimeSpan t = today - duedate;
                        string dayleft = t.TotalDays.ToString();
                        lblAged.Text = dayleft + " Days";
                        SqlCommand cmdpp = new SqlCommand("select*from tblCustomers where FllName=N'" + lblCustomer.Text + "'", con);

                        SqlDataReader readerpp = cmdpp.ExecuteReader();
                        if (readerpp.Read())
                        {
                            string mobile;
                            mobile = readerpp["Mobile"].ToString(); readerpp.Close();
                            lblPhone.Text = mobile;
                        }
                    }
                }
            }
        }
        private void bindshop()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmds = new SqlCommand("select * from tblshop where status='Free' or status='SUSPENDED'", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dt = new DataTable();

                    sdas.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind(); main4.Visible = false;
                    }
                    else
                    {
                        shopDiv.Visible = false;
                        FreeShopSpan.Visible = false;
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
                    Label lbl = item.FindControl("Label4") as Label;
                    if (lbl.Text == "Free")
                    {
                        lbl.Attributes.Add("class", "badge badge-success");
                    }
                    else if (lbl.Text == "Occupied")
                    {
                        lbl.Attributes.Add("class", "badge badge-danger");
                    }
                    else if (lbl.Text == "SUSPENDED")
                    {
                        lbl.Attributes.Add("class", "badge badge-warning");
                    }

                }
            }
        }
        private void bindlongtermliab()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select (SUM(Credit)-SUM(Debit)) as Balance  from tblGeneralLedger where AccountType='Long Term Liabilities'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string asset;
                asset = reader["Balance"].ToString();
                if (asset == "" || asset == null)
                {

                }
                else
                {
                    LongTermliability.InnerText = Convert.ToDouble(asset).ToString("#,##0.00");
                }

            }
        }
        private void bindcurrentliab()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select (SUM(Credit)-SUM(Debit)) as Balance  from tblGeneralLedger where AccountType='Other Current Liabilities' OR AccountType='Accounts Payable'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string asset;
                asset = reader["Balance"].ToString();
                if (asset == "" || asset == null)
                {

                }
                else
                {
                    Currentliability.InnerText = Convert.ToDouble(asset).ToString("#,##0.00");
                }

            }
        }
        private void bindTax()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dtBrandss = new DataTable();
                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                    SqlCommand cmd122 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                    using (SqlDataAdapter sda22 = new SqlDataAdapter(cmd122))
                    {
                        DataTable dtBrandss22 = new DataTable();
                        sda22.Fill(dtBrandss22); int iss22 = dtBrandss.Rows.Count;
                        //
                        if (iss22 != 0)
                        {

                            SqlDataReader readers = cmd122.ExecuteReader();

                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString(); readers.Close();
                                SqlCommand cmdin = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][7].ToString() + "'", con);
                                using (SqlDataAdapter sdain = new SqlDataAdapter(cmdin))
                                {
                                    DataTable dtin = new DataTable();
                                    sdain.Fill(dtin); int isin = dtin.Rows.Count;
                                    //
                                    if (isin != 0)
                                    {

                                        SqlDataReader readersin = cmdin.ExecuteReader();

                                        if (readersin.Read())
                                        {
                                            string ahin;
                                            ahin = readersin["Balance"].ToString();
                                            Taxt.InnerText = (Convert.ToDouble(ah1289) + Convert.ToDouble(ahin)).ToString("#,##0.00");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void profit()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select (SUM(Credit)-SUM(Debit)) as Balance  from tblGeneralLedger where AccountType='Income' group by year(Date)";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string income;
                income = reader["Balance"].ToString();
                reader.Close();
                con.Close();
                String query1 = "select (SUM(Debit)-SUM(Credit)) as Balance from tblGeneralLedger where AccountType='Expenses' OR AccountType='Cost of Sales' group by year(Date)";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    string expense;
                    expense = reader1["Balance"].ToString();
                    reader1.Close();
                    con.Close();
                    double nw = Convert.ToDouble(income) - Convert.ToDouble(expense);
                    if (nw < 0)
                    {
                        nw = -nw;
                        Span2.InnerText = nw.ToString("#,##0.00");
                        NetProfit.InnerText = nw.ToString("#,##0.00");
                        NetText1.InnerText = "Net Loss";
                        NetProfit.Attributes.Add("class", "text-danger font-weight-bold  text-uppercase");
                        Revenues.InnerText = Convert.ToDouble(income).ToString("#,##0.00");
                        Expenses1.InnerText = Convert.ToDouble(expense).ToString("#,##0.00");
                        Span2.Attributes.Add("class", "text-danger");

                    }
                    else
                    {
                        Revenues.InnerText = Convert.ToDouble(income).ToString("#,##0.00");
                        Expenses1.InnerText = Convert.ToDouble(expense).ToString("#,##0.00");
                        Span2.InnerText = nw.ToString("#,##0.00");
                        NetProfit.InnerText = nw.ToString("#,##0.00");
                    }

                }
            }
        }
        private void organization()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblOrganization", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {

                }
                else
                {
                    Response.Redirect("getstarted.aspx");
                }
            }
        }
        private void invoicebar()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand("select Sum(InvAmount) as Total,Sum(Payment) as PAID  from tblCustomerStatement", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string ah7g; string paid;
                    ah7g = reader["Total"].ToString();
                    paid = reader["PAID"].ToString();
                    reader.Close();

                    if (ah7g != "")
                    {

                        paidinv.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                    }
                    if (paid != "")
                    {
                        unpaidinv.InnerText = Convert.ToDouble(paid).ToString("#,##0.00");
                    }
                    //Creating the progress bar
                    double paid1 = 0; double unpaid1 = 0;

                    if (paidinv.InnerText != "")
                    {
                        paid1 = Convert.ToDouble(paidinv.InnerText);
                    }
                    if (unpaidinv.InnerText != "")
                    {
                        unpaid1 = Convert.ToDouble(unpaidinv.InnerText);
                    }
                    Double C = Math.Round(paid1);
                    Double C1 = Math.Round(unpaid1);
                    Double pd = C - C1;

                    Double C2 = Math.Round((C1 / C) * 100);
                    Double R = 100 - C2;
                    unpaidbill.Style.Add("width", R.ToString() + "%");
                    paidbill.Style.Add("width", C2.ToString() + "%");
                }
            }
        }
        private void cashflowbar()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblDashBoardSetting", con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                    if (i3 == 0)
                    {
                        SqlCommand cmd = new SqlCommand("select SUM(Debit) Debit,SUM(Credit) Credit from tblGeneralLedger where AccountType='Cash' and year(Date)='" + DateTime.Now.Year + "' group by Year(Date) ", con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string ah7g;
                            string ah5;
                            ah5 = reader["Debit"].ToString();
                            ah7g = reader["Credit"].ToString();

                            con.Close();
                            if (ah7g != "" || ah5 != "")
                            {
                                Double C = Math.Round(Convert.ToDouble(ah5));
                                Double C1 = Math.Round(Convert.ToDouble(ah7g));
                                Double Total = C + C1;
                                Double C2 = Math.Round((C1 / Total) * 100);
                                Double R = 100 - C2;
                                Atr2.Style.Add("width", R.ToString() + "%");
                                Atr3.Style.Add("width", C2.ToString() + "%");
                                inflow.InnerText = Convert.ToDouble(ah5).ToString("#,##0.00");
                                outflow.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                            }
                            else
                            {

                            }
                        }
                        reader.Close();
                    }
                    else
                    {
                        SqlCommand cmdpos = new SqlCommand("select*from tblDashBoardSetting", con);
                        SqlDataReader readerpos = cmdpos.ExecuteReader();

                        if (readerpos.Read())
                        {
                            String department = readerpos["cashset"].ToString(); readerpos.Close();
                            if (department == "This Year")
                            {
                                SqlCommand cmd = new SqlCommand("select SUM(Debit) Debit,SUM(Credit) Credit from tblGeneralLedger where AccountType='Cash' and year(Date)='" + DateTime.Now.Year + "' group by Year(Date) ", con);
                                SqlDataReader reader = cmd.ExecuteReader();

                                if (reader.Read())
                                {
                                    string ah7g;
                                    string ah5;
                                    ah5 = reader["Debit"].ToString();
                                    ah7g = reader["Credit"].ToString();

                                    con.Close();
                                    if (ah7g != "" || ah5 != "")
                                    {
                                        Double C = Math.Round(Convert.ToDouble(ah5));
                                        Double C1 = Math.Round(Convert.ToDouble(ah7g));
                                        Double Total = C + C1;
                                        Double C2 = Math.Round((C1 / Total) * 100);
                                        Double R = 100 - C2;
                                        Atr2.Style.Add("width", R.ToString() + "%");
                                        Atr3.Style.Add("width", C2.ToString() + "%");
                                        inflow.InnerText = Convert.ToDouble(ah5).ToString("#,##0.00");
                                        outflow.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                                    }
                                    else
                                    {

                                    }
                                    reader.Close();
                                }

                            }
                            else
                            {
                                Int64 year = DateTime.Now.Year;
                                Int64 difYear = year - 1;
                                SqlCommand cmdy = new SqlCommand("select SUM(Debit) Debit,SUM(Credit) Credit from tblGeneralLedger where AccountType='Cash' and year(Date)='" + difYear + "' group by Year(Date) ", con);
                                SqlDataReader readery = cmdy.ExecuteReader();

                                if (readery.Read())
                                {
                                    string ah7g;
                                    string ah5;
                                    ah5 = readery["Debit"].ToString();
                                    ah7g = readery["Credit"].ToString();

                                    con.Close();
                                    if (ah7g != "" || ah5 != "")
                                    {
                                        Double C = Math.Round(Convert.ToDouble(ah5));
                                        Double C1 = Math.Round(Convert.ToDouble(ah7g));
                                        Double Total = C + C1;
                                        Double C2 = Math.Round((C1 / Total) * 100);
                                        Double R = 100 - C2;
                                        Atr2.Style.Add("width", R.ToString() + "%");
                                        Atr3.Style.Add("width", C2.ToString() + "%");
                                        inflow.InnerText = Convert.ToDouble(ah5).ToString("#,##0.00");
                                        outflow.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                                    }
                                    else
                                    {

                                    }
                                    readery.Close();
                                }

                            }
                        }

                    }
                }

            }
        }
        private void BindBrandsRptr8()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd16g = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where balance > 0 ", con);
                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd16g))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
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
                                Span9.InnerText = "0.00";
                            }
                            else
                            {

                                Span9.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                            }


                        }

                    }
                    else
                    {
                        Span9.InnerText = "ETB 0.00";
                    }
                }
            }
        }
        private void BindBrandsRptrb()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();
                DateTime daysLeft = DateTime.Now.Date;

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') >30 and DATEDIFF(day, date, '" + daysLeft + "')<61 and balance>0", con);
                SqlDataReader reader6 = cmd16.ExecuteReader();
                if (reader6.Read())
                {
                    string ah;
                    ah = reader6["Balance"].ToString();
                    if (ah == "")
                    {

                    }
                    else
                    {

                        two.InnerText = Convert.ToDouble(ah).ToString("#,##0.00");

                    }
                    reader6.Close();
                    con.Close();
                }
            }
        }
        private void BindBrandsRptra()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();

                DateTime daysLeft = DateTime.Now.Date;

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') < 31 and balance>0", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd16))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                    if (i != 0)
                    {
                        SqlDataReader reader6 = cmd16.ExecuteReader();
                        if (reader6.Read())
                        {
                            string ah;
                            ah = reader6["Balance"].ToString(); reader6.Close();
                            if (ah == "")
                            {

                            }
                            else
                            {

                                one.InnerText = (Convert.ToDouble(ah)).ToString("#,##0.00");

                            }

                            con.Close();
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
        private void BindBrandsRptrc()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();

                DateTime daysLeft = DateTime.Now.Date;

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') > 60 and DATEDIFF(day, date, '" + daysLeft + "') <= 90 and balance>0", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd16))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                    if (i != 0)
                    {
                        SqlDataReader reader6 = cmd16.ExecuteReader();
                        if (reader6.Read())
                        {
                            string ah;
                            ah = reader6["Balance"].ToString();
                            if (ah == "")
                            {

                            }
                            else
                            {
                                three.InnerText = Convert.ToDouble(ah).ToString("#,##0.00");
                            }
                            reader6.Close();
                            con.Close();
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
        private void BindBrandsRptrd()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();

                DateTime daysLeft = DateTime.Now.Date;

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') > 90 and balance>0", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd16))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                    if (i != 0)
                    {
                        SqlDataReader reader6 = cmd16.ExecuteReader();
                        if (reader6.Read())
                        {
                            string ah;
                            ah = reader6["Balance"].ToString();
                            if (ah == "")
                            {

                            }
                            else
                            {
                                four.InnerText = Convert.ToDouble(ah).ToString("#,##0.00");
                            }
                            reader6.Close();
                            con.Close();
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
        private double BindAssetBillsBalance()
        {
            double balance = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd16g = new SqlCommand("select SUM(BalanceDue) BalanceDue from tblAsssetBills ", con);
                SqlDataReader reader6g = cmd16g.ExecuteReader();

                if (reader6g.Read())
                {
                    string ah7g;
                    ah7g = reader6g["BalanceDue"].ToString();
                    reader6g.Close();
                    if (ah7g == null || ah7g == "")
                    {
                    }
                    else
                    {
                        balance = Convert.ToDouble(ah7g);
                    }
                }
            }
            return balance;
        }
        private void ShowData()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblDashBoardSetting", con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                    if (i3 == 0)
                    {
                        String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();

                        String query = "select DATEPART(qq,Date) as month_name ,sum (InvAmount) as inv  from tblCustomerStatement  where YEAR(Date)='" + DateTime.Now.Year + "'  group by DATEPART(qq,Date)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                        {
                            DataTable dtBrands232c3 = new DataTable();
                            sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                            if (i2c3 != 0)
                            {


                                String chart = "";
                                chart = "<canvas id=\"line-chart2\" width=\"100%\" height=\"250\"></canvas>";
                                chart += "<script>";
                                chart += "new Chart(document.getElementById(\"line-chart2\"), { type: 'bar', data: {labels: [";

                                // more detais

                                for (int i2 = 0; i2 < dtBrands232c3.Rows.Count; i2++)

                                    chart += "\"" + ("Quarter " + dtBrands232c3.Rows[i2]["month_name"].ToString()) + "\"" + ",";

                                chart += "],datasets: [{ data: [";

                                // get data from database and add to chart
                                String value = "";
                                for (int im = 0; im < dtBrands232c3.Rows.Count; im++)
                                    value += (Convert.ToDouble(dtBrands232c3.Rows[im]["inv"]) / 1000000).ToString() + ",";

                                chart += value;



                                chart += "],label: \"Income\",display: false,borderColor: \"#a04ad4\",backgroundColor: \"#a04ad4\",hoverBackgroundColor: \"#FF9900\"},"; // Chart color

                                chart += "]";
                                chart += "},";
                                chart += "options: { indexAxis: 'y',maintainAspectRatio: false,layout: {padding: {left: 0,right: 0,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 12},maxBarThickness: 25,}],},legend: {display: false, position:'right'},gridLines: {display: false},}"; // Chart title

                                chart += "});";

                                chart += "</script>";

                                ltChart.Text = chart; main.Visible = false;

                            }
                            else { main.Visible = true; ltChart.Visible = false; info.Visible = false; infoicon.Visible = false; }
                        }
                    }
                    else
                    {
                        SqlCommand cmdpos = new SqlCommand("select*from tblDashBoardSetting", con);
                        SqlDataReader readerpos = cmdpos.ExecuteReader();

                        if (readerpos.Read())
                        {
                            String department = readerpos["revenset"].ToString(); readerpos.Close();
                            if (department == "This Year")
                            {
                                String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();

                                String query = "select DATEPART(qq,Date) as month_name ,sum (InvAmount) as inv  from tblCustomerStatement  where YEAR(Date)='" + DateTime.Now.Year + "'  group by DATEPART(qq,Date)";
                                SqlCommand cmd = new SqlCommand(query, con);
                                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                                {
                                    DataTable dtBrands232c3 = new DataTable();
                                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                                    if (i2c3 != 0)
                                    {


                                        String chart = "";
                                        chart = "<canvas id=\"line-chart2\" width=\"100%\" height=\"250\"></canvas>";
                                        chart += "<script>";
                                        chart += "new Chart(document.getElementById(\"line-chart2\"), { type: 'bar', data: {labels: [";

                                        // more detais

                                        for (int i2 = 0; i2 < dtBrands232c3.Rows.Count; i2++)

                                            chart += "\"" + ("Quarter " + dtBrands232c3.Rows[i2]["month_name"].ToString()) + "\"" + ",";

                                        chart += "],datasets: [{ data: [";

                                        // get data from database and add to chart
                                        String value = "";
                                        for (int im = 0; im < dtBrands232c3.Rows.Count; im++)
                                            value += (Convert.ToDouble(dtBrands232c3.Rows[im]["inv"]) / 1000000).ToString() + ",";

                                        chart += value;



                                        chart += "],label: \"Income\",display: false,borderColor: \"#a04ad4\",backgroundColor: \"#a04ad4\",hoverBackgroundColor: \"#FF9900\"},"; // Chart color

                                        chart += "]";
                                        chart += "},";
                                        chart += "options: { indexAxis: 'y',maintainAspectRatio: false,layout: {padding: {left: 0,right: 0,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 12},maxBarThickness: 25,}],},legend: {display: false, position:'right'},gridLines: {display: false},}"; // Chart title

                                        chart += "});";

                                        chart += "</script>";

                                        ltChart.Text = chart; main.Visible = false;

                                    }
                                    else { main.Visible = true; ltChart.Visible = false; info.Visible = false; infoicon.Visible = false; }
                                }
                            }
                            else
                            {
                                Int64 year = DateTime.Now.Year;
                                Int64 difYear = year - 1;
                                String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
                                String query = "select DATEPART(qq,Date) as month_name ,sum (InvAmount) as inv  from tblCustomerStatement  where YEAR(Date)='" + difYear + "'  group by DATEPART(qq,Date)";

                                SqlCommand cmd = new SqlCommand(query, con);
                                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                                {
                                    DataTable dtBrands232c3 = new DataTable();
                                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                                    if (i2c3 != 0)
                                    {

                                        String chart = "";
                                        chart = "<canvas id=\"line-chart2\" width=\"100%\" height=\"280\"></canvas>";
                                        chart += "<script>";
                                        chart += "new Chart(document.getElementById(\"line-chart2\"), { type: 'bar', data: {labels: [";

                                        // more detais

                                        for (int i2 = 0; i2 < dtBrands232c3.Rows.Count; i2++)

                                            chart += "\"" + ("Quarter " + dtBrands232c3.Rows[i2]["month_name"].ToString()) + "\"" + ",";

                                        chart += "],datasets: [{ data: [";

                                        // get data from database and add to chart
                                        String value = "";
                                        for (int im = 0; im < dtBrands232c3.Rows.Count; im++)
                                            value += (Convert.ToDouble(dtBrands232c3.Rows[im]["inv"]) / 1000000).ToString() + ",";

                                        chart += value;



                                        chart += "],label: \"Income\",display: false,borderColor: \"#a04ad4\",backgroundColor: \"#a04ad4\",hoverBackgroundColor: \"#FF9900\"},"; // Chart color

                                        chart += "]";
                                        chart += "},";
                                        chart += "options: { indexAxis: 'y',maintainAspectRatio: false,layout: {padding: {left: 0,right: 0,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 12},maxBarThickness: 25,}],},legend: {display: false, position:'right'},gridLines: {display: false},}"; // Chart title

                                        chart += "});";

                                        chart += "</script>";

                                        ltChart.Text = chart; main.Visible = false;

                                    }
                                    else { main.Visible = true; ltChart.Visible = false; info.Visible = false; infoicon.Visible = false; }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void ShowData2()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String query = "select TOP (5) Balance as Total, Account as month_name from tblGeneralLedger2  where AccountType='Expenses' or AccountType='Cost of Sales' Order by Balance DESC";
            SqlCommand cmd = new SqlCommand(query, con);
            using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
            {
                DataTable dtBrands232c3 = new DataTable();
                sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                if (i2c3 != 0)
                {
                    String chart = "";
                    chart = "<canvas id=\"line-chart\" width=\"100%\" height=\"250\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"line-chart\"), { type: 'doughnut', data: {labels: [";

                    // more detais

                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                        chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                    chart += "],datasets: [{ data: [";

                    // get data from database and add to chart
                    String value = "";
                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                        value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"])).ToString("##0.00") + ",";
                    value = value.Substring(0, value.Length - 1);

                    chart += value;

                    chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                    chart += "]},options: {elements: {center: {text: \'Red is 2/3 of the total numbers\',color: \'#FF6384\',fontStyle: \'Arial\'}},maintainAspectRatio: false,layout: {padding: { xPadding: 15, caretPadding: 10,borderWidth: 1,yPadding: 15}},scales: {xAxes: [{display: false,gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 25,}],},title:{display: false},legend: {display: false},cutoutPercentage: 79}"; // Chart title

                    chart += "});";

                    chart += "</script>";

                    Literal1.Text = chart; main1.Visible = false;
                }
                else { main1.Visible = true; Literal1.Visible = false; }

            }

        }
        private double bindtotalpaid()
        {
            double paid = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select  SUM(currentperiodue) as duetotal from tblrent where status='Active' and  DATEDIFF(day, '" + DateTime.Now.Date + "', duedate)>0", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;

                    if (i2c3 > 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string duetotal;
                            duetotal = reader["duetotal"].ToString();
                            if (duetotal == null || duetotal == "")
                            {
                                paid = 0;
                            }
                            else
                            {
                                paid += Convert.ToDouble(duetotal);
                            }
                        }
                    }
                }
            }
            return paid;
        }
        private Tuple<int, double> bindtotal()
        {
            int count = 0; double unpaid = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select  SUM(currentperiodue) as duetotal from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=15 and status='Active' ", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                    count += i2c3;
                    if (i2c3 > 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string duetotal;
                            duetotal = reader["duetotal"].ToString();
                            if (duetotal == "" || duetotal == null)
                            {
                                unpaid = 0;
                            }
                            else
                            {
                                unpaid += Convert.ToDouble(duetotal);
                            }
                        }
                    }
                }
            }
            return Tuple.Create(count, unpaid);
        }
        private void bindcustomerno()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblCustomers where Status='Active'", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                    ShopCustomer.InnerText = i2c3.ToString();

                    //BindFreeShop
                    SqlCommand cmd1 = new SqlCommand("select*from tblshop where Status='Free'", con);

                    using (SqlDataAdapter sda22c3c = new SqlDataAdapter(cmd1))
                    {
                        DataTable dtBrands232c3c = new DataTable();
                        sda22c3c.Fill(dtBrands232c3c); int i2c3c = dtBrands232c3c.Rows.Count;
                        ShopFree.InnerText = i2c3c.ToString();

                        //Bind occupied
                        SqlCommand cmd2 = new SqlCommand("select*from tblshop where status='Free' or status='Occupied'", con);

                        using (SqlDataAdapter sda22c31 = new SqlDataAdapter(cmd2))
                        {
                            DataTable dtBrands232c31 = new DataTable();
                            sda22c31.Fill(dtBrands232c31); double i2c31 = dtBrands232c31.Rows.Count;

                            //Bind All
                            SqlCommand cmd3 = new SqlCommand("select*from tblshop where status='Occupied'", con);

                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd3))
                            {
                                DataTable dt = new DataTable();
                                sda.Fill(dt); double i = dt.Rows.Count;
                                double percentage_occupied = Convert.ToDouble(i / i2c31);
                                ShopPercentage_Occupy.InnerText = Math.Round(percentage_occupied * 100).ToString() + "%";
                            }
                        }
                    }
                }
            }
        }
        private Tuple<double, double> bindshopcount()
        {
            double free = 0; double occupied = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //BindFreeShop
                SqlCommand cmd1 = new SqlCommand("select*from tblshop where Status='Free'", con);

                using (SqlDataAdapter sda22c3c = new SqlDataAdapter(cmd1))
                {
                    DataTable dtBrands232c3c = new DataTable();
                    sda22c3c.Fill(dtBrands232c3c); free = dtBrands232c3c.Rows.Count;

                    //Bind All
                    SqlCommand cmd3 = new SqlCommand("select*from tblshop where status='Occupied'", con);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd3))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); occupied = dt.Rows.Count;
                    }
                }
            }
            return Tuple.Create(free, occupied);
        }
        private void ShowUnpaidDoughnut()
        {

            double unpaid = bindtotal().Item2; double paid = bindtotalpaid();
            double paid_percentage = (paid / (unpaid + paid)) * 100;
            double unpaid_percentage = (100 - paid_percentage);
            PercentageUnpaid.InnerText = Math.Round(unpaid_percentage).ToString() + "%";
            RentPaidAmount.InnerText = paid.ToString("#,##0.00");
            RentUnpaidAmount.InnerText = unpaid.ToString("#,##0.00");
            string a = "Paid"; string b = "Unpaid";
            string cm = ",";
            string apst = "\"";
            String chart = " ";
            chart += "<canvas id=\"line-unpaid\" width=\"100%\" height=\"250\"></canvas>";
            chart += "<script>";
            chart += "new Chart(document.getElementById(\"line-unpaid\"), { type: 'doughnut', data: {";
            chart += "labels: [" + apst + a + apst + cm + apst + b + apst + "]";

            chart += ",datasets: [{ data: [";

            String value = "";

            value += paid + "," + unpaid + ",";

            value = value.Substring(0, value.Length - 1);
            chart += value;
            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#10e469\",\"#a20fb2\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
            chart += "]},options: {elements: {center: {text: \'Red is 2/3 of the total numbers\',color: \'#FF6384\',fontStyle: \'Arial\'}},maintainAspectRatio: false,layout: {padding: { xPadding: 15, caretPadding: 10,borderWidth: 1,yPadding: 15}},scales: {xAxes: [{display: false,gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 25,}],},title:{display: false},legend: {display: false},cutoutPercentage: 76}"; // Chart title

            chart += "});";

            chart += "</script>";

            ltrUnpaid.Text = chart;
        }
        private void ShowUnpaidDoughnutshop()
        {
            double occupied = bindshopcount().Item2;
            double free = bindshopcount().Item1;

            string a = "Free Shop"; string b = "Occupied Shop";
            string cm = ",";
            string apst = "\"";
            String chart = " ";
            chart += "<canvas id=\"line-unpaidb\" width=\"100%\" height=\"250\"></canvas>";
            chart += "<script>";
            chart += "new Chart(document.getElementById(\"line-unpaidb\"), { type: 'doughnut', data: {";
            chart += "labels: [" + apst + a + apst + cm + apst + b + apst + "]";

            chart += ",datasets: [{ data: [";

            String value = "";

            value += free + "," + occupied + ",";

            value = value.Substring(0, value.Length - 1);
            chart += value;
            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#a20fb2\",\"#ff6a00\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
            chart += "]},options: {elements: {center: {text: \'Red is 2/3 of the total numbers\',color: \'#FF6384\',fontStyle: \'Arial\'}},maintainAspectRatio: false,layout: {padding: { xPadding: 15, caretPadding: 10,borderWidth: 1,yPadding: 15}},scales: {xAxes: [{display: false,gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 25,}],},title:{display: false},legend: {display: false},cutoutPercentage: 76}"; // Chart title

            chart += "});";

            chart += "</script>";


            Literal2.Text = chart;

        }
        private void ShowUnpaidDoughnutIncome()
        {
            if (Expenses1.InnerText == "0.00" && Revenues.InnerText == "0.00")
            {
                mainb.Visible = true; Literal3.Visible = false;
            }
            else
            {
                double paid = Convert.ToDouble(Revenues.InnerText); double unpaid = Convert.ToDouble(Expenses1.InnerText);
                double paid_percentage = (paid / (unpaid + paid));
                double unpaid_percentage = (1 - paid_percentage);
                string a = "Revenues"; string b = "Expenses";
                string cm = ",";
                string apst = "\"";
                String chart = " ";
                chart += "<canvas id=\"line-unpaid22\" width=\"100%\" height=\"250\"></canvas>";
                chart += "<script>";
                chart += "new Chart(document.getElementById(\"line-unpaid22\"), { type: 'doughnut', data: {";
                chart += "labels: [" + apst + a + apst + cm + apst + b + apst + "]";

                chart += ",datasets: [{ data: [";

                String value = "";

                value += paid + "," + unpaid + ",";

                value = value.Substring(0, value.Length - 1);
                chart += value;
                chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#d5ff00\",\"#10e469\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                chart += "]},options: {elements: {center: {text: \'Red is 2/3 of the total numbers\',color: \'#FF6384\',fontStyle: \'Arial\'}},maintainAspectRatio: false,layout: {padding: { xPadding: 15, caretPadding: 10,borderWidth: 1,yPadding: 15}},scales: {xAxes: [{display: false,gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 25,}],},title:{display: false},legend: {display: false},cutoutPercentage: 76}"; // Chart title

                chart += "});";

                chart += "</script>";

                Literal3.Text = chart; Literal3.Visible = true;
                mainb.Visible = false;
            }

        }
        private void networth()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select (SUM(Debit)-SUM(Credit)) as Balance  from tblGeneralLedger where AccountType='Accounts Receivable'  OR  AccountType='Cash' OR AccountType='Other Assets'  OR  AccountType='Inventory'  OR  AccountType='Other Current Assets'   OR  AccountType='Fixed Assets' group by year(Date)";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string asset;
                asset = reader["Balance"].ToString();
                reader.Close();
                con.Close();
                String query1 = "select (SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where AccountType='Accounts Payable' OR AccountType='Other Current Liabilities'  OR AccountType='Long Term Liabilities'   OR  AccountType='Accumulated Depreciation'";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    string liability;
                    liability = reader1["Balance"].ToString();
                    reader1.Close();
                    con.Close();
                    double nw = Convert.ToDouble(asset) - Convert.ToDouble(liability);
                    Span3.InnerText = nw.ToString("#,##0.00");
                    Asset2.InnerText = Convert.ToDouble(asset).ToString("#,##0.00");
                    Liability2.InnerText = Convert.ToDouble(liability).ToString("#,##0.00");
                    NetWorth.InnerText = Convert.ToDouble(nw).ToString("#,##0.00");
                }
            }
        }
        private double Depreciate26()
        {
            double dexp = 0;
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select (SUM(Credit)-SUM(Debit)) as Balance  from tblGeneralLedger where AccountType='Accumulated Depreciation'";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string asset;
                asset = reader["Balance"].ToString();
                if (asset != "" || asset != null)
                {
                    dexp = Convert.ToDouble(asset);
                }
                reader.Close();
                con.Close();
            }
            return dexp;
        }
        private void bindCashDrop()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdpos = new SqlCommand("select*from tblDashBoardSetting", con);
                SqlDataReader readerpos = cmdpos.ExecuteReader();

                if (readerpos.Read())
                {
                    String cashcurrentyear2 = readerpos["cashset"].ToString();
                    String billcurrentyear = readerpos["billinset"].ToString();
                    String incomecurrent = readerpos["revenset"].ToString();
                    cashcurrentyear.InnerText = cashcurrentyear2;
                    cashdrop.InnerText = cashcurrentyear2;
                    billsdrop.InnerText = billcurrentyear;
                    billcurrentyear2.InnerText = billcurrentyear;
                    currentRevenueYear.InnerText = incomecurrent;
                    incomedrop.InnerText = incomecurrent;
                }
                readerpos.Close();
            }
        }
        protected void btnCashThisYear_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblDashBoardSetting", con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                    if (i3 == 0)
                    {
                        SqlCommand cmd111t = new SqlCommand("insert into tblDashBoardSetting values('This Year','This Year','This Year')", con);
                        cmd111t.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set cashset='This Year'", con);
                        cmdre.ExecuteNonQuery();
                    }
                    Response.Redirect("Home.aspx");
                }
            }
        }
        protected void btnCashLastYear_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblDashBoardSetting", con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                    if (i3 == 0)
                    {
                        SqlCommand cmd111t = new SqlCommand("insert into tblDashBoardSetting values('This Year','This Year','This Year')", con);
                        cmd111t.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set cashset='Last Year'", con);
                        cmdre.ExecuteNonQuery();
                    }
                    Response.Redirect("Home.aspx");
                }
            }
        }
        protected void btnBillThisYear_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblDashBoardSetting", con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                    if (i3 == 0)
                    {
                        SqlCommand cmd111t = new SqlCommand("insert into tblDashBoardSetting values('This Year','This Year','This Year')", con);
                        cmd111t.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set billinset='This Year'", con);
                        cmdre.ExecuteNonQuery();
                    }
                    Response.Redirect("Home.aspx");
                }
            }
        }
        protected void btnBillLastYear_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblDashBoardSetting", con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                    if (i3 == 0)
                    {
                        SqlCommand cmd111t = new SqlCommand("insert into tblDashBoardSetting values('This Year','This Year','This Year')", con);
                        cmd111t.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set billinset='Last Year'", con);
                        cmdre.ExecuteNonQuery();
                    }
                    Response.Redirect("Home.aspx");
                }
            }
        }
        protected void btnincomeThisYear_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblDashBoardSetting", con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                    if (i3 == 0)
                    {
                        SqlCommand cmd111t = new SqlCommand("insert into tblDashBoardSetting values('This Year','This Year','This Year')", con);
                        cmd111t.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set revenset='This Year'", con);
                        cmdre.ExecuteNonQuery();
                    }
                    Response.Redirect("Home.aspx");
                }
            }
        }
        protected void btnincomeLastYear_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblDashBoardSetting", con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                    if (i3 == 0)
                    {
                        SqlCommand cmd111t = new SqlCommand("insert into tblDashBoardSetting values('This Year','This Year','This Year')", con);
                        cmd111t.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set revenset='Last Year'", con);
                        cmdre.ExecuteNonQuery();
                    }
                    Response.Redirect("Home.aspx");
                }
            }
        }
        private void ShowPieAR()
        {
            if (one.InnerText == "0.00" && two.InnerText == "0.00" && three.InnerText == "0.00" && four.InnerText == "0.00")
            {
                main2.Visible = true;
            }
            else
            {
                main2.Visible = false;
                string a = "1-30 Days"; string b = "31-60 Days"; string c = "61-90 Days"; string d = "> 90 Days";
                string cm = ",";
                string apst = "\"";
                String chart = " ";
                chart += "<canvas id=\"line-charts\" width=\"100%\" height=\"280\"></canvas>";
                chart += "<script>";
                chart += "new Chart(document.getElementById(\"line-charts\"), { type: 'pie', data: {";
                chart += "labels: [" + apst + a + apst + cm + apst + b + apst + cm + apst + c + apst + cm + apst + d + apst + "]";

                chart += ",datasets: [{ data: [";

                String value = "";

                value += Convert.ToDouble(one.InnerText) + "," + Convert.ToDouble(two.InnerText) + "," + Convert.ToDouble(three.InnerText) + "," + Convert.ToDouble(four.InnerText) + ",";

                value = value.Substring(0, value.Length - 1);
                chart += value;
                chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                chart += "]},options: {elements: {center: {text: \'Red is 2/3 of the total numbers\',color: \'#FF6384\',fontStyle: \'Arial\'}},maintainAspectRatio: false,layout: {padding: { xPadding: 15, caretPadding: 10,borderWidth: 1,yPadding: 15}},scales: {xAxes: [{display: false,gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 25,}],},title:{display: false},legend: {display: true}}"; // Chart title

                chart += "});";

                chart += "</script>";

                ltrARPie.Text = chart;
            }
        }
        private void ShowBarAR()
        {
            if (one.InnerText == "0.00" && two.InnerText == "0.00" && three.InnerText == "0.00" && four.InnerText == "0.00")
            {
                main3.Visible = true;
            }
            else
            {
                main3.Visible = false;
                string a = "1-30 Days"; string b = "31-60 Days"; string c = "61-90 Days"; string d = "> 90 Days";
                string cm = ",";
                string apst = "\"";
                String chart = " ";
                chart += "<canvas id=\"line-chartsss\" width=\"100%\" height=\"280\"></canvas>";
                chart += "<script>";
                chart += "new Chart(document.getElementById(\"line-chartsss\"), { type: 'bar', data: {";
                chart += "labels: [" + apst + a + apst + cm + apst + b + apst + cm + apst + c + apst + cm + apst + d + apst + "]";

                chart += ",datasets: [{ data: [";

                String value = "";

                value += Convert.ToDouble(one.InnerText) + "," + Convert.ToDouble(two.InnerText) + "," + Convert.ToDouble(three.InnerText) + "," + Convert.ToDouble(four.InnerText) + ",";

                value = value.Substring(0, value.Length - 1);
                chart += value;
                chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                chart += "]},options: {elements: {center: {text: \'Red is 2/3 of the total numbers\',color: \'#FF6384\',fontStyle: \'Arial\'}},maintainAspectRatio: false,layout: {padding: { xPadding: 15, caretPadding: 10,borderWidth: 1,yPadding: 15}},scales: {xAxes: [{display: true,gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 25,}],},title:{display: false},legend: {display: false}}"; // Chart title

                chart += "});";

                chart += "</script>";

                ltrARBar.Text = chart;
            }
        }
        private string BindPDate()
        {
            string FirstDate = "";

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select TOP (1) *from tblGeneralLedger ORDER BY LedID ASC", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                        if (i != 0)
                        {
                            FirstDate = Convert.ToDateTime(dtBrands.Rows[0][6].ToString()).ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            return FirstDate;
        }
        private double CalculateOverallBalanceIncome()
        {
            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtCHDateFrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            str = "select Account from tblGeneralLedger where Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Income' group by Account";

            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP(1)* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }

            return Pbalance;
        }
        private double CalculateOverallBalanceExpense()
        {
            double Pbalance = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtCHDateFrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            str = "select Account from tblGeneralLedger where Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Expenses'";

            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP 1* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }

            return Pbalance;
        }
        private double BindbegAssetBalance()
        {

            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtCHDateFrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            string q11 = "select Account from tblGeneralLedger where Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Accounts Receivable'  OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "'  and AccountType='Cash' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Other Assets'  OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Inventory' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Other Current Assets' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Fixed Assets' group by Account";
            string str2r = q11;

            com = new SqlCommand(str2r, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP 1* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }
            return Pbalance;
        }
        private double BindbegLiabilityBalance()
        {

            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtCHDateFrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            string q11 = "select Account from tblGeneralLedger where Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Accumulated Depreciation' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Accounts Payable' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Other Current Liabilities'  OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Long Term Liabilities' group by Account";
            string str2r = q11;

            com = new SqlCommand(str2r, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP 1* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }
            return Pbalance;
        }
        private void networthcalculated()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();

            String query = "select (SUM(Debit)-SUM(Credit)) as Balance  from tblGeneralLedger where Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Accounts Receivable'  OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "'  and AccountType='Cash' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Other Assets'  OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Inventory' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Other Current Assets' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Fixed Assets'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string asset; double assetsssss;
                asset = reader["Balance"].ToString();
                if (asset == "" || asset == null)
                {
                    assetsssss = 0;
                }
                else
                {
                    assetsssss = Convert.ToDouble(asset) + BindbegAssetBalance();
                }
                reader.Close();
                con.Close();
                String query1 = "select (SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Accumulated Depreciation' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Accounts Payable' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Other Current Liabilities'  OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Long Term Liabilities'";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    string liability; double liabilityyyyy;
                    liability = reader1["Balance"].ToString();
                    if (liability == "" || liability == null)
                    {
                        liabilityyyyy = 0;
                    }
                    else
                    {
                        liabilityyyyy = Convert.ToDouble(liability) + BindbegLiabilityBalance();
                    }
                    reader1.Close();
                    con.Close();
                    Liability2.InnerText = liabilityyyyy.ToString("#,##0.00");
                    Asset2.InnerText = assetsssss.ToString("#,##0.00");

                    double nw = assetsssss - liabilityyyyy;
                    NetWorth.InnerText = nw.ToString("#,##0.00");
                    Span3.InnerText = nw.ToString("#,##0.00");
                }
            }
        }
        protected void btnBindBussinessSumary_Click(object sender, EventArgs e)
        {
            if (txtCHDateFrom.Text == "" || txtCHDateTo.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                datTo.Visible = true; datFrom1.Visible = true; tomiddle.Visible = true;

                datFrom1.InnerText = Convert.ToDateTime(txtCHDateFrom.Text).ToString("dd MMM yyyy"); datTo.InnerText = Convert.ToDateTime(txtCHDateTo.Text).ToString("dd MMM yyyy");
                SystemDate.Visible = false;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                //Calculating Net profit
                String query = "select (SUM(Credit)-SUM(Debit)) as Balance  from tblGeneralLedger where Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Income'";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string income; double incomesssssss; income = reader["Balance"].ToString();
                    if (income == null || income == "")
                    {
                        incomesssssss = CalculateOverallBalanceIncome();
                        Revenues.InnerText = incomesssssss.ToString("#,##0.00");
                    }
                    else
                    {
                        incomesssssss = Convert.ToDouble(income) + CalculateOverallBalanceIncome();
                        Revenues.InnerText = incomesssssss.ToString("#,##0.00");
                    }

                    reader.Close();
                    con.Close();
                    String query1 = "select (SUM(Debit)-SUM(Credit)) as Balance from tblGeneralLedger where Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Expenses' OR Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' and AccountType='Cost of Sales'";
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    if (reader1.Read())
                    {
                        string expense; double expensesssssss;
                        expense = reader1["Balance"].ToString();
                        if (expense == null || expense == "")
                        {
                            expensesssssss = CalculateOverallBalanceExpense();
                            Expenses1.InnerText = expensesssssss.ToString("#,##0.00");
                        }
                        else
                        {
                            expensesssssss = Convert.ToDouble(expense) + CalculateOverallBalanceExpense();
                            Expenses1.InnerText = expensesssssss.ToString("#,##0.00");
                        }
                        reader1.Close();
                        con.Close();

                        double nw = incomesssssss - expensesssssss;
                        if (nw < 0)
                        {
                            nw = -nw;
                            NetText1.InnerText = "Net Loss";

                            NetProfit.Attributes.Add("class", "font-weight-bold  text-uppercase text-danger");
                            NetProfit.InnerText = nw.ToString("#,##0.00");
                        }
                        else
                        {
                            NetText1.InnerText = "Net Profit";
                            NetProfit.Attributes.Add("class", "font-weight-bold text-gray-900  text-uppercase text-dark");
                            NetProfit.InnerText = nw.ToString("#,##0.00");
                        }
                    }
                }
                //Calculating Networth
                networthcalculated(); BalanceSheetDate.Visible = false; ShowUnpaidDoughnutIncome();
                //Calculating beg balance
            }
        }
        protected void btnBulkSMS_Click(object sender, EventArgs e)
        {
            if (txtBulkmessage.Text == "")
            {
                string message = "Please put your message to the textbox!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select*from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <= 15", con);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[j][6].ToString() != "")
                                {
                                    SqlCommand cmd41 = new SqlCommand("select * from tblCustomers where FllName='" + dt.Rows[j][2].ToString() + "'", con);
                                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                                    DataTable dt1 = new DataTable();
                                    sda1.Fill(dt1); int i1 = dt1.Rows.Count;
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
                                            body: txtBulkmessage.Text,
                                            from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                            to: new Twilio.Types.PhoneNumber("+251" + dt1.Rows[0][6].ToString())
                                        );
                                    }
                                    else
                                    {
                                        int d = -Convert.ToInt32(dayleft);
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
                    }
                }
            }
        }
        private void unpaidcounter()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <= 15 and status='Active'", con);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    if (i == 0)
                    {
                        UNPcounter.InnerText = "0";
                    }
                    else
                    {
                        UNPcounter.InnerText = i.ToString();
                    }

                }
            }
        }
        private void BindBalanceUnrecorded()
        {
            double balance = Convert.ToDouble(paidinv.InnerText) - Convert.ToDouble(unpaidinv.InnerText);
            double receivable = Convert.ToDouble(Span9.InnerText);
            double bal_left = balance - receivable;
            balance_left.InnerText = bal_left.ToString("#,##0.00");
            if (balance > receivable) { Imbalance_creditdiv.Visible = true; Receivablediv.Visible = true; }
        }
        private void agrremenetdateCounter()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblCustomers where DATEDIFF(day, '" + DateTime.Now.Date + "', agreementdate) < 0 and status='Active'", con);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    AGRDcounter.InnerText = i.ToString();
                }
            }
        }
        protected void btnBindCashSumary_Click(object sender, EventArgs e)
        {
            if (txtCHDateFromCash.Text == "" || txtCHDateToCash.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                datFromc.InnerText = Convert.ToDateTime(txtCHDateFromCash.Text).ToString("MMM dd, yyyy");
                datToc.InnerText = Convert.ToDateTime(txtCHDateToCash.Text).ToString("MMM dd, yyyy");
                datFromc.Visible = true; datToc.Visible = true; tomiddlec.Visible = true; cashcurrentyear.Visible = false;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("select SUM(Debit) Debit,SUM(Credit) Credit from tblGeneralLedger where AccountType='Cash' and Date between '" + txtCHDateFromCash.Text + "' and '" + txtCHDateToCash.Text + "' ", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string credit;
                        string debit;
                        debit = reader["Debit"].ToString();
                        credit = reader["Credit"].ToString();
                        Double C = 0; Double C1 = 0;
                        con.Close();
                        if (credit != "") { C1 += Math.Round(Convert.ToDouble(credit)); }
                        if (debit != "") { C += Math.Round(Convert.ToDouble(debit)); }
                        Double Total = C + C1; if (Total == 0) { Total = 1; }
                        Double C2 = Math.Round((C1 / Total) * 100);
                        Double R = Math.Round((C / Total) * 100); ;
                        Atr2.Style.Add("width", R.ToString() + "%");
                        Atr3.Style.Add("width", C2.ToString() + "%");
                        inflow.InnerText = C.ToString("#,##0.00");
                        outflow.InnerText = C1.ToString("#,##0.00");
                    }
                    reader.Close();
                }
            }
        }
        protected void btnBindInvSumary_Click(object sender, EventArgs e)
        {
            if (txtCHDateFrominv.Text == "" || txtCHDateToinv.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                datFromi.InnerText = Convert.ToDateTime(txtCHDateFrominv.Text).ToString("MMM dd, yyyy");
                datToi.InnerText = Convert.ToDateTime(txtCHDateToinv.Text).ToString("MMM dd, yyyy");
                datFromi.Visible = true; datToi.Visible = true; tomiddlei.Visible = true; billcurrentyear2.Visible = false;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();


                    SqlCommand cmd = new SqlCommand("select Sum(InvAmount) as Total,Sum(Payment) as PAID  from tblCustomerStatement where date between '" + txtCHDateFrominv.Text + "' and '" + txtCHDateToinv.Text + "'", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string ah7g; string paid;
                        ah7g = reader["Total"].ToString();
                        paid = reader["PAID"].ToString();
                        reader.Close();

                        if (ah7g != "")
                        {

                            paidinv.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                        }
                        if (paid != "")
                        {
                            unpaidinv.InnerText = Convert.ToDouble(paid).ToString("#,##0.00");
                        }
                        //Creating the progress bar
                        double paid1 = 0; double unpaid1 = 0;

                        if (paidinv.InnerText != "")
                        {
                            paid1 = Convert.ToDouble(paidinv.InnerText);
                        }
                        if (unpaidinv.InnerText != "")
                        {
                            unpaid1 = Convert.ToDouble(unpaidinv.InnerText);
                        }
                        Double C = Math.Round(paid1);
                        Double C1 = Math.Round(unpaid1);
                        Double pd = C - C1;

                        Double C2 = Math.Round((C1 / C) * 100);
                        Double R = 100 - C2;
                        unpaidbill.Style.Add("width", R.ToString() + "%");
                        paidbill.Style.Add("width", C2.ToString() + "%");
                    }
                }
            }
        }
    }
}