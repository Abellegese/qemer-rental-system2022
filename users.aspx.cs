using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace advtech.Finance.Accounta
{
    public partial class users : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'users.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'users.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    txtNewPass.Text = "";
                    bindinfo(); bindpersonalinfo(); bindimage();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindimage()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
            SqlDataReader readerAC = cmd2AC.ExecuteReader();

            if (readerAC.Read())
            {
                String FN = readerAC["Name"].ToString(); readerAC.Close();
                str = "select * from tblUserProfile where Name='" + FN + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                con.Close();
            }
        }
        protected void bindinfo()
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String FN = readerAC["Name"].ToString();
                    readerAC.Close();
                    SqlCommand cmd2 = new SqlCommand("select * from tblEmployeeBasic where FullName='" + FN + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; string kc3;
                        kc = reader["FullName"].ToString();
                        kc3 = reader["EmployeeID"].ToString();
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
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text != "" && txtComfirmPassword.Text != "" && txtNewPass.Text == txtComfirmPassword.Text)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {

                    SqlCommand cmd = new SqlCommand("update Users set Password=EncryptByPassPhrase('key', '" + txtNewPass.Text + "'),Username='" + txtUname.Text + "' where Username='" + Session["USERNAME"].ToString() + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMsg.Text = "Successfully Updated";
                    lblMsg.ForeColor = Color.Green;
                }
            }
            else
            {

            }
        }
        protected void bindpersonalinfo()
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
                    SqlCommand cmd2 = new SqlCommand("select * from tblPersonalInformation where FullName='" + FN + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; string kc1; string kc2; string kc3;
                        kc = reader["FathersName"].ToString();
                        kc1 = reader["DateofBirth"].ToString();
                        kc2 = reader["MobileNumber"].ToString();
                        kc3 = reader["ResidentialAddress"].ToString();
                        string workemail = reader["PersonalEmail"].ToString();
                        father.InnerText = kc;
                        birthdate.InnerText = kc1;
                        mobile.InnerText = kc2;
                        addressres.InnerText = kc3;
                        contemail.InnerText = workemail;

                        reader.Close();
                        con.Close();
                    }
                }
            }
        }

        protected void Button2_Click1(object sender, EventArgs e)
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
                    SqlCommand cmd246 = new SqlCommand("select*from tblUserProfile where Name='" + FN + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd246);
                    DataTable dt = new DataTable(); sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        if (FileUpload1.HasFile)
                        {
                            string SavePath = Server.MapPath("~/asset/userprofile/");
                            if (!Directory.Exists(SavePath))
                            {
                                Directory.CreateDirectory(SavePath);
                            }
                            string Extention = Path.GetExtension(FileUpload1.PostedFile.FileName);
                            FileUpload1.SaveAs(SavePath + "\\" + FileUpload1.FileName + Extention);

                            SqlCommand cmd3 = new SqlCommand("insert into tblUserProfile values('" + FN + "','" + FileUpload1.FileName + "','" + Extention + "')", con);
                            cmd3.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        if (FileUpload1.HasFile)
                        {
                            string SavePath = Server.MapPath("~/asset/userprofile/");
                            if (!Directory.Exists(SavePath))
                            {
                                Directory.CreateDirectory(SavePath);
                            }
                            string Extention = Path.GetExtension(FileUpload1.PostedFile.FileName);
                            FileUpload1.SaveAs(SavePath + "\\" + FileUpload1.FileName + Extention);
                            SqlCommand cmd3 = new SqlCommand("update tblUserProfile set FileName='" + FileUpload1.FileName + "',Extension='" + Extention + "'", con);
                            cmd3.ExecuteNonQuery();

                        }
                    }
                    Response.Redirect("users.aspx");
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
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
                    SqlCommand cmd3 = new SqlCommand("update Users set Email='" + txtEmail.Text + "' where Name='" + FN + "'", con);
                    cmd3.ExecuteNonQuery();
                    SqlCommand cmd34 = new SqlCommand("update tblEmployeeBasic set WorkEmail='" + txtEmail.Text + "'  where FullName='" + FN + "'", con);
                    cmd34.ExecuteNonQuery();
                    Response.Redirect("users.aspx");
                }
            }
        }
    }
}