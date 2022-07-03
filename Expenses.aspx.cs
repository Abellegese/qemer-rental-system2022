using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{

    public partial class Expenses : System.Web.UI.Page
    {
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'Expenses.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'Expenses.ds' is never used
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Column"] = "name";
                    ViewState["Column1"] = "amount";
                    ViewState["Column2"] = "date";
                    ViewState["Sortorder"] = "ASC";
                    BindBrandsRptr2(); bindfixedaccount1(); bindbankaccount();
                    bindcompany(); bindfixedaccount(); bindcashaccount();
                    binddetails(); bindattachment(); downloadfile();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
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
                    string company; string address;
                    company = reader["Oname"].ToString();
                    address = reader["BuissnessLocation"].ToString();
                    addressname.InnerText = address;
                    oname.InnerText = company;
                }
            }
        }
        private void bindfixedaccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblManageExpense", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlExpense.DataSource = dt;
                    ddlExpense.DataTextField = "name";
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
                SqlCommand cmd = new SqlCommand("select * from tblVendor", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlVendor.DataSource = dt;
                    ddlVendor.DataTextField = "FllName";
                    ddlVendor.DataValueField = "CID";
                    ddlVendor.DataBind();
                    ddlVendor.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
        private void binddetails()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String PID2 = Convert.ToString(Request.QueryString["expname"]);
            ExpenseType.InnerText = PID2;
            if (Request.QueryString["ref2"] != null)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2AC = new SqlCommand("select * from tblExpense where name='" + PID2 + "' and id='" + PID + "'", con);
                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                    if (readerAC.Read())
                    {
                        String vendor = readerAC["vendor"].ToString();
                        String amount = readerAC["amount"].ToString();
                        String date = readerAC["date"].ToString();
                        id.InnerText = PID;
                        amount1.InnerText = Convert.ToDouble(amount).ToString("#,##0.00");
                        date1.InnerText = Convert.ToDateTime(date).ToString("MMMM dd, yyyy");
                        BillDate1.InnerText = Convert.ToDateTime(date).ToString("MMMM dd, yyyy");
                        readerAC.Close();
                        SqlCommand cmd = new SqlCommand("select * from tblVendor where FllName='" + vendor + "'", con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            String address = reader["BillingAddress"].ToString();
                            vendor1.InnerText = vendor;
                            addressvendor.InnerText = address;
                        }
                    }
                }
            }
        }
        private void BindBrandsRptr2()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String PID2 = Convert.ToString(Request.QueryString["expname"]);
            if (Request.QueryString["ref2"] != null)
            {
                leaveempt.Visible = false;
                showdetail.Visible = true;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();

                str = "select * from tblExpense";
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

                str = "select * from tblExpense";
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
        private void downloadfile()
        {
            if (Request.QueryString["download"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select*from tblExpenseAttachment where exid='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; string ext = reader["Extension"].ToString(); kc = reader["FileName"].ToString(); reader.Close();
                        string SavePath = Server.MapPath("~/asset/expattachment/" + kc + ext);
                        Response.ContentType = ContentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + kc + ext);
                        Response.WriteFile(SavePath);
                        Response.End();
                    }
                }
            }

        }
        private void bindattachment()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String PID2 = Convert.ToString(Request.QueryString["expname"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select*from tblExpenseAttachment where exid='" + PID + "'", con);

                    SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd2);
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                    if (i2c3 != 0)
                    {
                        SqlDataReader reader = cmd2.ExecuteReader();
                        if (reader.Read())
                        {
                            string kc; kc = reader["FileName"].ToString(); reader.Close();

                            attachname.InnerText = kc;
                            attachlink.HRef = "Expenses.aspx" + "?ref2=" + PID + "&&download=true&&expname=" + PID2;
                        }
                    }
                    else
                    {
                        attachmnetdiv.Visible = false;
                    }

                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlExpense.Items.Count == 0)
            {
                lblMsg.Text = "No expense Name was added."; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                if (ddlExpense.SelectedItem.Text == "-Select-" || ddlVendor.SelectedItem.Text == "-Select-" || txtAmount.Text == "" || txtReference.Text == "" || ddlCash.SelectedItem.Text == "-Select-" && ddlBank.SelectedItem.Text == "-Select-" || ddlCash.SelectedItem.Text != "-Select-" && ddlBank.SelectedItem.Text != "-Select-")
                {
                    lblMsg.Text = "Please fill all the required input ( Or you selected both bank and cash account"; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmd2AC = new SqlCommand("select * from tblManageExpense where name='" + ddlExpense.SelectedItem.Text + "'", con);
                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                        if (readerAC.Read())
                        {
                            String account = readerAC["account"].ToString();
                            readerAC.Close();

                            SqlCommand cmdin = new SqlCommand("insert into tblExpense values('" + ddlExpense.SelectedItem.Text + "','" + account + "','" + txtAmount.Text + "','" + txtReference.Text + "','" + ddlVendor.SelectedItem.Text + "','" + DateTime.Now.Date + "')", con);
                            cmdin.ExecuteNonQuery();

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
                                        Double bl1c = M1c - Convert.ToDouble(txtAmount.Text);
                                        string total = "Cash credited for " + ddlExpense.SelectedItem.Text + " Expense";
                                        SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "' where Account='" + ddlCash.SelectedItem.Text + "'", con);
                                        cmd45c.ExecuteNonQuery();
                                        SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','0','" + txtAmount.Text + "','" + bl1c + "','" + DateTime.Now.Date + "','" + ddlCash.SelectedItem.Text + "','" + ah11c + "','" + ah1258c + "')", con);
                                        cmd1964c.ExecuteNonQuery();
                                    }
                                }

                                //Expense
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
                                        Double bl1c = M1c + Convert.ToDouble(txtAmount.Text);
                                        string total = "Expense Debited for " + ddlExpense.SelectedItem.Text + " Expense";
                                        SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "' where Account='" + account + "'", con);
                                        cmd45c.ExecuteNonQuery();
                                        SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','" + txtAmount.Text + "','0','" + bl1c + "','" + DateTime.Now.Date + "','" + account + "','" + ah11c + "','" + ah1258c + "')", con);
                                        cmd1964c.ExecuteNonQuery();
                                    }
                                }
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
                                        double t = Convert.ToDouble(dt.Rows[0][5].ToString()) - Convert.ToDouble(txtAmount.Text);
                                        SqlCommand cmd45 = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where Account='" + ddlBank.SelectedItem.Text + "'", con);
                                        cmd45.ExecuteNonQuery();
                                        SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('','" + txtReference.Text + "','','" + txtAmount.Text + "','" + t + "','" + ddlBank.SelectedItem.Text + "','','','" + DateTime.Now.Date + "')", con);
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
                                                Double bl1c = M1c - Convert.ToDouble(txtAmount.Text);
                                                string total1 = "Cash at Bank credited for " + ddlExpense.SelectedItem.Text + " Expense";
                                                SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "' where Account='Cash at Bank'", con);
                                                cmd45c.ExecuteNonQuery();
                                                SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total1 + "','','0','" + txtAmount.Text + "','" + bl1c + "','" + DateTime.Now.Date + "','Cash at Bank','" + ah11c + "','" + ah1258c + "')", con);
                                                cmd1964c.ExecuteNonQuery();
                                            }
                                        }

                                        ///Recordeing EXpense
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
                                                Double bl1c = M1c + Convert.ToDouble(txtAmount.Text);

                                                SqlCommand cmd45c = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1c + "' where Account='" + account + "'", con);
                                                cmd45c.ExecuteNonQuery();
                                                SqlCommand cmd1964c = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','" + txtAmount.Text + "','0','" + bl1c + "','" + DateTime.Now.Date + "','" + account + "','" + ah11c + "','" + ah1258c + "')", con);
                                                cmd1964c.ExecuteNonQuery();
                                            }
                                        }
                                    }

                                }
                            }
                            if (FileUpload1.HasFile)
                            {
                                SqlCommand cmddf = new SqlCommand("select * from tblExpense", con);
                                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                DataTable dtdf = new DataTable();
                                sdadf.Fill(dtdf); int nb = dtdf.Rows.Count;
                                string SavePath = Server.MapPath("~/asset/expattachment/");
                                if (!Directory.Exists(SavePath))
                                {
                                    Directory.CreateDirectory(SavePath);
                                }
                                string Extention = Path.GetExtension(FileUpload1.PostedFile.FileName);
                                FileUpload1.SaveAs(SavePath + "\\" + FileUpload1.FileName + Extention);
                                SqlCommand cmdinr = new SqlCommand("insert into tblExpenseAttachment values('" + nb + "','" + FileUpload1.FileName + "','" + Extention + "')", con);

                                cmdinr.ExecuteNonQuery();
                            }
                        }
                        Response.Redirect("Expenses.aspx");
                    }
                }
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblExpense where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            DataView dvData = new DataView(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btnAmountCondition_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                str = "select * from tblExpense where amount > '" + txtFilteredAmount.Text + "'";
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
                str = "select * from tblExpense where amount < '" + txtFilteredAmount.Text + "'";
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
                str = "select * from tblExpense where amount = '" + txtFilteredAmount.Text + "'";
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
}