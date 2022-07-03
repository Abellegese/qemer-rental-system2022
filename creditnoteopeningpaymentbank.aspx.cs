using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class creditnoteopeningpaymentbank : System.Web.UI.Page
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
        protected void Button3_Click(object sender, EventArgs e)
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
                    kc = reader["amount"].ToString(); reader.Close();

                    double due = Convert.ToDouble(kc);
                    double paid = Convert.ToDouble(txtqtyhand.Text);
                    double unpaid = due - paid;
                    SqlCommand cmd45 = new SqlCommand("Update tblcreditnote set Balance='" + unpaid + "' where customer='" + PID + "' and id='" + PID2 + "'", con);
                    cmd45.ExecuteNonQuery();
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
                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + txtReference.Text + "','" + txtReference.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                cvb.ExecuteNonQuery();
                            }
                            else
                            {
                                double t = Convert.ToDouble(txtqtyhand.Text);
                                SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + txtReference.Text + "','" + txtReference.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                cvb1.ExecuteNonQuery();
                                SqlCommand b = new SqlCommand("insert into tblbanktrans1 values('" + txtReference.Text + "','" + txtReference.Text + "','" + txtqtyhand.Text + "','0','" + t + "','" + DropDownList1.SelectedItem.Text + "','','payment through bank','" + DateTime.Now.Date + "')", con);
                                b.ExecuteNonQuery();

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
                                        Double bl1 = M1 + paid;
                                        SqlCommand cmdcash = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                        cmdcash.ExecuteNonQuery();
                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + PID + "','','" + paid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Cash at Bank','','Cash')", con);
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
                                                SqlCommand cmdinc = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "', Credit='" + income + "', Debit='', Explanation='Credit Sales', Date='" + DateTime.Now.Date + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                                cmdinc.ExecuteNonQuery();
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
                                    SqlCommand cmdri = new SqlCommand("insert into tblrentreceipt values('" + PID + "','" + DateTime.Now.Date + "','" + vatfree + "','" + paid + "','" + ah11 + "')", con);
                                    cmdri.ExecuteNonQuery();
                                    SqlCommand cmdAc = new SqlCommand("insert into tblActivity values('" + DateTime.Now + "','Payment Received','Payment received from customer','" + PID + "','Payment received from'+' '+'<b>" + PID + "</b>'+' '+'Was Recorded','" + FN + "','rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID + "')", con);

                                    cmdAc.ExecuteNonQuery();
                                    Response.Redirect("rentinvoicereport.aspx?date=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) + "&&cust=" + PID);

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}