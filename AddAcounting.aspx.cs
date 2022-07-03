using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace advtech.Finance.Accounta
{
    public partial class AddAcounting : System.Web.UI.Page
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
            str = "select * from tblBankAccounting";
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
                if (txtAccountName.Text == "" || txtAccountNumber.Text == "" || txtBankName.Text == "" || txtopening.Text == "" || txtOrderDate.Text == "")
                {
                    lblMsg.Text = "Please Fill All the required input"; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    SqlCommand cmd111 = new SqlCommand("insert into tblBankAccounting values('','" + txtAccountName.Text + "','" + txtAccountCode.Text + "','" + DropDownList1.SelectedItem.Text + "','" + txtAccountNumber.Text + "','" + txtBankName.Text + "','" + txtRemark.Text + "','Primary')", con);
                    con.Open();
                    cmd111.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from tblBankAccounting where AccountName='" + txtAccountName.Text + "' ", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string kc;
                        kc = reader["AccountNumber"].ToString();
                        reader.Close();
                        SqlCommand cmdbank = new SqlCommand("select * from tblbanktrans1 where account='" + txtAccountName.Text + "'", con);
                        using (SqlDataAdapter sda22 = new SqlDataAdapter(cmdbank))
                        {
                            DataTable dt = new DataTable();
                            sda22.Fill(dt); int j = dt.Rows.Count;
                            //
                            if (j != 0)
                            {
                                double t = Convert.ToDouble(dt.Rows[0][5].ToString()) + Convert.ToDouble(txtopening.Text);
                                SqlCommand cmd45 = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + txtAccountName.Text + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('-','-','" + txtopening.Text + "','0','" + t + "','" + txtAccountName.Text + "','','*Opening Balance*','" + DateTime.Now.Date + "')", con);
                                cvb.ExecuteNonQuery();
                            }
                            else
                            {
                                double t = Convert.ToDouble(txtopening.Text);
                                SqlCommand cvb = new SqlCommand("insert into tblbanktrans1 values('-','-','" + txtopening.Text + "','0','" + t + "','" + txtAccountName.Text + "','','*Opening Balance*','" + DateTime.Now.Date + "')", con);
                                cvb.ExecuteNonQuery();
                                SqlCommand cv1b = new SqlCommand("insert into tblbanktrans values('-','-','" + txtopening.Text + "','0','" + t + "','" + txtAccountName.Text + "','','*Opening Balance*','" + DateTime.Now.Date + "')", con);
                                cv1b.ExecuteNonQuery();

                            }
                        }
                    }
                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Cash at Bank'", con);
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
                                double paid = Convert.ToDouble(txtopening.Text);
                                Double M1 = Convert.ToDouble(ah12893);
                                Double bl1 = M1 + paid;
                                SqlCommand cmdcash = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Cash at Bank'", con);
                                cmdcash.ExecuteNonQuery();
                                string total = txtAccountName.Text + "-Opening Balance";
                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + total + "','','" + paid + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Cash at Bank','','Cash')", con);
                                cmd1974.ExecuteNonQuery();
                            }
                        }
                    }
                    Response.Redirect("AddAcounting.aspx");
                }

            }
        }
    }
}