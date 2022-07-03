using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class markasreceivable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCreditNumber(); BindBrandsRptr4();
            BindWriteOff();
        }
        private void BindWriteOff()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();

                DateTime daysLeft = DateTime.Now.Date;

                SqlCommand cmd16 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') >= 260", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd16))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                    if (i != 0)
                    {
                        SqlDataReader reader6 = cmd16.ExecuteReader();
                        if (reader6.Read())
                        {
                            string ah;
                            ah = reader6["Balance"].ToString();
                            if (ah == "")
                            {
                                TotalWriteOff.InnerText = "0.00";
                            }
                            else
                            {
                                TotalWriteOff.InnerText = Convert.ToDouble(ah).ToString("#,##0.00");
                            }
                            reader6.Close();
                            con.Close();
                        }
                    }
                }
            }
        }
        protected void BindBrandsRptr4()
        {
            if (Request.QueryString["cust"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where customer='" + PID + "'", con);

                    using (SqlDataAdapter sd = new SqlDataAdapter(cmd2))
                    {
                        DataTable dt = new DataTable();
                        sd.Fill(dt); int i2c = dt.Rows.Count;
                        SqlDataReader reader = cmd2.ExecuteReader();
                        if (i2c != 0)
                        {

                            if (reader.Read())
                            {
                                string kc;

                                kc = reader["Balance"].ToString();
                                if (kc == "" || kc == null)
                                {
                                    Span9.InnerText = "0.00";
                                }
                                else
                                {
                                    Span9.InnerText = "ETB " + Convert.ToDouble(kc).ToString("#,##0.00");

                                }

                                reader.Close();
                                con.Close();
                            }
                        }
                        else
                        {
                            Span9.InnerText = "No Transaction";
                        }
                    }
                }
            }
        }
        private void BindCreditNumber()
        {
            if (Request.QueryString["cust"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select* from tblcreditnote where customer='" + PID + "' and balance>0", con);

                    using (SqlDataAdapter sd = new SqlDataAdapter(cmd2))
                    {
                        DataTable dt = new DataTable();
                        sd.Fill(dt); int i2c = dt.Rows.Count;
                        SqlDataReader reader = cmd2.ExecuteReader();
                        if (i2c != 0)
                        {
                            NumberofCredit.InnerText = i2c.ToString(); ;
                        }
                        else
                        {
                            NumberofCredit.InnerText = "0";
                        }
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
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
                                string kc; string servicecharge; string duedates = reader["duedate"].ToString();
                                servicecharge = reader["servicecharge"].ToString();
                                kc = reader["currentperiodue"].ToString();
                                double totalpay = Convert.ToDouble(kc);
                                reader.Close();

                                double due = Convert.ToDouble(kc);


                                double climit = Convert.ToDouble(limit);
                                if (due > climit)
                                {
                                    double d = due - climit;

                                }
                                else
                                {

                                    {

                                        double newclimit = climit - due;
                                        SqlCommand cmdclim = new SqlCommand("Update tblCustomers set CreditLimit='" + newclimit + "' where FllName='" + PID + "'", con);
                                        cmdclim.ExecuteNonQuery();
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

                                                    Double bl1 = M1 + due; ;
                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                    cmd45.ExecuteNonQuery();
                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + due + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
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
                                                            double income = totalpay - 0.15 * totalpay;
                                                            Double bl1 = income + M1;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                            cmd1974.ExecuteNonQuery();

                                                        }
                                                    }
                                                }
                                            }
                                            //Selecting from tax acccount
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
                                                            Double bl1 = M1 + totalpay * 0.15;
                                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + totalpay * 0.15 + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                            cmd45.ExecuteNonQuery();
                                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + totalpay * 0.15 + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                            double payment = Convert.ToDouble(due);
                                            double balancedue = Convert.ToDouble(ah11);
                                            double remain = balancedue + payment;
                                            SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','Credit issued','','" + due + "','0','" + remain + "','" + PID + "')", con);
                                            custcmd.ExecuteNonQuery();
                                            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                            SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                            if (readerAC.Read())
                                            {
                                                String FN = readerAC["Name"].ToString();
                                                readerAC.Close();
                                                con.Close();
                                                //Activity
                                                SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Credit Issued','Credit Issued for','" + PID + "','Credit Issued for'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                                con.Open();
                                                cmdAc.ExecuteNonQuery();
                                                string money = "ETB";
                                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Issued as a Credit','" + FN + "','" + PID + "','Unseen','fas fa-dollar-sign text-white','icon-circle bg bg-warning','rentstatus1.aspx','MN')", con);
                                                cmd197h.ExecuteNonQuery();

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
                                                    SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + due + "','" + due + "','Credit for rent','" + DateTime.Now + "')", con);
                                                    cmdcrn.ExecuteNonQuery();
                                                }
                                            }
                                        }
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

                            double due = Convert.ToDouble(kc);

                            {
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

                                            Double bl1 = M1 + due; ;
                                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                            cmd45.ExecuteNonQuery();
                                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + due + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
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
                                                    double income = due - 0.15 * due;
                                                    Double bl1 = income + M1;
                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                    cmd45.ExecuteNonQuery();
                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                                    cmd1974.ExecuteNonQuery();

                                                }
                                            }
                                        }
                                    }
                                    //Selecting from tax acccount
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
                                                    Double bl1 = M1 + due * 0.15;
                                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + due * 0.15 + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                                    cmd45.ExecuteNonQuery();
                                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + due * 0.15 + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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
                                    double payment = Convert.ToDouble(due);
                                    double balancedue = Convert.ToDouble(ah11);
                                    double remain = balancedue + payment;
                                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','Credit issued','','" + due + "','0','" + remain + "','" + PID + "')", con);
                                    custcmd.ExecuteNonQuery();
                                    SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                    if (readerAC.Read())
                                    {
                                        String FN = readerAC["Name"].ToString();
                                        readerAC.Close();
                                        con.Close();
                                        //Activity
                                        SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Credit Issued','Credit Issued for','" + PID + "','Credit Issued for'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentstatus1.aspx')", con);
                                        con.Open();
                                        cmdAc.ExecuteNonQuery();
                                        string money = "ETB";
                                        SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + Convert.ToDouble(payment).ToString("#,##0.00") + "'+' '+'Issued as a Credit','" + FN + "','" + PID + "','Unseen','fas fa-dollar-sign text-white','icon-circle bg bg-warning','rentstatus1.aspx','MN')", con);
                                        cmd197h.ExecuteNonQuery();

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
                                            SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + due + "','" + due + "','Credit for rent','" + DateTime.Now + "')", con);
                                            cmdcrn.ExecuteNonQuery();
                                        }
                                    }
                                }

                            }
                        }


                    }
                    Response.Redirect("CustomerDetails.aspx?ref2=" + PID);
                }
            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("rentstatus.aspx");
        }
    }
}