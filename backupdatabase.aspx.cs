using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using Hangfire;
using Hangfire.SqlServer;


namespace advtech.Finance.Accounta
{
    public partial class backupdatabase : System.Web.UI.Page
    {
        public static string getDirectory = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                string explanation = "backup intitalted";
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
        private void SendEmail()
        {
            //string filePath = Server.MapPath("~/Finance/Accounta/backup/raksym_database.bak");
            //string fileName = Path.GetFileName(filePath);
            // byte[] bytes = File.ReadAllBytes(filePath);
            /**
            MailMessage mm = new MailMessage("abellegese5@gmail.com", "abellegese5@gmail.com");
            mm.Subject = "Database Backup";
            mm.Body = "Raksym plaza database backup sent form qemer rent platfrom.";
            mm.Attachments.Add(new Attachment(new MemoryStream(bytes), fileName));
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            
             NetworkCredential NetworkCred = new NetworkCredential();
             NetworkCred.UserName = "abellegese5@gmail.com";
             NetworkCred.Password = "Abel.lege2929#";
             smtp.Credentials = NetworkCred;
             smtp.Port = 587;
             smtp.Send(mm);
           
            string customerEmail = "abellegese5@gmail.com";
            string customerName = "Abel Legese";
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 25;
            WebMail.UserName = "abellegese5@gmail.com";
            WebMail.Password = "Abel.lege2929#";
            WebMail.From = "abellegese5@gmail.com";

            // Send email
            WebMail.Send(to: customerEmail,
                subject: "Help request from - " + customerName,
                body: customerName
            ); **/
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
                    string message = "The backup has been successfully created.";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + ex.ToString() + "');", true);
            }
        }
    }
}