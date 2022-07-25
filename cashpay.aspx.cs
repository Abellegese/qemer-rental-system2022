using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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
                    bindsearch();
                    bindqty(); bindoverpayment(); bindcredit();
                    BindNotesContent(); bindbankaccount();
                    BindBrandsRptr2(); ShowDataIncreaseMonthly(); 
                    bindSC(); bindFSnumber(); bindDueDate();
                    bindCreditTitle();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private double GetServiceCharge()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            double SC = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (Checkbox3.Checked == true){
                    SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                    SqlDataReader readercrd = cmdcrd.ExecuteReader();
                    if (readercrd.Read())
                    {

                        string pp = readercrd["PaymentDuePeriod"].ToString();
                        readercrd.Close();
                        SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                        SqlDataReader reader = cmd2.ExecuteReader();

                        if (reader.Read())
                        {
                            string servicecharge; servicecharge = reader["servicecharge"].ToString();
                            if (pp == "Every Three Month") { SC = Convert.ToDouble(servicecharge) * 3; }
                            else if (pp == "Every Six Month") { SC = Convert.ToDouble(servicecharge) * 6; }
                            else if (pp == "Monthly") { SC = Convert.ToDouble(servicecharge) * 1; }
                            else { SC = Convert.ToDouble(servicecharge) * 12; }
                        }
                    }
                }
            }
            return SC;
        }
        
        private void bindCreditTitle()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcn = new SqlCommand("select*from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); int nb = dtcn.Rows.Count;
                if(nb != 0)
                {
                    ddlExistingCredit.DataSource = dtcn;
                    ddlExistingCredit.DataTextField = "Notes";
                    ddlExistingCredit.DataValueField = "id";
                    ddlExistingCredit.DataBind();
                }
            }
        }
        private void GetandUpdateCredit(double newCredit)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2vb = new SqlCommand("select * from tblcreditnote where id='" + ddlExistingCredit.SelectedValue + "'", con);
                SqlDataReader readervb = cmd2vb.ExecuteReader();
                if (readervb.Read())
                {
                    string existingBalance;
                    existingBalance = readervb["Balance"].ToString();readervb.Close();
                    double newBalance = newCredit + Convert.ToDouble(existingBalance);
                    SqlCommand cmd = new SqlCommand("Update tblcreditnote set Balance='" + newBalance + "'  where id='" + ddlExistingCredit.SelectedValue + "'", con);
                    cmd.ExecuteNonQuery();
                }
                
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
                        long d = -Convert.ToInt32(dayleft1);
                        duedate2.InnerText = "Payment " + d + " Days" + " Passed";
                        duedate2.Attributes.Add("class", "small  text-danger border-bottom");
                    }
                }
            }
        }
        private bool CheckFSNumber()
        {
            bool isFound = false;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select fsno from tblrentreceipt where fsno = '" + txtFSNo.Text + "'", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); long i = dt.Rows.Count;
                    if (i != 0)
                    {
                        isFound = true;
                    }
                }
            }
            return isFound;
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
                        else if (pp == "Every Six Month") { SC = Convert.ToDouble(servicecharge) * 6; }
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
                        sda.Fill(dt); long i = dt.Rows.Count;
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
                sda22c3.Fill(dtBrands232c3); long i2c3 = dtBrands232c3.Rows.Count;
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
        private void BindNotesContent()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmdcn = new SqlCommand("select*from tblNotes where customer='" + PID + "' and status='unseen'", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
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
                sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
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
                SqlCommand cmdcn = new SqlCommand("select sum(InvAmount),sum(Payment)  from tblCustomerStatement where customer='" + PID + "'", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
                if (nb == 0 || dtcn.Rows[0][0].ToString()==null || dtcn.Rows[0][0].ToString()=="")
                {

                }
                else
                {
                    double overpaid = Convert.ToDouble(dtcn.Rows[0][0].ToString()) - Convert.ToDouble(dtcn.Rows[0][1].ToString());
                    if (overpaid < 0)
                    {
                       

                        OverPayAmount.InnerText = (-overpaid).ToString("#,##0.00");
                        double expamount = Convert.ToDouble(txtqtyhand.Text) - overpaid + bindcredit1();
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
                    double SC = GetServiceCharge();
                    UserUtility getUserName = new UserUtility();
                    string FN = getUserName.BindUser();                                
                    CustomerUtil getAmount = new CustomerUtil(PID);
                    double climit = Convert.ToDouble(getAmount.GetCustomerName.Item3);
                    double totalpay = Convert.ToDouble(getAmount.GetCustomerRentInfo.Item2);
                    double balance = Convert.ToDouble(txtqtyhand.Text) - totalpay;
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

                                if (-balance > climit && Convert.ToDouble(CashPay1.InnerText) > 0)
                                {

                                    double ex = -balance;
                                    string message = "Credit Limit Exceeded By " + ex.ToString("#,##0.00") + " Addind the existing credit " + Convert.ToDouble(CashPay1.InnerText);
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "')", true);

                                }
                                else
                                {
                                    BindNotes();
                                    if (balance == 0)
                                    {
                                        //Declare New User
                                        //selecting from "+ddlCashorBank.SelectedItem.Text+"
                                        GeneralLedger GLCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                        GLCash.increaseDebitAccount();
                                        //Selecting from account prefernce
                                        GeneralLedger getAccountInfo = new GeneralLedger();
                                        DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                        double income = (totalpay - SC) / 1.15;
                                        double vat = totalpay - SC - income;
                                        income = income + SC;

                                        GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                        GLIncome.increaseCreditAccount();

                                        GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                        GLTax.increaseCreditAccount();
                                        //Inserting to customer statement
                                        CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                        updateStatus.UpdateStatement();
                                        updateStatus.UpdateDueDate();
                                        //Insert into cash receipt journal
                                        double payment = Convert.ToDouble(txtqtyhand.Text);
                                        SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                        SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                        DataTable dtdf = new DataTable();
                                        sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                        double vatfree = totalpay - SC;
                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                        cmdri.ExecuteNonQuery();
                                        string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";
                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                        cmd197h.ExecuteNonQuery();
                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                        cmdAc.ExecuteNonQuery();
                                        Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash");

                                    }
                                    else if (balance < 0)
                                    {
                                        double newclimit = climit + balance;
                                        SqlCommand cmdclim = new SqlCommand("Update tblCustomers set CreditLimit='" + newclimit + "' where FllName='" + PID + "'", con);
                                        cmdclim.ExecuteNonQuery();
                                        //Calling and Updating Accounts Receivable
                                        GeneralLedger GlReceivable = new GeneralLedger("Accounts Receivable", PID, -balance);
                                        GlReceivable.increaseDebitAccount();
                                        //Calling Cash Account
                                        GeneralLedger GlCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                        GlCash.increaseDebitAccount();
                                        GeneralLedger getAccountInfo = new GeneralLedger();
                                        DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                        //Crediting income and tax account
                                        double income = (totalpay - SC) / 1.15;
                                        double vat = totalpay - SC - income;
                                        income = income + SC;
                                        GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                        GLIncome.increaseCreditAccount();

                                        GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                        GLTax.increaseCreditAccount();
                                        CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                        updateStatus.UpdateStatement();
                                        updateStatus.UpdateDueDate();

                                        if (Checkbox2.Checked == true)
                                        {
                                            if (Checkbox1.Checked == true)
                                            {
                                                GetandUpdateCredit(-balance);
                                                ///
                                                string crediturl = "creditnotedetails.aspx?ref2=" + ddlExistingCredit.SelectedValue + "&&cust=" + PID;
                                                SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB'+' '+'" + Convert.ToDouble(-balance).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                                cmdcn.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','"+ Convert.ToDouble(txtqtyhand.Text) + "','" + -balance + "','" + txtCreditTitle.Text + "','" + DateTime.Now + "','" + txtReference.Text + "')", con);
                                                cmdcrn.ExecuteNonQuery();
                                                ///Credit Url Extraction
                                                SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                                SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                                DataTable dtcni = new DataTable();
                                                sdacni.Fill(dtcni); long nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString());
                                                ///
                                                string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID;
                                                SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB'+' '+'" + Convert.ToDouble(-balance).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                                cmdcn.ExecuteNonQuery();
                                            }
                                        }
                                        double payment = Convert.ToDouble(txtqtyhand.Text);
                                        SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                        SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                        DataTable dtdf = new DataTable();
                                        sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                        double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                        cmdri.ExecuteNonQuery();
                                        string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";
                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                        cmd197h.ExecuteNonQuery();
                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                        cmdAc.ExecuteNonQuery();
                                        Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash");
                                    }
                                    else
                                    {
                                        SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                                        SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                                        DataTable dtcn = new DataTable();
                                        sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
                                        if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                                        {

                                            GeneralLedger GLCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                            GLCash.increaseDebitAccount();
                                            //Selecting from account prefernce
                                            GeneralLedger getAccountInfo = new GeneralLedger();
                                            DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                            double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                            double vat = Convert.ToDouble(txtqtyhand.Text) - SC - income;
                                            income = income + SC;
                                            GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                            GLIncome.increaseCreditAccount();
                                            //Tax account
                                            GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                            GLTax.increaseCreditAccount();
                                            //Inserting to customer statement
                                            CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                            updateStatus.UpdateStatement();
                                            updateStatus.UpdateDueDate();
                                            //Insert into cash receipt journal
                                            double payment = Convert.ToDouble(txtqtyhand.Text);
                                            SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                            SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                            DataTable dtdf = new DataTable();
                                            sdadf.Fill(dtdf); long nb1 = dtdf.Rows.Count + 1;
                                            double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                            SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb1 + "','" + txtFSNo.Text + "','Cash')", con);
                                            cmdri.ExecuteNonQuery();
                                            string url = "rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Cash";
                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                            cmd197h.ExecuteNonQuery();
                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                            cmdAc.ExecuteNonQuery();
                                            Response.Redirect("rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Cash");
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
                            else
                            {
                                BindNotes();
                                if (balance == 0)
                                {
                                    //Declare New User
                                    //selecting from "+ddlCashorBank.SelectedItem.Text+"
                                    GeneralLedger GLCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                    GLCash.increaseDebitAccount();
                                    //Selecting from account prefernce
                                    GeneralLedger getAccountInfo = new GeneralLedger();
                                    DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                    double income = (totalpay - SC) / 1.15;
                                    double vat = totalpay - SC - income;
                                    income = income + SC;

                                    GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                    GLIncome.increaseCreditAccount();

                                    GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                    GLTax.increaseCreditAccount();
                                    //Inserting to customer statement
                                    CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                    updateStatus.UpdateStatement();
                                    updateStatus.UpdateDueDate();
                                    //Insert into cash receipt journal
                                    double payment = Convert.ToDouble(txtqtyhand.Text);
                                    SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                    SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                    DataTable dtdf = new DataTable();
                                    sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                    double vatfree = totalpay - SC;
                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                    cmdri.ExecuteNonQuery();
                                    string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";
                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                    cmd197h.ExecuteNonQuery();
                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                    cmdAc.ExecuteNonQuery();
                                    Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash");

                                }
                                else if (balance < 0)
                                {
                                    double newclimit = climit + balance;
                                    SqlCommand cmdclim = new SqlCommand("Update tblCustomers set CreditLimit='" + newclimit + "' where FllName='" + PID + "'", con);
                                    cmdclim.ExecuteNonQuery();
                                    //Calling and Updating Accounts Receivable
                                    GeneralLedger GlReceivable = new GeneralLedger("Accounts Receivable", PID, -balance);
                                    GlReceivable.increaseDebitAccount();
                                    //Calling Cash Account
                                    GeneralLedger GlCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                    GlCash.increaseDebitAccount();
                                    GeneralLedger getAccountInfo = new GeneralLedger();
                                    DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                    //Crediting income and tax account
                                    double income = (totalpay - SC) / 1.15;
                                    double vat = totalpay - SC - income;
                                    income = income + SC;
                                    GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                    GLIncome.increaseCreditAccount();
                                    GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                    GLTax.increaseCreditAccount();
                                    CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                    updateStatus.UpdateStatement();
                                    updateStatus.UpdateDueDate();

                                    if (Checkbox2.Checked == true)
                                    {
                                        if (Checkbox1.Checked == true)
                                        {
                                            GetandUpdateCredit(-balance);
                                            ///
                                            string crediturl = "creditnotedetails.aspx?ref2=" + ddlExistingCredit.SelectedValue + "&&cust=" + PID;
                                            SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB'+' '+'" + Convert.ToDouble(-balance).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                            cmdcn.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','"+Convert.ToDouble(txtqtyhand.Text)+"','" + -balance + "','" + txtCreditTitle.Text + "','" + DateTime.Now + "','" + txtReference.Text + "')", con);
                                            cmdcrn.ExecuteNonQuery();
                                            ///Credit Url Extraction
                                            SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                            SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                            DataTable dtcni = new DataTable();
                                            sdacni.Fill(dtcni); long nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString());
                                            ///
                                            string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID;
                                            SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB'+' '+'" + Convert.ToDouble(-balance).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                            cmdcn.ExecuteNonQuery();
                                        }
                                    }
                                    double payment = Convert.ToDouble(txtqtyhand.Text);
                                    SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                    SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                    DataTable dtdf = new DataTable();
                                    sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                    double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                    cmdri.ExecuteNonQuery();
                                    string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";
                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                    cmd197h.ExecuteNonQuery();
                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);
                                    cmdAc.ExecuteNonQuery();
                                    Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash");
                                }
                                else
                                {
                                    SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                                    SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                                    DataTable dtcn = new DataTable();
                                    sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
                                    if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                                    {

                                        GeneralLedger GLCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                        GLCash.increaseDebitAccount();
                                        //Selecting from account prefernce
                                        GeneralLedger getAccountInfo = new GeneralLedger();
                                        DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                        double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                        double vat = Convert.ToDouble(txtqtyhand.Text) - SC - income;
                                        income = income + SC;
                                        GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                        GLIncome.increaseCreditAccount();
                                        //Tax account
                                        GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                        GLTax.increaseCreditAccount();
                                        //Inserting to customer statement
                                        CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                        updateStatus.UpdateStatement();
                                        updateStatus.UpdateDueDate();
                                        //Insert into cash receipt journal
                                        double payment = Convert.ToDouble(txtqtyhand.Text);
                                        SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                        SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                        DataTable dtdf = new DataTable();
                                        sdadf.Fill(dtdf); long nb1 = dtdf.Rows.Count + 1;
                                        double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb1 + "','" + txtFSNo.Text + "','Cash')", con);
                                        cmdri.ExecuteNonQuery();
                                        string url = "rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Cash";
                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                        cmd197h.ExecuteNonQuery();
                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                        cmdAc.ExecuteNonQuery();
                                        Response.Redirect("rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Cash");
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
                    double SC = GetServiceCharge();
                    UserUtility getUserName = new UserUtility();
                    string FN = getUserName.BindUser();
                    CustomerUtil getAmount = new CustomerUtil(PID);
                    double climit = Convert.ToDouble(getAmount.GetCustomerName.Item3);
                    double totalpay = Convert.ToDouble(getAmount.GetCustomerRentInfo.Item2);
                    double balance = Convert.ToDouble(txtqtyhand.Text) - totalpay;
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

                                if (-balance > climit && Convert.ToDouble(CashPay1.InnerText) > 0)
                                {

                                    double ex = -balance;
                                    string message = "Credit Limit Exceeded By " + ex.ToString("#,##0.00")+" Addind the existing credit "+Convert.ToDouble(CashPay1.InnerText);
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "')", true);

                                }
                                else
                                {
                                    BindNotes();
                                    //Calling and updating bank account
                                    BankOperation updateBankAccount = new BankOperation(DropDownList1.SelectedItem.Text,txtVoucher.Text,txtReference.Text,PID,Convert.ToDouble(txtqtyhand.Text));
                                    updateBankAccount.increaseBankAccount();
                                    if (balance == 0)
                                    {
                          

                                        //selecting from "+ddlCashorBank.SelectedItem.Text+"
                                        GeneralLedger GLCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                        GLCash.increaseDebitAccount();
                                        //Selecting from account prefernce
                                        GeneralLedger getAccountInfo = new GeneralLedger();
                                        DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                        double income = (totalpay - SC) / 1.15;
                                        double vat = totalpay - SC - income;
                                        income = income + SC;

                                        GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                        GLIncome.increaseCreditAccount();

                                        GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                        GLTax.increaseCreditAccount();
                                        //Inserting to customer statement
                                        CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                        updateStatus.UpdateStatement();
                                        updateStatus.UpdateDueDate();
                                        //Insert into cash receipt journal
                                        double payment = Convert.ToDouble(txtqtyhand.Text);
                                        SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                        SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                        DataTable dtdf = new DataTable();
                                        sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                        double vatfree = totalpay - SC;
                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb + "','" + txtFSNo.Text + "','Bank')", con);
                                        cmdri.ExecuteNonQuery();
                                        string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank";
                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                        cmd197h.ExecuteNonQuery();
                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                        cmdAc.ExecuteNonQuery();
                                        Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank");

                                    }
                                    else if (balance < 0)
                                    {
                                        double newclimit = climit + balance;
                                        SqlCommand cmdclim = new SqlCommand("Update tblCustomers set CreditLimit='" + newclimit + "' where FllName='" + PID + "'", con);
                                        cmdclim.ExecuteNonQuery();
                                        //Calling and Updating Accounts Receivable
                                        GeneralLedger GlReceivable = new GeneralLedger("Accounts Receivable", PID, -balance);
                                        GlReceivable.increaseDebitAccount();
                                        //Calling Cash Account
                                        GeneralLedger GlCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                        GlCash.increaseDebitAccount();
                                        GeneralLedger getAccountInfo = new GeneralLedger();
                                        DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                        //Crediting income and tax account
                                        double income = (totalpay - SC) / 1.15;
                                        double vat = totalpay - SC - income;
                                        income = income + SC;
                                        GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                        GLIncome.increaseCreditAccount();

                                        GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                        GLTax.increaseCreditAccount();
                                        CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                        updateStatus.UpdateStatement();
                                        updateStatus.UpdateDueDate();

                                        if (Checkbox2.Checked == true)
                                        {
                                            if (Checkbox1.Checked == true)
                                            {
                                                GetandUpdateCredit(-balance);
                                                ///
                                                string crediturl = "creditnotedetails.aspx?ref2=" + ddlExistingCredit.SelectedValue + "&&cust=" + PID;
                                                SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB'+' '+'" + Convert.ToDouble(-balance).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                                cmdcn.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + Convert.ToDouble(txtqtyhand.Text) + "','" + -balance + "','" + txtCreditTitle.Text + "','" + DateTime.Now + "','" + txtReference.Text + "')", con);
                                                cmdcrn.ExecuteNonQuery();
                                                ///Credit Url Extraction
                                                SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                                SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                                DataTable dtcni = new DataTable();
                                                sdacni.Fill(dtcni); long nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString());
                                                ///
                                                string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID;
                                                SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB'+' '+'" + Convert.ToDouble(-balance).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                                cmdcn.ExecuteNonQuery();
                                            }
                                        }
                                        double payment = Convert.ToDouble(txtqtyhand.Text);
                                        SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                        SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                        DataTable dtdf = new DataTable();
                                        sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                        double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb + "','" + txtFSNo.Text + "','Bank')", con);
                                        cmdri.ExecuteNonQuery();
                                        string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank";
                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                        cmd197h.ExecuteNonQuery();
                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                        cmdAc.ExecuteNonQuery();
                                        Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank");
                                    }
                                    else
                                    {
                                        SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                                        SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                                        DataTable dtcn = new DataTable();
                                        sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
                                        if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                                        {

                                            GeneralLedger GLCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                            GLCash.increaseDebitAccount();
                                            //Selecting from account prefernce
                                            GeneralLedger getAccountInfo = new GeneralLedger();
                                            DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                            double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                            double vat = Convert.ToDouble(txtqtyhand.Text) - SC - income;
                                            income = income + SC;
                                            GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                            GLIncome.increaseCreditAccount();
                                            //Tax account
                                            GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                            GLTax.increaseCreditAccount();
                                            //Inserting to customer statement
                                            CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                            updateStatus.UpdateStatement();
                                            updateStatus.UpdateDueDate();
                                            //Insert into cash receipt journal
                                            double payment = Convert.ToDouble(txtqtyhand.Text);
                                            SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                            SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                            DataTable dtdf = new DataTable();
                                            sdadf.Fill(dtdf); long nb1 = dtdf.Rows.Count + 1;
                                            double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                            SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb1 + "','" + txtFSNo.Text + "','Bank')", con);
                                            cmdri.ExecuteNonQuery();
                                            string url = "rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Bank";
                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                            cmd197h.ExecuteNonQuery();
                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                            cmdAc.ExecuteNonQuery();
                                            Response.Redirect("rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Bank");
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
                            else
                            {
                                BindNotes();
                                BankOperation updateBankAccount = new BankOperation(DropDownList1.SelectedItem.Text, txtVoucher.Text, txtReference.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                updateBankAccount.increaseBankAccount();
                                if (balance == 0)
                                {
                                    //Declare New User
                                    //selecting from "+ddlCashorBank.SelectedItem.Text+"
                                    GeneralLedger GLCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                    GLCash.increaseDebitAccount();
                                    //Selecting from account prefernce
                                    GeneralLedger getAccountInfo = new GeneralLedger();
                                    DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                    double income = (totalpay - SC) / 1.15;
                                    double vat = totalpay - SC - income;
                                    income = income + SC;

                                    GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                    GLIncome.increaseCreditAccount();

                                    GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                    GLTax.increaseCreditAccount();
                                    //Inserting to customer statement
                                    CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                    updateStatus.UpdateStatement();
                                    updateStatus.UpdateDueDate();
                                    //Insert into cash receipt journal
                                    double payment = Convert.ToDouble(txtqtyhand.Text);
                                    SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                    SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                    DataTable dtdf = new DataTable();
                                    sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                    double vatfree = totalpay - SC;
                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb + "','" + txtFSNo.Text + "','Bank')", con);
                                    cmdri.ExecuteNonQuery();
                                    string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank";
                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                    cmd197h.ExecuteNonQuery();
                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                    cmdAc.ExecuteNonQuery();
                                    Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank");

                                }
                                else if (balance < 0)
                                {
                                    double newclimit = climit + balance;
                                    SqlCommand cmdclim = new SqlCommand("Update tblCustomers set CreditLimit='" + newclimit + "' where FllName='" + PID + "'", con);
                                    cmdclim.ExecuteNonQuery();
                                    //Calling and Updating Accounts Receivable
                                    GeneralLedger GlReceivable = new GeneralLedger("Accounts Receivable", PID, -balance);
                                    GlReceivable.increaseDebitAccount();
                                    //Calling Cash Account
                                    GeneralLedger GlCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                    GlCash.increaseDebitAccount();
                                    GeneralLedger getAccountInfo = new GeneralLedger();
                                    DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                    //Crediting income and tax account
                                    double income = (totalpay - SC) / 1.15;
                                    double vat = totalpay - SC - income;
                                    income = income + SC;
                                    GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                    GLIncome.increaseCreditAccount();
                                    GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                    GLTax.increaseCreditAccount();
                                    CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                    updateStatus.UpdateStatement();
                                    updateStatus.UpdateDueDate();

                                    if (Checkbox2.Checked == true)
                                    {
                                        if (Checkbox1.Checked == true)
                                        {
                                            GetandUpdateCredit(-balance);
                                            ///
                                            string crediturl = "creditnotedetails.aspx?ref2=" + ddlExistingCredit.SelectedValue + "&&cust=" + PID;
                                            SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB'+' '+'" + Convert.ToDouble(-balance).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                            cmdcn.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + Convert.ToDouble(txtqtyhand.Text) + "','" + -balance + "','" + txtCreditTitle.Text + "','" + DateTime.Now + "','" + txtReference.Text + "')", con);
                                            cmdcrn.ExecuteNonQuery();
                                            ///Credit Url Extraction
                                            SqlCommand cmdcni = new SqlCommand("select * from tblcreditnote order by id desc", con);
                                            SqlDataAdapter sdacni = new SqlDataAdapter(cmdcni);
                                            DataTable dtcni = new DataTable();
                                            sdacni.Fill(dtcni); long nbcni = Convert.ToInt64(dtcni.Rows[0][0].ToString());
                                            ///
                                            string crediturl = "creditnotedetails.aspx?ref2=" + nbcni + "&&cust=" + PID;
                                            SqlCommand cmdcn = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB'+' '+'" + Convert.ToDouble(-balance).ToString("#,##0.00") + "'+' '+'Issued as credit into Accounts Receivable account','" + FN + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-warning','" + crediturl + "','MN')", con);
                                            cmdcn.ExecuteNonQuery();
                                        }
                                    }
                                    double payment = Convert.ToDouble(txtqtyhand.Text);
                                    SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                    SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                    DataTable dtdf = new DataTable();
                                    sdadf.Fill(dtdf); long nb = dtdf.Rows.Count + 1;
                                    double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb + "','" + txtFSNo.Text + "','Bank')", con);
                                    cmdri.ExecuteNonQuery();
                                    string url = "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank";
                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                    cmd197h.ExecuteNonQuery();
                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);
                                    cmdAc.ExecuteNonQuery();
                                    Response.Redirect("rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Bank");
                                }
                                else
                                {
                                    SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                                    SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                                    DataTable dtcn = new DataTable();
                                    sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
                                    if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                                    {

                                        GeneralLedger GLCash = new GeneralLedger(ddlCashorBank.SelectedItem.Text, PID, Convert.ToDouble(txtqtyhand.Text));
                                        GLCash.increaseDebitAccount();
                                        //Selecting from account prefernce
                                        GeneralLedger getAccountInfo = new GeneralLedger();
                                        DataTable dtBrandss = getAccountInfo.GetAccountInfo();
                                        double income = (Convert.ToDouble(txtqtyhand.Text) - SC) / 1.15;
                                        double vat = Convert.ToDouble(txtqtyhand.Text) - SC - income;
                                        income = income + SC;
                                        GeneralLedger GLIncome = new GeneralLedger(dtBrandss.Rows[0][1].ToString(), PID, income);
                                        GLIncome.increaseCreditAccount();
                                        //Tax account
                                        GeneralLedger GLTax = new GeneralLedger(dtBrandss.Rows[0][4].ToString(), PID, vat);
                                        GLTax.increaseCreditAccount();
                                        //Inserting to customer statement
                                        CustomerUtil updateStatus = new CustomerUtil(PID, txtReference.Text, txtqtyhand.Text, totalpay.ToString());
                                        updateStatus.UpdateStatement();
                                        updateStatus.UpdateDueDate();
                                        //Insert into cash receipt journal
                                        double payment = Convert.ToDouble(txtqtyhand.Text);
                                        SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                                        SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                        DataTable dtdf = new DataTable();
                                        sdadf.Fill(dtdf); long nb1 = dtdf.Rows.Count + 1;
                                        double vatfree = Convert.ToDouble(txtqtyhand.Text) - SC;
                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','0','" + vatfree + "','','" + nb1 + "','" + txtFSNo.Text + "','Bank')", con);
                                        cmdri.ExecuteNonQuery();
                                        string url = "rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Bank";
                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','ETB '+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into " + ddlCashorBank.SelectedItem.Text + " account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);

                                        cmd197h.ExecuteNonQuery();
                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                        cmdAc.ExecuteNonQuery();
                                        Response.Redirect("rentinvoicereport.aspx?id=" + nb1 + "&&cust=" + PID + "&&paymentmode=Bank");
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
        protected void Button3_Click(object sender, EventArgs e)
        {
            ReferenceFinder RF = new ReferenceFinder(txtReference.Text);
            bool IsReferenceFound = RF.FindReferenceNumber();
            bool isFound = CheckFSNumber();
            if (isFound == true)
            {
                string message = "FS Number Already Exist. Please increase the current FS Number by 1.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                if (ddlCashorBank.SelectedItem.Text == "Cash at Bank")
                {
                    if (IsReferenceFound == true)
                    {
                        string message = "Reference Number Already Exist";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');window.location.reload;", true);
                    }
                    else
                    {
                        if (Checkbox1.Checked == true)
                        {
                            if (ddlExistingCredit.Items.Count == 0)
                            {
                                string message = "There is not any existing credit to merge with!";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');window.location.reload;", true);
                            }
                            else
                            {
                                BindBankPay();
                            }
                        }
                        if (Checkbox2.Checked == true)
                        {
                            if (Checkbox1.Checked == false)
                            {
                                if (txtCreditTitle.Text == "")
                                {
                                    string message = "Please Put New Credit Title!";
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');window.location.reload;", true);
                                }
                                else
                                {
                                    BindBankPay();
                                }
                            }
                        }
                        if (Checkbox2.Checked == false && Checkbox1.Checked == false)
                        {
                            BindBankPay();
                        }
                    }
                }
                else
                {
                    if (IsReferenceFound == true)
                    {
                        string message = "Reference Number Already Exist";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');window.location.reload;", true);
                    }
                    else
                    {
                        if (Checkbox1.Checked == true)
                        {
                            if (ddlExistingCredit.Items.Count == 0)
                            {
                                string message = "There is not any existing credit to merge with!";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');window.location.reload;", true);
                            }
                            else
                            {
                                BindCashPay();
                            }
                        }
                        if (Checkbox2.Checked == true)
                        {
                            if (Checkbox1.Checked == false)
                            {
                                if (txtCreditTitle.Text == "")
                                {
                                    string message = "Please Put New Credit Title!";
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');window.location.reload;", true);
                                }
                                else
                                {
                                    BindCashPay();
                                }
                            }
                        }
                        if (Checkbox2.Checked == false && Checkbox1.Checked == false)
                        {
                            BindCashPay();
                        }
                    }
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
                sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
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