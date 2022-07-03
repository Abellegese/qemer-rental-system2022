using System.Configuration;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for Class1
/// </summary>
namespace advtech.Finance.Accounta
{
    public class ReferenceFinder
    {
        public string ReferenceNumber;

        public ReferenceFinder()
        {

        }
        public ReferenceFinder(string ReferenceNumber)
        {
            this.ReferenceNumber = ReferenceNumber;
        }
        public string reference
        {
            get { return ReferenceNumber; }
            set { ReferenceNumber = value; }
        }
        public bool FindReferenceNumber()
        {

            bool IsReferenceFound = false;
            string CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmddf = new SqlCommand("select * from tblrentreceipt where references1='" + ReferenceNumber + "'", con);
                SqlDataAdapter sdadf = new SqlDataAdapter(cmddf);
                DataTable dtdf = new DataTable();
                sdadf.Fill(dtdf); int nb = dtdf.Rows.Count;
                if (nb != 0)
                {
                    IsReferenceFound = true;
                }
            }
            return IsReferenceFound;
        }
    }
}
