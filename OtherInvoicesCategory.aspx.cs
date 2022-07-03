using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class OtherInvoicesCategory : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    bindEarning(); bindfixedaccount(); bindEarning(); bindfixedaccount3();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindEarning()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblIncomeBrand";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
        }
        private void bindfixedaccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where AccountType='Income'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlIncomeAccount.DataSource = dt;
                    ddlIncomeAccount.DataTextField = "Name";
                    ddlIncomeAccount.DataValueField = "ACT";
                    ddlIncomeAccount.DataBind();
                    ddlIncomeAccount.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtRate.Text == "" || ddlIncomeAccount.SelectedItem.Text == "-Select-")
            {
                lblMsg.Text = "Please fill all the required input"; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmdin = new SqlCommand("insert into tblIncomeBrand values('" + txtName.Text + "','" + txtRate.Text + "','" + txtUnit.Text + "','" + ddlIncomeAccount.SelectedItem.Text + "')", con);
                    con.Open();
                    cmdin.ExecuteNonQuery();
                    Response.Redirect("OtherInvoicesCategory.aspx");
                }
            }
        }
        private void bindfixedaccount3()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblIncomeBrand", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlExpense.DataSource = dt;
                    ddlExpense.DataTextField = "incomename";
                    ddlExpense.DataValueField = "id";
                    ddlExpense.DataBind();
                    ddlExpense.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
        protected void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Update tblIncomeBrand set  rate='" + txtUpdatePrice.Text + "',unit='" + txtUnitUpdate.Text + "' where incomename='" + ddlExpense.SelectedItem.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                Response.Redirect("OtherInvoicesCategory.aspx");
            }
        }
    }
}