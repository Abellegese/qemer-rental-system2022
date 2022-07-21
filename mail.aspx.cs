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
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        { 

        }
        
    }
}