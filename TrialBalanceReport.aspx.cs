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

namespace advtech.Finance.Accounta
{
    public partial class TrialBalanceReport : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str; string str2;
        SqlCommand com;
#pragma warning disable CS0169 // The field 'TrialBalanceReport.com1' is never used
        SqlCommand com1;
#pragma warning restore CS0169 // The field 'TrialBalanceReport.com1' is never used
        SqlCommand com2;
        SqlCommand com3;
        SqlDataAdapter sqlda;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    BindDebit(); BindCredit(); BindDebit1(); BindCredit1(); bindcompany();
                    datTo.Visible = false; datFrom1.Visible = false; tomiddle.Visible = false;
                    mont.InnerText = "As of " + DateTime.Now.ToString("MMMM dd, yyyy");
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindcompany()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Oname,BuissnessLocation,Contact from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company;
                    company = reader["Oname"].ToString();
                    oname.InnerText = company;
                }
            }
        }
        private void BindDebit1()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select (sum(Debit)-Sum(Credit)) as Balance from tblGeneralLedger where AccountType='Cash' or AccountType='Accounts Receivable' or AccountType='Inventory' or AccountType='Cost of Sales' or  AccountType='Expenses' or  AccountType='Fixed Assets'    or AccountType='Other Current Assets'  or AccountType='Other Assets'";
            com = new SqlCommand(str, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                string pin;
                pin = reader["Balance"].ToString();
                H1.InnerText = Convert.ToDouble(pin).ToString("#,##0.00");
            }
        }
        private void BindDebit()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date), (sum(Debit)-Sum(Credit)) as Balance,Account,AccountType from tblGeneralLedger where AccountType='Cash' or  AccountType='Accounts Receivable' or AccountType='Inventory' or  AccountType='Cost of Sales' or AccountType='Expenses' or AccountType='Fixed Assets'  or  AccountType='Other Current Assets'  or AccountType='Other Assets' group by year(Date),Account,AccountType";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
        }
        private void BindCredit1()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string q1 = "select (SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where  AccountType='Accounts Payable' or AccountType='Other Current Liabilities' or AccountType='Long Term Liabilities' or AccountType='Accumulated Depreciation' or";
            string q2 = "year(Date) ='" + DateTime.Now.Year + "' and AccountType='Income' or";
            string q3 = "year(Date) ='" + DateTime.Now.Year + "' and AccountType='Equity'";
            str2 = q1 + " " + q2 + " " + q3;
            com = new SqlCommand(str2, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                string pin;
                pin = reader["Balance"].ToString();
                Label2.InnerText = Convert.ToDouble(pin).ToString("#,##0.00");
            }
        }
        private void BindCredit()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string q1 = "select year(Date),(SUM(Credit)-SUM(Debit)) as Balance,Account from tblGeneralLedger where year(Date)= '" + DateTime.Now.Year + "' and AccountType='Accounts Payable' or year(Date)= '" + DateTime.Now.Year + "' and AccountType='Other Current Liabilities' or year(Date)= '" + DateTime.Now.Year + "' and AccountType='Long Term Liabilities' or year(Date)= '" + DateTime.Now.Year + "' and AccountType='Accumulated Depreciation' or";
            string q2 = "year(Date) ='" + DateTime.Now.Year + "' and AccountType='Income' or";
            string q3 = "year(Date) ='" + DateTime.Now.Year + "' and AccountType='Equity'    group by year(Date),Account,AccountType";
            str2 = q1 + " " + q2 + " " + q3;
            com = new SqlCommand(str2, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater2.DataSource = dtBrands;
                Repeater2.DataBind();
            }
        }
        private string BindDate1()
        {
            string FirstDate = "";

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select TOP (1) *from tblGeneralLedger  ORDER BY LedID ASC", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                        if (i != 0)
                        {
                            FirstDate = Convert.ToDateTime(dtBrands.Rows[0][6].ToString()).ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            return FirstDate;
        }
        private double CalculateEndingBalance1(string Account)
        {
            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindDate1());
            str = "select* from tblGeneralLedger where Account='" + Account + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Pbalance = Convert.ToDouble(dt.Rows[0][5].ToString());
            }

            return Pbalance;
        }
        private string BindDate(string account)
        {
            string FirstDate = "";

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select TOP (1) *from tblGeneralLedger where Account='" + account + "' ORDER BY LedID ASC", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                        if (i != 0)
                        {
                            FirstDate = Convert.ToDateTime(dtBrands.Rows[0][6].ToString()).ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            return FirstDate;
        }
        private double CalculateEndingBalance(string Account)
        {
            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindDate(Account));
            str = "select* from tblGeneralLedger where Account='" + Account + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Pbalance = Convert.ToDouble(dt.Rows[0][5].ToString());
            }

            return Pbalance;
        }
        //Calculating the total previous balance for all account
        private string BindPDate()
        {
            string FirstDate = "";

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select TOP (1) *from tblGeneralLedger ORDER BY LedID ASC", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                        if (i != 0)
                        {
                            FirstDate = Convert.ToDateTime(dtBrands.Rows[0][6].ToString()).ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            return FirstDate;
        }
        private double CalculateOverallBalanceCredit()
        {

            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindDate1());
            string q11 = "select Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Payable' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Liabilities' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Long Term Liabilities' or ";
            string q21 = "Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Income' or ";
            string q31 = "Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Equity'  or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accumulated Depreciation' group by Account";
            string str2r = q11 + " " + q21 + " " + q31;

            com = new SqlCommand(str2r, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP 1* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }

            }
            return Pbalance;
        }
        private double CalculateOverallBalanceDebit()
        {
            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindDate1());
            str = "select Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType = 'Cash' or  Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType = 'Accounts Receivable' or  Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType = 'Inventory' or  Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType = 'Cost of Sales' or  Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType = 'Expenses'  OR AccountType = 'Fixed Assets' and  Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'  OR AccountType = 'Other Assets' and  Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' OR AccountType = 'Other Current Assets' and  Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' group by Account";

            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP 1 * from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

                    SqlCommand com1 = new SqlCommand(str2, con);
                    SqlDataAdapter sqlda1 = new SqlDataAdapter(com1);

                    DataTable dt1 = new DataTable();
                    sqlda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        Pbalance = Pbalance + Convert.ToDouble(dt1.Rows[0][5].ToString());
                    }
                }
            }
            return Pbalance;
        }
        //
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDatefrom.Text == "" || txtDateto.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                datTo.Visible = true; datFrom1.Visible = true; tomiddle.Visible = true;
                datFrom1.InnerText = Convert.ToDateTime(txtDatefrom.Text).ToString("dd MMM yyyy"); datTo.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("dd MMM yyyy");
                mont.Visible = false; ;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select  (sum(Debit)-Sum(Credit)) as Balance from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Cash' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Receivable' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Inventory' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Cost of Sales' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Expenses'  OR AccountType='Fixed Assets' and Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'  OR AccountType='Other Assets' and Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' OR AccountType='Other Current Assets' and Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'";
                com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    string pin;
                    pin = reader["Balance"].ToString();
                    if (pin == "" || pin == null)
                    {
                        H1.InnerText = "0.00";
                    }
                    else
                    {
                        H1.InnerText = (Convert.ToDouble(pin) + CalculateOverallBalanceDebit()).ToString("#,##0.00");
                    }

                }
                reader.Close();
                string strr = "select (sum(Debit)-Sum(Credit)) as Balance,Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Cash' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Receivable' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Inventory' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Cost of Sales' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Expenses' OR AccountType='Fixed Assets' and Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "'  OR AccountType='Other Assets' and Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' OR AccountType='Other Current Assets' and Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' group by Account";
                SqlCommand cbn = new SqlCommand(strr, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cbn))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands);
                    Repeater1.DataSource = dtBrands;
                    Repeater1.DataBind();
                }
                string q1 = "select (SUM(Credit)-SUM(Debit)) as Balance from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Payable' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Liabilities' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Long Term Liabilities' or";
                string q2 = "Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Income' or";
                string q3 = "Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Equity' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accumulated Depreciation'";
                string bstr2 = q1 + " " + q2 + " " + q3;
                com2 = new SqlCommand(bstr2, con);
                SqlDataReader reader1 = com2.ExecuteReader();
                if (reader1.Read())
                {
                    string pin;
                    pin = reader1["Balance"].ToString();
                    if (pin == "" || pin == null)
                    {
                        Label2.InnerText = "0.00";
                    }
                    else
                    {
                        Label2.InnerText = (Convert.ToDouble(pin) + CalculateOverallBalanceCredit()).ToString("#,##0.00");
                    }
                }
                reader1.Close();
                string q11 = "select (SUM(Credit)-SUM(Debit)) as Balance,Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accounts Payable' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Other Current Liabilities' or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Long Term Liabilities' or";
                string q21 = "Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Income' or";
                string q31 = "Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Equity'  or Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Accumulated Depreciation'  group by Account,AccountType";
                string str2r = q11 + " " + q21 + " " + q31;
                com3 = new SqlCommand(str2r, con);
                using (SqlDataAdapter sda1 = new SqlDataAdapter(com3))
                {
                    DataTable dtBrands = new DataTable();
                    sda1.Fill(dtBrands);
                    Repeater2.DataSource = dtBrands;
                    Repeater2.DataBind();
                }
                BindTotalBalance(); BindTotalBalance1();
            }
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Rent_Status_Raksym_Trading" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);


            con.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }
        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    con.RenderControl(hw);
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
                        mailMessage.Body = txtBody.Text;
                        mailMessage.IsBodyHtml = false;
                        // Specify the email Subject
                        mailMessage.Subject = txtSubjuct.Text;
                        mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), "Trial_Balance.pdf"));
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
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void BindTotalBalance()
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("Label1") as Label;
                    Label balance = item.FindControl("Label3") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
        private void BindTotalBalance1()
        {
            foreach (RepeaterItem item in Repeater2.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label account = item.FindControl("Label4") as Label;
                    Label balance = item.FindControl("Label5") as Label;
                    if (txtDatefrom.Text != "" || txtDateto.Text != "")
                    {
                        double b1 = Convert.ToDouble(balance.Text) + CalculateEndingBalance1(account.Text);
                        balance.Text = b1.ToString("#,##0.00");
                    }
                }
            }
        }
    }
}