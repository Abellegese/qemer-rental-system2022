using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class TAXreport : System.Web.UI.Page
    {
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'TAXreport.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'TAXreport.ds' is never used
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    BindBrandsRptr2(); mont.InnerText = "As of " + DateTime.Now.ToString("MMMM dd, yyyy");
                    bindcompany(); BindBrandsRptrSUM(); datTo.Visible = false; datFrom1.Visible = false; tomiddle.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void bindcompany()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select*from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company;
                    company = reader["Oname"].ToString();
                    oname.InnerText = company;
                }
            }
        }
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrentreceipt";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            con.Close();
        }
        private void BindBrandsRptrSUMDateRange()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select sum(paid) as cash from tblrentreceipt where Date BETWEEN '" + txtDateform.Text + "' and '" + txtDateto.Text + "'";
            SqlCommand cmd = new SqlCommand("select sum(paid) as cash from tblrentreceipt where Date BETWEEN '" + txtDateform.Text + "' and '" + txtDateto.Text + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string SC1;

                SC1 = reader["cash"].ToString(); reader.Close();
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt); int i = dt.Rows.Count;
                if (SC1 == null || SC1 == "")
                {
                    Total.InnerText = "ETB 0.00";
                }
                else
                {
                    double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                    double VATFREE = Payment / 1.15;
                    double VAT = Convert.ToDouble(dt.Rows[0][0].ToString())- VATFREE;
                    Total.InnerText = "ETB " + VAT.ToString("#,##0.00");
                    con.Close();
                }
            }
        }
        private void BindBrandsRptrSUM()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select sum(paid) as cash from tblrentreceipt";
            SqlCommand cmd = new SqlCommand("select sum(paid) as cash from tblrentreceipt", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string SC1;
                SC1 = reader["cash"].ToString(); reader.Close();
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt); int i = dt.Rows.Count;
                if (SC1 ==null || SC1 =="")
                {
                    Total.InnerText = "ETB 0.00";
                }
                else
                {
                    double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                    double VATFREE = Payment / 1.15;
                    double VAT = Convert.ToDouble(dt.Rows[0][0].ToString()) - VATFREE;
                    Total.InnerText = "ETB " + VAT.ToString("#,##0.00");
                    con.Close();

                }
            }
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("lblPayment") as Label;

                    Label lbl3 = item.FindControl("lblVAT") as Label;
                    double VATFREE = (Convert.ToDouble(lbl.Text)) / 1.15;

                    double VAT = (Convert.ToDouble(lbl.Text)) - VATFREE;
                    lbl3.Text = VAT.ToString("#,##0.00");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDateform.Text == "" || txtDateto.Text == "")
            {
                string message = "Select date range to bind the summary!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                datTo.Visible = true; datFrom1.Visible = true; tomiddle.Visible = true;
                datFrom1.InnerText = Convert.ToDateTime(txtDateform.Text).ToString("MMM dd, yyyy"); datTo.InnerText = Convert.ToDateTime(txtDateto.Text).ToString("MMM dd, yyyy");
                mont.Visible = false;
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblrentreceipt where Date BETWEEN '" + txtDateform.Text + "' and '" + txtDateto.Text + "' ";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);

                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                BindBrandsRptrSUMDateRange();
                con.Close();
            }
        }
    }
}