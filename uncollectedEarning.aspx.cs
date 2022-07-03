using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace advtech.Finance.Accounta
{
    public partial class uncollectedEarning : System.Web.UI.Page
    {
        SqlDataAdapter sqlda;
        DataSet ds;
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Column"] = "date";

                    ViewState["Sortorder"] = "ASC";
                    BindBrandsRptr2(); mont.InnerText = "As of " + DateTime.Now.ToString("MMMM dd, yyyy");
                    bindcompany(); BindBrandsRptrSUM(); datTo.Visible = false; datFrom1.Visible = false; tomiddle.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void BindBrandsRptrSUMDateRange()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select sum(balance) from tblcreditnote where Date BETWEEN '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and balance >0 ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
            {
                Total.InnerText = "ETB 0.00";
            }
            else
            {
                double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                Total.InnerText = "ETB " + Payment.ToString("#,##0.00");
                con.Close();
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            datTo.Visible = true; datFrom1.Visible = true; tomiddle.Visible = true;
            datFrom1.InnerText = txtDateform.Text; datTo.InnerText = txtDateto.Text;
            mont.Visible = false;
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblcreditnote where Date BETWEEN '" + txtDateform.Text + "' and '" + txtDateto.Text + "' and balance >0 ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            BindBrandsRptrSUMDateRange();
            con.Close();
        }
        private void BindBrandsRptrSUM()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select SUM(balance) from tblcreditnote  where balance >0 ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            if (dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString() == "")
            {
                Total.InnerText = "ETB 0.00";
            }
            else
            {
                double Payment = Convert.ToDouble(dt.Rows[0][0].ToString());
                Total.InnerText = "ETB " + Payment.ToString("#,##0.00");
                con.Close();
            }
        }
        private void bindcompany()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select Oname,OAdress,Contact from tblOrganization", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string company;
                    company = reader["Oname"].ToString();
                    string address = reader["OAdress"].ToString();
                    string cont = reader["Contact"].ToString();
                    oname.InnerText = company;
                    addressname.InnerText = "Address: " + address;

                    phone.InnerText = "Contact: " + cont;
                }
            }
        }
        private void BindBrandsRptr2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblcreditnote where balance>0";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            con.Close();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblcreditnote where customer LIKE '%" + name + "%'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btnAmountCondition_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            if (greater.Checked == true)
            {
                str = "select * from tblcreditnote where balance > '" + txtFilteredAmount.Text + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
            else if (less.Checked == true)
            {
                str = "select * from tblcreditnote where balance < '" + txtFilteredAmount.Text + "' and balance >0";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                sqlda.Fill(dt);
                DataView dvData = new DataView(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == ViewState["Column"].ToString())
            {
                if (ViewState["Sortorder"].ToString() == "ASC")
                    ViewState["Sortorder"] = "DESC";
                else
                    ViewState["Sortorder"] = "ASC";
            }
            else
            {
                ViewState["Column"] = e.CommandName;
                ViewState["Sortorder"] = "ASC";
            }
            BindBrandsRptr2();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        Label lblPhone = item.FindControl("lblPhone") as Label;
                        Label lblCustomer = item.FindControl("Label2") as Label;
                        Label lblAged = item.FindControl("lblAged") as Label;
                        Label lbl = item.FindControl("lblDueDate") as Label;
                        DateTime today = DateTime.Now.Date;
                        DateTime duedate = Convert.ToDateTime(lbl.Text);
                        TimeSpan t = today - duedate;
                        string dayleft = t.TotalDays.ToString();
                        lblAged.Text = dayleft + " Days";
                        SqlCommand cmdpp = new SqlCommand("select*from tblCustomers where FllName='" + lblCustomer.Text + "' and Status='Active'", con);

                        SqlDataReader readerpp = cmdpp.ExecuteReader();
                        if (readerpp.Read())
                        {
                            string mobile;
                            mobile = readerpp["Mobile"].ToString(); readerpp.Close();
                            lblPhone.Text = mobile;
                        }
                    }
                }
            }
        }
    }
}