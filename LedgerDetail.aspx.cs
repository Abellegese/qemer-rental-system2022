using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class LedgerDetail : System.Web.UI.Page
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
                    ViewState["Column"] = "date";

                    ViewState["Sortorder"] = "DESC";
                    BindBrandsRptr(); bindcompany(); mont.InnerText = DateTime.Now.ToString("MMMM dd, yyyy");
                    BindBalance(); bindledger();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }
        private void BindEndingBalanceDateRange()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand cmdcredit = new SqlCommand("select TOP(1) balance from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' order by LedID DESC", con);
                SqlDataReader readercredit = cmdcredit.ExecuteReader();
                if (readercredit.Read())
                {
                    string debitR = readercredit["balance"].ToString();
                    if (debitR == "" || debitR == null)
                    {
                        TotDebitor.InnerText = "0.00";
                        TotalCreditor.InnerText = "0.00";
                        TotBala.InnerText = "0.00";
                    }
                    else
                    {
                        double balance = Convert.ToDouble(readercredit["balance"].ToString());

                        TotBala.InnerText = balance.ToString("#,##0.00");

                    }
                    readercredit.Close();
                }
            }
        }
        private void BindBalanceDateRange()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + PID + "'", con);

                SqlDataReader reader66 = cmd166.ExecuteReader();

                if (reader66.Read())
                {
                    string debited;
                    debited = reader66["AccountType"].ToString();
                    reader66.Close();
                    if (debited == "Cash" || debited == "Accounts Receivable" || debited == "Inventory" || debited == "Other Current Assets" ||
                          debited == "Fixed Assets" || debited == "Other Assets" || debited == "Cost of Sales" || debited == "Expenses")
                    {
                        SqlCommand cmdcredit = new SqlCommand("select sum(Debit) debit, sum(Credit) credit from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "'", con);
                        SqlDataReader readercredit = cmdcredit.ExecuteReader();
                        if (readercredit.Read())
                        {
                            string debitR = readercredit["debit"].ToString();
                            if (debitR == "" || debitR == null)
                            {
                                TotDebitor.InnerText = "0.00";
                                TotalCreditor.InnerText = "0.00";
                                TotBala.InnerText = "0.00";
                            }
                            else
                            {
                                double debit = Convert.ToDouble(readercredit["debit"].ToString());
                                double credit = Convert.ToDouble(readercredit["credit"].ToString());

                                readercredit.Close();
                                TotDebitor.InnerText = debit.ToString("#,##0.00");
                                TotalCreditor.InnerText = credit.ToString("#,##0.00");

                            }
                        }
                    }
                    else
                    {
                        SqlCommand cmdcredit = new SqlCommand("select sum(credit) debit, sum(debit) credit,sum(Credit)-sum(Credit) as balance from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "'", con);
                        SqlDataReader readercredit = cmdcredit.ExecuteReader();
                        if (readercredit.Read())
                        {
                            string debitR = readercredit["credit"].ToString();
                            if (debitR == "" || debitR == null)
                            {
                                TotDebitor.InnerText = "0.00";
                                TotalCreditor.InnerText = "0.00";

                            }
                            else
                            {
                                double debit = Convert.ToDouble(readercredit["debit"].ToString());
                                double credit = Convert.ToDouble(readercredit["credit"].ToString());

                                readercredit.Close();
                                TotDebitor.InnerText = debit.ToString("#,##0.00");
                                TotalCreditor.InnerText = credit.ToString("#,##0.00");

                            }
                        }
                    }
                }
            }
        }
        private void BindBalance()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + PID + "'", con);

                SqlDataReader reader66 = cmd166.ExecuteReader();

                if (reader66.Read())
                {
                    string debited;
                    debited = reader66["AccountType"].ToString();
                    reader66.Close();
                    if (debited == "Cash" || debited == "Accounts Receivable" || debited == "Inventory" || debited == "Other Current Assets" ||
                          debited == "Fixed Assets" || debited == "Other Assets" || debited == "Cost of Sales" || debited == "Expenses")
                    {
                        SqlCommand cmdcredit = new SqlCommand("select sum(Debit) debit, sum(Credit) credit from tblGeneralLedger where Account='" + PID + "'", con);
                        SqlDataReader readercredit = cmdcredit.ExecuteReader();
                        if (readercredit.Read())
                        {
                            double debit = Convert.ToDouble(readercredit["debit"].ToString());
                            double credit = Convert.ToDouble(readercredit["credit"].ToString());
                            double balance = debit - credit;
                            readercredit.Close();
                            TotDebitor.InnerText = debit.ToString("#,##0.00");
                            TotalCreditor.InnerText = credit.ToString("#,##0.00");
                            TotBala.InnerText = balance.ToString("#,##0.00");
                        }
                    }
                    else
                    {
                        SqlCommand cmdcredit = new SqlCommand("select sum(credit) debit, sum(debit) credit from tblGeneralLedger where Account='" + PID + "'", con);
                        SqlDataReader readercredit = cmdcredit.ExecuteReader();
                        if (readercredit.Read())
                        {
                            double debit = Convert.ToDouble(readercredit["debit"].ToString());
                            double credit = Convert.ToDouble(readercredit["credit"].ToString());
                            double balance = debit - credit;
                            readercredit.Close();
                            TotDebitor.InnerText = debit.ToString("#,##0.00");
                            TotalCreditor.InnerText = credit.ToString("#,##0.00");
                            TotBala.InnerText = balance.ToString("#,##0.00");
                            deb.InnerText = "CREDIT";
                            cre.InnerText = "DEBIT";
                        }
                    }
                }
            }
        }
        private void BindBrandsRptr()
        {
            if (Request.QueryString["led"] != null)
            {

                String PID = Convert.ToString(Request.QueryString["led"]);

                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblGeneralLedger where Account='" + PID + "'";
                account.InnerText = PID;
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    OpeningBal.InnerText = Convert.ToDouble(dt.Rows[0][5].ToString()).ToString("#,##0.00");
                }
                DataView dvData = new DataView(dt);
                dvData.Sort = ViewState["Column"] + " " + ViewState["Sortorder"];
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = dvData;
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

            BindBrandsRptr();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindBrandsRptr();
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
                    string company; string bl; string contact1;
                    company = reader["Oname"].ToString();
                    oname.InnerText = company;
                    bl = reader["BuissnessLocation"].ToString();
                    contact1 = reader["Contact"].ToString();
                    CompAddress.InnerText = bl;
                    Contact.InnerText = contact1;

                }
            }
        }
        private string BindDate()
        {
            string FirstDate = "";
            String PID = Convert.ToString(Request.QueryString["led"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select TOP (1) *from tblGeneralLedger where Account='" + PID + "' ORDER BY LedID ASC", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                        if (i != 0)
                        {
                            FirstDate = Convert.ToDateTime(dtBrands.Rows[0][6].ToString()).ToString("dd MMM yyyy");


                        }
                    }
                }
            }
            return FirstDate;
        }
        private void CalculateEndingBalance()
        {
            if (Request.QueryString["led"] != null)
            {

                String PID = Convert.ToString(Request.QueryString["led"]);
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                DateTime dTimeLast = Convert.ToDateTime(txtDateform.Text).AddDays(-1);
                DateTime dTimeFirst = Convert.ToDateTime(BindDate());
                str = "select* from tblGeneralLedger where Account='" + PID + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";
                account.InnerText = PID;
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    OpeningBal.InnerText = Convert.ToDouble(dt.Rows[0][5].ToString()).ToString("#,##0.00");
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDateform.Text == "" || txtDateto.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                if (Request.QueryString["led"] != null)
                {
                    BindBalanceDateRange(); BindEndingBalanceDateRange();
                    String PID = Convert.ToString(Request.QueryString["led"]);
                    SqlConnection con = new SqlConnection(strConnString);
                    con.Open();

                    datefrom.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy"); dateto1.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy"); between.Visible = true; asof.Visible = false;
                    str = "select * from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "'";

                    bindledgerdate();
                    account.InnerText = PID;
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);

                    DataView dvData = new DataView(dt);
                    sqlda.Fill(ds, "Cat");
                    PagedDataSource Pds1 = new PagedDataSource();
                    Pds1.DataSource = ds.Tables[0].DefaultView;
                    Pds1.AllowPaging = true;
                    Pds1.PageSize = 60;
                    Pds1.CurrentPageIndex = CurrentPage;
                    Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                    btnPrevious.Enabled = !Pds1.IsFirstPage;
                    btnNext.Enabled = !Pds1.IsLastPage;
                    Repeater1.DataSource = Pds1;
                    Repeater1.DataBind();
                    con.Close(); CalculateEndingBalance();
                }
            }
        }
        protected void Button14_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                lblMsg1.Text = "Please enter the amount to be adjusted";
                lblMsg1.ForeColor = Color.Red;
            }
            else
            {
                String PID = Convert.ToString(Request.QueryString["led"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + PID + "'", con);

                    SqlDataReader reader66 = cmd166.ExecuteReader();

                    if (reader66.Read())
                    {
                        string debited;
                        debited = reader66["AccountType"].ToString();
                        reader66.Close();

                        SqlCommand cmdcredit = new SqlCommand("select * from tblGeneralLedger2 where Account='" + PID + "'", con);
                        SqlDataReader readercredit = cmdcredit.ExecuteReader();
                        if (readercredit.Read())
                        {
                            double debitbalance = Convert.ToDouble(readercredit["Balance"].ToString());
                            readercredit.Close();
                            double G = Convert.ToDouble(txtAmount.Text);
                            if (G < 0)
                            {
                                if (debited == "Cash" || debited == "Accounts Receivable" || debited == "Inventory" || debited == "Other Current Assets" ||
        debited == "Fixed Assets" || debited == "Other Assets" || debited == "Cost of Sales" || debited == "Expenses")
                                {
                                    Double bl1 = debitbalance + G;
                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + PID + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values(N'" + txtRemark.Text + "','','0','" + -G + "','" + bl1 + "','" + DateTime.Now.Date + "','" + PID + "','','" + debited + "')", con);
                                    cmd19.ExecuteNonQuery();

                                }
                                else
                                {
                                    Double bl1 = debitbalance + G;
                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + PID + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values(N'" + txtRemark.Text + "','','" + -G + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + PID + "','','" + debited + "')", con);
                                    cmd19.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                if (debited == "Cash" || debited == "Accounts Receivable" || debited == "Inventory" || debited == "Other Current Assets" ||
        debited == "Fixed Assets" || debited == "Other Assets" || debited == "Cost of Sales" || debited == "Expenses")
                                {
                                    Double bl1 = debitbalance + G;
                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + PID + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values(N'" + txtRemark.Text + "','','" + txtAmount.Text + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + PID + "','','" + debited + "')", con);
                                    cmd19.ExecuteNonQuery();

                                }
                                else
                                {
                                    Double bl1 = debitbalance + G;
                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + PID + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values(N'" + txtRemark.Text + "','','0','" + txtAmount.Text + "','" + bl1 + "','" + DateTime.Now.Date + "','" + PID + "','','" + debited + "')", con);
                                    cmd19.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    Response.Redirect("LedgerDetail.aspx?led=" + PID);
                }
            }
        }

        protected void btnAmountCondition_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                if (Credit.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Credit > '" + txtFilteredAmount.Text + "' and Account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else if (Debit.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Debit > '" + txtFilteredAmount.Text + "' and Account='" + PID + "'";
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
                    str = "select * from tblGeneralLedger where Balance > '" + txtFilteredAmount.Text + "' and Account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
            else if (less.Checked == true)
            {
                if (Credit.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Credit < '" + txtFilteredAmount.Text + "' and Account='" + PID + "'  and Credit >0 and Account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else if (Debit.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Debit < '" + txtFilteredAmount.Text + "' and Account='" + PID + "' and  Debit >0 and Account='" + PID + "'";
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
                    str = "select * from tblGeneralLedger where Balance < '" + txtFilteredAmount.Text + "' and Account='" + PID + "' and  Balance > 0 and Account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
            else
            {
                if (Credit.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Credit = '" + txtFilteredAmount.Text + "' and Account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else if (Debit.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Debit = '" + txtFilteredAmount.Text + "' and Account='" + PID + "'";
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
                    str = "select * from tblGeneralLedger where Balance = '" + txtFilteredAmount.Text + "' and Account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
        }
        private void bindledger()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblGeneralLedger where Account='" + PID + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            DataView dvData = new DataView(dt);
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        private void bindledgerdate()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblGeneralLedger where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Account='" + PID + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            DataView dvData = new DataView(dt);
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == ViewState["Column"].ToString())
            {
                if (ViewState["Sortorder"].ToString() == "ASC")
                    ViewState["Sortorder"] = "DESC";
                else
                    ViewState["Sortorder"] = "ASC";
            }
            else
            {
                ViewState["Column"] = e.CommandName;
                ViewState["Sortorder"] = "ASC";
            }
            BindBrandsRptr();
        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            con1.Visible = true;
            String PID = Convert.ToString(Request.QueryString["led"]);
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
    }
}