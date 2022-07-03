using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class Activity : System.Web.UI.Page
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
                    ViewState["Column"] = "time";
                    ViewState["Sortorder"] = "ASC";
                    BindBrandsRptr2();

                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(DropDownList1.SelectedItem.Text);
            str = "select * from tblActivity where Module LIKE '%" + name + "%'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblActivity where Time between '" + Convert.ToDateTime(txtDateform.Text) + "' and '" + Convert.ToDateTime(txtDateto.Text) + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "Cat");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
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
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblActivity order by Time Desc";
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