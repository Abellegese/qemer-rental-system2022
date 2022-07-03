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
    public partial class BalanceSheetReport : System.Web.UI.Page
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
                    datTo.Visible = false; datFrom1.Visible = false; tomiddle.Visible = false;
                    BindIncomSData(); Bindliablility();
                    BindIncomSData2();
                    //BindIncomSData3();
                    BindIncomSData5(); bindcompany();
                    networth(); BindFixedAsset(); BindIncomSDataOtherCurrent();
                    BindDepreciation(); calculatenetfixedasset();
                    mont.InnerText = "As of " + DateTime.Now.ToString("MMMM dd, yyyy");
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private double BindbegAssetBalance()
        {

            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            string q11 = "select Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Receivable'  OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'  and AccountType='Cash' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Assets'  OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Inventory' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Assets' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Fixed Assets' group by Account";
            string str2r = q11;

            com = new SqlCommand(str2r, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP 1* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }
            return Pbalance;
        }
        private double BindbegLiabilityBalance()
        {

            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            string q11 = "select Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accumulated Depreciation' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Payable' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Liabilities'  OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Long Term Liabilities' group by Account";
            string str2r = q11;

            com = new SqlCommand(str2r, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP 1* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }
            return Pbalance;
        }
        private Tuple<double, double> BindbegBalanceNetFixedAsset()
        {
            double Pbalance = 0; double Pbalance1 = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            string q11 = "select Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accumulated Depreciation' group by Account";
            string str2r = q11;

            com = new SqlCommand(str2r, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2b = "select TOP 1* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2b, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }

            //Finding fixed asset account


            string q1 = "select Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Fixed Assets' group by Account";
            string str2 = q1;

            SqlCommand com3 = new SqlCommand(str2, con);
            SqlDataAdapter sqlda12 = new SqlDataAdapter(com3);
            DataTable dt2 = new DataTable();
            sqlda.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    string str24 = "select TOP 1* from tblGeneralLedger where Account='" + dt2.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str24, con);
                    SqlDataAdapter sqldaty = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqldaty.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance1 = Pbalance1 + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }

            return Tuple.Create(Pbalance, Pbalance1);
        }
        private void calculatenetfixedassetDated()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select (SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where AccountType='Accumulated Depreciation' and Date BETWEEN '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                string str3 = "select (sum(Debit)-Sum(Credit)) as Balance from tblGeneralLedger where AccountType='Fixed Assets' and Date BETWEEN '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'";
                SqlCommand com3 = new SqlCommand(str3, con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(com3))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3);
                    if (dtBrands3.Rows[0][0].ToString() == null || dtBrands3.Rows[0][0].ToString() == "")
                    {
                        H3.InnerText = "0.00";
                    }
                    else
                    {
                        double fixedassd = Convert.ToDouble(dtBrands3.Rows[0][0].ToString()) + BindbegBalanceNetFixedAsset().Item2;
                        double accdepr = Convert.ToDouble(dtBrands.Rows[0][0].ToString()) + BindbegBalanceNetFixedAsset().Item1;
                        double netasset = fixedassd - accdepr;
                        H3.InnerText = netasset.ToString("#,##0.00");
                    }
                }
            }
        }
        private void calculatenetfixedasset()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),(SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where AccountType='Accumulated Depreciation'  group by year(Date)";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                string str3 = "select year(Date), (sum(Debit)-Sum(Credit)) as Balance from tblGeneralLedger where AccountType='Fixed Assets'  group by year(Date)";
                SqlCommand com3 = new SqlCommand(str3, con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(com3))
                {
                    DataTable dtBrands3 = new DataTable();
                    sda3.Fill(dtBrands3);
                    double fixedassd = Convert.ToDouble(dtBrands3.Rows[0][1].ToString());
                    double accdepr = Convert.ToDouble(dtBrands.Rows[0][1].ToString());
                    double netasset = fixedassd - accdepr;
                    H3.InnerText = netasset.ToString("#,##0.00");
                }
            }
        }
        private void BindIncomSData()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date), (sum(Debit)-Sum(Credit)) as Balance,Account,AccountType from tblGeneralLedger where AccountType='Cash'  group by year(Date),Account,AccountType";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
        }
        private void BindDepreciation()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),(SUM(Credit)-SUM(Debit)) as Balance,Account,AccountType from tblGeneralLedger where AccountType='Accumulated Depreciation'  group by year(Date),Account,AccountType";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater6.DataSource = dtBrands;
                Repeater6.DataBind();
            }
        }
        private void BindFixedAsset()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),(SUM(Debit)-SUM(Credit)) as Balance,Account,AccountType from tblGeneralLedger where AccountType='Fixed Assets'   group by year(Date),Account,AccountType";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater5.DataSource = dtBrands;
                Repeater5.DataBind();
            }
        }
        private void BindIncomSData2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),(SUM(Debit)-SUM(Credit)) as Balance,Account,AccountType from tblGeneralLedger where AccountType='Accounts Receivable'   group by year(Date),Account,AccountType";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater2.DataSource = dtBrands;
                Repeater2.DataBind();
            }
        }
        private void Bindliablility()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),(SUM(Credit)-SUM(Debit)) as Balance,Account from tblGeneralLedger where AccountType='Accounts Payable' or AccountType='Other Current Liabilities' or AccountType='Long Term Liabilities'   group by year(Date),Account,AccountType";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater4.DataSource = dtBrands;
                Repeater4.DataBind();
            }
        }
        private void BindIncomSDataOtherCurrent()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),(sum(Debit)-sum(Credit)) as Balance,Account from tblGeneralLedger where  AccountType='Other Assets' or AccountType='Other Current Assets' group by year(Date),Account";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater7.DataSource = dtBrands;
                Repeater7.DataBind();
            }
        }
        private void BindIncomSData5()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),(sum(Debit)-sum(Credit)) as Balance,Account from tblGeneralLedger where   AccountType='Inventory'   group by  year(Date), Account,AccountType";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater3.DataSource = dtBrands;
                Repeater3.DataBind();
            }
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
                    string company;
                    company = reader["Oname"].ToString();
                    oname.InnerText = company;
                }
            }
        }
        private void networthcalculated()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();

            String query = "select (SUM(Debit)-SUM(Credit)) as Balance  from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Receivable'  OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'  and AccountType='Cash' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Assets'  OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Inventory' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Assets' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Fixed Assets'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string asset; double assetsssss;
                asset = reader["Balance"].ToString();
                if (asset == "" || asset == null)
                {
                    assetsssss = 0;
                }
                else
                {
                    assetsssss = Convert.ToDouble(asset) + BindbegAssetBalance();
                }
                reader.Close();
                con.Close();
                String query1 = "select (SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accumulated Depreciation' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Payable' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Liabilities'  OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Long Term Liabilities'";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    string liability; double liabilityyyyy;
                    liability = reader1["Balance"].ToString();
                    if (liability == "" || liability == null)
                    {
                        liabilityyyyy = 0;
                    }
                    else
                    {
                        liabilityyyyy = Convert.ToDouble(liability) + BindbegLiabilityBalance();
                    }
                    reader1.Close();
                    con.Close();
                    TotalLiability.InnerText = liabilityyyyy.ToString("#,##0.00");
                    TotalAssets.InnerText = assetsssss.ToString("#,##0.00");
                    TotalLiabilityandNetworth.InnerText = assetsssss.ToString("#,##0.00");
                    double nw = assetsssss - liabilityyyyy;
                    Label2.InnerText = nw.ToString("#,##0.00");
                }
            }
        }
        private void networth()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select (SUM(Debit)-SUM(Credit)) as Balance  from tblGeneralLedger where AccountType='Accounts Receivable'  OR AccountType='Cash' OR AccountType='Other Assets'  OR AccountType='Inventory'  OR  AccountType='Other Current Assets'   OR AccountType='Fixed Assets' group by year(Date)";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string asset;
                asset = reader["Balance"].ToString();
                TotalAssets.InnerText = Convert.ToDouble(asset).ToString("#,##0.00");
                TotalLiabilityandNetworth.InnerText = Convert.ToDouble(asset).ToString("#,##0.00");
                reader.Close();
                con.Close();
                String query1 = "select (SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where AccountType='Accounts Payable' OR  AccountType='Other Current Liabilities'  OR AccountType='Long Term Liabilities'   OR AccountType='Accumulated Depreciation' group by year(Date)";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    string liability;
                    liability = reader1["Balance"].ToString();
                    TotalLiability.InnerText = Convert.ToDouble(liability).ToString("#,##0.00");
                    reader1.Close();
                    con.Close();
                    double nw = Convert.ToDouble(asset) - Convert.ToDouble(liability);
                    Label2.InnerText = nw.ToString("#,##0.00");
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDatefrom.Text == "" || txtDateto.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                datTo.Visible = true; datFrom1.Visible = true; tomiddle.Visible = true;
                datFrom1.InnerText = Convert.ToDateTime(txtDatefrom.Text).ToString("dd MMM yyyy"); datTo.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                mont.Visible = false; ;
                calculatenetfixedassetDated();
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select year(Date), (sum(Debit)-Sum(Credit)) as Balance,Account,AccountType from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Cash'  group by year(Date),Account,AccountType";
                com = new SqlCommand(str, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(com))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands);
                    Repeater1.DataSource = dtBrands;
                    Repeater1.DataBind();
                }
                string str1 = "select year(Date),(SUM(Credit)-SUM(Debit)) as Balance,Account,AccountType from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accumulated Depreciation'  group by year(Date),Account,AccountType";
                SqlCommand com1 = new SqlCommand(str1, con);
                using (SqlDataAdapter sda1 = new SqlDataAdapter(com1))
                {
                    DataTable dtBrands = new DataTable();
                    sda1.Fill(dtBrands);
                    Repeater6.DataSource = dtBrands;
                    Repeater6.DataBind();
                }
                string strg = "select year(Date),(SUM(Debit)-SUM(Credit)) as Balance,Account,AccountType from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Assets'  group by year(Date),Account,AccountType";
                SqlCommand comg = new SqlCommand(strg, con);
                using (SqlDataAdapter sdag = new SqlDataAdapter(comg))
                {
                    DataTable dtBrands = new DataTable();
                    sdag.Fill(dtBrands);
                    Repeater7.DataSource = dtBrands;
                    Repeater7.DataBind();
                }
                string str2 = "select year(Date),(SUM(Debit)-SUM(Credit)) as Balance,Account,AccountType from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Fixed Assets'   group by year(Date),Account,AccountType";
                SqlCommand com2 = new SqlCommand(str2, con);
                using (SqlDataAdapter sda2 = new SqlDataAdapter(com2))
                {
                    DataTable dtBrands = new DataTable();
                    sda2.Fill(dtBrands);
                    Repeater5.DataSource = dtBrands;
                    Repeater5.DataBind();
                }
                string str3 = "select year(Date),(SUM(Debit)-SUM(Credit)) as Balance,Account,AccountType from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Receivable'   group by year(Date),Account,AccountType";
                SqlCommand com3 = new SqlCommand(str3, con);
                using (SqlDataAdapter sda3 = new SqlDataAdapter(com3))
                {
                    DataTable dtBrands = new DataTable();
                    sda3.Fill(dtBrands);
                    Repeater2.DataSource = dtBrands;
                    Repeater2.DataBind();
                }
                string str4 = "select year(Date),(SUM(Credit)-SUM(Debit)) as Balance,Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Payable' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Liabilities' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Long Term Liabilities'   group by year(Date),Account,AccountType";
                SqlCommand com4 = new SqlCommand(str4, con);
                using (SqlDataAdapter sda4 = new SqlDataAdapter(com4))
                {
                    DataTable dtBrands = new DataTable();
                    sda4.Fill(dtBrands);
                    Repeater4.DataSource = dtBrands;
                    Repeater4.DataBind();
                }
                string str5 = "select year(Date),(sum(Debit)-sum(Credit)) as Balance,Account from tblGeneralLedger where  Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Inventory'   group by  year(Date), Account,AccountType";
                SqlCommand com5 = new SqlCommand(str5, con);
                using (SqlDataAdapter sda5 = new SqlDataAdapter(com5))
                {
                    DataTable dtBrands = new DataTable();
                    sda5.Fill(dtBrands);
                    Repeater3.DataSource = dtBrands;
                    Repeater3.DataBind();
                }
                //Net Worth
                networthcalculated();
                //Calculating beg balance
                BindTotalBalance1(); BindTotalBalance2(); BindTotalBalance3(); BindTotalBalance5(); BindTotalBalance6(); BindTotalBalance7();
                BindTotalBalance4();
            }
            //End calculating
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Rent_Status_Raksym_Trading" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);


            con.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();



        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }
        /// <summary>
        /// Ending Balance calculation
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>
        private string BindPDate()
        {
            string FirstDate = "";

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select TOP (1) *from tblGeneralLedger ORDER BY LedID ASC", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                        if (i != 0)
                        {
                            FirstDate = Convert.ToDateTime(dtBrands.Rows[0][6].ToString()).ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            return FirstDate;
        }
        //Repeater1 Beg
        private double CalculateEndingBalance(string Account)
        {
            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            str = "select* from tblGeneralLedger where Account='" + Account + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Pbalance = Convert.ToDouble(dt.Rows[0][5].ToString());
            }

            return Pbalance;
        }
        private void BindTotalBalance1()
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("lbl1R1") as Label;
                    Label balance = item.FindControl("lbl2R1") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
        ///Repeater 1 Ending
        private void BindTotalBalance2()
        {
            foreach (RepeaterItem item in Repeater2.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("lbl1R2") as Label;
                    Label balance = item.FindControl("lbl2R2") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
        //
        private void BindTotalBalance3()
        {
            foreach (RepeaterItem item in Repeater3.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("lbl1R3") as Label;
                    Label balance = item.FindControl("lbl2R3") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
        //
        private void BindTotalBalance5()
        {
            foreach (RepeaterItem item in Repeater5.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("lbl1R5") as Label;
                    Label balance = item.FindControl("lbl2R5") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
        //
        private void BindTotalBalance6()
        {
            foreach (RepeaterItem item in Repeater6.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("lbl1R6") as Label;
                    Label balance = item.FindControl("lbl2R6") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
        //
        private void BindTotalBalance7()
        {
            foreach (RepeaterItem item in Repeater7.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("lbl1R7") as Label;
                    Label balance = item.FindControl("lbl2R7") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
        //
        private void BindTotalBalance4()
        {
            foreach (RepeaterItem item in Repeater4.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("lbl1R4") as Label;
                    Label balance = item.FindControl("lbl2R4") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
    }
}