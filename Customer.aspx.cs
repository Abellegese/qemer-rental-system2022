using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
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
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblshop where status='Free'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlshop.DataSource = dt;
                    ddlshop.DataTextField = "shopno";
                    ddlshop.DataValueField = "ID";
                    ddlshop.DataBind();
                    ddlshop.Items.Insert(0, new ListItem("-Select shop-", "0"));
                }
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


                            SqlCommand cmd2AC = new SqlCommand("select * from tblshop where shopno='" + ddlshop.SelectedItem.Text + "'", con);
                            SqlDataReader readerAC = cmd2AC.ExecuteReader();

                            if (readerAC.Read())
                            {
                                String location = readerAC["location"].ToString();
                                String area = readerAC["area"].ToString();
                                String price = readerAC["monthlyprice"].ToString();
                                String status = readerAC["status"].ToString();

                                readerAC.Close();
                                if (status == "Occupied")
                                {
                                    lblMsg.Text = "The selected shop are occupied"; lblMsg.ForeColor = Color.Red;
                                }
                                else
                                {
                                    SqlCommand cmd4 = new SqlCommand("select * from tblCustomers where FllName='" + txtCustomerName.Text + "'", con);
                                    SqlDataAdapter sdacust = new SqlDataAdapter(cmd4);
                                    DataTable dtcust = new DataTable();
                                    sdacust.Fill(dtcust); int cu = dtcust.Rows.Count;
                                    if (cu == 0)
                                    {

                                        if (Checkbox2.Checked == true)
                                        {
                                            SqlCommand cmdus = new SqlCommand("insert into UsersCust values('" + txtCustomerName.Text + "',EncryptByPassPhrase('key', 'AB12345678#?'),'" + txtEmail.Text + "',N'" + txtCustomerName.Text + "','Active')", con);
                                            cmdus.ExecuteNonQuery();
                                            SqlCommand cmd2AC2 = new SqlCommand("select * from UsersCust where Username='" + txtCustomerName.Text + "'", con);
                                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                                            if (readerAC2.Read())
                                            {
                                                String location2 = readerAC2["password"].ToString(); readerAC2.Close();
                                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym customer portal</h3><br />";
                                                HTMLBODY += "<h4 class=\"\">Password:AB12345678#?</h4>";
                                                HTMLBODY += "<h4 class=\"\">Username:" + txtCustomerName.Text + "</h4>...please visit your portal:  https://localhost:44357/customer_home/Home.aspx";

                                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtEmail.Text);
                                                // Specify the email body
                                                mailMessage.Body = HTMLBODY;
                                                mailMessage.IsBodyHtml = true;
                                                // Specify the email Subject
                                                mailMessage.Subject = "Portal Authentication";

                                                // Specify the SMTP server name and post number
                                                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                                                // Specify your gmail address and password
                                                smtpClient.Credentials = new System.Net.NetworkCredential()
                                                {
                                                    UserName = "abellegese5@gmail.com",
                                                    Password = "Abel.lege2929#"
                                                };
                                                // Gmail works on SSL, so set this property to true
                                                smtpClient.EnableSsl = true;

                                                // Finall send the email message using Send() method
                                                smtpClient.Send(mailMessage);
                                            }
                                        }
                                        else
                                        {
                                            SqlCommand cmdus = new SqlCommand("insert into UsersCust values('" + txtCustomerName.Text + "',EncryptByPassPhrase('key', 'AB12345678#?'),'" + txtEmail.Text + "',N'" + txtCustomerName.Text + "','Inactive')", con);
                                            cmdus.ExecuteNonQuery();
                                        }

                                        SqlCommand cmd455 = new SqlCommand("Update tblshop set status='Occupied' where shopno='" + ddlshop.SelectedItem.Text + "'", con);
                                        cmd455.ExecuteNonQuery();
                                        SqlCommand cmdm = new SqlCommand("insert into tblCustomers values(N'" + txtBuisnesstype.Text.TrimEnd() + "',N'" + txtCustomerName.Text.TrimEnd() + "',N'" + txtCompanyName.Text.TrimEnd() + "','" + txtEmail.Text.TrimEnd() + "','" + txtWorkPhone.Text.TrimEnd() + "','" + txtMobile.Text.TrimEnd() + "','" + txtWebsite.Text.TrimEnd() + "','" + txtCreditLimit.Text.TrimEnd() + "','" + txtContactPerson.Text.TrimEnd() + "','" + ddlterms.SelectedItem.Text.TrimEnd() + "','Active','" + txtCreditLimit.Text + "','" + ddlshop.SelectedItem.Text + "','" + location + "','" + area + "','" + price + "','" + txtOpeningBalance.Text + "','" + txtDatejoinig.Text + "','" + txtDueDate.Text + "','" + txtContigency.Text + "','" + txtServiceCharge.Text + "','" + txtAgreement.Text + "','" + txtGurentorName.Text.TrimEnd() + "','" + txtGAddress.Text + "','" + txtContactGurentor.Text + "','" + txtAddress.Text.TrimEnd() + "','" + txtTIN.Text.TrimEnd() + "','"+txtVatRegNumber.Text.TrimEnd() + "')", con);
                                        cmdm.ExecuteNonQuery();

                                        /////////////////////////////////////////////////
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

                                        ///////////////////////////////////////////////
                                        SqlCommand cmdSt = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','**Opening Balance**','','','','" + txtOpeningBalance.Text + "','" + txtCustomerName.Text + "')", con);
                                        cmdSt.ExecuteNonQuery();
                                        /**
                                        string exp = txtCustomerName.Text + "-Opening Balance";
                                        SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + txtCustomerName.Text + "','" + DateTime.Now + "','" + txtOpeningBalance.Text + "','" + txtOpeningBalance.Text + "','" + exp + "','" + txtDueDate.Text + "')", con);
                                        cmdcrn.ExecuteNonQuery();
                                        SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='Accounts Receivable'", con);
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

                                                        Double bl1 = M1;
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + exp + "','','" + Convert.ToDouble(txtOpeningBalance.Text) + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                        cmd1974.ExecuteNonQuery();
                                                    }
                                                    else
                                                    {

                                                        Double bl1 = M1 + Convert.ToDouble(txtOpeningBalance.Text);
                                                        SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='Accounts Receivable'", con);
                                                        cmd45.ExecuteNonQuery();
                                                        SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + exp + "','','" + Convert.ToDouble(txtOpeningBalance.Text) + "','0','" + bl1 + "','" + DateTime.Now.Date + "','Accounts Receivable','','Accounts Receivable')", con);
                                                        cmd1974.ExecuteNonQuery();
                                                    }
                                                }
                                            }
                                        }
                                        **/
                                        SqlCommand cmd = new SqlCommand("select * from tblCustomers where FllName='" + txtCustomerName.Text + "'", con);
                                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                                        DataTable dt = new DataTable();
                                        sda.Fill(dt);
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (dt.Rows[i][10].ToString() == "Every Three Month")
                                            {
                                                if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][21].ToString() == "" || dt.Rows[i][21].ToString() == null)
                                                {
                                                    double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                                                    SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 3 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + txtCustomerNameAmharic.Text + "')", con);
                                                    cmdin.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15 + Convert.ToDouble(dt.Rows[i][21].ToString());
                                                    SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 3 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + txtCustomerNameAmharic.Text + "')", con);
                                                    cmdin.ExecuteNonQuery();
                                                }
                                            }
                                            else if (dt.Rows[i][10].ToString() == "Every Six Month")
                                            {
                                                if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][21].ToString() == "" || dt.Rows[i][21].ToString() == null)
                                                {
                                                    double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                                                    SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 6 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + txtCustomerNameAmharic.Text + "')", con);
                                                    cmdin.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15 + Convert.ToDouble(dt.Rows[i][21].ToString());
                                                    SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 6 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + txtCustomerNameAmharic.Text + "')", con);
                                                    cmdin.ExecuteNonQuery();
                                                }
                                            }
                                            else if (dt.Rows[i][10].ToString() == "Monthly")
                                            {
                                                if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][21].ToString() == "" || dt.Rows[i][21].ToString() == null)
                                                {
                                                    double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                                                    SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 1 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + txtCustomerNameAmharic.Text + "')", con);
                                                    cmdin.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15 + Convert.ToDouble(dt.Rows[i][21].ToString());
                                                    SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 1 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + txtCustomerNameAmharic.Text + "')", con);
                                                    cmdin.ExecuteNonQuery();
                                                }
                                            }
                                            else if (dt.Rows[i][10].ToString() == "Yearly")
                                            {
                                                if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][21].ToString() == "" || dt.Rows[i][21].ToString() == null)
                                                {
                                                    double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                                                    SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 12 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + txtCustomerNameAmharic.Text + "')", con);
                                                    cmdin.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15 + Convert.ToDouble(dt.Rows[i][21].ToString());
                                                    SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 12 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + txtCustomerNameAmharic.Text + "')", con);
                                                    cmdin.ExecuteNonQuery();
                                                }
                                            }
                                        }
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
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd4 = new SqlCommand("select * from tblCustomers where Status='Active'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd4);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double total = Convert.ToDouble(dt.Rows[i][16].ToString());
                    if (dt.Rows[i][10].ToString() == "Every Three Month")
                    {
                        double pricevat = 3 * total + 3 * Convert.ToDouble(txtServiceChargeUpdate.Text) + 3 * total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + pricevat + "', servicecharge='" + txtServiceChargeUpdate.Text + "' where customer='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + txtServiceChargeUpdate.Text + "' where FllName='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                    else if (dt.Rows[i][10].ToString() == "Every Six Month")
                    {
                        double pricevat = 6 * total + 6 * Convert.ToDouble(txtServiceChargeUpdate.Text) + 6 * total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + pricevat + "', servicecharge='" + txtServiceChargeUpdate.Text + "' where customer='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + txtServiceChargeUpdate.Text + "' where FllName='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                    else if (dt.Rows[i][10].ToString() == "Monthly")
                    {
                        double pricevat = total + Convert.ToDouble(txtServiceChargeUpdate.Text) + total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set  currentperiodue='" + pricevat + "', servicecharge='" + txtServiceChargeUpdate.Text + "' where customer='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + txtServiceChargeUpdate.Text + "' where FllName='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                    else
                    {
                        double pricevat = 12 * total + 12 * Convert.ToDouble(txtServiceChargeUpdate.Text) + 12 * total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set  currentperiodue='" + pricevat + "', servicecharge='" + txtServiceChargeUpdate.Text + "' where customer='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + txtServiceChargeUpdate.Text + "' where FllName='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                    if (Checkbox12.Checked == true)
                    {
                        if (dt.Rows[i][6].ToString() != "")
                        {

                            string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                            string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                            TwilioClient.Init(accountSid, authToken);

                            var message = MessageResource.Create(
                                body: "Dear customer, Monthly service charge updated to ETB " + Convert.ToDouble(txtServiceChargeUpdate.Text).ToString("#,#0.0"),
                                from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                to: new Twilio.Types.PhoneNumber("+251" + dt.Rows[i][6].ToString())
                            );
                        }
                    }
                }
                Response.Redirect("Customer.aspx");
            }
        }
    }
}
