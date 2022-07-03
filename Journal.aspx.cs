using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class Journal : System.Web.UI.Page
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

                    BindBrandsRptr(); BindBrandsRptr34();
                    BindMainCategory(); bindpayablevendor();
                    BindMainCategory2(); bindpayablecredit();
                    BindBrandsRptr3(); bindpayabledebit();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }
        private void bindpayablevendor()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblVendor ", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlVendor.DataSource = dt;
                    ddlVendor.DataTextField = "FllName";
                    ddlVendor.DataValueField = "CID";
                    ddlVendor.DataBind();
                    ddlVendor.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void bindpayablecredit()
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
                    ddlCashCredit.DataSource = dt;
                    ddlCashCredit.DataTextField = "Name";
                    ddlCashCredit.DataValueField = "ACT";
                    ddlCashCredit.DataBind();
                    ddlCashCredit.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void bindpayabledebit()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where AccountType='Other Current Liabilities' or AccountType='Accounts Payable' or AccountType='Long Term Liabilities'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlPayableDebit.DataSource = dt;
                    ddlPayableDebit.DataTextField = "Name";
                    ddlPayableDebit.DataValueField = "ACT";
                    ddlPayableDebit.DataBind();
                    ddlPayableDebit.Items.Insert(0, new ListItem("-Select-", "0"));
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

                if (dt.Rows.Count != 0)
                {
                    ddl1.DataSource = dt;
                    ddl1.DataTextField = "Name";
                    ddl1.DataValueField = "ACT";
                    ddl1.DataBind();
                    ddl1.Items.Insert(0, new ListItem("-Select-", "0"));
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
        private void BindBrandsRptr()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblGeneralJournal";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "Cat");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 200;
            Pds1.CurrentPageIndex = CurrentPage;
            Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
            btnPrevious.Enabled = !Pds1.IsFirstPage;
            btnNext.Enabled = !Pds1.IsLastPage;
            Repeater1.DataSource = Pds1;
            Repeater1.DataBind();
            con.Close();
        }
        private void BindBrandsRptr34()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select*from tblJournalAttachment";
            com = new SqlCommand(str, con);
            ds = new DataSet();
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater2.DataSource = dtBrands;
                Repeater2.DataBind();
            }
        }
        private void BindBrandsRptr3()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select SUM(Debit) Debit,SUM(Credit) Credit from tblGeneralJournal";
            com = new SqlCommand(str, con);
            ds = new DataSet();
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater3.DataSource = dtBrands;
                Repeater3.DataBind();
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
        public int CurrentPage2
        {
            get
            {
                object s1 = this.ViewState["CurrentPage2"];
                if (s1 == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(s1);
                }
            }

            set { this.ViewState["CurrentPage2"] = value; }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;

            BindBrandsRptr();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindBrandsRptr();
        }
        protected void Sub1(object sender, EventArgs e)
        {

        }
        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            if (txtDateform.Text == "" || txtDateto.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();

                str = "select * from tblGeneralJournal where  Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "'";

                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 45;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
            }

        }
        private double select_debit_balance(string debit_account)
        {
            double balance = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmddebit = new SqlCommand("select * from tblGeneralLedger2 where Account='" + debit_account + "'", con);
                SqlDataReader readerdebit = cmddebit.ExecuteReader();
                if (readerdebit.Read())
                {
                    balance += Convert.ToDouble(readerdebit["Balance"].ToString());
                    readerdebit.Close();
                }
            }
            return balance;
        }
        private double select_credit_balance(string credit_account)
        {
            double balance = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcredit = new SqlCommand("select * from tblGeneralLedger2 where Account='" + credit_account + "'", con);
                SqlDataReader readercredit = cmdcredit.ExecuteReader();
                if (readercredit.Read())
                {
                    balance += Convert.ToDouble(readercredit["Balance"].ToString());
                    readercredit.Close();
                }
            }
            return balance;
        }
        protected void Yes(object sender, EventArgs e)
        {
            if (ddl1.SelectedItem.Text == DropDownList1.SelectedItem.Text)
            {
                string message = "Same account on both credit and debit can not be selected.";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    if (ddl1.SelectedItem.Text == "" | DropDownList1.SelectedItem.Text == "" || txtAmount.Text == "" || txtDate.Text == "")
                    {
                        lblMsg.Text = "Please Fill All the required input"; lblMsg.ForeColor = Color.Red;
                    }
                    else
                    {
                        SqlCommand cmd16 = new SqlCommand("select * from tblLedgAccTyp where Name='" + ddl1.SelectedItem.Text + "'", con);

                        SqlDataReader reader6 = cmd16.ExecuteReader();

                        if (reader6.Read())
                        {
                            string debited;
                            debited = reader6["AccountType"].ToString();
                            reader6.Close();


                            SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + DropDownList1.SelectedItem.Text + "'", con);


                            SqlDataReader reader66 = cmd166.ExecuteReader();

                            if (reader66.Read())
                            {
                                string credited;
                                credited = reader66["AccountType"].ToString();
                                reader66.Close();




                                SqlCommand cmd111 = new SqlCommand("insert into tblGeneralJournal values('" + txtExplanation.Text + "','','" + txtAmount.Text + "','','" + txtDate.Text + "','','" + ddl1.SelectedItem.Text + "','','" + debited + "')", con);
                                cmd111.ExecuteNonQuery();

                                SqlCommand cmd1112 = new SqlCommand("insert into tblGeneralJournal values('','','','" + txtAmount.Text + "','" + txtDate.Text + "','" + DropDownList1.SelectedItem.Text + "','','','" + credited + "')", con);
                                cmd1112.ExecuteNonQuery();
                                //Journal Entry

                                //Ledger Entry Debit


                                Double G = Convert.ToDouble(txtAmount.Text);

                                if (debited == "Cash" || debited == "Accounts Receivable" || debited == "Inventory" || debited == "Other Current Assets" ||
                                    debited == "Fixed Assets" || debited == "Other Assets" || debited == "Cost of Sales" || debited == "Expenses")
                                {
                                    Double bl1 = select_debit_balance(ddl1.SelectedItem.Text) + G;

                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddl1.SelectedItem.Text + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + txtExplanation.Text + "'+'-'+'" + DropDownList1.SelectedItem.Text + "','','" + txtAmount.Text + "','0','" + bl1 + "','" + txtDate.Text + "','" + ddl1.SelectedItem.Text + "','','" + debited + "')", con);
                                    cmd19.ExecuteNonQuery();

                                }
                                else
                                {
                                    Double bl1 = select_debit_balance(ddl1.SelectedItem.Text) - G;
                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddl1.SelectedItem.Text + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + txtExplanation.Text + "'+'-'+'" + DropDownList1.SelectedItem.Text + "','','" + txtAmount.Text + "','0','" + bl1 + "','" + txtDate.Text + "','" + ddl1.SelectedItem.Text + "','','" + debited + "')", con);
                                    cmd19.ExecuteNonQuery();
                                }

                                if (credited == "Cash" || credited == "Accounts Receivable" || credited == "Inventory" || credited == "Other Current Assets" ||
                                   credited == "Fixed Assets" || credited == "Other Assets" || credited == "Cost of Sales" || credited == "Expenses")
                                {
                                    Double bl1 = select_credit_balance(DropDownList1.SelectedItem.Text) - G;

                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + DropDownList1.SelectedItem.Text + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + txtExplanation.Text + "'+'-'+'" + ddl1.SelectedItem.Text + "','','0','" + txtAmount.Text + "','" + bl1 + "','" + txtDate.Text + "','" + DropDownList1.SelectedItem.Text + "','','" + credited + "')", con);
                                    cmd19.ExecuteNonQuery();

                                }
                                else
                                {
                                    Double bl1 = select_credit_balance(DropDownList1.SelectedItem.Text) + G; ;

                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + DropDownList1.SelectedItem.Text + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + txtExplanation.Text + "'+'-'+'" + ddl1.SelectedItem.Text + "','','0','" + txtAmount.Text + "','" + bl1 + "','" + txtDate.Text + "','" + DropDownList1.SelectedItem.Text + "','','" + credited + "')", con);
                                    cmd19.ExecuteNonQuery();
                                }


                            }
                        }

                    }
                    if (Checkbox2.Checked == true)
                    {
                        SqlCommand cmd4551 = new SqlCommand("Update tblPensionReport set pension='0'", con);
                        cmd4551.ExecuteNonQuery();
                    }
                    if (Checkbox1.Checked == true)
                    {
                        SqlCommand cmd4551 = new SqlCommand("Update tblPensionCompanyReport set pension='0'", con);
                        cmd4551.ExecuteNonQuery();
                    }
                    Response.Redirect("Journal.aspx");
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
                    Label lbl1 = item.FindControl("Label3") as Label;
                    Label lbl3 = item.FindControl("Label7") as Label;
                    Label lbl2 = item.FindControl("Label4") as Label;
                    Label lbl4 = item.FindControl("Label8") as Label;
                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("select * from tblGeneralJournal ", con);
                        SqlDataReader reader = cmd2.ExecuteReader();

                        if (reader.Read())
                        {
                            string ke;
                            string k1;


                            ke = reader["Debit"].ToString();
                            k1 = reader["Credit"].ToString();

                            reader.Close();
                            con.Close();

                            if (lbl.Text == "0.00")
                            {
                                lbl.ForeColor = Color.Red;
                                lbl.Text = "";

                            }
                            if (lbl.Text != "")
                            {
                                lbl.ForeColor = Color.Green;
                            }
                            if (lbl1.Text == "0.00")
                            {
                                lbl1.ForeColor = Color.Red;
                                lbl1.Text = "";
                            }
                            if (lbl1.Text != "")
                            {
                                lbl1.ForeColor = Color.DarkRed;

                            }
                            if (lbl3.Text == "01/01/1900")
                            {

                                lbl3.Text = lbl3.Text.Trim('/', '0', '1');
                            }
                            if (lbl4.Text == "Adj. Entry" || lbl4.Text == "Corrct. Entry")
                            {

                                lbl4.ForeColor = Color.Red;
                            }

                        }
                    }
                }
            }

        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblGeneralJournal where Date between CONVERT(datetime, '" + TextBox5.Text + "') AND CONVERT(datetime, '" + TextBox7.Text + "') AND Status='Not Posted'", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();

                    }
                }


                //

                con.Open();
                str = "select SUM(Debit) Debit,SUM(Credit) Credit from tblGeneralJournal where Status='Not Posted' AND Date between CONVERT(datetime, '" + TextBox5.Text + "') AND CONVERT(datetime, '" + TextBox7.Text + "')";
                com = new SqlCommand(str, con);
                ds = new DataSet();
                using (SqlDataAdapter sda = new SqlDataAdapter(com))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands);
                    Repeater3.DataSource = dtBrands;
                    Repeater3.DataBind();
                }
            }
            //
        }
        protected void Month(object sender, EventArgs e)
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("JournalMonthly", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 50).Value = Convert.ToInt64(DateTime.Now.Month);


                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();
                    }
                }
            }
        }
        protected void PrevMonth(object sender, EventArgs e)
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("JournalMonthly", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 50).Value = Convert.ToInt64(DateTime.Now.Month) - 1;


                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();
                    }
                }
            }
        }
        protected void year(object sender, EventArgs e)
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("JournalMonthly2", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 50).Value = Convert.ToInt64(DateTime.Now.Year);


                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();
                    }
                }
            }
        }
        protected void year1(object sender, EventArgs e)
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("JournalMonthly2", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Month", SqlDbType.NVarChar, 50).Value = Convert.ToInt64(DateTime.Now.Year) - 1;


                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();
                    }
                }
            }
        }
        protected void btnPyable_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                if (ddlPayableDebit.SelectedItem.Text == "-Select-" || ddlPayableDebit.SelectedItem.Text == "-Select-" || ddlVendor.SelectedItem.Text == "-Select-" || txtPayableAmount.Text == "" || txtPayableDate.Text == "")
                {
                    lblMsg.Text = "Please Fill All the required input"; lblMsg.ForeColor = Color.Red;
                }
                else
                {


                    con.Open();
                    string name = "Pending-Opening";
                    SqlCommand cmd2vb = new SqlCommand("select * from tblBills where CustomerNumber='" + ddlVendor.SelectedItem.Text + "' and Status LIKE '%" + name + "%'", con);
                    SqlDataReader readervb = cmd2vb.ExecuteReader();
                    if (readervb.Read())
                    {
                        string kc;

                        kc = readervb["BalanceDue"].ToString();


                        con.Close();
                        Double D = Convert.ToDouble(kc) - Convert.ToDouble(txtPayableAmount.Text);

                        SqlCommand cmd45vvb = new SqlCommand("Update tblBills set BalanceDue='" + D + "',Status='Opening-Completed'  where CustomerNumber='" + ddlVendor.SelectedItem.Text + "' and  Status LIKE '%" + name + "%'", con);
                        con.Open();
                        cmd45vvb.ExecuteNonQuery();
                    }
                    readervb.Close();
                    SqlCommand cmd190h7c = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlPayableDebit.SelectedItem.Text + "'", con);
                    using (SqlDataAdapter sda22c = new SqlDataAdapter(cmd190h7c))
                    {
                        DataTable dtBrands232c = new DataTable();
                        sda22c.Fill(dtBrands232c); int i2c = dtBrands232c.Rows.Count;
                        //
                        if (i2c != 0)
                        {
                            SqlCommand cmd166c = new SqlCommand("select * from tblLedgAccTyp where Name='" + ddlPayableDebit.SelectedItem.Text + "'", con);
                            SqlDataReader reader66c = cmd166c.ExecuteReader();

                            if (reader66c.Read())
                            {
                                string ah11c;
                                string ah1258c;
                                ah11c = reader66c["No"].ToString();

                                ah1258c = reader66c["AccountType"].ToString();
                                reader66c.Close();
                                con.Close();
                                con.Open();

                                SqlCommand cmd190cc = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlPayableDebit.SelectedItem.Text + "'", con);

                                SqlDataReader reader66790c = cmd190cc.ExecuteReader();

                                if (reader66790c.Read())
                                {
                                    string ah1289c;
                                    ah1289c = reader66790c["Balance"].ToString();
                                    reader66790c.Close();
                                    con.Close();
                                    con.Open();
                                    Double M1c = Convert.ToDouble(ah1289c);
                                    Double bl1c = M1c - Convert.ToDouble(txtPayableAmount.Text);
                                    string total = ddlVendor.SelectedItem.Text + '-' + ddlCashCredit.SelectedItem.Text;
                                    SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "'  where Account='" + ddlPayableDebit.SelectedItem.Text + "'", con);
                                    cmd45c.ExecuteNonQuery();
                                    SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','" + txtPayableAmount.Text + "','0','" + bl1c + "','" + txtPayableDate.Text + "','" + ddlPayableDebit.SelectedItem.Text + "','" + ah11c + "','" + ah1258c + "')", con);
                                    cmd1964c.ExecuteNonQuery();
                                }
                                //Creit account
                                SqlCommand cmdcr = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCashCredit.SelectedItem.Text + "'", con);

                                SqlDataReader readercr = cmdcr.ExecuteReader();

                                if (readercr.Read())
                                {
                                    string ah1289c;
                                    ah1289c = readercr["Balance"].ToString(); readercr.Close();
                                    reader66790c.Close();
                                    con.Close();
                                    con.Open();
                                    Double M1c = Convert.ToDouble(ah1289c);
                                    Double bl1c = M1c - Convert.ToDouble(txtPayableAmount.Text);
                                    SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "'  where Account='" + ddlCashCredit.SelectedItem.Text + "'", con);
                                    cmd45c.ExecuteNonQuery();
                                    string total = ddlVendor.SelectedItem.Text + '-' + ddlPayableDebit.SelectedItem.Text;
                                    SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','0','" + txtPayableAmount.Text + "','" + bl1c + "','" + txtPayableDate.Text + "','" + ddlCashCredit.SelectedItem.Text + "','" + ah11c + "','" + ah1258c + "')", con);
                                    cmd1964c.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    //Storing the files
                    if (FileUpload1.HasFile)
                    {
                        string SavePath = Server.MapPath("~/asset/payableattachment/");
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);
                        }
                        string Extention = Path.GetExtension(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(SavePath + "\\" + FileUpload1.FileName + Extention);

                        SqlCommand cmd3 = new SqlCommand("insert into tblJournalAttachment values('" + txtAttachmentName.Text + "','" + FileUpload1.FileName + "','" + Extention + "','" + txtPayableExplanation.Text + "','" + txtPayableDate.Text + "')", con);
                        cmd3.ExecuteNonQuery();
                    }
                    SqlCommand cmd16 = new SqlCommand("select * from tblLedgAccTyp where Name='" + ddlPayableDebit.SelectedItem.Text + "'", con);

                    SqlDataReader reader6 = cmd16.ExecuteReader();

                    if (reader6.Read())
                    {
                        string debited;
                        debited = reader6["AccountType"].ToString();
                        reader6.Close();
                        SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + ddlCashCredit.SelectedItem.Text + "'", con);


                        SqlDataReader reader66 = cmd166.ExecuteReader();

                        if (reader66.Read())
                        {
                            string credited;
                            credited = reader66["AccountType"].ToString();
                            reader66.Close();
                            SqlCommand cmd111 = new SqlCommand("insert into tblGeneralJournal values('" + txtPayableExplanation.Text + "','','" + txtPayableAmount.Text + "','','" + txtPayableDate.Text + "','','" + ddlPayableDebit.SelectedItem.Text + "','','" + debited + "')", con);
                            cmd111.ExecuteNonQuery();

                            SqlCommand cmd1112 = new SqlCommand("insert into tblGeneralJournal values('','','','" + txtPayableAmount.Text + "','" + txtPayableDate.Text + "','" + ddlCashCredit.SelectedItem.Text + "','','','" + credited + "')", con);
                            cmd1112.ExecuteNonQuery();
                        }
                    }


                    Response.Redirect("Journal.aspx");
                }
            }
        }
    }
}
