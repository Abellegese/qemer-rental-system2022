using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelectPdf;

namespace advtech.Finance.Accounta
{
    public partial class rentinvoicereport : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        SqlCommand com2;
        SqlDataAdapter sqlda2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                DuplicateRow.Visible = false;
                if (!IsPostBack)
                {
                    bindsearch();
                    ViewState["Column"] = "date";
                    ViewState["Sortorder"] = "DESC";

                    bindcompany(); bindstatus(); InvNoBinding.Visible = false;

                    RefTag.Visible = false; datTo.Visible = false; datFrom1.Visible = false; tomiddle.Visible = false;
                    if (Request.QueryString["cust"] != null)
                    {
                        bindrequest(); bindTotals(); bindcredit(); BindShopNo(); bindINFO();
                        bind_logo_visibility();
                    }
                    else
                    {
                        bindTotals2(); CreditDiv.Visible = false; CreditDiv2.Visible = false;
                    }
                    bindPM();
                    bind_footer(); bindInvExistence(); bindPaymentstatus();
                    NumberToWord();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }
        protected string bindInvoiceDateVisibility()
        {
            if (Request.QueryString["cust"] != null)
            {
                return "style=\"display:none\"";
            }
            else
            {
                return "style=\"display:normal\"";
            }
        }
        protected string bindDescriptionColVisibility()
        {
            if (Request.QueryString["cust"] != null)
            {
                return "style=\"display:normal\"";
            }
            else
            {
                return "style=\"display:none\"";
            }
        }
        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string explanation = "Dear "+PID+" you made transaction amount of " + double.Parse(Total.InnerText).ToString("#,##0.00")+" from our" +
                " company. Thanks for your service.";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    con1.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();
                        MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtEmail.Text);
                        // Specify the email body
                        mailMessage.Body = explanation;
                        mailMessage.IsBodyHtml = true;
                        // Specify the email Subject
                        mailMessage.Subject = "Transaction for invoice# "+PID2;
                        mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), "Invoice.pdf"));
                        // Specify the SMTP server name and post number
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        // Specify your gmail address and password
                        smtpClient.Credentials = new System.Net.NetworkCredential()
                        {
                            UserName = "abellegese5@gmail.com",
                            Password = "xjiwawyuksgestqk"
                        };
                        // Gmail works on SSL, so set this property to true
                        smtpClient.EnableSsl = true;
                        // Finall send the email message using Send() method
                        smtpClient.Send(mailMessage);
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
        }
        private void NumberToWord()
        {
            if (Request.QueryString["cust"] != null)
            {
                double total = double.Parse(Total.InnerText);
                NumberToWords NumToWrd = new NumberToWords();
                AmountInWords.InnerText = NumToWrd.ConvertAmount(total);
            }
        }
        private void bindPM()
        {
            if (Request.QueryString["cust"] != null)
            {
                PayMode.Visible = true; PayMode1.Visible = true;
            }
            else
            {
                PayMode.Visible = false; PayMode1.Visible = false;
            }
        }

        private void bindINFO()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["cust"]);
                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {
                    string Address2 = readercrd["addresscust"].ToString();
                    string TIN = readercrd["TIN"].ToString();
                    string vatRegNumber = readercrd["vatregnumber"].ToString();
                    readercrd.Close();
                    if (Address2 == "" || Address2 == null) { Address.InnerText = "-         -"; DupAddress.InnerText = "-         -"; }
                    else { Address.InnerText = Address2; DupAddress.InnerText = Address2; }
                    TINNUMBER.InnerText = TIN;
                    DupCustomerPIN.InnerText = TIN;
                    if (vatRegNumber != "")
                    {
                        CustVatRegNumber.InnerText = vatRegNumber;
                    }
                    //Duplivate binding
                }
            }
        }
        private void bindInvExistence()
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblrentreceipt", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); long i = dt.Rows.Count;
                    if (i == 0) { container.Visible = false; NoInvoiceDiv.Visible = true; }
                }
            }
        }
        private void bindsearch()
        {
            if (Request.QueryString["search"] != null)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    String PID = Convert.ToString(Request.QueryString["search"]);
                    PID = PID.Substring(2);
                    SqlCommand cmd = new SqlCommand("select * from tblrentreceipt where id2 LIKE '%" + PID + "%' or references1='" + PID + "'", con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); long i = dt.Rows.Count;
                        if (i != 0)
                        {
                            SqlDataReader reader22 = cmd.ExecuteReader();
                            if (reader22.Read())
                            {
                                string pstatus; pstatus = reader22["customer"].ToString();
                                string id2; id2 = reader22["id2"].ToString();
                                reader22.Close();

                                Response.Redirect("rentinvoicereport.aspx?id=" + id2 + "&&cust=" + pstatus);
                            }
                        }
                        else
                        {

                            CCF.Visible = true; container.Visible = false;
                        }
                    }

                }
            }
        }
        private void BindShopNo()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                SqlDataReader reader = cmd2.ExecuteReader();

                if (reader.Read())
                {
                    string shopno = reader["shopno"].ToString();
                    ShopNo.InnerText = shopno;
                    shopno1.InnerText = shopno;
                }
            }
        }
        private void bindcredit()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            Name.InnerText = PID;
            Name1.InnerText = PID;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcn = new SqlCommand("select sum(balance) from tblcreditnote where customer='" + PID + "' and balance > 0", con);
                SqlDataAdapter sdacn = new SqlDataAdapter(cmdcn);
                DataTable dtcn = new DataTable();
                sdacn.Fill(dtcn); long nb = dtcn.Rows.Count;
                if (dtcn.Rows[0][0].ToString() == null || dtcn.Rows[0][0].ToString() == "")
                {
                    credittotal.InnerText = "0.00";
                }
                else
                {
                    credittotal.InnerText = Convert.ToDouble(dtcn.Rows[0][0].ToString()).ToString("#,##0.00");
                }
            }
        }
        private void bindTotals2()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select sum(vatfree) as cash,sum(paid) as cashvat from tblrentreceipt", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string SC1;
                    string cash;
                    string cashvat;
                    double d;

                    cash = reader["cash"].ToString();
                    SC1 = cash;
                    cashvat = reader["cashvat"].ToString();
                    if (cash == "" || cash == null)
                    {

                    }
                    else
                    {
                        double VATFREE = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC1)) / 1.15;
                        VATfree.InnerText = Convert.ToDouble(VATFREE).ToString("#,##0.00");
                        d = Convert.ToDouble(cashvat) - Convert.ToDouble(SC1) - VATFREE;
                        VAT.InnerText = Convert.ToDouble(d).ToString("#,##0.00");

                        Total.InnerText = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC1)).ToString("#,##0.00");
                        //Duplicate Details

                    }
                    reader.Close();
                }
            }
        }

        private void bindTotals()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (Request.QueryString["cust"] != null)
                {
                    String PID = Convert.ToString(Request.QueryString["cust"]);
                    String PID2 = Convert.ToString(Request.QueryString["id"]);
                    SqlCommand cmd2SC = new SqlCommand("select * from tblrent where customer='" + PID + "'", con);
                    SqlDataReader readerSC = cmd2SC.ExecuteReader();

                    if (readerSC.Read())
                    {
                        string servicecharge;

                        servicecharge = readerSC["servicecharge"].ToString();
                        readerSC.Close();
                        SqlCommand cmd = new SqlCommand("select sum(vatfree) as cash,sum(paid) as cashvat,fsno,payment_mode from tblrentreceipt where customer='" + PID + "' and id2='" + PID2 + "' group by fsno,payment_mode", con);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string SC1;
                            string cash;
                            string cashvat;
                            double d;
                            string FSnumber = reader["fsno"].ToString();
                            string payment_mode = reader["payment_mode"].ToString();
                            PaymentMode.InnerText = payment_mode;
                            PaymentMode1.InnerText = payment_mode;
                            FSno.InnerText = "FS# 0000" + FSnumber;
                            FSno1.InnerText = "FS# 0000" + FSnumber;
                            SC1 = reader["cash"].ToString();
                            cash = reader["cash"].ToString();
                            cashvat = reader["cashvat"].ToString(); reader.Close();
                            if (cash == "" || cash == null)
                            {

                            }
                            else
                            {
                                double VATFREE = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC1)) / 1.15;
                                VATfree.InnerText = Convert.ToDouble(VATFREE).ToString("#,##0.00");
                                d = Convert.ToDouble(cashvat) - Convert.ToDouble(SC1) - VATFREE;
                                VAT.InnerText = Convert.ToDouble(d).ToString("#,##0.00");

                                Total.InnerText = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC1)).ToString("#,##0.00");
                                //Duplicate Details
                                DupVatFree.InnerText = Convert.ToDouble(VATFREE).ToString("#,##0.00");
                                DupVAT.InnerText = Convert.ToDouble(d).ToString("#,##0.00");
                                DupGrandTotal.InnerText = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC1)).ToString("#,##0.00");

                            }
                        }
                    }

                }
            }
        }
        private void bindcompany()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company; string bl; string contact1;
                    company = reader["Oname"].ToString();
                    bl = reader["OAdress"].ToString();
                    contact1 = reader["Contact"].ToString();
                    string TINNumber = reader["TIN"].ToString();
                    VendorTIN.InnerText = TINNumber;
                    VendorVatRegNumber.InnerText = reader["vatregnumber"].ToString();
                    DupVendorPIN.InnerText = TINNumber;
                    CompAddress.InnerText = bl;
                    Contact.InnerText = contact1;
                    Ad2.InnerText = bl;
                    Ct2.InnerText = contact1;
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDatefrom.Text == "" || txtDateto.Text == "")
            {

            }
            else
            {
                printdate.InnerText = "Invoice summary";
                datTo.Visible = true; datFrom1.Visible = true; tomiddle.Visible = true;
                datFrom1.InnerText = Convert.ToDateTime(txtDatefrom.Text).ToString("dd MMM yyyy"); datTo.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");

                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                string name = Convert.ToString(txtCustomerName.Text);
                SqlCommand cmd = new SqlCommand("select sum(vatfree) as cash,sum(paid) as cashvat from tblrentreceipt where date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string SC;

                    string cash;
                    string cashvat;
                    SC = reader["cash"].ToString();
                    cash = reader["cash"].ToString();
                    cashvat = reader["cashvat"].ToString(); reader.Close();
                    if (cash != "" || cashvat != "")
                    {
                        double VF = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC)) / 1.15;
                        VATfree.InnerText = VF.ToString("#,##0.00");
                        VAT.InnerText = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC) - VF).ToString("#,##0.00");

                        Total.InnerText = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC)).ToString("#,##0.00");
                    }
                    else
                    {

                        VATfree.InnerText = "ETB 0.00";
                        VAT.InnerText = "ETB 0.00";
                        Total.InnerText = "ETB 0.00";
                    }
                    str = "select * from tblrentreceipt  where date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'";
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
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            SqlCommand cmd = new SqlCommand("select sum(vatfree) as cash,sum(paid) as cashvat from tblrentreceipt where customer LIKE '%" + name + "%'", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string cash;
                string SC;
                string cashvat;
                double d;
                SC = reader["cash"].ToString();
                cash = reader["cash"].ToString();
                cashvat = reader["cashvat"].ToString(); reader.Close();

                if (cash == "" || cash == null)
                {

                }
                else
                {
                    d = Convert.ToDouble(cashvat) - Convert.ToDouble(cash);
                    double VATFREE = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC)) / 1.15;
                    VATfree.InnerText = VATFREE.ToString("#,##0.00");
                    VAT.InnerText = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC) - VATFREE).ToString("#,##0.00");
                    Total.InnerText = (Convert.ToDouble(cashvat) - Convert.ToDouble(SC)).ToString("#,##0.00");
                }
                str = "select * from tblrentreceipt where customer LIKE '%" + name + "%'";
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
        }
        private void bindrequest()
        {
            if (Request.QueryString["cust"] != null)
            {
                InvNoBinding.Visible = true;
                Button3.Visible = true;
                Button4.Visible = true;
                RaksTDiv.Visible = true;
                NotesDiv.Visible = true;
                CustomerTIN.Visible = true;
                CustVatRegNumberSpan.Visible = true;
                Addressbar.Visible = true;
                Button5.Visible = true;
                Button1.Visible = false;
                Sp2.Visible = false;
                modalMain.Visible = false;
                btnDelete.Visible = true;
                btnEdit.Visible = true;
                btnDeleteAll.Visible = false;
                amoundInWordsDiv.Visible = true;
                dateDiv.Visible = true;
                String PID = Convert.ToString(Request.QueryString["cust"]);
                String PID2 = Convert.ToString(Request.QueryString["id"]); InvNoBinding.InnerText = "INV# -" + PID2;
                InvoiceBadge.InnerText = "INV# -" + PID2;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select TOP 1 * from tblrentreceipt where customer='" + PID + "' and id2='" + PID2 + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();

                string str2 = "select TOP 1 * from tblrentreceipt where customer='" + PID + "' and id2='" + PID2 + "'";
                com2 = new SqlCommand(str2, con);
                sqlda2 = new SqlDataAdapter(com2);
                DataTable dt = new DataTable();
                sqlda2.Fill(dt);
                long i = dt.Rows.Count;
                if (i != 0)
                {
                    RefTag.Visible = true;
                    dateSpan.InnerText = Convert.ToDateTime(dt.Rows[0]["date"].ToString()).ToString("MMM dd, yyyy");
                    RefTag.InnerText = "Ref# " + dt.Rows[0][2].ToString();
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                    btnNext.Visible = false;
                    btnPrevious.Visible = false;
                    Label1.Visible = false;
                    BindShop.Visible = true;
                }
            }
        }
        private void bindstatus()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrentreceipt";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "PName");
            DataTable dt1 = new DataTable();
            sqlda.Fill(dt1);
            DataView dvData = new DataView(dt1);
            dvData.Sort = ViewState["Column"] + " " + ViewState["Sortorder"];
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = dvData;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 30;
            Pds1.CurrentPageIndex = CurrentPage;


            string str2 = "select * from tblrentreceipt order by date desc";
            com2 = new SqlCommand(str2, con);
            sqlda2 = new SqlDataAdapter(com2);
            DataTable dt = new DataTable(); sqlda2.Fill(dt); long i = dt.Rows.Count;
            if (i != 0)
            {
                Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
                btnPrevious.Enabled = !Pds1.IsFirstPage;
                btnNext.Enabled = !Pds1.IsLastPage;
                Repeater1.DataSource = Pds1;
                Repeater1.DataBind();

                con.Close(); main2.Visible = false;

            }
            else
            {
                btnNext.Visible = false;
                btnPrevious.Visible = false;
                TotalRow.Visible = false;
                con1.Visible = false;
                DuplicateRow.Visible = false;
                LeftDiv.Visible = false;
                BodyDiv.Style.Add("height", "700px");

            }
        }
        private void bindduplicate()
        {
            if (Request.QueryString["cust"] != null)
            {
                InvNoBinding.Visible = true;
                DuplicateRow.Visible = true;
                btnDeleteAll.Visible = false;
                String PID = Convert.ToString(Request.QueryString["cust"]);

                String PID2 = Convert.ToString(Request.QueryString["id"]); InVNo.InnerText = "INV# -" + PID2;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select TOP 1 * from tblrentreceipt where customer='" + PID + "' and id2='" + PID2 + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                ds = new DataSet();

                string str2 = "select TOP 1 * from tblrentreceipt where customer='" + PID + "' and id2='" + PID2 + "'";
                com2 = new SqlCommand(str2, con);
                sqlda2 = new SqlDataAdapter(com2);
                DataTable dt = new DataTable();
                sqlda2.Fill(dt);
                long i = dt.Rows.Count;
                if (i > 0)
                {
                    RefTag.Visible = true;
                    RefTag.InnerText = "Ref# " + dt.Rows[0][2].ToString();
                    Ref2.InnerText = "Ref# " + dt.Rows[0][2].ToString();
                    Repeater2.DataSource = dt;
                    Repeater2.DataBind();
                    btnNext.Visible = false;
                    btnPrevious.Visible = false;
                    Label1.Visible = false;
                }
            }
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
            bindstatus();

        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            bindstatus();

        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater2.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblSC = item.FindControl("lblSC") as Label;
                    Label lblCust = item.FindControl("lblCust") as Label;
                    Label lbl = item.FindControl("lblvatplus") as Label;
                    Label lbl2 = item.FindControl("lblcatminus") as Label;
                    Label lbl3 = item.FindControl("lblVAT") as Label;

                    double VATFREE = (Convert.ToDouble(lbl.Text) - Convert.ToDouble(lblSC.Text)) / 1.15;
                    lbl2.Text = VATFREE.ToString("#,##0.00");
                    double VAT = (Convert.ToDouble(lbl.Text) - Convert.ToDouble(lblSC.Text)) - VATFREE;
                    lbl3.Text = VAT.ToString("#,##0.00");
                    lbl.Text = (Convert.ToDouble(lbl.Text) - Convert.ToDouble(lblSC.Text)).ToString("#,##0.00");
                }
            }
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
            bindstatus();
        }
        protected void btnDuplicateInvoice_Click(object sender, EventArgs e)
        {
            bindduplicate();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            String PID = "report";
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "invoce_" + PID + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/x-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            conw.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        private string BindUser()
        {
            string user = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String FN = readerAC["Name"].ToString();
                    readerAC.Close();
                    user += FN;
                }
            }
            return user;
        }
        private double bindDifferenece()
        {
            double difference = Convert.ToDouble(Total.InnerText) - Convert.ToDouble(txtEditAmount.Text);
            return difference;
        }
        private void bindPaymentstatus()
        {
            double SC = 0; double due = 0;
            String PID = Convert.ToString(RefTag.InnerText);

            String PID2 = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID2 + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {

                    string pp = readercrd["PaymentDuePeriod"].ToString();

                    readercrd.Close();

                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID2 + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {

                        string kc; string dueamount = reader["currentperiodue"].ToString();
                        string servicecharge; servicecharge = reader["servicecharge"].ToString();
                        kc = reader["currentperiodue"].ToString();
                        if (pp == "Every Three Month") { SC += Convert.ToDouble(servicecharge) * 3; }
                        else if (pp == "Monthly") { SC += Convert.ToDouble(servicecharge) * 1; }
                        else if (pp == "Every Six Month") { SC += Convert.ToDouble(servicecharge) * 6; }
                        else { SC += Convert.ToDouble(servicecharge) * 12; }
                        due += Convert.ToDouble(dueamount);

                        double invoice_amount = due - SC;
                        double payment_status = Convert.ToDouble(Total.InnerText) - invoice_amount;
                        if (Math.Round(payment_status) == 0)
                        {
                            PaymensStatus.InnerText = "fully paid";
                            PaymensStatus.Attributes.Add("class", "badge badge-success mr-2 text-uppercase");
                        }
                        else if (Math.Round(payment_status) > 0)
                        {
                            PaymensStatus.InnerText = "over paid";
                            PaymensStatus.Attributes.Add("class", "badge badge-warning mr-2 text-uppercase");
                        }
                        else
                        {
                            PaymensStatus.InnerText = "credited";
                            Link.HRef = "creditnotedetails.aspx?ref=" + PID.Substring(5) + "&&cust=" + PID2;
                            PaymensStatus.Attributes.Add("class", "badge badge-warning mr-2 text-uppercase");
                        }
                    }
                }
            }
        }
        private Tuple<double, double> bindSC()
        {
            double SC = 0; double due = 0;
            String PID = Convert.ToString(Request.QueryString["id"]);
            String PID2 = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where FllName='" + PID2 + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {

                    string pp = readercrd["PaymentDuePeriod"].ToString();
                    string limit = readercrd["CreditLimit"].ToString();
                    readercrd.Close();

                    SqlCommand cmd2 = new SqlCommand("select * from tblrent where customer='" + PID2 + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {

                        string kc; string duedates = reader["duedate"].ToString();
                        string servicecharge; servicecharge = reader["servicecharge"].ToString();
                        kc = reader["currentperiodue"].ToString();
                        if (pp == "Every Three Month") { SC += Convert.ToDouble(servicecharge) * 3; }
                        else if (pp == "Monthly") { SC += Convert.ToDouble(servicecharge) * 1; }
                        else if (pp == "Every Six Month") { SC += Convert.ToDouble(servicecharge) * 6; }
                        else { SC += Convert.ToDouble(servicecharge) * 12; }
                        due = Convert.ToDouble(kc);

                    }
                }
            }
            return Tuple.Create(SC, due);
        }
        private double calculate_balance()
        {
            double full_amount = bindSC().Item2;
            double current_total = Convert.ToDouble(Total.InnerText) + bindSC().Item1;

            double new_total = Convert.ToDouble(txtEditAmount.Text) + bindSC().Item1;
            double difference_current = full_amount - current_total;
            double difference_new = full_amount - new_total;
            double new_balabce = difference_current - difference_new;
            return new_balabce;
        }
        private double calculate_current_balance()
        {
            double full_amount = bindSC().Item2;
            double current_total = Convert.ToDouble(Total.InnerText) + bindSC().Item1;
            double difference_current = full_amount - current_total;
            return difference_current;
        }
        private void bind_account_receivable_remove()
        {
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string CashorBank = "Accounts Receivable";
            string explanation = "Account adjustment for invoice number " + PID2 + " deletion";
            double diff = calculate_current_balance();
            if (diff != 0)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'", con);
                    using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                    {
                        DataTable dtBrands2322 = new DataTable();
                        sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
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
                                Double bl1 = M1 - (diff);

                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','','" + diff + "','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','" + CashorBank + "')", con);
                                cmd1974.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        private void bind_account_receivable()
        {
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string CashorBank = "Accounts Receivable";
            string explanation = "Account adjustment for invoice number " + PID2;
            double diff = calculate_balance();

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'", con);
                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                {
                    DataTable dtBrands2322 = new DataTable();
                    sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
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
                            if (diff < 0)
                            {
                                Double M1 = Convert.ToDouble(ah12893);
                                Double bl1 = M1 + (-diff);

                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + -diff + "','','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','" + CashorBank + "')", con);
                                cmd1974.ExecuteNonQuery();
                            }
                            else
                            {
                                Double M1 = Convert.ToDouble(ah12893);
                                Double bl1 = M1 - (diff);

                                SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                                cmd45.ExecuteNonQuery();
                                SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','','" + diff + "','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','" + CashorBank + "')", con);
                                cmd1974.ExecuteNonQuery();
                            }

                        }
                    }
                }
            }
        }
        private void adjustLedger_debit()
        {
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string CashorBank = "";
            if (PaymentMode.InnerText == "Cash") { CashorBank += "Cash on Hand"; }
            else { CashorBank += "Cash at Bank"; }
            string explanation = "Account adjustment for invoice number " + PID2;
            double diff = -bindDifferenece();
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'  ", con);
                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                {
                    DataTable dtBrands2322 = new DataTable();
                    sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
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
                            Double bl1 = M1 + diff;

                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + diff + "','','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','Cash')", con);
                            cmd1974.ExecuteNonQuery();
                        }
                    }
                }
                //Selecting from account prefernce
                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dtBrandss = new DataTable();
                    sdas.Fill(dtBrandss); long iss = dtBrandss.Rows.Count;
                    //Selecting from Income account
                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                    using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                    {
                        DataTable dtBrandss2 = new DataTable();
                        sdas2.Fill(dtBrandss2); long iss2 = dtBrandss2.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmds2.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

                                SqlDataReader reader66 = cmd166.ExecuteReader();

                                if (reader66.Read())
                                {
                                    string ah11;
                                    string ah1258;
                                    ah11 = reader66["No"].ToString();
                                    ah1258 = reader66["AccountType"].ToString();
                                    reader66.Close();
                                    con.Close();
                                    con.Open();
                                    Double M1 = Convert.ToDouble(ah1289);
                                    double income = diff / 1.15;
                                    Double bl1 = M1 + income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                    //Selecting from cash acccount
                    SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                    using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                    {
                        DataTable dttax = new DataTable();
                        sdatax.Fill(dttax); long iss2 = dttax.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmdintax.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                SqlDataReader reader66 = cmdintax.ExecuteReader();

                                if (reader66.Read())
                                {
                                    string ah11;
                                    string ah1258;
                                    ah11 = reader66["No"].ToString();
                                    ah1258 = reader66["AccountType"].ToString();
                                    reader66.Close();
                                    con.Close();
                                    con.Open();
                                    Double M1 = Convert.ToDouble(ah1289);
                                    double vatfree = diff / 1.15;
                                    double income = diff - vatfree;
                                    Double bl1 = M1 + income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','0','" + income + "','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                }
            }

        }

        private void adjustLedger_credit_delete()
        {
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string CashorBank;
            if (PaymentMode.InnerText == "Cash") { CashorBank = "Cash on Hand"; }
            else { CashorBank = "Cash at Bank"; }
            string explanation = "Account adjustment for invoice number " + PID2 + " deleted";
            double diff = Convert.ToDouble(Total.InnerText);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'", con);
                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                {
                    DataTable dtBrands2322 = new DataTable();
                    sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
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
                            Double bl1 = M1 - (diff + bindSC().Item1);

                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','','" + (diff + bindSC().Item1) + "','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','Cash')", con);
                            cmd1974.ExecuteNonQuery();
                        }
                    }
                }
                //Selecting from account prefernce
                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dtBrandss = new DataTable();
                    sdas.Fill(dtBrandss); long iss = dtBrandss.Rows.Count;
                    //Selecting from Income account
                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                    using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                    {
                        DataTable dtBrandss2 = new DataTable();
                        sdas2.Fill(dtBrandss2); long iss2 = dtBrandss2.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmds2.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

                                SqlDataReader reader66 = cmd166.ExecuteReader();

                                if (reader66.Read())
                                {
                                    string ah11;
                                    string ah1258;
                                    ah11 = reader66["No"].ToString();
                                    ah1258 = reader66["AccountType"].ToString();
                                    reader66.Close();
                                    con.Close();
                                    con.Open();
                                    Double M1 = Convert.ToDouble(ah1289);
                                    double income = diff / 1.15;
                                    income = income + bindSC().Item1;
                                    Double bl1 = M1 - income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + income + "','0','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                    //Selecting from cash acccount
                    SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                    using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                    {
                        DataTable dttax = new DataTable();
                        sdatax.Fill(dttax); long iss2 = dttax.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmdintax.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                SqlDataReader reader66 = cmdintax.ExecuteReader();

                                if (reader66.Read())
                                {
                                    string ah11;
                                    string ah1258;
                                    ah11 = reader66["No"].ToString();
                                    ah1258 = reader66["AccountType"].ToString();
                                    reader66.Close();
                                    con.Close();
                                    con.Open();
                                    Double M1 = Convert.ToDouble(ah1289);
                                    double vatfree = diff / 1.15;
                                    double income = diff - vatfree;
                                    Double bl1 = M1 - income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1.ToString("#,##0.00") + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + income.ToString("#,##0.00") + "','0','" + bl1.ToString("#,##0.00") + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                }
            }
        }
        private void bind_account_receivable_when_credited(double diff)
        {
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string CashorBank = "Accounts Receivable";
            string explanation = "Account adjustment for invoice number " + PID2;

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'", con);
                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                {
                    DataTable dtBrands2322 = new DataTable();
                    sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
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
                            Double bl1 = M1 + diff;

                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + diff + "','','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','" + CashorBank + "')", con);
                            cmd1974.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        private void bind_customer_statement_adjustment_deleted()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string explanation = "Account adjustment for invoice number " + PID2 + " deleted!!";
            string reftag = Convert.ToString(RefTag.InnerText).Substring(5);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                double totalpay = bindSC().Item2 - Convert.ToDouble(Total.InnerText);
                if (totalpay != 0)
                {
                    SqlCommand custcmdw = new SqlCommand("delete tblcreditnote  where ref='" + reftag + "'", con);
                    custcmdw.ExecuteNonQuery();
                }
                SqlCommand custcmdw1 = new SqlCommand("delete from tblCustomerStatement where Trans='" + reftag + "'", con);
                custcmdw1.ExecuteNonQuery();
            }
        }

        private void bind_customer_statement_adjustment()
        {
            String PID = Convert.ToString(Request.QueryString["cust"]);
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string explanation = "Account adjustment for invoice number " + PID2;
            string reftag = Convert.ToString(RefTag.InnerText).Substring(5);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand custcmd = new SqlCommand("delete tblCustomerStatement where Trans='" + reftag + "'", con);
                custcmd.ExecuteNonQuery();
                SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + PID + "' ORDER BY CSID DESC", con);

                SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                if (readerbcustb.Read())
                {
                    string ah11;

                    ah11 = readerbcustb["Balance"].ToString();
                    readerbcustb.Close();

                    double totalpay = 0;
                    //if(bindDifferenece() < 0) { totalpay = -bindDifferenece()+bindSC().Item1; }
                    //else { totalpay = -bindDifferenece()-bindSC().Item1; }
                    totalpay = bindSC().Item2 - Convert.ToDouble(txtEditAmount.Text);
                    if (totalpay != 0)
                    {
                        string str2 = "select * from tblcreditnote where ref='" + reftag + "'";
                        com2 = new SqlCommand(str2, con);
                        sqlda2 = new SqlDataAdapter(com2);
                        DataTable dt = new DataTable();
                        sqlda2.Fill(dt);
                        long i = dt.Rows.Count;
                        if (i > 0)
                        {
                            SqlCommand custcmdw = new SqlCommand("update tblcreditnote set balance='" + totalpay + "' where ref='" + reftag + "'", con);
                            custcmdw.ExecuteNonQuery();
                            bind_account_receivable();
                        }
                        else
                        {
                            SqlCommand cmdcrn = new SqlCommand("insert into tblcreditnote values('" + PID + "','" + DateTime.Now + "','" + bindSC().Item2 + "','" + totalpay + "','Credit for rent','" + DateTime.Now + "','" + reftag + "')", con);
                            cmdcrn.ExecuteNonQuery();
                            bind_account_receivable_when_credited(totalpay);
                        }
                    }
                    double new_balance = Convert.ToDouble(ah11) + totalpay;

                    SqlCommand custcmdw1 = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + reftag + "','','" + bindSC().Item2 + "','" + Convert.ToDouble(txtEditAmount.Text) + "','" + new_balance + "','" + PID + "')", con);
                    custcmdw1.ExecuteNonQuery();
                }
            }
        }
        private void adjustLedger_credit()
        {
            String PID2 = Convert.ToString(Request.QueryString["id"]);
            string CashorBank;
            if (PaymentMode.InnerText == "Cash") { CashorBank = "Cash on Hand"; }
            else { CashorBank = "Cash at Bank"; }
            string explanation = "Account adjustment for invoice number " + PID2;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd19012 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + CashorBank + "'", con);
                using (SqlDataAdapter sda2222 = new SqlDataAdapter(cmd19012))
                {
                    DataTable dtBrands2322 = new DataTable();
                    sda2222.Fill(dtBrands2322); long i2 = dtBrands2322.Rows.Count;
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
                            Double bl1 = M1 - bindDifferenece();

                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + CashorBank + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','','" + bindDifferenece() + "','" + bl1 + "','" + DateTime.Now.Date + "','" + CashorBank + "','','Cash')", con);
                            cmd1974.ExecuteNonQuery();
                        }
                    }
                }
                //Selecting from account prefernce
                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    DataTable dtBrandss = new DataTable();
                    sdas.Fill(dtBrandss); long iss = dtBrandss.Rows.Count;
                    //Selecting from Income account
                    SqlCommand cmds2 = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                    using (SqlDataAdapter sdas2 = new SqlDataAdapter(cmds2))
                    {
                        DataTable dtBrandss2 = new DataTable();
                        sdas2.Fill(dtBrandss2); long iss2 = dtBrandss2.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmds2.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][1].ToString() + "'", con);

                                SqlDataReader reader66 = cmd166.ExecuteReader();

                                if (reader66.Read())
                                {
                                    string ah11;
                                    string ah1258;
                                    ah11 = reader66["No"].ToString();
                                    ah1258 = reader66["AccountType"].ToString();
                                    reader66.Close();
                                    con.Close();
                                    con.Open();
                                    Double M1 = Convert.ToDouble(ah1289);
                                    double income = bindDifferenece() / 1.15;
                                    Double bl1 = M1 - income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][1].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + income + "','0','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][1].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                    //Selecting from cash acccount
                    SqlCommand cmdintax = new SqlCommand("select * from tblGeneralLedger2 where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                    using (SqlDataAdapter sdatax = new SqlDataAdapter(cmdintax))
                    {
                        DataTable dttax = new DataTable();
                        sdatax.Fill(dttax); long iss2 = dttax.Rows.Count;
                        //
                        if (iss2 != 0)
                        {
                            SqlDataReader readers = cmdintax.ExecuteReader();
                            if (readers.Read())
                            {
                                string ah1289;
                                ah1289 = readers["Balance"].ToString();
                                readers.Close();
                                con.Close();
                                con.Open();
                                SqlCommand cmd166 = new SqlCommand("select * from tblLedgAccTyp where Name='" + dtBrandss.Rows[0][4].ToString() + "'", con);

                                SqlDataReader reader66 = cmdintax.ExecuteReader();

                                if (reader66.Read())
                                {
                                    string ah11;
                                    string ah1258;
                                    ah11 = reader66["No"].ToString();
                                    ah1258 = reader66["AccountType"].ToString();
                                    reader66.Close();
                                    con.Close();
                                    con.Open();
                                    Double M1 = Convert.ToDouble(ah1289);
                                    double vatfree = bindDifferenece() / 1.15;
                                    double income = bindDifferenece() - vatfree;
                                    Double bl1 = M1 - income;
                                    SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + dtBrandss.Rows[0][4].ToString() + "'", con);
                                    cmd45.ExecuteNonQuery();
                                    SqlCommand cmd1974 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + income + "','0','" + bl1 + "','" + DateTime.Now + "','" + dtBrandss.Rows[0][4].ToString() + "','" + ah11 + "','" + ah1258 + "')", con);
                                    cmd1974.ExecuteNonQuery();

                                }
                            }
                        }
                    }
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["id"]);
            String PID2 = Convert.ToString(Request.QueryString["cust"]);
            if (txtEditAmount.Text == "")
            {
                string message = "Please assign the total amount!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    double total = Convert.ToDouble(txtEditAmount.Text);

                    SqlCommand cmdreb1 = new SqlCommand("Update tblrentreceipt set paid='" + total + "' where id2='" + PID + "' and customer='" + PID2 + "'", con);
                    cmdreb1.ExecuteNonQuery();
                    string url = "rentinvoicereport.aspx?id=" + PID + "&&cust=" + PID2;
                    string exlanation = "Invoice# " + PID + " has been updated from ETB " + Convert.ToDouble(Total.InnerText).ToString("#,##0.00") + " to " + total.ToString("#,##0.00");
                    if (Checkbox2.Checked == true)
                    {
                        bind_customer_statement_adjustment();
                        double diff = bindDifferenece();
                        if (diff > 0)
                        {
                            adjustLedger_credit();
                        }
                        else if (diff < 0)
                        {
                            adjustLedger_debit();
                        }
                        else
                        {

                        }
                    }
                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-edit text-white','icon-circle bg bg-primary','" + url + "','MN')", con);
                    cmd197h.ExecuteNonQuery();

                    Response.Redirect("rentinvoicereport.aspx?id=" + PID + "&&cust=" + PID2);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string reftag = Convert.ToString(RefTag.InnerText).Substring(5);
            String PID = Convert.ToString(Request.QueryString["id"]);
            String PID2 = Convert.ToString(Request.QueryString["cust"]);
            String PID3 = Convert.ToString(Request.QueryString["paymentmode"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

                if (deleteCheck.Checked == true)
                {
                    adjustLedger_credit_delete();
                    bind_account_receivable_remove();
                    bind_customer_statement_adjustment_deleted();
                    SqlCommand custcmdw = new SqlCommand("delete tblcreditnote  where ref='" + reftag + "'", con);
                    custcmdw.ExecuteNonQuery();
                }
                SqlCommand cmdreb1 = new SqlCommand("delete tblrentreceipt  where id2='" + PID + "' and customer='" + PID2 + "'", con);
                cmdreb1.ExecuteNonQuery();
                string url = "rentinvoicereport.aspx?id=" + PID + "&&cust=" + PID2 + "&&paymentmode=" + PID3;
                string exlanation = "Invoice# " + PID + " has been deleted";

                SqlCommand cmdd = new SqlCommand("delete tblNotification where query='" + url + "'", con);
                cmdd.ExecuteNonQuery();
                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-trash text-white','icon-circle bg bg-danger','rentinvoicereport.aspx','MN')", con);
                cmd197h.ExecuteNonQuery();

                Response.Redirect("rentinvoicereport.aspx");
            }
        }
        protected void bind_footer()
        {
            string fs_style = ""; string lh_style = ""; string lh_style_body = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string fs; string lh; string fontsizebody; string margin; string logosize; string invoicename;
                    invoicename = reader["invoice_name"].ToString();
                    fs = reader["invheaderfs"].ToString();
                    lh = reader["invheaderlh"].ToString();
                    fontsizebody = reader["invbodyfs"].ToString();
                    logosize = reader["invlogosize"].ToString();
                    margin = reader["margin"].ToString();
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    lh_style_body += " font-size:" + fontsizebody + "px";
                    txtFontsize.Text = fs;
                    txtLineHeight.Text = lh;
                    txtBodyFontSize.Text = fontsizebody;
                    txtMarigin.Text = margin;
                    txtLogoSize.Text = logosize;
                    HeaderInv.Style.Add("font-size", fs + "px");
                    txtInvoiceName.Text = invoicename.ToUpper();
                    HeaderInv.InnerText = invoicename.ToUpper();
                    HeaderInv.Style.Add("line-height", lh + "px");
                    HeaderInvDup.InnerText = invoicename;

                    HeaderRaks.Style.Add("font-size", fs + "px");
                    HeaderRaks.Style.Add("line-height", lh + "px");
                    MarginDiv.Style.Add("margin-left", margin + "px");
                    MarginDiv.Style.Add("margin-right", margin + "px");

                    Body1.Style.Add("font-size", fontsizebody + "px");
                    Body2.Style.Add("font-size", fontsizebody + "px");
                    conw.Style.Add("font-size", fontsizebody + "px");

                    //For duplicate
                    HeaderInvDup.Style.Add("font-size", fs + "px");

                    HeaderInvDup.Style.Add("line-height", lh + "px");
                    LogoImage.Style.Add("width", logosize + "px");
                    LogoImage.Style.Add("height", logosize + "px");
                    LogoImage1.Style.Add("width", logosize + "px");
                    LogoImage1.Style.Add("height", logosize + "px");
                    HeaderRaksDup.Style.Add("font-size", fs + "px");
                    HeaderRaksDup.Style.Add("line-height", lh + "px");
                    DuplicateRow.Style.Add("margin-left", margin + "px");
                    DuplicateRow.Style.Add("margin-right", margin + "px");

                    body1dup.Style.Add("font-size", fontsizebody + "px");
                    body2dup.Style.Add("font-size", fontsizebody + "px");
                    conx.Style.Add("font-size", fontsizebody + "px");

                }
            }
        }
        protected void bind_logo_visibility()
        {
            string text = ""; string credit = ""; string water = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text = reader["visibility"].ToString();
                    credit = reader["credit_balance_visibility"].ToString();
                    water = reader["watermark"].ToString();
                    //Logo
                    if (text == "True") { LogoImage.Visible = true; logoCheck.Checked = true; }
                    else { LogoImage.Visible = false; logoCheck.Checked = false; }
                    //Credit Div
                    if (credit == "True") { CreditDiv2.Visible = true; CreditDiv.Visible = true; creditCheck.Checked = true; }
                    else { CreditDiv2.Visible = false; CreditDiv.Visible = false; creditCheck.Checked = false; }
                    //WaterMark Div
                    if (water == "True") { RaksTDiv.Visible = true; RaksTDiv2.Visible = true; waterCheck.Checked = true; }
                    else { RaksTDiv.Visible = false; RaksTDiv2.Visible = false; waterCheck.Checked = false; }
                }
            }
        }
        private bool return_checkbox_logo_visibility()
        {
            bool logic = false;
            if (logoCheck.Checked == true)
            {
                logic = true;
            }
            return logic;
        }
        private bool return_checkbox_credit_visibility()
        {
            bool logic = false;
            if (creditCheck.Checked == true)
            {
                logic = true;
            }
            return logic;
        }
        private bool return_checkbox_water_visibility()
        {
            bool logic = false;
            if (waterCheck.Checked == true)
            {
                logic = true;
            }
            return logic;
        }
        protected void btnCustomizeSave_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["id"]);
            String PID2 = Convert.ToString(Request.QueryString["cust"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdf = new SqlCommand("update tblCustomizeLetterMarigin set invheaderfs='" + txtFontsize.Text + "',invheaderlh='" + txtLineHeight.Text + "',invbodyfs='" + txtBodyFontSize.Text + "'" +
                    ",invlogosize='" + txtLogoSize.Text + "',visibility='" + return_checkbox_logo_visibility() + "'" +
                    ",margin='" + txtMarigin.Text + "',invoice_name='" + txtInvoiceName.Text + "',credit_balance_visibility='" + return_checkbox_credit_visibility() + "',watermark='" + return_checkbox_water_visibility() + "'", con);
                cmdf.ExecuteNonQuery();
                Response.Redirect("rentinvoicereport.aspx?id=" + PID + "&&cust=" + PID2);
            }
        }
        protected void vtnDeleteAll_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdreb1 = new SqlCommand("delete tblrentreceipt", con);
                cmdreb1.ExecuteNonQuery();
                Response.Redirect("rentinvoicereport.aspx");
            }
        }
    }
}
