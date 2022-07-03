using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class bankpay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    bindbankaccount(); bindqty();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
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
                    DropDownList1.DataSource = dt;
                    DropDownList1.DataTextField = "AccountName";
                    DropDownList1.DataValueField = "AC";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
        }
        private void bindqty()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            if (Request.QueryString["pay"] != null)
            {
                string paym = Convert.ToString(Request.QueryString["pay"]);
                txtqtyhand.Text = Convert.ToDouble(paym).ToString("#,##.00");
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string kc;
                        kc = reader["currentperiodue"].ToString();
                        txtqtyhand.Text = Convert.ToDouble(kc).ToString("#,##0.00");
                    }
                }
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (txtqtyhand.Text == "" || txtVoucher.Text == "" || DropDownList1.SelectedItem.Text == "-Select-")
            {
                lblMsg.Text = "Fill the required Input"; lblMsg.Visible = true; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmdset = new SqlCommand("select * from tblsetting", con);
                    SqlDataReader readerset = cmdset.ExecuteReader();
                    if (readerset.Read())
                    {
                        string set = readerset["climit"].ToString();
                        readerset.Close();
                        if (set == "Yes")
                        {
                            SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                            SqlDataReader readercrd = cmdcrd.ExecuteReader();
                            if (readercrd.Read())
                            {

                                string limit = readercrd["CreditLimit"].ToString();
                                readercrd.Close();
                                SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                                SqlDataReader reader = cmd2.ExecuteReader();

                                if (reader.Read())
                                {
                                    string servicecharge; servicecharge = reader["servicecharge"].ToString();
                                    string kc; string duedates = reader["duedate"].ToString();
                                    kc = reader["currentperiodue"].ToString();
                                    double totalpay = Convert.ToDouble(kc);
                                    reader.Close();
                                    double balance = Convert.ToDouble(txtqtyhand.Text) - Convert.ToDouble(kc);
                                    double due = Convert.ToDouble(txtqtyhand.Text);
                                    double climit = Convert.ToDouble(limit);
                                    if (-balance > climit)
                                    {
                                        double ex = -balance;
                                        lblMsg.Text = "Credit Limit Exceeded By " + ex.ToString("#,##0.00"); lblMsg.ForeColor = Color.Red;
                                        lblMsg.Visible = true; infoicon.Visible = true;
                                    }
                                    else
                                    {
                                        if (balance == 0)
                                        {

                                            SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                            SqlDataReader readerbank = cmdbank.ExecuteReader();

                                            if (readerbank.Read())
                                            {
                                                string bankno;
                                                bankno = readerbank["AccountNumber"].ToString();
                                                readerbank.Close();
                                                SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                                {
                                                    string totalannounc = PID + " Paid through bank";
                                                    DataTable dt1 = new DataTable();
                                                    sda221.Fill(dt1); int j = dt1.Rows.Count;
                                                    //
                                                    if (j != 0)
                                                    {
                                                        double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                        SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                        cmdday.ExecuteNonQuery();
                                                        SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        cvb.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        double t = Convert.ToDouble(txtqtyhand.Text);
                                                        SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        cvb1.ExecuteNonQuery();
                                                        SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                        b.ExecuteNonQuery();

                                                    }
                                                }
                                                //selecting from cash at bank
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
                                                            double deb = Convert.ToDouble(txtqtyhand.Text);
                                                            Double M1 = Convert.ToDouble(ah12893);
                                                            Double bl1 = M1 + deb;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Cash at Bank','','Cash')", con);
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
                                                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
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
                                                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

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
                                                                    double income = totalpay / 1.15;

                                                                    Double bl1 = income + M1;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                                    double vatfree = totalpay / 1.15;
                                                                    double income = totalpay - vatfree;
                                                                    Double bl1 = M1 + income;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                    cmd1974.ExecuteNonQuery();

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //Inserting to customer statement
                                                SqlCommand cmdreadb = new SqlCommand("select TOP 1* from tblCustomerStatement where Customer='" + PID + "'  ORDER BY CSID DESC", con);

                                                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                if (readerbcustb.Read())
                                                {
                                                    string ah11;

                                                    ah11 = readerbcustb["Balance"].ToString();
                                                    readerbcustb.Close();

                                                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtVoucher.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + ah11 + "','" + PID + "')", con);
                                                    custcmd.ExecuteNonQuery();
                                                    SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                    if (readerAC.Read())
                                                    {
                                                        String FN = readerAC["Name"].ToString();
                                                        readerAC.Close();
                                                        con.Close();
                                                        //Activity
                                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                        con.Open();
                                                        cmdAc.ExecuteNonQuery();
                                                        string money = "ETB";
                                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','MN')", con);
                                                        cmd197h.ExecuteNonQuery();
                                                        SqlCommand cmd197h2 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','FH')", con);
                                                        cmd197h2.ExecuteNonQuery();
                                                        //Updating the Due Date
                                                        SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                        SqlDataReader readerup = cmdup.ExecuteReader();

                                                        if (readerup.Read())
                                                        {
                                                            String terms = readerup["PaymentDuePeriod"].ToString();
                                                            readerup.Close();
                                                            if (terms == "Monthly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(30);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Three Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(90);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(365);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                        }
                                                    }
                                                }
                                                if (Request.QueryString["bill"] != null)
                                                {
                                                    SqlCommand cmdupbill = new SqlCommand("Update tblcustomerbill set status='Billed' where customer='" + PID + "'", con);
                                                    cmdupbill.ExecuteNonQuery();
                                                    SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Your bill request has been approved','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                                                    cmd197hb.ExecuteNonQuery();
                                                }
                                            }
                                            hidediv.Visible = false;
                                            lblMsg.Text = "You have successfully recorded the bill. See the statment in bank module."; lblMsg.ForeColor = Color.Green;
                                            lblMsg.Visible = true; infoicon.Visible = true;
                                        }
                                        else if (balance < 0)
                                        {
                                            double newclimit = climit + balance;
                                            SqlCommand cmdclim = new SqlCommand("Update tblCustomers set CreditLimit='" + newclimit + "' where FllName='" + PID + "'", con);
                                            cmdclim.ExecuteNonQuery();
                                            SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                            SqlDataReader readerbank = cmdbank.ExecuteReader();

                                            if (readerbank.Read())
                                            {
                                                string bankno;
                                                bankno = readerbank["AccountNumber"].ToString();
                                                readerbank.Close();
                                                SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                                {
                                                    DataTable dt1 = new DataTable();
                                                    sda221.Fill(dt1); int j = dt1.Rows.Count;
                                                    //
                                                    if (j != 0)
                                                    {
                                                        double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                        SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                        cmdday.ExecuteNonQuery();
                                                        SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                                        cvb.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        double t = Convert.ToDouble(txtqtyhand.Text);
                                                        SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                                        cvb1.ExecuteNonQuery();
                                                        SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                                        b.ExecuteNonQuery();

                                                    }
                                                }
                                                //selecting from Accounts Receivable
                                                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
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
                                                            double unpaid = -balance;
                                                            Double bl1 = M1 + unpaid; ;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + unpaid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                            cmd1974.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                SqlCommand cmdacr = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash at Bank'", con);
                                                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmdacr))
                                                {
                                                    DataTable dtBrands2322 = new DataTable();
                                                    sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
                                                    //
                                                    if (i2 != 0)
                                                    {
                                                        SqlDataReader reader6679034 = cmdacr.ExecuteReader();

                                                        if (reader6679034.Read())
                                                        {
                                                            string ah12893;
                                                            ah12893 = reader6679034["Balance"].ToString();
                                                            reader6679034.Close();
                                                            con.Close();
                                                            con.Open();
                                                            Double M1 = Convert.ToDouble(ah12893);
                                                            Double bl1 = M1 + Convert.ToDouble(txtqtyhand.Text); ;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + txtqtyhand.Text + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Cash at Bank','','Cash')", con);
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
                                                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
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
                                                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

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
                                                                    double income = totalpay / 1.15;

                                                                    Double bl1 = income + M1;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                                    double vatfree = totalpay / 1.15;
                                                                    double income = totalpay - vatfree;
                                                                    Double bl1 = M1 + income;
                                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                                    cmd45.ExecuteNonQuery();
                                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                                    cmd1974.ExecuteNonQuery();

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                //Inserting to customer statement
                                                SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                                if (readerbcustb.Read())
                                                {
                                                    string ah11;

                                                    ah11 = readerbcustb["Balance"].ToString();
                                                    readerbcustb.Close();
                                                    double payment = Convert.ToDouble(txtqtyhand.Text);
                                                    double credit = due - payment;
                                                    double balancedue = Convert.ToDouble(ah11);
                                                    double unpaid = -balance;
                                                    double remain = balancedue + unpaid;
                                                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtVoucher.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + remain + "','" + PID + "')", con);
                                                    custcmd.ExecuteNonQuery();
                                                    SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                                    if (readerAC.Read())
                                                    {
                                                        String FN = readerAC["Name"].ToString();
                                                        readerAC.Close();
                                                        con.Close();
                                                        //Activity
                                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                        con.Open();
                                                        cmdAc.ExecuteNonQuery();
                                                        string money = "ETB";
                                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','MN')", con);
                                                        cmd197h.ExecuteNonQuery();
                                                        SqlCommand cmd197h3 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','FH')", con);
                                                        cmd197h3.ExecuteNonQuery();
                                                        //Updating the Due Date
                                                        SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                        SqlDataReader readerup = cmdup.ExecuteReader();

                                                        if (readerup.Read())
                                                        {
                                                            String terms = readerup["PaymentDuePeriod"].ToString();
                                                            readerup.Close();
                                                            if (terms == "Monthly")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(30);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else if (terms == "Every Three Month")
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(90);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            else
                                                            {
                                                                DateTime duedate = Convert.ToDateTime(duedates);
                                                                DateTime newduedate = duedate.AddDays(365);
                                                                SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                                cmdrent.ExecuteNonQuery();
                                                            }
                                                            SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + due + "','" + -balance + "','Credit for rent','" + txtDuedate.Text + "')", con);
                                                            cmdcrn.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                if (Request.QueryString["bill"] != null)
                                                {
                                                    SqlCommand cmdupbill = new SqlCommand("Update tblcustomerbill set status='Billed' where customer='" + PID + "'", con);
                                                    cmdupbill.ExecuteNonQuery();
                                                    SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Your bill request has been approved','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                                                    cmd197hb.ExecuteNonQuery();
                                                }
                                            }
                                            hidediv.Visible = false;
                                            lblMsg.Text = "You have successfully recorded the bill. See the statment in bank module."; lblMsg.ForeColor = Color.Green;
                                            lblMsg.Visible = true; infoicon.Visible = true;
                                        }
                                        else
                                        {
                                            lblMsg.Visible = true; lblMsg.ForeColor = Color.Red;
                                            lblMsg.Text = "Error Amount";
                                        }
                                    }
                                }

                            }

                        }
                        else
                        {
                            SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                            SqlDataReader reader = cmd2.ExecuteReader();

                            if (reader.Read())
                            {
                                string servicecharge; servicecharge = reader["servicecharge"].ToString();
                                string kc; string duedates = reader["duedate"].ToString();
                                kc = reader["currentperiodue"].ToString();
                                reader.Close();
                                double totalpay = Convert.ToDouble(kc);
                                double balance = Convert.ToDouble(txtqtyhand.Text) - Convert.ToDouble(kc);
                                double due = Convert.ToDouble(txtqtyhand.Text);
                                if (balance == 0)
                                {

                                    SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                    SqlDataReader readerbank = cmdbank.ExecuteReader();

                                    if (readerbank.Read())
                                    {
                                        string bankno;
                                        bankno = readerbank["AccountNumber"].ToString();
                                        readerbank.Close();
                                        SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                        using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                        {
                                            DataTable dt1 = new DataTable();
                                            sda221.Fill(dt1); int j = dt1.Rows.Count;
                                            //
                                            string totalannounc = PID + " Paid through bank";
                                            if (j != 0)
                                            {
                                                double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                cmdday.ExecuteNonQuery();
                                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                cvb.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                double t = Convert.ToDouble(txtqtyhand.Text);
                                                SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                cvb1.ExecuteNonQuery();
                                                SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                                                b.ExecuteNonQuery();

                                            }
                                        }
                                        //selecting from cash at bank
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
                                                    double deb = Convert.ToDouble(txtqtyhand.Text);
                                                    Double M1 = Convert.ToDouble(ah12893);
                                                    Double bl1 = M1 + deb;
                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                                    cmd45.ExecuteNonQuery();
                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Cash at Bank','','Cash')", con);
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
                                            SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
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
                                                        SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

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
                                                            double income = totalpay / 1.15;

                                                            Double bl1 = income + M1;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                            double vatfree = totalpay / 1.15;
                                                            double income = totalpay - vatfree;
                                                            Double bl1 = M1 + income;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                            cmd1974.ExecuteNonQuery();

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //Inserting to customer statement
                                        SqlCommand cmdreadb = new SqlCommand("select * from tblCustomerStatement where Customer='" + PID + "'  ORDER BY CSID DESC", con);

                                        SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                        if (readerbcustb.Read())
                                        {
                                            string ah11;

                                            ah11 = readerbcustb["Balance"].ToString();
                                            readerbcustb.Close();

                                            SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtVoucher.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + ah11 + "','" + PID + "')", con);
                                            custcmd.ExecuteNonQuery();
                                            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                            SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                            if (readerAC.Read())
                                            {
                                                String FN = readerAC["Name"].ToString();
                                                readerAC.Close();
                                                con.Close();
                                                //Activity
                                                SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                con.Open();
                                                cmdAc.ExecuteNonQuery();
                                                string money = "ETB";
                                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','MN')", con);
                                                cmd197h.ExecuteNonQuery();
                                                SqlCommand cmd197h2 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(txtqtyhand.Text).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','FH')", con);
                                                cmd197h2.ExecuteNonQuery();
                                                //Updating the Due Date
                                                SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                SqlDataReader readerup = cmdup.ExecuteReader();

                                                if (readerup.Read())
                                                {
                                                    String terms = readerup["PaymentDuePeriod"].ToString();
                                                    readerup.Close();
                                                    if (terms == "Monthly")
                                                    {
                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                        DateTime newduedate = duedate.AddDays(30);
                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                        cmdrent.ExecuteNonQuery();
                                                    }
                                                    else if (terms == "Every Three Month")
                                                    {
                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                        DateTime newduedate = duedate.AddDays(90);
                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                        cmdrent.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                        DateTime newduedate = duedate.AddDays(365);
                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                        cmdrent.ExecuteNonQuery();
                                                    }

                                                    if (Request.QueryString["bill"] != null)
                                                    {
                                                        SqlCommand cmdupbill = new SqlCommand("Update tblcustomerbill set status='Billed' where customer='" + PID + "'", con);
                                                        cmdupbill.ExecuteNonQuery();
                                                        SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Your bill request has been approved','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                                                        cmd197hb.ExecuteNonQuery();
                                                    }
                                                    hidediv.Visible = false;
                                                    lblMsg.Text = "You have successfully recorded the bill. See the statment in bank module."; lblMsg.ForeColor = Color.Green;
                                                    lblMsg.Visible = true; infoicon.Visible = true;
                                                }
                                            }
                                        }
                                    }

                                }
                                else if (balance < 0)
                                {
                                    SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + DropDownList1.SelectedItem.Text + "' ", con);
                                    SqlDataReader readerbank = cmdbank.ExecuteReader();

                                    if (readerbank.Read())
                                    {
                                        string bankno;
                                        bankno = readerbank["AccountNumber"].ToString();
                                        readerbank.Close();
                                        SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                        using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                                        {
                                            DataTable dt1 = new DataTable();
                                            sda221.Fill(dt1); int j = dt1.Rows.Count;
                                            //
                                            if (j != 0)
                                            {
                                                double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(txtqtyhand.Text);
                                                SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + DropDownList1.SelectedItem.Text + "'", con);
                                                cmdday.ExecuteNonQuery();
                                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                                cvb.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                double t = Convert.ToDouble(txtqtyhand.Text);
                                                SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                                cvb1.ExecuteNonQuery();
                                                SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtVoucher.Text + "','" + txtVoucher.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                                b.ExecuteNonQuery();

                                            }
                                        }
                                        //selecting from Accounts Receivable
                                        SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
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
                                                    double unpaid = -balance;
                                                    Double bl1 = M1 + unpaid; ;
                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                    cmd45.ExecuteNonQuery();
                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + unpaid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                    cmd1974.ExecuteNonQuery();
                                                }
                                            }
                                        }
                                        SqlCommand cmdacr = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash at Bank'", con);
                                        using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmdacr))
                                        {
                                            DataTable dtBrands2322 = new DataTable();
                                            sda2222.Fill(dtBrands2322); int i2 = dtBrands2322.Rows.Count;
                                            //
                                            if (i2 != 0)
                                            {
                                                SqlDataReader reader6679034 = cmdacr.ExecuteReader();

                                                if (reader6679034.Read())
                                                {
                                                    string ah12893;
                                                    ah12893 = reader6679034["Balance"].ToString();
                                                    reader6679034.Close();
                                                    con.Close();
                                                    con.Open();
                                                    Double M1 = Convert.ToDouble(ah12893);
                                                    Double bl1 = M1 + Convert.ToDouble(txtqtyhand.Text); ;
                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                                    cmd45.ExecuteNonQuery();
                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + txtqtyhand.Text + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Cash at Bank','','Cash')", con);
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
                                            SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
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
                                                        SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

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
                                                            double income = Convert.ToDouble(txtqtyhand.Text) / 1.15;

                                                            Double bl1 = income + M1;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                                            double vatfree = Convert.ToDouble(txtqtyhand.Text) / 1.15;
                                                            double income = Convert.ToDouble(txtqtyhand.Text) - vatfree;
                                                            Double bl1 = M1 + income;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                            cmd1974.ExecuteNonQuery();

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //Inserting to customer statement
                                        SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                                        SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                                        if (readerbcustb.Read())
                                        {
                                            string ah11;

                                            ah11 = readerbcustb["Balance"].ToString();
                                            readerbcustb.Close();
                                            double payment = Convert.ToDouble(txtqtyhand.Text);
                                            double credit = due - payment;
                                            double balancedue = Convert.ToDouble(ah11);
                                            double unpaid = -balance;
                                            double remain = balancedue + unpaid;
                                            SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtVoucher.Text + "','','" + totalpay + "','" + txtqtyhand.Text + "','" + remain + "','" + PID + "')", con);
                                            custcmd.ExecuteNonQuery();
                                            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                            SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                            if (readerAC.Read())
                                            {
                                                String FN = readerAC["Name"].ToString();
                                                readerAC.Close();
                                                con.Close();
                                                //Activity
                                                SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                con.Open();
                                                cmdAc.ExecuteNonQuery();
                                                string money = "ETB";
                                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','MN')", con);
                                                cmd197h.ExecuteNonQuery();
                                                SqlCommand cmd197h3 = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus1.aspx','FH')", con);
                                                cmd197h3.ExecuteNonQuery();

                                                //Updating the Due Date
                                                SqlCommand cmdup = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                                                SqlDataReader readerup = cmdup.ExecuteReader();

                                                if (readerup.Read())
                                                {
                                                    String terms = readerup["PaymentDuePeriod"].ToString();
                                                    readerup.Close();
                                                    if (terms == "Monthly")
                                                    {
                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                        DateTime newduedate = duedate.AddDays(30);
                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                        cmdrent.ExecuteNonQuery();
                                                    }
                                                    else if (terms == "Every Three Month")
                                                    {
                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                        DateTime newduedate = duedate.AddDays(90);
                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                        cmdrent.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {
                                                        DateTime duedate = Convert.ToDateTime(duedates);
                                                        DateTime newduedate = duedate.AddDays(365);
                                                        SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + PID + "'", con);
                                                        cmdrent.ExecuteNonQuery();
                                                    }
                                                    SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + due + "','" + -balance + "','Credit for rent','" + txtDuedate.Text + "')", con);
                                                    cmdcrn.ExecuteNonQuery();
                                                    double vatfree = due - 0.15 * due;

                                                    if (Request.QueryString["bill"] != null)
                                                    {
                                                        SqlCommand cmdupbill = new SqlCommand("Update tblcustomerbill set status='Billed' where customer='" + PID + "'", con);
                                                        cmdupbill.ExecuteNonQuery();
                                                        SqlCommand cmd197hb = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','Your bill request has been approved','" + PID + "','" + PID + "','Unseen','fas fa-calendar text-white','icon-circle bg bg-success','','CUST')", con);
                                                        cmd197hb.ExecuteNonQuery();
                                                    }
                                                    hidediv.Visible = false;
                                                    lblMsg.Text = "You have successfully recorded the bill. See the statment in bank module."; lblMsg.ForeColor = Color.Green;
                                                    lblMsg.Visible = true; infoicon.Visible = true;
                                                }
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    lblMsg.Visible = true; lblMsg.ForeColor = Color.Red;
                                    lblMsg.Text = "Error Amount";
                                }
                            }
                        }
                    }

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("rentstatus1.aspx");
        }
    }
}

