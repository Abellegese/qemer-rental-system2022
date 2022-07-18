using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class Ledger : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownList1.Items.Insert(0, new ListItem("Bba", ""));
            ddl2.Items.Insert(0, new ListItem("-Select-", "0"));
            if (Session["USERNAME"] != null)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "' ", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {

                    }
                }
                if (!IsPostBack)
                {

                    BindBrandsRptr(); bindRevenueExpense(); bindRevenueExpense();
                    BindMainCategory(); bindAssetLiability();
                    BindMainCategory2(); bindAssetLiability(); bindCash();
                    SystemDate.InnerText = "As of " + DateTime.Now.ToString("dd MMM, yyyy");
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private string bindAccountNumber(string account)
        {
            string no = string.Empty;
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + account + "'", con);

            SqlDataReader reader66 = cmd166.ExecuteReader();

            if (reader66.Read())
            {
                no = reader66["No"].ToString();
                reader66.Close();

            }
            con.Close();
            return no;
        }
        private void bindCash()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select (SUM(Debit)-SUM(Credit)) as Balance  from tblGeneralLedger where AccountType='Cash'";
            con.Open();
            Int64 year = DateTime.Now.Year;
            Int64 difYear1 = year - 1;
            Int64 difYear = year;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string cashbalance;
                cashbalance = reader["Balance"].ToString();

                reader.Close();
                con.Close();
                String querycurrent = "select (SUM(Debit)-SUM(Credit)) as Balance  from tblGeneralLedger where AccountType='Cash' and year(Date)='" + difYear + "'";
                con.Open();
                SqlCommand cmdc = new SqlCommand(querycurrent, con);
                SqlDataReader readerc = cmdc.ExecuteReader();
                if (readerc.Read())
                {
                    string cashcurrent;
                    cashcurrent = readerc["Balance"].ToString();

                    readerc.Close();
                    if (cashcurrent == "" || cashcurrent == null)
                    {
                        CashCurrent.InnerText = "0.00";
                    }
                    else
                    {
                        CashCurrent.InnerText = Convert.ToDouble(cashcurrent).ToString("#,##0.00");
                    }
                    con.Close();
                    String querylast = "select (SUM(Debit)-SUM(Credit)) as Balance  from tblGeneralLedger where AccountType='Cash' and year(Date)='" + difYear1 + "'";
                    con.Open();
                    SqlCommand cmdl = new SqlCommand(querylast, con);
                    SqlDataReader readerl = cmdl.ExecuteReader();
                    if (readerl.Read())
                    {
                        string cashlast;
                        cashlast = readerl["Balance"].ToString();

                        readerl.Close();
                        if (cashlast == "" || cashlast == null)
                        {
                            CashLast.InnerText = "0.00";
                        }
                        else
                        {
                            CashLast.InnerText = Convert.ToDouble(cashlast).ToString("#,##0.00");
                        }
                    }
                }
                if (cashbalance == "" || cashbalance == null)
                {
                    CashBalance.InnerText = "0.00";
                }
                else
                {
                    CashBalance.InnerText = Convert.ToDouble(cashbalance).ToString("#,##0.00");
                }
                //

            }
        }
        private void bindRevenueExpense()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select SUM(Credit) as Credit, SUM(Debit) as Debit, (SUM(Credit)-SUM(Debit)) as Balance  from tblGeneralLedger where AccountType='Income' group by year(Date)";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string income; string revenuedebit; string revenuecredit;
                income = reader["Balance"].ToString();
                revenuedebit = reader["Debit"].ToString();
                revenuecredit = reader["Credit"].ToString();
                reader.Close();
                con.Close();
                String query1 = "select SUM(Credit) as Credit, SUM(Debit) as Debit, (SUM(Debit)-SUM(Credit)) as Balance from tblGeneralLedger where AccountType='Expenses' OR AccountType='Cost of Sales' group by year(Date)";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    string expense; string expensedebit; string expensecredit;
                    expense = reader1["Balance"].ToString();
                    expensedebit = reader1["Debit"].ToString();
                    expensecredit = reader1["Credit"].ToString();
                    reader1.Close();
                    con.Close();
                    //Revenue
                    RevenueBalance.InnerText = Convert.ToDouble(income).ToString("#,##0.00");
                    RevenueDebit.InnerText = Convert.ToDouble(revenuedebit).ToString("#,##0.00");
                    RevenueCredit.InnerText = Convert.ToDouble(revenuecredit).ToString("#,##0.00");
                    //Expense
                    ExpenseBalance.InnerText = Convert.ToDouble(expense).ToString("#,##0.00");
                    ExpenseDebit.InnerText = Convert.ToDouble(expensedebit).ToString("#,##0.00");
                    ExpenseCredit.InnerText = Convert.ToDouble(expensecredit).ToString("#,##0.00");
                }
            }
        }
        private void bindAssetLiability()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select SUM(Debit) as Debit,SUM(Credit) as Credit, (SUM(Debit)-SUM(Credit)) as Balance  from tblGeneralLedger where AccountType='Accounts Receivable'  OR  AccountType='Cash' OR AccountType='Other Assets'  OR  AccountType='Inventory'  OR  AccountType='Other Current Assets'   OR  AccountType='Fixed Assets' group by year(Date)";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string asset; string assetdebit; string assetcredit;
                asset = reader["Balance"].ToString();
                assetdebit = reader["Debit"].ToString();
                assetcredit = reader["Credit"].ToString();
                reader.Close();
                con.Close();
                String query1 = "select SUM(Debit) as Debit,SUM(Credit) as Credit, (SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where AccountType='Accounts Payable' OR AccountType='Other Current Liabilities'  OR AccountType='Long Term Liabilities'   OR  AccountType='Accumulated Depreciation'";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    string liability; string liabcredit; string libilitydebit;
                    liability = reader1["Balance"].ToString();
                    libilitydebit = reader1["Debit"].ToString();
                    liabcredit = reader1["Credit"].ToString();
                    reader1.Close();
                    con.Close();
                    //Asset

                    AssetBalance.InnerText = Convert.ToDouble(asset).ToString("#,##0.00");
                    AssetDebit.InnerText = Convert.ToDouble(assetdebit).ToString("#,##0.00");
                    AssetCredit.InnerText = Convert.ToDouble(assetcredit).ToString("#,##0.00");
                    //Liability

                    LiabilityBlance.InnerText = Convert.ToDouble(liability).ToString("#,##0.00");
                    LiabilityDebit.InnerText = Convert.ToDouble(libilitydebit).ToString("#,##0.00");
                    LiabilittCredit.InnerText = Convert.ToDouble(liabcredit).ToString("#,##0.00");
                }
            }
        }
        private void BindMainCategory()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp", con);
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
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddl2.DataSource = dt;
                    ddl2.DataTextField = "Name";
                    ddl2.DataValueField = "ACT";
                    ddl2.DataBind();
                    ddl2.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void BindBrandsRptr()
        {
            if (Request.QueryString["asset"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblGeneralLedger2 where AccountType='Fixed Assets' or AccountType='Other Assets' or AccountType='Cash' or AccountType='Other Current Assets' or AccountType='Inventory' or AccountType='Accounts Receivable' ";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 25;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
            }
            else if (Request.QueryString["liability"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblGeneralLedger2 where AccountType='Accounts Payable' or AccountType='Other Current Liabilities' or AccountType='Long Term Liabilities' ";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 25;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
            }
            else if (Request.QueryString["income"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblGeneralLedger2 where AccountType='Income'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 25;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
            }
            else if (Request.QueryString["expense"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblGeneralLedger2 where AccountType='Expenses' or AccountType='Cost of Sales'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 25;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
            }
            else if (Request.QueryString["accdep"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblGeneralLedger2 where AccountType='Accumulated Depreciation'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 25;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblGeneralLedger2";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 25;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
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
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd16 = new SqlCommand("select * from tblLedgAccTyp where Name='" + ddl1.SelectedItem.Text + "'", con);
                SqlDataReader reader6 = cmd16.ExecuteReader();

                if (reader6.Read())
                {
                    string ah;
                    string ah1;

                    ah = reader6["Name"].ToString();
                    ah1 = reader6["No"].ToString();

                    reader6.Close();
                    con.Close();
                    con.Open();
                    if (txtCredit.Text != "")
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblGeneralLedger values('" + txtExplanation.Text + "','" + txtRef.Text + "','0','" + txtCredit.Text + "','" + txtDate.Text + "','" + ah + "','" + ah1 + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                    if (txtDebit.Text != "")
                    {

                        SqlCommand cmd111 = new SqlCommand("insert into tblGeneralLedger values('" + txtExplanation.Text + "','" + txtRef.Text + "','" + txtDebit.Text + "','0','" + txtDate.Text + "','" + ah + "','" + ah1 + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                }
                Response.Redirect("Ledger.aspx");
            }
        }
        protected void btnUpdate2_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "')", con);
                con.Open();
                cmd111.ExecuteNonQuery();
                Response.Redirect("Ledger.aspx");
            }
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblGeneralLedger where Date between CONVERT(datetime, '" + TextBox5.Text + "') AND CONVERT(datetime, '" + TextBox7.Text + "') AND Account='" + ddl2.SelectedItem.Text + "'", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();

                    }
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblGeneralLedger2 where Account LIKE '%" + txtAccountName.Text + "%'", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();

                    }
                }
            }
        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblGeneralLedger2", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        Repeater1.DataSource = dtBrands;
                        Repeater1.DataBind();

                    }
                }
            }
            String PID = "report";
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Ledger_" + PID + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/x-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            con1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        private string GenerateCSV(string amount)
        {
            string csv = string.Empty;

            csv += " Date,Reference,Date Clear in Bank Rec,Number of Distributions,G/L Account,Description,Amount,Job ID,Used for Reimbursable Expenses,Transaction Period,Transaction Number,Consolidated Transaction,Recur Number,Recur Frequency,";
            csv += "\r\n";
            csv += amount;
            return csv;
        }
        private void bindCsvData()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblGeneralLedger where Date between '"+txtCsvDateFrom.Text+"' and '"+txtCsvDateTo.Text+"'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater2.DataSource = dt;
            Repeater2.DataBind();
            con.Close();
        }
        private void bindCsvDataForGJ()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select Date,Explanation,Account,Debit,Credit,Balance from tblGeneralLedger where Date between CONVERT(datetime, '" + txtGJDateFrom.Text + "') AND CONVERT(datetime, '" + txtGJDateTo.Text + "')", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        string csv = string.Empty;
                        foreach (DataColumn column in dtBrands.Columns)
                        {
                            //Add the Header row for CSV file.
                            csv += column.ColumnName + ',';
                        }
                        csv += "\r\n";
                        foreach (DataRow row in dtBrands.Rows)
                        {
                            foreach (DataColumn column in dtBrands.Columns)
                            {
                                //Add the Data rows.
                                csv += row[column.ColumnName].ToString() + ',';
                            }

                            //Add new line.
                            csv += "\r\n";
                        }
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename=General_Journal_Custom.csv");
                        
                        Response.Charset = "";
                        Response.ContentType = "application/text";
                        Response.Output.Write(csv);
                        Response.Flush();
                        Response.End();
                        con.Close();

                    }
                }
            }
     
        }
        private void DownLoadCSVFile(string csv)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GeneralJournal.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            bindCsvData();
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                string csvValues = "";
                string amountCollector = string.Empty;
                string dateCollector = string.Empty;
                string descriptionCollector = string.Empty;
                foreach (RepeaterItem item in Repeater2.Items)
                {
                    Label Account = item.FindControl("lblID") as Label;
                    Label AccountName = item.FindControl("lblAccount") as Label;
                    Label AccountNumber = item.FindControl("lblAccountNumber") as Label;
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        Label Amount = item.FindControl("lblAmount") as Label;
                        SqlCommand cmdcrd = new SqlCommand("select * from tblGeneralLedger where LedID = '" + Account.Text + "'", con);
                        SqlDataReader readercrd = cmdcrd.ExecuteReader();
                        if (readercrd.Read())
                        {
                            string date = Convert.ToDateTime(readercrd["Date"].ToString()).ToString("MM/dd/yyyy");
                            string description = readercrd["Explanation"].ToString();
                            double debit = Convert.ToDouble(readercrd["Debit"].ToString());
                            double credit = Convert.ToDouble(readercrd["Credit"].ToString());
                            string accountType = readercrd["AccountType"].ToString();
                            AccountNumber.Text = bindAccountNumber(AccountName.Text);
                            if (debit != 0)
                            {
                                Amount.Text = debit.ToString();
                                csvValues += date + "," + " " + "," + " " + "," + "3" + "," + AccountNumber.Text + "," + description + "," + Amount.Text + "," + " " + "," + "FALSE" + "," + " " + "," + " " + "," + "FALSE" + "," + "0" + "," + "0" +","+ "\r\n";
                            }
                            else
                            {
                                Amount.Text = (-credit).ToString();
                                csvValues += date + "," + " " + "," + " " + "," + "3" + "," + AccountNumber.Text + "," + description + "," + Amount.Text + "," + " " + "," + "FALSE" + "," + " " + "," + " " + "," + "FALSE" + "," + "0" + "," + "0" + "," + "\r\n";
                            }
                        }
                        readercrd.Close();
                    }
                }
                con.Close();


                DownLoadCSVFile(GenerateCSV(csvValues));
            }
            
        }
        protected void btnGJ_Click(object sender, EventArgs e)
        {
            bindCsvDataForGJ();
        }
    }
}