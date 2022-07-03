using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{

    public partial class agedreceivable : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
#pragma warning disable CS0169 // The field 'agedreceivable.str2' is never used
        string str; string str2;
#pragma warning restore CS0169 // The field 'agedreceivable.str2' is never used
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'agedreceivable.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'agedreceivable.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    BindDailySales(); bindcompany(); BindBrandsRptr();

                    mont.InnerText = DateTime.Now.ToString("MMMM dd, yyyy");
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void BindBrandsRptr()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd16g = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where balance>0", con);
                SqlDataReader reader6g = cmd16g.ExecuteReader();

                if (reader6g.Read())
                {
                    string ah7g;
                    ah7g = reader6g["Balance"].ToString();
                    reader6g.Close();
                    con.Close();
                    if (ah7g == null || ah7g == "")
                    {
                        GrandTotal.InnerText = "0.00";
                    }
                    else
                    {
                        GrandTotal.InnerText = Convert.ToDouble(ah7g).ToString("#,##0.00");
                    }
                }
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
        private void BindDailySales()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select customer from tblcreditnote where balance > 0 group by customer";
            com = new SqlCommand(str, con);
            using (SqlDataAdapter sda = new SqlDataAdapter(com))
            {
                DataTable dtBrands = new DataTable();
                sda.Fill(dtBrands);
                Repeater1.DataSource = dtBrands;
                Repeater1.DataBind();
            }
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

                        Label lblcust = item.FindControl("lblCustomer") as Label;
                        Label lblone = item.FindControl("lblone") as Label;
                        Label lbltwo = item.FindControl("lbltwo") as Label;
                        Label lblthree = item.FindControl("lblthree") as Label;
                        Label lblfour = item.FindControl("lblfour") as Label;
                        Label lblTotal = item.FindControl("lblTotal") as Label;
                        Label lblOpening = item.FindControl("lblOpening") as Label;
                        string exp = lblcust.Text + "-Opening Balance";
                        DateTime daysLeft = DateTime.Now.Date;
                        string name = "Opening";
                        SqlCommand cmdop = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where Notes LIKE '%" + name + "%' and customer='" + lblcust.Text + "'", con);
                        SqlDataReader readerop = cmdop.ExecuteReader();
                        if (readerop.Read())
                        {
                            string ahop;
                            ahop = readerop["Balance"].ToString();
                            if (ahop == "" || ahop == null)
                            {
                                lblOpening.Text = "0.00";
                            }
                            else
                            {
                                lblOpening.Text = (Convert.ToDouble(ahop)).ToString("#,##0.00");
                            }

                        }
                        readerop.Close();
                        SqlCommand cmd16g = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') < 31 and customer='" + lblcust.Text + "' and balance>0", con);
                        using (SqlDataAdapter sda22c3 = new SqlDataAdapter(cmd16g))
                        {
                            DataTable dtBrands232c3 = new DataTable();
                            sda22c3.Fill(dtBrands232c3); int i2c3 = dtBrands232c3.Rows.Count;
                            if (i2c3 != 0)
                            {
                                SqlDataReader reader6g = cmd16g.ExecuteReader();

                                if (reader6g.Read())
                                {
                                    string ah7g;
                                    ah7g = reader6g["Balance"].ToString();


                                    if (ah7g == null || ah7g == "")
                                    {
                                        lblone.Text = "0.00";
                                    }
                                    else
                                    {
                                        lblone.Text = Convert.ToDouble(ah7g).ToString("#,##0.00");
                                    }
                                }
                                reader6g.Close();
                            }
                        }
                        SqlCommand cmd16 = new SqlCommand("select SUM(Balance) as Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') >30 and DATEDIFF(day, date, '" + daysLeft + "')<61 and customer='" + lblcust.Text + "' and balance>0", con);
                        SqlDataReader reader6 = cmd16.ExecuteReader();
                        if (reader6.Read())
                        {
                            string ah;
                            ah = reader6["Balance"].ToString();
                            if (ah == "")
                            {
                                lbltwo.Text = "0.00";
                            }
                            else
                            {
                                lbltwo.Text = Convert.ToDouble(ah).ToString("#,##0.00");
                            }
                        }
                        reader6.Close();
                        SqlCommand cmdtot = new SqlCommand("select SUM(Balance) Balance from tblcreditnote where customer='" + lblcust.Text + "' and balance>0", con);
                        SqlDataReader readertot = cmdtot.ExecuteReader();
                        if (readertot.Read())
                        {
                            string ahtot;
                            ahtot = readertot["Balance"].ToString();

                            if (ahtot == null || ahtot == "")
                            {
                                lblTotal.Text = "0.00";
                            }
                            else
                            {
                                lblTotal.Text = Convert.ToDouble(ahtot).ToString("#,##0.00");
                            }
                        }
                        readertot.Close();
                        SqlCommand cmd163 = new SqlCommand("select SUM(Balance) as Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') > 60  and customer='" + lblcust.Text + "'  and DATEDIFF(day, date, '" + daysLeft + "') <= 90 and balance>0", con);
                        using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd163))
                        {
                            DataTable dtBrands3 = new DataTable();
                            sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                            if (i3 != 0)
                            {
                                SqlDataReader reader63 = cmd16.ExecuteReader();
                                if (reader63.Read())
                                {
                                    string ah3;
                                    ah3 = reader63["Balance"].ToString();
                                    if (ah3 == "")
                                    {

                                    }
                                    else
                                    {
                                        lblthree.Text = Convert.ToDouble(ah3).ToString("#,##0.00");
                                    }
                                }
                                reader63.Close();
                            }
                        }
                        SqlCommand cmd1634 = new SqlCommand("select SUM(Balance) as Balance from tblcreditnote where DATEDIFF(day, date, '" + daysLeft + "') > 90  and customer='" + lblcust.Text + "' and balance>0", con);
                        using (SqlDataAdapter sda3 = new SqlDataAdapter(cmd1634))
                        {
                            DataTable dtBrands3 = new DataTable();
                            sda3.Fill(dtBrands3); int i3 = dtBrands3.Rows.Count;
                            if (i3 != 0)
                            {
                                SqlDataReader reader63 = cmd1634.ExecuteReader();
                                if (reader63.Read())
                                {
                                    string ah3;
                                    ah3 = reader63["Balance"].ToString();
                                    if (ah3 == "")
                                    {

                                    }
                                    else
                                    {
                                        lblfour.Text = Convert.ToDouble(ah3).ToString("#,##0.00");
                                    }
                                }
                                reader63.Close();
                            }
                        }
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName1.Text);
            str = "select * from tblCustomers where FllName LIKE '%" + name + "%'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
    }
}