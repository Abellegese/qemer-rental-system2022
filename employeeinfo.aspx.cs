using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace advtech.Finance.Accounta
{
    public partial class employeeinfo : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'employeeinfo.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'employeeinfo.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    BindBrandsRptr2(); bindimage(); BindEmpStatus();
                    bindinfo(); bindpersonalinfo(); bindpaymentmde();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }

        }
        private void BindEmpStatus()
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID2 = Convert.ToString(Request.QueryString["fname"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from Users where Name='" + PID2 + "' ", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string status;
                        status = reader["status"].ToString();
                        reader.Close();
                        if (status == "Active")
                        {
                            StatusIndicator.Attributes.Add("class", "status-indicator1 bg-success");
                        }
                        else
                        {
                            StatusIndicator.Attributes.Add("class", "status-indicator1 bg-danger");
                        }
                    }
                }
            }
        }
        private void bindimage()
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID2 = Convert.ToString(Request.QueryString["fname"]);
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();

                str = "select * from tblUserProfile where Name='" + PID2 + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt); int i = dt.Rows.Count;
                if (i != 0)
                {
                    defaltprf.Visible = false;
                }
                else
                {
                    Repeater2.Visible = false;
                }
                Repeater2.DataSource = dt;
                Repeater2.DataBind();
                con.Close();

            }
        }
        private void BindBrandsRptr2()
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID2 = Convert.ToString(Request.QueryString["fname"]);
                Span7.InnerText = PID2;
                string fname = PID2;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblEmpPayrol where Name LIKE '%" + fname + "%'";
                com = new SqlCommand(str, con);

                using (SqlDataAdapter sda = new SqlDataAdapter(com))
                {
                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands); int i = dtBrands.Rows.Count;
                    if (i != 0)
                    {
                        if (dtBrands.Rows[0][5].ToString() == "Active")
                        {
                            Statusof.InnerText = dtBrands.Rows[0][5].ToString();
                            Statusof.Attributes.Add("class", "text-success badge badge-light font-weight-bold small");
                        }
                        else
                        {
                            Statusof.InnerText = dtBrands.Rows[0][5].ToString();
                            Statusof.Attributes.Add("class", "text-danger badge badge-light font-weight-bold small");
                        }

                    }

                    A1.HRef = "markemp.aspx?mark=" + dtBrands.Rows[0][1].ToString();
                    modalMain.HRef = "markemp.aspx?markas=" + dtBrands.Rows[0][1].ToString() + "&&status=" + dtBrands.Rows[0][5].ToString();
                    A2.HRef = "workexperience.aspx?markas=" + dtBrands.Rows[0][1].ToString();
                    A4.HRef = "EmployeeReport.aspx?fname=" + dtBrands.Rows[0][1].ToString();
                    Repeater1.DataSource = dtBrands;
                    Repeater1.DataBind();
                }
            }
        }

        protected void bindinfo()
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["fname"]);

                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from tblEmployeeBasic where FullName  LIKE '%" + PID + "%'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; string kc3;
                        kc = reader["FullName"].ToString();

                        kc3 = reader["EmployeeID"].ToString();

                        //Basic
                        string workemail = reader["WorkEmail"].ToString();
                        string dateofj = reader["DateofJoining"].ToString();
                        string department2 = reader["Department"].ToString();
                        name.InnerText = kc; ;
                        emailwork.InnerText = workemail; datejoining.InnerText = dateofj;
                        department.InnerText = department2;
                        reader.Close();
                        con.Close();
                    }
                }
            }
        }
        protected void bindpersonalinfo()
        {
            if (Request.QueryString["fname"] != null)
            {

                String PID2 = Convert.ToString(Request.QueryString["fname"]);

                string fname = PID2;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    SqlCommand cmd2 = new SqlCommand("select * from tblPersonalInformation where FullName='" + fname + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; string kc1; string kc2; string kc3; string leave;
                        kc = reader["FathersName"].ToString();
                        kc1 = reader["DateofBirth"].ToString();
                        kc2 = reader["MobileNumber"].ToString();
                        kc3 = reader["ResidentialAddress"].ToString();
                        leave = reader["leave_days_left"].ToString();
                        string workemail = reader["PersonalEmail"].ToString();
                        father.InnerText = kc;
                        birthdate.InnerText = kc1;
                        mobile.InnerText = kc2;
                        addressres.InnerText = kc3;
                        contemail.InnerText = workemail;
                        sPanLeaveDays.InnerText = leave;
                        reader.Close();
                        con.Close();
                    }
                }
            }
        }
        private void bindpaymentmde()
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID2 = Convert.ToString(Request.QueryString["fname"]);

                string fname = PID2;
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from tblPaymentMode where FullName='" + fname + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc;
                        kc = reader["Type"].ToString();
                        payment.InnerText = kc;
                        reader.Close();
                        con.Close();
                    }
                }
            }
        }
        protected void OnEdit(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
            this.ToggleElements(item, true);
        }
        private void ToggleElements(RepeaterItem item, bool isEdit)
        {
            //Toggle Buttons.
            item.FindControl("lnkEdit").Visible = !isEdit;
            item.FindControl("lnkUpdate").Visible = isEdit;



            //Toggle Labels.

            item.FindControl("Label1").Visible = !isEdit;
            item.FindControl("Label2").Visible = !isEdit;
            item.FindControl("Label3").Visible = !isEdit;
            //Toggle TextBoxes.
            item.FindControl("TextBox1").Visible = isEdit;

        }
        protected void OnUpdate(object sender, EventArgs e)
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID2 = Convert.ToString(Request.QueryString["fname"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    RepeaterItem item = (sender as LinkButton).Parent as RepeaterItem;
                    Label customerId = item.FindControl("lblCustomerId") as Label;
                    TextBox salary = item.FindControl("TextBox1") as TextBox;

                    double basicsalary = Convert.ToDouble(salary.Text);
                    double tax; double deduction;
                    if (basicsalary >= 0 && basicsalary <= 600)
                    {
                        tax = 0; deduction = 0;
                        SqlCommand cmd = new SqlCommand("Update tblEmpPayrol set  Salary='" + salary.Text + "',Tax='" + tax / 100 + "',Deduction='" + deduction + "' where Name='" + customerId.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 601 && basicsalary <= 1650)
                    {
                        tax = 10; deduction = 60;
                        SqlCommand cmd = new SqlCommand("Update tblEmpPayrol set  Salary='" + salary.Text + "',Tax='" + tax / 100 + "',Deduction='" + deduction + "' where Name='" + customerId.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 1651 && basicsalary <= 3200)
                    {
                        tax = 15; deduction = 142.5;
                        SqlCommand cmd = new SqlCommand("Update tblEmpPayrol set  Salary='" + salary.Text + "',Tax='" + tax / 100 + "',Deduction='" + deduction + "' where Name='" + customerId.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 3201 && basicsalary <= 5250)
                    {
                        tax = 20; deduction = 302.5;
                        SqlCommand cmd = new SqlCommand("Update tblEmpPayrol set  Salary='" + salary.Text + "',Tax='" + tax / 100 + "',Deduction='" + deduction + "' where Name='" + customerId.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 5251 && basicsalary <= 7800)
                    {
                        tax = 25; deduction = 565;
                        SqlCommand cmd = new SqlCommand("Update tblEmpPayrol set  Salary='" + salary.Text + "',Tax='" + tax / 100 + "',Deduction='" + deduction + "' where Name='" + customerId.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    else if (basicsalary >= 7801 && basicsalary <= 10900)
                    {
                        tax = 30; deduction = 955;
                        SqlCommand cmd = new SqlCommand("Update tblEmpPayrol set  Salary='" + salary.Text + "',Tax='" + tax / 100 + "',Deduction='" + deduction + "' where Name='" + customerId.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        tax = 35; deduction = 1500;
                        SqlCommand cmd = new SqlCommand("Update tblEmpPayrol set  Salary='" + salary.Text + "',Tax='" + tax / 100 + "',Deduction='" + deduction + "' where Name='" + customerId.Text + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    Response.Redirect("employeeinfo.aspx?fname=" + PID2);
                }
            }
        }
        protected void btnEmpInfo_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["fname"] != null)
            {
                String PID2 = Convert.ToString(Request.QueryString["fname"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {

                    SqlCommand cmd = new SqlCommand("Update tblEmployeeBasic set  DateofJoining='" + txtDateofJoining.Text + "',Position='" + ddlPosition.SelectedItem.Text + "',WorkEmail='" + txtWorkMail.Text + "',Department='" + ddlDepartment.SelectedItem.Text + "' where FullName='" + PID2 + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //Update email in users
                    SqlCommand cmdu1 = new SqlCommand("Update Users set  Email='" + txtPersonalEmail.Text + "' where Name='" + PID2 + "'", con);
                    cmdu1.ExecuteNonQuery();
                    //PersonalInformation
                    SqlCommand cmd1 = new SqlCommand("Update tblPersonalInformation set  PersonalEmail='" + txtPersonalEmail.Text + "',MobileNumber='" + txtMobile.Text + "',DateofBirth='" + txtDateOfBirth.Text + "',FathersName='" + txtFatherName.Text + "',ResidentialAddress='" + txtResidentialAddress.Text + "' where FullName='" + PID2 + "'", con);
                    cmd1.ExecuteNonQuery();
                    if (ddlPosition.SelectedItem.Text == "Accountant")
                    {

                        SqlCommand cmdu = new SqlCommand("Update Users set  Utype='Acc' where Name='" + PID2 + "'", con);
                        cmdu.ExecuteNonQuery();
                    }
                    else if (ddlPosition.SelectedItem.Text == "Finance Head")
                    {
                        SqlCommand cmdu = new SqlCommand("Update Users set  Utype='FH' where Name='" + PID2 + "'", con);
                        cmdu.ExecuteNonQuery();
                    }
                    else if (ddlPosition.SelectedItem.Text == "Manager")
                    {
                        SqlCommand cmdu = new SqlCommand("Update Users set  Utype='MN' where Name='" + PID2 + "'", con);
                        cmdu.ExecuteNonQuery();
                    }
                    else if (ddlPosition.SelectedItem.Text == "Store Keeper")
                    {
                        SqlCommand cmdu = new SqlCommand("Update Users set  Utype='SK' where Name='" + PID2 + "'", con);
                        cmdu.ExecuteNonQuery();
                    }
                    else if (ddlPosition.SelectedItem.Text == "HR")
                    {
                        SqlCommand cmdu = new SqlCommand("Update Users set  Utype='HR' where Name='" + PID2 + "'", con);
                        cmdu.ExecuteNonQuery();
                    }
                    else if (ddlPosition.SelectedItem.Text == "Technical")
                    {
                        SqlCommand cmdu = new SqlCommand("Update Users set  Utype='TC' where Name='" + PID2 + "'", con);
                        cmdu.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmdu = new SqlCommand("Update Users set  Utype='TH' where Name='" + PID2 + "'", con);
                        cmdu.ExecuteNonQuery();
                    }
                    Response.Redirect("employeeinfo.aspx?fname=" + PID2);
                }
            }
        }
    }
}