using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class OtherInvoices : System.Web.UI.Page
    {
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'OtherInvoices.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'OtherInvoices.ds' is never used
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Column"] = "incomename";
                    ViewState["Column1"] = "amount";
                    ViewState["Column2"] = "date";
                    ViewState["Sortorder"] = "ASC";
                    BindBrandsRptr2(); bindfixedaccount1(); bindbankaccount();
                    bindcompany(); bindfixedaccount(); bindcashaccount(); bindotherinvoice();
                    binddetails(); bindINFO(); bindIncomeINFO(); bindFSnumber(); bindPaymentmode();
                    GetColumnVisibilityValues(); GetFontSizeAndHeadingName(); GetOtherVisiblityValues();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private string bindAddress(string customer)
        {
            string return_customer = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String PID2 = Convert.ToString(Request.QueryString["ref2"]);

                INVNO.InnerText = "INV# -" + PID2;
                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + customer + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {
                    string Address2 = readercrd["addresscust"].ToString();
                    if (Address2 == "" || Address2 == null)
                    {

                    }
                    else
                    {
                        return_customer = Address2;
                    }
                }
            }
            return return_customer;
        }
        private void bindIncomeINFO()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String PayMode = Convert.ToString(Request.QueryString["paymentmode"]);
                PaymentMode.InnerText = PayMode;
                SqlCommand cmdcrd = new SqlCommand("select * from tblIncome where id='" + PID + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {
                    string fsNO1 = readercrd["fsno"].ToString();
                    readercrd.Close();
                    if (fsNO1 == "" || fsNO1 == null)
                    {

                    }
                    else
                    {
                        FSno.InnerText = "FS# 0000" + fsNO1;
                    }
                    //Duplivate binding
                }
            }
        }
        private void bindPaymentmode()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["expname"]);
                String PID2 = Convert.ToString(Request.QueryString["ref2"]);
                SqlCommand cmdcrd1 = new SqlCommand("select* from tblIncome where id='" + PID2 + "' and incomename='" + PID + "'", con);
                SqlDataReader readercrd1 = cmdcrd1.ExecuteReader();
                if (readercrd1.Read())
                {

                    string payment_mode = readercrd1["paymode"].ToString();
                    readercrd1.Close();
                    if (payment_mode == "" || payment_mode == null)
                    {

                    }
                    else
                    {
                        PaymentMode.InnerText = payment_mode;
                    }
                    //Duplivate binding
                }
            }
        }
        private void bindFSnumber()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String PID2 = Convert.ToString(Request.QueryString["ref2"]);
                SqlCommand cmdcrd1 = new SqlCommand("select TOP 1* from tblIncome order by id desc", con);
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
                    //Duplivate binding
                }
            }
        }
        private void bindINFO()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String PID2 = Convert.ToString(Request.QueryString["ref2"]);

                INVNO.InnerText = "INV# " + PID2;
                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + vendor1.InnerText + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {
                    string Address2 = readercrd["addresscust"].ToString();


                }
                readercrd.Close();
                SqlCommand cmdcrd1 = new SqlCommand("select * from tblIncome where id='" + PID2 + "'", con);
                SqlDataReader readercrd1 = cmdcrd1.ExecuteReader();
                if (readercrd1.Read())
                {
                    string Cust = readercrd1["customer"].ToString();
                    string TIN = readercrd1["TIN"].ToString();
                    string FSNumber = readercrd1["fsno"].ToString();
                    string Address_reader = readercrd1["address"].ToString();

                    if (TIN == "" || TIN == null)
                    {

                    }
                    else
                    {
                        TINNUMBER.InnerText = TIN;
                    }
                    if (Address_reader == "" || Address_reader == null)
                    {

                    }
                    else
                    {
                        Address.InnerText = Address_reader;
                    }
                    if (Cust == "" || Cust == null)
                    {

                    }
                    else
                    {
                        vendor1.InnerText = Cust;
                    }
                    //Duplivate binding
                }
                readercrd1.Close();
            }
        }
        private readonly Random _random = new Random();
        public int RandomNumber(int min, int max)
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
        private void binddetails()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String PID2 = Convert.ToString(Request.QueryString["expname"]);
            ExpenseType.InnerText = "**" + PID2 + "**";
            if (Request.QueryString["ref2"] != null)
            {
                deleteTab.Visible = true;
                editTab.Visible = true;
                PayMode.Visible = true;
                I1.Visible = false; I2.Visible = false;
                buttonback.Visible = true; InvoiceBadge.Visible = true;
                InvoiceBadge.InnerText = "INV#-" + PID;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2AC = new SqlCommand("select * from tblIncome where incomename='" + PID2 + "' and id='" + PID + "'", con);
                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                    if (readerAC.Read())
                    {
                        String vendor = readerAC["customer"].ToString();
                        String amount = readerAC["amount"].ToString();
                        String date = readerAC["date"].ToString();
                        String rate1 = readerAC["rate"].ToString();
                        String unit1 = readerAC["unit"].ToString();
                        String ref1 = readerAC["refernces"].ToString();
                        String Qty1 = readerAC["qty"].ToString();
                        if (rate1 == "1.0000")
                        {
                            Qty.InnerText = "-";
                            Unit2.InnerText = "-";
                            Rate.InnerText = "-";
                        }
                        else
                        {
                            Qty.InnerText = Convert.ToDouble(Qty1).ToString("#,##0.00");
                            Rate.InnerText = "ETB " + Convert.ToDouble(rate1).ToString("#,##0.00") + "/" + unit1;
                            Unit2.InnerText = unit1;
                        }
                        RefNum.InnerText = ref1;

                        id.InnerText = PID;
                        double VATFREE = Convert.ToDouble(amount) / 1.15;
                        double vat = Convert.ToDouble(amount) - VATFREE;

                        amount1.InnerText = Convert.ToDouble(VATFREE).ToString("#,##0.00");
                        TotalAmount.InnerText = Convert.ToDouble(amount).ToString("#,##0.00");
                        VAT15.InnerText = Convert.ToDouble(vat).ToString("#,##0.00");
                        DupVatFree.InnerText = Convert.ToDouble(VATFREE).ToString("#,##0.00");
                        DupVAT.InnerText = Convert.ToDouble(vat).ToString("#,##0.00");
                        DupGrandTotal.InnerText = Convert.ToDouble(amount).ToString("#,##0.00");
                        BillDate1.InnerText = Convert.ToDateTime(date).ToString("MMMM dd, yyyy");

                        readerAC.Close();
                        SqlCommand cmd = new SqlCommand("select * from tblCustomers where FllName='" + vendor + "'", con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {

                            vendor1.InnerText = vendor;

                        }
                    }
                }
            }
        }
        private void bindcompany()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Oname,BuissnessLocation,Contact,TIN from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company; string address; string cont;
                    company = reader["Oname"].ToString();
                    address = reader["BuissnessLocation"].ToString();
                    cont = reader["Contact"].ToString();
                    VendorTIN.InnerText = reader["TIN"].ToString();
                    addressname.InnerText = address;
                    oname.InnerText = company;
                    phone.InnerText = cont;
                }
            }
        }
        private void bindfixedaccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblIncomeBrand", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlExpense.DataSource = dt;
                    ddlExpense.DataTextField = "incomename";
                    ddlExpense.DataValueField = "id";
                    ddlExpense.DataBind();
                    ddlExpense.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
        private void bindcashaccount()
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
                    ddlCash.DataSource = dt;
                    ddlCash.DataTextField = "Name";
                    ddlCash.DataValueField = "ACT";
                    ddlCash.DataBind();
                    ddlCash.Items.Insert(0, new ListItem("-Select-", "0"));

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
                    ddlBank.DataSource = dt;
                    ddlBank.DataTextField = "AccountName";
                    ddlBank.DataValueField = "AC";
                    ddlBank.DataBind();
                    ddlBank.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
        private void bindfixedaccount1()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblCustomers", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlVendor.DataSource = dt;
                    ddlVendor.DataTextField = "FllName";
                    ddlVendor.DataValueField = "TIN";
                    ddlVendor.DataBind();
                    ddlVendor.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void BindcustomerStatement(string customer, double NetIncome1, string expense_name, string references)
        {
            if (customer != "-Select")
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + customer + "' ORDER BY CSID DESC", con);
                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                    if (readerbcustb.Read())
                    {
                        string bala;

                        bala = readerbcustb["Balance"].ToString();
                        readerbcustb.Close();

                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + references + "','','" + NetIncome1 + "','" + NetIncome1 + "','" + bala + "','" + customer + "')", con);
                        custcmd.ExecuteNonQuery();
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
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string customer = ""; string address = ""; string paymode = "";
            if (ddlCash.SelectedItem.Text != "-Select-")
            {
                paymode += "Cash";
            }
            else
            {
                paymode += "Bank";
            }
            if (ddlVendor.SelectedItem.Text != "-Select-")
            {
                customer += ddlVendor.SelectedItem.Text;
                address = bindAddress(customer);
                if (address == "" || address == null)
                {
                    if (txtAddress.Text != "")
                    {
                        address += txtAddress.Text;
                    }
                }
            }
            if (ddlVendor.SelectedItem.Text == "-Select-")
            {
                customer += txtCustomer.Text;
                address += txtAddress.Text;
            }
            if (ddlExpense.Items.Count == 0)
            {
                lblMsg.Text = "No Invoice Type was added."; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                if (ddlExpense.SelectedItem.Text == "-Select-" || txtAmount.Text == "" || txtReference.Text == "" || ddlCash.SelectedItem.Text == "-Select-" && ddlBank.SelectedItem.Text == "-Select-" || ddlCash.SelectedItem.Text != "-Select-" && ddlBank.SelectedItem.Text != "-Select-")
                {
                    lblMsg.Text = "Please fill all the required input ( Or you selected both bank and cash account"; lblMsg.ForeColor = Color.Red;
                }
                else
                {

                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmd2AC = new SqlCommand("select * from tblIncomeBrand where incomename='" + ddlExpense.SelectedItem.Text + "'", con);
                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                        if (readerAC.Read())
                        {
                            String account = readerAC["incomeaccount"].ToString();
                            String rate = readerAC["rate"].ToString();
                            String unit = readerAC["unit"].ToString();

                            readerAC.Close();
                            //Inserting to income Table
                            Double NetIncome1 = 0; Double VatFree = 0; Double Vat = 0;
                            if (rate == "1")
                            {
                                NetIncome1 = Convert.ToDouble(txtAmount.Text);
                                VatFree = NetIncome1 / 1.15;
                                Vat = NetIncome1 - VatFree;
                                SqlCommand cmdin = new SqlCommand("insert into tblIncome values('" + ddlExpense.SelectedItem.Text + "','" + customer + "','" + rate + "','" + unit + "','" + NetIncome1 + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','1','" + txtFSNo.Text + "','" + txtTIN.Text + "','" + address + "','" + paymode + "')", con);
                                cmdin.ExecuteNonQuery();
                            }
                            else
                            {
                                NetIncome1 = Convert.ToDouble(txtAmount.Text) * Convert.ToDouble(rate) * 0.15 + Convert.ToDouble(txtAmount.Text) * Convert.ToDouble(rate);
                                VatFree = Convert.ToDouble(txtAmount.Text) * Convert.ToDouble(rate);
                                Vat = NetIncome1 - VatFree;
                                SqlCommand cmdin = new SqlCommand("insert into tblIncome values('" + ddlExpense.SelectedItem.Text + "','" + customer + "','" + rate + "','" + unit + "','" + NetIncome1 + "','" + txtReference.Text + "','" + DateTime.Now.Date + "','" + txtAmount.Text + "','" + txtFSNo.Text + "','" + txtTIN.Text + "','" + address + "','" + paymode + "')", con);
                                cmdin.ExecuteNonQuery();
                            }
                            //END Inserting

                            BindcustomerStatement(ddlVendor.SelectedItem.Text, NetIncome1, ddlExpense.SelectedItem.Text, txtReference.Text);
                            if (ddlCash.SelectedItem.Text != "-Select-")
                            {

                                SqlCommand cmd166c3 = new SqlCommand("select * from tblLedgAccTyp where Name='" + ddlCash.SelectedItem.Text + "'", con);
                                SqlDataReader reader66c3 = cmd166c3.ExecuteReader();

                                if (reader66c3.Read())
                                {
                                    string ah11c;
                                    string ah1258c;
                                    ah11c = reader66c3["No"].ToString();
                                    ah1258c = reader66c3["AccountType"].ToString();
                                    reader66c3.Close();
                                    con.Close();
                                    con.Open();
                                    SqlCommand cmd190cc = new SqlCommand("select * from tblGeneralLedger2 where Account='" + ddlCash.SelectedItem.Text + "'", con);

                                    SqlDataReader reader66790c = cmd190cc.ExecuteReader();

                                    if (reader66790c.Read())
                                    {
                                        string ah1289c;
                                        ah1289c = reader66790c["Balance"].ToString();
                                        reader66790c.Close();
                                        con.Close();
                                        con.Open();
                                        Double M1c = Convert.ToDouble(ah1289c);
                                        Double bl1c = M1c + NetIncome1;
                                        string total = "Cash Debited for " + ddlExpense.SelectedItem.Text + " Income";
                                        SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "' where Account='" + ddlCash.SelectedItem.Text + "'", con);
                                        cmd45c.ExecuteNonQuery();
                                        SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','" + NetIncome1 + "','0','" + bl1c + "','" + DateTime.Now.Date + "','" + ddlCash.SelectedItem.Text + "','" + ah11c + "','" + ah1258c + "')", con);
                                        cmd1964c.ExecuteNonQuery();
                                    }
                                }
                                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Name='" + account + "'", con);
                                SqlDataReader reader = cmd.ExecuteReader();

                                if (reader.Read())
                                {
                                    string ah11c;
                                    string ah1258c;
                                    ah11c = reader["No"].ToString();
                                    ah1258c = reader["AccountType"].ToString();
                                    reader.Close();
                                    con.Close();
                                    con.Open();
                                    SqlCommand cmd190cc = new SqlCommand("select * from tblGeneralLedger2 where Account='" + account + "'", con);

                                    SqlDataReader reader66790c = cmd190cc.ExecuteReader();

                                    if (reader66790c.Read())
                                    {
                                        string ah1289c;
                                        ah1289c = reader66790c["Balance"].ToString();
                                        reader66790c.Close();
                                        con.Close();
                                        con.Open();
                                        Double M1c = Convert.ToDouble(ah1289c);
                                        Double bl1c = M1c + VatFree;
                                        string total = "Income Credited for " + ddlExpense.SelectedItem.Text + " from customer " + customer;
                                        SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "' where Account='" + account + "'", con);
                                        cmd45c.ExecuteNonQuery();
                                        SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','0','" + VatFree + "','" + bl1c + "','" + DateTime.Now.Date + "','" + account + "','" + ah11c + "','" + ah1258c + "')", con);
                                        cmd1964c.ExecuteNonQuery();
                                    }
                                }
                                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                {
                                    DataTable dtBrandss = new DataTable();
                                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;

                                    SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                    using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                                    {
                                        DataTable dttax = new DataTable();
                                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
                                        //
                                        if (iss2 != 0)
                                        {
                                            SqlDataReader readers = cmdintax.ExecuteReader();
                                            if (readers.Read())
                                            {
                                                string ah1289;
                                                ah1289 = readers["Balance"].ToString();
                                                readers.Close();
                                                con.Close();
                                                con.Open();
                                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                                SqlDataReader reader66 = cmdintax.ExecuteReader();

                                                if (reader66.Read())
                                                {
                                                    string ah11;
                                                    string ah1258;
                                                    ah11 = reader66["No"].ToString();
                                                    ah1258 = reader66["AccountType"].ToString();
                                                    reader66.Close();
                                                    con.Close();
                                                    con.Open();
                                                    string total = "Income VAT Credited for " + ddlExpense.SelectedItem.Text + " from customer " + customer;
                                                    Double M1 = Convert.ToDouble(ah1289);
                                                    Double bl1 = M1 + Vat;
                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                    cmd45.ExecuteNonQuery();
                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','0','" + Vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                    cmd1974.ExecuteNonQuery();

                                                }
                                            }
                                        }
                                    }
                                }
                                SqlCommand cmddf = new SqlCommand("select * from tblIncome Order by id DESC", con);
                                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                DataTable dtdf = new DataTable();
                                sdadf.Fill(dtdf); Int64 nb = Convert.ToInt64(dtdf.Rows[0][0].ToString());
                                string url = "OtherInvoices.aspx?ref2=" + nb + "&&expname=" + ddlExpense.SelectedItem.Text + "&&paymentmode=Bank";
                                string money = "ETB";
                                string text = money + NetIncome1.ToString("#,##0.00") + " invoiced for " + ddlExpense.SelectedItem.Text;
                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + text + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-hand-holding-usd text-white','icon-circle bg bg-primary','" + url + "','MN')", con);
                                cmd197h.ExecuteNonQuery();
                                Response.Redirect("OtherInvoices.aspx?ref2=" + nb + "&&expname=" + ddlExpense.SelectedItem.Text + "&&paymentmode=Cash");
                            }
                            else
                            {
                                SqlCommand cmdbank = new SqlCommand("select * from tblbanktrans1 where account='" + ddlBank.SelectedItem.Text + "'", con);
                                using (SqlDataAdapter sda22 = new SqlDataAdapter(cmdbank))
                                {
                                    DataTable dt = new DataTable();
                                    sda22.Fill(dt); int j = dt.Rows.Count;
                                    //
                                    if (j != 0)
                                    {
                                        string total = "Expense Debited for " + ddlExpense.SelectedItem.Text + " Expense";
                                        double t = Convert.ToDouble(dt.Rows[0][5].ToString()) + NetIncome1;
                                        SqlCommand cmd45 = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where Account='" + ddlBank.SelectedItem.Text + "'", con);
                                        cmd45.ExecuteNonQuery();
                                        SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('','" + txtReference.Text + "','" + NetIncome1 + "','','" + t + "','" + ddlBank.SelectedItem.Text + "','','" + txtReference.Text + "','" + DateTime.Now.Date + "')", con);
                                        cvb.ExecuteNonQuery();

                                        ///Recording Cash
                                        SqlCommand cmd166c3 = new SqlCommand("select * from tblLedgAccTyp where Name='Cash at Bank'", con);
                                        SqlDataReader reader66c3 = cmd166c3.ExecuteReader();

                                        if (reader66c3.Read())
                                        {
                                            string ah11c;
                                            string ah1258c;
                                            ah11c = reader66c3["No"].ToString();
                                            ah1258c = reader66c3["AccountType"].ToString();
                                            reader66c3.Close();
                                            con.Close();
                                            con.Open();
                                            SqlCommand cmd190cc = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash at Bank'", con);

                                            SqlDataReader reader66790c = cmd190cc.ExecuteReader();

                                            if (reader66790c.Read())
                                            {
                                                string ah1289c;
                                                ah1289c = reader66790c["Balance"].ToString();
                                                reader66790c.Close();
                                                con.Close();
                                                con.Open();
                                                Double M1c = Convert.ToDouble(ah1289c);
                                                Double bl1c = M1c + NetIncome1;
                                                string total1 = "Cash at Bank Debited for " + ddlExpense.SelectedItem.Text + " for customer " + customer;
                                                SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "' where Account='Cash at Bank'", con);
                                                cmd45c.ExecuteNonQuery();
                                                SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total1 + "','','" + NetIncome1 + "','0','" + bl1c + "','" + DateTime.Now.Date + "','Cash at Bank','" + ah11c + "','" + ah1258c + "')", con);
                                                cmd1964c.ExecuteNonQuery();
                                            }
                                        }
                                        SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where Name='" + account + "'", con);
                                        SqlDataReader reader = cmd.ExecuteReader();

                                        if (reader.Read())
                                        {
                                            string ah11c;
                                            string ah1258c;
                                            ah11c = reader["No"].ToString();
                                            ah1258c = reader["AccountType"].ToString();
                                            reader.Close();
                                            con.Close();
                                            con.Open();
                                            SqlCommand cmd190cc = new SqlCommand("select * from tblGeneralLedger2 where Account='" + account + "'", con);

                                            SqlDataReader reader66790c = cmd190cc.ExecuteReader();

                                            if (reader66790c.Read())
                                            {
                                                string ah1289c;
                                                ah1289c = reader66790c["Balance"].ToString();
                                                reader66790c.Close();
                                                con.Close();
                                                con.Open();
                                                Double M1c = Convert.ToDouble(ah1289c);
                                                Double bl1c = M1c + VatFree;
                                                string total2 = "Income Credited for " + ddlExpense.SelectedItem.Text + " from customer " + customer;
                                                SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "' where Account='" + account + "'", con);
                                                cmd45c.ExecuteNonQuery();
                                                SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total2 + "','','0','" + VatFree + "','" + bl1c + "','" + DateTime.Now.Date + "','" + account + "','" + ah11c + "','" + ah1258c + "')", con);
                                                cmd1964c.ExecuteNonQuery();
                                            }
                                        }
                                        SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                                        using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                                        {
                                            DataTable dtBrandss = new DataTable();
                                            sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;

                                            SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                            using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                                            {
                                                DataTable dttax = new DataTable();
                                                sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
                                                //
                                                if (iss2 != 0)
                                                {
                                                    SqlDataReader readers = cmdintax.ExecuteReader();
                                                    if (readers.Read())
                                                    {
                                                        string ah1289;
                                                        ah1289 = readers["Balance"].ToString();
                                                        readers.Close();
                                                        con.Close();
                                                        con.Open();
                                                        SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                                        SqlDataReader reader66 = cmdintax.ExecuteReader();

                                                        if (reader66.Read())
                                                        {
                                                            string ah11;
                                                            string ah1258;
                                                            ah11 = reader66["No"].ToString();
                                                            ah1258 = reader66["AccountType"].ToString();
                                                            reader66.Close();
                                                            con.Close();
                                                            con.Open();
                                                            string total1 = "Income VAT Credited for " + ddlExpense.SelectedItem.Text + " from customer " + customer;
                                                            Double M1 = Convert.ToDouble(ah1289);
                                                            Double bl1 = M1 + Vat;
                                                            SqlCommand cmd451 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                            cmd451.ExecuteNonQuery();
                                                            SqlCommand cmd19741 = new SqlCommand("insert into tblGeneralLedger values('" + total1 + "','','0','" + Vat + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                            cmd19741.ExecuteNonQuery();

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                SqlCommand cmddf = new SqlCommand("select * from tblIncome Order by id DESC", con);
                                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                DataTable dtdf = new DataTable();
                                sdadf.Fill(dtdf); Int64 nb = Convert.ToInt64(dtdf.Rows[0][0].ToString());
                                string url = "OtherInvoices.aspx?ref2=" + nb + "&&expname=" + ddlExpense.SelectedItem.Text + "&&paymentmode=Bank";
                                string money = "ETB";
                                string text = money + NetIncome1.ToString("#,##0.00") + " invoiced for " + ddlExpense.SelectedItem.Text;
                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + text + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-hand-holding-usd text-white','icon-circle bg bg-primary','" + url + "','MN')", con);
                                cmd197h.ExecuteNonQuery();
                                Response.Redirect("OtherInvoices.aspx?ref2=" + nb + "&&expname=" + ddlExpense.SelectedItem.Text + "&&paymentmode=Bank");
                            }
                        }

                    }

                }
            }
        }
        private void bindotherinvoice()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblIncome";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            DataView dvData = new DataView(dt);
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            con.Visible = true;
            String PID = "report";
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "invoce_" + PID + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/x-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            con.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        protected void btnAmountCondition_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                str = "select * from tblIncome where amount > '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            else if (less.Checked == true)
            {
                str = "select * from tblIncome where amount < '" + txtFilteredAmount.Text + "'";
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
                str = "select * from tblIncome where amount = '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblIncome where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            DataView dvData = new DataView(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        private void BindBrandsRptr2()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String PID2 = Convert.ToString(Request.QueryString["expname"]);
            txtReference.Text = "RAKS-" + RandomPassword();
            if (Request.QueryString["ref2"] != null)
            {
                leaveempt.Visible = false;
                showdetail.Visible = true;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();

                str = "select * from tblIncome";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                dvData.Sort = ViewState["Column"] + " " + ViewState["Sortorder"];

                Repeater1.DataSource = dvData;
                Repeater1.DataBind();
                con.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();

                str = "select * from tblIncome";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                dvData.Sort = ViewState["Column"] + " " + ViewState["Sortorder"];
                Repeater1.DataSource = dvData;
                Repeater1.DataBind();
                leaveempt.Visible = true;
                showdetail.Visible = false;
            }

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
            BindBrandsRptr2();
        }
        private string bindIncomeaccount()
        {
            String PID = Convert.ToString(Request.QueryString["expname"]);
            string income = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from tblIncomeBrand where incomename='" + PID + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    income += readerAC["incomeaccount"].ToString();
                }
            }
            return income;
        }
        private void adjustLedger_debit()
        {
            String PID2 = Convert.ToString(Request.QueryString["ref2"]);
            String PID = Convert.ToString(Request.QueryString["expname"]);
            string CashorBank = "";
            if (PaymentMode.InnerText == "Cash") { CashorBank += "Cash on Hand"; }
            else { CashorBank += "Cash at Bank"; }
            string explanation = "Account adjustment for " + PID + " invoice number " + PID2;
            double diff = -bindDifferenece();
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'  ", con);
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
                            Double bl1 = M1 + diff;

                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + diff + "','','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','Cash')", con);
                            cmd1974.ExecuteNonQuery();
                        }
                    }
                }
                //Selecting from account prefernce
                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dtBrandss = new DataTable();
                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                    //Selecting from Income account
                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + bindIncomeaccount() + "'", con);
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
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + bindIncomeaccount() + "'", con);

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
                                    double income = diff / 1.15;
                                    Double bl1 = M1 + income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + bindIncomeaccount() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + bindIncomeaccount() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                    //Selecting from cash acccount
                    SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                    using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                    {
                        DataTable dttax = new DataTable();
                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmdintax.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                    double vatfree = diff / 1.15;
                                    double income = diff - vatfree;
                                    Double bl1 = M1 + income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                }
            }

        }

        private void adjustLedger_credit_delete()
        {
            String PID2 = Convert.ToString(Request.QueryString["ref2"]);
            String PID = Convert.ToString(Request.QueryString["expname"]);
            string CashorBank;
            if (PaymentMode.InnerText == "Cash") { CashorBank = "Cash on Hand"; }
            else { CashorBank = "Cash at Bank"; }
            string explanation = "Account adjustment for " + PID + " invoice number " + PID2 + " deletion";
            double diff = Convert.ToDouble(DupGrandTotal.InnerText);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'", con);
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
                            Double bl1 = M1 - diff;

                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','','" + diff + "','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','Cash')", con);
                            cmd1974.ExecuteNonQuery();
                        }
                    }
                }
                //Selecting from account prefernce
                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dtBrandss = new DataTable();
                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                    //Selecting from Income account
                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + bindIncomeaccount() + "'", con);
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
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + bindIncomeaccount() + "'", con);

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
                                    double income = diff / 1.15;
                                    Double bl1 = M1 - income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1.ToString("#,##0.00") + "' where Account='" + bindIncomeaccount() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + income + "','0','" + bl1.ToString("#,##0.00") + "','" + DateTime.Now + "','" + bindIncomeaccount() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                    //Selecting from cash acccount
                    SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                    using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                    {
                        DataTable dttax = new DataTable();
                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmdintax.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                    double vatfree = diff / 1.15;
                                    double income = diff - vatfree;
                                    Double bl1 = M1 - income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1.ToString("#,##0.00") + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + income.ToString("#,##0.00") + "','0','" + bl1.ToString("#,##0.00") + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                }
            }
        }
        private void bind_customer_statement_adjustment_deleted()
        {
            string reftag = Convert.ToString(RefNum.InnerText);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand custcmdw1 = new SqlCommand("delete from tblCustomerStatement where Trans='" + reftag + "'", con);
                custcmdw1.ExecuteNonQuery();
            }
        }

        private void bind_customer_statement_adjustment()
        {
            String PID = Convert.ToString(Request.QueryString["expname"]);
            String PID2 = Convert.ToString(Request.QueryString["ref2"]);
            string explanation = "Account adjustment for " + PID2 + " invoice number " + PID;
            string reftag = Convert.ToString(RefNum.InnerText).Substring(5);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand custcmd = new SqlCommand("delete tblCustomerStatement where Trans='" + reftag + "'", con);
                custcmd.ExecuteNonQuery();
                SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                if (readerbcustb.Read())
                {
                    string ah11;

                    ah11 = readerbcustb["Balance"].ToString();
                    readerbcustb.Close();


                    double new_balance = Convert.ToDouble(ah11);

                    SqlCommand custcmdw1 = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + reftag + "','','" + txtEditAmount.Text + "','" + Convert.ToDouble(txtEditAmount.Text) + "','" + new_balance + "','" + PID + "')", con);
                    custcmdw1.ExecuteNonQuery();
                }
            }
        }
        private void adjustLedger_credit()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);

            String PID2 = Convert.ToString(Request.QueryString["expname"]);
            string CashorBank;
            if (PaymentMode.InnerText == "Cash") { CashorBank = "Cash on Hand"; }
            else { CashorBank = "Cash at Bank"; }
            string explanation = "Account adjustment for " + PID2 + " invoice number " + PID;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'", con);
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
                            Double bl1 = M1 - bindDifferenece();

                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','','" + bindDifferenece() + "','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','Cash')", con);
                            cmd1974.ExecuteNonQuery();
                        }
                    }
                }
                //Selecting from account prefernce
                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dtBrandss = new DataTable();
                    sdas.Fill(dtBrandss); int iss = dtBrandss.Rows.Count;
                    //Selecting from Income account
                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + bindIncomeaccount() + "'", con);
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
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + bindIncomeaccount() + "'", con);

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
                                    double income = bindDifferenece() / 1.15;
                                    Double bl1 = M1 - income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + bindIncomeaccount() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + income + "','0','" + bl1 + "','" + DateTime.Now + "','" + bindIncomeaccount() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                    //Selecting from cash acccount
                    SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                    using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                    {
                        DataTable dttax = new DataTable();
                        sdatax.Fill(dttax); int iss2 = dttax.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmdintax.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                SqlDataReader reader66 = cmdintax.ExecuteReader();

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
                                    double vatfree = bindDifferenece() / 1.15;
                                    double income = bindDifferenece() - vatfree;
                                    Double bl1 = M1 - income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + income + "','0','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                }
            }
        }
        private double bindDifferenece()
        {
            double difference = Convert.ToDouble(DupGrandTotal.InnerText) - Convert.ToDouble(txtEditAmount.Text);
            return difference;
        }
        private void calculateTotal()
        {
            String PID2 = Convert.ToString(Request.QueryString["expname"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from tblIncomeBrand where incomename='" + PID2 + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String account = readerAC["incomeaccount"].ToString();
                    String rate = readerAC["rate"].ToString();
                    String unit = readerAC["unit"].ToString();

                    readerAC.Close();
                    //Inserting to income Table
                    Double NetIncome1 = 0; Double VatFree = 0; Double Vat = 0;
                    if (rate == "1")
                    {
                        NetIncome1 = Convert.ToDouble(txtAmountEdited.Text);
                        VatFree = NetIncome1 / 1.15;
                        Vat = NetIncome1 - VatFree;
                        SqlCommand cmdin = new SqlCommand("update tblIncome set amount='" + NetIncome1 + "', qty='" + txtAmountEdited.Text + "'", con);
                        cmdin.ExecuteNonQuery();
                    }
                    else
                    {
                        NetIncome1 = Convert.ToDouble(txtAmountEdited.Text) * Convert.ToDouble(rate) * 0.15 + Convert.ToDouble(txtAmountEdited.Text) * Convert.ToDouble(rate);
                        SqlCommand cmdin = new SqlCommand("update tblIncome set amount='" + NetIncome1 + "', qty='" + txtAmountEdited.Text + "'", con);
                        cmdin.ExecuteNonQuery();
                    }
                    txtEditAmount.Text = NetIncome1.ToString();
                }
            }
        }
        protected void btnEditInsert_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String PID2 = Convert.ToString(Request.QueryString["expname"]);
            if (txtAmountEdited.Text == "")
            {
                string message = "Please assign the total amount!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                calculateTotal();
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    double total = Convert.ToDouble(txtEditAmount.Text);

                    string url = "OtherInvoices.aspx?id=" + PID + "&&expname=" + PID2;
                    string exlanation = PID2 + " Invoice# " + PID + " has been updated from ETB " + Convert.ToDouble(DupGrandTotal.InnerText).ToString("#,##0.00") + " to " + total.ToString("#,##0.00");
                    if (Checkbox2.Checked == true)
                    {
                        bind_customer_statement_adjustment();
                        double diff = bindDifferenece();
                        if (diff > 0)
                        {
                            adjustLedger_credit();
                        }
                        else if (diff < 0)
                        {
                            adjustLedger_debit();
                        }
                        else
                        {

                        }
                    }
                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-edit text-white','icon-circle bg bg-primary','" + url + "','MN')", con);
                    cmd197h.ExecuteNonQuery();

                    Response.Redirect("OtherInvoices.aspx?ref2=" + PID + "&&expname=" + PID2);
                }
            }
        }

        protected void btnDelete1_Click(object sender, EventArgs e)
        {
            string reftag = Convert.ToString(RefNum.InnerText);
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String PID2 = vendor1.InnerText;
            String PID3 = Convert.ToString(Request.QueryString["expname"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                if (deleteCheck.Checked == true)
                {
                    adjustLedger_credit_delete();
                    bind_customer_statement_adjustment_deleted();

                }
                SqlCommand cmdreb1 = new SqlCommand("delete tblIncome  where id='" + PID + "'", con);
                cmdreb1.ExecuteNonQuery();

                string exlanation = PID3 + " Invoice# " + PID + " has been deleted";
                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-trash text-white','icon-circle bg bg-danger','OtherInvoices.aspx','MN')", con);
                cmd197h.ExecuteNonQuery();

                Response.Redirect("OtherInvoices.aspx");
            }
        }
        private void GetFontSizeAndHeadingName()
        {
            string heading_name = ""; string fontsize = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblOtherInvoiceCustomizing", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    heading_name = reader["invoice_name"].ToString();
                    fontsize = reader["font_size"].ToString();
                    txtFontsize.Text = fontsize;
                    txtInvoiceName.Text = heading_name.ToUpper();
                    InvoiceName.InnerText = heading_name;
                    BodyFontSizeDiv1.Style.Add("font-size", fontsize + "px");
                    BodyFontSizeDiv2.Style.Add("font-size", fontsize + "px");
                    BodyFontSize3.Style.Add("font-size", fontsize + "px");
                }
            }
        }
        private void GetColumnVisibilityValues()
        {
            string col_num = ""; string col_unit = ""; string col_rate = ""; string col_qty = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblOtherInvoiceCustomizing", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    col_num = reader["col_number_visibility"].ToString();
                    col_unit = reader["col_unit_visibility"].ToString();
                    col_rate = reader["col_rate_visibility"].ToString();
                    col_qty = reader["col_qty_visibility"].ToString();
                    //Logo
                    if (col_num == "True")
                    {
                        NumbCheck.Checked = true;
                        Col_number_header.Visible = true;
                        Col_number_body.Visible = true;
                    }
                    else
                    {
                        NumbCheck.Checked = false;
                        Col_number_header.Visible = false;
                        Col_number_body.Visible = false;
                    }
                    //Credit Div
                    if (col_unit == "True")
                    {
                        UnitCheck.Checked = true;
                        Col_unitbase_header.Visible = true;
                        Col_unitbase_body.Visible = true;
                    }
                    else
                    {
                        UnitCheck.Checked = false;
                        Col_unitbase_header.Visible = false;
                        Col_unitbase_body.Visible = false;
                    }
                    //WaterMark 
                    if (col_rate == "True")
                    {
                        RateCheck.Checked = true;
                        Col_ratebase_header.Visible = true;
                        Col_ratebase_body.Visible = true;
                    }
                    else
                    {
                        RateCheck.Checked = false;
                        Col_ratebase_header.Visible = false;
                        Col_ratebase_body.Visible = false;
                    }
                    //WaterMark Div
                    if (col_qty == "True")
                    {
                        QtyCheck.Checked = true;
                        Col_Qtybase_header.Visible = true;
                        Col_Qtybase_body.Visible = true;
                    }
                    else
                    {
                        QtyCheck.Checked = false;
                        Col_Qtybase_header.Visible = false;
                        Col_Qtybase_body.Visible = false;
                    }
                }
            }
        }
        private void GetOtherVisiblityValues()
        {
            string logo_visibility_value = ""; string watermark_visibility_value = "";
            string invoiceType_visibility_value = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblOtherInvoiceCustomizing", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    logo_visibility_value = reader["logo_visibility"].ToString();
                    watermark_visibility_value = reader["atachment_visibility"].ToString();
                    invoiceType_visibility_value = reader["invoice_type_visibility"].ToString();
                    if (logo_visibility_value == "True")
                    {
                        logoCheck.Checked = true;
                    }
                    else
                    {
                        logoElement.Visible = false;
                        logoCheck.Checked = false;
                    }
                    //
                    if (watermark_visibility_value == "True")
                    {
                        attachCheck.Checked = true;
                    }
                    else
                    {
                        attachCheck.Checked = false;
                        RaksTDiv.Visible = false;
                    }
                    //
                    if (invoiceType_visibility_value == "True")
                    {
                        invCheck.Checked = true;
                    }
                    else
                    {
                        invCheck.Checked = false;
                        ExpenseType.Visible = false;
                    }
                }
            }
        }
        protected void btnCustomizeSave_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand cmdin = new SqlCommand("update tblOtherInvoiceCustomizing set invoice_name='" + txtInvoiceName.Text + "', invoice_type_visibility='" + invCheck.Checked + "'" +
                    ", logo_visibility='" + logoCheck.Checked + "', atachment_visibility='" + attachCheck.Checked + "', font_size='" + txtFontsize.Text + "'" +
                    ",col_number_visibility='" + NumbCheck.Checked + "',col_unit_visibility='" + UnitCheck.Checked + "',col_rate_visibility='" + RateCheck.Checked + "',col_qty_visibility='" + QtyCheck.Checked + "'", con);
                cmdin.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}
