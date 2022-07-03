using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class BankStatement : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'BankStatement.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'BankStatement.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {

                    BindBrandsRptr(); BindMainCategory23();
                    BindMainCategory24(); Bindaccount();
                    Bindaccount2(); ShowData1(); ShowData();

                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }
        private void ShowData1()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select account as month_name,sum (balance) as Total  from tblbanktrans  group by account";
            SqlCommand cmd = new SqlCommand(query, con);
            using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
            {
                DataTable dtBrands232c3 = new DataTable();
                sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                if (i2c3 != 0)
                {
                    String chart = "";
                    chart = "<canvas id=\"line-chart2\" width=\"100%\" height=\"160\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"line-chart2\"), { type: 'doughnut', data: {labels: [";

                    // more detais

                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                        chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";



                    chart += "],datasets: [{ data: [";

                    // get data from database and add to chart
                    String value = "";
                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                        value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString() + ",";
                    value = value.Substring(0, value.Length - 1);
                    chart += value;

                    chart += "],label: \"Cash\",display: false,backgroundColor: [\"#a538ba\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                    chart += "]},options: {elements: {center: {text: \'Red is 2/3 of the total numbers\',color: \'#FF6384\',fontStyle: \'Arial\'}},maintainAspectRatio: false,layout: {padding: { xPadding: 15, caretPadding: 10,borderWidth: 1,yPadding: 15}},scales: {xAxes: [{display: false,gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 10},maxBarThickness: 25,}],},title:{display: false},legend: {display: false},cutoutPercentage: 79}"; // Chart title

                    chart += "});";

                    chart += "</script>";

                    ltrUnpaid.Text = chart; main1.Visible = false; ltrUnpaid.Visible = true;
                }
                else { main1.Visible = true; ltrUnpaid.Visible = false; }
            }

        }
        private void ShowData()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select year(Date),DATENAME(MONTH,DATEADD(MONTH, month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number ,sum (balance) as Total,sum(cashin) as INC, sum(cashout) as OUTC  from tblbanktrans  group by month(Date),year(Date) order by month_number ";
            SqlCommand cmd = new SqlCommand(query, con);
            using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
            {
                DataTable dtBrands232c3 = new DataTable();
                sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                if (i2c3 != 0)
                {
                    String chart = "";
                    chart = "<canvas id=\"line-chart\" width=\"100%\" height=\"200\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"line-chart\"), { type: 'bar', data: {labels: [";

                    // more detais

                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                        chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                    chart += "],datasets: [{ data: [";

                    // get data from database and add to chart
                    String value = "";
                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                        value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString() + ",";
                    value = value.Substring(0, value.Length - 1);
                    chart += value;

                    chart += "],label: \"Balance\",display: false,borderColor: \"#FF9900\",backgroundColor: \"#FF9900\",hoverBackgroundColor: \"#FF9900\"},"; // Chart color
                    //Increase trends

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
        private void ShowDataDateRange()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select year(Date),DATENAME(MONTH,DATEADD(MONTH, month(Date),-1))+' '+CAST(year(Date) AS char(10)) as month_name, month(Date) as month_number ,sum (balance) as Total,sum(cashin) as INC, sum(cashout) as OUTC  from tblbanktrans where date between '" + txtSmDateFrom.Text + "' and '" + txtSmDateTo.Text + "'  group by month(Date),year(Date) order by month_number ";
            SqlCommand cmd = new SqlCommand(query, con);
            using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
            {
                DataTable dtBrands232c3 = new DataTable();
                sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                if (i2c3 != 0)
                {
                    String chart = "";
                    chart = "<canvas id=\"line-chart\" width=\"100%\" height=\"200\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"line-chart\"), { type: 'bar', data: {labels: [";

                    // more detais

                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                        chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                    chart += "],datasets: [{ data: [";

                    // get data from database and add to chart
                    String value = "";
                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                        value += (Convert.ToDouble(dtBrands232c3.Rows[i]["Total"]) / 1000000).ToString() + ",";
                    value = value.Substring(0, value.Length - 1);
                    chart += value;

                    chart += "],label: \"Balance\",display: false,borderColor: \"#FF9900\",backgroundColor: \"#FF9900\",hoverBackgroundColor: \"#FF9900\"},"; // Chart color
                    //Increase trends

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

        private void Bindaccount()
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
                    ddlcreditaccount.DataSource = dt;
                    ddlcreditaccount.DataTextField = "Name";
                    ddlcreditaccount.DataValueField = "ACT";
                    ddlcreditaccount.DataBind();
                    ddlcreditaccount.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void BindMainCategory23()
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
                    ddlbanknumber.DataSource = dt;
                    ddlbanknumber.DataTextField = "AccountName";
                    ddlbanknumber.DataValueField = "AC";
                    ddlbanknumber.DataBind();
                    ddlbanknumber.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void BindMainCategory24()
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
                    DropDownList1.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void Bindaccount2()
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
                    DropDownList2.DataSource = dt;
                    DropDownList2.DataTextField = "Name";
                    DropDownList2.DataValueField = "ACT";
                    DropDownList2.DataBind();
                    DropDownList2.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void BindBrandsRptr()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblbanktrans1 ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            con.Close();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (ddlbanknumber.SelectedItem.Text == "-Select-" || txtCash.Text == "" || txtDate.Text == "" || ddlcreditaccount.SelectedItem.Text == "")
                {
                    lblMsg.Text = "Please Fill All the required input"; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("select * from tblBankAccounting where AccountName='" + ddlbanknumber.SelectedItem.Text + "' ", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string kc;
                        kc = reader["AccountNumber"].ToString();
                        reader.Close();
                        SqlCommand cmdbank = new SqlCommand("select * from tblbanktrans1 where account='" + ddlbanknumber.SelectedItem.Text + "'", con);
                        using (SqlDataAdapter sda22 = new SqlDataAdapter(cmdbank))
                        {
                            DataTable dt = new DataTable();
                            sda22.Fill(dt); int j = dt.Rows.Count;
                            //
                            if (j != 0)
                            {
                                double t = Convert.ToDouble(dt.Rows[0][5].ToString()) + Convert.ToDouble(txtCash.Text);
                                SqlCommand cmd45 = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + ddlbanknumber.SelectedItem.Text + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtvouch.Text + "','" + txtcheque.Text + "','" + txtCash.Text + "','0','" + t + "','" + ddlbanknumber.SelectedItem.Text + "','','" + txtremark.Text + "','" + txtDate.Text + "')", con);
                                cvb.ExecuteNonQuery();
                            }
                            else
                            {
                                double t = Convert.ToDouble(txtCash.Text);
                                SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtvouch.Text + "','" + txtcheque.Text + "','" + txtCash.Text + "','0','" + t + "','" + ddlbanknumber.SelectedItem.Text + "','','" + txtremark.Text + "','" + txtDate.Text + "')", con);
                                cvb1.ExecuteNonQuery();
                                SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtvouch.Text + "','" + txtcheque.Text + "','" + txtCash.Text + "','0','" + t + "','" + ddlbanknumber.SelectedItem.Text + "','','" + txtremark.Text + "','" + txtDate.Text + "')", con);
                                b.ExecuteNonQuery();

                            }
                        }
                        SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash at Bank'", con);
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
                                    Double bl1 = M1 + Convert.ToDouble(txtCash.Text); ;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                    cmd45.ExecuteNonQuery();
                                    string total = ddlcreditaccount.SelectedItem.Text + "- " + txtremark.Text;
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','" + txtCash.Text + "','0','" + bl1 + "','" + txtDate.Text + "','Cash at Bank','','Cash')", con);
                                    cmd1974.ExecuteNonQuery();
                                }
                            }
                        }

                        SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + ddlcreditaccount.SelectedItem.Text + "'", con);

                        SqlDataReader reader66 = cmd166.ExecuteReader();

                        if (reader66.Read())
                        {
                            string credited;
                            credited = reader66["AccountType"].ToString();
                            reader66.Close();

                            SqlCommand cmdcredit = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlcreditaccount.SelectedItem.Text + "'", con);
                            SqlDataReader readercredit = cmdcredit.ExecuteReader();
                            if (readercredit.Read())
                            {
                                double creditbalance = Convert.ToDouble(readercredit["Balance"].ToString());
                                readercredit.Close();
                                double G = Convert.ToDouble(txtCash.Text);
                                if (credited == "Cash" || credited == "Accounts Receivable" || credited == "Inventory" || credited == "Other Current Assets" ||
    credited == "Fixed Assets" || credited == "Other Assets" || credited == "Cost of Sales" || credited == "Expenses")
                                {
                                    Double bl1 = creditbalance - G;

                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlcreditaccount.SelectedItem.Text + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + txtremark.Text + "','','0','" + txtCash.Text + "','" + bl1 + "','" + txtDate.Text + "','" + ddlcreditaccount.SelectedItem.Text + "','','" + credited + "')", con);
                                    cmd19.ExecuteNonQuery();

                                }
                                else
                                {
                                    Double bl1 = creditbalance + G;

                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + ddlcreditaccount.SelectedItem.Text + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + txtremark.Text + "','','0','" + txtCash.Text + "','" + bl1 + "','" + txtDate.Text + "','" + ddlcreditaccount.SelectedItem.Text + "','','" + credited + "')", con);
                                    cmd19.ExecuteNonQuery();
                                }
                            }
                        }
                        Response.Redirect("BankStatement.aspx");

                    }

                }

            }
        }
        protected void btnUpdate_Click2(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (DropDownList1.SelectedItem.Text == "-Select-" || txtCash1.Text == "" || txtDate1.Text == "" || DropDownList2.SelectedItem.Text == "")
                {
                    lblMsg.Text = "Please Fill All the required input"; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string kc;
                        kc = reader["AccountNumber"].ToString();
                        reader.Close();
                        SqlCommand cmdbank = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                        using (SqlDataAdapter sda22 = new SqlDataAdapter(cmdbank))
                        {
                            DataTable dt = new DataTable();
                            sda22.Fill(dt); int j = dt.Rows.Count;
                            //
                            if (j != 0)
                            {
                                double t = Convert.ToDouble(dt.Rows[0][5].ToString()) - Convert.ToDouble(txtCash1.Text);
                                SqlCommand cmd45 = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtvouch1.Text + "','" + txtcheque1.Text + "','0','" + txtCash1.Text + "','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + txtremark1.Text + "','" + txtDate1.Text + "')", con);
                                cvb.ExecuteNonQuery();
                            }
                            else
                            {
                                double t = Convert.ToDouble(txtCash1.Text);
                                SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtvouch1.Text + "','" + txtcheque1.Text + "','0','" + txtCash1.Text + "'," + t + "','" + DropDownList1.SelectedItem.Text + "','','" + txtremark1.Text + "','" + txtDate1.Text + "')", con);
                                cvb1.ExecuteNonQuery();
                                SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtvouch1.Text + "','" + txtcheque1.Text + "','0','" + txtCash1.Text + "','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + txtremark1.Text + "','" + txtDate1.Text + "')", con);
                                b.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                        SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash at Bank'", con);
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
                                    Double bl1 = M1 - Convert.ToDouble(txtCash1.Text);
                                    string total = DropDownList2.SelectedItem.Text + "- " + txtremark.Text;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','0','" + txtCash1.Text + "','" + bl1 + "','" + txtDate1.Text + "','Cash at Bank','','Cash')", con);
                                    cmd1974.ExecuteNonQuery();
                                }
                            }
                        }

                        SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + DropDownList2.SelectedItem.Text + "'", con);

                        SqlDataReader reader66 = cmd166.ExecuteReader();

                        if (reader66.Read())
                        {
                            string debited;
                            debited = reader66["AccountType"].ToString();
                            reader66.Close();

                            SqlCommand cmdcredit = new SqlCommand("select * from tblGeneralLedger2 where Account='" + DropDownList2.SelectedItem.Text + "'", con);
                            SqlDataReader readercredit = cmdcredit.ExecuteReader();
                            if (readercredit.Read())
                            {
                                double debitbalance = Convert.ToDouble(readercredit["Balance"].ToString());
                                readercredit.Close();
                                double G = Convert.ToDouble(txtCash1.Text);

                                if (debited == "Cash" || debited == "Accounts Receivable" || debited == "Inventory" || debited == "Other Current Assets" ||
                                    debited == "Fixed Assets" || debited == "Other Assets" || debited == "Cost of Sales" || debited == "Expenses")
                                {
                                    Double bl1 = debitbalance + G;
                                    Double bl2 = debitbalance + G;
                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + DropDownList2.SelectedItem.Text + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + txtremark1.Text + "','','" + txtCash1.Text + "','0','" + bl1 + "','" + txtDate1.Text + "','" + DropDownList2.SelectedItem.Text + "','','" + debited + "')", con);
                                    cmd19.ExecuteNonQuery();

                                }
                                else
                                {
                                    Double bl1 = debitbalance - G;
                                    //For the existing Ledger Account
                                    SqlCommand cmd = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + DropDownList2.SelectedItem.Text + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('" + txtremark1.Text + "','','" + txtCash1.Text + "','0','" + bl1 + "','" + txtDate1.Text + "','" + DropDownList2.SelectedItem.Text + "','','" + debited + "')", con);
                                    cmd19.ExecuteNonQuery();
                                }
                            }
                        }
                        Response.Redirect("BankStatement.aspx");

                    }
                }
            }
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select * from BankStatement where Date between CONVERT(datetime, '" + TextBox5.Text + "') AND CONVERT(datetime, '" + TextBox7.Text + "')", con))
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
        protected void RES(object sender, EventArgs e)
        {

            Response.Redirect("BankStatement.aspx");
        }
        protected void btnSearchSummary_Click(object sender, EventArgs e)
        {
            ShowDataDateRange();
            if (txtSmDateFrom.Text != "" || txtSmDateTo.Text != "")
            {
                datFrom.InnerText = Convert.ToDateTime(txtSmDateFrom.Text).ToString("dd MMM yyyy");
                datTo.InnerText = Convert.ToDateTime(txtSmDateTo.Text).ToString("dd MMM yyyy");
            }
        }
    }
}