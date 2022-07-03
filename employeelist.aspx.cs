using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class employeelist : System.Web.UI.Page
    {
        SqlDataAdapter sqlda;
        DataTable ds;
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
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
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblEmployeeBasic where FullName LIKE '%" + name + "%'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataTable();
            sqlda.Fill(ds);

            Repeater1.DataSource = ds;
            Repeater1.DataBind();
        }
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblEmployeeBasic";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
        }
    }
}