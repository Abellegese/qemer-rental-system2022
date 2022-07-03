using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace SignalR_Research.Finance.Accounta
{
    public partial class leaveform : System.Web.UI.Page
    {
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'leaveform.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'leaveform.ds' is never used
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    BindBrandsRptr2(); bindattachment();
                    bindcompany(); bindLeave(); downloadfile();
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
        private void bindLeave()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            Int64 id = Convert.ToInt64(PID);
            if (Request.QueryString["ref2"] != null)
            {
                leaveempt.Visible = false;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2AC = new SqlCommand("select * from tblLeaveform where id='" + id + "'", con);
                    SqlDataReader readerAC = cmd2AC.ExecuteReader();

                    if (readerAC.Read())
                    {
                        String Alloweddates = readerAC["allowed_date_start"].ToString();
                        String Alloweddatee = readerAC["allowed_date_end"].ToString();
                        String approver4 = readerAC["approver"].ToString();
                        String phone = readerAC["phone"].ToString();
                        String reason = readerAC["reason"].ToString();
                        String position = readerAC["position"].ToString();
                        String dayremains = readerAC["dayremains"].ToString();
                        currentleave.InnerText = dayremains;
                        position2.InnerText = position;
                        phoneno.InnerText = phone;
                        String status = readerAC["status"].ToString();
                        String date = readerAC["date_requested"].ToString();
                        String applicant2 = readerAC["applicant"].ToString();
                        String datefrom = readerAC["required_date_start"].ToString();
                        String dateto = readerAC["required_date_end"].ToString();
                        String denialreason = readerAC["DenyResaon"].ToString();
                        reason2.InnerText = reason;
                        durationfrom.InnerText = Convert.ToDateTime(datefrom).ToString("MM/dd/yyyy");
                        durationto.InnerText = Convert.ToDateTime(dateto).ToString("MM/dd/yyyy");
                        RequestedDate.InnerText = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
                        Reqno.InnerText = PID;
                        applicant.InnerText = applicant2;
                        if (status == "Pending")
                        {
                            approvershow.Visible = false;
                            approveddate.Visible = false;
                            appstat.Visible = true;
                            Status2.Attributes.Add("class", "badge badge-danger");
                            Status2.InnerText = status;
                        }
                        else if (status == "Approved")
                        {
                            approvershow.Visible = true;
                            alldatefrom.InnerText = Convert.ToDateTime(Alloweddates).ToString("MM/dd/yyyy");
                            allodateto.InnerText = Convert.ToDateTime(Alloweddatee).ToString("MM/dd/yyyy");
                            approver.InnerText = approver4;
                            approveddate.Visible = true;
                            appstat.Visible = false;
                            Status2.Attributes.Add("class", "badge badge-success");
                            Status2.InnerText = status;
                        }
                        else
                        {
                            approver.InnerText = approver4;
                            reasongh.InnerText = denialreason;
                            approveddate.Visible = false;
                            appstat.Visible = false;
                            ad.Visible = false; approvershow.Visible = true;
                            Status2.Attributes.Add("class", "badge badge-danger");
                            Status2.InnerText = status;
                        }
                        readerAC.Close();
                    }
                }
            }
            else
            {
                leavecard.Visible = false;
            }
        }
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
            SqlDataReader readerAC = cmd2AC.ExecuteReader();

            if (readerAC.Read())
            {
                String FN = readerAC["Name"].ToString();
                readerAC.Close();
                str = "select * from tblLeaveform where applicant='" + FN + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);

                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                con.Close();
            }
        }
        private void downloadfile()
        {
            if (Request.QueryString["download"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select*from tblLeaveAttachment where msgid='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; string ext = reader["extension"].ToString(); kc = reader["Name"].ToString(); reader.Close();
                        string SavePath = Server.MapPath("~/asset/attachment_leave/" + kc + ext);
                        Response.ContentType = ContentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + kc + ext);
                        Response.WriteFile(SavePath);
                        Response.End();
                    }
                }
            }

        }
        private void bindattachment()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);

                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select*from tblLeaveAttachment where msgid='" + PID + "'", con);

                    SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd2);
                    DataTable dtBrands232c3 = new DataTable();
                    sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                    if (i2c3 != 0)
                    {
                        SqlDataReader reader = cmd2.ExecuteReader();
                        if (reader.Read())
                        {
                            string kc; kc = reader["Name"].ToString(); reader.Close();

                            attachname.InnerText = kc;
                            attachlink.HRef = "leaveform.aspx" + "?ref2=" + PID + "&&download=true";
                        }
                    }
                    else
                    {
                        attachmnetdiv.Visible = false;
                    }

                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDateform.Text == "" || txtDateto.Text == "" || txtReason.Text == "")
            {
                lblMsg.Text = "Please Fill all the required input to proceed."; lblMsg.ForeColor = Color.Red;
            }
            else
            {
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

                        SqlCommand cmdm = new SqlCommand("select * from tblPersonalInformation where FullName='" + FN + "'", con);
                        SqlDataReader readerm = cmdm.ExecuteReader();

                        if (readerm.Read())
                        {
                            String mobile = readerm["MobileNumber"].ToString();
                            string daysleaft = readerm["leave_days_left"].ToString();
                            readerm.Close();
                            SqlCommand cmdpos = new SqlCommand("select * from tblEmployeeBasic where FullName='" + FN + "'", con);
                            SqlDataReader readerpos = cmdpos.ExecuteReader();

                            if (readerpos.Read())
                            {
                                String pos = readerpos["Position"].ToString();
                                String department = readerpos["Department"].ToString();
                                readerpos.Close();

                                SqlCommand cmddf = new SqlCommand("select * from tblLeaveform", con);
                                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                                DataTable dtdf = new DataTable();
                                sdadf.Fill(dtdf); int nb = dtdf.Rows.Count + 1;
                                SqlCommand cmdin = new SqlCommand("insert into tblLeaveform values('" + FN + "','" + mobile + "','" + pos + "','" + daysleaft + "','" + txtReason.Text + "','" + txtDateform.Text + "','" + txtDateto.Text + "','Pending','','','','" + DateTime.Now.Date + "','" + department + "','none')", con);

                                cmdin.ExecuteNonQuery();
                                if (FileUpload1.HasFile)
                                {
                                    string SavePath = Server.MapPath("~/asset/attachment_leave/");
                                    if (!Directory.Exists(SavePath))
                                    {
                                        Directory.CreateDirectory(SavePath);
                                    }
                                    string Extention = Path.GetExtension(FileUpload1.PostedFile.FileName);
                                    FileUpload1.SaveAs(SavePath + "\\" + FileUpload1.FileName + Extention);
                                    SqlCommand cmdinr = new SqlCommand("insert into tblLeaveAttachment values('" + FileUpload1.FileName + "','" + nb + "','" + Extention + "')", con);

                                    cmdinr.ExecuteNonQuery();
                                }
                                String email = ""; string utype = "";

                                SqlCommand cmd2AC1 = new SqlCommand("select * from Users where Utype='MN'", con);
                                SqlDataReader readerAC1 = cmd2AC1.ExecuteReader();

                                if (readerAC1.Read())
                                {
                                    String FN1 = readerAC1["Email"].ToString();
                                    utype = readerAC1["Utype"].ToString();
                                    readerAC1.Close();
                                    email = FN1;
                                }

                                string emailbody = "Employee, " + FN + " Sent Leave request. For more info visit our portal: https://localhost:44357/Login/LogIn1.aspx";
                                MailMessage mailMessage = new MailMessage("abellegese5@gmail.com", email);
                                // Specify the email body
                                mailMessage.Body = emailbody;
                                mailMessage.IsBodyHtml = true;
                                // Specify the email Subject
                                mailMessage.Subject = "Leave Request";

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

                                //
                                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + FN + " Sent Leave Request','Machine','','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-danger','leavedeprtment.aspx','" + utype + "')", con);
                                cmd197h.ExecuteNonQuery();
                            }
                        }
                    }
                    Response.Redirect("leaveform.aspx");
                }
            }

        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblExp = item.FindControl("Label2") as Label;
                    if (lblExp.Text == "Pending")
                    {
                        lblExp.Attributes.Add("class", "badge badge-danger");
                    }
                    else if (lblExp.Text == "Denied")
                    {
                        lblExp.Attributes.Add("class", "badge badge-danger");
                    }
                    else
                    {
                        lblExp.Attributes.Add("class", "badge badge-success");
                    }
                }
            }
        }
    }
}