using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.WebControls;
namespace advtech.Finance.Accounta
{
    public partial class Manage_Expense : System.Web.UI.Page
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
                    bindEarning(); bindfixedaccount();
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
            str = "select * from tblManageExpense";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtNotes.Text == "" || ddlExpenseAccount.SelectedItem.Text == "-Select-")
            {
                lblMsg.Text = "Please fill all the required input"; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmdin = new SqlCommand("insert into tblManageExpense values('" + txtName.Text + "','" + ddlExpenseAccount.SelectedItem.Text + "','" + txtNotes.Text + "')", con);
                    con.Open();
                    cmdin.ExecuteNonQuery();
                    Response.Redirect("Manage_Expense.aspx");
                }
            }

        }
        private void bindfixedaccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblLedgAccTyp where AccountType='Expenses'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    ddlExpenseAccount.DataSource = dt;
                    ddlExpenseAccount.DataTextField = "Name";
                    ddlExpenseAccount.DataValueField = "ACT";
                    ddlExpenseAccount.DataBind();
                    ddlExpenseAccount.Items.Insert(0, new ListItem("-Select-", "0"));

                }
            }
        }
    }
}