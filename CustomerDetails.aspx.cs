using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace advtech.Finance.Accounta
{
    public partial class CustomerDetails : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'CustomerDetails.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'CustomerDetails.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    bindsearch();
                    String PID = Convert.ToString(Request.QueryString["ref2"]);
                    Literal1.Text = "<title>" + PID + "</title >"; bindimage(); bindimage2(); binddelinquency();
                    BindBrandsRptr3(); BindBrandsRptr4(); ShowData(); BindMainCategory12(); bindduedate(); bindshop(); bindshop2();
                    BindBrandsRptrc(); BindBrandsRptra(); BindBrandsRptrb(); agreementdate(); bindgurantor(); bindshop1();
                    BindDueAmount(); BindBrandsRptrd(); BindWriteOff(); BindNotDue(); BindTotalBad();
                    bindoverpayment(); invoiceinfo(); bind_customer_amharic_name_null(); bindExchange_customer();

                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }
        private void bindExchange_customer()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblCustomers where status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlExchangedShop.DataSource = dt;
                    ddlExchangedShop.DataTextField = "FllName";
                    ddlExchangedShop.DataValueField = "shop";
                    ddlExchangedShop.DataBind();
                    ddlExchangedShop.Items.Insert(0, new ListItem("-Select Customer-", "0"));
                }
            }
        }
        private string BindUser()
        {
            string user = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String FN = readerAC["Name"].ToString();
                    readerAC.Close();
                    user += FN;
                }
            }
            return user;
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
                            ShopFree.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                        }
                        if (paid != "")
                        {
                            payment = Convert.ToDouble(paid);
                            ShopCustomer.InnerText = Convert.ToDouble(paid).ToString("#,##0.00");
                        }
                        double balance = invoice - payment;
                        if (balance != 0)
                        {
                            ShopPercentage_Occupy.InnerText = balance.ToString("#,##0.00");
                        }

                    }
                }
            }
        }
        private void bindshop2()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblshop where status='Free' or status='Occupied'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlSHopExpand.DataSource = dt;
                    ddlSHopExpand.DataTextField = "shopno";
                    ddlSHopExpand.DataValueField = "ID";
                    ddlSHopExpand.DataBind();
                    ddlSHopExpand.Items.Insert(0, new ListItem("-Select shop-", "0"));
                }
            }
        }
        private void bindshop1()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblshop where status='Free'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlMergedShop.DataSource = dt;
                    ddlMergedShop.DataTextField = "shopno";
                    ddlMergedShop.DataValueField = "ID";
                    ddlMergedShop.DataBind();
                    ddlMergedShop.Items.Insert(0, new ListItem("-Select shop-", "0"));
                }
            }
        }
        private void bindshop()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblshop where status='Free'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlShops.DataSource = dt;
                    ddlShops.DataTextField = "shopno";
                    ddlShops.DataValueField = "ID";
                    ddlShops.DataBind();
                    ddlShops.Items.Insert(0, new ListItem("-Select shop-", "0"));
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
                    SqlCommand cmd22 = new SqlCommand("select * from tblCustomers where FllName LIKE '" + PID + "%'", con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd22))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            SqlDataReader reader22 = cmd22.ExecuteReader();
                            if (reader22.Read())
                            {
                                string pstatus;
                                //Shop Details
                                pstatus = reader22["FllName"].ToString(); reader22.Close();
                                Response.Redirect("CustomerDetails.aspx?ref2=" + pstatus);
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
        private void bindimage2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            str = "select * from tblCustomerProfileImage where customer='" + PID + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            if (i != 0)
            {
                profilediv.Visible = true;
                defaultprofile.Visible = false;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                con.Close();
            }
            else
            {
                defaultprofile.Visible = true;
                profilediv.Visible = false;
            }


        }
        protected string GetActiveClass(int ItemIndex)
        {
            if (ItemIndex == 0)
            {
                return "active";
            }
            else
            {
                return "";
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
            if (i > 0)
            {
                maind.Visible = false;
                Repeater4.DataSource = dt;
                Repeater4.DataBind();
                con.Close();
            }
            else
            {
                maind.Visible = true;
                delinquecyDiv.Visible = false;
            }
        }
        private void bindimage()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            str = "select * from tblCustomerIdImage where customer='" + PID + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            if (i != 0)
            {
                main1.Visible = false;
                prfdiv.Visible = true;
            }
            else
            {
                main1.Visible = true;
                prfdiv.Visible = false;
            }
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
            con.Close();
        }
        protected void Button2_Click1(object sender, EventArgs e)
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                SqlCommand cmd246 = new SqlCommand("select*from tblCustomerProfileImage where  customer='" + PID + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd246);
                DataTable dt = new DataTable(); sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    if (FileUpload1.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/custProfile/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(SavePath + "\\" + FileUpload1.FileName + Extention);

                        SqlCommand cmd3 = new SqlCommand("insert into tblCustomerProfileImage values('" + PID + "','" + FileUpload1.FileName + "','" + Extention + "')", con);
                        cmd3.ExecuteNonQuery();
                    }
                }
                else
                {
                    if (FileUpload1.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/custProfile/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(SavePath + "\\" + FileUpload1.FileName + Extention);
                        SqlCommand cmd3 = new SqlCommand("update tblCustomerProfileImage set filename='" + FileUpload1.FileName + "',extension='" + Extention + "'", con);
                        cmd3.ExecuteNonQuery();

                    }
                }
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
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

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') >30 and DATEDIFF(day, date, '" + daysLeft + "')<61  and customer='" + PID + "' and balance > 0", con);
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
                        Aged60.InnerText = (Convert.ToDouble(ah) * 0.1).ToString("#,##0.00");
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

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') < 31 and customer='" + PID + "' and balance > 0", con);
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
                                one.InnerText = Convert.ToDouble(ah).ToString("#,##0.00");
                                Aged30.InnerText = (Convert.ToDouble(ah) * 0.04).ToString("#,##0.00");
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
        private void agreementdate()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select*from tblCustomers  where FllName='" + PID + "' ", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string pp;
                    string agreementdate;
                    agreementdate = reader["agreementdate"].ToString();
                    if (agreementdate == "" || agreementdate == null || agreementdate == "1/1/1900 12:00:00 AM")
                    {
                        agreemd.InnerText = "No Data.";
                    }
                    else
                    {
                        agreemd.InnerText = Convert.ToDateTime(agreementdate).ToString("MMMM dd, yyyy");
                    }


                    pp = reader["PaymentDuePeriod"].ToString();
                    termsda.InnerText = pp;
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

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') > 60 and customer='" + PID + "' and balance > 0", con);
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
                                Aged90.InnerText = (Convert.ToDouble(ah) * 0.2).ToString("#,##0.00");
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

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') > 90", con);
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
                                AgedGr90.InnerText = (Convert.ToDouble(ah) * 0.4).ToString("#,##0.00");
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
        private void BindNotDue()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();

                DateTime daysLeft = DateTime.Now.Date;

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') < 1", con);
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
                                NotDue.InnerText = (Convert.ToDouble(ah) * 0.02).ToString("#,##0.00");
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
        private void BindWriteOff()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();

                DateTime daysLeft = DateTime.Now.Date;

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') >= 260", con);
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
                                TotalWriteOff.InnerText = Convert.ToDouble(ah).ToString("#,##0.00");
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
        private void bindoverpayment()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcn = new SqlCommand("select sum(InvAmount) as inv, sum(Payment) as payment from tblCustomerStatement where customer='" + PID + "'", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
                if (nb == 0)
                {

                }
                else
                {
                    double balance = Convert.ToDouble(dtcn.Rows[0][0].ToString()) - Convert.ToDouble(dtcn.Rows[0][1].ToString());
                    if(balance < 0) { balance = -balance; }
                    OverPayAmount.InnerText = (balance).ToString("#,##0.00");
                }
            }
        }
        private void ShowData()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select DATENAME(MONTH,DATEADD(MONTH,month(Date),-1)) as month_name, month(Date) as month_number ,sum (Payment) as Total  from tblCustomerStatement  where YEAR(Date)='" + DateTime.Now.Year + "' and  customer='" + PID + "'  group by   month(Date) order by  month_number";
            SqlCommand cmd = new SqlCommand(query, con);
            using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
            {
                DataTable dtBrands232c3 = new DataTable();
                sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                if (i2c3 != 0)
                {
                    String chart = "";
                    chart = "<canvas id=\"line-chart\" width=\"100%\" height=\"180\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"line-chart\"), { type: 'bar', data: {labels: [";

                    // more detais

                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                        chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";



                    chart += "],datasets: [{ data: [";

                    // get data from database and add to chart
                    String value = "";
                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                        value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000).ToString() + ",";
                    value = value.Substring(0, value.Length - 1);
                    chart += value;

                    chart += "],label: \"Revenue\",display: false,borderColor: \"#ff6a00\",backgroundColor: \"#ff6a00\",hoverBackgroundColor: \"#ff6a00\"},"; // Chart color
                    chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                    chart += "});";

                    chart += "</script>";

                    ltChart.Text = chart; main.Visible = false;
                }
                else { main.Visible = true; ltChart.Visible = false; }
            }

        }
        private void bindgurantor()
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
                        string gfull; string gaddress; string gcontact;

                        //Shop Details
                        gfull = reader["gurentor"].ToString();
                        gaddress = reader["address"].ToString();
                        gcontact = reader["contact"].ToString();

                        Gurantorfullname.InnerText = gfull;
                        GurantorAddress.InnerText = gaddress;
                        GurantorMobile.InnerText = gcontact;
                    }
                }
            }

        }
        protected void BindBrandsRptr3()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                A9.HRef = "TenantPIR.aspx?ref2=" + PID;
                A13.HRef = "NoticeLetter.aspx?ref2=" + PID;
                A18.HRef = "CreditStatement.aspx?ref2=" + PID;
                A20.HRef = "CreditStatement.aspx?ref2=" + PID;
                A19.HRef = "CustomerWelcomeLetter.aspx?ref2=" + PID;
                txtCustomerName.Text = PID;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd22 = new SqlCommand("select * from UsersCust where Name='" + PID + "'", con);
                    SqlDataReader reader22 = cmd22.ExecuteReader();
                    if (reader22.Read())
                    {
                        string pstatus;
                        //Shop Details
                        pstatus = reader22["status"].ToString(); reader22.Close();
                        SqlCommand cmd2 = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                        SqlDataReader reader = cmd2.ExecuteReader();
                        if (reader.Read())
                        {
                            string kc; string kc7;
                            string kc3; string kc4; string kc5; string kc6;
                            string kc8; string kc10; string kc11; string kc12;
                            //Shop Details
                            kc = reader["contigency"].ToString();
                            if (kc == "" || kc == null)
                            {

                            }
                            else
                            {
                                Span3.InnerText = Convert.ToDouble(kc).ToString("#,##0.00");
                            }


                            string shop; string locat; string price; string status; string area;
                            shop = reader["shop"].ToString(); shopno.InnerText = shop;
                            locat = reader["location"].ToString(); location.InnerText = locat;
                            price = reader["price"].ToString(); Rate.InnerText = Convert.ToDouble(price).ToString("#,##0.00");
                            status = reader["Status"].ToString();
                            area = reader["area"].ToString(); Area1.InnerText = Convert.ToDouble(area).ToString("#,##0.00");
                            if (status == "Active")
                            {
                                status1.InnerText = status; status1.Attributes.Add("class", "text-success");
                            }
                            else
                            {
                                status1.InnerText = status; status1.Attributes.Add("class", "text-danger");
                            }
                            string servicecharge = reader["servicesharge"].ToString();
                            if (servicecharge == "" || servicecharge == null)
                            {
                                Span4.InnerText = "0.00";
                            }
                            else
                            {
                                Span4.InnerText = Convert.ToDouble(servicecharge).ToString("#,##0.00");
                            }
                            string agreementdate = reader["agreementdate"].ToString();
                            DateTime today1 = DateTime.Now.Date;
                            DateTime duedate1 = Convert.ToDateTime(agreementdate);
                            TimeSpan t1 = duedate1 - today1; string dayleft1 = t1.TotalDays.ToString();
                            if (Convert.ToInt32(dayleft1) <= 15 || Convert.ToInt32(dayleft1) > 0)
                            {
                                agredate.InnerText = dayleft1 + " " + "Days" + " Remains";
                                agredate.Attributes.Add("class", "small text-success border-bottom");

                            }
                            if (Convert.ToInt32(dayleft1) < 0)
                            {
                                int d = -Convert.ToInt32(dayleft1);
                                agredate.InnerText = d + " Days" + " Passed";
                                agredate.Attributes.Add("class", "small  text-danger border-bottom");
                            }
                            kc3 = reader["CompanyName"].ToString();
                            kc4 = reader["CustomerEmail"].ToString(); kc5 = reader["Website"].ToString();
                            kc6 = reader["Mobile"].ToString(); kc7 = reader["WorkPhone"].ToString();
                            kc8 = reader["CreditLimit"].ToString(); kc10 = reader["CreditLimit"].ToString();
                            kc11 = reader["ContactPerson"].ToString(); kc12 = reader["PaymentDuePeriod"].ToString();
                            /*
                             Bind customer information to the textbox
                             */
                            //
                            txtEmail.Text = kc4; txtTenantComoany.Text = kc3;
                            txtAddress.Text = reader["addresscust"].ToString();
                            CustAddressIcon.InnerText = reader["addresscust"].ToString();
                            txtBusinessType.Text = reader["BuissinessType"].ToString();
                            BusType.InnerText = reader["BuissinessType"].ToString();
                            txtContigency.Text = reader["contigency"].ToString();
                            txtEmail.Text = kc4;
                            txtTenantWebsites.Text = kc5;
                            txtDateofJoiningUpdate.Text = reader["joiningdate"].ToString();
                            TINNumber.InnerText = reader["TIN"].ToString();
                            txtTINNumber.Text = reader["TIN"].ToString();
                            txtVatRegNumber.Text = reader["vatregnumber"].ToString();
                            //
                            Span1.InnerText = kc3; Span2.InnerText = Convert.ToDouble(kc10).ToString("#,##0.00");
                            Credit.InnerText = Convert.ToDouble(kc8).ToString("#,##0.00");
                            email.InnerText = kc4; ; mobile.InnerText = kc6;
                            Span.InnerText = PID; work.InnerText = kc7;
                            L.HRef = "http://" + kc5; L.InnerText = kc5;
                            CustLink.HRef = "CustomerStatement.aspx?ref=" + PID;

                            A2.HRef = "cashpay.aspx?cust=" + PID;
                            A3.HRef = "markasreceivable.aspx?cust=" + PID;

                            reader.Close();
                            con.Close();
                        }
                    }
                }
            }
        }
        private void BindMainCategory12()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where AccountType='Cash'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    DropDownList1.DataSource = dt;
                    DropDownList1.DataTextField = "Name";
                    DropDownList1.DataValueField = "ACT";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                double adjsusted = Convert.ToDouble(txtcreditlimit.Text);
                SqlCommand cmd = new SqlCommand("Update tblCustomers set  CreditLimit='" + adjsusted + "' where FllName='" + PID + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        protected void BindDueAmount()
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
        protected void BindBrandsRptr4()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where customer='" + PID + "' and balance>0", con);

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
                                    Span9.InnerText = "0.00";
                                }
                                else
                                {
                                    Span9.InnerText = "ETB " + Convert.ToDouble(kc).ToString("#,##0.00");
                                    CA.InnerText = Convert.ToDouble(kc).ToString("#,##0.00");
                                }

                                reader.Close();
                                con.Close();
                            }
                        }
                        else
                        {
                            Span9.InnerText = "No Transaction";
                        }
                    }
                }
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Status from tblCustomers where FllName='" + PID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company;
                    company = reader["Status"].ToString();
                    reader.Close();
                    if (company == "Active")
                    {
                        SqlCommand cmdcl = new SqlCommand("Update tblCustomers set  Status='Terminated' where FllName='" + PID + "'", con);
                        cmdcl.ExecuteNonQuery();
                        SqlCommand cmd1cl = new SqlCommand("Update UsersCust set  status='Inactive' where Name='" + PID + "'", con);
                        cmd1cl.ExecuteNonQuery();
                        SqlCommand cmdcl1 = new SqlCommand("Update tblrent set  status='Terminated' where customer='" + PID + "'", con);
                        cmdcl1.ExecuteNonQuery();
                        SqlCommand cmdcl1c = new SqlCommand("Update tblshop set  status='Free' where shopno='" + shopno.InnerText + "'", con);
                        cmdcl1c.ExecuteNonQuery();
                        Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                    }
                    else
                    {
                        SqlCommand cmd2AC = new SqlCommand("select * from tblshop where shopno='" + shopno.InnerText + "'", con);
                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                        if (readerAC.Read())
                        {
                            String location = readerAC["location"].ToString();
                            String area = readerAC["area"].ToString();
                            String price = readerAC["monthlyprice"].ToString();
                            String status = readerAC["status"].ToString();

                            readerAC.Close();
                            if (status == "Occupied")
                            {
                                lblMsg.Text = "The customer shop are aleardy occupied;"; lblMsg.ForeColor = Color.Red;
                            }
                            else
                            {
                                SqlCommand cmdcl = new SqlCommand("Update tblCustomers set  status='Active'  where FllName='" + PID + "'", con);
                                cmdcl.ExecuteNonQuery();
                                SqlCommand cmd1cl = new SqlCommand("Update UsersCust set  status='Active' where Name='" + PID + "'", con);
                                cmd1cl.ExecuteNonQuery();
                                SqlCommand cmdcl1 = new SqlCommand("Update tblrent set  status='Active' where customer='" + PID + "'", con);
                                cmdcl1.ExecuteNonQuery();
                                SqlCommand cmdcl1c = new SqlCommand("Update tblshop set  status='Occupied' where shopno='" + shopno.InnerText + "'", con);
                                cmdcl1c.ExecuteNonQuery();
                                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                            }
                        }
                    }
                }

            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcl = new SqlCommand("Update tblCustomers set  Mobile='" + txtMob.Text + "'  where FllName='" + PID + "'", con);
                cmdcl.ExecuteNonQuery();
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        protected void btncontig_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd190c = new SqlCommand("select * from tblGeneralLedger2 where Account='" + DropDownList1.SelectedItem.Text + "'", con);
                using (SqlDataAdapter sda22 = new SqlDataAdapter(cmd190c))
                {
                    DataTable dtBrands232 = new DataTable();
                    sda22.Fill(dtBrands232); int i2 = dtBrands232.Rows.Count;
                    //
                    if (i2 != 0)
                    {

                        SqlDataReader reader66790 = cmd190c.ExecuteReader();

                        if (reader66790.Read())
                        {
                            string ah1289;
                            ah1289 = reader66790["Balance"].ToString();
                            reader66790.Close();
                            con.Close();
                            con.Open();
                            SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + DropDownList1.SelectedItem.Text + "'", con);

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
                                Double bl1 = M1 + (Convert.ToDouble(Span3.InnerText));
                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='0', Debit='" + Span3.InnerText + "', Explanation='Contigency from customer', Date='" + DateTime.Now.Date + "' where Account='" + DropDownList1.SelectedItem.Text + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "'+'-'+'Contigency','','" + Span3.InnerText + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + DropDownList1.SelectedItem.Text + "','" + ah11 + "','" + ah1258 + "')", con);
                                cmd1974.ExecuteNonQuery();
                                SqlCommand cmd5 = new SqlCommand("Update tblCustomers set contigency='" + 0.ToString() + "' where FllName='" + PID + "'", con);
                                cmd5.ExecuteNonQuery();
                                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                            }

                        }

                    }
                }
            }
        }
        protected void contreturn_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd5 = new SqlCommand("Update tblCustomers set contigency='" + 0.ToString() + "' where FllName='" + PID + "'", con);
                cmd5.ExecuteNonQuery();
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        private void bindduedate()
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
                    string duedates = reader["duedate"].ToString(); reader.Close();
                    Span5.InnerText = Convert.ToDateTime(duedates).ToString("MMMM dd, yyyy");
                    DateTime today = DateTime.Now.Date;
                    DateTime duedate = Convert.ToDateTime(duedates);
                    TimeSpan t = duedate - today;
                    string dayleft = t.TotalDays.ToString();

                    if (Convert.ToInt32(dayleft) <= 15 || Convert.ToInt32(dayleft) > 0)
                    {
                        duedate2.InnerText = "Due Date, " + dayleft + " " + "Days" + " Remains";
                        duedate2.Attributes.Add("class", "small text-success border-bottom mx-2");

                    }
                    if (Convert.ToInt32(dayleft) < 0)
                    {
                        int d = -Convert.ToInt32(dayleft);
                        duedate2.InnerText = "Due Date, " + d + " Days" + " Passed";
                        duedate2.Attributes.Add("class", "small  text-danger border-bottom mx-2");
                    }
                }
            }
        }
        protected void btnBillingTerms_Click(object sender, EventArgs e)
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
                    string duedates = reader["duedate"].ToString(); reader.Close();
                    SqlCommand cmd5 = new SqlCommand("Update tblCustomers set PaymentDuePeriod='" + ddlBillingTerms.SelectedItem.Text + "' where FllName='" + PID + "'", con);
                    cmd5.ExecuteNonQuery();
                    if (ddlBillingTerms.SelectedItem.Text == "Monthly")
                    {
                        if (Span4.InnerText == "0.00")
                        {
                            double total = Convert.ToDouble(Rate.InnerText) * 0.15 + Convert.ToDouble(Rate.InnerText);
                            SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "' where customer='" + PID + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                        else
                        {
                            double total = Convert.ToDouble(Rate.InnerText) * 0.15 + Convert.ToDouble(Rate.InnerText) + Convert.ToDouble(Span4.InnerText);
                            SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "' where customer='" + PID + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                    }
                    else if (ddlBillingTerms.SelectedItem.Text == "Every Three Month")
                    {
                        if (Span4.InnerText == "0.00")
                        {
                            double total = Convert.ToDouble(Rate.InnerText) * 3 * 0.15 + Convert.ToDouble(Rate.InnerText) * 3;
                            SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "' where customer='" + PID + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                        else
                        {
                            double total = Convert.ToDouble(Rate.InnerText) * 3 * 0.15 + Convert.ToDouble(Rate.InnerText) * 3 + Convert.ToDouble(Span4.InnerText) * 3;
                            SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "' where customer='" + PID + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                    }
                    else if (ddlBillingTerms.SelectedItem.Text == "Every Six Month")
                    {
                        if (Span4.InnerText == "0.00")
                        {
                            double total = Convert.ToDouble(Rate.InnerText) * 6 * 0.15 + Convert.ToDouble(Rate.InnerText) * 6;
                            SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "' where customer='" + PID + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                        else
                        {
                            double total = Convert.ToDouble(Rate.InnerText) * 6 * 0.15 + Convert.ToDouble(Rate.InnerText) * 6 + Convert.ToDouble(Span4.InnerText) * 6;
                            SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "' where customer='" + PID + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        if (Span4.InnerText == "0.00")
                        {
                            double total = Convert.ToDouble(Rate.InnerText) * 12 * 0.15 + Convert.ToDouble(Rate.InnerText) * 12;
                            SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "' where customer='" + PID + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                        else
                        {
                            double total = Convert.ToDouble(Rate.InnerText) * 12 * 0.15 + Convert.ToDouble(Rate.InnerText) * 12 + Convert.ToDouble(Span4.InnerText) * 12;
                            SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "' where customer='" + PID + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                    }
                    if (Checkbox12.Checked == true)
                    {
                        string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                        string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                        TwilioClient.Init(accountSid, authToken);

                        var message = MessageResource.Create(
                            body: "Dear customer, your billing terms updated to " + ddlBillingTerms.SelectedItem.Text,
                            from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                            to: new Twilio.Types.PhoneNumber("+251" + mobile.InnerText)
                        );
                    }
                    string date2 = "Billing Terms Update for " + ddlBillingTerms.SelectedItem.Text + "  Your payment will be accordigly";
                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + date2 + "','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                    cmd197h.ExecuteNonQuery();
                    Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                }
            }
        }
        protected void btnAgreement_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd5 = new SqlCommand("Update tblCustomers set agreementdate='" + txtAgreementDate.Text + "' where FllName='" + PID + "'", con);
                cmd5.ExecuteNonQuery();
                string date2 = "Agreement renewed for the end of " + Convert.ToDateTime(txtAgreementDate.Text).ToString("MMMM dd, yyyy");
                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + date2 + "','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                cmd197h.ExecuteNonQuery();
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdre = new SqlCommand("Update tblrent set duedate='" + txtDueDateUpdate.Text + "' where customer='" + PID + "'", con);
                cmdre.ExecuteNonQuery();
                SqlCommand cmdre1 = new SqlCommand("Update tblCustomers set duedate='" + txtDueDateUpdate.Text + "' where FllName='" + PID + "'", con);
                cmdre1.ExecuteNonQuery();
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        protected void Button14_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + txtMessage.Text + "','" + PID + "','" + PID + "','Unseen','fas fa-info text-white','icon-circle bg bg-info','','CUST')", con);
                cmd197hb.ExecuteNonQuery();
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        protected void Button15_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                SqlCommand cmd246 = new SqlCommand("select*from tblCustomerIdImage where  customer='" + PID + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd246);
                DataTable dt = new DataTable(); sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    if (FileUpload2.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/custID/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload2.PostedFile.FileName);
                        FileUpload2.SaveAs(SavePath + "\\" + FileUpload2.FileName + Extention);

                        SqlCommand cmd3 = new SqlCommand("insert into tblCustomerIdImage values('" + PID + "','" + FileUpload2.FileName + "','" + Extention + "')", con);
                        cmd3.ExecuteNonQuery();
                    }
                    if (FileUpload3.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/custID/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload3.PostedFile.FileName);
                        FileUpload3.SaveAs(SavePath + "\\" + FileUpload3.FileName + Extention);

                        SqlCommand cmd3 = new SqlCommand("insert into tblCustomerIdImage values('" + PID + "','" + FileUpload3.FileName + "','" + Extention + "')", con);
                        cmd3.ExecuteNonQuery();
                    }
                    if (FileUpload4.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/custID/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload4.PostedFile.FileName);
                        FileUpload4.SaveAs(SavePath + "\\" + FileUpload4.FileName + Extention);

                        SqlCommand cmd3 = new SqlCommand("insert into tblCustomerIdImage values('" + PID + "','" + FileUpload4.FileName + "','" + Extention + "')", con);
                        cmd3.ExecuteNonQuery();
                    }
                }
                else
                {
                    if (FileUpload2.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/custID/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload2.PostedFile.FileName);
                        FileUpload2.SaveAs(SavePath + "\\" + FileUpload2.FileName + Extention);

                        SqlCommand cmd3 = new SqlCommand("update tblCustomerIdImage set filename='" + FileUpload2.FileName + "',extension='" + Extention + "' where customer='" + PID + "' and id='" + dt.Rows[0][0].ToString() + "'", con);
                        cmd3.ExecuteNonQuery();
                    }
                    if (FileUpload3.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/custID/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload3.PostedFile.FileName);
                        FileUpload3.SaveAs(SavePath + "\\" + FileUpload3.FileName + Extention);

                        SqlCommand cmd3 = new SqlCommand("update tblCustomerIdImage set filename='" + FileUpload3.FileName + "',extension='" + Extention + "'  where customer='" + PID + "'  and id='" + dt.Rows[1][0].ToString() + "'", con);
                        cmd3.ExecuteNonQuery();
                    }
                    if (FileUpload4.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/custID/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload4.PostedFile.FileName);
                        FileUpload4.SaveAs(SavePath + "\\" + FileUpload4.FileName + Extention);

                        SqlCommand cmd3 = new SqlCommand("update tblCustomerIdImage set filename='" + FileUpload4.FileName + "',extension='" + Extention + "'  where customer='" + PID + "'  and id='" + dt.Rows[2][0].ToString() + "'", con);
                        cmd3.ExecuteNonQuery();
                    }
                }
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        protected void Button16_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdre1 = new SqlCommand("Update tblCustomers set gurentor='" + txtGFullName.Text + "',address='" + txtGAdress.Text + "',contact='" + txtContact.Text + "' where FllName='" + PID + "'", con);
                cmdre1.ExecuteNonQuery();
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        protected void Button17_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (txtDelinquency.Text == "" || txtDelinquency.Text == null)
                {
                    string message = "Please put your deinquency on the field!!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
                else
                {
                    SqlCommand cmd197hb = new SqlCommand("insert into tblCustomerDelinquency values('" + PID + "',N'" + txtDelinquency.Text + "','" + DateTime.Now.Date + "')", con);
                    cmd197hb.ExecuteNonQuery();
                    Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                }
            }
        }
        private void bind_customer_amharic_name()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();
                SqlCommand cmdre1 = new SqlCommand("Update tblrent set customer_amharic=N'" + txtAmharic_name.Text + "',buisnesstype='" + txtBusinessType.Text + "',customer='" + txtCustomerName.Text + "'  where customer='" + PID + "'", con);
                cmdre1.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand("Update tblCustomerStatement set Customer='" + txtCustomerName.Text + "'  where Customer='" + PID + "'", con);
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("Update tblcreditnote set customer='" + txtCustomerName.Text + "'  where customer='" + PID + "'", con);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("Update tblrentreceipt set customer='" + txtCustomerName.Text + "'  where customer='" + PID + "'", con);
                cmd3.ExecuteNonQuery();
                SqlCommand cmduser = new SqlCommand("Update UsersCust set Name='" + txtCustomerName.Text + "'  where Name='" + PID + "'", con);
                cmduser.ExecuteNonQuery();
            }
        }
        private void bind_customer_amharic_name_null()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            string name = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    name += reader["customer_amharic"].ToString(); reader.Close();
                    if (name != "" || name != null)
                    {
                        txtAmharic_name.Text = name;
                    }
                    else
                    {

                    }
                }
            }
        }
        protected void Button18_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdre1 = new SqlCommand("Update tblCustomers set CustomerEmail='" + txtEmail.Text + "',CompanyName='" + txtTenantComoany.Text + "',Website='" + txtTenantWebsites.Text + "',BuissinessType='" + txtBusinessType.Text + "',contigency='" + txtContigency.Text + "',joiningdate='" + txtDateofJoiningUpdate.Text + "',addresscust='" + txtAddress.Text + "',TIN='" + txtTINNumber.Text + "',FllName='" + txtCustomerName.Text + "',vatregnumber='" + txtVatRegNumber.Text+"' where FllName='" + PID + "'", con);
                cmdre1.ExecuteNonQuery();
                bind_customer_amharic_name();
                Response.Redirect("CustomerDetails.aspx?ref2=" + txtCustomerName.Text);
            }
        }
        protected void btnTransferSHOP_Click(object sender, EventArgs e)
        {
            if (ddlShops.Items.Count == 0)
            {
                lblMsg.Text = "All shops are ocuupied, please free one."; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                if (ddlShops.SelectedItem.Text == "-Select-")
                {
                    lblMsg.Text = "Please select shop."; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    String PID = Convert.ToString(Request.QueryString["ref2"]);
                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmdshop = new SqlCommand("select * from tblshop where shopno='" + ddlShops.SelectedItem.Text + "'", con);
                        SqlDataReader readershop = cmdshop.ExecuteReader();

                        if (readershop.Read())
                        {
                            String location = readershop["location"].ToString();
                            String area = readershop["area"].ToString();
                            String price = readershop["monthlyprice"].ToString();
                            String status = readershop["status"].ToString();
                            readershop.Close();
                            SqlCommand cmdcust = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                            SqlDataReader readerscust = cmdcust.ExecuteReader();
                            if (readerscust.Read())
                            {
                                double total;
                                String pp = readerscust["PaymentDuePeriod"].ToString();
                                String SC = readerscust["servicesharge"].ToString(); readerscust.Close();
                                //Calculate total due amount of the current shops
                                if (pp == "Monthly")
                                {
                                    total = Convert.ToDouble(SC) + Convert.ToDouble(price) + Convert.ToDouble(SC) * 0.15 + Convert.ToDouble(price) * 0.15;
                                }
                                else if (pp == "Every Three Month")
                                {
                                    total = Convert.ToDouble(SC) * 3 + Convert.ToDouble(price) * 3 + Convert.ToDouble(SC) * 3 * 0.15 + Convert.ToDouble(price) * 3 * 0.15;
                                }
                                else
                                {
                                    total = Convert.ToDouble(SC) * 12 + Convert.ToDouble(price) * 12 + Convert.ToDouble(SC) * 12 * 0.15 + Convert.ToDouble(price) * 12 * 0.15;
                                }

                                //Update Customer table and rent table
                                SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "', shopno='" + ddlShops.SelectedItem.Text + "', area='" + area + "', price='" + price + "' where customer='" + PID + "'", con);
                                cmdre.ExecuteNonQuery();
                                SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set shop='" + ddlShops.SelectedItem.Text + "', location='" + location + "', area='" + area + "', price='" + price + "' where FllName='" + PID + "'", con);
                                cmdre2.ExecuteNonQuery();
                                //Updating the shop table to be occupied
                                SqlCommand cmd455 = new SqlCommand("Update tblshop set status='Free' where shopno='" + shopno.InnerText + "'", con);
                                cmd455.ExecuteNonQuery();
                                SqlCommand cmd4551 = new SqlCommand("Update tblshop set status='Occupied' where shopno='" + ddlShops.SelectedItem.Text + "'", con);
                                cmd4551.ExecuteNonQuery();
                                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                            }
                        }
                    }
                }
            }
        }
        protected void btnMiscCost_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                if (readerbcustb.Read())
                {
                    string ah11;

                    ah11 = readerbcustb["Balance"].ToString();
                    if (txtCost.Text == "" || txtCost.Text == null)
                    {
                        string message = "Please put your credit on the field!!";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                    }
                    else
                    {
                        double total = Convert.ToDouble(txtCost.Text);
                        double remain = total + Convert.ToDouble(ah11);
                        readerbcustb.Close();
                        SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + txtCost.Text + "','" + txtCost.Text + "','" + txtCostRemark.Text + "','" + DateTime.Now.AddDays(30) + "','" + txtCostRemark.Text + "')", con);
                        cmdcrn.ExecuteNonQuery();
                        //
                        if (bindCustomerStatement() > 0)
                        {

                        }
                        else
                        {
                            SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtCostRemark.Text + "','','" + total + "','','" + remain + "','" + PID + "')", con);
                            custcmd.ExecuteNonQuery();
                        }
                        //////////////////////////////////////
                        if (Checkbox2.Checked == true)
                        {
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
                                        double unpaid = total;
                                        Double bl1 = M1 + unpaid;
                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                        cmd45.ExecuteNonQuery();
                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + unpaid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                        cmd1974.ExecuteNonQuery();
                                    }
                                }
                            }

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

                                                string ah1258;

                                                ah1258 = reader66["AccountType"].ToString();
                                                reader66.Close();
                                                con.Close();
                                                con.Open();
                                                Double M1 = Convert.ToDouble(ah1289);
                                                double income = total;
                                                Double bl1 = income + M1;
                                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                cmd45.ExecuteNonQuery();
                                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','','" + ah1258 + "')", con);
                                                cmd1974.ExecuteNonQuery();

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                    }
                }
            }
        }
        protected void btnUpdateServiceCharge_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Update tblCustomers set  servicesharge='" + txtServiceCharge.Text + "' where FllName='" + PID + "'", con);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd4 = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd4);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                double total = Convert.ToDouble(dt.Rows[0][16].ToString());
                if (dt.Rows[0][10].ToString() == "Every Three Month")
                {
                    double pricevat = 3 * total + 3 * Convert.ToDouble(txtServiceCharge.Text) + 3 * total * 0.15;
                    SqlCommand cmdre = new SqlCommand("Update tblrent set  servicecharge='" + Convert.ToDouble(txtServiceCharge.Text) + "',currentperiodue='" + pricevat + "' where customer='" + PID + "'", con);
                    cmdre.ExecuteNonQuery();
                }
                else if (dt.Rows[0][10].ToString() == "Monthly")
                {
                    double pricevat = total + Convert.ToDouble(txtServiceCharge.Text) + total * 0.15;
                    SqlCommand cmdre = new SqlCommand("Update tblrent set  servicecharge='" + Convert.ToDouble(txtServiceCharge.Text) + "',currentperiodue='" + pricevat + "' where customer='" + PID + "'", con);
                    cmdre.ExecuteNonQuery();
                }
                else if (dt.Rows[0][10].ToString() == "Every Six Month")
                {
                    double pricevat = 6 * total + 6 * Convert.ToDouble(txtServiceCharge.Text) + 6 * total * 0.15;
                    SqlCommand cmdre = new SqlCommand("Update tblrent set  servicecharge='" + Convert.ToDouble(txtServiceCharge.Text) + "',currentperiodue='" + pricevat + "' where customer='" + PID + "'", con);
                    cmdre.ExecuteNonQuery();
                }
                else
                {
                    double pricevat = 12 * total + 12 * total * 0.15 + Convert.ToDouble(txtServiceCharge.Text) * 12;
                    SqlCommand cmdre = new SqlCommand("Update tblrent set  servicecharge='" + Convert.ToDouble(txtServiceCharge.Text) + "',currentperiodue='" + pricevat + "' where customer='" + PID + "'", con);
                    cmdre.ExecuteNonQuery();
                }
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
        protected void btnMergeShop_Click(object sender, EventArgs e)
        {
            if (ddlMergedShop.Items.Count == 0)
            {
                lblMsg.Text = "All shops are ocuupied, please free one."; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                if (ddlMergedShop.SelectedItem.Text == "-Select-")
                {
                    lblMsg.Text = "Please select shop."; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    String PID = Convert.ToString(Request.QueryString["ref2"]);
                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmdshop = new SqlCommand("select * from tblshop where shopno='" + shopno.InnerText + "'", con);
                        SqlDataReader readershop = cmdshop.ExecuteReader();

                        if (readershop.Read())
                        {
                            String location = readershop["location"].ToString();
                            String rate = readershop["rate"].ToString();
                            String area = readershop["area"].ToString();
                            double price;
                            String status = readershop["status"].ToString();
                            readershop.Close();
                            SqlCommand cmdshop1 = new SqlCommand("select * from tblshop where shopno='" + ddlMergedShop.SelectedItem.Text + "'", con);
                            SqlDataReader readershop1 = cmdshop1.ExecuteReader();

                            if (readershop1.Read())
                            {

                                String areamerge = readershop1["area"].ToString();
                                readershop1.Close();
                                SqlCommand cmdcust = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                SqlDataReader readerscust = cmdcust.ExecuteReader();
                                if (readerscust.Read())
                                {
                                    double total; double areatotal;
                                    areatotal = Convert.ToDouble(areamerge) + Convert.ToDouble(area);
                                    price = Convert.ToDouble(rate) * areatotal;
                                    String pp = readerscust["PaymentDuePeriod"].ToString();
                                    String SC = readerscust["servicesharge"].ToString(); readerscust.Close();
                                    //Calculate total due amount of the current shops
                                    if (pp == "Monthly")
                                    {
                                        total = Convert.ToDouble(SC) + price + price * 0.15;
                                    }
                                    else if (pp == "Every Three Month")
                                    {
                                        total = Convert.ToDouble(SC) * 3 + price * 3 + price * 3 * 0.15;
                                    }
                                    else
                                    {
                                        total = Convert.ToDouble(SC) * 12 + price * 12 + price * 12 * 0.15;
                                    }

                                    //Update Customer table and rent table
                                    SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "', shopno='" + shopno.InnerText + "', area='" + areatotal + "', price='" + price + "' where customer='" + PID + "'", con);
                                    cmdre.ExecuteNonQuery();
                                    SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set shop='" + shopno.InnerText + "', location='" + location + "', area='" + areatotal + "', price='" + price + "' where FllName='" + PID + "'", con);
                                    cmdre2.ExecuteNonQuery();
                                    //Updating the shop table to be occupied
                                    SqlCommand cmd4551 = new SqlCommand("Update tblshop set area='" + areatotal + "' where shopno='" + shopno.InnerText + "'", con);
                                    cmd4551.ExecuteNonQuery();
                                    SqlCommand cmd455 = new SqlCommand("Update tblshop set status='SUSPENDED',rate='',location='',monthlyprice='',area='' where shopno='" + ddlMergedShop.SelectedItem.Text + "'", con);
                                    cmd455.ExecuteNonQuery();
                                    string action = "Shop " + shopno.InnerText + " merged with shop number " + ddlMergedShop.SelectedItem.Text;
                                    SqlCommand cmdAc = new SqlCommand("insert into tblShopActivity values('" + action + "','" + BindUser() + "',getdate(),'badge badge-warning','SHOP MERGED','" + shopno.InnerText + "')", con);
                                    cmdAc.ExecuteNonQuery();
                                    Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void btnExpandArea_Click(object sender, EventArgs e)
        {
            if (txtAreaExpand.Text == "")
            {
                lblMsg.Text = "Please insert the area"; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmdshop = new SqlCommand("select * from tblshop where shopno='" + shopno.InnerText + "'", con);
                    SqlDataReader readershop = cmdshop.ExecuteReader();

                    if (readershop.Read())
                    {
                        String location = readershop["location"].ToString();
                        String rate = readershop["rate"].ToString();
                        String area = readershop["area"].ToString(); readershop.Close();
                        double newarea = Convert.ToDouble(area) + Convert.ToDouble(txtAreaExpand.Text);
                        if (newarea < 0)
                        {
                            lblMsg.Text = "New area can not be negative."; lblMsg.ForeColor = Color.Red;
                        }
                        else
                        {
                            SqlCommand cmdcust = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                            SqlDataReader readerscust = cmdcust.ExecuteReader();
                            if (readerscust.Read())
                            {
                                double total, total2; double price;
                                price = Convert.ToDouble(rate) * newarea;
                                String pp = readerscust["PaymentDuePeriod"].ToString();
                                String SC = readerscust["servicesharge"].ToString(); readerscust.Close();
                                //Calculate total due amount of the current shops
                                if (pp == "Monthly")
                                {
                                    total = Convert.ToDouble(SC) + price + price * 0.15;

                                }
                                else if (pp == "Every Three Month")
                                {
                                    total = Convert.ToDouble(SC) * 3 + price * 3 + price * 3 * 0.15;

                                }
                                else
                                {
                                    total = Convert.ToDouble(SC) * 12 + price * 12 + price * 12 * 0.15;

                                }
                                //Update Customer table and rent table
                                SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "', shopno='" + shopno.InnerText + "', area='" + newarea + "', price='" + price + "' where customer='" + PID + "'", con);
                                cmdre.ExecuteNonQuery();
                                SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set shop='" + shopno.InnerText + "', location='" + location + "', area='" + newarea + "', price='" + price + "' where FllName='" + PID + "'", con);
                                cmdre2.ExecuteNonQuery();
                                //Updating the shop table to be occupied
                                SqlCommand cmd4551 = new SqlCommand("Update tblshop set area='" + newarea + "',monthlyprice='" + price + "' where shopno='" + shopno.InnerText + "'", con);
                                cmd4551.ExecuteNonQuery();
                                string action1 = "Shop area get updated from " + Area1.InnerText + " square meter to " + newarea;
                                SqlCommand cmdAc1 = new SqlCommand("insert into tblShopActivity values('" + action1 + "','" + BindUser() + "',getdate(),'badge badge-warning','SHOP AREA UPDATED','" + shopno.InnerText + "')", con);
                                cmdAc1.ExecuteNonQuery();
                                if (ddlSHopExpand.SelectedItem.Text != "-Select shop-")
                                {
                                    double newarea2;
                                    SqlCommand cmdshop1 = new SqlCommand("select * from tblshop where shopno='" + ddlSHopExpand.SelectedItem.Text + "'", con);
                                    SqlDataReader readershop1 = cmdshop1.ExecuteReader();

                                    if (readershop1.Read())
                                    {

                                        String status = readershop1["status"].ToString();
                                        String area2 = readershop1["area"].ToString();
                                        String rate2 = readershop1["rate"].ToString();
                                        newarea2 = Convert.ToDouble(area2) - Convert.ToDouble(txtAreaExpand.Text);
                                        double price2 = Convert.ToDouble(rate2) * newarea2;
                                        readershop1.Close();
                                        if (pp == "Monthly")
                                        {
                                            total = Convert.ToDouble(SC) + price + price * 0.15;
                                            total2 = Convert.ToDouble(SC) + price2 + price2 * 0.15;
                                        }
                                        else if (pp == "Every Three Month")
                                        {
                                            total = Convert.ToDouble(SC) * 3 + price * 3 + price * 3 * 0.15;
                                            total2 = Convert.ToDouble(SC) * 3 + price2 * 3 + price2 * 0.15 * 3;
                                        }
                                        else
                                        {
                                            total = Convert.ToDouble(SC) * 12 + price * 12 + price * 12 * 0.15;
                                            total2 = Convert.ToDouble(SC) * 12 + price2 * 12 + price2 * 0.15 * 12;
                                        }
                                        SqlCommand cmdc = new SqlCommand("Update tblshop set area='" + newarea2 + "',monthlyprice='" + price2 + "' where shopno='" + ddlSHopExpand.SelectedItem.Text + "'", con);
                                        cmdc.ExecuteNonQuery();
                                        if (status == "Occupied")
                                        {
                                            SqlCommand cmdreb = new SqlCommand("Update tblrent set currentperiodue='" + total2 + "', shopno='" + ddlSHopExpand.SelectedItem.Text + "', area='" + newarea2 + "', price='" + price2 + "' where shopno='" + ddlSHopExpand.SelectedItem.Text + "'", con);
                                            cmdreb.ExecuteNonQuery();
                                            SqlCommand cmdre2b = new SqlCommand("Update tblCustomers set shop='" + ddlSHopExpand.SelectedItem.Text + "', area='" + newarea2 + "', price='" + price2 + "' where shop='" + ddlSHopExpand.SelectedItem.Text + "'", con);
                                            cmdre2b.ExecuteNonQuery();
                                        }
                                        string action = "Shop area get updated from " + Area1.InnerText + " square meter to " + newarea;
                                        SqlCommand cmdAc = new SqlCommand("insert into tblShopActivity values('" + action + "','" + BindUser() + "',getdate(),'badge badge-warning','SHOP AREA UPDATED','" + shopno.InnerText + "')", con);
                                        cmdAc.ExecuteNonQuery();
                                    }
                                }
                                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                            }
                        }
                    }
                    else
                    {
                        readershop.Close();
                    }
                }
            }
        }
        protected void btnSendAlert_Click(object sender, EventArgs e)
        {
            if (CA.InnerText == "0.00")
            {
                string message = "Customer don't have any credit!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd4 = new SqlCommand("select * from tblCustomers where Status='Active' and FllName='" + PID + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd4);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                    string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                    TwilioClient.Init(accountSid, authToken);

                    var message = MessageResource.Create(
                        body: "Dear customer you have credit amount of ETB " + Convert.ToDouble(CA.InnerText).ToString("#,#0.0") + ". Please pay the credit soon.",
                        from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                        to: new Twilio.Types.PhoneNumber("+251" + dt.Rows[0][6].ToString())
                        );
                }
            }
        }
        private void BindTotalBad()
        {
            double total = Convert.ToDouble(NotDue.InnerText) + Convert.ToDouble(Aged30.InnerText) +
                Convert.ToDouble(Aged60.InnerText) + Convert.ToDouble(Aged90.InnerText) +
                Convert.ToDouble(AgedGr90.InnerText);
            TotalBadEstimated.InnerText = total.ToString("#,##0.00");
        }
        private double bindCustomerStatement()
        {
            double balance_difference = 0;
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand("select Sum(InvAmount) as Total,Sum(Payment) as PAID  from tblCustomerStatement where Customer='" + PID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    double total = 0;
                    double paid_inv = 0;
                    string total_inv; string paid;
                    total_inv = reader["Total"].ToString();
                    paid = reader["PAID"].ToString();
                    reader.Close();
                    if (total_inv != null) { total += Convert.ToDouble(total_inv); }
                    if (paid != null) { paid_inv += Convert.ToDouble(paid); }
                    double receivable = 0;
                    double balance = total - paid_inv;
                    if (Span9.InnerText == "0.00") { }
                    else { receivable += Convert.ToDouble(Span9.InnerText); }
                    balance_difference = balance - receivable;
                }
            }
            return balance_difference;
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
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                datFromc.InnerText = Convert.ToDateTime(txtCHDateFromCash.Text).ToString("MMM dd, yyyy");
                datToc.InnerText = Convert.ToDateTime(txtCHDateToCash.Text).ToString("MMM dd, yyyy");
                datFromc.Visible = true; datToc.Visible = true; tomiddlec.Visible = true;
                String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
                SqlConnection con = new SqlConnection(myConnection);
                String query = "select DATENAME(MONTH,DATEADD(MONTH,month(Date),-1)) as month_name, month(Date) as month_number ,sum (Payment) as Total  from tblCustomerStatement  where Date between '" + txtCHDateFromCash.Text + "' and  '" + txtCHDateToCash.Text + "'  group by   month(Date) order by  month_number";
                SqlCommand cmd = new SqlCommand(query, con);
                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                    if (i2c3 != 0)
                    {
                        String chart = "";
                        chart = "<canvas id=\"line-chart\" width=\"100%\" height=\"210\"></canvas>";
                        chart += "<script>";
                        chart += "new Chart(document.getElementById(\"line-chart\"), { type: 'bar', data: {labels: [";

                        // more detais

                        for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                            chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";



                        chart += "],datasets: [{ data: [";

                        // get data from database and add to chart
                        String value = "";
                        for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                            value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString() + ",";
                        value = value.Substring(0, value.Length - 1);
                        chart += value;

                        chart += "],label: \"Revenue\",display: false,borderColor: \"#ff6a00\",backgroundColor: \"#ff6a00\",hoverBackgroundColor: \"#ff6a00\"},"; // Chart color
                        chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                        chart += "});";

                        chart += "</script>";

                        ltChart.Text = chart; main.Visible = false; ltChart.Visible = true;
                    }
                    else { main.Visible = true; ltChart.Visible = false; }
                }
            }
        }

        protected void btnRemoveCustomer_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                string exlanation = "Customer " + PID + " has been deleted";
                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-trash text-white','icon-circle bg bg-danger','Customer.aspx','MN')", con);
                cmd197h.ExecuteNonQuery();
                SqlCommand cmdAc = new SqlCommand("delete tblCustomerStatement where Customer='" + PID + "'", con);
                cmdAc.ExecuteNonQuery();

                SqlCommand cmdAc1 = new SqlCommand("delete tblrent  where customer='" + PID + "'", con);
                cmdAc1.ExecuteNonQuery();
                if (status1.InnerText == "Active")
                {
                    SqlCommand cmdshop = new SqlCommand("update tblshop set Status='Free'  where shopno='" + shopno.InnerText + "'", con);
                    cmdshop.ExecuteNonQuery();
                }
                SqlCommand cmdAc2 = new SqlCommand("delete tblCustomers  where FllName='" + PID + "'", con);
                cmdAc2.ExecuteNonQuery();

                SqlCommand cmdAcu = new SqlCommand("delete UsersCust  where Name='" + PID + "'", con);
                cmdAcu.ExecuteNonQuery();
                SqlCommand cmdAcu1 = new SqlCommand("delete tblCustomerProfileImage  where customer='" + PID + "'", con);
                cmdAcu1.ExecuteNonQuery();

                Response.Redirect("Customer.aspx");
            }
        }
        protected void btnExchangeShop_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            if (ddlExchangedShop.SelectedItem.Text == "-Select Customer-")
            {
                string message = "Select customer!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string shopno1, area, price, currentdue, monthlyvat;

                        shopno1 = reader["shopno"].ToString();
                        area = reader["area"].ToString();
                        price = reader["price"].ToString();
                        currentdue = reader["currentperiodue"].ToString();
                        monthlyvat = reader["monthlyvat"].ToString(); reader.Close();
                        SqlCommand cmd1 = new SqlCommand("select * from tblrent where customer='" + ddlExchangedShop.SelectedItem.Text + "'", con);
                        SqlDataReader reader1 = cmd1.ExecuteReader();

                        if (reader1.Read())
                        {
                            string shopno11, area1, price1, currentdue1, monthlyvat1;

                            shopno11 = reader1["shopno"].ToString();
                            area1 = reader1["area"].ToString();
                            price1 = reader1["price"].ToString();
                            currentdue1 = reader1["currentperiodue"].ToString();
                            monthlyvat1 = reader1["monthlyvat"].ToString(); reader1.Close();
                            SqlCommand cmd2 = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                            SqlDataReader reader2 = cmd2.ExecuteReader();

                            if (reader2.Read())
                            {
                                string shopno12, area2, price2, location2;

                                shopno12 = reader2["shop"].ToString();
                                area2 = reader2["area"].ToString();
                                price2 = reader2["price"].ToString();
                                location2 = reader2["location"].ToString(); reader2.Close();
                                SqlCommand cmd3 = new SqlCommand("select * from tblCustomers where FllName='" + ddlExchangedShop.SelectedItem.Text + "'", con);
                                SqlDataReader reader3 = cmd3.ExecuteReader();

                                if (reader3.Read())
                                {
                                    string shopno3, area3, price3, location3;

                                    shopno3 = reader3["shop"].ToString();
                                    area3 = reader3["area"].ToString();
                                    price3 = reader3["price"].ToString();
                                    location3 = reader3["location"].ToString(); reader3.Close();

                                    SqlCommand cmdreb1 = new SqlCommand("Update tblCustomers set shop='" + shopno3 + "', area='" + area3 + "', price='" + price3 + "', location='" + location3 + "' where FllName='" + PID + "'", con);
                                    cmdreb1.ExecuteNonQuery();


                                    SqlCommand cmdreb2 = new SqlCommand("Update tblCustomers set shop='" + shopno.InnerText + "', area='" + area2 + "', price='" + price2 + "', location='" + location2 + "' where FllName='" + ddlExchangedShop.SelectedItem.Text + "'", con);
                                    cmdreb2.ExecuteNonQuery();


                                    SqlCommand cmdreb3 = new SqlCommand("Update tblrent set currentperiodue='" + currentdue1 + "', shopno='" + shopno11 + "', area='" + area1 + "', price='" + price1 + "', monthlyvat='" + monthlyvat1 + "' where customer='" + PID + "'", con);
                                    cmdreb3.ExecuteNonQuery();


                                    SqlCommand cmdreb4 = new SqlCommand("Update tblrent set currentperiodue='" + currentdue + "', shopno='" + shopno.InnerText + "', area='" + area + "', price='" + price + "', monthlyvat='" + monthlyvat + "' where customer='" + ddlExchangedShop.SelectedItem.Text + "'", con);
                                    cmdreb4.ExecuteNonQuery();

                                    string exlanation = "Shop exchanged between " + PID + " and " + ddlExchangedShop.SelectedItem.Text + " from shop# " + shopno.InnerText + " to " + shopno3 + " has been takes place";
                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-exchange-alt text-white','icon-circle bg bg-primary','rentstatus1.aspx','MN')", con);
                                    cmd197h.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
            }
        }
    }
}