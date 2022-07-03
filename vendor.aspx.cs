using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
namespace advtech.Finance.Accounta
{
    public partial class vendor : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
#pragma warning disable CS0169 // The field 'vendor.sqlda' is never used
        SqlDataAdapter sqlda;
#pragma warning restore CS0169 // The field 'vendor.sqlda' is never used
#pragma warning disable CS0169 // The field 'vendor.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'vendor.ds' is never used
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
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblVendor";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
        }
        protected void save(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                if (txtCustomerName.Text == "" || txtCompanyName.Text == "" || txtEmail.Text == "" || txtWorkPhone.Text == "" || txtMobile.Text == "" || txtCreditLimit.Text == "" || txtBillingAddress.Text == "" || txtOpeningBalance.Text == "" || txtDate.Text == "")
                {
                    lblMsg.Text = "Please Fill All the required input"; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    if (RadioButton5.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblVendor values('" + RadioButton5.Text + "','" + txtCustomerName.Text + "','" + txtCompanyName.Text + "','" + txtEmail.Text + "','" + txtWorkPhone.Text + "','" + txtMobile.Text + "','" + txtWebsite.Text + "','" + txtCreditLimit.Text + "','" + txtBillingAddress.Text + "','" + txtContactPerson.Text + "','" + txtOpeningBalance.Text + "','" + txtTerms.Text + "','Active','" + txtCreditLimit.Text + "')", con);
                        con.Open();
                        cmd111.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblVendor values('" + RadioButton6.Text + "','" + txtCustomerName.Text + "','" + txtCompanyName.Text + "','" + txtEmail.Text + "','" + txtWorkPhone.Text + "','" + txtMobile.Text + "','" + txtWebsite.Text + "','" + txtCreditLimit.Text + "','" + txtBillingAddress.Text + "','" + txtContactPerson.Text + "','" + txtOpeningBalance.Text + "','" + txtTerms.Text + "','Active','" + txtCreditLimit.Text + "')", con);
                        con.Open();
                        cmd111.ExecuteNonQuery();
                    }
                    //

                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Payable'", con);
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
                                if (txtOpeningBalance.Text == "0")
                                {
                                    string exp = Convert.ToString(txtCustomerName.Text) + "-Opening Balance";
                                    Double bl1 = M1;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Payable'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + exp + "','','0','" + Convert.ToDouble(txtOpeningBalance.Text) + "','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Payable','','Accounts Payable')", con);
                                    cmd1974.ExecuteNonQuery();
                                }
                                else
                                {
                                    string exp = Convert.ToString(txtCustomerName.Text) + "-Opening Balance";
                                    Double bl1 = M1 + Convert.ToDouble(txtOpeningBalance.Text);
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Payable'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + exp + "','','0','" + Convert.ToDouble(txtOpeningBalance.Text) + "','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Payable','','Accounts Payable')", con);
                                    cmd1974.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    Response.Redirect("vendor.aspx");
                }
            }
        }
    }
}