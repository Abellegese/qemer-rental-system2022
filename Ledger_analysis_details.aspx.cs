using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class Ledger_analysis_details : System.Web.UI.Page
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
                    InvoiceDiv.Visible = false; bindsearch();
                    BindRecentActivity(); BindBalanceDateRange();
                    BindDate(); BindAccountType(); ShowData(); ShowDataIncreaseMonthly();
                    ShowDataDecreaseMonthly(); bindReneameDelete();

                    InTrendDate.InnerText = "[" + DateTime.Now.ToString("yyyy") + "]";
                    Dec.InnerText = "[" + DateTime.Now.ToString("yyyy") + "]";
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindReneameDelete()
        {
            if (Request.QueryString["led"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["led"]);
                if (PID == "Cash at Bank")
                {
                    renameLink.Visible = false;
                    deleteLink.Visible = false;
                }
                if (PID == "Cash on Hand")
                {
                    renameLink.Visible = false;
                    deleteLink.Visible = false;
                }
                if (PID == "Accounts Receivable")
                {
                    renameLink.Visible = false;
                    deleteLink.Visible = false;
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
                    SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Name LIKE '%" + PID + "%'", con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            SqlDataReader reader22 = cmd.ExecuteReader();
                            if (reader22.Read())
                            {
                                string pstatus;
                                //Shop Details
                                pstatus = reader22["Name"].ToString(); reader22.Close();
                                Response.Redirect("Ledger_analysis_details.aspx?led=" + pstatus);
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
        private string BindDate2()
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
        private double CalculateEndingBalance()
        {
            double balance = 0;
            if (Request.QueryString["led"] != null)
            {

                String PID = Convert.ToString(Request.QueryString["led"]);
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                DateTime dTimeLast = Convert.ToDateTime(txtSmDateFrom.Text).AddDays(-1);
                DateTime dTimeFirst = Convert.ToDateTime(BindDate2());
                str = "select* from tblGeneralLedger where Account='" + PID + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    balance = Convert.ToDouble(dt.Rows[0][5].ToString());
                    OpBalance.InnerText = "Beg. " + Convert.ToDouble(dt.Rows[0][5].ToString()).ToString("#,##0.00");
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
                SqlCommand cmdpos = new SqlCommand("select*from tblDashBoardSetting", con);
                SqlDataReader readerpos = cmdpos.ExecuteReader();

                if (readerpos.Read())
                {
                    String type = readerpos["accountoption"].ToString(); readerpos.Close();
                    if (type == "This Year")
                    {
                        ShowDataYearly();
                    }
                    else if (type == "Monthly")
                    {
                        ShowDataMonthly();
                    }
                    else
                    {
                        ShowDataQuarterly();
                    }
                }
            }
        }
        private void BindBalanceDateRange2()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            if (txtSmDateFrom.Text == "" || txtSmDateTo.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
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
                            SqlCommand cmdcredit = new SqlCommand("select sum(Debit) debit, sum(Credit) credit from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtSmDateFrom.Text + "' and '" + txtSmDateTo.Text + "'", con);
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
                                    double balance = debit - credit;
                                    readercredit.Close();
                                    TotDebitor.InnerText = debit.ToString("#,##0.00");
                                    TotalCreditor.InnerText = credit.ToString("#,##0.00");
                                    TotBala.InnerText = (balance + CalculateEndingBalance()).ToString("#,##0.00");
                                }
                            }
                        }
                        else
                        {
                            SqlCommand cmdcredit = new SqlCommand("select sum(credit) debit, sum(debit) credit from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtSmDateFrom.Text + "' and '" + txtSmDateTo.Text + "'", con);
                            SqlDataReader readercredit = cmdcredit.ExecuteReader();
                            if (readercredit.Read())
                            {
                                string debitR = readercredit["credit"].ToString();
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
                                    double balance = debit - credit;
                                    readercredit.Close();
                                    TotDebitor.InnerText = debit.ToString("#,##0.00");
                                    TotalCreditor.InnerText = credit.ToString("#,##0.00");
                                    TotBala.InnerText = (balance + CalculateEndingBalance()).ToString("#,##0.00");
                                }
                            }
                        }
                    }
                }
                datFrom.InnerText = Convert.ToDateTime(txtSmDateFrom.Text).ToString("dd MMM yyyy");
                datTo.InnerText = Convert.ToDateTime(txtSmDateTo.Text).ToString("dd MMM yyyy");
                Dec.InnerHtml = Convert.ToDateTime(txtSmDateFrom.Text).ToString("dd MMM yyyy") + " - " + Convert.ToDateTime(txtSmDateTo.Text).ToString("dd MMM yyyy");
                InTrendDate.InnerHtml = Convert.ToDateTime(txtSmDateFrom.Text).ToString("dd MMM yyyy") + " - " + Convert.ToDateTime(txtSmDateTo.Text).ToString("dd MMM yyyy");
                BegSpan.Visible = true;
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
                        SqlCommand cmdcredit = new SqlCommand("select sum(Debit) debit, sum(Credit) credit from tblGeneralLedger where Account='" + PID + "'", con);
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
                                double balance = debit - credit;
                                readercredit.Close();
                                TotDebitor.InnerText = debit.ToString("#,##0.00");
                                TotalCreditor.InnerText = credit.ToString("#,##0.00");
                                TotBala.InnerText = balance.ToString("#,##0.00");
                                TextBox2.Text = balance.ToString("##0.00");
                            }
                        }
                    }
                    else
                    {
                        SqlCommand cmdcredit = new SqlCommand("select sum(credit) debit, sum(debit) credit from tblGeneralLedger where Account='" + PID + "'", con);
                        SqlDataReader readercredit = cmdcredit.ExecuteReader();
                        if (readercredit.Read())
                        {
                            string debitR = readercredit["credit"].ToString();
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
                                double balance = debit - credit;
                                readercredit.Close();
                                TotDebitor.InnerText = debit.ToString("#,##0.00");
                                TotalCreditor.InnerText = credit.ToString("#,##0.00");
                                TotBala.InnerText = balance.ToString("#,##0.00");
                                TextBox2.Text = balance.ToString("##0.00");
                            }
                        }
                    }
                }
            }
        }
        private void BindAccountType()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            AccountName.InnerText = PID; AccName.InnerText = PID;
            A6.HRef = "LedgerDetail.aspx?led=" + PID;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblGeneralLedger where Account='" + PID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string debited = reader["AccountType"].ToString();
                    AccType.InnerText = debited;
                }
            }
        }
        private void ShowDataYearly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select sum(Debit)-sum(Credit) as Total,year(Date) as month_name,year(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-charte\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-charte\"), { type: 'bar', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
                else
                {
                    String query = "select sum(Credit)-sum(Debit) as Total,year(Date) as month_name,year(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-charte\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-charte\"), { type: 'bar', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
            }
        }
        private void ShowDataMonthly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select year(Date),sum(Debit)-sum(Credit) as Total,sum(Debit) as Debit , sum(Credit) as Credit, DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   month(Date),year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartm\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartm\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Balance\",display: false,borderColor: \"#ffd800\",backgroundColor: [\"#ffd800\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color


                            //Increase trends
                            chart += "{data: [";
                            string value1 = "";
                            for (int i1 = 0; i1 < dtBrands232c3.Rows.Count; i1++)
                                value1 += (Convert.ToDouble(dtBrands232c3.Rows[i1]["Debit"]) / 1000000).ToString() + ",";
                            chart += value1;

                            chart += "],label: \"Increasing Trends\",display: false,borderColor: \"#4cff00\",backgroundColor: \"#4cff00\",hoverBackgroundColor: \"#4cff00\"},"; // Chart color
                                                                                                                                                                                //END INCREASE TRENDS

                            //Decrease trends
                            chart += "{data: [";
                            string value2 = "";
                            for (int i1 = 0; i1 < dtBrands232c3.Rows.Count; i1++)
                                value2 += (Convert.ToDouble(dtBrands232c3.Rows[i1]["Credit"]) / 1000000).ToString() + ",";
                            chart += value2;

                            chart += "],label: \"Decreasing Trends\",display: false,borderColor: \"#ff0000\",backgroundColor: \"#ff0000\",hoverBackgroundColor: \"#ff0000\"},"; // Chart color
                                                                                                                                                                                //END DECREASE TRENDS
                            chart += "]";
                            chart += "},";

                            chart += "options: { maintainAspectRatio: false,layout: {padding: {left: 0,right: 0,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 200},maxBarThickness: 25,}],},legend: {display: false, position:'right'},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
                else
                {
                    String query = "select year(Date),sum(Credit)-sum(Debit) as Total,sum(Debit) as Debit , sum(Credit) as Credit, DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   month(Date),year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartm\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartm\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Balance\",display: false,borderColor: \"#ffd800\",backgroundColor: [\"#ffd800\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color

                            //Increase trends
                            chart += "{data: [";
                            string value1 = "";
                            for (int i1 = 0; i1 < dtBrands232c3.Rows.Count; i1++)
                                value1 += (Convert.ToDouble(dtBrands232c3.Rows[i1]["Credit"]) / 1000000).ToString() + ",";
                            chart += value1;

                            chart += "],label: \"Increasing Trends\",display: false,borderColor: \"#4cff00\",backgroundColor: \"#4cff00\",hoverBackgroundColor: \"#4cff00\"},"; // Chart color
                                                                                                                                                                                //END INCREASE TRENDS

                            //Decrease trends
                            chart += "{data: [";
                            string value2 = "";
                            for (int i1 = 0; i1 < dtBrands232c3.Rows.Count; i1++)
                                value2 += (Convert.ToDouble(dtBrands232c3.Rows[i1]["Debit"]) / 1000000).ToString() + ",";
                            chart += value2;

                            chart += "],label: \"Decreasing Trends\",display: false,borderColor: \"#ff0000\",backgroundColor: \"#ff0000\",hoverBackgroundColor: \"#ff0000\"},"; // Chart color
                                                                                                                                                                                //END DECREASE TRENDS
                            chart += "]";
                            chart += "},";

                            chart += "options: { maintainAspectRatio: false,layout: {padding: {left: 0,right: 0,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 200},maxBarThickness: 25,}],},legend: {display: false, position:'right'},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
            }

        }
        private void ShowDataMonthlyDateRange()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select year(Date),sum(Debit)-sum(Credit) as Total,sum(Debit) as Debit , sum(Credit) as Credit, DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' and Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' group by   month(Date),year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartm\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartm\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Balance\",display: false,borderColor: \"#ffd800\",backgroundColor: [\"#ffd800\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color


                            //Increase trends
                            chart += "{data: [";
                            string value1 = "";
                            for (int i1 = 0; i1 < dtBrands232c3.Rows.Count; i1++)
                                value1 += (Convert.ToDouble(dtBrands232c3.Rows[i1]["Debit"]) / 1000000).ToString() + ",";
                            chart += value1;

                            chart += "],label: \"Increasing Trends\",display: false,borderColor: \"#4cff00\",backgroundColor: \"#4cff00\",hoverBackgroundColor: \"#4cff00\"},"; // Chart color
                                                                                                                                                                                //END INCREASE TRENDS

                            //Decrease trends
                            chart += "{data: [";
                            string value2 = "";
                            for (int i1 = 0; i1 < dtBrands232c3.Rows.Count; i1++)
                                value2 += (Convert.ToDouble(dtBrands232c3.Rows[i1]["Credit"]) / 1000000).ToString() + ",";
                            chart += value2;

                            chart += "],label: \"Decreasing Trends\",display: false,borderColor: \"#ff0000\",backgroundColor: \"#ff0000\",hoverBackgroundColor: \"#ff0000\"},"; // Chart color
                                                                                                                                                                                //END DECREASE TRENDS
                            chart += "]";
                            chart += "},";

                            chart += "options: { maintainAspectRatio: false,layout: {padding: {left: 0,right: 0,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 200},maxBarThickness: 25,}],},legend: {display: false, position:'right'},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false; ltChart.Visible = true;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
                else
                {
                    String query = "select year(Date),sum(Credit)-sum(Debit) as Total,sum(Debit) as Debit , sum(Credit) as Credit, DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' and Date between '" + txtCHDateFrom.Text + "' and '" + txtCHDateTo.Text + "' group by   month(Date),year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartm\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartm\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Balance\",display: false,borderColor: \"#ffd800\",backgroundColor: [\"#ffd800\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color

                            //Increase trends
                            chart += "{data: [";
                            string value1 = "";
                            for (int i1 = 0; i1 < dtBrands232c3.Rows.Count; i1++)
                                value1 += (Convert.ToDouble(dtBrands232c3.Rows[i1]["Credit"]) / 1000000).ToString() + ",";
                            chart += value1;

                            chart += "],label: \"Increasing Trends\",display: false,borderColor: \"#4cff00\",backgroundColor: \"#4cff00\",hoverBackgroundColor: \"#4cff00\"},"; // Chart color
                                                                                                                                                                                //END INCREASE TRENDS

                            //Decrease trends
                            chart += "{data: [";
                            string value2 = "";
                            for (int i1 = 0; i1 < dtBrands232c3.Rows.Count; i1++)
                                value2 += (Convert.ToDouble(dtBrands232c3.Rows[i1]["Debit"]) / 1000000).ToString() + ",";
                            chart += value2;

                            chart += "],label: \"Decreasing Trends\",display: false,borderColor: \"#ff0000\",backgroundColor: \"#ff0000\",hoverBackgroundColor: \"#ff0000\"},"; // Chart color
                                                                                                                                                                                //END DECREASE TRENDS
                            chart += "]";
                            chart += "},";

                            chart += "options: { maintainAspectRatio: false,layout: {padding: {left: 0,right: 0,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 200},maxBarThickness: 25,}],},legend: {display: false, position:'right'},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false; ltChart.Visible = true;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
            }

        }
        //Increase and decrease trends for date range search
        private void ShowDataIncreaseDateRMonthly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select year(Date),sum(Debit) as Total,DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' and Date between '" + txtSmDateFrom.Text + "' and '" + txtSmDateTo.Text + "' group by   month(Date),year(Date) order by  month_number";
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
                            chart += "new Chart(document.getElementById(\"line-chartin\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Increased Amount\",display: false,borderColor: \"#99FF66\",backgroundColor: [\"#99FF66\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 200},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltrIncreaseTrends.Text = chart; main2.Visible = false; ltrIncreaseTrends.Visible = true; IncDiv.Visible = true;
                        }
                        else
                        {
                            main2.Visible = true; ltrIncreaseTrends.Visible = false; IncDiv.Visible = false; VIT2.Visible = false;
                        }


                    }
                }
                else
                {
                    String query = "select year(Date),sum(Credit) as Total,DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' and Date between '" + txtSmDateFrom.Text + "' and '" + txtSmDateTo.Text + "' group by   month(Date),year(Date) order by  month_number";
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
                            chart += "new Chart(document.getElementById(\"line-chartin\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Increased Amount\",display: false,borderColor: \"#99FF66\",backgroundColor: [\"#99FF66\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 200},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltrIncreaseTrends.Text = chart; main2.Visible = false; ltrIncreaseTrends.Visible = true; IncDiv.Visible = true;
                        }
                        else
                        {
                            main2.Visible = true; ltrIncreaseTrends.Visible = false; IncDiv.Visible = false; VIT2.Visible = false;
                        }


                    }
                }
            }

        }
        private void ShowDataDecreaseDateRMonthly()
        {


            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select year(Date),sum(Credit) as Total,DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' and Date between '" + txtSmDateFrom.Text + "' and '" + txtSmDateTo.Text + "' group by   month(Date),year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartinv\" width=\"100%\" height=\"120\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartinv\"), { type: 'line', data: {labels: [";

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

                            chart += "],label: \"Decreased Amount\",display: false,borderColor: \"#FF6955\",backgroundColor: [\"#FF6955\"],hoverBackgroundColor: \"#FF6955\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 200},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltrDecrease.Text = chart; main3.Visible = false; ltrDecrease.Visible = true; DecDiv.Visible = true;
                        }
                        else
                        {
                            main3.Visible = true; ltrDecrease.Visible = false; DecDiv.Visible = false; VIT.Visible = false;
                        }
                    }
                }
                else
                {
                    String query = "select year(Date),sum(Debit) as Total,DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' and Date between '" + txtSmDateFrom.Text + "' and '" + txtSmDateTo.Text + "' group by   month(Date),year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartinv\" width=\"100%\" height=\"120\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartinv\"), { type: 'line', data: {labels: [";

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

                            chart += "],label: \"Decreased Amount\",display: false,borderColor: \"#FF6955\",backgroundColor: [\"#FF6955\"],hoverBackgroundColor: \"#FF6955\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 200},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltrDecrease.Text = chart; main3.Visible = false; ltrDecrease.Visible = true; DecDiv.Visible = true;
                        }
                        else
                        {
                            main3.Visible = true; ltrDecrease.Visible = false; DecDiv.Visible = false; VIT.Visible = false;
                        }
                    }
                }
            }

        }
        /// <summary>
        /// /////////////////
        /// </summary>
        private void ShowDataIncreaseMonthly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select year(Date),sum(Debit) as Total, DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   month(Date),year(Date) order by  month_number";
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
                            chart += "new Chart(document.getElementById(\"line-chartin\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Increased Amount\",display: false,borderColor: \"#99FF66\",backgroundColor: [\"#99FF66\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 12},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltrIncreaseTrends.Text = chart; main2.Visible = false;
                        }
                        else
                        {
                            main2.Visible = true; VIT2.Visible = false;
                        }


                    }
                }
                else
                {
                    String query = "select year(Date),sum(Credit) as Total, DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   month(Date),year(Date) order by  month_number";
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
                            chart += "new Chart(document.getElementById(\"line-chartin\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;


                            chart += "],label: \"Increased Amount\",display: false,borderColor: \"#99FF66\",backgroundColor: [\"#99FF66\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 12},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltrIncreaseTrends.Text = chart; main2.Visible = false;
                        }
                        else
                        {
                            main2.Visible = true; VIT2.Visible = false;
                        }

                    }
                }
            }

        }
        private void ShowDataDecreaseMonthly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select year(Date),sum(Credit) as Total, DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   month(Date),year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartinv\" width=\"100%\" height=\"120\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartinv\"), { type: 'line', data: {labels: [";

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

                            chart += "],label: \"Decreased Amount\",display: false,borderColor: \"#FF6955\",backgroundColor: [\"#FF6955\"],hoverBackgroundColor: \"#FF6955\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 12},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltrDecrease.Text = chart; main3.Visible = false;
                        }
                        else
                        {
                            main3.Visible = true; VIT.Visible = false;
                        }


                    }
                }
                else
                {
                    String query = "select year(Date),sum(Debit) as Total, DATENAME(MONTH,DATEADD(MONTH,month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   month(Date),year(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartinv\" width=\"100%\" height=\"120\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartinv\"), { type: 'line', data: {labels: [";

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

                            chart += "],label: \"Decreased Amount\",display: false,borderColor: \"#FF6955\",backgroundColor: [\"#FF6955\"],hoverBackgroundColor: \"#FF6955\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 12},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltrDecrease.Text = chart; main3.Visible = false;
                        }
                        else
                        {
                            main3.Visible = true; VIT.Visible = false;
                        }


                    }
                }
            }

        }
        private void ShowDataQuarterly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select sum(Debit)-sum(Credit) as Total,DATEPART(qq,Date) as month_name from tblGeneralLedger  where Account='" + PID + "' group by  DATEPART(qq,Date)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartm\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartm\"), { type: 'line', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + ("Quarter " + dtBrands232c3.Rows[i]["month_name"].ToString()) + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
                else
                {
                    String query = "select sum(Credit)-sum(Debit) as Total,DATEPART(qq,Date) as month_name from tblGeneralLedger  where Account='" + PID + "' group by  DATEPART(qq,Date)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartm\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartm\"), { type: 'bar', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + ("Quarter " + dtBrands232c3.Rows[i]["month_name"].ToString()) + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
            }

        }
        private void ShowDataLastYearly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select sum(Debit)-sum(Credit) as Total,year(Date)-1 as month_name,year(Date)-1 as month_number from tblGeneralLedger  where Account='" + PID + "' and year(Date)=year(GETDATE())-1 group by   year(Date)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartl\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartl\"), { type: 'bar', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
                else
                {
                    String query = "select sum(Credit)-sum(Debit) as Total,year(Date)-1 as month_name,year(Date)-1 as month_number from tblGeneralLedger  where Account='" + PID + "' and year(Date)=year(GETDATE())-1 group by   year(Date)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartl\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartl\"), { type: 'bar', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
            }
        }
        private void ShowDataLastMonthly()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["led"]);
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
                    String query = "select sum(Debit)-sum(Credit) as Total,DATENAME(MONTH,DATEADD(MONTH,month(Date),-1)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   month(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartt\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartt\"), { type: 'bar', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

                    }
                }
                else
                {
                    String query = "select sum(Credit)-sum(Debit) as Total,DATENAME(MONTH,DATEADD(MONTH,month(Date),-1)) as month_name, month(Date) as month_number from tblGeneralLedger  where Account='" + PID + "' group by   month(Date) order by  month_number";
                    SqlCommand cmd = new SqlCommand(query, con);
                    using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands232c3 = new DataTable();
                        sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                        if (i2c3 != 0)
                        {
                            String chart = "";
                            chart = "<canvas id=\"line-chartt\" width=\"100%\" height=\"180\"></canvas>";
                            chart += "<script>";
                            chart += "new Chart(document.getElementById(\"line-chartt\"), { type: 'bar', data: {labels: [";

                            // more detais

                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                                chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                            chart += "],datasets: [{ data: [";

                            // get data from database and add to chart
                            String value = "";
                            for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                                value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 100000).ToString("##0.00") + ",";
                            value = value.Substring(0, value.Length - 1);
                            chart += value;

                            chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                            chart += "]},options: {maintainAspectRatio: false,layout: {padding: {left: 10,right: 25,top: 3,bottom: 0}},scales: {xAxes: [{time: {unit: 'month'},gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 18,}],},legend: {display: false},}"; // Chart title

                            chart += "});";

                            chart += "</script>";

                            ltChart.Text = chart; main.Visible = false;
                        }
                        else { main.Visible = true; ltChart.Visible = false; }

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
                if (equal.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Debit='" + txtReconcileAmount.Text + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds, "Cat");
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    Repeater3.DataSource = dt;
                    Repeater3.DataBind();
                    con.Close();
                }
                else if (greater.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Debit > '" + txtReconcileAmount.Text + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds, "Cat");
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    Repeater3.DataSource = dt;
                    Repeater3.DataBind();
                    con.Close();
                }
                else if (less.Checked == true)
                {
                    str = "select * from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Debit < '" + txtReconcileAmount.Text + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds, "Cat");
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    Repeater3.DataSource = dt;
                    Repeater3.DataBind();
                    con.Close();
                }
                else
                {
                    str = "select * from tblGeneralLedger where Account='" + PID + "' and Date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Debit between '" + txtRangeMin.Text + "' and '" + txtRangeMax.Text + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds, "Cat");
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    Repeater3.DataSource = dt;
                    Repeater3.DataBind();
                    con.Close();
                }
            }
        }
        private void BindDate()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            AccountName.InnerText = PID; AccName.InnerText = PID;
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
                            datFrom.InnerText = Convert.ToDateTime(dtBrands.Rows[0][6].ToString()).ToString("dd MMM yyyy");
                            datTo.InnerText = DateTime.Now.ToString("dd MMM yyyy");
                        }
                    }
                }
            }
        }
        private void BindRecentActivity()
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            AccountName.InnerText = PID; AccName.InnerText = PID;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select TOP (100) *from tblGeneralLedger where Account='" + PID + "' ORDER BY LedID DESC", con))
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
                        Label lblProduct = item.FindControl("Label2") as Label;

                        Label lblStstus = item.FindControl("Label3") as Label;
                        Label lblID = item.FindControl("Label4") as Label;
                        Label lblDescription = item.FindControl("Label5") as Label;
                        SqlCommand cmd = new SqlCommand("select TOP (100) *from tblGeneralLedger where Account='" + lblProduct.Text + "' and LedID='" + lblID.Text + "' ORDER BY LedID DESC", con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string debited = reader["AccountType"].ToString();
                            string inc = reader["Debit"].ToString();
                            string outc = reader["Credit"].ToString(); reader.Close();
                            if (debited == "Cash" || debited == "Accounts Receivable" || debited == "Inventory" || debited == "Other Current Assets" ||
                                 debited == "Fixed Assets" || debited == "Other Assets" || debited == "Cost of Sales" || debited == "Expenses")
                            {
                                if (inc == "0.0000" || inc == "0.00" || inc == "" || inc == null)
                                {
                                    lblDescription.Text = "[" + Convert.ToDouble(outc).ToString("#,##0.0") + "]" + " amount withdrawl.";
                                    lblStstus.Text = "Account Decreased";
                                    lblStstus.Attributes.Add("class", "h6 font-weight-light text-danger");
                                }
                                else
                                {
                                    lblDescription.Text = "[" + Convert.ToDouble(inc).ToString("#,##0.0") + "]" + " amount get debited.";
                                    lblStstus.Text = "Account Increased";
                                    lblStstus.Attributes.Add("class", "h6 font-weight-light text-primary");
                                }
                            }
                            else
                            {
                                if (outc == "0.0000" || outc == "0.00" || outc == "" || outc == null)
                                {
                                    lblDescription.Text = "[" + Convert.ToDouble(inc).ToString("#,##0.0") + "]" + " amount withdrawl.";
                                    lblStstus.Text = "Account Decreased";
                                    lblStstus.Attributes.Add("class", "h6 font-weight-light text-danger");
                                }
                                else
                                {
                                    lblDescription.Text = "[" + Convert.ToDouble(outc).ToString("#,##0.0") + "]" + " amount get debited.";
                                    lblStstus.Text = "Account Increased";
                                    lblStstus.Attributes.Add("class", "h6 font-weight-light text-primary");
                                }
                            }
                        }
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

                    ///////////////////////////////////////
                    ///Duplicate Version


                }
            }
        }
        protected void btnReconcilation_Click(object sender, EventArgs e)
        {
            if (txtDateform.Text == "" || txtDateto.Text == "")
            {
                string message = "Select date range!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                main1.Visible = false; InvoiceDiv.Visible = true;
                BindBrandsRptr();
                if (ddlRecType.SelectedItem.Text == "Cash Invoice")
                {
                    DivInvoice.Visible = true;
                    if (equal.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblrentreceipt  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and paid='" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                    }
                    else if (greater.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblrentreceipt  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and paid >'" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                    }
                    else if (less.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblrentreceipt  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and paid <'" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                    }
                    else
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtRangeMin.Text).ToString("#,##0.00") + " and " + Convert.ToDouble(txtRangeMax.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblrentreceipt  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and paid between '" + txtRangeMin.Text + "' and '" + txtRangeMax.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                    }
                }
                else if (ddlRecType.SelectedItem.Text == "Bills")

                {
                    DivBills.Visible = true;
                    if (equal.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblPurchaseOrderItem  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Total='" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrBills.DataSource = dt;
                        rptrBills.DataBind();
                    }
                    else if (greater.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblPurchaseOrderItem  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Total >'" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrBills.DataSource = dt;
                        rptrBills.DataBind();
                    }
                    else if (less.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblPurchaseOrderItem  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Total <'" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrBills.DataSource = dt;
                        rptrBills.DataBind();
                    }
                    else
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtRangeMin.Text).ToString("#,##0.00") + " and " + Convert.ToDouble(txtRangeMax.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblPurchaseOrderItem  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Total between '" + txtRangeMin.Text + "' and '" + txtRangeMax.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrBills.DataSource = dt;
                        rptrBills.DataBind();
                    }
                }
                else if (ddlRecType.SelectedItem.Text == "Asset Bills")
                {
                    DivAssetBills.Visible = true;
                    if (equal.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblFixedOrderItem  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and total='" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptAssetBills.DataSource = dt;
                        rptAssetBills.DataBind();
                    }
                    else if (greater.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblFixedOrderItem  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and total >'" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptAssetBills.DataSource = dt;
                        rptAssetBills.DataBind();
                    }
                    else if (less.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblFixedOrderItem  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and total <'" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptAssetBills.DataSource = dt;
                        rptAssetBills.DataBind();
                    }
                    else
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtRangeMin.Text).ToString("#,##0.00") + " and " + Convert.ToDouble(txtRangeMax.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblFixedOrderItem  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and total between '" + txtRangeMin.Text + "' and '" + txtRangeMax.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptAssetBills.DataSource = dt;
                        rptAssetBills.DataBind();
                    }
                }
                else if (ddlRecType.SelectedItem.Text == "Bank Transaction")
                {
                    DivBankTransaction.Visible = true;
                    if (equal.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblbanktrans  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and cashin='" + txtReconcileAmount.Text + "' or date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and cashout='" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrBank.DataSource = dt;
                        rptrBank.DataBind();
                    }
                    else if (greater.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblbanktrans  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and cashin >'" + txtReconcileAmount.Text + "' or date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and cashout='" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrBank.DataSource = dt;
                        rptrBank.DataBind();
                    }
                    else if (less.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblbanktrans  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and cashin <'" + txtReconcileAmount.Text + "' or date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and cashout='" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrBank.DataSource = dt;
                        rptrBank.DataBind();
                    }
                    else
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtRangeMin.Text).ToString("#,##0.00") + " and " + Convert.ToDouble(txtRangeMax.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblbanktrans  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and cashin between '" + txtRangeMin.Text + "' and '" + txtRangeMax.Text + "' or date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and cashout between '" + txtRangeMin.Text + "' and '" + txtRangeMax.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrBank.DataSource = dt;
                        rptrBank.DataBind();
                    }
                }
                else
                {
                    DivOtherInvoice.Visible = true;
                    if (equal.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblIncome  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and amount='" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrOtherInvoice.DataSource = dt;
                        rptrOtherInvoice.DataBind();
                    }
                    else if (greater.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblIncome  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and amount >'" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrOtherInvoice.DataSource = dt;
                        rptrOtherInvoice.DataBind();
                    }
                    else if (less.Checked == true)
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtReconcileAmount.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblIncome  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and amount <'" + txtReconcileAmount.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrOtherInvoice.DataSource = dt;
                        rptrOtherInvoice.DataBind();
                    }
                    else
                    {
                        d1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM yyyy");
                        d2.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                        Camount.InnerText = Convert.ToDouble(txtRangeMin.Text).ToString("#,##0.00") + " and " + Convert.ToDouble(txtRangeMax.Text).ToString("#,##0.00");
                        TrType.InnerText = ddlRecType.SelectedItem.Text;
                        SqlConnection con = new SqlConnection(strConnString);
                        str = "select * from tblIncome  where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and amount between '" + txtRangeMin.Text + "' and '" + txtRangeMax.Text + "'";
                        com = new SqlCommand(str, con);
                        sqlda = new SqlDataAdapter(com);
                        DataTable dt = new DataTable();
                        sqlda.Fill(dt);
                        rptrOtherInvoice.DataSource = dt;
                        rptrOtherInvoice.DataBind();
                    }
                }
            }
        }
        [WebMethod]
        public static void SaveUser(User user)
        {
            String PID = "Cash on Hand";
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
                        double G = Convert.ToDouble(user.Username);
                        if (G < 0)
                        {
                            if (debited == "Cash" || debited == "Accounts Receivable" || debited == "Inventory" || debited == "Other Current Assets" ||
    debited == "Fixed Assets" || debited == "Other Assets" || debited == "Cost of Sales" || debited == "Expenses")
                            {
                                Double bl1 = debitbalance + G;
                                //For the existing Ledger Account
                                SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + PID + "'", con);

                                cmd.ExecuteNonQuery();
                                SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + user.Password + "','','0','" + -G + "','" + bl1 + "','" + DateTime.Now.Date + "','" + PID + "','','" + debited + "')", con);
                                cmd19.ExecuteNonQuery();

                            }
                            else
                            {
                                Double bl1 = debitbalance + G;
                                //For the existing Ledger Account
                                SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + PID + "'", con);

                                cmd.ExecuteNonQuery();
                                SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + user.Password + "','','" + -G + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + PID + "','','" + debited + "')", con);
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
                                SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + user.Password + "','','" + user.Username + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + PID + "','','" + debited + "')", con);
                                cmd19.ExecuteNonQuery();

                            }
                            else
                            {
                                Double bl1 = debitbalance + G;
                                //For the existing Ledger Account
                                SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + PID + "'", con);

                                cmd.ExecuteNonQuery();
                                SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + user.Password + "','','0','" + user.Username + "','" + bl1 + "','" + DateTime.Now.Date + "','" + PID + "','','" + debited + "')", con);
                                cmd19.ExecuteNonQuery();
                            }
                        }
                    }
                }

            }
        }
#pragma warning disable CS0108 // 'Ledger_analysis_details.User' hides inherited member 'Page.User'. Use the new keyword if hiding was intended.
        public class User
#pragma warning restore CS0108 // 'Ledger_analysis_details.User' hides inherited member 'Page.User'. Use the new keyword if hiding was intended.
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        protected void btnSearchSummary_Click(object sender, EventArgs e)
        {
            BindBalanceDateRange2(); ShowDataIncreaseDateRMonthly(); ShowDataDecreaseDateRMonthly();
            //Binding date range
        }
        protected void btnMonthly_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set accountoption='Monthly'", con);
                cmdre.ExecuteNonQuery();
                ShowDataMonthly();
            }
        }
        protected void btnYearly_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set accountoption='This Year'", con);
                cmdre.ExecuteNonQuery();
                ShowDataYearly();
            }
        }
        protected void btnQuarterClick(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdre = new SqlCommand("Update tblDashBoardSetting set accountoption='This Quarter'", con);
                cmdre.ExecuteNonQuery();
                ShowDataQuarterly();
            }
        }
        protected void btnPeriodType_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            if (PeriodType.InnerText == "This Year")
            {
                PeriodType.InnerText = "Last Year";
                ShowDataLastYearly();
            }
            else
            {
                PeriodType.InnerText = "This Year";
                Response.Redirect("Ledger_analysis_details.aspx?led=" + PID);
            }
        }
        protected void rptrBills_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("lblVAT") as Label;
                    Label lbl2 = item.FindControl("lblTotal") as Label;
                    Label lbl3 = item.FindControl("lblTAX") as Label;
                    if (lbl2.Text == "" || lbl2.Text == null)
                    {

                    }
                    else
                    {
                        double VAT = Convert.ToDouble(lbl2.Text) - Convert.ToDouble(lbl.Text);
                        lbl3.Text = VAT.ToString("#,##0.00");
                    }
                }
            }
        }
        protected void rptAssetBills_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("lblVAT") as Label;
                    Label lbl2 = item.FindControl("lblTotal") as Label;
                    Label lbl3 = item.FindControl("lblTAX") as Label;
                    double VAT = Convert.ToDouble(lbl2.Text) - Convert.ToDouble(lbl.Text);
                    lbl3.Text = VAT.ToString("#,##0.00");
                }
            }
        }

        protected void btnBindChart_Click(object sender, EventArgs e)
        {
            ShowDataMonthlyDateRange();
            if (txtCHDateFrom.Text != "" || txtCHDateTo.Text != "")
            {
                CHDateFrom.InnerText = Convert.ToDateTime(txtCHDateFrom.Text).ToString("MMM dd, yyyy");
                CHDateTo.InnerText = Convert.ToDateTime(txtCHDateTo.Text).ToString("MMM dd, yyyy");
            }
        }

        protected void btnDelete1_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["led"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("delete tblGeneralLedger where Account='" + PID + "'", con);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("delete tblGeneralLedger2 where Account='" + PID + "'", con);
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("delete tblGeneralJournal where Account2='" + PID + "' or  Account1='" + PID + "'", con);
                cmd3.ExecuteNonQuery();
                SqlCommand cmd4 = new SqlCommand("delete tblLedgAccTyp where Name='" + PID + "'", con);
                cmd4.ExecuteNonQuery();
                Response.Redirect("Ledger.aspx");
            }
        }

        protected void btnRenameAccount_Click(object sender, EventArgs e)
        {
            if (txtRenameAccount.Text == "")
            {
                string message = "Please put the new name!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String PID = Convert.ToString(Request.QueryString["led"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("update tblGeneralLedger set Account='" + txtRenameAccount.Text + "' where Account='" + PID + "'", con);
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("update tblGeneralLedger2 set Account='" + txtRenameAccount.Text + "' where Account='" + PID + "'", con);
                    cmd2.ExecuteNonQuery();
                    SqlCommand cmd3 = new SqlCommand("update tblGeneralJournal set Account1='" + txtRenameAccount.Text + "',Account2='" + txtRenameAccount.Text + "' where Account2='" + PID + "' or  Account1='" + PID + "'", con);
                    cmd3.ExecuteNonQuery();
                    SqlCommand cmd4 = new SqlCommand("update tblLedgAccTyp set Name='" + txtRenameAccount.Text + "' where Name='" + PID + "'", con);
                    cmd4.ExecuteNonQuery();
                    //Updating account info
                    SqlCommand cmds1 = new SqlCommand("Update tblaccountinfo set  sales='" + txtRenameAccount.Text + "' where sales='" + PID + "'", con);
                    cmds1.ExecuteNonQuery();
                    SqlCommand cmds2 = new SqlCommand("Update tblaccountinfo set  cogs='" + txtRenameAccount.Text + "' where cogs='" + PID + "'", con);
                    cmds2.ExecuteNonQuery();
                    SqlCommand cmds3 = new SqlCommand("Update tblaccountinfo set  inventory='" + txtRenameAccount.Text + "' where inventory='" + PID + "'", con);
                    cmds3.ExecuteNonQuery();
                    SqlCommand cmds4 = new SqlCommand("Update tblaccountinfo set  tax='" + txtRenameAccount.Text + "' where tax='" + PID + "'", con);
                    cmds4.ExecuteNonQuery();
                    SqlCommand cmds5 = new SqlCommand("Update tblaccountinfo set  payrollexpense='" + txtRenameAccount.Text + "' where payrollexpense='" + PID + "'", con);
                    cmds5.ExecuteNonQuery();
                    SqlCommand cmds6 = new SqlCommand("Update tblaccountinfo set  salariespayable='" + txtRenameAccount.Text + "' where salariespayable='" + PID + "'", con);
                    cmds6.ExecuteNonQuery();
                    SqlCommand cmds7 = new SqlCommand("Update tblaccountinfo set  incometaxpayable='" + txtRenameAccount.Text + "' where incometaxpayable='" + PID + "'", con);
                    cmds7.ExecuteNonQuery();
                    SqlCommand cmds8 = new SqlCommand("Update tblaccountinfo set  cashaccount='" + txtRenameAccount.Text + "' where cashaccount='" + PID + "'", con);
                    cmds8.ExecuteNonQuery();
                    SqlCommand cmds9 = new SqlCommand("Update tblaccountinfo set  bankaccount='" + txtRenameAccount.Text + "' where bankaccount='" + PID + "'", con);
                    cmds9.ExecuteNonQuery();
                    SqlCommand cmds10 = new SqlCommand("Update tblaccountinfo set  assetaccount='" + txtRenameAccount.Text + "' where assetaccount='" + PID + "'", con);
                    cmds10.ExecuteNonQuery();
                    SqlCommand cmds11 = new SqlCommand("Update tblaccountinfo set  pensionexpense='" + txtRenameAccount.Text + "' where pensionexpense='" + PID + "'", con);
                    cmds11.ExecuteNonQuery();
                    SqlCommand cmds12 = new SqlCommand("Update tblaccountinfo set  pensionliability='" + txtRenameAccount.Text + "' where pensionliability='" + PID + "'", con);
                    cmds12.ExecuteNonQuery();
                    Response.Redirect("Ledger_analysis_details.aspx?led=" + txtRenameAccount.Text);
                }
            }
        }
    }
}
