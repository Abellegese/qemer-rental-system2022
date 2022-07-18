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
        protected void Button5_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblCustomers ", con);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        SqlCommand cmdrent = new SqlCommand("Update tblCustomers set FllName='" + dt.Rows[j]["FllName"].ToString().TrimEnd() + "' where FllName='" + dt.Rows[j]["FllName"].ToString() + "'", con);
                        cmdrent.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}