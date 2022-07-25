using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using Twilio;
using System.Web.UI.WebControls;
using Twilio.Rest.Api.V2010.Account;
using System.Security.Cryptography;

namespace advtech.Finance.Accounta
{
    public partial class mail : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        { 

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblCustomers where Status='Active'", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SqlCommand cmd2 = new SqlCommand("insert into tblShopByCustomer values('"+dt.Rows[i]["FllName"].ToString()+"','"+dt.Rows[i]["shop"].ToString()+"','Primary')",con);
                        cmd2.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {


            //    set namep=abdi ahemed where shopno=502A
            //    set namep=Line Addis where shopno=G01
        }
    }
}