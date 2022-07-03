using Hangfire;
using Hangfire.SqlServer;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace advtech.Finance.Accounta
{
    public partial class CustomerVendorPreferences : System.Web.UI.Page
    {
#pragma warning disable CS0414 // The field 'CustomerVendorPreferences.deprecexp' is assigned but its value is never used
        double deprecexp = 0;
#pragma warning restore CS0414 // The field 'CustomerVendorPreferences.deprecexp' is assigned but its value is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    BindMainCategory(); BindMainCategory2(); BindMainCategory3(); bindtax(); bindPayrollDate();
                    bindautomationstatus(); bindTimesheet(); BindBankAccount(); payrollstatus(); BindPensionLiability();
                    BindCashAccount(); BindTaxPayable(); BindPayrolPayable(); BindPayrollExpense(); creditlimit();
                    BindPensionExpense(); BindAccountantAccess(); bindRestrictionDiv();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }

        private void bindRestrictionDiv()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String FN = readerAC["Name"].ToString();
                    String Utype = readerAC["Utype"].ToString();
                    readerAC.Close();
                    if (Utype == "MN")
                    {
                        AccountAuthDiv.Visible = true;
                        AutomationDiv.Visible = true;
                    }
                }
            }
        }
        private void BindAccountantAccess()
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
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string ah1; string ah2; string ah3; string ah4; string ah5; string ah6;
                            ah1 = reader["setting"].ToString();
                            ah2 = reader["fixedasset"].ToString();
                            ah3 = reader["expense"].ToString();
                            ah4 = reader["banking"].ToString();
                            ah5 = reader["crm"].ToString();
                            ah6 = reader["employee"].ToString();
                            if (ah1 == "No")
                            {
                                settingCheck.Checked = false;
                            }
                            if (ah1 == "Yes")
                            {
                                settingCheck.Checked = true;
                            }
                            //
                            if (ah2 == "No")
                            {
                                FixedCheck.Checked = false;
                            }
                            if (ah2 == "Yes")
                            {
                                FixedCheck.Checked = true;
                            }
                            //
                            if (ah3 == "No")
                            {
                                ExpenseCheck.Checked = false;
                            }
                            if (ah3 == "Yes")
                            {
                                ExpenseCheck.Checked = true;
                            }
                            //
                            if (ah4 == "No")
                            {
                                BankCheck.Checked = false;
                            }
                            if (ah4 == "Yes")
                            {
                                BankCheck.Checked = true;
                            }
                            //
                            if (ah5 == "No")
                            {
                                CRMCheck.Checked = false;
                            }
                            if (ah5 == "Yes")
                            {
                                CRMCheck.Checked = true;
                            }
                            //
                            if (ah6 == "No")
                            {
                                EmployeeCheck.Checked = false;
                            }
                            if (ah6 == "Yes")
                            {
                                EmployeeCheck.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        private void bindPayrollDate()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblPayrolldate", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    txtPayrollDate.TextMode = TextBoxMode.SingleLine;
                    txtPayrollDate.Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    txtPayrollDate.TextMode = TextBoxMode.Date;


                }
            }
        }
        private void BindPensionExpense()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlPensionExpense.DataSource = dt;
                    ddlPensionExpense.DataTextField = "Name";
                    ddlPensionExpense.DataBind();
                    ddlPensionExpense.Items.Insert(0, new ListItem(dt1.Rows[0][11].ToString(), "0"));

                }
            }
        }
        private void BindPensionLiability()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlPensionLiability.DataSource = dt;
                    ddlPensionLiability.DataTextField = "Name";
                    ddlPensionLiability.DataBind();
                    ddlPensionLiability.Items.Insert(0, new ListItem(dt1.Rows[0][12].ToString(), "0"));

                }
            }
        }
        private void BindCashAccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlCashaccount.DataSource = dt;
                    ddlCashaccount.DataTextField = "Name";
                    ddlCashaccount.DataBind();
                    ddlCashaccount.Items.Insert(0, new ListItem(dt1.Rows[0][8].ToString(), "0"));

                }
            }
        }
        private void BindTaxPayable()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlIncomeTaxPayable.DataSource = dt;
                    ddlIncomeTaxPayable.DataTextField = "Name";
                    ddlIncomeTaxPayable.DataBind();
                    ddlIncomeTaxPayable.Items.Insert(0, new ListItem(dt1.Rows[0][7].ToString(), "0"));

                }
            }
        }
        private void BindPayrolPayable()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlSalaryPayable.DataSource = dt;
                    ddlSalaryPayable.DataTextField = "Name";
                    ddlSalaryPayable.DataBind();
                    ddlSalaryPayable.Items.Insert(0, new ListItem(dt1.Rows[0][6].ToString(), "0"));

                }
            }
        }
        private void BindPayrollExpense()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlSalaryExpense.DataSource = dt;
                    ddlSalaryExpense.DataTextField = "Name";
                    ddlSalaryExpense.DataBind();
                    ddlSalaryExpense.Items.Insert(0, new ListItem(dt1.Rows[0][5].ToString(), "0"));

                }
            }
        }
        private void BindMainCategory()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlinventory.DataSource = dt;
                    ddlinventory.DataTextField = "Name";
                    ddlinventory.DataBind();
                    ddlinventory.Items.Insert(0, new ListItem(dt1.Rows[0][3].ToString(), "0"));

                }
            }
        }
        private void BindMainCategory2()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlAcountsales.DataSource = dt;
                    ddlAcountsales.DataTextField = "Name";

                    ddlAcountsales.DataBind();
                    ddlAcountsales.Items.Insert(0, new ListItem(dt1.Rows[0][1].ToString(), "0"));

                }
            }
        }
        private void BindMainCategory3()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlaccountcogs.DataSource = dt;
                    ddlaccountcogs.DataTextField = "Name";

                    ddlaccountcogs.DataBind();
                    ddlaccountcogs.Items.Insert(0, new ListItem(dt1.Rows[0][2].ToString(), "0"));

                }
            }
        }
        private void bindtax()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Status='Active'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddltaxpayable.DataSource = dt;
                    ddltaxpayable.DataTextField = "Name";

                    ddltaxpayable.DataBind();
                    ddltaxpayable.Items.Insert(0, new ListItem(dt1.Rows[0][4].ToString(), "0"));

                }
            }
        }
        private void bindTimesheet()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblsettingTimesheet", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i2 = dt.Rows.Count;
                    //
                    if (i2 != 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string ah1;
                            ah1 = reader["status"].ToString();
                            if (ah1 == "Enabled")
                            {
                                Checkbox1.Checked = true;
                            }
                            if (ah1 == "Disabled")
                            {
                                Checkbox1.Checked = false;
                            }
                        }
                    }
                }
            }
        }
        private void creditlimit()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblsetting", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i2 = dt.Rows.Count;
                    //
                    if (i2 != 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string ah1; string ah2;
                            ah2 = reader["portal"].ToString();
                            ah1 = reader["climit"].ToString();
                            if (ah1 == "Yes")
                            {
                                customCheck1.Checked = true;
                            }
                            if (ah1 == "No")
                            {
                                customCheck1.Checked = false;
                            }
                            if (ah2 == "No")
                            {
                                Checkbox3.Checked = false;
                            }
                            if (ah2 == "Yes")
                            {
                                Checkbox3.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        private void payrollstatus()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblsettingpayroll", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i2 = dt.Rows.Count;
                    //
                    if (i2 != 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string ah1;
                            ah1 = reader["status"].ToString();
                            if (ah1 == "Enabled")
                            {
                                Checkbox2.Checked = true;
                            }
                            if (ah1 == "Disabled")
                            {
                                Checkbox2.Checked = false;
                            }
                        }
                    }
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmds = new SqlCommand("Update tblaccountinfo set  salariespayable='" + ddlSalaryPayable.SelectedItem.Text + "',incometaxpayable='" + ddlIncomeTaxPayable.SelectedItem.Text + "',cashaccount='" + ddlCashaccount.SelectedItem.Text + "',payrollexpense='" + ddlSalaryExpense.SelectedItem.Text + "',inventory='" + ddlinventory.SelectedItem.Text + "',cogs='" + ddlaccountcogs.SelectedItem.Text + "',sales='" + ddlAcountsales.SelectedItem.Text + "',tax='" + ddltaxpayable.SelectedItem.Text + "',bankaccount='" + ddlBankAccount.SelectedItem.Text + "',pensionexpense='" + ddlPensionExpense.SelectedItem.Text + "',pensionliability='" + ddlPensionLiability.SelectedItem.Text + "'", con);
                con.Open();
                cmds.ExecuteNonQuery();
                SqlCommand cmdse = new SqlCommand("Update tblPayrolldate set  paymentdate='" + txtPayrollDate.Text + "'", con);

                cmdse.ExecuteNonQuery();
                if (Checkbox3.Checked == true)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblsetting set  portal='Yes'", con);

                    cmdcl.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblsetting set  portal='No'", con);

                    cmdcl.ExecuteNonQuery();

                }
                if (customCheck1.Checked == true)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblsetting set  climit='Yes'", con);

                    cmdcl.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblsetting set  climit='No'", con);

                    cmdcl.ExecuteNonQuery();

                }
                if (Checkbox1.Checked == true)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblsettingTimesheet set  status='Enabled'", con);

                    cmdcl.ExecuteNonQuery();
                }
                if (Checkbox1.Checked == false)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblsettingTimesheet set  status='Disabled'", con);

                    cmdcl.ExecuteNonQuery();

                }
                if (Checkbox2.Checked == true)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblsettingpayroll set  status='Enabled'", con);

                    cmdcl.ExecuteNonQuery();
                }
                if (Checkbox2.Checked == false)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblsettingpayroll set  status='Disabled'", con);

                    cmdcl.ExecuteNonQuery();
                }

                //Accountant Authorization setting saving

                if (settingCheck.Checked == false)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  setting='No'", con);

                    cmdcl.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  setting='Yes'", con);

                    cmdcl.ExecuteNonQuery();
                }
                //Fixed
                if (FixedCheck.Checked == false)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  fixedasset='No'", con);

                    cmdcl.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  fixedasset='Yes'", con);

                    cmdcl.ExecuteNonQuery();
                }
                //Expense
                if (ExpenseCheck.Checked == false)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  expense='No'", con);

                    cmdcl.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  expense='Yes'", con);

                    cmdcl.ExecuteNonQuery();
                }
                //BankModule
                if (BankCheck.Checked == false)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  banking='No'", con);

                    cmdcl.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  banking='Yes'", con);

                    cmdcl.ExecuteNonQuery();
                }
                //CRM
                if (CRMCheck.Checked == false)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  crm='No'", con);

                    cmdcl.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  crm='Yes'", con);

                    cmdcl.ExecuteNonQuery();
                }
                //Employee
                if (EmployeeCheck.Checked == false)
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  employee='No'", con);

                    cmdcl.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdcl = new SqlCommand("Update tblModuleAuthorization set  employee='Yes'", con);

                    cmdcl.ExecuteNonQuery();
                }
                Response.Redirect("CustomerVendorPreferences.aspx");

            }
        }
        private void bindautomationstatus()
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
                        Label1.Text = company; Label1.ForeColor = Color.Red;
                    }
                    else
                    {
                        Label1.Text = company; Label1.ForeColor = Color.Green;
                    }
                    SqlCommand cmdde = new SqlCommand("select status from tblDepreciationAutomation", con);
                    SqlDataReader readerde = cmdde.ExecuteReader();

                    if (readerde.Read())
                    {
                        string stat;
                        stat = readerde["status"].ToString();
                        if (stat == "stoped")
                        {
                            Label3.Text = stat; Label3.ForeColor = Color.Red;
                        }
                        else
                        {
                            Label3.Text = stat; Label3.ForeColor = Color.Green;
                        }
                    }

                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
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
                    company = reader["status"].ToString();
                    reader.Close();
                    if (company == "stoped")
                    {
                        SqlCommand cmdcl = new SqlCommand("Update tblautomation set  status='Started'", con);

                        cmdcl.ExecuteNonQuery();

                        JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                        RecurringJob.AddOrUpdate<updater1>("SMSCust", x => x.Update1(), Cron.Daily());

                    }
                    else
                    {
                        SqlCommand cmdcl = new SqlCommand("Update tblautomation set  status='stoped'", con);

                        cmdcl.ExecuteNonQuery();
                        RecurringJob.RemoveIfExists("SMSCust");

                    }
                }
                Response.Redirect("CustomerVendorPreferences.aspx");
            }

        }
        private void BindBankAccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblBankAccounting", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                SqlCommand cmd1 = new SqlCommand("select * from tblaccountinfo", con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt.Rows.Count != 0)
                {
                    ddlBankAccount.DataSource = dt;
                    ddlBankAccount.DataTextField = "AccountName";
                    ddlBankAccount.DataValueField = "AC";
                    ddlBankAccount.DataBind();
                    ddlBankAccount.Items.Insert(0, new ListItem(dt1.Rows[0][9].ToString(), "0"));
                }
            }
        }
        public class LeaveUpdate
        {
            public void LeaveUpdates()
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd41 = new SqlCommand("select * from tblPersonalInformation where DATEDIFF(day, datejoiningupdate, '" + DateTime.Now.Date + "') = 0", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1); int i1 = dt1.Rows.Count;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[i][1].ToString() + " Annual Leave days Renewed Automatically','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-success','employeeinfo.aspx?fname=" + dt1.Rows[i][1].ToString() + "','Acc')", con);
                        cmd197h.ExecuteNonQuery();
                        SqlCommand cmd197h2 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[i][1].ToString() + " Annual Leave days Renewed Automatically','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-success','employeeinfo.aspx?fname=" + dt1.Rows[i][1].ToString() + "','MN')", con);
                        cmd197h2.ExecuteNonQuery();
                        SqlCommand cmd197h2g = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[i][1].ToString() + " Annual Leave days Renewed Automatically','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-success','employeeinfo.aspx?fname=" + dt1.Rows[i][1].ToString() + "','FH')", con);
                        cmd197h2g.ExecuteNonQuery();
                        if (dt1.Rows[i][10].ToString() == "0" || dt1.Rows[i][10].ToString() == "" || dt1.Rows[i][10].ToString() == null)
                        {
                            Int64 j = 1;
                            Int64 k = j + 1;
                            Int64 M = 15;
                            SqlCommand cmdup = new SqlCommand("update tblPersonalInformation set leave_days_left='" + M + "',datejoiningupdate='" + DateTime.Now.Date + "',m='" + j + "'  where FullName='" + dt1.Rows[i][1].ToString() + "'", con);
                            cmdup.ExecuteNonQuery();
                        }
                        else
                        {
                            Int64 j = Convert.ToInt64(dt1.Rows[i][12].ToString());
                            Int64 b = Convert.ToInt64(dt1.Rows[i][10].ToString());
                            Int64 k = j + 1;
                            Int64 M = b + 15 + j;
                            SqlCommand cmdup = new SqlCommand("update tblPersonalInformation set leave_days_left='" + M + "',datejoiningupdate='" + DateTime.Now.Date + "',m='" + k + "' where FullName='" + dt1.Rows[i][1].ToString() + "'", con);
                            cmdup.ExecuteNonQuery();
                        }

                    }
                }
            }
        }
        public class Agreement
        {
            public void Agreements()
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd41 = new SqlCommand("select * from tblCustomers where DATEDIFF(day, agreementdate, '" + DateTime.Now.Date + "') < 10", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1); int i1 = dt1.Rows.Count;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[i][2].ToString() + " Reaches its Agreement Renewal Date','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','CustomerDetails.aspx?ref2='+" + dt1.Rows[2][1].ToString() + ",'Acc')", con);
                        cmd197h.ExecuteNonQuery();
                        SqlCommand cmd197h2 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[2][1].ToString() + " Reaches its Agreement Renewal Date','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','CustomerDetails.aspx?ref2='+" + dt1.Rows[2][1].ToString() + ",'MN')", con);
                        cmd197h2.ExecuteNonQuery();
                        SqlCommand cmd197h2g = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[2][1].ToString() + " Reaches its Agreement Renewal Date','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','CustomerDetails.aspx?ref2='+" + dt1.Rows[2][1].ToString() + ",'FH')", con);
                        cmd197h2g.ExecuteNonQuery();

                    }
                }
            }
        }
        public class Reorder
        {
            public void Reorders()
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd4 = new SqlCommand("select * from tblSalesBrand", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd4);
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    int j = 0;
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        SqlCommand cmd41 = new SqlCommand("select * from tblStockCard where product='" + dt.Rows[i][3].ToString() + "' and balance < " + dt.Rows[i][11].ToString() + "", con);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1); j = dt1.Rows.Count;
                        if (j != 0)
                        {
                            for (int k = 0; k < dt1.Rows.Count; k++)
                            {
                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[k][1].ToString() + " Reaches its Reorder Quantity','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','SalesProduct2324.aspx','Acc')", con);
                                cmd197h.ExecuteNonQuery();
                                SqlCommand cmd197h2 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[k][1].ToString() + " Reaches its Reorder Quantity','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','SalesProduct2324.aspx','MN')", con);
                                cmd197h2.ExecuteNonQuery();
                                SqlCommand cmd197h2g = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + dt1.Rows[k][1].ToString() + " Reaches its Reorder Quantity','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','SalesProduct2324.aspx','FH')", con);
                                cmd197h2g.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        public class remindDueDate
        {
            public void remindDueDates()
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select*from tblcreditnote where  balance > 0", con);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {                            //Calculate aged days
                                DateTime today = DateTime.Now.Date;
                                DateTime duedate1 = Convert.ToDateTime(dt.Rows[j][2].ToString());
                                TimeSpan t = today - duedate1;
                                string dayleft = t.TotalDays.ToString();
                                //End calculating
                                SqlCommand cmd41 = new SqlCommand("select * from tblCustomers where FllName='" + dt.Rows[j][1].ToString() + "'", con);
                                SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                                DataTable dt1 = new DataTable();
                                sda1.Fill(dt1); int i1 = dt1.Rows.Count;
                                string message2 = "Customer, " + dt.Rows[j][1].ToString() + " Credit credits aged " + dayleft + " days";
                                string url = "creditnotedetails.aspx?ref2=" + dt.Rows[j][0].ToString() + "&&" + "cust=" + dt.Rows[j][1].ToString();
                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + message2 + "','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','" + url + "','Acc')", con);
                                cmd197h.ExecuteNonQuery();
                                SqlCommand cmde = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + message2 + "','Machine','','Unseen','fas fa-exclamation-triangle fa-1x text-white','icon-circle bg bg-warning','" + url + "','MN')", con);
                                cmde.ExecuteNonQuery();
                                string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                                string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";
                                string sms = "Dear customer your credit passed " + dayleft + " days";
                                TwilioClient.Init(accountSid, authToken);

                                var message = MessageResource.Create(
                                    body: sms,
                                    from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                    to: new Twilio.Types.PhoneNumber("+251" + dt1.Rows[j][6].ToString())
                                );
                            }
                        }
                    }
                }
            }
        }
        public class updater1
        {
            public void Update1()
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select*from tblrent where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) = 15", con);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                SqlCommand cmd41 = new SqlCommand("select * from tblCustomers where FllName='" + dt.Rows[j][2].ToString() + "'", con);
                                SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                                DataTable dt1 = new DataTable();
                                sda1.Fill(dt1); int i1 = dt1.Rows.Count;
                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + i + "'+' '+'Customers remains 15 days for payment','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','rentstatus1.aspx','Acc')", con);
                                cmd197h.ExecuteNonQuery();
                                SqlCommand cmde = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + i + "'+' '+'Customers remains 15 days for payment','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','rentstatus1.aspx','MN')", con);
                                cmde.ExecuteNonQuery();

                                string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                                string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                                TwilioClient.Init(accountSid, authToken);

                                var message = MessageResource.Create(
                                    body: "Dear customer, your payment remains 11 days. for more info, visit your portal.",
                                    from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                    to: new Twilio.Types.PhoneNumber("+251" + dt1.Rows[j][6].ToString())
                                );
                            }
                        }
                    }
                }
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select status from tblDepreciationAutomation", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company;
                    company = reader["status"].ToString();
                    reader.Close();
                    if (company == "stoped")
                    {
                        SqlCommand cmdcl = new SqlCommand("Update tblDepreciationAutomation set  status='Started'", con);

                        cmdcl.ExecuteNonQuery();
                        JobStorage.Current = new SqlServerStorage(CS);

                        RecurringJob.AddOrUpdate<Reorder>("Reorder", x => x.Reorders(), Cron.Monthly());
                        RecurringJob.AddOrUpdate<Agreement>("Agreement", x => x.Agreements(), Cron.Monthly());
                        RecurringJob.AddOrUpdate<LeaveUpdate>("Leave", x => x.LeaveUpdates(), Cron.Monthly());
                        RecurringJob.AddOrUpdate<remindDueDate>("remindDueDate", x => x.remindDueDates(), Cron.Monthly());
                    }
                    else
                    {
                        SqlCommand cmdcl = new SqlCommand("Update tblDepreciationAutomation set  status='stoped'", con);

                        cmdcl.ExecuteNonQuery();
                        RecurringJob.RemoveIfExists("Depr");
                        RecurringJob.RemoveIfExists("Reorder");
                        RecurringJob.RemoveIfExists("Agreement");
                        RecurringJob.RemoveIfExists("Leave");
                        RecurringJob.RemoveIfExists("remindDueDate");
                    }
                }
                Response.Redirect("CustomerVendorPreferences.aspx");
            }
        }


    }
}