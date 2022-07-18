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
    public partial class IncomeStatementReport : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                datTo.Visible = false; datFrom1.Visible = false; tomiddle.Visible = false;
                BindIncomSData();
                BindIncomSData3(); bindcompany(); profit();
                //BindNetIncome();
                mont.InnerText = "As of " + DateTime.Now.ToString("MMMM dd, yyyy");
            }
        }
        private void profit()
        {
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            String query = "select (SUM(Credit)-SUM(Debit)) as Balance  from tblGeneralLedger where AccountType='Income' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string income;
                income = reader["Balance"].ToString();
                TotRevenue.InnerText = Convert.ToDouble(income).ToString("#,##0.00");
                reader.Close();

                con.Close();
                String query1 = "select (SUM(Debit)-SUM(Credit)) as Balance from tblGeneralLedger where AccountType='Expenses' or AccountType='Cost of Sales'";
                con.Open();
                SqlCommand cmd1 = new SqlCommand(query1, con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {
                    string expense;
                    expense = reader1["Balance"].ToString();
                    TotExpense.InnerText = Convert.ToDouble(expense).ToString("#,##0.00");
                    reader1.Close();
                    con.Close();


                    double nw = Convert.ToDouble(income) - Convert.ToDouble(expense);

                    if (nw < 0)
                    {
                        nw = -nw;
                        Label2.InnerText = "(ETB " + nw.ToString("#,##0.00") + ")";
                        Label2.Attributes.Add("class", "text-danger text-lg-right");
                        NetText.Attributes.Add("class", "font-weight-bold  text-uppercase text-danger");
                        NetText.InnerText = "NET LOSS";
                    }
                    else
                    {
                        Label2.InnerText = nw.ToString("#,##0.00");
                        Label2.Attributes.Add("class", "text-dark text-lg-right font-weight-bold");
                        NetText.Attributes.Add("class", "font-weight-bold text-gray-900  text-uppercase");
                        NetText.InnerText = "NET PROFIT";
                    }
                }
            }
        }
        private string BindDate1(string account)
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
        private double CalculateEndingBalance1(string Account)
        {
            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindDate1(Account));
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
        private double CalculateOverallBalanceIncome()
        {
            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            str = "select Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Income' group by Account";

            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = "select TOP(1)* from tblGeneralLedger where Account='" + dt.Rows[i][0].ToString() + "' and Date between '" + dTimeFirst + "' and '" + dTimeLast + "' ORDER BY LedID DESC";

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
        private double CalculateOverallBalanceExpense()
        {
            double Pbalance = 0;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            DateTime dTimeLast = Convert.ToDateTime(txtDatefrom.Text).AddDays(-1);
            DateTime dTimeFirst = Convert.ToDateTime(BindPDate());
            str = "select Account from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Expenses'";

            com = new SqlCommand(str, con);
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
        //
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
        private void BindIncomSData()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),sum(Credit)-sum(Debit) as Balance, Account,AccountType   from tblGeneralLedger where AccountType='Income'  group by year(Date), Account,AccountType ORDER BY ACCOUNT DESC";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
        }
        private void BindIncomSData3()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select year(Date),sum(Debit)-sum(Credit) as Balance, Account,AccountType   from tblGeneralLedger where AccountType='Expenses'  group by year(Date), Account,AccountType";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater3.DataSource = dtBrands;
                Repeater3.DataBind();
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
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);


            con.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

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
                str = "select sum(Credit)-sum(Debit) as Balance, Account,AccountType   from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Income' group by Account,AccountType";
                com = new SqlCommand(str, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(com))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands);
                    Repeater1.DataSource = dtBrands;
                    Repeater1.DataBind();
                }
                //
                //
                str = "select sum(Debit)-sum(Credit) as Balance, Account,AccountType   from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Expenses' group by  Account,AccountType";
                com = new SqlCommand(str, con);
                using (SqlDataAdapter sda = new SqlDataAdapter(com))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands);
                    Repeater3.DataSource = dtBrands;
                    Repeater3.DataBind();
                }
                //
                String query = "select (SUM(Credit)-SUM(Debit)) as Balance  from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Income'";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string income; double incomesssssss; income = reader["Balance"].ToString();
                    if (income == null || income == "")
                    {
                        incomesssssss = CalculateOverallBalanceIncome();
                        TotRevenue.InnerText = incomesssssss.ToString("#,##0.00");
                    }
                    else
                    {
                        incomesssssss = Convert.ToDouble(income) + CalculateOverallBalanceIncome();
                        TotRevenue.InnerText = incomesssssss.ToString("#,##0.00");
                    }

                    reader.Close();
                    con.Close();
                    String query1 = "select (SUM(Debit)-SUM(Credit)) as Balance from tblGeneralLedger where Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Expenses' OR Date between '" + txtDatefrom.Text + "' and '" + txtDateto.Text + "' and AccountType='Cost of Sales'";
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    if (reader1.Read())
                    {
                        string expense; double expensesssssss;
                        expense = reader1["Balance"].ToString();
                        if (expense == null || expense == "")
                        {
                            expensesssssss = CalculateOverallBalanceExpense();
                            TotExpense.InnerText = expensesssssss.ToString("#,##0.00");
                        }
                        else
                        {
                            expensesssssss = Convert.ToDouble(expense) + CalculateOverallBalanceExpense();
                            TotExpense.InnerText = expensesssssss.ToString("#,##0.00");
                        }
                        reader1.Close();
                        con.Close();

                        double nw = incomesssssss - expensesssssss;
                        if (nw < 0)
                        {
                            nw = -nw;
                            Label2.InnerText = "(ETB " + nw.ToString("#,##0.00") + ")";
                            Label2.Attributes.Add("class", "text-danger text-lg-right");
                            NetText.Attributes.Add("class", "font-weight-bold  text-uppercase text-danger");
                            NetText.InnerText = "NET LOSS";
                        }
                        else
                        {
                            Label2.InnerText = nw.ToString("#,##0.00");
                            Label2.Attributes.Add("class", "text-gray-900 text-lg-right font-weight-bold");
                            NetText.Attributes.Add("class", "font-weight-bold text-gray-900  text-uppercase text-dark");
                            NetText.InnerText = "NET PROFIT";
                        }
                    }
                }
                BindTotalBalance1(); BindTotalBalance();
            }
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
            foreach (RepeaterItem item in Repeater3.Items)
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
                        mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), "Income_Statement.pdf"));
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
                    }
                }
            }
        }
    }
}