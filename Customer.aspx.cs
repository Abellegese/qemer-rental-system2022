using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Web.Services;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace advtech.Finance.Accounta
{
    public partial class CustomerUtil : System.Web.UI.Page
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
                    BindBrandsRptr2(); bindshop(); bindcustomerno();
                    bindshopno(); BindBrandsRptr8();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void BindBrandsRptr8()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd16g = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where balance > 0", con);
                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd16g))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                    if (i2c3 != 0)
                    {
                        SqlDataReader reader6g = cmd16g.ExecuteReader();

                        if (reader6g.Read())
                        {
                            string ah7g;
                            ah7g = reader6g["Balance"].ToString();
                            reader6g.Close();
                            con.Close();
                            if (ah7g == null || ah7g == "")
                            {
                                H4.InnerText = "0.00";
                            }
                            else
                            {
                                H4.InnerText = "ETB " + Convert.ToDouble(ah7g).ToString("#,##0.00");
                            }


                        }

                    }
                    else
                    {
                        H4.InnerText = "ETB 0.00";
                    }
                }
            }
        }
        private void bindshopno()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblshop where status='Free'", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                    H3.InnerText = i2c3.ToString();
                }
            }
        }
        private void bindcustomerno()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblCustomers where Status='Active'", con);

                using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
                {
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                    H2.InnerText = i2c3.ToString();
                }
            }
        }
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblCustomers";
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
        private void bindshop()
        {
            ShopOperation bindShop = new ShopOperation();
            DataTable dt = bindShop.BindShop();
            if (dt.Rows.Count != 0)
            {
                ddlshop.DataSource = dt;
                ddlshop.DataTextField = "shopno";
                ddlshop.DataValueField = "ID";
                ddlshop.DataBind();
                ddlshop.Items.Insert(0, new ListItem("-Select shop-", "0"));
            }
        }
        protected void save(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (txtCustomerNameAmharic.Text == "")
                {
                    string message = "Please fill amharic name field!!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
                else
                {
                    if (ddlshop.Items.Count == 0)
                    {
                        lblMsg.Text = "Shop is not added. Please add the shop first."; lblMsg.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (txtBuisnesstype.Text == "" || txtCustomerName.Text == "" || txtCompanyName.Text == "" || txtEmail.Text == "" || txtWorkPhone.Text == "" || txtMobile.Text == "" || ddlshop.SelectedItem.Text == "-Select shop-" || txtContactPerson.Text == "")
                        {
                            lblMsg.Text = "Please fill all the required input"; lblMsg.ForeColor = Color.Red;
                        }
                        else
                        {

                            ShopOperation getShopInfo = new ShopOperation(ddlshop.SelectedItem.Text);
                            string status = getShopInfo.GetShopStatus().Item1;
                            string location = getShopInfo.GetShopStatus().Item4;
                            string area = getShopInfo.GetShopStatus().Item2;
                            string price = getShopInfo.GetShopStatus().Item3;
                            if (status == "Occupied")
                            {
                                lblMsg.Text = "The selected shop are occupied"; lblMsg.ForeColor = Color.Red;
                            }
                            else
                            {
                                CustomerUtil CustFinder = new CustomerUtil(txtCustomerName.Text);
                                int cu = CustFinder.CustomerChecker();
                                if (cu == 0)
                                {
                                    getShopInfo.AddShop(txtCustomerName.Text,ddlshop.SelectedItem.Text,"Primary");
                                    SqlCommand cmdus = new SqlCommand("insert into UsersCust values(N'" + txtCustomerName.Text + "',EncryptByPassPhrase('key', 'AB12345678#?'),'" + txtEmail.Text + "',N'" + txtCustomerName.Text + "','Active')", con);
                                    cmdus.ExecuteNonQuery();
                                    SqlCommand cmd455 = new SqlCommand("Update tblshop set status='Occupied' where shopno='" + ddlshop.SelectedItem.Text + "'", con);
                                    cmd455.ExecuteNonQuery();
                                    SqlCommand cmdm = new SqlCommand("insert into tblCustomers values(N'" + txtBuisnesstype.Text.TrimEnd() + "',N'" + txtCustomerName.Text.TrimEnd() + "',N'" + txtCompanyName.Text.TrimEnd() + "','" + txtEmail.Text.TrimEnd() + "','" + txtWorkPhone.Text.TrimEnd() + "','" + txtMobile.Text.TrimEnd() + "','" + txtWebsite.Text.TrimEnd() + "','" + txtCreditLimit.Text.TrimEnd() + "','" + txtContactPerson.Text.TrimEnd() + "','" + ddlterms.SelectedItem.Text.TrimEnd() + "','Active','" + txtCreditLimit.Text + "','" + ddlshop.SelectedItem.Text + "','" + location + "','" + area + "','" + price + "','" + txtOpeningBalance.Text + "','" + txtDatejoinig.Text + "','" + txtDueDate.Text + "','" + txtContigency.Text + "','" + txtServiceCharge.Text + "','" + txtAgreement.Text + "','" + txtGurentorName.Text.TrimEnd() + "','" + txtGAddress.Text + "','" + txtContactGurentor.Text + "','" + txtAddress.Text.TrimEnd() + "','" + txtTIN.Text.TrimEnd() + "','" + txtVatRegNumber.Text.TrimEnd() + "')", con);
                                    cmdm.ExecuteNonQuery();
                                    //Uploading Image {customer ID}
                                    if (FileUpload1.HasFile)
                                    {
                                        string SavePath = Server.MapPath("~/asset/custID/");
                                        if (!Directory.Exists(SavePath))
                                        {
                                            Directory.CreateDirectory(SavePath);
                                        }
                                        string Extention = Path.GetExtension(FileUpload1.PostedFile.FileName);
                                        FileUpload1.SaveAs(SavePath + "\\" + FileUpload1.FileName + Extention);

                                        SqlCommand cmd3 = new SqlCommand("insert into tblCustomerIdImage values('" + txtCustomerName.Text + "','" + FileUpload1.FileName + "','" + Extention + "')", con);
                                        cmd3.ExecuteNonQuery();
                                    }
                                    if (FileUpload2.HasFile)
                                    {
                                        string SavePath = Server.MapPath("~/asset/custID/");
                                        if (!Directory.Exists(SavePath))
                                        {
                                            Directory.CreateDirectory(SavePath);
                                        }
                                        string Extention = Path.GetExtension(FileUpload2.PostedFile.FileName);
                                        FileUpload2.SaveAs(SavePath + "\\" + FileUpload2.FileName + Extention);

                                        SqlCommand cmd3 = new SqlCommand("insert into tblCustomerIdImage values('" + txtCustomerName.Text + "','" + FileUpload2.FileName + "','" + Extention + "')", con);
                                        cmd3.ExecuteNonQuery();
                                    }
                                    if (FileUpload3.HasFile)
                                    {
                                        string SavePath = Server.MapPath("~/asset/custID/");
                                        if (!Directory.Exists(SavePath))
                                        {
                                            Directory.CreateDirectory(SavePath);
                                        }
                                        string Extention = Path.GetExtension(FileUpload3.PostedFile.FileName);
                                        FileUpload3.SaveAs(SavePath + "\\" + FileUpload3.FileName + Extention);

                                        SqlCommand cmd3 = new SqlCommand("insert into tblCustomerIdImage values('" + txtCustomerName.Text + "','" + FileUpload3.FileName + "','" + Extention + "')", con);
                                        cmd3.ExecuteNonQuery();
                                    }

                                    SqlCommand cmdSt = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','**Opening Balance**','','','','" + txtOpeningBalance.Text + "','" + txtCustomerName.Text + "')", con);
                                    cmdSt.ExecuteNonQuery();
                                    CustomerUtil InsertPeriodPayment = new CustomerUtil(txtCustomerName.Text);
                                    InsertPeriodPayment.amharicName = txtCustomerNameAmharic.Text;
                                    InsertPeriodPayment.PriceCalculator();
                                    Response.Redirect("Customer.aspx");
                                }
                                else
                                {
                                    lblMsg.Text = "Customer with the exact name has already been existing in the database. Try to make the name a bit different like changing some spelling."; lblMsg.ForeColor = Color.Red;
                                }
                            }
                        }
                    }
                }
            }
        }
    
        protected void Button2_Click1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName1.Text);
            str = "select * from tblCustomers where FllName LIKE '%" + name + "%'";
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
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("Label2") as Label;
                    if (lbl.Text == "Active")
                    {
                        lbl.Attributes.Add("class", "badge badge-success");
                    }
                    else
                    {
                        lbl.Attributes.Add("class", "badge badge-danger");
                    }
                }
            }
        }
        protected void btnSrviceChargeUpdate_Click(object sender, EventArgs e)
        {
            CustomerUtil SetServiceCharge = new CustomerUtil();
            SetServiceCharge.ServiceChargeAmount = txtServiceChargeUpdate.Text;
            SetServiceCharge.isChecked = Checkbox12.Checked;
            SetServiceCharge.ServiceChargeBulkUpdater();
        }
    }
}
