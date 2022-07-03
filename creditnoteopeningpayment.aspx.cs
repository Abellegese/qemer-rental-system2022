using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class creditnoteopeningpayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    bindqty();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindqty()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblcreditnote where customer='" + PID + "' and id='" + PID2 + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    string kc;
                    kc = reader["amount"].ToString();
                    txtqtyhand.Text = kc;
                }
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {

            String PID = Convert.ToString(Request.QueryString["cust"]);
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                //selecting from cash at bank
                SqlCommand cmd2 = new SqlCommand("select * from tblcreditnote where customer='" + PID + "' and id='" + PID2 + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    string kc;
                    kc = reader["amount"].ToString(); reader.Close();

                    double due = Convert.ToDouble(kc);
                    double paid = Convert.ToDouble(txtqtyhand.Text);
                    double unpaid = due - paid;
                    SqlCommand cmd45 = new SqlCommand("Update tblcreditnote set Balance='" + unpaid + "' where customer='" + PID + "' and id='" + PID2 + "'", con);
                    cmd45.ExecuteNonQuery();
                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash on Hand'", con);
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
                                Double bl1 = M1 + paid;
                                SqlCommand cmdcash = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash on Hand'", con);
                                cmdcash.ExecuteNonQuery();
                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + paid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Cash on Hand','','Cash')", con);
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
                                        double income = paid - 0.15 * paid;
                                        Double bl1 = income + M1;
                                        SqlCommand cmdinv = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                        cmdinv.ExecuteNonQuery();
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
                                        Double bl1 = M1 + paid * 0.15;
                                        SqlCommand cmdtx = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + paid * 0.15 + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                        cmdtx.ExecuteNonQuery();
                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','0','" + paid * 0.15 + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
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

                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + txtReference.Text + "','','" + paid + "','" + txtqtyhand.Text + "','" + ah11 + "','" + PID + "')", con);
                        custcmd.ExecuteNonQuery();
                        SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                        if (readerAC.Read())
                        {
                            String FN = readerAC["Name"].ToString();
                            readerAC.Close();
                            con.Close();
                            //Activity

                            string money = "ETB";
                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + money + "'+' '+'" + txtqtyhand.Text + "'+' '+'Deposited into bank account','" + FN + "','" + PID + "','Unseen','fas fa-donate text-white','icon-circle bg bg-success','rentstatus.aspx','MN')", con);
                            con.Open();
                            cmd197h.ExecuteNonQuery();


                            //Insert into cash receipt journal
                            double vatfree = paid - 0.15 * paid;
                            SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt", con);
                            SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                            DataTable dtdf = new DataTable();
                            sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                            SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + DateTime.Now.Date + "','" + vatfree + "','" + paid + "','" + ah11 + "','" + nb + "')", con);
                            cmdri.ExecuteNonQuery();
                            SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                            cmdAc.ExecuteNonQuery();
                            Response.Redirect("rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID);

                        }
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("rentstatus.aspx");
        }
    }
}