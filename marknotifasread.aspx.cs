using System;
using System.Configuration;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class marknotifasread : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["markas"] != null)
            {
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
                        SqlCommand cmd197h = new SqlCommand("Update tblNotification set  Status='Seen' where status='Unseen' and Utype='" + Uty + "'", con);
                        cmd197h.ExecuteNonQuery();

                        Response.Redirect("Home.aspx");
                    }

                }
            }
            if (Request.QueryString["r"] != null)
            {
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
                        SqlCommand cmd197h = new SqlCommand("delete from tblNotification", con);
                        cmd197h.ExecuteNonQuery();

                        Response.Redirect("Home.aspx");
                    }

                }
            }
        }
    }
}