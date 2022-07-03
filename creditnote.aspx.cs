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
    public partial class creditnote : System.Web.UI.Page
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
                    BindBrandsRptr2(); bindInvExistence();

                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindInvExistence()
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblcreditnote", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    if (i == 0) { container.Visible = false; NoInvoiceDiv.Visible = true; }
                }
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Credit_Notes_Raksym_Trading" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);


            Repeater1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblcreditnote";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "PName");
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                PagedDataSource Pds1 = new PagedDataSource();
                Pds1.DataSource = ds.Tables[0].DefaultView;
                Pds1.AllowPaging = true;
                Pds1.PageSize = 30;
                Pds1.CurrentPageIndex = CurrentPage;
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();
                con.Close();
            }
            else
            {
                mainb.Visible = true;
                buttondiv.Visible = false;
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

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("Label2") as Label;
                    Label lbl2 = item.FindControl("Label3") as Label;
                    Label lbl4 = item.FindControl("Label_write_off") as Label;
                    Label lbl3 = item.FindControl("Label5") as Label;
                    Label lblExp = item.FindControl("lblExp") as Label;
                    Label lblcust = item.FindControl("lblCustomer") as Label;
                    Label lblID = item.FindControl("lblID") as Label;
                    Literal Literal = item.FindControl("Literal1") as Literal;
                    Literal LiteralDrop = item.FindControl("LiteralDropdown") as Literal;
                    string totalexp = lblcust.Text + "-Opening Balance";
                    if (lbl.Visible == true)
                    {
                        LiteralDrop.Visible = true;
                    }
                    else
                    {
                        LiteralDrop.Visible = false;
                    }
                    if (lblExp.Text == totalexp)
                    {
                        Literal.Text = "<a class=\"text-primary\" id=\"link\" runat =\"server\" href=\"creditnotedetails.aspx?ref2=" + lblID.Text + "&&cust=" + lblcust.Text + "\">CN#-" + lblID.Text + "</a>";
                        LiteralDrop.Text = "<div class=\"dropdown mb-4\"><a class=\"btn btn-sm btn-icon-only text-light\" href=\"#\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"><i class=\"fas fa-ellipsis-v text-primary\"></i></a><div class=\"dropdown-menu dropdown-menu-right shadow animated--fade-in dropdown-menu-arrow\"><a class=\"dropdown-item\" href=\"creditnoteopeningpayment.aspx?cust=" + lblcust.Text + "&&id=" + lblID.Text + "\">Cash Payment</a><a class=\"dropdown-item\" href=\"creditnoteopeningpaymentbank.aspx?cust=" + lblcust.Text + "&&id=" + lblID.Text + "\">Bank Payment</a></div></div>";

                    }
                    else
                    {
                        LiteralDrop.Visible = false;
                        Literal.Text = "<a class=\"text-primary\" id=\"link\" runat =\"server\" href=\"creditnotedetails.aspx?ref2=" + lblID.Text + "&&cust=" + lblcust.Text + "\">CN#-" + lblID.Text + "</a>";
                    }
                    if (Convert.ToDouble(lbl3.Text) > 0)
                    {
                        lbl.Visible = true;
                        lbl2.Visible = false;
                        lbl4.Visible = false;
                        lbl3.Attributes.Add("class", "text-danger");
                    }
                    if (Convert.ToDouble(lbl3.Text) < 0)
                    {

                        lbl.Visible = false;
                        lbl2.Visible = false;
                        lbl4.Visible = true;
                        lbl3.Attributes.Add("class", "text-warning");

                    }
                    if (Convert.ToDouble(lbl3.Text) == 0)
                    {
                        lbl.Visible = false;
                        lbl2.Visible = true;
                        lbl4.Visible = false;
                        lbl3.Attributes.Add("class", "text-success");
                    }
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblcreditnote where customer LIKE '%" + name + "%'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 80;
            Pds1.CurrentPageIndex = CurrentPage;
            Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
            btnPrevious.Enabled = !Pds1.IsFirstPage;
            btnNext.Enabled = !Pds1.IsLastPage;
            Repeater1.DataSource = Pds1;
            Repeater1.DataBind();
        }
        protected void btnAmountCondition_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                str = "select * from tblcreditnote where balance > '" + txtFilteredAmount.Text + "'";
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
                str = "select * from tblcreditnote where balance < '" + txtFilteredAmount.Text + "'";
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
                str = "select * from tblcreditnote where balance = '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }

        }
        protected void btnBindFreeShops_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblcreditnote where balance>0";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 200;
            Pds1.CurrentPageIndex = CurrentPage;
            Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
            btnPrevious.Enabled = !Pds1.IsFirstPage;
            btnNext.Enabled = !Pds1.IsLastPage;
            Repeater1.DataSource = Pds1;
            Repeater1.DataBind();
        }

        protected void btnbindall_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblcreditnote";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 200;
            Pds1.CurrentPageIndex = CurrentPage;
            Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
            btnPrevious.Enabled = !Pds1.IsFirstPage;
            btnNext.Enabled = !Pds1.IsLastPage;
            Repeater1.DataSource = Pds1;
            Repeater1.DataBind();
        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            String PID = "All";
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "creditbote_" + PID + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/x-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            con.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        protected void btnBindWrite_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblcreditnote where balance < 0";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 200;
            Pds1.CurrentPageIndex = CurrentPage;
            Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
            btnPrevious.Enabled = !Pds1.IsFirstPage;
            btnNext.Enabled = !Pds1.IsLastPage;
            Repeater1.DataSource = Pds1;
            Repeater1.DataBind();
        }
    }
}