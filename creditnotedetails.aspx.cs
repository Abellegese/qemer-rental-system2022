using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{

    public partial class creditnotedetails : System.Web.UI.Page
    {

        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    bindcompany(); BindMainCategory23(); bindbankaccount(); bindstatus();
                    bindTotals(); BindShopNo(); BindReference(); bindFSnumber(); bindexpenseaccount();
                    datecurrent.InnerText = DateTime.Now.ToString("MMMM dd, yyyy"); bindid();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }

        private void bindFSnumber()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcrd1 = new SqlCommand("select TOP 1* from tblrentreceipt order by id desc", con);
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
                        txtFSNo1.Text = "0000" + (Convert.ToInt64(FSNumber) + 1).ToString();
                    }
                }
            }
        }
        private void bindid()
        {
            if (Request.QueryString["ref"] != null)
            {
                String PID2 = Convert.ToString(Request.QueryString["ref"]);
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select* from tblcreditnote where ref='" + PID2 + "'", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string refe = reader["id"].ToString();
                        Response.Redirect("creditnotedetails.aspx" + "?ref2=" + refe + "&&cust=" + PID);
                    }
                }
            }
        }

        private void bindstatus()
        {

            String PID = Convert.ToString(Request.QueryString["cust"]);
            DropDownList1.Items.Insert(0, new ListItem(PID, "0"));
            DropDownList3.Items.Insert(0, new ListItem(PID, "0"));
            String PID2 = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select* from tblcreditnote where customer='" + PID + "' and id='" + PID2 + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string balance;
                    balance = reader["balance"].ToString();
                    string refe = reader["ref"].ToString();
                    string notes = reader["Notes"].ToString();
                    Ref.InnerText = refe;
                    Notes.InnerText = notes;
                    CreditTitle.InnerText = notes;
                    RedInv.HRef = "rentinvoicereport.aspx?search=i-" + refe;
                    txtCash.Text = Convert.ToDouble(balance).ToString("#,##0.00");
                    txtCash1.Text = Convert.ToDouble(balance).ToString("#,##0.00");
                    if (balance == "0.0000" || balance == null || balance == "")
                    {
                        paybutoon.Visible = false;
                        status_indicator.InnerText = "Completed";
                        status_indicator.Attributes.Add("class", "badge badge-success");
                    }
                    else if (Convert.ToDouble(balance) > 0)
                    {
                        paybutoon.Visible = true;
                        modalwriteoff.Visible = true;
                        status_indicator.InnerText = "Pending";
                        status_indicator.Attributes.Add("class", "badge badge-danger");
                    }
                    else
                    {
                        paybutoon.Visible = false;
                        modalwriteoff.Visible = false;
                        status_indicator.InnerText = "Writte Off";
                        status_indicator.Attributes.Add("class", "badge badge-warning");
                    }
                }
            }
        }
        private void BindShopNo()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    string shopno = reader["shopno"].ToString();
                    ShopNo.InnerText = shopno;
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
                    DropDownList4.DataSource = dt;
                    DropDownList4.DataTextField = "AccountName";
                    DropDownList4.DataValueField = "AC";
                    DropDownList4.DataBind();
                    DropDownList4.Items.Insert(0, new ListItem("-Select-", "0"));
                    //

                }
            }
        }
        private void BindMainCategory23()
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
                    DropDownList2.DataSource = dt;
                    DropDownList2.DataTextField = "Name";
                    DropDownList2.DataValueField = "ACT";
                    DropDownList2.DataBind();
                    DropDownList2.Items.Insert(0, new ListItem("Cash on Hand", "0"));
                    //

                }
            }
        }
        private readonly Random _random = new Random();
        public long RandomNumber(int min, int max)
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
        private void BindReference()
        {
            TextBox4.Text = "RAKS-" + RandomPassword();
            TextBox1.Text = "RAKS-" + RandomPassword();
        }
        private Tuple<string, long> GetReceiptUrl()
        {
            string PID = Convert.ToString(Request.QueryString["cust"]);
            string url = ""; long nb = 0;
            string CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                DataTable dtdf = new DataTable();
                sdadf.Fill(dtdf); nb = dtdf.Rows.Count + 1;

                url += "rentinvoicereport.aspx?id=" + nb + "&&cust=" + PID + "&&paymentmode=Cash";
            }
            return Tuple.Create(url, nb);
        }

        protected void Save(object sender, EventArgs e)
        {
            //bool IsReferenceFound = FindReferenceNumber(TextBox4.Text);
            ReferenceFinder RF = new ReferenceFinder();
            RF.ReferenceNumber = TextBox4.Text;
            bool IsReferenceFound = RF.FindReferenceNumber();
            string url = GetReceiptUrl().Item1; long nb = GetReceiptUrl().Item2;
            string PID2 = Convert.ToString(Request.QueryString["ref2"]);
            string PID = Convert.ToString(Request.QueryString["cust"]);
            if (IsReferenceFound == true)
            {
                string message = "Reference Number Already Exist";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                string CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    //Invoice Recording
                    if (DropDownList1.SelectedItem.Text == "-Select-" || DropDownList2.SelectedItem.Text == "-Select-" || TextBox4.Text == "")
                    {
                        lblError.Text = "Please fill the required input"; lblError.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            SqlCommand cmd2vb = new SqlCommand("select * from tblcreditnote where id='" + PID2 + "'", con);
                            SqlDataReader readervb = cmd2vb.ExecuteReader();
                            if (readervb.Read())
                            {
                                string kc;

                                kc = readervb["Balance"].ToString();
                                string calculatedouble = Convert.ToDouble(kc).ToString("#,##0.00");
                                readervb.Close();

                                Double D = Convert.ToDouble(calculatedouble) - Convert.ToDouble(txtCash.Text);

                                if (D == 0 || D > 0)
                                {
                                    double due = Convert.ToDouble(txtCash.Text);
                                    SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                    if (readerbcustb.Read())
                                    {
                                        string ah11bn;

                                        ah11bn = readerbcustb["Balance"].ToString();
                                        readerbcustb.Close();

                                        double payment = Convert.ToDouble(txtCash.Text);

                                        double balancedue = Convert.ToDouble(ah11bn);

                                        double remain = balancedue - payment;
                                        string total12 = PID + " payment for credit through Cash for credit-#" + PID2;
                                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + total12 + "','','','" + txtCash.Text + "','" + remain + "','" + PID + "')", con);
                                        custcmd.ExecuteNonQuery();
                                        SqlCommand cmd45vvb = new SqlCommand("Update tblcreditnote set Balance='" + D + "'  where id='" + PID2 + "'", con);

                                        cmd45vvb.ExecuteNonQuery();
                                        //
                                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                        if (readerAC.Read())
                                        {
                                            String FN = readerAC["Name"].ToString();
                                            readerAC.Close();
                                            con.Close();
                                            //Activity
                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + DropDownList1.SelectedItem.Text + "','Payment received from'+' '+'<b>" + DropDownList1.SelectedItem.Text + "</b>'+' '+'Was Recorded','" + FN + "','creditnote.aspx')", con);
                                            con.Open();
                                            cmdAc.ExecuteNonQuery();
                                            string money = "ETB";
                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtCash.Text).ToString("#,##0.00") + "'+' '+'Deposited into Cash on Hand account from credit','" + FN + "','" + DropDownList1.SelectedItem.Text + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                            cmd197h.ExecuteNonQuery();
                                        }
                                        SqlCommand cmd190c = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
                                        using (SqlDataAdapter sda22 = new SqlDataAdapter(cmd190c))
                                        {
                                            DataTable dtBrands232 = new DataTable();
                                            sda22.Fill(dtBrands232); long i2 = dtBrands232.Rows.Count;
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
                                                    SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='Accounts Receivable'", con);

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
                                                        Double bl1 = M1 - (Convert.ToDouble(txtCash.Text) + Convert.ToDouble(txtDiscount.Text));
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + txtCash.Text + "', Debit='', Explanation='cash payment from customer Sales', Date='" + DateTime.Now + "' where Account='Accounts Receivable'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        string total = "cash payment from customer- " + DropDownList1.SelectedItem.Text + "-#" + PID2;
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','0','" + txtCash.Text + "','" + bl1 + "','" + DateTime.Now + "','Accounts Receivable','" + ah11 + "','" + ah1258 + "')", con);
                                                        cmd1974.ExecuteNonQuery();

                                                        SqlCommand cmd190h7c = new SqlCommand("select * from tblGeneralLedger2 where Account='" + DropDownList2.SelectedItem.Text + "'", con);
                                                        using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd190h7c))
                                                        {
                                                            DataTable dtBrands232c3 = new DataTable();
                                                            sda22c3.Fill(dtBrands232c3); long i2c3 = dtBrands232c3.Rows.Count;
                                                            //
                                                            if (i2c3 != 0)
                                                            {
                                                                SqlCommand cmd166c3 = new SqlCommand("select * from tblLedgAccTyp where Name='" + DropDownList2.SelectedItem.Text + "'", con);
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
                                                                    Double vb = Convert.ToDouble(txtCash.Text) + Convert.ToDouble(txtDiscount.Text);
                                                                    //Selecting balance for the sales account

                                                                    SqlCommand cmd190cc = new SqlCommand("select * from tblGeneralLedger2 where Account='" + DropDownList2.SelectedItem.Text + "'", con);

                                                                    SqlDataReader reader66790c = cmd190cc.ExecuteReader();

                                                                    if (reader66790c.Read())
                                                                    {
                                                                        string ah1289c;
                                                                        ah1289c = reader66790c["Balance"].ToString();
                                                                        reader66790c.Close();
                                                                        con.Close();
                                                                        con.Open();
                                                                        Double M1c = Convert.ToDouble(ah1289c);
                                                                        Double bl1c = M1c + Convert.ToDouble(txtCash.Text);
                                                                        SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "', Debit='" + txtCash.Text + "', Explanation='cash payment from customer Sales', Date='" + DateTime.Now + "' where Account='" + DropDownList2.SelectedItem.Text + "'", con);
                                                                        cmd45c.ExecuteNonQuery();
                                                                        SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','" + txtCash.Text + "','0','" + bl1c + "','" + DateTime.Now + "','" + DropDownList2.SelectedItem.Text + "','" + ah11c + "','" + ah1258c + "')", con);
                                                                        cmd1964c.ExecuteNonQuery();
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }

                                                }

                                            }
                                        }
                                    }
                                    if (CheckGene.Checked == true)
                                    {


                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + TextBox4.Text + "','" + DateTime.Now.Date + "','0','" + due + "','','" + nb + "','" + txtFSNo.Text + "','Cash')", con);
                                        cmdri.ExecuteNonQuery();
                                        Response.Redirect(url);
                                    }
                                    else
                                    {
                                        Response.Redirect("creditnotedetails.aspx" + "?ref2=" + PID2 + "&&cust=" + PID);
                                    }
                                }
                                else
                                {
                                    lblError.Text = "The Enetered Amount is greate than the due amount"; lblError.ForeColor = Color.Red;
                                }

                            }
                        }
                    }
                }
            }
        }
        protected void Save1(object sender, EventArgs e)
        {
            ReferenceFinder RF1 = new ReferenceFinder(TextBox1.Text);
            bool IsReferenceFound = RF1.FindReferenceNumber();
            string url = GetReceiptUrl().Item1; long nb = GetReceiptUrl().Item2;
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String PID2 = Convert.ToString(Request.QueryString["cust"]);
            string refe = Convert.ToString(TextBox1.Text);
            if (IsReferenceFound == true)
            {
                string message = "Reference Number Already Exist";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    //Invoice Recording
                    if (DropDownList3.SelectedItem.Text == "-Select-" || DropDownList4.SelectedItem.Text == "-Select-" || TextBox5.Text == "")
                    {
                        lblError.Text = "Please fill the required input"; lblError.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            SqlCommand cmd2vb = new SqlCommand("select * from tblcreditnote where id='" + PID + "'", con);
                            SqlDataReader readervb = cmd2vb.ExecuteReader();
                            if (readervb.Read())
                            {
                                string kc;

                                kc = readervb["balance"].ToString();
                                string calculatedouble = Convert.ToDouble(kc).ToString("#,##0.00");
                                readervb.Close();

                                Double D = Convert.ToDouble(calculatedouble) - Convert.ToDouble(txtCash1.Text);

                                if (D == 0 || D > 0)
                                {
                                    SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID2 + "' ORDER BY CSID DESC", con);

                                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                    if (readerbcustb.Read())
                                    {
                                        string ah11bn;

                                        ah11bn = readerbcustb["Balance"].ToString();
                                        readerbcustb.Close();

                                        double payment = Convert.ToDouble(txtCash1.Text);

                                        double balancedue = Convert.ToDouble(ah11bn);

                                        double remain = balancedue - payment;
                                        string total12 = PID2 + " payment for credit-#" + PID;
                                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + total12 + "','','','" + txtCash1.Text + "','" + remain + "','" + PID2 + "')", con);
                                        custcmd.ExecuteNonQuery();
                                        SqlCommand cmd45vvb = new SqlCommand("Update tblcreditnote set Balance='" + D + "'  where id='" + PID + "'", con);

                                        cmd45vvb.ExecuteNonQuery();
                                        SqlCommand cmdbank = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList4.SelectedItem.Text + "'", con);
                                        using (SqlDataAdapter sda22 = new SqlDataAdapter(cmdbank))
                                        {
                                            DataTable dt = new DataTable();
                                            sda22.Fill(dt); long j = dt.Rows.Count;
                                            string totalannounc = PID2 + " Paid through bank with ref# " + refe;
                                            if (j != 0)
                                            {

                                                double t = Convert.ToDouble(dt.Rows[0][5].ToString()) + Convert.ToDouble(txtCash1.Text);
                                                SqlCommand cmd45 = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where Account='" + DropDownList4.SelectedItem.Text + "'", con);
                                                cmd45.ExecuteNonQuery();
                                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('','" + TextBox5.Text + "','" + txtCash1.Text + "','0','" + t + "','" + DropDownList4.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now + "')", con);
                                                cvb.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                double t = Convert.ToDouble(txtCash1.Text);
                                                SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('','" + TextBox5.Text + "','" + txtCash1.Text + "','0','" + t + "','" + DropDownList4.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now + "')", con);
                                                cvb1.ExecuteNonQuery();
                                                SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('','" + TextBox5.Text + "','" + txtCash1.Text + "','0','" + t + "','" + DropDownList4.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now + "')", con);
                                                b.ExecuteNonQuery();
                                                con.Close();
                                            }
                                        }
                                        con.Close();
                                        con.Open();
                                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                        if (readerAC.Read())
                                        {
                                            String FN = readerAC["Name"].ToString();
                                            readerAC.Close();
                                            con.Close();
                                            //Activity
                                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer through cheque','" + DropDownList3.SelectedItem.Text + "','Payment received from'+' '+'<b>" + DropDownList3.SelectedItem.Text + "</b>'+' '+'Was Recorded','" + FN + "','creditnote.aspx')", con);
                                            con.Open();
                                            cmdAc.ExecuteNonQuery();
                                            string money = "ETB";
                                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtCash1.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + DropDownList1.SelectedItem.Text + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','" + url + "','MN')", con);
                                            cmd197h.ExecuteNonQuery();
                                        }
                                        SqlCommand cmd190c = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
                                        using (SqlDataAdapter sda22 = new SqlDataAdapter(cmd190c))
                                        {
                                            DataTable dtBrands232 = new DataTable();
                                            sda22.Fill(dtBrands232); long i2 = dtBrands232.Rows.Count;
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
                                                    SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='Accounts Receivable'", con);

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
                                                        Double bl1 = M1 - (Convert.ToDouble(txtCash1.Text) + Convert.ToDouble(txtDiscount1.Text));
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + txtCash1.Text + "', Debit='', Explanation='cash payment from customer Sales', Date='" + DateTime.Now + "' where Account='Accounts Receivable'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('cash payment from customer Sales','','0','" + txtCash1.Text + "','" + bl1 + "','" + DateTime.Now + "','Accounts Receivable','" + ah11 + "','" + ah1258 + "')", con);
                                                        cmd1974.ExecuteNonQuery();
                                                        //Checking Cash account in the general ledger
                                                        con.Close();
                                                        con.Open();
                                                        SqlCommand cmd190h7c = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash at Bank'", con);
                                                        using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd190h7c))
                                                        {
                                                            DataTable dtBrands232c3 = new DataTable();
                                                            sda22c3.Fill(dtBrands232c3); long i2c3 = dtBrands232c3.Rows.Count;
                                                            //
                                                            if (i2c3 != 0)
                                                            {
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
                                                                    Double vb = Convert.ToDouble(txtCash1.Text) + Convert.ToDouble(txtDiscount1.Text);

                                                                    //Selecting balance for the sales account

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
                                                                        Double bl1c = M1c + Convert.ToDouble(txtCash1.Text);
                                                                        SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "', Debit='" + txtCash1.Text + "', Explanation='cash payment from customer Sales', Date='" + DateTime.Now + "' where Account='Cash at Bank'", con);
                                                                        cmd45c.ExecuteNonQuery();
                                                                        SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('cash payment from customer Sales','','" + txtCash1.Text + "','0','" + bl1c + "','" + DateTime.Now + "','Cash at Bank','" + ah11c + "','" + ah1258c + "')", con);
                                                                        cmd1964c.ExecuteNonQuery();
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }

                                                }

                                            }
                                        }
                                    }
                                    if (Checkbox1.Checked == true)
                                    {
                                        double due = Convert.ToDouble(txtCash1.Text);

                                        SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID2 + "','" + TextBox1.Text + "','" + DateTime.Now.Date + "','0','" + due + "','','" + nb + "','" + txtFSNo1.Text + "','Bank')", con);
                                        cmdri.ExecuteNonQuery();
                                        Response.Redirect(url);
                                    }
                                    else
                                    {
                                        Response.Redirect("creditnotedetails.aspx" + "?ref2=" + PID + "&&cust=" + PID2);
                                    }
                                }
                                else
                                {
                                    lblError.Text = "The Enetered Amount is greate than the due amount"; lblError.ForeColor = Color.Red;
                                }
                            }
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
                SqlCommand cmd = new SqlCommand("select Oname,BuissnessLocation,Contact from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company; string address; string cont;
                    company = reader["Oname"].ToString();
                    address = reader["BuissnessLocation"].ToString();
                    cont = reader["Contact"].ToString();
                    addressname.InnerText = "Address: " + address;
                    oname.InnerText = company;
                    WaterMarkOname.InnerText = company;
                    phone.InnerText = "Contact: " + cont;

                }
            }
        }
        private void bindTotals()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (Request.QueryString["cust"] != null)
                {
                    String PID = Convert.ToString(Request.QueryString["cust"]);
                    String PID2 = Convert.ToString(Request.QueryString["ref2"]);
                    CN.InnerText = PID2; CN3.InnerText = PID2; CRNo.InnerText = "CN# " + PID2;
                    Name.InnerText = PID;
                    SqlCommand cmd = new SqlCommand("select amount,date,balance from tblcreditnote where customer='" + PID + "' and id='" + PID2 + "'", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string cash;
                        string duedate;
                        string balance1;
                        cash = reader["amount"].ToString();
                        duedate = reader["date"].ToString();
                        dateofcredit.InnerText = Convert.ToDateTime(duedate).ToString("MMMM dd, yyyy");
                        DateTime today = DateTime.Now.Date;
                        DateTime duedate1 = Convert.ToDateTime(duedate);
                        TimeSpan t = today - duedate1;
                        string dayleft = t.TotalDays.ToString();
                        lblAged.InnerText = dayleft + " days";
                        balance1 = reader["balance"].ToString();
                        Total.InnerText = Convert.ToDouble(cash).ToString("#,##0.00");
                        if (Convert.ToDouble(balance1) < 0) { balance.InnerText = (-Convert.ToDouble(balance1)).ToString("#,##0.00"); }
                        else { balance.InnerText = Convert.ToDouble(balance1).ToString("#,##0.00"); }
                        txtWriteOffAmount.Text = Convert.ToDouble(balance1).ToString("#,##0.00");
                    }
                }
            }
        }
        private void bindexpenseaccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where AccountType='Expenses'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlExpense.DataSource = dt;
                    ddlExpense.DataTextField = "Name";
                    ddlExpense.DataValueField = "ACT";
                    ddlExpense.DataBind();
                    ddlExpense.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
        protected void WriteOff(object sender, EventArgs e)
        {
            if (ddlExpense.SelectedItem.Text == "-Select-")
            {
                string message = "Please Select Expense Account!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else if (Convert.ToDouble(txtWriteOffAmount.Text) > Convert.ToDouble(balance.InnerText))
            {
                string message = "Write off amount can not be exceeded the credit balance amount !!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else if (Convert.ToDouble(txtWriteOffAmount.Text) > Convert.ToDouble(balance.InnerText) && ddlExpense.SelectedItem.Text == "-Select-")
            {
                string message = "1) Write off amount can not be exceeded the credit balance amount !! 2) Select expense account";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {                 
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String PID2 = Convert.ToString(Request.QueryString["ref2"]);  
                string explanation = "Receivable written off from " + PID + " with credit number " + PID2;
                double writeoff_amount = Convert.ToDouble(txtWriteOffAmount.Text);
                GeneralLedger GLWriteOff = new GeneralLedger(ddlExpense.SelectedItem.Text,explanation,writeoff_amount);
                GLWriteOff.increaseDebitAccount();
                //Removing from Accounts Receivabel
                GeneralLedger GLAR = new GeneralLedger(ddlExpense.SelectedItem.Text, explanation, writeoff_amount);
                GLAR.decreaseDebitAccount();
                UserUtility getUserName = new UserUtility();
                string FN = getUserName.BindUser();
                CustomerUtil updateStatus = new CustomerUtil(PID, PID2, writeoff_amount.ToString());
                updateStatus.DcreaseStatementWriteOff();
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    //selecting from Accounts Receivable
                    double newbalance = 0;
                    double bal = Convert.ToDouble(balance.InnerText);
                    double amount = Convert.ToDouble(txtWriteOffAmount.Text);
                    if (bal > amount)
                    {
                        newbalance += bal - amount;
                    }
                    else if (bal == amount)
                    {
                        newbalance += -amount;
                    }
                    SqlCommand cmd_creddit = new SqlCommand("Update tblcreditnote set balance='" + newbalance + "' where id='" + PID2 + "'", con);
                    cmd_creddit.ExecuteNonQuery();
                    string url = "creditnotedetails.aspx" + "?ref2=" + PID2 + "&&cust=" + PID;
                    string money = "ETB";
                    string text = money + amount.ToString("#,##0.00") + " Writte off for credit number #" + PID2;
                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + text + "','" + FN + "','" + FN + "','Unseen','fas fa-trash text-white','icon-circle bg bg-danger','" + url + "','MN')", con);
                    cmd197h.ExecuteNonQuery();
                    Response.Redirect("creditnotedetails.aspx" + "?ref2=" + PID2 + "&&cust=" + PID);
                }
            }
        }
    }
}