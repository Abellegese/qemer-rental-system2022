using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SignalR_Research.Finance.Accounta
{
    public partial class leavedeprtment : System.Web.UI.Page
    {
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'leavedeprtment.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'leavedeprtment.ds' is never used
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    BindBrandsRptr2(); bindbutton(); downloadfile();
                    bindcompany(); bindLeave(); bindattachment();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindbutton()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
            SqlDataReader readerAC = cmd2AC.ExecuteReader();

            if (readerAC.Read())
            {
                String Utype = readerAC["Utype"].ToString();
                if (Utype == "MN")
                {
                    btnApprove.Visible = true;
                    btnDeny.Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    btnDeny.Visible = false;
                }
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
                        reason2.InnerText = reason;
                        durationfrom.InnerText = Convert.ToDateTime(datefrom).ToString("MM/dd/yyyy");
                        durationto.InnerText = Convert.ToDateTime(dateto).ToString("MM/dd/yyyy");
                        RequestedDate.InnerText = Convert.ToDateTime(date).ToString("MM/dd/yyyy");
                        Reqno.InnerText = PID;
                        applicant.InnerText = applicant2;
                        if (status == "Pending")
                        {
                            approveddate.Visible = false;
                            appstat.Visible = true;
                            Status2.Attributes.Add("class", "badge badge-danger");
                            Status2.InnerText = status;
                            btnApprove.Visible = true;
                            btnDeny.Visible = true;
                        }
                        else if (status == "Approved")
                        {
                            approveddate.Visible = true;
                            appstat.Visible = false;
                            btnApprove.Visible = false;
                            btnDeny.Visible = false;
                            Status2.Attributes.Add("class", "badge badge-success");
                            Status2.InnerText = status;
                        }
                        else
                        {
                            btnApprove.Visible = false;
                            btnDeny.Visible = false;
                            Status2.Attributes.Add("class", "badge badge-danger");
                            Status2.InnerText = status;
                        }
                        readerAC.Close();
                    }
                }
            }
            else
            {
                btnApprove.Visible = false;
                btnDeny.Visible = false;

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
                String Utype = readerAC["Utype"].ToString();
                readerAC.Close();
                if (Utype == "MN")
                {
                    str = "select * from tblLeaveform";
                    com = new SqlCommand(str, con);
                    sqlda = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    sqlda.Fill(dt);

                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                    con.Close();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
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
                    SqlCommand cmd2AC1 = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                    SqlDataReader readerAC1 = cmd2AC1.ExecuteReader();

                    if (readerAC1.Read())
                    {
                        String Utype = readerAC1["Name"].ToString(); readerAC1.Close();
                        SqlCommand cmd2AC = new SqlCommand("select * from tblLeaveform where id='" + id + "'", con);
                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                        if (readerAC.Read())
                        {
                            String empName = readerAC["applicant"].ToString();

                            String dayremains = readerAC["dayremains"].ToString();

                            String datefrom = readerAC["required_date_start"].ToString(); readerAC.Close();
                            SqlCommand cmdm = new SqlCommand("select * from tblPersonalInformation where FullName='" + empName + "'", con);
                            SqlDataReader readerm = cmdm.ExecuteReader();

                            if (readerm.Read())
                            {

                                string daysleaft = readerm["leave_days_left"].ToString(); readerm.Close();
                                Int64 days = Convert.ToInt64(daysleaft) - Convert.ToInt64(txtAllowedDate.Text);
                                DateTime allowedD = Convert.ToDateTime(datefrom).AddDays(Convert.ToInt32(txtAllowedDate.Text));
                                if (Checkbox2.Checked == true)
                                {
                                    SqlCommand cmd = new SqlCommand("Update tblLeaveform set  status='Approved',allowed_date_start='" + datefrom + "',allowed_date_end='" + allowedD + "',approver='" + Utype + "' where applicant='" + empName + "' and id='" + PID + "'", con);

                                    cmd.ExecuteNonQuery();
                                    SqlCommand cmdds = new SqlCommand("Update tblPersonalInformation set  leave_days_left='" + days + "' where FullName='" + empName + "'", con);

                                    cmdds.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmd = new SqlCommand("Update tblLeaveform set  status='Approved',allowed_date_start='" + datefrom + "',allowed_date_end='" + allowedD + "',approver='" + Utype + "' where applicant='" + empName + "' and id='" + PID + "'", con);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + empName + " Your Leave Request has been approved','" + Utype + "','" + empName + "','Unseen','fas fa-check fa-2x text-white','icon-circle bg bg-success','leaveform.aspx','')", con);
                            cmd197h.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("leavedeprtment.aspx");
                }
            }
        }

        protected void btnDeny1_Click(object sender, EventArgs e)
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
                    SqlCommand cmd2AC1 = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                    SqlDataReader readerAC1 = cmd2AC1.ExecuteReader();

                    if (readerAC1.Read())
                    {
                        String Utype = readerAC1["Name"].ToString(); readerAC1.Close();
                        SqlCommand cmd2AC = new SqlCommand("select * from tblLeaveform where id='" + id + "'", con);
                        SqlDataReader readerAC = cmd2AC.ExecuteReader();

                        if (readerAC.Read())
                        {
                            String empName = readerAC["applicant"].ToString(); readerAC.Close();
                            SqlCommand cmd = new SqlCommand("Update tblLeaveform set  status='Request Denied',approver='" + Utype + "',DenyResaon='" + txtMob.Text + "' where applicant='" + empName + "' and id='" + PID + "'", con);

                            cmd.ExecuteNonQuery();
                            SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + empName + " Your Leave Request has been denied','" + Utype + "','" + empName + "','Unseen','fas fa-exclamation-triangle fa-2x text-white','icon-circle bg bg-danger','leaveform.aspx','')", con);
                            cmd197h.ExecuteNonQuery();
                            Response.Redirect("leavedeprtment.aspx");
                        }
                    }
                }
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
                            attachlink.HRef = "leavedeprtment.aspx" + "?ref2=" + PID + "&&download=true";
                        }
                    }
                    else
                    {
                        attachmnetdiv.Visible = false;
                    }

                }
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblstatus = item.FindControl("Label2") as Label;
                    if (lblstatus.Text == "Approved")
                    {
                        lblstatus.Attributes.Add("class", " badge badge-success");
                    }
                    else if (lblstatus.Text == "Pending")
                    {
                        lblstatus.Attributes.Add("class", "  badge badge-danger");
                    }
                    else
                    {
                        lblstatus.Attributes.Add("class", "  badge badge-danger");
                    }
                }
            }
        }
    }
}