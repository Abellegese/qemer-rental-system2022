using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace advtech.Finance.Accounta
{
    public partial class OrganizationProfile : System.Web.UI.Page
    {
        static String resumelink;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string path = Path.GetFileName(FileUpload1.FileName);
                path = path.Replace(" ", "");
                String filename1 = path;
                if (FileUpload1.HasFile)
                {
                    FileUpload1.SaveAs(Server.MapPath("~/Finance/Accounta/Logo/") + path); resumelink = "Logo/" + path;
                }
                SqlCommand cmd111t = new SqlCommand("insert into tblOrganization values('" + txtOname.Text + "','" + resumelink + "','" + txtAddress.Text + "','" + txtCity.Text + "','" + txtEmail.Text + "','" + txtFax.Text + "','" + txtContact.Text + "','" + txtBussinessLocation.Text + "','" + txtCountry.Text + "','" + DropDownList1.SelectedItem.Text + "')", con);
                con.Open();
                cmd111t.ExecuteNonQuery();
                Response.Redirect("AccountUpdate.aspx");
            }
        }
    }
}