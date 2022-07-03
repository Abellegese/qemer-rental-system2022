using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class cashpay : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    bindqty(); bindoverpayment(); bindcredit();
                    BindNotesContent(); bindbankaccount();
                    BindBrandsRptr2(); ShowDataIncreaseMonthly(); bindsearch();
                    bindSC(); bindFSnumber(); bindDueDate();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindDueDate()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    string agreementdate = reader["duedate"].ToString();
                    DateTime today1 = DateTime.Now.Date;
                    DateTime duedate1 = Convert.ToDateTime(agreementdate);
                    TimeSpan t1 = duedate1 - today1; string dayleft1 = t1.TotalDays.ToString();
                    if (Convert.ToInt32(dayleft1) <= 15 || Convert.ToInt32(dayleft1) > 0)
                    {
                        duedate2.InnerText = "Payment " + dayleft1 + " " + "Days" + " Remains";
                        duedate2.Attributes.Add("class", "small text-success border-bottom");

                    }
                    if (Convert.ToInt32(dayleft1) < 0)
                    {
                        int d = -Convert.ToInt32(dayleft1);
                        duedate2.InnerText = "Payment " + d + " Days" + " Passed";
                        duedate2.Attributes.Add("class", "small  text-danger border-bottom");
                    }
                }
            }
        }
        private void bindFSnumber()
        {
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
                        txtFSNo.Text = "0000" + (Convert.ToInt64(FSNumber) + 1).ToString();
                    }
                }
            }
        }
        private void bindSC()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["cust"]);
                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {
                    double SC = 0;
                    string TNIN = readercrd["TIN"].ToString();
                    string address = readercrd["addresscust"].ToString();
                    if (TNIN == "" || TNIN == null)
                    {
                        TINNumber.InnerText = "No TIN# Added!";
                        txtTIN.Text = "";
                    }
                    else
                    {
                        tinSpan.Visible = false;
                        txtTIN.Text = TNIN;
                    }
                    if (address == "" || address == null)
                    {
                        txtCustAddress.Text = "";
                    }
                    else
                    {
                        txtCustAddress.Text = address;
                    }
                    string pp = readercrd["PaymentDuePeriod"].ToString();
                    string limit = readercrd["CreditLimit"].ToString();
                    readercrd.Close();
                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string servicecharge; servicecharge = reader["servicecharge"].ToString();

                        reader.Close();
                        if (pp == "Every Three Month") { SC = Convert.ToDouble(servicecharge) * 3; }
                        else if (pp == "Monthly") { SC = Convert.ToDouble(servicecharge) * 1; }
                        else { SC = Convert.ToDouble(servicecharge) * 12; }
                        ServieCharge.InnerText = SC.ToString("#,##0.00");
                    }
                }
            }
        }
        private void bindsearch()
        {
            if (Request.QueryString["search"] != null)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    String PID = Convert.ToString(Request.QueryString["search"]);
                    PID = PID.Substring(2);
                    SqlCommand cmd = new SqlCommand("select FllName from tblCustomers where FllName LIKE '%" + PID + "%'", con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            SqlDataReader reader22 = cmd.ExecuteReader();
                            if (reader22.Read())
                            {
                                string pstatus; pstatus = reader22["FllName"].ToString(); reader22.Close();

                                Response.Redirect("cashpay.aspx?cust=" + pstatus);
                            }
                        }
                        else
                        {

                            CCF.Visible = true; container.Visible = false;
                        }
                    }

                }
            }
        }
        private void ShowDataIncreaseMonthly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String query = "select sum(balance) as Total,DATENAME(MONTH,DATEADD(MONTH,month(Date),-1)) as month_name, month(Date) as month_number  from tblcreditnote where customer = '" + PID + "' and balance > 0  group by   month(Date) order by  month_number";
            SqlCommand cmd = new SqlCommand(query, con);
            using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
            {
                DataTable dtBrands232c3 = new DataTable();
                sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                if (i2c3 != 0)
                {
                    String chart = "";
                    chart = "<canvas id=\"line-chartin\" width=\"100%\" height=\"120\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"line-chartin\"), { type: 'bar', data: {labels: [";

                    // more detais

                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                        chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                    chart += "],datasets: [{ data: [";

                    // get data from database and add to chart
                    String value = "";
                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                        value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000).ToString("##0.00") + ",";
                    value = value.Substring(0, value.Length - 1);
                    chart += value;

                    chart += "],label: \"Credit Amount\",display: false,backgroundColor: [\"#a82dfc\",\"#a82dfc\",\"#a82dfc\",\"#a82dfc\", \"#a82dfc\",\"#a82dfc\",\"#a82dfc\",\"#a82dfc\",\"#a82dfc\",\"#a82dfc\",\"#a82dfc\",\"#a82dfc\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                    chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 12},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                    chart += "});";

                    chart += "</script>";

                    ltrIncreaseTrends.Text = chart; main2.Visible = false;
                }
                else
                {
                    main2.Visible = true; AnalyticsDiv.Visible = false;
                }
            }
        }
        private void BindBrandsRptr2()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            CustCreate.InnerText = PID;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblcreditnote where customer='" + PID + "' and balance > 0";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            con.Close();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("Label2") as Label;
                    Label lbl2 = item.FindControl("Label3") as Label;
                    Label lbl3 = item.FindControl("Label5") as Label;
                    Label lblExp = item.FindControl("lblExp") as Label;
                    Label lblcust = item.FindControl("lblCustomer") as Label;
                    Label lblID = item.FindControl("lblID") as Label;
                    Literal Literal = item.FindControl("Literal1") as Literal;
                    Literal LiteralDrop = item.FindControl("LiteralDropdown") as Literal;

                    Literal.Text = "<a class=\"text-primary\" id=\"link\" target=\"_blank\" runat =\"server\" href=\"creditnotedetails.aspx?ref2=" + lblID.Text + "&&cust=" + lblcust.Text + "\">CN#-" + lblID.Text + "</a>";


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

                }
            }
        }
        private readonly Random _random = new Random();
        public int RandomNumber(int min, int max)
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
        private void BindNotesContent()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmdcn = new SqlCommand("select*from tblNotes where customer='" + PID + "' and status='unseen'", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                if (nb != 0)
                {
                    btnClear.Visible = true;
                    Notes.InnerText = "\"" + dtcn.Rows[0][2].ToString() + "\"";
                }
                else
                {
                    Div1.Visible = true;
                }
            }
        }
        private void BindNotes()
        {
            if (txtNotes.Text != "")
            {
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd1974 = new SqlCommand("insert into tblNotes values('" + PID + "',N'" + txtNotes.Text + "','unseen','" + DateTime.Now + "')", con);
                    con.Open();
                    cmd1974.ExecuteNonQuery();
                }
            }
        }
        private void bindcredit()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                {
                    CashPay1.InnerText = "0.00";
                }
                else
                {
                    CashPay1.InnerText = Convert.ToDouble(dtcn.Rows[0][0].ToString()).ToString("#,##0.00");
                }
            }
        }
        private void bindoverpayment()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcn = new SqlCommand("select TOP 1 * from tblCustomerStatement where customer='" + PID + "'  ORDER BY CSID DESC", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                if (nb == 0)
                {

                }
                else
                {
                    if (Convert.ToDouble(dtcn.Rows[0][6].ToString()) < 0)
                    {

                        OverPayAmount.InnerText = Convert.ToDouble(-Convert.ToDouble(dtcn.Rows[0][6].ToString())).ToString("#,##0.00");
                        double expamount = Convert.ToDouble(txtqtyhand.Text) + Convert.ToDouble(dtcn.Rows[0][6].ToString()) + bindcredit1();
                        if (expamount < 0)
                        {
                            ExpecAmount.InnerText = "0.00";
                        }
                        else
                        {
                            ExpecAmount.InnerText = expamount.ToString("#,##0.00");
                        }

                    }
                    else
                    {
                        double expamount = Convert.ToDouble(txtqtyhand.Text) + bindcredit1();
                        ExpecAmount.InnerText = expamount.ToString("#,##0.00");
                        OverPayAmount.InnerText = "0.00";
                    }
                }
            }
        }
        private void bindqty()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    string kc;
                    kc = reader["currentperiodue"].ToString();
                    txtqtyhand.Text = Convert.ToDouble(kc).ToString("#,##.00");
                    Span1.InnerText = Convert.ToDouble(kc).ToString("#,##.00");
                }
            }
        }
        private void BindCashPay()
        {
            if (TINNumber.InnerText == "No TIN# Added!" || TINNumber.InnerText == null)
            {
                string message = "Please provide the TIN number for the customer. To provide TIN, click \" No TIN# Added \"  text, that found beside customer name.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else if (txtCustAddress.Text == "" || txtCustAddress.Text == null)
            {
                string message = "Please provide customer address so that its address included in the invoice. Click TIN Number Link";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else if (txtFSNo.Text == "")
            {
                string message = "Please provide FS number to continue";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else if (txtCustAddress.Text == "" && TINNumber.InnerText == "No TIN# Added!")
            {
                string message = "Please provide customer address and TIN number so that their avalues included in the invoice";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                if (txtqtyhand.Text == "" || txtReference.Text == "")
                {
                    lblMsg.Text = "Fill the required Input"; lblMsg.Visible = true; lblMsg.ForeColor = Color.Red;
                }
                else
                {

                    String PID = Convert.ToString(Request.QueryString["cust"]);
                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmdset = new SqlCommand("select * from tblsetting", con);
                        SqlDataReader readerset = cmdset.ExecuteReader();
                        if (readerset.Read())
                        {
                            string set = readerset["climit"].ToString();
                            readerset.Close();
                            if (set == "Yes")
                            {
                                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                                if (readercrd.Read())
                                {
                                    double SC = 0;
                                    string pp = readercrd["PaymentDuePeriod"].ToString();
                                    string limit = readercrd["CreditLimit"].ToString();
                                    readercrd.Close();

                                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
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
                                        string totalpay1 = Convert.ToDouble(kc).ToString("#,##0.00");
                                        double totalpay = Convert.ToDouble(totalpay1);
                                        reader.Close();
                                        double balance = Convert.ToDouble(txtqtyhand.Text) - totalpay;

                                        double due = Convert.ToDouble(kc);
                                        double climit = Convert.ToDouble(limit);
                                        if (-balance > climit && Convert.ToDouble(CashPay1.InnerText) > 0)
                                        {

                                            double ex = -balance;
                                            lblMsg.Text = "Credit Limit Exceeded By " + ex.ToString("#,##0.00"); lblMsg.ForeColor = Color.Red;
                                            lblMsg.Visible = true; infoicon.Visible = true;

                                        }
                                        else
                                        {
                                            BindNotes();
                                            if (balance == 0)
                                            {
                                                //selecting from "+ddlCashorBank.SelectedItem.Text+"

                                                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                {
                                                    DataTable dtBrands2322 = new DataTable();
                                                    sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                            double deb = Convert.ToDouble(txtqtyhand.Text);
                                                            Double M1 = Convert.ToDouble(ah12893);
                                                            Double bl1 = M1 + deb;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                            cmd1974.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                //Selecting from account prefernce
                                                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                {
                                                    DataTable dtBrandss = new DataTable();
                                                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                    //Selecting from Income account
                                                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                    using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                    {
                                                        DataTable dtBrandss2 = new DataTable();
                                                        sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                    double income = (totalpay - SC) - vatfree;
                                                                    Double bl1 = M1 + income;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                    cmd1974.ExecuteNonQuery();

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //Inserting to customer statement
                                                SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                if (readerbcustb.Read())
                                                {
                                                    string ah11;

                                                    ah11 = readerbcustb["Balance"].ToString();
                                                    readerbcustb.Close();

                                                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + ah11 + "','" + PID + "')", con);
                                                    custcmd.ExecuteNonQuery();
                                                    SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                    if (readerAC.Read())
                                                    {
                                                        String FN = readerAC["Name"].ToString();
                                                        readerAC.Close();
                                                        con.Close();
                                                        //Activity
                                                        con.Open();
                                                        string money = "ETB";


                                                        //Updating the Due Date
                                                        SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                        SqlDataReader readerup = cmdup.ExecuteReader();

                                                        if (readerup.Read())
                                                        {
                                                            String terms = readerup["PaymentDuePeriod"].ToString();
                                                            readerup.Close();
                                                            if (terms == "Monthly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(30);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Three Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(90);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Six Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(180);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(365);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            //Insert into cash receipt journal
                                                            double payment = Convert.ToDouble(txtqtyhand.Text);
                                                            double credit = due - payment;
                                                            double balancedue = Convert.ToDouble(ah11);
                                                            double unpaid = -balance;
                                                            double remain = balancedue + unpaid;

                                                            SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                            SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                            DataTable dtdf = new DataTable();
                                                            sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                                            double vatfree = due - SC;
                                                            SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','" + ah11 + "','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                                            cmdri.ExecuteNonQuery();
                                                            string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";
                                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                                            cmd197h.ExecuteNonQuery();
                                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                                            cmdAc.ExecuteNonQuery();
                                                            Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash");
                                                        }
                                                    }
                                                }
                                            }
                                            else if (balance < 0)
                                            {

                                                //selecting from Accounts Receivable
                                                double newclimit = climit + balance;
                                                SqlCommand cmdclim = new SqlCommand("Update tblCustomers set CreditLimit='" + newclimit + "' where FllName='" + PID + "'", con);
                                                cmdclim.ExecuteNonQuery();
                                                if (Checkbox2.Checked == true)
                                                {
                                                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
                                                    using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                    {
                                                        DataTable dtBrands2322 = new DataTable();
                                                        sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                                double unpaid = -balance;
                                                                Double bl1 = M1 + unpaid; ;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + unpaid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                                cmd1974.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                }
                                                SqlCommand cmdacr = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmdacr))
                                                {
                                                    DataTable dtBrands2322 = new DataTable();
                                                    sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
                                                    //
                                                    if (i2 != 0)
                                                    {
                                                        SqlDataReader reader6679034 = cmdacr.ExecuteReader();

                                                        if (reader6679034.Read())
                                                        {
                                                            string ah12893;
                                                            ah12893 = reader6679034["Balance"].ToString();
                                                            reader6679034.Close();
                                                            con.Close();
                                                            con.Open();
                                                            double deb = Convert.ToDouble(txtqtyhand.Text);
                                                            Double M1 = Convert.ToDouble(ah12893);
                                                            Double bl1 = M1 + deb;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                            cmd1974.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                //Selecting from account prefernce
                                                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                {
                                                    DataTable dtBrandss = new DataTable();
                                                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                    //Selecting from Income account
                                                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                    using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                    {
                                                        DataTable dtBrandss2 = new DataTable();
                                                        sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                    double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                    income = income + SC;
                                                                    Double bl1 = income + M1;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                    double vatfree = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                    double vat = (Convert.ToDouble(txtqtyhand.Text) - SC) - vatfree;
                                                                    Double bl1 = M1 + vat;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                    cmd1974.ExecuteNonQuery();

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //Inserting to customer statement
                                                SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                if (readerbcustb.Read())
                                                {
                                                    string ah11;

                                                    ah11 = readerbcustb["Balance"].ToString();
                                                    readerbcustb.Close();

                                                    double payment = Convert.ToDouble(txtqtyhand.Text);
                                                    double credit = due - payment;
                                                    double balancedue = Convert.ToDouble(ah11);
                                                    double unpaid = -balance;
                                                    double remain = balancedue + unpaid;
                                                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + remain + "','" + PID + "')", con);
                                                    custcmd.ExecuteNonQuery();
                                                    SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                    if (readerAC.Read())
                                                    {
                                                        String FN = readerAC["Name"].ToString();
                                                        readerAC.Close();
                                                        con.Close();
                                                        //Activity
                                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                        con.Open();
                                                        cmdAc.ExecuteNonQuery();
                                                        string money = "ETB";

                                                        //Updating the Due Date
                                                        SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                        SqlDataReader readerup = cmdup.ExecuteReader();

                                                        if (readerup.Read())
                                                        {
                                                            String terms = readerup["PaymentDuePeriod"].ToString();
                                                            readerup.Close();
                                                            if (terms == "Monthly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(30);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Three Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(90);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Six Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(180);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(365);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            double cash = due - credit - SC;
                                                            if (Checkbox2.Checked == true)
                                                            {
                                                                SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + due + "','" + -balance + "','Credit for rent','" + DateTime.Now + "','" + txtReference.Text + "')", con);
                                                                cmdcrn.ExecuteNonQuery();
                                                                ///Credit Url Extraction
                                                                SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                                                SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                                                DataTable dtcni = new DataTable();
                                                                sdacni.Fill(dtcni); Int64 nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString());
                                                                ///
                                                                string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID;
                                                                SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(credit).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                                                cmdcn.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(credit).ToString("#,##0.00") + "'+' '+'was not recognized as credit','" + FN + "','" + PID + "','Unseen','fas fa-exclamation-circle text-white','icon-circle bg bg-danger','#','MN')", con);
                                                                cmdcn.ExecuteNonQuery();
                                                            }
                                                            SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                            SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                            DataTable dtdf = new DataTable();
                                                            sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                                            SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + cash + "','" + ah11 + "','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                                            cmdri.ExecuteNonQuery();
                                                            string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";

                                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                            cmd197h.ExecuteNonQuery();
                                                            SqlCommand cmdfh = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','FH')", con);
                                                            cmdfh.ExecuteNonQuery();

                                                            Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash");

                                                        }
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                                                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                                                DataTable dtcn = new DataTable();
                                                sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                                                if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                                                {

                                                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                    using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                    {
                                                        DataTable dtBrands2322 = new DataTable();
                                                        sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                                double deb = Convert.ToDouble(txtqtyhand.Text);
                                                                Double M1 = Convert.ToDouble(ah12893);
                                                                Double bl1 = M1 + deb;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                                cmd1974.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                    //Selecting from account prefernce
                                                    SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                    using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                    {
                                                        DataTable dtBrandss = new DataTable();
                                                        sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                        //Selecting from Income account
                                                        SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                        using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                        {
                                                            DataTable dtBrandss2 = new DataTable();
                                                            sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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

                                                                        double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                        income = income + SC;
                                                                        Double bl1 = income + M1;
                                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                        cmd45.ExecuteNonQuery();
                                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                            sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                    SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                        double vatfree = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                        double vat = (Convert.ToDouble(txtqtyhand.Text) - SC) - vatfree;
                                                                        Double bl1 = M1 + vat;
                                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + vat + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                        cmd45.ExecuteNonQuery();
                                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                        cmd1974.ExecuteNonQuery();

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //Inserting to customer statement
                                                    SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                    if (readerbcustb.Read())
                                                    {
                                                        string ah11;

                                                        ah11 = readerbcustb["Balance"].ToString();
                                                        readerbcustb.Close();
                                                        double balcust = totalpay - Convert.ToDouble(txtqtyhand.Text);
                                                        double statbalance = Convert.ToDouble(ah11);
                                                        double finalbalance = balcust + statbalance;
                                                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + finalbalance + "','" + PID + "')", con);
                                                        custcmd.ExecuteNonQuery();
                                                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                        if (readerAC.Read())
                                                        {
                                                            String FN = readerAC["Name"].ToString();
                                                            readerAC.Close();
                                                            con.Close();
                                                            //Activity
                                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                            con.Open();
                                                            cmdAc.ExecuteNonQuery();
                                                            string money = "ETB";


                                                            //Updating the Due Date
                                                            SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                            SqlDataReader readerup = cmdup.ExecuteReader();

                                                            if (readerup.Read())
                                                            {
                                                                String terms = readerup["PaymentDuePeriod"].ToString();
                                                                readerup.Close();
                                                                if (terms == "Monthly")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(30);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else if (terms == "Every Three Month")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(90);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else if (terms == "Every Six Month")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(180);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(365);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                //Insert into cash receipt journal
                                                                double vatfree = due - 0.15 * due;
                                                                SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                                DataTable dtdf = new DataTable();
                                                                sdadf.Fill(dtdf); int nb1 = dtdf.Rows.Count + 1;
                                                                double amount1 = Convert.ToDouble(txtqtyhand.Text) - SC;
                                                                SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + amount1 + "','" + ah11 + "','" + nb1 + "','" + txtFSNo.Text + "','Cash')", con);
                                                                cmdri.ExecuteNonQuery();
                                                                string url = "rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Cash";
                                                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                                cmd197h.ExecuteNonQuery();
                                                                Response.Redirect("rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Cash");
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Tast.Visible = false;
                                                    lblMsg.Text = "ደንበኛው የቀደመ እዳ በመጠን " + Convert.ToDouble(dtcn.Rows[0][0].ToString()).ToString("#,##0.00") + " ብር ስላለበት እላፊ ብር " + balance.ToString("#,##0.00") + " ለእዳ ይከፈል፡፡";
                                                    lblMsg.ForeColor = Color.Red; lblMsg.Visible = true;
                                                }

                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {
                                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                                if (readercrd.Read())
                                {
                                    double SC = 0;
                                    string pp = readercrd["PaymentDuePeriod"].ToString();
                                    readercrd.Close();
                                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                                    SqlDataReader reader = cmd2.ExecuteReader();

                                    if (reader.Read())
                                    {
                                        string kc; string servicecharge; string duedates = reader["duedate"].ToString();
                                        kc = reader["currentperiodue"].ToString(); servicecharge = reader["servicecharge"].ToString();
                                        if (pp == "Every Three Month") { SC = Convert.ToDouble(servicecharge) * 3; }
                                        else if (pp == "Every Six Month") { SC = Convert.ToDouble(servicecharge) * 6; }
                                        else if (pp == "Monthly") { SC = Convert.ToDouble(servicecharge) * 1; }
                                        else { SC = Convert.ToDouble(servicecharge) * 12; }
                                        string totalpay1 = Convert.ToDouble(kc).ToString("#,##0.00");
                                        double totalpay = Convert.ToDouble(kc);
                                        reader.Close();
                                        double balance = Convert.ToDouble(txtqtyhand.Text) - Convert.ToDouble(totalpay1);
                                        double due = Convert.ToDouble(kc);
                                        BindNotes();
                                        if (balance == 0)
                                        {
                                            //selecting from Cash at BANK
                                            SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                            using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                            {
                                                DataTable dtBrands2322 = new DataTable();
                                                sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                        double deb = Convert.ToDouble(txtqtyhand.Text);
                                                        Double M1 = Convert.ToDouble(ah12893);
                                                        Double bl1 = M1 + deb;
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                        cmd1974.ExecuteNonQuery();
                                                    }
                                                }
                                            }
                                            //Selecting from account prefernce
                                            SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                            using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                            {
                                                DataTable dtBrandss = new DataTable();
                                                sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                //Selecting from Income account
                                                SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                {
                                                    DataTable dtBrandss2 = new DataTable();
                                                    sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                    sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                            SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + vat + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                cmd1974.ExecuteNonQuery();

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            //Inserting to customer statement
                                            SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                            SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                            if (readerbcustb.Read())
                                            {
                                                string ah11;

                                                ah11 = readerbcustb["Balance"].ToString();
                                                readerbcustb.Close();

                                                SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + ah11 + "','" + PID + "')", con);
                                                custcmd.ExecuteNonQuery();
                                                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                if (readerAC.Read())
                                                {
                                                    String FN = readerAC["Name"].ToString();
                                                    readerAC.Close();
                                                    con.Close();
                                                    //Activity
                                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                    con.Open();
                                                    cmdAc.ExecuteNonQuery();
                                                    string money = "ETB";


                                                    //Updating the Due Date
                                                    SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                    SqlDataReader readerup = cmdup.ExecuteReader();

                                                    if (readerup.Read())
                                                    {
                                                        String terms = readerup["PaymentDuePeriod"].ToString();
                                                        readerup.Close();
                                                        if (terms == "Monthly")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(30);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        else if (terms == "Every Three Month")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(90);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        else if (terms == "Every Six Month")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(180);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(365);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        //Insert into cash receipt journal
                                                        double vatfree = due - SC;
                                                        SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                        SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                        DataTable dtdf = new DataTable();
                                                        sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','" + ah11 + "','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                                        cmdri.ExecuteNonQuery();
                                                        string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";
                                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                        cmd197h.ExecuteNonQuery();
                                                        Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash");
                                                    }
                                                }
                                            }
                                        }
                                        else if (balance < 0)
                                        {

                                            //selecting from Accounts Receivable
                                            if (Checkbox2.Checked == true)
                                            {
                                                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
                                                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                {
                                                    DataTable dtBrands2322 = new DataTable();
                                                    sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                            double unpaid = -balance;
                                                            Double bl1 = M1 + unpaid; ;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + unpaid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                            cmd1974.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                            }
                                            SqlCommand cmdacr = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                            using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmdacr))
                                            {
                                                DataTable dtBrands2322 = new DataTable();
                                                sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
                                                //
                                                if (i2 != 0)
                                                {
                                                    SqlDataReader reader6679034 = cmdacr.ExecuteReader();

                                                    if (reader6679034.Read())
                                                    {
                                                        string ah12893;
                                                        ah12893 = reader6679034["Balance"].ToString();
                                                        reader6679034.Close();
                                                        con.Close();
                                                        con.Open();
                                                        double deb = Convert.ToDouble(txtqtyhand.Text);
                                                        Double M1 = Convert.ToDouble(ah12893);
                                                        Double bl1 = M1 + deb;
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                        cmd1974.ExecuteNonQuery();
                                                    }
                                                }
                                            }
                                            //Selecting from account prefernce
                                            SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                            using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                            {
                                                DataTable dtBrandss = new DataTable();
                                                sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                //Selecting from Income account
                                                SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                {
                                                    DataTable dtBrandss2 = new DataTable();
                                                    sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                income = income + SC;
                                                                Double bl1 = income + M1;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                    sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                            SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                double vatfree = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                double vat = (Convert.ToDouble(txtqtyhand.Text) - SC) - vatfree;
                                                                Double bl1 = M1 + vat;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                cmd1974.ExecuteNonQuery();

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            //Inserting to customer statement
                                            SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                            SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                            if (readerbcustb.Read())
                                            {
                                                string ah11;

                                                ah11 = readerbcustb["Balance"].ToString();
                                                readerbcustb.Close();

                                                double payment = Convert.ToDouble(txtqtyhand.Text);
                                                double credit = due - payment;
                                                double balancedue = Convert.ToDouble(ah11);
                                                double unpaid = -balance;
                                                double remain = balancedue + unpaid;
                                                SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + remain + "','" + PID + "')", con);
                                                custcmd.ExecuteNonQuery();
                                                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                if (readerAC.Read())
                                                {
                                                    String FN = readerAC["Name"].ToString();
                                                    readerAC.Close();
                                                    con.Close();
                                                    //Activity
                                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                    con.Open();
                                                    cmdAc.ExecuteNonQuery();
                                                    string money = "ETB";


                                                    //Updating the Due Date
                                                    SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                    SqlDataReader readerup = cmdup.ExecuteReader();

                                                    if (readerup.Read())
                                                    {
                                                        String terms = readerup["PaymentDuePeriod"].ToString();
                                                        readerup.Close();
                                                        if (terms == "Monthly")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(30);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        else if (terms == "Every Three Month")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(90);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        else if (terms == "Every Six Month")
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(180);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            DateTime duedate = Convert.ToDateTime(duedates);
                                                            DateTime newduedate = duedate.AddDays(365);
                                                            SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                            cmdrent.ExecuteNonQuery();
                                                        }
                                                        double cash = due - credit - SC;
                                                        double cashvat = cash + cash * 0.15;
                                                        if (Checkbox2.Checked == true)
                                                        {
                                                            SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + due + "','" + -balance + "','Credit for rent','" + DateTime.Now + "','" + txtReference.Text + "')", con);
                                                            cmdcrn.ExecuteNonQuery();
                                                            ///Credit Url Extraction
                                                            SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                                            SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                                            DataTable dtcni = new DataTable();
                                                            sdacni.Fill(dtcni); Int64 nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString());
                                                            ///
                                                            string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID;
                                                            SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(credit).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                                            cmdcn.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(credit).ToString("#,##0.00") + "'+' '+'was not recognized as credit','" + FN + "','" + PID + "','Unseen','fas fa-exclamation-circle text-white','icon-circle bg bg-danger','#','MN')", con);
                                                            cmdcn.ExecuteNonQuery();
                                                        }
                                                        SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                        SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                        DataTable dtdf = new DataTable();
                                                        sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                                        string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";
                                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + cash + "','" + ah11 + "','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                                        cmdri.ExecuteNonQuery();
                                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                        cmd197h.ExecuteNonQuery();
                                                        Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash");

                                                    }
                                                }
                                            }

                                        }
                                        else
                                        {
                                            SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                                            SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                                            DataTable dtcn = new DataTable();
                                            sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                                            if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                                            {
                                                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                {
                                                    DataTable dtBrands2322 = new DataTable();
                                                    sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                            double deb = Convert.ToDouble(txtqtyhand.Text);
                                                            Double M1 = Convert.ToDouble(ah12893);
                                                            Double bl1 = M1 + deb;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                            cmd1974.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                //Selecting from account prefernce
                                                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                {
                                                    DataTable dtBrandss = new DataTable();
                                                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                    //Selecting from Income account
                                                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                    using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                    {
                                                        DataTable dtBrandss2 = new DataTable();
                                                        sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                    double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                    income = income + SC;
                                                                    Double bl1 = income + M1;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                    double vatfree = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                    double vat = (Convert.ToDouble(txtqtyhand.Text) - SC) - vatfree;
                                                                    Double bl1 = M1 + vat;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                    cmd1974.ExecuteNonQuery();

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //Inserting to customer statement
                                                SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                if (readerbcustb.Read())
                                                {
                                                    string ah11;

                                                    ah11 = readerbcustb["Balance"].ToString();
                                                    readerbcustb.Close();
                                                    double balcust = totalpay - Convert.ToDouble(txtqtyhand.Text);
                                                    double statbalance = Convert.ToDouble(ah11);
                                                    double finalbalance = balcust + statbalance;
                                                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + finalbalance + "','" + PID + "')", con);
                                                    custcmd.ExecuteNonQuery();
                                                    SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                    if (readerAC.Read())
                                                    {
                                                        String FN = readerAC["Name"].ToString();
                                                        readerAC.Close();
                                                        con.Close();
                                                        //Activity
                                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                        con.Open();
                                                        cmdAc.ExecuteNonQuery();
                                                        string money = "ETB";


                                                        //Updating the Due Date
                                                        SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                        SqlDataReader readerup = cmdup.ExecuteReader();

                                                        if (readerup.Read())
                                                        {
                                                            String terms = readerup["PaymentDuePeriod"].ToString();
                                                            readerup.Close();
                                                            if (terms == "Monthly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(30);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Three Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(90);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Six Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(180);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(365);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            //Insert into cash receipt journal
                                                            double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                                            SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                            SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                            DataTable dtdf = new DataTable();
                                                            sdadf.Fill(dtdf); int nb1 = dtdf.Rows.Count + 1;
                                                            string url = "rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Cash";
                                                            SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','" + ah11 + "','" + nb1 + "','" + txtFSNo.Text + "','Cash')", con);
                                                            cmdri.ExecuteNonQuery();
                                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                            cmd197h.ExecuteNonQuery();
                                                            Response.Redirect("rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Cash");
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Tast.Visible = false;
                                                lblMsg.Text = "ደንበኛው የቀደመ እዳ በመጠን " + Convert.ToDouble(dtcn.Rows[0][0].ToString()).ToString("#,##0.00") + " ብር ስላለበት እላፊ ብር " + balance.ToString("#,##0.00") + " ለእዳ ይከፈል፡፡";
                                                lblMsg.ForeColor = Color.Red; lblMsg.Visible = true;
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
        private void BindBankPay()
        {
            if (TINNumber.InnerText == "No TIN# Added!" || TINNumber.InnerText == null)
            {
                string message = "Please provide the TIN number for the customer. To provide TIN, click \" No TIN# Added \"  text, that found beside customer name.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else if (txtCustAddress.Text == "" || txtCustAddress.Text == null)
            {
                string message = "Please provide customer address so that its address included in the invoice. Click TIN Number Link";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else if (txtFSNo.Text == "")
            {
                string message = "Please provide FS number to continue";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else if (txtCustAddress.Text == "" && TINNumber.InnerText == "No TIN# Added!")
            {
                string message = "Please provide customer address and TIN number so that their avalues included in the invoice";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                if (txtqtyhand.Text == "")
                {
                    lblMsg.Text = "Fill the required Input"; lblMsg.Visible = true; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    String PID = Convert.ToString(Request.QueryString["cust"]);
                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmdset = new SqlCommand("select * from tblsetting", con);
                        SqlDataReader readerset = cmdset.ExecuteReader();
                        if (readerset.Read())
                        {
                            string set = readerset["climit"].ToString();
                            readerset.Close();
                            if (set == "Yes")
                            {
                                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                                if (readercrd.Read())
                                {

                                    double SC = 0;
                                    string pp = readercrd["PaymentDuePeriod"].ToString();
                                    string limit = readercrd["CreditLimit"].ToString();
                                    readercrd.Close();
                                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
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
                                        string totalpay1 = Convert.ToDouble(kc).ToString("#,##0.00");
                                        double totalpay = Convert.ToDouble(kc);
                                        reader.Close();
                                        double balance = Convert.ToDouble(txtqtyhand.Text) - Convert.ToDouble(totalpay1);
                                        double due = Convert.ToDouble(txtqtyhand.Text);
                                        double climit = Convert.ToDouble(limit);
                                        if (-balance > climit && Convert.ToDouble(CashPay1.InnerText) > 0)
                                        {
                                            double ex = -balance;
                                            lblMsg.Text = "Credit Limit Exceeded By " + ex.ToString("#,##0.00"); lblMsg.ForeColor = Color.Red;
                                            lblMsg.Visible = true; infoicon.Visible = true;
                                        }
                                        else
                                        {
                                            if (balance == 0)
                                            {

                                                SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                                SqlDataReader readerbank = cmdbank.ExecuteReader();

                                                if (readerbank.Read())
                                                {
                                                    string bankno;
                                                    bankno = readerbank["AccountNumber"].ToString();
                                                    readerbank.Close();
                                                    SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                    using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                                    {
                                                        string refe = Convert.ToString(txtReference.Text);
                                                        string totalannounc = PID + " Paid through bank with ref# " + refe;
                                                        DataTable dt1 = new DataTable();
                                                        sda221.Fill(dt1); int j = dt1.Rows.Count;
                                                        //
                                                        if (j != 0)
                                                        {
                                                            double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                            SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                            cmdday.ExecuteNonQuery();

                                                            SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                            cvb.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            double t = Convert.ToDouble(txtqtyhand.Text);
                                                            SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                            cvb1.ExecuteNonQuery();
                                                            SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                            b.ExecuteNonQuery();

                                                        }
                                                    }
                                                    //selecting from "+ddlCashorBank.SelectedItem.Text+"
                                                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                    using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                    {
                                                        DataTable dtBrands2322 = new DataTable();
                                                        sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                                double deb = Convert.ToDouble(txtqtyhand.Text);
                                                                Double M1 = Convert.ToDouble(ah12893);
                                                                Double bl1 = M1 + deb;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                                cmd1974.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                    //Selecting from account prefernce
                                                    SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                    using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                    {
                                                        DataTable dtBrandss = new DataTable();
                                                        sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                        //Selecting from Income account
                                                        SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                        using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                        {
                                                            DataTable dtBrandss2 = new DataTable();
                                                            sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                        cmd45.ExecuteNonQuery();
                                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                            sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                    SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                        double income = (totalpay - SC) - vatfree;
                                                                        Double bl1 = M1 + income;
                                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                        cmd45.ExecuteNonQuery();
                                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                        cmd1974.ExecuteNonQuery();

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //Inserting to customer statement
                                                    SqlCommand cmdreadb = new SqlCommand("select TOP 1* from tblCustomerStatement where Customer='" + PID + "'  ORDER BY CSID DESC", con);

                                                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                    if (readerbcustb.Read())
                                                    {
                                                        string ah11;

                                                        ah11 = readerbcustb["Balance"].ToString();
                                                        readerbcustb.Close();

                                                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtVoucher.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + ah11 + "','" + PID + "')", con);
                                                        custcmd.ExecuteNonQuery();
                                                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                        if (readerAC.Read())
                                                        {
                                                            String FN = readerAC["Name"].ToString();
                                                            readerAC.Close();
                                                            con.Close();
                                                            //Activity
                                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                            con.Open();
                                                            cmdAc.ExecuteNonQuery();
                                                            string money = "ETB";

                                                            //Updating the Due Date
                                                            SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                            SqlDataReader readerup = cmdup.ExecuteReader();

                                                            if (readerup.Read())
                                                            {
                                                                String terms = readerup["PaymentDuePeriod"].ToString();
                                                                readerup.Close();
                                                                if (terms == "Monthly")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(30);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else if (terms == "Every Three Month")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(90);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else if (terms == "Every Six Month")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(180);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(365);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                //Generating invoice if checked
                                                                if (CheckGene.Checked == true)
                                                                {
                                                                    SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                                    SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                                    DataTable dtdf = new DataTable();
                                                                    sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                                                    double vatfree = due - SC;
                                                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','" + ah11 + "','" + nb + "','" + txtFSNo.Text + "','Bank')", con);
                                                                    cmdri.ExecuteNonQuery();
                                                                    string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank";
                                                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                                    cmd197h.ExecuteNonQuery();
                                                                    SqlCommand cmd197h2 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','FH')", con);
                                                                    cmd197h2.ExecuteNonQuery();
                                                                    SqlCommand cmdAc1 = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                                                    cmdAc1.ExecuteNonQuery();
                                                                    Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (Request.QueryString["bill"] != null)
                                                    {
                                                        SqlCommand cmdupbill = new SqlCommand("Update tblcustomerbill set status='Billed' where customer='" + PID + "'", con);
                                                        cmdupbill.ExecuteNonQuery();
                                                        SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Your bill request has been approved','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                                                        cmd197hb.ExecuteNonQuery();
                                                    }
                                                }
                                                Response.Redirect("bankstatment.aspx?ref2=" + DropDownList1.SelectedItem.Text);
                                            }
                                            else if (balance < 0)
                                            {
                                                double newclimit = climit + balance;
                                                SqlCommand cmdclim = new SqlCommand("Update tblCustomers set CreditLimit='" + newclimit + "' where FllName='" + PID + "'", con);
                                                cmdclim.ExecuteNonQuery();
                                                SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                                SqlDataReader readerbank = cmdbank.ExecuteReader();

                                                if (readerbank.Read())
                                                {
                                                    string bankno;
                                                    bankno = readerbank["AccountNumber"].ToString();
                                                    readerbank.Close();
                                                    string refe = Convert.ToString(txtReference.Text);
                                                    string totalannounc = PID + " Paid through bank with ref# " + refe;
                                                    SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                    using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                                    {
                                                        DataTable dt1 = new DataTable();
                                                        sda221.Fill(dt1); int j = dt1.Rows.Count;
                                                        //
                                                        if (j != 0)
                                                        {
                                                            double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                            SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                            cmdday.ExecuteNonQuery();
                                                            SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                            cvb.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            double t = Convert.ToDouble(txtqtyhand.Text);
                                                            SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                                            cvb1.ExecuteNonQuery();
                                                            SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                                            b.ExecuteNonQuery();

                                                        }
                                                    }
                                                    //selecting from Accounts Receivable
                                                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
                                                    using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                    {
                                                        DataTable dtBrands2322 = new DataTable();
                                                        sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                                double unpaid = -balance;
                                                                Double bl1 = M1 + unpaid; ;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + unpaid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                                cmd1974.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                    SqlCommand cmdacr = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                    using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmdacr))
                                                    {
                                                        DataTable dtBrands2322 = new DataTable();
                                                        sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
                                                        //
                                                        if (i2 != 0)
                                                        {
                                                            SqlDataReader reader6679034 = cmdacr.ExecuteReader();

                                                            if (reader6679034.Read())
                                                            {
                                                                string ah12893;
                                                                ah12893 = reader6679034["Balance"].ToString();
                                                                reader6679034.Close();
                                                                con.Close();
                                                                con.Open();
                                                                Double M1 = Convert.ToDouble(ah12893);
                                                                Double bl1 = M1 + Convert.ToDouble(txtqtyhand.Text); ;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + txtqtyhand.Text + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                                cmd1974.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                    //Selecting from account prefernce
                                                    SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                    using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                    {
                                                        DataTable dtBrandss = new DataTable();
                                                        sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                        //Selecting from Income account
                                                        SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                        using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                        {
                                                            DataTable dtBrandss2 = new DataTable();
                                                            sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                        double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                        income = income + SC;
                                                                        Double bl1 = income + M1;
                                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                        cmd45.ExecuteNonQuery();
                                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                            sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                    SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                        double vatfree = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                        double income = (Convert.ToDouble(txtqtyhand.Text) - SC) - vatfree;
                                                                        Double bl1 = M1 + income;
                                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                        cmd45.ExecuteNonQuery();
                                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                        cmd1974.ExecuteNonQuery();

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //Inserting to customer statement
                                                    SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                    if (readerbcustb.Read())
                                                    {
                                                        string ah11;

                                                        ah11 = readerbcustb["Balance"].ToString();
                                                        readerbcustb.Close();
                                                        double payment = Convert.ToDouble(txtqtyhand.Text);
                                                        double credit = due - payment;
                                                        double balancedue = Convert.ToDouble(ah11);
                                                        double unpaid = -balance;
                                                        double remain = balancedue + unpaid;
                                                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtVoucher.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + remain + "','" + PID + "')", con);
                                                        custcmd.ExecuteNonQuery();
                                                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                        if (readerAC.Read())
                                                        {
                                                            String FN = readerAC["Name"].ToString();
                                                            readerAC.Close();
                                                            con.Close();
                                                            //Activity
                                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                            con.Open();
                                                            cmdAc.ExecuteNonQuery();
                                                            string money = "ETB";

                                                            //Updating the Due Date
                                                            SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                            SqlDataReader readerup = cmdup.ExecuteReader();

                                                            if (readerup.Read())
                                                            {
                                                                String terms = readerup["PaymentDuePeriod"].ToString();
                                                                readerup.Close();
                                                                if (terms == "Monthly")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(30);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else if (terms == "Every Three Month")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(90);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else if (terms == "Every Six Month")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(180);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(365);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                double cash = due - credit - SC;
                                                                if (Checkbox2.Checked == true)
                                                                {
                                                                    SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + due + "','" + -balance + "','Credit for rent','" + DateTime.Now + "','" + txtReference.Text + "')", con);
                                                                    cmdcrn.ExecuteNonQuery();
                                                                    ///Credit Url Extraction
                                                                    SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                                                    SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                                                    DataTable dtcni = new DataTable();
                                                                    sdacni.Fill(dtcni); Int64 nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString());
                                                                    ///
                                                                    string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID;
                                                                    SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(credit).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                                                    cmdcn.ExecuteNonQuery();
                                                                }
                                                                else
                                                                {
                                                                    SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(credit).ToString("#,##0.00") + "'+' '+'was not recognized as credit','" + FN + "','" + PID + "','Unseen','fas fa-exclamation-circle text-white','icon-circle bg bg-danger','#','MN')", con);
                                                                    cmdcn.ExecuteNonQuery();
                                                                }
                                                                if (CheckGene.Checked == true)
                                                                {
                                                                    SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                                    SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                                    DataTable dtdf = new DataTable();
                                                                    sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + cash + "','" + ah11 + "','" + nb + "','" + txtFSNo.Text + "','Bank')", con);
                                                                    cmdri.ExecuteNonQuery();
                                                                    string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank";
                                                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                                    cmd197h.ExecuteNonQuery();
                                                                    SqlCommand cmd197h3 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','FH')", con);
                                                                    cmd197h3.ExecuteNonQuery();
                                                                    Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (Request.QueryString["bill"] != null)
                                                    {
                                                        SqlCommand cmdupbill = new SqlCommand("Update tblcustomerbill set status='Billed' where customer='" + PID + "'", con);
                                                        cmdupbill.ExecuteNonQuery();
                                                        SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Your bill request has been approved','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                                                        cmd197hb.ExecuteNonQuery();
                                                    }
                                                }
                                                Response.Redirect("bankstatment.aspx?ref2=" + DropDownList1.SelectedItem.Text);
                                            }
                                            //Or record the payment as overpayment
                                            else
                                            {
                                                SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                                                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                                                DataTable dtcn = new DataTable();
                                                sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                                                if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                                                {
                                                    SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                                    SqlDataReader readerbank = cmdbank.ExecuteReader();

                                                    if (readerbank.Read())
                                                    {
                                                        string bankno;
                                                        bankno = readerbank["AccountNumber"].ToString();
                                                        readerbank.Close();
                                                        string refe = Convert.ToString(txtReference.Text);
                                                        string totalannounc = PID + " Paid through bank with ref# " + refe;
                                                        SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                        using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                                        {

                                                            DataTable dt1 = new DataTable();
                                                            sda221.Fill(dt1); int j = dt1.Rows.Count;
                                                            //
                                                            if (j != 0)
                                                            {
                                                                double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                                SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                                cmdday.ExecuteNonQuery();
                                                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                                cvb.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                double t = Convert.ToDouble(txtqtyhand.Text);
                                                                SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                                cvb1.ExecuteNonQuery();
                                                                SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                                b.ExecuteNonQuery();

                                                            }
                                                        }
                                                        SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                        using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                        {
                                                            DataTable dtBrands2322 = new DataTable();
                                                            sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                                    double deb = Convert.ToDouble(txtqtyhand.Text);
                                                                    Double M1 = Convert.ToDouble(ah12893);
                                                                    Double bl1 = M1 + deb;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                                    cmd1974.ExecuteNonQuery();
                                                                }
                                                            }
                                                        }
                                                        //Selecting from account prefernce
                                                        SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                        using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                        {
                                                            DataTable dtBrandss = new DataTable();
                                                            sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                            //Selecting from Income account
                                                            SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                            using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                            {
                                                                DataTable dtBrandss2 = new DataTable();
                                                                sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                            double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                            income = income + SC;
                                                                            Double bl1 = income + M1;
                                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                            cmd45.ExecuteNonQuery();
                                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                                sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                        SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                            double vatfree = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                            double vat = (Convert.ToDouble(txtqtyhand.Text) - SC) - vatfree;
                                                                            Double bl1 = M1 + vat;
                                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                            cmd45.ExecuteNonQuery();
                                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                            cmd1974.ExecuteNonQuery();

                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //Inserting to customer statement
                                                        SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                        SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                        if (readerbcustb.Read())
                                                        {
                                                            string ah11;

                                                            ah11 = readerbcustb["Balance"].ToString();
                                                            readerbcustb.Close();
                                                            double balcust = totalpay - Convert.ToDouble(txtqtyhand.Text);
                                                            double statbalance = Convert.ToDouble(ah11);
                                                            double finalbalance = balcust + statbalance;
                                                            SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + finalbalance + "','" + PID + "')", con);
                                                            custcmd.ExecuteNonQuery();
                                                            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                            SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                            if (readerAC.Read())
                                                            {
                                                                String FN = readerAC["Name"].ToString();
                                                                readerAC.Close();
                                                                con.Close();
                                                                //Activity
                                                                SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                                con.Open();
                                                                cmdAc.ExecuteNonQuery();
                                                                string money = "ETB";


                                                                //Updating the Due Date
                                                                SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                                SqlDataReader readerup = cmdup.ExecuteReader();

                                                                if (readerup.Read())
                                                                {
                                                                    String terms = readerup["PaymentDuePeriod"].ToString();
                                                                    readerup.Close();
                                                                    if (terms == "Monthly")
                                                                    {
                                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                                        DateTime newduedate = duedate.AddDays(30);
                                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                        cmdrent.ExecuteNonQuery();
                                                                    }
                                                                    else if (terms == "Every Three Month")
                                                                    {
                                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                                        DateTime newduedate = duedate.AddDays(90);
                                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                        cmdrent.ExecuteNonQuery();
                                                                    }
                                                                    else if (terms == "Every Six Month")
                                                                    {
                                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                                        DateTime newduedate = duedate.AddDays(180);
                                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                        cmdrent.ExecuteNonQuery();
                                                                    }
                                                                    else
                                                                    {
                                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                                        DateTime newduedate = duedate.AddDays(365);
                                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                        cmdrent.ExecuteNonQuery();
                                                                    }
                                                                    //Insert into cash receipt journal
                                                                    double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                                                    SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                                    SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                                    DataTable dtdf = new DataTable();
                                                                    sdadf.Fill(dtdf); int nb1 = dtdf.Rows.Count + 1;
                                                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','" + ah11 + "','" + nb1 + "','" + txtFSNo.Text + "','Bank')", con);
                                                                    cmdri.ExecuteNonQuery();
                                                                    string url = "rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Bank";
                                                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                                    cmd197h.ExecuteNonQuery();
                                                                    Response.Redirect("rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Bank");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    Response.Redirect("bankstatment.aspx?ref2=" + DropDownList1.SelectedItem.Text);
                                                }
                                                else
                                                {
                                                    Tast.Visible = false;
                                                    lblMsg.Text = "ደንበኛው የቀደመ እዳ በመጠን " + Convert.ToDouble(dtcn.Rows[0][0].ToString()).ToString("#,##0.00") + " ብር ስላለበት እላፊ ብር " + balance.ToString("#,##0.00") + " ለእዳ ይከፈል፡፡";
                                                    lblMsg.ForeColor = Color.Red; lblMsg.Visible = true;
                                                }
                                            }

                                        }
                                    }

                                }

                            }
                            else
                            {
                                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                                if (readercrd.Read())
                                {

                                    double SC = 0;
                                    string pp = readercrd["PaymentDuePeriod"].ToString(); readercrd.Close();
                                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                                    SqlDataReader reader = cmd2.ExecuteReader();

                                    if (reader.Read())
                                    {
                                        string servicecharge; servicecharge = reader["servicecharge"].ToString();
                                        string kc; string duedates = reader["duedate"].ToString();
                                        kc = reader["currentperiodue"].ToString();
                                        reader.Close();
                                        string totalpay1 = Convert.ToDouble(kc).ToString("#,##0.00");
                                        double totalpay = Convert.ToDouble(kc);
                                        if (pp == "Every Three Month") { SC = Convert.ToDouble(servicecharge) * 3; }
                                        else if (pp == "Every Six Month") { SC = Convert.ToDouble(servicecharge) * 6; }
                                        else if (pp == "Monthly") { SC = Convert.ToDouble(servicecharge) * 1; }
                                        else { SC = Convert.ToDouble(servicecharge) * 12; }
                                        double balance = Convert.ToDouble(txtqtyhand.Text) - Convert.ToDouble(totalpay1);
                                        double due = Convert.ToDouble(txtqtyhand.Text);
                                        if (balance == 0)
                                        {

                                            SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                            SqlDataReader readerbank = cmdbank.ExecuteReader();

                                            if (readerbank.Read())
                                            {
                                                string bankno;
                                                bankno = readerbank["AccountNumber"].ToString();
                                                readerbank.Close();
                                                SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                                {
                                                    DataTable dt1 = new DataTable();
                                                    sda221.Fill(dt1); int j = dt1.Rows.Count;
                                                    //
                                                    string refe = Convert.ToString(txtReference.Text);
                                                    string totalannounc = PID + " Paid through bank with ref# " + refe;
                                                    if (j != 0)
                                                    {
                                                        double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                        SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                        cmdday.ExecuteNonQuery();
                                                        SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        cvb.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        double t = Convert.ToDouble(txtqtyhand.Text);
                                                        SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        cvb1.ExecuteNonQuery();
                                                        SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        b.ExecuteNonQuery();

                                                    }
                                                }
                                                //selecting from "+ddlCashorBank.SelectedItem.Text+"
                                                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                {
                                                    DataTable dtBrands2322 = new DataTable();
                                                    sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                            double deb = Convert.ToDouble(txtqtyhand.Text);
                                                            Double M1 = Convert.ToDouble(ah12893);
                                                            Double bl1 = M1 + deb;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                            cmd1974.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                //Selecting from account prefernce
                                                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                {
                                                    DataTable dtBrandss = new DataTable();
                                                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                    //Selecting from Income account
                                                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                    using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                    {
                                                        DataTable dtBrandss2 = new DataTable();
                                                        sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                    cmd1974.ExecuteNonQuery();

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //Inserting to customer statement
                                                SqlCommand cmdreadb = new SqlCommand("select * from tblCustomerStatement where Customer='" + PID + "'  ORDER BY CSID DESC", con);

                                                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                if (readerbcustb.Read())
                                                {
                                                    string ah11;

                                                    ah11 = readerbcustb["Balance"].ToString();
                                                    readerbcustb.Close();

                                                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtVoucher.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + ah11 + "','" + PID + "')", con);
                                                    custcmd.ExecuteNonQuery();
                                                    SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                    if (readerAC.Read())
                                                    {
                                                        String FN = readerAC["Name"].ToString();
                                                        readerAC.Close();
                                                        con.Close();
                                                        //Activity
                                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                        con.Open();
                                                        cmdAc.ExecuteNonQuery();
                                                        string money = "ETB";

                                                        //Updating the Due Date
                                                        SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                        SqlDataReader readerup = cmdup.ExecuteReader();

                                                        if (readerup.Read())
                                                        {
                                                            String terms = readerup["PaymentDuePeriod"].ToString();
                                                            readerup.Close();
                                                            if (terms == "Monthly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(30);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Three Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(90);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Six Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(180);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(365);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            //Generating invoice if checked
                                                            if (CheckGene.Checked == true)
                                                            {
                                                                SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                                DataTable dtdf = new DataTable();
                                                                sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                                                double vatfree = due - SC;
                                                                SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','" + ah11 + "','" + nb + "','" + txtFSNo.Text + "','Bank')", con);
                                                                cmdri.ExecuteNonQuery();
                                                                string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank";
                                                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                                cmd197h.ExecuteNonQuery();
                                                                SqlCommand cmd197h2 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','FH')", con);
                                                                cmd197h2.ExecuteNonQuery();
                                                                SqlCommand cmdAc1 = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                                                cmdAc1.ExecuteNonQuery();
                                                                Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank");
                                                            }
                                                            if (Request.QueryString["bill"] != null)
                                                            {
                                                                SqlCommand cmdupbill = new SqlCommand("Update tblcustomerbill set status='Billed' where customer='" + PID + "'", con);
                                                                cmdupbill.ExecuteNonQuery();
                                                                SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Your bill request has been approved','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                                                                cmd197hb.ExecuteNonQuery();
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                            Response.Redirect("bankstatment.aspx?ref2=" + DropDownList1.SelectedItem.Text);
                                        }
                                        else if (balance < 0)
                                        {
                                            SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                            SqlDataReader readerbank = cmdbank.ExecuteReader();

                                            if (readerbank.Read())
                                            {
                                                string bankno;
                                                bankno = readerbank["AccountNumber"].ToString();
                                                readerbank.Close();
                                                SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                                {
                                                    string refe = Convert.ToString(txtReference.Text);
                                                    string totalannounc = PID + " Paid through bank with ref# " + refe;
                                                    DataTable dt1 = new DataTable();
                                                    sda221.Fill(dt1); int j = dt1.Rows.Count;
                                                    //
                                                    if (j != 0)
                                                    {
                                                        double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                        SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                        cmdday.ExecuteNonQuery();
                                                        SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        cvb.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        double t = Convert.ToDouble(txtqtyhand.Text);
                                                        SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        cvb1.ExecuteNonQuery();
                                                        SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        b.ExecuteNonQuery();

                                                    }
                                                }
                                                //selecting from Accounts Receivable
                                                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
                                                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                {
                                                    DataTable dtBrands2322 = new DataTable();
                                                    sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                            double unpaid = -balance;
                                                            Double bl1 = M1 + unpaid; ;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + unpaid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                            cmd1974.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                SqlCommand cmdacr = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmdacr))
                                                {
                                                    DataTable dtBrands2322 = new DataTable();
                                                    sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
                                                    //
                                                    if (i2 != 0)
                                                    {
                                                        SqlDataReader reader6679034 = cmdacr.ExecuteReader();

                                                        if (reader6679034.Read())
                                                        {
                                                            string ah12893;
                                                            ah12893 = reader6679034["Balance"].ToString();
                                                            reader6679034.Close();
                                                            con.Close();
                                                            con.Open();
                                                            Double M1 = Convert.ToDouble(ah12893);
                                                            Double bl1 = M1 + Convert.ToDouble(txtqtyhand.Text); ;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + txtqtyhand.Text + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                            cmd1974.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                //Selecting from account prefernce
                                                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                {
                                                    DataTable dtBrandss = new DataTable();
                                                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                    //Selecting from Income account
                                                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                    using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                    {
                                                        DataTable dtBrandss2 = new DataTable();
                                                        sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                    double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                    income += SC;
                                                                    Double bl1 = income + M1;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                    double vatfree = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                    double vat = (Convert.ToDouble(txtqtyhand.Text) - SC) - vatfree;
                                                                    Double bl1 = M1 + vat;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                    cmd1974.ExecuteNonQuery();

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //Inserting to customer statement
                                                SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                if (readerbcustb.Read())
                                                {
                                                    string ah11;

                                                    ah11 = readerbcustb["Balance"].ToString();
                                                    readerbcustb.Close();
                                                    double payment = Convert.ToDouble(txtqtyhand.Text);
                                                    double credit = due - payment;
                                                    double balancedue = Convert.ToDouble(ah11);
                                                    double unpaid = -balance;
                                                    double remain = balancedue + unpaid;
                                                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtVoucher.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + remain + "','" + PID + "')", con);
                                                    custcmd.ExecuteNonQuery();
                                                    SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                    if (readerAC.Read())
                                                    {
                                                        String FN = readerAC["Name"].ToString();
                                                        readerAC.Close();
                                                        con.Close();
                                                        //Activity
                                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                        con.Open();
                                                        cmdAc.ExecuteNonQuery();
                                                        string money = "ETB";


                                                        //Updating the Due Date
                                                        SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                        SqlDataReader readerup = cmdup.ExecuteReader();

                                                        if (readerup.Read())
                                                        {
                                                            String terms = readerup["PaymentDuePeriod"].ToString();
                                                            readerup.Close();
                                                            if (terms == "Monthly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(30);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Three Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(90);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Six Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(180);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(365);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            double cash = due - credit - SC;
                                                            if (Checkbox2.Checked == true)
                                                            {
                                                                SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + due + "','" + -balance + "','Credit for rent','" + DateTime.Now + "','" + txtReference.Text + "')", con);
                                                                cmdcrn.ExecuteNonQuery();
                                                                ///Credit Url Extraction
                                                                SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                                                SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                                                DataTable dtcni = new DataTable();
                                                                sdacni.Fill(dtcni); Int64 nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString());
                                                                ///
                                                                string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID + "&&paymentmode=Bank";
                                                                SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(credit).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                                                cmdcn.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(credit).ToString("#,##0.00") + "'+' '+'was not recognized as credit','" + FN + "','" + PID + "','Unseen','fas fa-exclamation-circle text-white','icon-circle bg bg-danger','#','MN')", con);
                                                                cmdcn.ExecuteNonQuery();
                                                            }
                                                            if (CheckGene.Checked == true)
                                                            {
                                                                SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                                DataTable dtdf = new DataTable();
                                                                sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                                                SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + cash + "','" + ah11 + "','" + nb + "','" + txtFSNo.Text + "','Bank')", con);
                                                                cmdri.ExecuteNonQuery();
                                                                string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank";
                                                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                                cmd197h.ExecuteNonQuery();
                                                                SqlCommand cmd197h3 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','FH')", con);
                                                                cmd197h3.ExecuteNonQuery();
                                                                Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank");
                                                            }
                                                            if (Request.QueryString["bill"] != null)
                                                            {
                                                                SqlCommand cmdupbill = new SqlCommand("Update tblcustomerbill set status='Billed' where customer='" + PID + "'", con);
                                                                cmdupbill.ExecuteNonQuery();
                                                                SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Your bill request has been approved','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                                                                cmd197hb.ExecuteNonQuery();
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                            Response.Redirect("bankstatment.aspx?ref2=" + DropDownList1.SelectedItem.Text);
                                        }
                                        else
                                        {
                                            SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                                            SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                                            DataTable dtcn = new DataTable();
                                            sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                                            if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                                            {
                                                SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                                SqlDataReader readerbank = cmdbank.ExecuteReader();

                                                if (readerbank.Read())
                                                {
                                                    string bankno;
                                                    bankno = readerbank["AccountNumber"].ToString();
                                                    readerbank.Close();
                                                    SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                    using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                                    {
                                                        string refe = Convert.ToString(txtReference.Text);
                                                        string totalannounc = PID + " Paid through bank with ref# " + refe;
                                                        DataTable dt1 = new DataTable();
                                                        sda221.Fill(dt1); int j = dt1.Rows.Count;
                                                        //
                                                        if (j != 0)
                                                        {
                                                            double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                            SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                            cmdday.ExecuteNonQuery();
                                                            SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                            cvb.ExecuteNonQuery();
                                                        }
                                                        else
                                                        {
                                                            double t = Convert.ToDouble(txtqtyhand.Text);
                                                            SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                            cvb1.ExecuteNonQuery();
                                                            SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                            b.ExecuteNonQuery();

                                                        }
                                                    }
                                                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                    using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                                                    {
                                                        DataTable dtBrands2322 = new DataTable();
                                                        sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
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
                                                                double deb = Convert.ToDouble(txtqtyhand.Text);
                                                                Double M1 = Convert.ToDouble(ah12893);
                                                                Double bl1 = M1 + deb;
                                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlCashorBank.SelectedItem.Text + "'", con);
                                                                cmd45.ExecuteNonQuery();
                                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + ddlCashorBank.SelectedItem.Text + "','','Cash')", con);
                                                                cmd1974.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                    //Selecting from account prefernce
                                                    SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                                    using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                                    {
                                                        DataTable dtBrandss = new DataTable();
                                                        sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                                                        //Selecting from Income account
                                                        SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                        using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                                                        {
                                                            DataTable dtBrandss2 = new DataTable();
                                                            sdas2.Fill(dtBrandss2); int iss2 = dtBrandss2.Rows.Count;
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
                                                                        double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                        income += SC;
                                                                        Double bl1 = income + M1;
                                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                        cmd45.ExecuteNonQuery();
                                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                            sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
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

                                                                    SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                                                        double vatfree = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                                                        double vat = (Convert.ToDouble(txtqtyhand.Text) - SC) - vatfree;
                                                                        Double bl1 = M1 + vat;
                                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                        cmd45.ExecuteNonQuery();
                                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                        cmd1974.ExecuteNonQuery();

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //Inserting to customer statement
                                                    SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                    if (readerbcustb.Read())
                                                    {
                                                        string ah11;

                                                        ah11 = readerbcustb["Balance"].ToString();
                                                        readerbcustb.Close();
                                                        double balcust = totalpay - Convert.ToDouble(txtqtyhand.Text);
                                                        double statbalance = Convert.ToDouble(ah11);
                                                        double finalbalance = balcust + statbalance;
                                                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + finalbalance + "','" + PID + "')", con);
                                                        custcmd.ExecuteNonQuery();
                                                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                        if (readerAC.Read())
                                                        {
                                                            String FN = readerAC["Name"].ToString();
                                                            readerAC.Close();
                                                            con.Close();
                                                            //Activity
                                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                            con.Open();
                                                            cmdAc.ExecuteNonQuery();
                                                            string money = "ETB";


                                                            //Updating the Due Date
                                                            SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                            SqlDataReader readerup = cmdup.ExecuteReader();

                                                            if (readerup.Read())
                                                            {
                                                                String terms = readerup["PaymentDuePeriod"].ToString();
                                                                readerup.Close();
                                                                if (terms == "Monthly")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(30);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else if (terms == "Every Three Month")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(90);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else if (terms == "Every Six Month")
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(180);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                else
                                                                {
                                                                    DateTime duedate = Convert.ToDateTime(duedates);
                                                                    DateTime newduedate = duedate.AddDays(365);
                                                                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                    cmdrent.ExecuteNonQuery();
                                                                }
                                                                //Insert into cash receipt journal
                                                                double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                                                SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                                                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                                                DataTable dtdf = new DataTable();
                                                                sdadf.Fill(dtdf); int nb1 = dtdf.Rows.Count + 1;
                                                                SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','" + ah11 + "','" + nb1 + "','" + txtFSNo.Text + "','Bank')", con);
                                                                cmdri.ExecuteNonQuery();
                                                                string url = "rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Bank";
                                                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                                                cmd197h.ExecuteNonQuery();
                                                                Response.Redirect("rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Bank");
                                                            }
                                                        }
                                                    }
                                                }
                                                Response.Redirect("bankstatment.aspx?ref2=" + DropDownList1.SelectedItem.Text);
                                            }
                                            else
                                            {
                                                Tast.Visible = false;
                                                lblMsg.Text = "ደንበኛው የቀደመ እዳ በመጠን " + Convert.ToDouble(dtcn.Rows[0][0].ToString()).ToString("#,##0.00") + " ብር ስላለበት እላፊ ብር " + balance.ToString("#,##0.00") + " ለእዳ ይከፈል፡፡";
                                                lblMsg.ForeColor = Color.Red; lblMsg.Visible = true;
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
        protected void Button3_Click(object sender, EventArgs e)
        {
            ReferenceFinder RF = new ReferenceFinder(txtReference.Text);
            bool IsReferenceFound = RF.FindReferenceNumber();


            if (ddlCashorBank.SelectedItem.Text == "Cash at Bank")
            {
                if (IsReferenceFound == true)
                {
                    string message = "Reference Number Already Exist";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
                else
                {
                    BindBankPay();
                }
            }
            else
            {
                if (IsReferenceFound == true)
                {
                    string message = "Reference Number Already Exist";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
                else
                {
                    BindCashPay();
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("rentstatus1.aspx");
        }
        private double bindcredit1()
        {
            string refe = "RAKS-" + RandomPassword();
            txtReference.Text = refe;
            String PID = Convert.ToString(Request.QueryString["cust"]);
            double credit = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                {

                }
                else
                {
                    credit = Convert.ToDouble(dtcn.Rows[0][0].ToString());
                }

            }
            return credit;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmdrent = new SqlCommand("delete tblNotes where customer='" + PID + "'", con);
                con.Open();
                cmdrent.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
        }
        protected void ddlCashorBank_TextChanged(object sender, EventArgs e)
        {
            if (ddlCashorBank.SelectedItem.Text == "Cash at Bank")
            {
                BankDiv.Visible = true;
                ChequeDiv.Visible = true;
            }
            if (ddlCashorBank.SelectedItem.Text == "Cash on Hand")
            {
                BankDiv.Visible = false;
                ChequeDiv.Visible = false;
            }
        }
        protected void btnUpdateSC_Click(object sender, EventArgs e)
        {
            if (txtServiceChargeUpdate.Text == "")
            {
                string message = "Please put the service charge amount.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    String PID = Convert.ToString(Request.QueryString["cust"]);
                    SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                    SqlDataReader readercrd = cmdcrd.ExecuteReader();
                    if (readercrd.Read())
                    {
                        double SC = 0;
                        string price1 = readercrd["price"].ToString();
                        string pp = readercrd["PaymentDuePeriod"].ToString();
                        string limit = readercrd["CreditLimit"].ToString();
                        readercrd.Close();
                        double total = Convert.ToDouble(price1);
                        if (pp == "Every Three Month") { SC = Convert.ToDouble(txtServiceChargeUpdate.Text) * 3; }
                        else if (pp == "Monthly") { SC = Convert.ToDouble(txtServiceChargeUpdate.Text) * 1; }
                        else { SC = Convert.ToDouble(txtServiceChargeUpdate.Text) * 12; }
                        double pricevat = 3 * total + 3 * Convert.ToDouble(txtServiceChargeUpdate.Text) + 3 * total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + pricevat + "', servicecharge='" + txtServiceChargeUpdate.Text + "' where customer='" + PID + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + txtServiceChargeUpdate.Text + "' where FllName='" + PID + "'", con);
                        cmdre2.ExecuteNonQuery();
                        Response.Redirect("cashpay.aspx?cust=" + PID);
                    }
                }
            }
        }
        protected void btnUpdateTIN_Click(object sender, EventArgs e)
        {
            if (txtTIN.Text == "")
            {
                string message = "Please put the TIN number.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    String PID = Convert.ToString(Request.QueryString["cust"]);
                    SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set TIN='" + txtTIN.Text + "',addresscust='" + txtCustAddress.Text + "' where FllName='" + PID + "'", con);
                    cmdre2.ExecuteNonQuery();
                    Response.Redirect("cashpay.aspx?cust=" + PID);
                }
            }
        }
    }
}