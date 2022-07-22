using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.Services;
using System.Web.UI.WebControls;
namespace advtech.Finance.Accounta
{
    public partial class shop_details : System.Web.UI.Page
    {
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'shop_details.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'shop_details.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    bindsearch();
                    bindimage2(); BindInfo();
                    bindshop2(); bindactivity();
                    bindcomment(); binduser();
                    bindINFO1();
                    if (Status.InnerText == "SUSPENDED")
                    {
                        A2.Visible = true;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        [WebMethod]
        private void bindsearch()
        {
            if (Request.QueryString["search"] != null)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    String PID = Convert.ToString(Request.QueryString["search"]);
                    PID = PID.Substring(2);
                    SqlCommand cmd = new SqlCommand("select * from tblshop where shopno LIKE '%" + PID + "%' ", con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            SqlDataReader reader22 = cmd.ExecuteReader();
                            if (reader22.Read())
                            {
                                string pstatus; pstatus = reader22["shopno"].ToString(); reader22.Close();

                                Response.Redirect("shop_details.aspx?ref2=" + pstatus);
                            }
                        }
                        else
                        {

                            CCF.Visible = true; container.Visible = false;
                        }
                    }

                }
            }
        }
        private static string GetData(string shop)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblshop where shopno='" + shop + "'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt.Rows[0][1].ToString();
            }
        }
        public class Products
        {


            public string area { get; set; }

        }
        private void bindshop2()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblshop where status='Free' or status='Occupied'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    ddlSHopExpand.DataSource = dt;
                    ddlSHopExpand.DataTextField = "shopno";
                    ddlSHopExpand.DataValueField = "area";
                    ddlSHopExpand.DataBind();
                    ddlSHopExpand.Items.Insert(0, new ListItem("-Select shop-", "0"));
                    ddlMergedShop.DataSource = dt;
                    ddlMergedShop.DataTextField = "shopno";
                    ddlMergedShop.DataValueField = "area";

                    ddlMergedShop.DataBind();
                    ddlMergedShop.Items.Insert(0, new ListItem("-Select shop-", "0"));
                }
            }
        }
        private void BindInfo()
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdshop = new SqlCommand("select * from tblshop where shopno='" + PID + "'", con);
                SqlDataReader readershop = cmdshop.ExecuteReader();

                if (readershop.Read())
                {
                    String shop_location = readershop["location"].ToString();
                    String status = readershop["status"].ToString();
                    String area1 = readershop["area"].ToString();
                    String rate1 = readershop["rate"].ToString();
                    String mr = readershop["monthlyprice"].ToString();
                    string descr = readershop["description"].ToString();
                    Desc.InnerText = descr;
                    if (descr == "")
                    {
                        main3.Visible = true;
                        descIcon.Visible = false;
                    }
                    else
                    {
                        main3.Visible = false;
                        descIcon.Visible = true;
                    }
                    if (shop_location == "")
                    {

                    }
                    else
                    {
                        txtLocation.Text = shop_location;
                        Location.InnerText = shop_location;
                    }
                    Status.InnerText = status;
                    if (Status.InnerText == "SUSPENDED")
                    {
                        Status.Attributes.Add("class", "badge badge-warning mx-2");
                    }
                    else if (Status.InnerText == "Occupied")
                    {
                        Status.Attributes.Add("class", "badge badge-danger mx-2");
                    }
                    else
                    {
                        Status.Attributes.Add("class", "badge badge-success mx-2");
                    }
                    area.InnerText = Convert.ToDouble(area1).ToString("#,##0.00");
                    rate.InnerText = Convert.ToDouble(rate1).ToString("#,##0.00");
                    MonthlyRate.InnerText = Convert.ToDouble(mr).ToString("#,##0.00");
                }
            }
        }
        protected string GetActiveClass(int ItemIndex)
        {
            if (ItemIndex == 0)
            {
                return "active";
            }
            else
            {
                return "";
            }
        }
        private void bindcomment()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            ShopNo.InnerText = PID;
            str = "select * from tblShopComment where shopno='" + PID + "' order by id desc";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            CommentCounter.InnerText = i.ToString();
            if (i != 0)
            {
                main.Visible = false;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                con.Close();
            }
            else
            {
                ComDiv.Visible = false;
                main.Visible = true;
            }
        }
        private void bindactivity()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            ShopNo.InnerText = PID;
            str = "select * from tblShopActivity where shopno='" + PID + "' order by id desc";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            ActiveCounter.InnerText = i.ToString();
            if (i != 0)
            {
                main1.Visible = false;
                Repeater3.DataSource = dt;
                Repeater3.DataBind();
                con.Close();
            }
            else
            {
                main1.Visible = true;
            }
        }

        private void bindimage2()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            String PID = Convert.ToString(Request.QueryString["ref2"]);

            str = "select * from tblShopIdImage where customer='" + PID + "'";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            sqlda.Fill(dt); int i = dt.Rows.Count;
            if (i != 0)
            {
                main2.Visible = false;
                Repeater2.DataSource = dt;
                Repeater2.DataBind();
                con.Close();
            }
            else
            {
                Repiv.Visible = false;
            }
        }
        protected void Button15_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                if (FileUpload2.HasFile)
                {

                    string SavePath = Server.MapPath("~/asset/shp/");
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extention = Path.GetExtension(FileUpload2.PostedFile.FileName);
                    FileUpload2.SaveAs(SavePath + "\\" + FileUpload2.FileName + Extention);
                    SqlCommand cmd246 = new SqlCommand("select*from tblShopIdImage where  pos='1'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd246);
                    DataTable dt = new DataTable(); sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd3 = new SqlCommand("insert into tblShopIdImage values('" + PID + "','" + FileUpload2.FileName + "','" + Extention + "','1')", con);
                        cmd3.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd3 = new SqlCommand("update tblShopIdImage set filename='" + FileUpload2.FileName + "',extension='" + Extention + "' where customer='" + PID + "' and pos='1'", con);
                        cmd3.ExecuteNonQuery();
                    }
                }
                if (FileUpload3.HasFile)
                {
                    string SavePath = Server.MapPath("~/asset/shp/");
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extention = Path.GetExtension(FileUpload3.PostedFile.FileName);
                    FileUpload3.SaveAs(SavePath + "\\" + FileUpload3.FileName + Extention);
                    SqlCommand cmd246 = new SqlCommand("select*from tblShopIdImage where  pos='2'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd246);
                    DataTable dt = new DataTable(); sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd3 = new SqlCommand("insert into tblShopIdImage values('" + PID + "','" + FileUpload3.FileName + "','" + Extention + "','2')", con);
                        cmd3.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd3 = new SqlCommand("update tblShopIdImage set filename='" + FileUpload3.FileName + "',extension='" + Extention + "'  where customer='" + PID + "' and pos='2'", con);
                        cmd3.ExecuteNonQuery();
                    }
                }
                if (FileUpload4.HasFile)
                {
                    string SavePath = Server.MapPath("~/asset/shp/");
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extention = Path.GetExtension(FileUpload4.PostedFile.FileName);
                    FileUpload4.SaveAs(SavePath + "\\" + FileUpload4.FileName + Extention);
                    SqlCommand cmd246 = new SqlCommand("select*from tblShopIdImage where  pos='3'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd246);
                    DataTable dt = new DataTable(); sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        SqlCommand cmd3 = new SqlCommand("insert into tblShopIdImage values('" + PID + "','" + FileUpload4.FileName + "','" + Extention + "','3')", con);
                        cmd3.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd3 = new SqlCommand("update tblShopIdImage set filename='" + FileUpload4.FileName + "',extension='" + Extention + "'  where customer='" + PID + "' and pos='3'", con);
                        cmd3.ExecuteNonQuery();
                    }
                }
                Response.Redirect("shop_details.aspx?ref2=" + PID);
            }
        }
        protected void btnExpandArea_Click(object sender, EventArgs e)
        {
            if (txtAreaExpand.Text == "")
            {
                string message = "Please assign area to be updated!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmdshop = new SqlCommand("select * from tblshop where shopno='" + PID + "'", con);
                    SqlDataReader readershop = cmdshop.ExecuteReader();

                    if (readershop.Read())
                    {
                        String location = readershop["location"].ToString();
                        String rate = readershop["rate"].ToString();
                        String area = readershop["area"].ToString(); readershop.Close();
                        double newarea = Convert.ToDouble(area) + Convert.ToDouble(txtAreaExpand.Text);
                        if (newarea < 0)
                        {
                            string message = "New area can not be negative!!";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                        }
                        else
                        {
                            //Updating the shop table to be occupied
                            double total, total2; double price;
                            price = Convert.ToDouble(rate) * newarea;
                            SqlCommand cmd4551 = new SqlCommand("Update tblshop set area='" + newarea + "',monthlyprice='" + price + "' where shopno='" + PID + "'", con);
                            cmd4551.ExecuteNonQuery();
                            SqlCommand cmdcust = new SqlCommand("select * from tblCustomers where shop='" + PID + "'", con);
                            SqlDataReader readerscust = cmdcust.ExecuteReader();
                            if (readerscust.Read())
                            {

                                String pp = readerscust["PaymentDuePeriod"].ToString();
                                String SC = readerscust["servicesharge"].ToString(); readerscust.Close();
                                //Calculate total due amount of the current shops
                                if (pp == "Monthly")
                                {
                                    total = Convert.ToDouble(SC) + price + price * 0.15;

                                }
                                else if (pp == "Every Three Month")
                                {
                                    total = Convert.ToDouble(SC) * 3 + price * 3 + price * 3 * 0.15;

                                }
                                else if (pp == "Every Six Month")
                                {
                                    total = Convert.ToDouble(SC) * 6 + price * 6 + price * 6 * 0.15;

                                }
                                else
                                {
                                    total = Convert.ToDouble(SC) * 12 + price * 12 + price * 12 * 0.15;

                                }
                                //Update Customer table and rent table
                                SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "', shopno='" + PID + "', area='" + newarea + "', price='" + price + "' where shopno='" + PID + "'", con);
                                cmdre.ExecuteNonQuery();
                                SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set shop='" + PID + "', location='" + location + "', area='" + newarea + "', price='" + price + "' where shop='" + PID + "'", con);
                                cmdre2.ExecuteNonQuery();

                                if (ddlSHopExpand.SelectedItem.Text != "-Select shop-")
                                {
                                    double newarea2;
                                    SqlCommand cmdshop1 = new SqlCommand("select * from tblshop where shopno='" + ddlSHopExpand.SelectedItem.Text + "'", con);
                                    SqlDataReader readershop1 = cmdshop1.ExecuteReader();

                                    if (readershop1.Read())
                                    {

                                        String status = readershop1["status"].ToString();
                                        String area2 = readershop1["area"].ToString();
                                        String rate2 = readershop1["rate"].ToString();
                                        newarea2 = Convert.ToDouble(area2) - Convert.ToDouble(txtAreaExpand.Text);
                                        double price2 = Convert.ToDouble(rate2) * newarea2;
                                        readershop1.Close();
                                        if (pp == "Monthly")
                                        {
                                            total = Convert.ToDouble(SC) + price + price * 0.15;
                                            total2 = Convert.ToDouble(SC) + price2 + price2 * 0.15;
                                        }
                                        else if (pp == "Every Three Month")
                                        {
                                            total = Convert.ToDouble(SC) * 3 + price * 3 + price * 3 * 0.15;
                                            total2 = Convert.ToDouble(SC) * 3 + price2 * 3 + price2 * 0.15 * 3;
                                        }
                                        else if (pp == "Every Six Month")
                                        {
                                            total = Convert.ToDouble(SC) * 6 + price * 6 + price * 6 * 0.15;
                                            total2 = Convert.ToDouble(SC) * 6 + price2 * 6 + price2 * 0.15 * 6;
                                        }
                                        else
                                        {
                                            total = Convert.ToDouble(SC) * 12 + price * 12 + price * 12 * 0.15;
                                            total2 = Convert.ToDouble(SC) * 12 + price2 * 12 + price2 * 0.15 * 12;
                                        }
                                        SqlCommand cmdc = new SqlCommand("Update tblshop set area='" + newarea2 + "',monthlyprice='" + price2 + "' where shopno='" + ddlSHopExpand.SelectedItem.Text + "'", con);
                                        cmdc.ExecuteNonQuery();
                                        if (status == "Occupied")
                                        {
                                            SqlCommand cmdreb = new SqlCommand("Update tblrent set currentperiodue='" + total2 + "', shopno='" + ddlSHopExpand.SelectedItem.Text + "', area='" + newarea2 + "', price='" + price2 + "' where shopno='" + ddlSHopExpand.SelectedItem.Text + "'", con);
                                            cmdreb.ExecuteNonQuery();
                                            SqlCommand cmdre2b = new SqlCommand("Update tblCustomers set shop='" + ddlSHopExpand.SelectedItem.Text + "', area='" + newarea2 + "', price='" + price2 + "' where shop='" + ddlSHopExpand.SelectedItem.Text + "'", con);
                                            cmdre2b.ExecuteNonQuery();
                                        }
                                    }
                                }
                                SqlCommand cmd2AC = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "'", con);
                                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                                if (readerAC.Read())
                                {
                                    String FN = readerAC["Name"].ToString();
                                    readerAC.Close();
                                    con.Close();
                                    //Activity
                                    string action = ""; string badge = ""; string headline = "";
                                    if (Convert.ToDouble(txtAreaExpand.Text) < 0)
                                    {
                                        action = "Shop area decreased by " + (-Convert.ToDouble(txtAreaExpand.Text)).ToString() + " square meter";
                                        headline = "Shop area get decreased";
                                        badge = "badge-danger";
                                    }
                                    else
                                    {
                                        action = "Shop area increased by " + (Convert.ToDouble(txtAreaExpand.Text)).ToString() + " square meter";
                                        headline = "Shop area get increased";
                                        badge = "badge-success";
                                    }

                                    SqlCommand cmdAc = new SqlCommand("insert into tblShopActivity values('" + action + "','" + FN + "',getdate(),'" + badge + "','" + headline + "','" + PID + "')", con); con.Open();

                                    cmdAc.ExecuteNonQuery();
                                }
                                
                            }
                            Response.Redirect("shop_details.aspx?ref2=" + PID);
                        }
                    }
                    else
                    {
                        readershop.Close();
                    }
                }
            }
        }
        protected void B1_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
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
                    SqlCommand cmd3 = new SqlCommand("insert into tblShopComment values(getdate(),'" + txtComment.Text + "','" + FN + "','" + PID + "')", con);

                    cmd3.ExecuteNonQuery();
                    Response.Redirect("shop_details.aspx?ref2=" + PID);
                }
            }
        }
        protected void btnMergeShop_Click(object sender, EventArgs e)
        {
            if (ddlMergedShop.Items.Count == 0)
            {
                lblMsg.Text = "All shops are ocuupied, please free one."; lblMsg.ForeColor = Color.Red;
            }
            else
            {
                if (ddlMergedShop.SelectedItem.Text == "-Select-")
                {
                    lblMsg.Text = "Please select shop."; lblMsg.ForeColor = Color.Red;
                }
                else
                {
                    if (Status.InnerText == "SUSPENDED")
                    {
                        string message = "Suspended shop can not be merged!!";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                    }
                    else
                    {
                        String PID = Convert.ToString(Request.QueryString["ref2"]);
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
                                SqlCommand cmdshop = new SqlCommand("select * from tblshop where shopno='" + PID + "'", con);
                                SqlDataReader readershop = cmdshop.ExecuteReader();

                                if (readershop.Read())
                                {
                                    String location = readershop["location"].ToString();
                                    String rate = readershop["rate"].ToString();
                                    String area = readershop["area"].ToString();
                                    double price;
                                    String status = readershop["status"].ToString();
                                    if (status == "SUSPENDED")
                                    {
                                        string message = "Suspended shop can not be merged!!";
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                                    }
                                    else
                                    {
                                        readershop.Close();
                                        SqlCommand cmdshop1 = new SqlCommand("select * from tblshop where shopno='" + ddlMergedShop.SelectedItem.Text + "'", con);
                                        SqlDataReader readershop1 = cmdshop1.ExecuteReader();

                                        if (readershop1.Read())
                                        {
                                            String areamerge = readershop1["area"].ToString();
                                            readershop1.Close();
                                            double total; double areatotal;
                                            areatotal = Convert.ToDouble(areamerge) + Convert.ToDouble(area);
                                            price = Convert.ToDouble(rate) * areatotal;
                                            //Updating the shop table to be occupied
                                            SqlCommand cmd4551 = new SqlCommand("Update tblshop set area='" + areatotal + "',monthlyprice='" + price + "' where shopno='" + PID + "'", con);
                                            cmd4551.ExecuteNonQuery();
                                            SqlCommand cmd455 = new SqlCommand("Update tblshop set status='SUSPENDED',rate='',location='',monthlyprice='',area='' where shopno='" + ddlMergedShop.SelectedItem.Text + "'", con);
                                            cmd455.ExecuteNonQuery();
                                            string action = "Shop get merged with shop number-" + ddlMergedShop.SelectedItem.Text;
                                            SqlCommand cmdAc = new SqlCommand("insert into tblShopActivity values('" + action + "','" + FN + "',getdate(),'badge badge-warning','SHOP MERGED','" + PID + "')", con);
                                            cmdAc.ExecuteNonQuery();
                                            SqlCommand cmdcust = new SqlCommand("select * from tblCustomers where shop='" + PID + "'", con);
                                            SqlDataReader readerscust = cmdcust.ExecuteReader();
                                            if (readerscust.Read())
                                            {

                                                String pp = readerscust["PaymentDuePeriod"].ToString();
                                                String SC = readerscust["servicesharge"].ToString(); readerscust.Close();
                                                //Calculate total due amount of the current shops
                                                if (pp == "Monthly")
                                                {
                                                    total = Convert.ToDouble(SC) + price + price * 0.15;
                                                }
                                                else if (pp == "Every Three Month")
                                                {
                                                    total = Convert.ToDouble(SC) * 3 + price * 3 + price * 3 * 0.15;
                                                }
                                                else if (pp == "Every Six Month")
                                                {
                                                    total = Convert.ToDouble(SC) * 6 + price * 6 + price * 6 * 0.15;
                                                }
                                                else
                                                {
                                                    total = Convert.ToDouble(SC) * 12 + price * 12 + price * 12 * 0.15;
                                                }

                                                //Update Customer table and rent table
                                                SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + total + "', shopno='" + PID + "', area='" + areatotal + "', price='" + price + "' where shopno='" + PID + "'", con);
                                                cmdre.ExecuteNonQuery();
                                                SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set shop='" + PID + "', location='" + location + "', area='" + areatotal + "', price='" + price + "' where shop='" + PID + "'", con);
                                                cmdre2.ExecuteNonQuery();
                                                Response.Redirect("shop_details.aspx?ref2=" + PID);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        protected void btnAssignArea_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (txtNewArea.Text == "" || txtRate.Text == "")
                {
                    string message = "Please assign the new area OR price!!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
                else
                {
                    double total = Convert.ToDouble(txtNewArea.Text) * Convert.ToDouble(txtRate.Text);
                    SqlCommand cmd = new SqlCommand("Update tblshop set  monthlyprice='" + total + "',area='" + txtNewArea.Text + "',rate='" + Convert.ToDouble(txtRate.Text) + "',status='Free' where shopno='" + PID + "'", con);
                    cmd.ExecuteNonQuery();
                    Response.Redirect("shop_details.aspx?ref2=" + PID);
                }
            }
        }
        protected void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            if (txtUpdatePrice.Text == "")
            {
                string message = "Please put the new price!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                double total = Convert.ToDouble(txtUpdatePrice.Text) * Convert.ToDouble(area.InnerText);
                using (SqlConnection con = new SqlConnection(CS))
                {
                    if (Status.InnerText == "Occupied")
                    {
                        con.Open();
                        SqlCommand cmd2 = new SqlCommand("Update tblCustomers set  price='" + total + "' where shop='" + PID + "'", con);
                        cmd2.ExecuteNonQuery();
                        SqlCommand cmd4 = new SqlCommand("select * from tblCustomers where shop='" + PID + "'", con);
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
                            else if (dt.Rows[i][10].ToString() == "Every Six Month")
                            {
                                if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][10].ToString() == null)
                                {
                                    double pricevat = 6 * total + 6 * total * 0.15;
                                    SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where shopno='" + dt.Rows[i][13].ToString() + "'", con);
                                    cmdre.ExecuteNonQuery();
                                }
                                else
                                {
                                    double pricevat = 6 * total + 6 * Convert.ToDouble(dt.Rows[i][21].ToString()) + 6 * total * 0.15;
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
                        SqlCommand cmd = new SqlCommand("Update tblshop set  monthlyprice='" + total + "',rate='" + Convert.ToDouble(txtUpdatePrice.Text) + "' where shopno='" + PID + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("shop_details.aspx?ref2=" + PID);
                    }
                    else if (Status.InnerText == "SUSPENDED")
                    {
                        string message = "The shop is suspended. Please assign the area & price first!!";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("Update tblshop set  monthlyprice='" + total + "',rate='" + Convert.ToDouble(txtUpdatePrice.Text) + "' where shopno='" + PID + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Response.Redirect("shop_details.aspx?ref2=" + PID);
                    }
                }
            }
        }
        private void bindINFO1()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdcrd = new SqlCommand("select * from tblCustomers where shop='" + ShopNo.InnerText + "'", con);
                SqlDataReader readercrd = cmdcrd.ExecuteReader();
                if (readercrd.Read())
                {
                    string Address2 = readercrd["FllName"].ToString();

                    readercrd.Close();
                    if (Address2 == "" || Address2 == null)
                    {

                    }
                    else
                    {
                        SI1.InnerText = Address2;
                    }
                }
            }
        }
        private void binduser()
        {
            if (Status.InnerText == "Occupied")
            {
                SI.Visible = true; SI1.Visible = true;
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
        protected void btnRemovrShop_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdAc = new SqlCommand("delete from tblshop where shopno='" + ShopNo.InnerText + "'", con);
                cmdAc.ExecuteNonQuery();

                string exlanation = "Shop# " + ShopNo.InnerText + " was deleted";
                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "','" + exlanation + "','" + BindUser() + "','" + BindUser() + "','Unseen','fas fa-trash text-white','icon-circle bg bg-danger','addshop.aspx','MN')", con);
                cmd197h.ExecuteNonQuery();
                Response.Redirect("addshop.aspx");
            }
        }
        private bool bindsusshop(string shopno)
        {
            bool existOrnot = false; String PID = Convert.ToString(Request.QueryString["ref2"]);
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
        protected void btnRenameShop_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);
            if (txtRenameShop.Text == "")
            {
                string message = "Please put the new name!!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                if (bindsusshop(txtRenameShop.Text) == true)
                {
                    string message = "Shop aleady existed!!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
                }
                else
                {

                    String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        con.Open();
                        SqlCommand cmdAc = new SqlCommand("update tblshop set shopno='" + txtRenameShop.Text + "' where shopno='" + ShopNo.InnerText + "'", con);
                        cmdAc.ExecuteNonQuery();

                        SqlCommand cmdAc1 = new SqlCommand("update tblrent set shopno='" + txtRenameShop.Text + "' where shopno='" + ShopNo.InnerText + "'", con);
                        cmdAc1.ExecuteNonQuery();

                        SqlCommand cmdAc2 = new SqlCommand("update tblCustomers set shop='" + txtRenameShop.Text + "' where shop='" + ShopNo.InnerText + "'", con);
                        cmdAc2.ExecuteNonQuery();

                        string action = "Shop name changed from " + PID + " to " + txtRenameShop.Text;
                        SqlCommand cmdAcv = new SqlCommand("insert into tblShopActivity values('" + action + "','" + BindUser() + "',getdate(),'badge badge-warning','SHOP RENAMED','" + txtRenameShop.Text + "')", con);
                        cmdAcv.ExecuteNonQuery();
                        Response.Redirect("shop_details.aspx?ref2=" + txtRenameShop.Text);
                    }
                }
            }
        }

        protected void btnDescription_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdAc = new SqlCommand("update tblshop set description=N'" + txtShopDescription.Text + "' where shopno='" + PID + "'", con);
                cmdAc.ExecuteNonQuery();
                Response.Redirect("shop_details.aspx?ref2=" + PID);
            }
        }

        protected void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdAc = new SqlCommand("update tblshop set location='" + txtLocation.Text + "' where shopno='" + PID + "'", con);
                cmdAc.ExecuteNonQuery();
                Response.Redirect("shop_details.aspx?ref2=" + PID);
            }
        }

        protected void btnRemoveImage_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdAc = new SqlCommand("delete tblShopIdImage  where customer='" + PID + "'", con);
                cmdAc.ExecuteNonQuery();
                Response.Redirect("shop_details.aspx?ref2=" + PID);
            }
        }

        protected void btnActivityRemove_Click(object sender, EventArgs e)
        {
            String PID = Convert.ToString(Request.QueryString["ref2"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdAc = new SqlCommand("delete tblShopActivity  where shopno='" + PID + "'", con);
                cmdAc.ExecuteNonQuery();
                SqlCommand cmdAc1 = new SqlCommand("delete tblShopComment  where shopno='" + PID + "'", con);
                cmdAc1.ExecuteNonQuery();
                Response.Redirect("shop_details.aspx?ref2=" + PID);
            }
        }
    }
}