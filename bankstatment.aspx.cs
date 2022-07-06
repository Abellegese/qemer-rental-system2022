using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class bankstatment : System.Web.UI.Page
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
                    ViewState["Column"] = "date";

                    ViewState["Sortorder"] = "DESC";
                    datTo.Visible = false; datFrom1.Visible = false; tomiddle.Visible = false;
                    String PID = Convert.ToString(Request.QueryString["ref2"]);
                    Span2.InnerText = PID;
                    bindbankname(); bindbalance();
                    BindBrandsRptr2(); bindcompany(); bindapprover();
                    mont.InnerText = "As of " + DateTime.Now.ToString("MMMM dd, yyyy");
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        protected void Button14_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                lblMsg.Text = "Please enter your amount"; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmdbank = new SqlCommand("select * from tblbanktrans1 where account='" + PID + "'", con);
                    using (SqlDataAdapter sda22 = new SqlDataAdapter(cmdbank))
                    {
                        DataTable dt = new DataTable();
                        sda22.Fill(dt); int j = dt.Rows.Count;
                        //
                        double adjusted = Convert.ToDouble(txtAmount.Text);
                        if (j != 0)
                        {
                            if (adjusted < 0)
                            {
                                double t = Convert.ToDouble(dt.Rows[0][5].ToString()) + adjusted;
                                SqlCommand cmd45 = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + PID + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('-','-','0','" + -adjusted + "','" + t + "','" + PID + "','','" + txtRemark.Text + "','" + DateTime.Now.Date + "')", con);
                                cvb.ExecuteNonQuery();
                            }
                            else
                            {
                                double t = Convert.ToDouble(dt.Rows[0][5].ToString()) + adjusted;
                                SqlCommand cmd45 = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + PID + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('-','-','" + adjusted + "','0','" + t + "','" + PID + "','','" + txtRemark.Text + "','" + DateTime.Now.Date + "')", con);
                                cvb.ExecuteNonQuery();
                            }
                            Response.Redirect("bankstatment.aspx?ref2=" + PID);
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
                    string company;
                    company = reader["Oname"].ToString();

                    company1.InnerText = company;
                    string bl; string contact1;
                    bl = reader["BuissnessLocation"].ToString();
                    contact1 = reader["Contact"].ToString();
                    CompAddress.InnerText = bl;
                    Contact.InnerText = contact1;
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDateform.Text == "" || txtDateto.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                if (Request.QueryString["ref2"] != null)
                {
                    String PID = Convert.ToString(Request.QueryString["ref2"]);
                    Span2.InnerText = PID;
                    datTo.Visible = true; datFrom1.Visible = true; tomiddle.Visible = true;
                    datFrom1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("dd MMM, yyyy"); datTo.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM, yyyy");
                    mont.Visible = false; ;
                    SqlConnection con = new SqlConnection(strConnString);
                    con.Open();
                    str = "select * from tblbanktrans where account='" + PID + "' and date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    ds = new DataSet();
                    sqlda.Fill(ds, "Cat");
                    PagedDataSource Pds1 = new PagedDataSource();
                    Pds1.DataSource = ds.Tables[0].DefaultView;
                    Pds1.AllowPaging = true;
                    Pds1.PageSize = 100;
                    Pds1.CurrentPageIndex = CurrentPage;
                    Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                    btnPrevious.Enabled = !Pds1.IsFirstPage;
                    btnNext.Enabled = !Pds1.IsLastPage;
                    Repeater1.DataSource = Pds1;
                    Repeater1.DataBind();
                    con.Close();
                    BalanceDiv.Visible = false;
                    sPan1.Visible = false;
                }
            }
        }
        private void bindbankname()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                Badge.InnerHtml = PID;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select BankName,AccountNumber from tblBankAccounting where AccountName='" + PID + "'", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string company; string company2;
                        company = reader["BankName"].ToString();
                        company2 = reader["AccountNumber"].ToString();
                        H1.InnerText = company;
                    }
                }
            }
        }
        private void bindbalance()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select balance from tblbanktrans1 where account='" + PID + "'", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string company;
                        company = reader["balance"].ToString();

                        sPan1.InnerText = Convert.ToDouble(company).ToString("#,##0.00");
                    }
                }
            }
        }

        private void bindapprover()
        {
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

                }
            }
        }
        private void BindBrandsRptr2()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblbanktrans where account='" + PID + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();
                sqlda.Fill(ds, "Cat");
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                dvData.Sort = ViewState["Column"] + " " + ViewState["Sortorder"];
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = dvData;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 45;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
            }
        }
        public int CurrentPage
        {
            get
            {
                object s1 = this.ViewState["CurrentPage"];
                if (s1 == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(s1);
                }
            }

            set { this.ViewState["CurrentPage"] = value; }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindBrandsRptr2();

        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindBrandsRptr2();

        }

        protected void btnAmountCondition_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                if (Credit.Checked == true)
                {
                    str = "select * from tblbanktrans where cashin > '" + txtFilteredAmount.Text + "' and account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else if (Debit.Checked == true)
                {
                    str = "select * from tblbanktrans where cashout > '" + txtFilteredAmount.Text + "' and account='" + PID + "'";
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
                    str = "select * from tblbanktrans where balance > '" + txtFilteredAmount.Text + "' and account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
            else if (less.Checked == true)
            {
                if (Credit.Checked == true)
                {
                    str = "select * from tblbanktrans where cashin < '" + txtFilteredAmount.Text + "' and account='" + PID + "' and cashout > 0 and account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else if (Debit.Checked == true)
                {
                    str = "select * from tblbanktrans where cashout < '" + txtFilteredAmount.Text + "' and account='" + PID + "' and cashout > 0 and account='" + PID + "'";
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
                    str = "select * from tblbanktrans where balance < '" + txtFilteredAmount.Text + "' and account='" + PID + "' and cashout > 0 and account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
            else
            {
                if (Credit.Checked == true)
                {
                    str = "select * from tblbanktrans where cashin= '" + txtFilteredAmount.Text + "' and account='" + PID + "'";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);
                    DataView dvData = new DataView(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else if (Debit.Checked == true)
                {
                    str = "select * from tblbanktrans where cashout ='" + txtFilteredAmount.Text + "' and account='" + PID + "'";
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
                    str = "select * from tblbanktrans where balance = '" + txtFilteredAmount.Text + "' and account='" + PID + "'";
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
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            con2.Visible = true;
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            if (txtDateto.Text == "")
            {
                bindBankDataForExcel();
            }
            else
            {
                bindBankDataForExcelDate();
            }

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "bankStatement_" + PID + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/x-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            con2.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        private void bindBankDataForExcel()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            SqlConnection con = new SqlConnection(strConnString);
            str = "select * from tblbanktrans where account='" + PID + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            DataView dvData = new DataView(dt);
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        private void bindBankDataForExcelDate()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            SqlConnection con = new SqlConnection(strConnString);
            str = "select * from tblbanktrans where date between '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and  account='" + PID + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            DataView dvData = new DataView(dt);
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
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
    }
}