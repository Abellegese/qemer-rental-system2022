using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;


namespace advtech.Finance.Accounta
{
    public partial class EmployeesInformation : System.Web.UI.Page
    {
        public String encrypwd;
        protected void Page_Load(object sender, EventArgs e)
        {
            Panel2.Visible = false; Panel3.Visible = false; Panel4.Visible = false; Panel5.Visible = false; Panel1.Visible = true; Panel6.Visible = false;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (txtEmployeeFName.Text == "" || txtEmployeeID.Text == "" || ddlGender.SelectedItem.Text == "-Select-" || txtDateofJoining.Text == "" || ddlPosition.SelectedItem.Text == "-Select-" || txtWorkEmail.Text == "" || ddlDepartment.SelectedItem.Text == "-Select-" || txtWorkLocation.Text == "")
            {
                BasicIcon.InnerText = "❌"; BasicIcon.Attributes.Add("class", "text-warning font-weight-bold");
                Panel5.Visible = true;
                Span.InnerText = "Please fill all the required field (The field with the red star)";
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    if (Checkbox1.Checked == true)
                    {
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmployeeBasic values('" + txtEmployeeFName.Text + "','" + txtEmployeeID.Text + "','" + ddlGender.SelectedItem.Text + "','" + txtDateofJoining.Text + "','" + ddlPosition.SelectedItem.Text + "','Director/Person with special interest','" + txtWorkEmail.Text + "','" + ddlDepartment.SelectedItem.Text + "','" + txtWorkLocation.Text + "')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                        var names = txtEmployeeFName.Text.ToLower();
                        var lname = names.Split(' ');
                        string firstName = lname[0];
                        SqlCommand cmd3 = new SqlCommand("insert into tblUserProfile values('" + txtEmployeeFName.Text + "','images','.png')", con);
                        cmd3.ExecuteNonQuery();
                        if (ddlPosition.SelectedItem.Text == "Accountant")
                        {

                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','Acc','Active')", con);
                            cmdus.ExecuteNonQuery();
                            SqlCommand cmd2AC2 = new SqlCommand("select convert(varchar(100),DecryptByPassPhrase('key',Password)) as Decrypt from Users where Username='" + firstName + "'", con);
                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                            if (readerAC2.Read())
                            {
                                String location2 = readerAC2["Decrypt"].ToString(); readerAC2.Close();
                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym Employees portal</h3><br />";
                                HTMLBODY += "<h4 class=\"\">Password: " + location2 + "</h4>";
                                HTMLBODY += "<h4 class=\"\">Username: " + firstName + "</h4>";
                                HTMLBODY += "Click the link to get started <span>https://localhost:44357/Login/LogIn1.aspx</span>";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtWorkEmail.Text);
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
                        else if (ddlPosition.SelectedItem.Text == "Finance Head")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','FH','Active')", con);
                            cmdus.ExecuteNonQuery();
                            SqlCommand cmd2AC2 = new SqlCommand("select convert(varchar(100),DecryptByPassPhrase('key',Password)) as Decrypt from Users where Username='" + firstName + "'", con);
                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                            if (readerAC2.Read())
                            {
                                String location2 = readerAC2["Decrypt"].ToString(); readerAC2.Close();
                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym Employees portal</h3><br />";
                                HTMLBODY += "<h4 class=\"\">Password: " + location2 + "</h4>";
                                HTMLBODY += "<h4 class=\"\">Username: " + firstName + "</h4>";
                                HTMLBODY += "Click the link to get started <span>https://localhost:44357/Login/LogIn1.aspx</span>";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtWorkEmail.Text);
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
                        else if (ddlPosition.SelectedItem.Text == "Manager")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','MN','Active')", con);
                            cmdus.ExecuteNonQuery();
                            SqlCommand cmd2AC2 = new SqlCommand("select convert(varchar(100),DecryptByPassPhrase('key',Password)) as Decrypt from Users where Username='" + firstName + "'", con);
                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                            if (readerAC2.Read())
                            {
                                String location2 = readerAC2["Decrypt"].ToString(); readerAC2.Close();
                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym Employees portal</h3><br />";
                                HTMLBODY += "<h4 class=\"\">Password: " + location2 + "</h4>";
                                HTMLBODY += "<h4 class=\"\">Username: " + firstName + "</h4>";
                                HTMLBODY += "Click the link to get started <span>https://localhost:44357/Login/LogIn1.aspx</span>";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtWorkEmail.Text);
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
                        else if (ddlPosition.SelectedItem.Text == "Store Keeper")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','SK','Active')", con);
                            cmdus.ExecuteNonQuery();
                            SqlCommand cmd2AC2 = new SqlCommand("select convert(varchar(100),DecryptByPassPhrase('key',Password)) as Decrypt from Users where Username='" + firstName + "'", con);
                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                            if (readerAC2.Read())
                            {
                                String location2 = readerAC2["Decrypt"].ToString(); readerAC2.Close();
                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym Employees portal</h3><br />";
                                HTMLBODY += "<h4 class=\"\">Password: " + location2 + "</h4>";
                                HTMLBODY += "<h4 class=\"\">Username: " + firstName + "</h4>";
                                HTMLBODY += "Click the link to get started <span>https://localhost:44357/Login/LogIn1.aspx</span>";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtWorkEmail.Text);
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
                        else if (ddlPosition.SelectedItem.Text == "HR")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','HR','Active')", con);
                            cmdus.ExecuteNonQuery();
                            SqlCommand cmd2AC2 = new SqlCommand("select convert(varchar(100),DecryptByPassPhrase('key',Password)) as Decrypt from Users where Username='" + firstName + "'", con);
                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                            if (readerAC2.Read())
                            {
                                String location2 = readerAC2["Decrypt"].ToString(); readerAC2.Close();
                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym Employees portal</h3><br />";
                                HTMLBODY += "<h4 class=\"\">Password: " + location2 + "</h4>";
                                HTMLBODY += "<h4 class=\"\">Username: " + firstName + "</h4>";
                                HTMLBODY += "Click the link to get started <span>https://localhost:44357/Login/LogIn1.aspx</span>";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtWorkEmail.Text);
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
                        else if (ddlPosition.SelectedItem.Text == "Technical")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','TC','Active')", con);
                            cmdus.ExecuteNonQuery();
                            SqlCommand cmd2AC2 = new SqlCommand("select convert(varchar(100),DecryptByPassPhrase('key',Password)) as Decrypt from Users where Username='" + firstName + "'", con);
                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                            if (readerAC2.Read())
                            {
                                String location2 = readerAC2["Decrypt"].ToString(); readerAC2.Close();
                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym Employees portal</h3><br />";
                                HTMLBODY += "<h4 class=\"\">Password: " + location2 + "</h4>";
                                HTMLBODY += "<h4 class=\"\">Username: " + firstName + "</h4>";
                                HTMLBODY += "Click the link to get started <span>https://localhost:44357/Login/LogIn1.aspx</span>";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtWorkEmail.Text);
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
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','OTH','Active')", con);
                            cmdus.ExecuteNonQuery();
                            SqlCommand cmd2AC2 = new SqlCommand("select convert(varchar(100),DecryptByPassPhrase('key',Password)) as Decrypt from Users where Username='" + firstName + "'", con);
                            SqlDataReader readerAC2 = cmd2AC2.ExecuteReader();

                            if (readerAC2.Read())
                            {
                                String location2 = readerAC2["Decrypt"].ToString(); readerAC2.Close();
                                string HTMLBODY = "<h3 class=\"text-uppercase text-center\">Welcome to raksym Employees portal</h3><br />";
                                HTMLBODY += "<h4 class=\"\">Password: " + location2 + "</h4>";
                                HTMLBODY += "<h4 class=\"\">Username: " + firstName + "</h4>";
                                HTMLBODY += "Click the link to get started <span>https://localhost:44357/Login/LogIn1.aspx</span>";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", txtWorkEmail.Text);
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
                    }
                    else
                    {
                        var names = txtEmployeeFName.Text.ToLower();
                        var lname = names.Split(' ');
                        string firstName = lname[0];
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmployeeBasic values('" + txtEmployeeFName.Text + "','" + txtEmployeeID.Text + "','" + ddlGender.SelectedItem.Text + "','" + txtDateofJoining.Text + "','" + ddlPosition.SelectedItem.Text + "','Not','" + txtWorkEmail.Text + "','" + ddlDepartment.SelectedItem.Text + "','" + txtWorkLocation.Text + "')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                        SqlCommand cmd3 = new SqlCommand("insert into tblUserProfile values('" + txtEmployeeFName.Text + "','images.png','.png')", con);
                        cmd3.ExecuteNonQuery();

                        ///Inserting the Employee as Inactive
                        if (ddlPosition.SelectedItem.Text == "Accountant")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','Acc','Inactive')", con);
                            cmdus.ExecuteNonQuery();
                        }
                        else if (ddlPosition.SelectedItem.Text == "Finance Head")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','FH','Inactive')", con);
                            cmdus.ExecuteNonQuery();
                        }
                        else if (ddlPosition.SelectedItem.Text == "Manager")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','MN','Inactive')", con);
                            cmdus.ExecuteNonQuery();
                        }
                        else if (ddlPosition.SelectedItem.Text == "Store Keeper")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','SK','Inactive')", con);
                            cmdus.ExecuteNonQuery();
                        }
                        else if (ddlPosition.SelectedItem.Text == "HR")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','HR','Inactive')", con);
                            cmdus.ExecuteNonQuery();
                        }
                        else if (ddlPosition.SelectedItem.Text == "Technical")
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','TC','Inactive')", con);
                            cmdus.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmdus = new SqlCommand("insert into Users values('" + firstName + "',EncryptByPassPhrase('key', '" + RandomPassword() + "'),'" + txtWorkEmail.Text + "','" + txtEmployeeFName.Text + "','OTH','Inactive')", con);
                            cmdus.ExecuteNonQuery();
                        }
                    }
                    Basic.Attributes.Add("class", "icon-circle bg-success border-dark border-left border-top border-right border-bottom");
                    BasicIcon.Attributes.Add("class", "text-white fas fa-check font-weight-bold");
                    BasicIcon.InnerText = "";
                    BasicSpan.Attributes.Add("class", "text-gray-600 font-weight-bold");
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                }

            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (txtBasic.Text == "")
            {
                Panel5.Visible = true; Panel1.Visible = false; Panel2.Visible = true;
                SalaryIcon.InnerText = "❌"; SalaryIcon.Attributes.Add("class", "text-warning font-weight-bold");
                Span.InnerText = "Please fill all the required field(The field with the red star)";
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    double basicsalary = Convert.ToDouble(txtBasic.Text); double tax; double deduction;
                    if (basicsalary >= 0 && basicsalary <= 600)
                    {
                        tax = 0; deduction = 0;
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmpPayrol values('" + txtEmployeeFName.Text + "','" + txtBasic.Text + "','" + tax / 100 + "','" + deduction + "','Active')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 601 && basicsalary <= 1650)
                    {
                        tax = 10; deduction = 60;
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmpPayrol values('" + txtEmployeeFName.Text + "','" + txtBasic.Text + "','" + tax / 100 + "','" + deduction + "','Active')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 1651 && basicsalary <= 3200)
                    {
                        tax = 15; deduction = 142.5;
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmpPayrol values('" + txtEmployeeFName.Text + "','" + txtBasic.Text + "','" + tax / 100 + "','" + deduction + "','Active')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 3201 && basicsalary <= 5250)
                    {
                        tax = 20; deduction = 302.5;
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmpPayrol values('" + txtEmployeeFName.Text + "','" + txtBasic.Text + "','" + tax / 100 + "','" + deduction + "','Active')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 5251 && basicsalary <= 7800)
                    {
                        tax = 25; deduction = 565;
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmpPayrol values('" + txtEmployeeFName.Text + "','" + txtBasic.Text + "','" + tax / 100 + "','" + deduction + "','Active')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 7801 && basicsalary <= 10900)
                    {
                        tax = 30; deduction = 955;
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmpPayrol values('" + txtEmployeeFName.Text + "','" + txtBasic.Text + "','" + tax / 100 + "','" + deduction + "','Active')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                    }
                    else
                    {
                        tax = 35; deduction = 1500;
                        SqlCommand cmd111t = new SqlCommand("insert into tblEmpPayrol values('" + txtEmployeeFName.Text + "','" + txtBasic.Text + "','" + tax / 100 + "','" + deduction + "','Active')", con);
                        con.Open();
                        cmd111t.ExecuteNonQuery();
                    }
                    Salary.Attributes.Add("class", "icon-circle bg-success border-dark border-left border-top border-right border-bottom");
                    SalaryIcon.Attributes.Add("class", "text-white fas fa-check font-weight-bold");
                    SalaryIcon.InnerText = "";
                    SalarySpan.Attributes.Add("class", "text-gray-600 font-weight-bold");
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = true;
                }

            }
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (txtPersonalEmail.Text == "" || txtMobileNumber.Text == "" || txtDateofBirth.Text == "" || txtFathersName.Text == "" || txtResidentialAdress.Text == "")
            {
                Panel5.Visible = true; Panel1.Visible = false;
                Panel2.Visible = false; Panel3.Visible = true;
                PersonalIcon.InnerText = "❌"; PersonalIcon.Attributes.Add("class", "text-warning font-weight-bold");
                Span.InnerText = "Please fill all the required field(The field with the red star)";
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    int today1 = DateTime.Now.Year;
                    DateTime duedate1 = Convert.ToDateTime(txtDateofJoining.Text);
                    int t1 = today1 - duedate1.Year; string dayleft1 = t1.ToString();
                    SqlCommand cmd111t = new SqlCommand("insert into tblPersonalInformation values('" + txtEmployeeFName.Text + "','" + txtPersonalEmail.Text + "','" + txtMobileNumber.Text + "','" + txtDateofBirth.Text + "','" + txtFathersName.Text + "','" + txtResidentialAdress.Text + "','" + txtStreet.Text + "','" + txtCity.Text + "','','" + txtLeavedays.Text + "','" + txtDateofJoining.Text + "','" + t1 + "')", con);
                    con.Open();
                    cmd111t.ExecuteNonQuery();
                    Personal.Attributes.Add("class", "icon-circle bg-success border-dark border-left border-top border-right border-bottom");
                    PersonalIcon.Attributes.Add("class", "text-white fas fa-check font-weight-bold");
                    PersonalIcon.InnerText = "";
                    PersonalSpan.Attributes.Add("class", "text-gray-600 font-weight-bold");
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = true;
                }
            }
        }
        private readonly Random _random = new Random();
        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            // 4-Letters lower case   
            passwordBuilder.Append(RandomString(2, true));

            // 4-Digits between 1000 and 9999  
            passwordBuilder.Append(RandomNumber(1000, 9999));

            // 2-Letters upper case  
            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                if (Radio1.Checked == true)
                {

                    string fname = txtEmployeeFName.Text;

                    con.Open();

                    SqlCommand cmdnh = new SqlCommand("insert into tblPaymentMode values('Bank','" + fname + "')", con);

                    cmdnh.ExecuteNonQuery();
                    Payment.Attributes.Add("class", "icon-circle bg-success border-dark border-left border-top border-right border-bottom");
                    PaymentIcon.Attributes.Add("class", "text-white fas fa-check font-weight-bold");
                    PaymentIcon.InnerText = "";
                    PaymentSpan.Attributes.Add("class", "text-gray-600 font-weight-bold");
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = false; Panel6.Visible = true;
                }

                else if (Radio2.Checked == true)
                {

                    string fname = txtEmployeeFName.Text;

                    con.Open();

                    SqlCommand cmdnh = new SqlCommand("insert into tblPaymentMode values('Cheque','" + fname + "')", con);
                    cmdnh.ExecuteNonQuery();
                    Payment.Attributes.Add("class", "icon-circle bg-success border-dark border-left border-top border-right border-bottom");
                    PaymentIcon.Attributes.Add("class", "text-white fas fa-check font-weight-bold");
                    PaymentIcon.InnerText = "";
                    PaymentSpan.Attributes.Add("class", "text-gray-600 font-weight-bold");
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = false; Panel6.Visible = true;

                }
                else
                {

                    string fname = txtEmployeeFName.Text;

                    con.Open();

                    SqlCommand cmdnh = new SqlCommand("insert into tblPaymentMode values('Cash','" + fname + "')", con);

                    cmdnh.ExecuteNonQuery();
                    Payment.Attributes.Add("class", "icon-circle bg-success border-dark border-left border-top border-right border-bottom");
                    PaymentIcon.Attributes.Add("class", "text-white fas fa-check font-weight-bold");
                    PaymentIcon.InnerText = "";
                    PaymentSpan.Attributes.Add("class", "text-gray-600 font-weight-bold");
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel4.Visible = false; Panel6.Visible = true;

                }


                SqlCommand cmdde = new SqlCommand("insert into tblEmpWorkExperience values('" + txtEmployeeFName.Text + "','included installing new hardware and software, routinely testing IT systems and technologies to ensure they meet necessary standards, ensuring safe and secure data storage, assisting with network administration tasks and resolving all coworker complaints and issues regarding IT systems and software. I can confirm he possesses the strong analytical and problem-solving skills needed to diagnose, resolve and maintain IT systems and technology and has excellent verbal and written communication skills.')", con);
                cmdde.ExecuteNonQuery();



            }
        }
        public string encryption1(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}
