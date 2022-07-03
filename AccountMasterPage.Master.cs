using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace advtech.Finance.Accounta
{
    public partial class AccountMasterPage : System.Web.UI.MasterPage
    {
        public string session;
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;

        protected void Page_Load(object sender, EventArgs e)
        {
            //RecurringJob.AddOrUpdate<updater>("",x=>x.Update(),Cron.Minutely);
            //RecurringJob.RemoveIfExists("");
            if (Session["USERNAME"] != null)
            {
                BindBrandsRptr53();

                bindname(); bindtab(); unpaidcounter(); bindbutton(); bindStockRequest();
                bindprofileimage(); bindAccountantRestriction(); BindCounter();
                if (!IsPostBack)
                {

                }
            }
            else
            {

                Response.Redirect("~/Login/LogIn1.aspx");

            }
        }
        private void bindAccountantRestriction()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblModuleAuthorization", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i2 = dt.Rows.Count;
                    //
                    if (i2 != 0)
                    {

                        SqlCommand cmd2 = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "' ", con);
                        SqlDataReader reader1 = cmd2.ExecuteReader();

                        if (reader1.Read())
                        {
                            string kc; kc = reader1["Utype"].ToString(); reader1.Close();
                            if (kc == "Acc")
                            {
                                SqlDataReader reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    string ah1; string ah2; string ah3; string ah4; string ah5; string ah6;
                                    ah1 = reader["setting"].ToString();
                                    ah2 = reader["fixedasset"].ToString();
                                    ah3 = reader["expense"].ToString();
                                    ah4 = reader["banking"].ToString();
                                    ah5 = reader["crm"].ToString();
                                    ah6 = reader["employee"].ToString(); reader.Close();
                                    if (ah1 == "No")
                                    {
                                        setting.Visible = false; Settingbar.Visible = false;
                                    }
                                    if (ah1 == "Yes")
                                    {
                                        setting.Visible = true; Settingbar.Visible = true;
                                    }
                                    //
                                    if (ah2 == "No")
                                    {
                                        assetmanagement.Visible = false;
                                    }
                                    if (ah2 == "Yes")
                                    {
                                        assetmanagement.Visible = false;
                                    }
                                    //
                                    if (ah3 == "No")
                                    {
                                        Li2.Visible = false;
                                    }
                                    if (ah3 == "Yes")
                                    {
                                        Li2.Visible = true;
                                    }
                                    //
                                    if (ah4 == "No")
                                    {
                                        bankmodule.Visible = false;
                                    }
                                    if (ah4 == "Yes")
                                    {
                                        bankmodule.Visible = true;
                                    }
                                    //
                                    if (ah5 == "No")
                                    {
                                        rentservice.Visible = false;
                                        hiddensearch.Visible = false;
                                        test.Visible = false;
                                        Customerbar.Visible = false;
                                    }
                                    if (ah5 == "Yes")
                                    {
                                        rentservice.Visible = true;
                                    }
                                    //
                                    if (ah6 == "No")
                                    {
                                        employee.Visible = false;
                                    }
                                    if (ah6 == "Yes")
                                    {
                                        employee.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void bindprofileimage()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
            SqlDataReader readerAC = cmd2AC.ExecuteReader();

            if (readerAC.Read())
            {
                String FN = readerAC["Name"].ToString(); readerAC.Close();

                str = "select * from tblUserProfile where Name='" + FN + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt); int i = dt.Rows.Count;
                if (i != 0)
                {
                    defaultprofile.Visible = false;
                }
                Repeater4.DataSource = dt;
                Repeater4.DataBind();
                con.Close();
            }
        }

        private void BindCounter()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "' ", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    string kc; string kc1;
                    kc = reader["Utype"].ToString();
                    kc1 = reader["Name"].ToString();
                    reader.Close();
                    con.Close();
                    con.Open();


                    SqlCommand com1 = new SqlCommand("select * from tblNotification where Status='Unseen' and Utype='" + kc + "'", con);
                    using (SqlDataAdapter sda2 = new SqlDataAdapter(com1))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(com1))
                        {
                            DataTable dtBrands2 = new DataTable();
                            sda.Fill(dtBrands2);
                            int i2 = dtBrands2.Rows.Count;

                            if (i2 <= 0)
                            {

                            }
                            else
                            {
                                counter.InnerText = i2.ToString();
                            }
                        }
                    }
                }

            }

        }
        private void BindBrandsRptr53()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "' ", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    string kc; string kc1;
                    kc = reader["Utype"].ToString();
                    kc1 = reader["Name"].ToString();
                    reader.Close();
                    con.Close();
                    con.Open();


                    SqlCommand com1 = new SqlCommand("select * from tblNotification where Utype='" + kc + "' ORDER BY NID DESC ", con);
                    using (SqlDataAdapter sda2 = new SqlDataAdapter(com1))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(com1))
                        {
                            DataTable dtBrands = new DataTable();
                            sda2.Fill(dtBrands); int i = dtBrands.Rows.Count;
                            if (i > 0)
                            {
                                Repeater1.DataSource = dtBrands;
                                Repeater1.DataBind();
                            }
                            else
                            {
                                mainb.Visible = true;
                                unread_tab.Visible = false;
                                delete_tab.Visible = false;
                            }
                        }
                    }
                }

            }

        }
        private void bindbutton()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String Utype = readerAC["Utype"].ToString();
                    String Name = readerAC["Name"].ToString();
                    var Fname = Regex.Replace(Name.Split()[0], @"[^0-9a-zA-Z\ ]+", "");
                    UserNameWelcome.InnerText = Fname;
                    SystemDate.InnerText = DateTime.Now.ToString("MMM dd, yyyy: HH:mm");
                    if (Utype == "FH")
                    {
                        leavereuest.Visible = true;

                    }
                    else if (Utype == "TH")
                    {
                        leavereuest.Visible = true;

                    }
                    else if (Utype == "STH")
                    {
                        leavereuest.Visible = true;

                    }
                    else if (Utype == "MH")
                    {
                        leavereuest.Visible = true;

                    }
                    else
                    {
                        leavereuest.Visible = false;


                    }
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
        private void MakeCustomerasPaid(string name)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                CustomerUtil CustomerInfo = new CustomerUtil(name);
                string getCustomerName = CustomerInfo.GetCustomerName.Item1;
                string terms = CustomerInfo.GetCustomerName.Item2;
                string duedates = CustomerInfo.GetCustomerRentInfo;
                DateTime today = DateTime.Now.Date;
                DateTime duedate1 = Convert.ToDateTime(duedates);
                TimeSpan t = duedate1 - today;
                string dayleft = "";
                dayleft = t.TotalDays.ToString();
                if (Convert.ToInt32(dayleft) < 15)
                {
                    if (terms == "Monthly")
                    {
                        DateTime duedate = Convert.ToDateTime(duedates);
                        DateTime newduedate = duedate.AddDays(30);
                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + getCustomerName + "'", con);
                        cmdrent.ExecuteNonQuery();
                    }
                    else if (terms == "Every Three Month")
                    {
                        DateTime duedate = Convert.ToDateTime(duedates);
                        DateTime newduedate = duedate.AddDays(90);
                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + getCustomerName + "'", con);
                        cmdrent.ExecuteNonQuery();
                    }
                    else if (terms == "Every Six Month")
                    {
                        DateTime duedate = Convert.ToDateTime(duedates);
                        DateTime newduedate = duedate.AddDays(180);
                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + getCustomerName + "'", con);
                        cmdrent.ExecuteNonQuery();
                    }
                    else
                    {
                        DateTime duedate = Convert.ToDateTime(duedates);
                        DateTime newduedate = duedate.AddDays(365);
                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + getCustomerName + "'", con);
                        cmdrent.ExecuteNonQuery();
                    }
                    string explanation = getCustomerName + " due date updated successfully ";
                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','"+explanation+"','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','#','MN')", con);
                    cmd197h.ExecuteNonQuery();
                    string message = "The customer marked as paid successfully!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + message + "');", true);
                }
                else
                {
                    string message = "The customer due date is not reached!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + message + "');", true);
                }
            }
        }
        private void bindStockRequest()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblStockOutRequest where status='Pending'", con);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    if (i == 0)
                    {

                    }
                    else
                    {
                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                        if (readerAC.Read())
                        {
                            String FN = readerAC["Name"].ToString();
                            String Utype = readerAC["Utype"].ToString();
                            readerAC.Close();
                            if (Utype == "MN")
                            {
                                Div8.Visible = true;
                            }
                        }


                        Repeater3.DataSource = dt;
                        Repeater3.DataBind();
                    }

                }
            }
        }
        private void bindtab()
        {
            TCsidebar.Visible = false;

            bookmark.Visible = true;
            bankmodule.Visible = true;
            dashboard3.Visible = true;
            inventory.Visible = false;
            rentservice.Visible = true;
            purchase.Visible = false;
            ledger.Visible = true;
            payroll.Visible = false;
            reports.Visible = true;
            activity.Visible = true;
            assetmanagement.Visible = false;
            setting.Visible = true;
            Settingbar.Visible = true;
            Customerbar.Visible = true;
            Li1.Visible = false;
            Div751.Visible = true;
            activity.Visible = true;
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
                        Span1.InnerText = "0";
                    }
                    else
                    {
                        Span1.InnerText = i.ToString();
                    }

                }
            }
        }
        private void bindname()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String Utype = readerAC["Utype"].ToString();

                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            Session["USERENAME"] = null;
            Response.Redirect("~/Login/LogIn1.aspx");


        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string invoceindicator = Convert.ToString(txtCustomer.Text);
            if (invoceindicator.Length < 3)
            {
                string message = "The search character length must not be less than three!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                string getfirstvalue = invoceindicator.Substring(0, 2);

                if (getfirstvalue.ToLower() == "c-")
                {
                    Response.Redirect("cashpay.aspx?search=" + txtCustomer.Text);
                }
                else if (getfirstvalue.ToLower() == "l-")
                {
                    Response.Redirect("Ledger_analysis_details.aspx?search=" + txtCustomer.Text);
                }
                else if (getfirstvalue.ToLower() == "s-")
                {
                    Response.Redirect("shop_details.aspx?search=" + txtCustomer.Text);
                }
                else if (getfirstvalue.ToLower() == "i-")
                {
                    Response.Redirect("rentinvoicereport.aspx?search=" + txtCustomer.Text);
                }
                else if (getfirstvalue.ToLower() == "m-")
                {
                    string name = Convert.ToString(txtCustomer.Text).Substring(2);
                    MakeCustomerasPaid(name);
                }
                else
                {
                    Response.Redirect("CustomerDetails.aspx?search=" + txtCustomer.Text);
                }
            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblStockOutRequest where status='Pending'", con);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    if (i != 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            SqlCommand cmd2 = new SqlCommand("select * from tblStockCard where product='" + dt.Rows[j][1].ToString() + "'", con);

                            //Sale Reader
                            SqlDataReader reader = cmd2.ExecuteReader();

                            if (reader.Read())
                            {
                                string balance;
                                balance = reader["balance"].ToString();
                                double bal = Convert.ToDouble(balance) - Convert.ToDouble(dt.Rows[j][2].ToString());
                                reader.Close();
                                SqlCommand cmdre = new SqlCommand("Update tblStockCard set  balance='" + bal + "' where product='" + dt.Rows[j][1].ToString() + "'", con);
                                cmdre.ExecuteNonQuery();
                                SqlCommand cmd1 = new SqlCommand("insert into tblStockCard1 values('" + dt.Rows[j][1].ToString() + "','" + 0.ToString() + "','" + dt.Rows[j][2].ToString() + "','" + bal + "','" + DateTime.Now.Date + "','" + dt.Rows[j][6].ToString() + "','" + dt.Rows[j][3].ToString() + "','')", con);
                                cmd1.ExecuteNonQuery();
                                if (dt.Rows[j][6].ToString() == "Asset")
                                {
                                    SqlCommand cmdrey = new SqlCommand("Update tblFixedAssetdetails set  status='Occupied' where barcode='" + dt.Rows[j][3].ToString() + "'", con);
                                    cmdrey.ExecuteNonQuery();
                                    SqlCommand cmd11 = new SqlCommand("select*from tblFixedAsset where name='" + dt.Rows[j][1].ToString() + "'", con);

                                    SqlDataReader reader11 = cmd11.ExecuteReader();
                                    if (reader11.Read())
                                    {
                                        string unit11;
                                        unit11 = reader11["unitcost"].ToString(); reader11.Close();
                                        SqlCommand cmdin = new SqlCommand("insert into tblEmpAsset values('" + dt.Rows[j][7].ToString() + "','" + dt.Rows[j][1].ToString() + "','" + DateTime.Now.Date + "','" + unit11 + "','" + unit11 + "','" + 1 + "','" + dt.Rows[j][3].ToString() + "','" + dt.Rows[j][3].ToString() + "','Active')", con);
                                        cmdin.ExecuteNonQuery();
                                    }
                                }
                                if (dt.Rows[j][4].ToString() == "Return")
                                {
                                    SqlCommand cmd4 = new SqlCommand("Update tblEmpAsset set  Qty='0',Cost='0',status='Returned' where references2='" + dt.Rows[j][3].ToString() + "' and Name='" + dt.Rows[j][7].ToString() + "' and AssetName='" + dt.Rows[j][1].ToString() + "'", con);

                                    cmd4.ExecuteNonQuery();

                                    SqlCommand cmdrey = new SqlCommand("Update tblFixedAssetdetails set  status='Available' where barcode='" + dt.Rows[j][3].ToString() + "'", con);
                                    cmdrey.ExecuteNonQuery();
                                    double bal1 = Convert.ToDouble(balance) + Convert.ToDouble(dt.Rows[j][2].ToString());

                                    SqlCommand cmdre1 = new SqlCommand("Update tblStockCard set  balance='" + bal1 + "' where product='" + dt.Rows[j][1].ToString() + "'", con);
                                    cmdre1.ExecuteNonQuery();
                                    SqlCommand cmd11 = new SqlCommand("insert into tblStockCard1 values('" + dt.Rows[j][1].ToString() + "','" + dt.Rows[j][2].ToString() + "','" + 0.ToString() + "','" + bal1 + "','" + DateTime.Now.Date + "','" + dt.Rows[j][6].ToString() + "','" + dt.Rows[j][3].ToString() + "','')", con);
                                    cmd11.ExecuteNonQuery();
                                }
                            }
                        }
                        SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Stock out request has been approved.','Manager','','Unseen','fas fa-check text-white','icon-circle bg bg-success','','SK')", con);
                        cmd197hb.ExecuteNonQuery();
                        SqlCommand cmdw = new SqlCommand("Update tblStockOutRequest set  status='Approved' where status='Pending'", con);

                        cmdw.ExecuteNonQuery();
                        Response.Redirect("Home.aspx");
                    }
                }
            }
        }
        protected void btnDeny_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                String PID = Convert.ToString(Request.QueryString["stock"]);
                con.Open();

                string total = "Stock Out Request Denied: " + txtDenyReason.Text.Trim('\'');
                SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + total + "','Manager','','Unseen','fas fa-info text-white','icon-circle bg bg-danger','','SK')", con);
                cmd197hb.ExecuteNonQuery();
                SqlCommand cmd = new SqlCommand("Update tblStockOutRequest set  status='Denied' where status='Pending'", con);

                cmd.ExecuteNonQuery();
                Response.Redirect("Home.aspx");
            }
        }
        protected void Button2_Click1(object sender, EventArgs e)
        {
            Response.Redirect("stockcard.aspx?search=" + txtProductSearch.Text);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerDetails.aspx?search=" + txtHidenSearch.Text);
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string SavePath = Server.MapPath("~/Finance/Accounta/backup/raksym_database.bak");

            Response.AppendHeader("Content-Disposition", "attachment; filename=raksym_database.bak");
            Response.WriteFile(SavePath);
            Response.End();
        }
    }
}
