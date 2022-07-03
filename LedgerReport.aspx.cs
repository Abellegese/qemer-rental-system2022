using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class LedgerReport : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
#pragma warning disable CS0169 // The field 'LedgerReport.sqlda' is never used
        SqlDataAdapter sqlda;
#pragma warning restore CS0169 // The field 'LedgerReport.sqlda' is never used
#pragma warning disable CS0169 // The field 'LedgerReport.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'LedgerReport.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBrandsRptr();
            }
        }
        protected void Save(object sender, EventArgs e)
        {

        }
        private void BindBrandsRptr()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblGeneralLedger2";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
        }
    }
}