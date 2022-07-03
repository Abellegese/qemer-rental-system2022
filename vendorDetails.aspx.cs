using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class vendorDetails : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
#pragma warning disable CS0169 // The field 'vendorDetails.str' is never used
        string str;
#pragma warning restore CS0169 // The field 'vendorDetails.str' is never used
#pragma warning disable CS0169 // The field 'vendorDetails.com' is never used
        SqlCommand com;
#pragma warning restore CS0169 // The field 'vendorDetails.com' is never used
#pragma warning disable CS0169 // The field 'vendorDetails.sqlda' is never used
        SqlDataAdapter sqlda;
#pragma warning restore CS0169 // The field 'vendorDetails.sqlda' is never used
#pragma warning disable CS0169 // The field 'vendorDetails.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'vendorDetails.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    BindBrandsRptr3(); ShowData2();

                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }


        private void ShowData2()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            SqlConnection con = new SqlConnection(myConnection);
            con.Open();
            String query = "select DATENAME(MONTH,DATEADD(MONTH, month(Date),-1)) as month_name ,sum (amount) as inv  from tblExpense  where YEAR(Date)='" + DateTime.Now.Year + "'  group by DATENAME(MONTH,DATEADD(MONTH, month(Date),-1))";
            SqlCommand cmd = new SqlCommand(query, con);
            using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd))
            {
                DataTable dtBrands232c3 = new DataTable();
                sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                if (i2c3 != 0)
                {
                    String chart = "";
                    chart = "<canvas id=\"line-chart\" width=\"100%\" height=\"250\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"line-chart\"), { type: 'bar', data: {labels: [";

                    // more detais

                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)

                        chart += "\"" + dtBrands232c3.Rows[i]["month_name"].ToString() + "\"" + ",";

                    chart += "],datasets: [{ data: [";

                    // get data from database and add to chart
                    String value = "";
                    for (int i = 0; i < dtBrands232c3.Rows.Count; i++)
                        value += (Convert.ToDouble(dtBrands232c3.Rows[i]["inv"])).ToString("##0.00") + ",";
                    value = value.Substring(0, value.Length - 1);

                    chart += value;

                    chart += "],label: \"Revenue\",display: false,backgroundColor: [\"#FF6955\",\"#3333FF\",\"#CCFF66\",\"#339933\", \"#FF0012\",\"#4433FF\",\"#36FF66\",\"#55CC33\",\"#546955\",\"#553300\",\"#HHFF66\",\"#669978\"],hoverBackgroundColor: \"#99FF66\"},"; // Chart color
                    chart += "]},options: {elements: {center: {text: \'Red is 2/3 of the total numbers\',color: \'#FF6384\',fontStyle: \'Arial\'}},maintainAspectRatio: false,layout: {padding: { xPadding: 15, caretPadding: 10,borderWidth: 1,yPadding: 15}},scales: {xAxes: [{display: false,gridLines: {display: false,drawBorder: false},ticks: {maxTicksLimit: 6},maxBarThickness: 25,}],},title:{display: false},legend: {display: false},cutoutPercentage: 79}"; // Chart title

                    chart += "});";

                    chart += "</script>";

                    ltChart.Text = chart; main.Visible = false;
                }
                else { main.Visible = true; ltChart.Visible = false; }

            }

        }
        protected void BindBrandsRptr3()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from tblVendor where FllName='" + PID + "'", con);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    if (reader.Read())
                    {
                        string kc; string kc1; string kc7;
                        string kc3; string kc4; string kc5; string kc6;
                        string kc8; string kc10; string kc11; string kc12;
                        kc = reader["FllName"].ToString();
                        kc1 = reader["BillingAddress"].ToString();

                        kc3 = reader["CompanyName"].ToString();
                        kc4 = reader["CustomerEmail"].ToString(); kc5 = reader["Website"].ToString();
                        kc6 = reader["Mobile"].ToString(); kc7 = reader["WorkPhone"].ToString();
                        kc8 = reader["CreditLimit"].ToString(); kc10 = reader["UnusedCredit"].ToString();
                        kc11 = reader["ContactPerson"].ToString(); kc12 = reader["PaymentDuePeriod"].ToString();
                        Span1.InnerText = kc3; Span2.InnerText = Convert.ToDouble(kc8).ToString("#,##0.00");
                        billing.InnerText = kc1; Credit.InnerText = Convert.ToDouble(kc10).ToString("#,##0.00");
                        email.InnerText = kc4; ; mobile.InnerText = kc6;
                        Span.InnerText = PID; work.InnerText = kc7;
                        L.HRef = "http://" + kc5; L.InnerText = kc5;


                        reader.Close();
                        con.Close();
                    }
                }
            }
        }
    }
}