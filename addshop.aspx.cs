using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class addshop : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {

                if (!IsPostBack)
                {
                    ViewState["Column"] = "shopno";

                    ViewState["Sortorder"] = "ASC";
                    bindshop(); bindshop1();

                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private string BindUser()
        {
            string user = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    String FN = readerAC["Name"].ToString();
                    readerAC.Close();
                    user += FN;
                }
            }
            return user;
        }
        private void bindshop()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblshop";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "Cat");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 14;
            Pds1.CurrentPageIndex = CurrentPage;
            Label1.Text = "Showing Page: " + (CurrentPage + 1).ToString() + " of " + Pds1.PageCount.ToString();
            btnPrevious.Enabled = !Pds1.IsFirstPage;
            btnNext.Enabled = !Pds1.IsLastPage;
            Repeater1.DataSource = Pds1;
            Repeater1.DataBind();
            con.Close();
        }
        public int CurrentPage
        {
            get
            {
                object s1 = this.ViewState["CurrentPage"];
                if (s1 == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(s1);
                }
            }

            set { this.ViewState["CurrentPage"] = value; }
        }
        public int CurrentPage2
        {
            get
            {
                object s1 = this.ViewState["CurrentPage2"];
                if (s1 == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(s1);
                }
            }

            set { this.ViewState["CurrentPage2"] = value; }
        }
        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;

            bindshop();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            bindshop();
        }
        private void bindshop1()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblshop";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();

            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        private bool bindsusshop(string shopno)
        {
            bool existOrnot = false;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblshop where shopno='" + shopno + "'", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i = dt.Rows.Count;
                    if (i > 0)
                    {
                        existOrnot = true;
                    }
                }
            }
            return existOrnot;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (txtshopno.Text == "" || txtrate.Text == "" || txtarea.Text == "" || txtlocation.Text == "")
            {
                lblMsg.Text = "Please fill all the required input"; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                if (bindsusshop(txtshopno.Text) == true) { lblMsg.Text = "Shop already exist"; lblMsg.ForeColor = Color.Red; }
                else
                {
                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        double rate = Convert.ToDouble(txtrate.Text);
                        double monthlyrate = Convert.ToDouble(txtarea.Text) * rate;
                        SqlCommand cmd = new SqlCommand("insert into tblshop values('" + txtshopno.Text + "','" + txtlocation.Text + "','" + txtarea.Text + "','" + rate + "','" + monthlyrate + "','Free','" + txtDescription.Text + "')", con);
                        con.Open();
                        cmd.ExecuteNonQuery();

                        Response.Redirect("addshop.aspx");
                    }
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblshop where shopno LIKE '%" + name + "%'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("Label3") as Label;
                    if (lbl.Text == "Free")
                    {
                        lbl.Attributes.Add("class", "badge badge-success");
                    }
                    else if (lbl.Text == "Occupied")
                    {
                        lbl.Attributes.Add("class", "badge badge-danger");
                    }
                    else if (lbl.Text == "SUSPENDED")
                    {
                        lbl.Attributes.Add("class", "badge badge-warning");
                    }

                }
            }
        }
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            foreach (RepeaterItem item in Repeater2.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl = item.FindControl("Label3") as Label;
                    if (lbl.Text == "Free")
                    {
                        lbl.Attributes.Add("class", "badge badge-success");
                    }
                    else if (lbl.Text == "Occupied")
                    {
                        lbl.Attributes.Add("class", "badge badge-danger");
                    }
                    else if (lbl.Text == "SUSPENDED")
                    {
                        lbl.Attributes.Add("class", "badge badge-warning");
                    }

                }
            }
        }
        protected void btnBindFreeShops_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblshop where status='Free'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btnbindsus_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblshop where status='SUSPENDED'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        protected void btnbindall_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            string name = Convert.ToString(txtCustomerName.Text);
            str = "select * from tblshop";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "ID");
            DataTable dt = new DataTable();
            sqlda.Fill(dt);

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                int i_s = 0;
                foreach (RepeaterItem item in Repeater1.Items)
                {

                    CheckBox CheckRow = item.FindControl("chkRows") as CheckBox;
                    Label customerId = item.FindControl("lblCustomerId") as Label;
                    Label lblArea = item.FindControl("Label2") as Label;
                    Label rate = item.FindControl("Label1") as Label;
                    Label lblStatus = item.FindControl("Label3") as Label;
                    if (CheckRow.Checked == true)
                    {
                        i_s += 1;
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {
                            double total = Convert.ToDouble(txtNewRate.Text) * Convert.ToDouble(lblArea.Text);
                            string action = "Shop price updated from rate per month of " + Convert.ToDouble(rate.Text).ToString("#,##0.00") + " to " + Convert.ToDouble(txtNewRate.Text).ToString("#,##0.00");
                            SqlCommand cmdAc = new SqlCommand("insert into tblShopActivity values('" + action + "','" + BindUser() + "',getdate(),'badge badge-warning','SHOP RATE UPDATES','" + customerId.Text + "')", con);
                            cmdAc.ExecuteNonQuery();

                            if (lblStatus.Text == "Occupied")
                            {
                                SqlCommand cmd2 = new SqlCommand("Update tblCustomers set  price='" + total + "' where shop='" + customerId.Text + "'", con);
                                cmd2.ExecuteNonQuery();
                                SqlCommand cmd4 = new SqlCommand("select * from tblCustomers where shop='" + customerId.Text + "'", con);
                                SqlDataAdapter sda = new SqlDataAdapter(cmd4);
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][10].ToString() == "Every Three Month")
                                    {
                                        if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][10].ToString() == null)
                                        {
                                            double pricevat = 3 * total + 3 * total * 0.15;
                                            SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where shopno='" + dt.Rows[i][13].ToString() + "'", con);
                                            cmdre.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            double pricevat = 3 * total + 3 * Convert.ToDouble(dt.Rows[i][21].ToString()) + 3 * total * 0.15;
                                            SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where shopno='" + dt.Rows[i][13].ToString() + "'", con);
                                            cmdre.ExecuteNonQuery();
                                        }
                                    }
                                    else if (dt.Rows[i][10].ToString() == "Monthly")
                                    {
                                        if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][10].ToString() == null)
                                        {
                                            double pricevat = total + total * 0.15;
                                            SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where shopno='" + dt.Rows[i][13].ToString() + "'", con);
                                            cmdre.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            double pricevat = total + Convert.ToDouble(dt.Rows[i][21].ToString()) + total * 0.15;
                                            SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where shopno='" + dt.Rows[i][13].ToString() + "'", con);
                                            cmdre.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][10].ToString() == null)
                                        {
                                            double pricevat = 12 * total + 12 * total * 0.15;
                                            SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where shopno='" + dt.Rows[i][13].ToString() + "'", con);
                                            cmdre.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            double pricevat = 12 * total + 12 * Convert.ToDouble(dt.Rows[i][21].ToString()) + 12 * total * 0.15;
                                            SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where shopno='" + dt.Rows[i][13].ToString() + "'", con);
                                            cmdre.ExecuteNonQuery();
                                        }
                                    }

                                }
                                SqlCommand cmd = new SqlCommand("Update tblshop set  monthlyprice='" + total + "',area='" + lblArea.Text + "',rate='" + Convert.ToDouble(txtNewRate.Text) + "' where shopno='" + customerId.Text + "'", con);

                                cmd.ExecuteNonQuery();
                            }
                            if (lblStatus.Text == "SUSPENDED")
                            {
                                SqlCommand cmd = new SqlCommand("Update tblshop set  monthlyprice='" + total + "',area='" + lblArea.Text + "',rate='" + Convert.ToDouble(txtNewRate.Text) + "',status='Free' where shopno='" + customerId.Text + "'", con);

                                cmd.ExecuteNonQuery();

                            }
                            else
                            {
                                SqlCommand cmd = new SqlCommand("Update tblshop set  monthlyprice='" + total + "',area='" + lblArea.Text + "',rate='" + Convert.ToDouble(txtNewRate.Text) + "' where shopno='" + customerId.Text + "'", con);

                                cmd.ExecuteNonQuery();
                            }

                        }
                    }
                }
                if (i_s == 1)
                {
                    string exlanation = i_s.ToString() + " shop rate was updated";
                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-home text-white','icon-circle bg bg-danger','addshop.aspx','MN')", con);
                    cmd197h.ExecuteNonQuery();
                }
                else
                {
                    string exlanation = i_s.ToString() + " shop rates were updated";
                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-home text-white','icon-circle bg bg-danger','addshop.aspx','MN')", con);
                    cmd197h.ExecuteNonQuery();
                }
                Response.Redirect("addshop.aspx");
            }
        }
        protected void btnUncollected_Click(object sender, EventArgs e)
        {
            con.Visible = true;
            String PID = "shop_details";
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "SHOP_" + PID + "_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/x-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            con.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                int i_s = 0;
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    CheckBox CheckRow = item.FindControl("chkRows") as CheckBox;
                    Label customerId = item.FindControl("lblCustomerId") as Label;
                    Label lblStatus = item.FindControl("Label3") as Label;

                    if (CheckRow.Checked == true)
                    {
                        i_s += 1;
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {
                            if (lblStatus.Text != "Occupied")
                            {
                                SqlCommand cmdAc = new SqlCommand("delete from tblshop where shopno='" + customerId.Text + "'", con);
                                cmdAc.ExecuteNonQuery();

                                if (i_s == 1)
                                {
                                    string exlanation = i_s.ToString() + " shop was deleted";
                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-trash text-white','icon-circle bg bg-danger','addshop.aspx','MN')", con);
                                    cmd197h.ExecuteNonQuery();
                                }
                                else
                                {
                                    string exlanation = i_s.ToString() + " shops were deleted";
                                    SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-trash text-white','icon-circle bg bg-danger','addshop.aspx','MN')", con);
                                    cmd197h.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                Response.Redirect("addshop.aspx");
            }
        }
    }
}