using System;
using System.Configuration;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class backupdatabase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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