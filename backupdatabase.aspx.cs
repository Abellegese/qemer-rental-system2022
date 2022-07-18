using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using Hangfire;
using Hangfire.SqlServer;
using System.Net;
using System.Net.Mail;
using System.IO;



namespace advtech.Finance.Accounta
{
    public partial class backupdatabase : System.Web.UI.Page
    {
        public static string getDirectory = string.Empty;
        public static string emailAddress = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                emailAddress = EmailAdd.Text;
                getDirectory = Server.MapPath("~/Finance/Accounta/backup/raksym_database.bak");

            }
        }
        [WebMethod]
        public static string bindEmail()
        {
            string email = string.Empty;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + System.Web.HttpContext.Current.Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    email = readerAC["Email"].ToString();
                    readerAC.Close();
                }
            }
            return email;
        }
        [WebMethod]
        public static void UpdateAutomationPeriod(string period)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("update tblAutomaticBackupOption set period='" + period + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                if (period == "Daily")
                {
                    JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                    RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupDaily", x => x.IntitiateAutoBackUps(), Cron.Daily());
                    RecurringJob.RemoveIfExists("AutoBackupWeekly");
                    RecurringJob.RemoveIfExists("AutoBackupMonthly");
                    RecurringJob.RemoveIfExists("AutoBackupYearly");
                    RecurringJob.RemoveIfExists("AutoBackupMinutely");
                }
                else if (period == "Minutely")
                {
                    JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                    RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupMinutely", x => x.IntitiateAutoBackUps(), Cron.Minutely());
                    RecurringJob.RemoveIfExists("AutoBackupDaily");
                    RecurringJob.RemoveIfExists("AutoBackupMonthly");
                    RecurringJob.RemoveIfExists("AutoBackupYearly");
                    RecurringJob.RemoveIfExists("AutoBackupWeekly");
                }
                else if (period == "Weekly")
                {
                    JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                    RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupWeekly", x => x.IntitiateAutoBackUps(), Cron.Weekly());
                    RecurringJob.RemoveIfExists("AutoBackupDaily");
                    RecurringJob.RemoveIfExists("AutoBackupMonthly");
                    RecurringJob.RemoveIfExists("AutoBackupYearly");
                    RecurringJob.RemoveIfExists("AutoBackupMinutely");
                }
                else if (period == "Monthly")
                {
                    JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                    RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupMonthly", x => x.IntitiateAutoBackUps(), Cron.Monthly);
                    RecurringJob.RemoveIfExists("AutoBackupDaily");
                    RecurringJob.RemoveIfExists("AutoBackupWeekly");
                    RecurringJob.RemoveIfExists("AutoBackupYearly");
                    RecurringJob.RemoveIfExists("AutoBackupMinutely");
                }
                else
                {
                    JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                    RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupYearly", x => x.IntitiateAutoBackUps(), Cron.Yearly);
                    RecurringJob.RemoveIfExists("AutoBackupDaily");
                    RecurringJob.RemoveIfExists("AutoBackupWeekly");
                    RecurringJob.RemoveIfExists("AutoBackupMonthly");
                    RecurringJob.RemoveIfExists("AutoBackupMinutely");
                }
            }
        }
        public class Period
        {
            public string Per { get; set; }
        }
        [WebMethod]
        public static void UpdateAutomationStatus()
        {
            string isTurnedOn = string.Empty;
            if (bindBackupInfoCheck() == "On")
            {
                isTurnedOn = "Off";
                RecurringJob.RemoveIfExists("AutoBackupDaily");
                RecurringJob.RemoveIfExists("AutoBackupWeekly");
                RecurringJob.RemoveIfExists("AutoBackupMonthly");
                RecurringJob.RemoveIfExists("AutoBackupYearly");
                RecurringJob.RemoveIfExists("AutoBackupMinutely");
            }
            else
            {
                backupdatabase BackupDatabase = new backupdatabase();
                isTurnedOn = "On"; BackupDatabase.IntiateAutomation();

            }
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                SqlCommand cmd = new SqlCommand("update tblAutomaticBackupOption set IsTurnedOn='" + isTurnedOn + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        [WebMethod]
        public static string bindBackupInfoCheck()
        {
            string backInfo = string.Empty;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //
                SqlCommand cmd = new SqlCommand("select * from tblAutomaticBackupOption", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                backInfo = dt.Rows[0]["IsTurnedOn"].ToString();
            }
            return backInfo;
        }
        [WebMethod]
        public static string  bindBackupInfo()
        {
            string backInfo = string.Empty;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //
                SqlCommand cmd = new SqlCommand("select * from tblAutomaticBackupOption", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                backInfo = dt.Rows[0]["period"].ToString();
            }
            return backInfo;
        }
        [WebMethod]
        public string GetPeriodValues()
        {
            string periodText = string.Empty;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //
                SqlCommand cmd = new SqlCommand("select * from tblAutomaticBackupOption", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                periodText = dt.Rows[0][0].ToString();
            }
            return periodText;
        }
        public void IntiateAutomation()
        {
            backupdatabase PeriodValues = new backupdatabase();
            string period = PeriodValues.GetPeriodValues();
            if (period == "Daily")
            {
                JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupDaily", x => x.IntitiateAutoBackUps(), Cron.Daily());
                RecurringJob.RemoveIfExists("AutoBackupWeekly");
                RecurringJob.RemoveIfExists("AutoBackupMonthly");
                RecurringJob.RemoveIfExists("AutoBackupYearly");
                RecurringJob.RemoveIfExists("AutoBackupMinutely");
            }
            else if (period == "Minutely")
            {
                JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupMinutely", x => x.IntitiateAutoBackUps(), Cron.Minutely());
                RecurringJob.RemoveIfExists("AutoBackupDaily");
                RecurringJob.RemoveIfExists("AutoBackupMonthly");
                RecurringJob.RemoveIfExists("AutoBackupYearly");
                RecurringJob.RemoveIfExists("AutoBackupWeekly");
            }
            else if(period == "Weekly")
            {
                JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupWeekly", x => x.IntitiateAutoBackUps(), Cron.Weekly());
                RecurringJob.RemoveIfExists("AutoBackupDaily");
                RecurringJob.RemoveIfExists("AutoBackupMonthly");
                RecurringJob.RemoveIfExists("AutoBackupYearly");
                RecurringJob.RemoveIfExists("AutoBackupMinutely");
            }
            else if (period == "Monthly")
            {
                JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupMonthly", x => x.IntitiateAutoBackUps(), Cron.Monthly);
                RecurringJob.RemoveIfExists("AutoBackupDaily");
                RecurringJob.RemoveIfExists("AutoBackupWeekly");
                RecurringJob.RemoveIfExists("AutoBackupYearly");
                RecurringJob.RemoveIfExists("AutoBackupMinutely");
            }
            else
            {
                JobStorage.Current = new SqlServerStorage("MyDatabaseConnectionString1");
                RecurringJob.AddOrUpdate<IntitiateAutoBackUp>("AutoBackupYearly", x => x.IntitiateAutoBackUps(), Cron.Yearly);
                RecurringJob.RemoveIfExists("AutoBackupDaily");
                RecurringJob.RemoveIfExists("AutoBackupWeekly");
                RecurringJob.RemoveIfExists("AutoBackupMonthly");
                RecurringJob.RemoveIfExists("AutoBackupMinutely");
            }
        }
        public static void InsertToNotification()
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                con.Open();
                string explanation = "backup intitalted for the next scheduled cycle.";
                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + explanation + "','System','System','Unseen','fas fa-database text-white','icon-circle bg bg-primary','backupdatabase.aspx','MN')", con);
                cmd197h.ExecuteNonQuery();
            }
        }
        public class IntitiateAutoBackUp
        {
            public void IntitiateAutoBackUps()
            {

                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {

                    con.Open();
                    string backup_query = "BACKUP DATABASE [raksym_database] TO  DISK = N'" + getDirectory+ "' WITH FORMAT, INIT,  MEDIANAME = N'raksym_database',  NAME = N'raksym_database-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                    //String query = "backup database raksym_database to disk='" + backlocation + databasename + "';";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = backup_query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    //Calling HangFire Automation Methods

                    InsertToNotification();
                }
            }
        }
        public static void SendEmail()
        {
            backupdatabase backdata = new backupdatabase();
            string email = backdata.bindEmailAddress();
            string filePath = backdata.GetDtatabasePath();
            string fileName = Path.GetFileName(filePath);
            byte[] bytes = File.ReadAllBytes(filePath);

            MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", email);
            // Specify the email body
            mailMessage.Body = "Raksym plaza database backup sent form qemer rent platfrom.";
            mailMessage.Attachments.Add(new Attachment(new MemoryStream(bytes), fileName));
            mailMessage.IsBodyHtml = true;
            // Specify the email Subject
            mailMessage.Subject = "Database Backup";
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
        protected void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                string backlocation = Server.MapPath("~/Finance/Accounta/backup/");
                string databasename = "raksym_database.bak";

                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {

                    con.Open();
                    string backup_query = "BACKUP DATABASE [raksym_database] TO  DISK = N'" + backlocation + databasename + "' WITH FORMAT, INIT,  MEDIANAME = N'raksym_database',  NAME = N'raksym_database-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                    //String query = "backup database raksym_database to disk='" + backlocation + databasename + "';";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = backup_query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    SendEmail();
                    string message = "The backup has been successfully created.";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + ex.ToString() + "');", true);
            }

        }
        public string GetDtatabasePath()
        {
            string path = string.Empty;
            path = Server.MapPath("~/Finance/Accounta/backup/raksym_database.bak");
            return path;
        }
        public string bindEmailAddress()
        {
            string email = string.Empty;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    email = readerAC["Email"].ToString();
                    readerAC.Close();
                }
            }
            return email;
        }
    }
}