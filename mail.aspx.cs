using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using Twilio;
using System.Web.UI.WebControls;
using Twilio.Rest.Api.V2010.Account;
using System.Security.Cryptography;

namespace advtech.Finance.Accounta
{
    public partial class mail : System.Web.UI.Page
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
                BindBrandsRptr();
            }
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("HHmmss");
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            using (StreamReader reader = new StreamReader(Server.MapPath("~/Finance/Accounta/HtmlPage4.html")))
            {
                string body = string.Empty;
                body = reader.ReadToEnd();
                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", "abellegese5@gmail.com");
                // Specify the email body
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                // Specify the email Subject
                mailMessage.Subject = "Exception";

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

        protected void Button2_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblcreditnote where DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) = 7 and balance > 0", con);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    if (i != 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            SqlCommand cmd41 = new SqlCommand("select * from tblCustomers where FllName='" + dt.Rows[j][1].ToString() + "'", con);
                            SqlDataAdapter sda1 = new SqlDataAdapter(cmd41);
                            DataTable dt1 = new DataTable();
                            sda1.Fill(dt1); int i1 = dt1.Rows.Count;
                            string message2 = "Customer, " + dt.Rows[j][1].ToString() + " Credit due date remains 7 days!";
                            string url = "creditnotedetails.aspx?ref2=" + dt.Rows[j][0].ToString() + "&&" + "cust=" + dt.Rows[j][1].ToString();
                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + message2 + "','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','" + url + "','Acc')", con);
                            cmd197h.ExecuteNonQuery();
                            SqlCommand cmde = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + message2 + "','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-warning','" + url + "','MN')", con);
                            cmde.ExecuteNonQuery();
                            string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                            string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                            TwilioClient.Init(accountSid, authToken);

                            var message = MessageResource.Create(
                                body: "Dear customer, your credit due date remains 7 days. for more info, visit your portal.",
                                from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                to: new Twilio.Types.PhoneNumber("+251" + dt1.Rows[j][6].ToString())
                            );
                            //Email automation

                            string emailbody = "Dear " + dt.Rows[j][1].ToString() + " Your rent payment remains 10 Days please pay before the due date. For more info visit our portal: https://localhost:44357/Customer_Login/LogIn1.aspx";
                            MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", dt1.Rows[0][4].ToString());
                            // Specify the email body
                            mailMessage.Body = emailbody;
                            mailMessage.IsBodyHtml = true;
                            // Specify the email Subject
                            mailMessage.Subject = "Payment Days Announcement";

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
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblGeneralLedger", con);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    if (i != 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            double balance = Convert.ToDouble(dt.Rows[j][5].ToString());

                            SqlCommand cmdre = new SqlCommand("Update tblGeneralLedger2 set Balance ='" + balance + "' where Account='" + dt.Rows[j][7].ToString() + "'", con);
                            cmdre.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            String CS2 = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString2"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                using (SqlConnection con1 = new SqlConnection(CS2))
                {

                    SqlCommand cmd = new SqlCommand("select*from tblrent", con);

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            con1.Open();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {


                                SqlCommand cmdre = new SqlCommand("Update tblrent set buisnesstype =N'" + dt.Rows[j][3].ToString() + "',customer_amharic =N'" + dt.Rows[j][12].ToString() + "' where customer='" + dt.Rows[j][2].ToString() + "'", con1);
                                cmdre.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        private void BindBrandsRptr()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblGeneralLedger";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            con.Close();
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=SqlExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(counter.InnerText);
            Response.Flush();
            Response.End();
        }
        protected double bindLedgerValues(string account)
        {
            double values = 0;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcrd = new SqlCommand("select * from tblGeneralLedger where Account='" +account + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {
                    
                    double debit = Convert.ToDouble(readercrd["Debit"].ToString());
                    double credit = Convert.ToDouble(readercrd["Credit"].ToString());
                    string accountType = readercrd["AccountType"].ToString();
                    if (debit != 0)
                    {
                        values = debit;
                    }
                    else
                    {
                        values = -credit;
                    }
                }
                readercrd.Close();
            }
            return values;
        }
        private string GenerateCSV(string amount)
        {
            string csv = string.Empty;

            csv += " Date,Reference,Date Clear in Bank Rec,Number of Distributions,G/L Account,Description,Amount,Job ID,Used for Reimbursable Expenses,Transaction Period,Transaction Number,Consolidated Transaction,Recur Number,Recur Frequency";
            csv += "\r\n";
            csv += amount;
            return csv;
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                string csvValues = "";
                string amountCollector = string.Empty;
                string dateCollector = string.Empty;
                string descriptionCollector = string.Empty;
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    
                    Label Account = item.FindControl("lblID") as Label;
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        Label Amount = item.FindControl("lblAmount") as Label;
                        SqlCommand cmdcrd = new SqlCommand("select * from tblGeneralLedger where LedID LIKE '%" + Account.Text + "%' order by LedID asc", con);
                        SqlDataReader readercrd = cmdcrd.ExecuteReader();
                        if (readercrd.Read())
                        {
                            string date = readercrd["Date"].ToString();
                            string description = readercrd["Explanation"].ToString();
                            double debit = Convert.ToDouble(readercrd["Debit"].ToString());
                            double credit = Convert.ToDouble(readercrd["Credit"].ToString());
                            string accountType = readercrd["AccountType"].ToString();
                            if (debit != 0)
                            {
                                Amount.Text = debit.ToString();
                                csvValues += date + ',' + "" + ',' + "" + ',' + "2" + ',' + "1000" + ',' + description + ',' + Amount.Text + ',' + "" + ',' + "FALSE" + ',' + "" + ',' + "FALSE" + ',' + "0" + ',' + "0" + "\r\n";
                            }
                            else
                            {
                                Amount.Text = (-credit).ToString();
                                csvValues += date + ',' + "" + ',' + "" + ',' + "2" + ',' + "1000" + ',' + description + ',' + Amount.Text + ',' + "" + ',' + "FALSE" + ',' + "" + ',' + "FALSE" + ',' + "0" + ',' + "0" + "\r\n";

                            }
                        }
                        readercrd.Close();
                    }
                }
                con.Close();
                counter.InnerText =GenerateCSV(csvValues);
            }
        }
    }
}