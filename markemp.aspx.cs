using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;


namespace advtech.Finance.Accounta
{
    public partial class markemp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["markas"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["markas"]);
                String PID2 = Convert.ToString(Request.QueryString["status"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "' ", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string kc; string kc1; string Uty;
                        kc = reader["Uid"].ToString();
                        kc1 = reader["Name"].ToString();
                        Uty = reader["Utype"].ToString();
                        reader.Close();
                        if (PID2 == "Active")
                        {
                            SqlCommand cmd197h = new SqlCommand("Update tblEmpPayrol set  status='Terminated' where Name='" + PID + "'", con);
                            cmd197h.ExecuteNonQuery();
                            SqlCommand cmd1 = new SqlCommand("Update tblEmpEarning set  status='Terminated' where fullname='" + PID + "'", con);
                            cmd1.ExecuteNonQuery();
                            SqlCommand cmd23 = new SqlCommand("Update tblEmpDeduction set  status='Terminated' where fullname='" + PID + "'", con);
                            cmd23.ExecuteNonQuery();
                            SqlCommand cmd197hg = new SqlCommand("Update Users set  status='Inactive' where Name='" + PID + "'", con);
                            cmd197hg.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd197hg = new SqlCommand("Update Users set  status='Active' where Name='" + PID + "'", con);
                            cmd197hg.ExecuteNonQuery();
                            SqlCommand cmd197h = new SqlCommand("Update tblEmpPayrol set  status='Active' where Name='" + PID + "'", con);
                            cmd197h.ExecuteNonQuery();
                            SqlCommand cmd1 = new SqlCommand("Update tblEmpEarning set  status='Active' where fullname='" + PID + "'", con);
                            cmd1.ExecuteNonQuery();
                            SqlCommand cmd23 = new SqlCommand("Update tblEmpDeduction set  status='Active' where fullname='" + PID + "'", con);
                            cmd23.ExecuteNonQuery();
                        }


                        Response.Redirect("employeeinfo.aspx?fname=" + PID);
                    }

                }
            }
            if (Request.QueryString["mark"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["mark"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from Users where Name='" + PID + "' ", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string status; string email; string username;
                        status = reader["status"].ToString();
                        email = reader["Email"].ToString();
                        username = reader["Username"].ToString();
                        reader.Close();
                        if (status == "Active")
                        {
                            SqlCommand cmd197h = new SqlCommand("Update Users set  status='Inactive' where Name='" + PID + "'", con);
                            cmd197h.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd197h = new SqlCommand("Update Users set  status='Active' where Name='" + PID + "'", con);
                            cmd197h.ExecuteNonQuery();
                            SqlCommand cmd2AC2 = new SqlCommand("select convert(varchar(100),DecryptByPassPhrase('key',Password)) as Decrypt from Users where Username='" + username + "'", con);
                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                            if (readerAC2.Read())
                            {
                                String location2 = readerAC2["Decrypt"].ToString(); readerAC2.Close();
                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym Employees portal</h3><br />";
                                HTMLBODY += "<h4 class=\"\">Password: " + location2 + "</h4>";
                                HTMLBODY += "<h4 class=\"\">Username: " + username + "</h4>";
                                HTMLBODY += "Click the link to get started <span>https://bsite.net/raksyrmerp/Login/Login1.aspx</span>";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", email);
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
                        Response.Redirect("employeeinfo.aspx?fname=" + PID);
                    }
                }
            }
        }
    }
}