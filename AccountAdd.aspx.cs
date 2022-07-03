using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;

namespace advtech.Finance.Accounta
{
    public partial class AccountAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (txtAccounttype.Text == "" || DropDownList4.SelectedItem.Text == "")
                {
                    lblMsg.Text = "Please Fill All the required input"; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    if (DropDownList4.SelectedItem.Text == "Cash" || DropDownList4.SelectedItem.Text == "Accounts Receivable" || DropDownList4.SelectedItem.Text == "Inventory" || DropDownList4.SelectedItem.Text == "Other Current Assets" ||
    DropDownList4.SelectedItem.Text == "Fixed Assets" || DropDownList4.SelectedItem.Text == "Other Assets" || DropDownList4.SelectedItem.Text == "Cost of Sales" || DropDownList4.SelectedItem.Text == "Expenses")
                    {
                        SqlCommand cmd = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','" + DropDownList4.SelectedItem.Text + "','Active','" + TextBox4.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('**Opening Balance**','','" + txtOpeningbalance.Text + "','0','" + txtOpeningbalance.Text + "','" + txtDate.Text + "','" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);
                        cmd19.ExecuteNonQuery();
                        SqlCommand cmd196 = new SqlCommand("insert into tblGeneralLedger2 values('**Opening Balance**','','0','0','" + txtOpeningbalance.Text + "','" + txtDate.Text + "','" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);
                        cmd196.ExecuteNonQuery();
                        Response.Redirect("Account.aspx");
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','" + DropDownList4.SelectedItem.Text + "','Active','" + TextBox4.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd19 = new SqlCommand("insert into tblGeneralLedger values('**Opening Balance**','','0','" + txtOpeningbalance.Text + "','" + txtOpeningbalance.Text + "','" + txtDate.Text + "','" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);
                        cmd19.ExecuteNonQuery();
                        SqlCommand cmd196 = new SqlCommand("insert into tblGeneralLedger2 values('**Opening Balance**','','0','0','" + txtOpeningbalance.Text + "','" + txtDate.Text + "','" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);
                        cmd196.ExecuteNonQuery();
                        Response.Redirect("Account.aspx");
                    }
                }
            }
        }
    }
}