using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class EarnedRevenue : System.Web.UI.Page
    {
        SqlDataAdapter sqlda;
        DataSet ds;
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Column"] = "Date";

                    ViewState["Sortorder"] = "ASC";
                    BindBrandsRptr2(); mont.InnerText = "As of " + DateTime.Now.ToString("MMMM dd, yyyy");
                    bindcompany(); BindBrandsRptrSUM(); datTo.Visible = false; datFrom1.Visible = false; tomiddle.Visible = false;
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
                SqlCommand cmd = new SqlCommand("select Oname,OAdress,Contact from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company;
                    company = reader["Oname"].ToString();
                    string address = reader["OAdress"].ToString();
                    string cont = reader["Contact"].ToString();
                    oname.InnerText = company;
                    addressname.InnerText = "Address: " + address;

                    phone.InnerText = "Contact: " + cont;
                }
            }
        }
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblCustomerStatement where Payment>0";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            DataView dvData = new DataView(dt);
            dvData.Sort = ViewState["Column"] + " " + ViewState["Sortorder"];

            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = dvData;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 100;
            Pds1.CurrentPageIndex = CurrentPage;
            Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
            btnPrevious.Enabled = !Pds1.IsFirstPage;
            btnNext.Enabled = !Pds1.IsLastPage;
            Repeater1.DataSource = Pds1;
            Repeater1.DataBind();
            con.Close();
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
        private void BindBrandsRptrSUM()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select SUM(Payment) from tblCustomerStatement where Payment >0 ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
            {
                Total.InnerText = "ETB 0.00";
            }
            else
            {
                double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                Total.InnerText = "ETB " + Payment.ToString("#,##0.00");
                con.Close();
            }
        }
        private void BindBrandsRptrSUMDateRange()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select SUM(Payment) from tblCustomerStatement where Date BETWEEN '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Payment >0 ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
            {
                Total.InnerText = "ETB 0.00";
            }
            else
            {
                double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                Total.InnerText = "ETB " + Payment.ToString("#,##0.00");
                con.Close();
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            datTo.Visible = true; datFrom1.Visible = true; tomiddle.Visible = true;
            datFrom1.InnerText = txtDateform.Text; datTo.InnerText = txtDateto.Text;
            mont.Visible = false;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblCustomerStatement where Date BETWEEN '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and Payment >0 ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            BindBrandsRptrSUMDateRange();
            con.Close();
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

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblCustomerStatement where Payment >0 ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            BindBrandsRptrSUM();
            con.Close();
        }
        private void sumgreater()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                str = "select sum(Payment) from tblCustomerStatement where Payment > '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt); int i = dt.Rows.Count;
                if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
                {
                    Total.InnerText = "ETB 0.00";
                }
                else
                {
                    double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                    Total.InnerText = "ETB " + Payment.ToString("#,##0.00");
                    con.Close();
                }
            }
            else if (less.Checked == true)
            {
                str = "select sum(Payment) from tblCustomerStatement where Payment < '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt); int i = dt.Rows.Count;
                if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
                {
                    Total.InnerText = "ETB 0.00";
                }
                else
                {
                    double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                    Total.InnerText = "ETB " + Payment.ToString("#,##0.00");
                    con.Close();
                }
            }
            else
            {
                str = "select sum(Payment) from tblCustomerStatement where Payment = '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt); int i = dt.Rows.Count;
                if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
                {
                    Total.InnerText = "ETB 0.00";
                }
                else
                {
                    double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                    Total.InnerText = "ETB " + Payment.ToString("#,##0.00");
                    con.Close();
                }
            }
        }
        protected void btnCondition_Click(object sender, EventArgs e)
        {
            sumgreater();
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                str = "select * from tblCustomerStatement where Payment > '" + txtFilteredAmount.Text + "'";
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
                str = "select * from tblCustomerStatement where Payment < '" + txtFilteredAmount.Text + "' and Payment>0";
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
                str = "select * from tblCustomerStatement where Payment = '" + txtFilteredAmount.Text + "'";
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