using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public class UserUtility
    {
        public string UserName { get; set; }
        public UserUtility() { }
        public string BindUser()
        {

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + System.Web.HttpContext.Current.Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String FN = readerAC["Name"].ToString();
                    readerAC.Close();
                    UserName = FN;
                }
            }
            return UserName;
        }
    }
}