using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class NoticeLetter : System.Web.UI.Page
    {
#pragma warning disable CS0414 // The field 'NoticeLetter.ntsal' is assigned but its value is never used
        double ntsal = 0;
#pragma warning restore CS0414 // The field 'NoticeLetter.ntsal' is assigned but its value is never used
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
#pragma warning disable CS0169 // The field 'NoticeLetter.ds' is never used
        DataSet ds;
#pragma warning restore CS0169 // The field 'NoticeLetter.ds' is never used
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERNAME"] != null)
            {
                if (!IsPostBack)
                {
                    bindstatus(); div1.Visible = false; div2.Visible = false; div3.Visible = false; div4.Visible = false;
                    div5.Visible = false; div6.Visible = false; div7.Visible = false; div8.Visible = false;
                    binddiv(); bindstatuscustomer(); bindstatusall(); bind_page_marigin(); bind_text_alignment1(); bind_border_all_precedence();
                    bindLetter(); bindLetterType();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }

        private void bindLetter()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblCustomLetter", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                ddlLettrType.DataSource = dt;
                ddlLettrType.DataTextField = "letter_name";
                ddlLettrType.DataValueField = "id";
                ddlLettrType.DataBind();
                ddlLettrType.Items.Insert(0, new ListItem("Default Letter", "0"));

                //Bind Form Custom Letter Update dropdown content
                ddlCustomLetterName.DataSource = dt;
                ddlCustomLetterName.DataTextField = "letter_name";
                ddlCustomLetterName.DataValueField = "id";
                ddlCustomLetterName.DataBind();
            }
        }
        private void bind_page_marigin()
        {
            string left = ""; string right = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    left += reader["mariginleft"].ToString();
                    right += reader["mariginright"].ToString();
                    txtmariginleft.Text = left;
                    txtmariginright.Text = right;
                }
            }

            div1.Style.Add("margin-left", left + "px");
            div1.Style.Add("margin-right", right + "px");

            div2.Style.Add("margin-left", left + "px");
            div2.Style.Add("margin-right", right + "px");

            div3.Style.Add("margin-left", left + "px");
            div3.Style.Add("margin-right", right + "px");

            div4.Style.Add("margin-left", left + "px");
            div4.Style.Add("margin-right", right + "px");
        }
        protected string getethiopianDate()
        {
            DateTime gDate = DateTime.Now.Date;
            int d = DateTime.Now.Day;
            int m = DateTime.Now.Month;
            int y = DateTime.Now.Year;
            //Calculating ethiopian year
            int ethiopianYear; string ethioianDate = ""; string ethioianMonth = "";

            if (m >= 01 && m <= 08)
            {
                ethiopianYear = y - 8;
            }
            else
            {
                ethiopianYear = y - 7;
            }
            //End calculating
            if (m == 1) //January
            {
                if (ethiopianYear % 4 == 0)
                {
                    if (d < 10)
                    {
                        ethioianMonth = "04";
                        ethioianDate = (gDate.AddDays(31).AddDays(-10)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "05";
                        ethioianDate = (gDate.AddDays(-9)).Day.ToString();
                    }
                }
                else
                {
                    if (d < 9)
                    {
                        ethioianMonth = "04";
                        ethioianDate = (gDate.AddDays(31).AddDays(-9)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "05";
                        ethioianDate = (gDate.AddDays(-8)).Day.ToString();
                    }
                }
            }
            else if (m == 2)//February
            {
                if (ethiopianYear % 4 == 0)
                {
                    if (d < 10)
                    {
                        ethioianMonth = "05";
                        ethioianDate = (gDate.AddDays(31).AddDays(-9)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "06";
                        ethioianDate = (gDate.AddDays(-8)).Day.ToString();
                    }
                }
                else
                {
                    if (d < 8)
                    {
                        ethioianMonth = "05";
                        ethioianDate = (gDate.AddDays(31).AddDays(-8)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "06";
                        ethioianDate = (gDate.AddDays(-7)).Day.ToString();
                    }
                }
            }
            else if (m == 3)//March
            {
                if (d < 10)
                {
                    ethioianMonth = "06";
                    ethioianDate = (gDate.AddDays(29).AddDays(-8)).Day.ToString();
                }
                else
                {
                    ethioianMonth = "07";
                    ethioianDate = (gDate.AddDays(-9)).Day.ToString();
                }
            }
            else if (m == 4)//April
            {
                if (d < 9)
                {
                    ethioianMonth = "07";
                    ethioianDate = (gDate.AddDays(30).AddDays(-8)).Day.ToString();
                }
                else
                {
                    ethioianMonth = "08";
                    ethioianDate = (gDate.AddDays(-8)).Day.ToString();
                }
            }
            else if (m == 5)//May
            {
                if (d < 9)
                {
                    ethioianMonth = "08";
                    ethioianDate = (gDate.AddDays(30).AddDays(-8)).Day.ToString();
                }
                else
                {
                    ethioianMonth = "09";
                    ethioianDate = (gDate.AddDays(-8)).Day.ToString();
                }
            }
            else if (m == 6)//June
            {
                if (d < 8)
                {
                    ethioianMonth = "09";
                    ethioianDate = (gDate.AddDays(31).AddDays(-8)).Day.ToString();
                }
                else
                {
                    ethioianMonth = "10";
                    ethioianDate = (gDate.AddDays(-7)).Day.ToString();
                }
            }
            else if (m == 7)//July
            {
                if (d < 8)
                {
                    ethioianMonth = "10";
                    ethioianDate = (gDate.AddDays(30).AddDays(-7)).Day.ToString();
                }
                else
                {
                    ethioianMonth = "11";
                    ethioianDate = (gDate.AddDays(-7)).Day.ToString();
                }
            }
            else if (m == 8)//August
            {
                if (d < 7)
                {
                    ethioianMonth = "11";
                    ethioianDate = (gDate.AddDays(31).AddDays(-7)).Day.ToString();
                }
                else
                {
                    ethioianMonth = "12";
                    ethioianDate = (gDate.AddDays(-6)).Day.ToString();
                }
            }
            else if (m == 9)//September
            {
                if (ethiopianYear % 4 == 0)
                {
                    if (y % 4 == 0)
                    {
                        if (d < 6)
                        {
                            ethioianMonth = "12";
                            ethioianDate = (gDate.AddDays(31).AddDays(-6)).Day.ToString();
                        }
                        else
                        {
                            if (d < 12)
                            {
                                ethioianMonth = "13";
                                ethioianDate = (gDate.AddDays(-5)).Day.ToString();
                            }
                            else
                            {
                                ethioianMonth = "01";
                                ethioianDate = (gDate.AddDays(-10)).Day.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (d < 6)
                        {
                            ethioianMonth = "12";
                            ethioianDate = (gDate.AddDays(31)).AddDays(-7).Day.ToString();
                        }
                        else
                        {
                            if (d < 12)
                            {
                                ethioianMonth = "13";
                                ethioianDate = (gDate.AddDays(-6)).Day.ToString();
                            }
                            else
                            {
                                ethioianMonth = "01";
                                ethioianDate = (gDate.AddDays(-10)).Day.ToString();
                            }
                        }
                    }
                }
                else
                {
                    if (y % 4 == 0)
                    {
                        if (d < 5)
                        {
                            ethioianMonth = "12";
                            ethioianDate = (gDate.AddDays(31).AddDays(-5)).Day.ToString();
                        }
                        else
                        {
                            if (d < 11)
                            {
                                ethioianMonth = "13";
                                ethioianDate = (gDate.AddDays(-4)).Day.ToString();
                            }
                            else
                            {
                                ethioianMonth = "01";
                                ethioianDate = (gDate.AddDays(-10)).Day.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (d < 5)
                        {
                            ethioianMonth = "12";
                            ethioianDate = (gDate.AddDays(31).AddDays(-6)).Day.ToString();
                        }
                        else
                        {
                            if (d < 11)
                            {
                                ethioianMonth = "13";
                                ethioianDate = (gDate.AddDays(-5)).Day.ToString();
                            }
                            else
                            {
                                ethioianMonth = "01";
                                ethioianDate = (gDate.AddDays(-10)).Day.ToString();
                            }
                        }
                    }
                }
            }
            else if (m == 10)//October
            {
                if (ethiopianYear % 4 == 0)
                {
                    if (d < 12)
                    {
                        ethioianMonth = "01";
                        ethioianDate = (gDate.AddDays(30).AddDays(-11)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "02";
                        ethioianDate = (gDate.AddDays(-11)).Day.ToString();
                    }
                }
                else
                {
                    if (d < 11)
                    {
                        ethioianMonth = "01";
                        ethioianDate = (gDate.AddDays(30).AddDays(-10)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "02";
                        ethioianDate = (gDate.AddDays(-10)).Day.ToString();
                    }
                }
            }
            else if (m == 11)//November
            {
                if (ethiopianYear % 4 == 0)
                {
                    if (d < 11)
                    {
                        ethioianMonth = "02";
                        ethioianDate = (gDate.AddDays(31).AddDays(-11)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "03";
                        ethioianDate = (gDate.AddDays(-10)).Day.ToString();
                    }
                }
                else
                {
                    if (d < 10)
                    {
                        ethioianMonth = "02";
                        ethioianDate = (gDate.AddDays(31).AddDays(-10)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "03";
                        ethioianDate = (gDate.AddDays(-9)).Day.ToString();
                    }
                }
            }
            else if (m == 12)//December
            {
                if (ethiopianYear % 4 == 0)
                {
                    if (d < 11)
                    {
                        ethioianMonth = "03";
                        ethioianDate = (gDate.AddDays(30).AddDays(-10)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "04";
                        ethioianDate = (gDate.AddDays(-10)).Day.ToString();
                    }
                }
                else
                {
                    if (d < 10)
                    {
                        ethioianMonth = "03";
                        ethioianDate = (gDate.AddDays(30).AddDays(-9)).Day.ToString();
                    }
                    else
                    {
                        ethioianMonth = "04";
                        ethioianDate = (gDate.AddDays(-9)).Day.ToString();
                    }
                }
            }
            string completeDate = ethioianDate + "/" + ethioianMonth + "/" + ethiopianYear;
            return completeDate;
        }
        protected int GetEthYear()
        {
            int d = DateTime.Now.Month;
            int y = DateTime.Now.Year;
            if (d >= 01 && d <= 08)
            {
                int ey = y - 8;
                return ey;
            }
            else
            {
                int ey = y - 7;
                return ey;
            }
        }
        protected string GetActiveClass()
        {
            int d = DateTime.Now.Month;
            int y = DateTime.Now.Year;
            if (d >= 01 && d <= 08)
            {
                int ey = y - 8;
                string md = DateTime.Now.Date.ToString("MMdd") + "/" + ey.ToString();

                return md;
            }
            else
            {
                int ey = y - 7;
                string md = DateTime.Now.Date.ToString("MMdd") + "/" + ey.ToString();
                return md;
            }
        }
        private void bindstatuscustomer()
        {
            if (Request.QueryString["ref2"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["ref2"]);
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblrent where status='Active' and customer='" + PID + "'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable ds = new DataTable();
                sqlda.Fill(ds); int i = ds.Rows.Count;
                counter.InnerText = i.ToString();
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater2.DataSource = ds;
                Repeater2.DataBind();
                Repeater3.DataSource = ds;
                Repeater3.DataBind();
                Repeater4.DataSource = ds;
                Repeater4.DataBind();
                //Custom Letter
                Repeater5.DataSource = ds;
                Repeater5.DataBind();
                Repeater6.DataSource = ds;
                Repeater6.DataBind();
                Repeater7.DataSource = ds;
                Repeater7.DataBind();
                Repeater8.DataSource = ds;
                Repeater8.DataBind();
            }
        }
        private void bindstatus()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrent where status='Active' and DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=20";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            sqlda.Fill(ds); int i = ds.Rows.Count;
            counter.InnerText = i.ToString();



        }
        private void bindstatusall()
        {
            if (Request.QueryString["all"] != null)
            {
                SqlConnection con = new SqlConnection(strConnString);
                con.Open();
                str = "select * from tblrent where status='Active'";
                com = new SqlCommand(str, con);
                sqlda = new SqlDataAdapter(com);
                DataTable ds = new DataTable();
                sqlda.Fill(ds); int i = ds.Rows.Count;
                counter.InnerText = i.ToString();
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                Repeater2.DataSource = ds;
                Repeater2.DataBind();
                Repeater3.DataSource = ds;
                Repeater3.DataBind();
                Repeater4.DataSource = ds;
                Repeater4.DataBind();
                //Custom Letter
                Repeater5.DataSource = ds;
                Repeater5.DataBind();
                Repeater6.DataSource = ds;
                Repeater6.DataBind();
                Repeater7.DataSource = ds;
                Repeater7.DataBind();
                Repeater8.DataSource = ds;
                Repeater8.DataBind();
            }
        }
        private void binddiv()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblrent where status='Active' and DATEDIFF(day, '" + DateTime.Now.Date + "', duedate) <=20";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            DataTable ds = new DataTable();
            sqlda.Fill(ds); int i = ds.Rows.Count;

            counter.InnerText = i.ToString();
            if (Request.QueryString["p1"] != null)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                div9.Visible = false;
                div1.Visible = true;
                periodSpan.Visible = true;
                periodSpan.InnerText = "ነሃሴ እስከ ጥቅምት";
            }
            if (Request.QueryString["p2"] != null)
            {
                Repeater2.DataSource = ds;
                Repeater2.DataBind();
                div9.Visible = false;
                div2.Visible = true;

                periodSpan.Visible = true;
                periodSpan.InnerText = "ህዳር እስከ ጥር";
  
            }
            if (Request.QueryString["p3"] != null)
            {
                Repeater3.DataSource = ds;
                Repeater3.DataBind();
                div9.Visible = false;
                div3.Visible = true;
                periodSpan.Visible = true;
                periodSpan.InnerText = "የካቲት እስከ ሚያዚያ";

            }
            if (Request.QueryString["p4"] != null)
            {
                Repeater4.DataSource = ds;
                Repeater4.DataBind();
                div9.Visible = false;
                div4.Visible = true;
                periodSpan.Visible = true;
                periodSpan.InnerText = "ግንቦት እስከ ሐምሌ";

            }
            //Custom Letter
            if (Request.QueryString["pCustom1"] != null)
            {
                Repeater5.DataSource = ds;
                Repeater5.DataBind();
                div9.Visible = false;
                div5.Visible = true;
                periodSpan.Visible = true;
                periodSpan.InnerText = "ነሃሴ እስከ ጥቅምት";

            }
            if (Request.QueryString["pCustom2"] != null)
            {
                Repeater6.DataSource = ds;
                Repeater6.DataBind();
                div9.Visible = false;
                div6.Visible = true;
                periodSpan.Visible = true;
                periodSpan.InnerText = "ህዳር እስከ ጥር";

            }
            if (Request.QueryString["pCustom3"] != null)
            {
                Repeater7.DataSource = ds;
                Repeater7.DataBind();
                div9.Visible = false;
                div7.Visible = true;
                periodSpan.Visible = true;
                periodSpan.InnerText = "የካቲት እስከ ሚያዚያ";

            }
            if (Request.QueryString["pCustom4"] != null)
            {
                Repeater8.DataSource = ds;
                Repeater8.DataBind();
                div9.Visible = false;
                div8.Visible = true;
                periodSpan.Visible = true;
                periodSpan.InnerText = "ግንቦት እስከ ሐምሌ";

            }

        }
        private void bindLetterType()
        {
            if (Request.QueryString["lty"] != null)
            {
                String PID = Convert.ToString(Request.QueryString["lty"]);

                ddlLettrType.Items.Insert(1, new ListItem(PID, "1"));

            }
        }
        protected void contreturn_Click(object sender, EventArgs e)
        {
            //Show period
            periodSpan.Visible = true;
            periodSpan.InnerText = DropDownList1.SelectedItem.Text;
            string letterType = ddlLettrType.SelectedItem.Text;
            UserUtility userutil = new UserUtility();
            String FN = userutil.BindUser();
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            string url = "NoticeLetter.aspx";
            string explanation = "Letter has been generated for period " + periodSpan.InnerText+"<br/>Date:"+getethiopianDate()+ "<br/>" + "Ref No./የደ.ቁ፤- ራክሲ፡-"+GetActiveClass();
            if (Convert.ToInt32(counter.InnerText) > 0)
            {
                con.Open();
                SqlCommand cmd197h = new SqlCommand("insert into tblNotification values('" + DateTime.Now + "',N'" + explanation + "','" + FN + "','" + FN + "','Unseen','fas fa-book-open text-white','icon-circle bg bg-primary','" + url + "','MN')", con);
                cmd197h.ExecuteNonQuery();
                con.Close();
            }
            if (Checkbox2.Checked == true)
            {
                if (ddlLettrType.SelectedItem.Text == "Default Letter")
                {
                    if (DropDownList1.SelectedIndex == 0)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p1=true&&all=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p1=true&&all=true");
                        }

                    }
                    else if (DropDownList1.SelectedIndex == 1)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p2=true&&all=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p2=true&&all=true");
                        }
                    }
                    else if (DropDownList1.SelectedIndex == 2)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p3=true&&all=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p3=true&&all=true");
                        }
                    }
                    else
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p4=true&&all=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p4=true&&all=true");
                        }
                    }
                }
                else
                {
                    if (DropDownList1.SelectedIndex == 0)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?pCustom1=true&&all=true&&ref2=" + PID + "&&lty=" + letterType);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?pCustom1=true&&all=true" + "&&lty=" + letterType);
                        }

                    }
                    else if (DropDownList1.SelectedIndex == 1)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?pCustom2=true&&all=true&&ref2=" + PID + "&&lty=" + letterType);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?pCustom2=true&&all=true" + "&&lty=" + letterType);
                        }
                    }
                    else if (DropDownList1.SelectedIndex == 2)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?pCustom3=true&&all=true&&ref2=" + PID + "&&lty=" + letterType);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?pCustom3=true&&all=true" + "&&lty=" + letterType);
                        }
                    }
                    else
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?pCustom4=true&&all=true&&ref2=" + PID + "&&lty=" + letterType);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?pCustom4=true&&all=true" + "&&lty=" + letterType);
                        }
                    }
                }
            }
            else
            {
                if (ddlLettrType.SelectedItem.Text == "Default Letter")
                {
                    if (DropDownList1.SelectedIndex == 0)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p1=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p1=true");
                        }

                    }
                    else if (DropDownList1.SelectedIndex == 1)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p2=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p2=true");
                        }
                    }
                    else if (DropDownList1.SelectedIndex == 2)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p3=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p3=true");
                        }
                    }
                    else
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p4=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p4=true");
                        }
                    }
                }
                else
                {
                    if (DropDownList1.SelectedIndex == 0)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?pCustom1=true&&ref2=" + PID + "&&lty=" + letterType);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?pCustom1=true" + "&&lty=" + letterType);
                        }

                    }
                    else if (DropDownList1.SelectedIndex == 1)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?pCustom2=true&&ref2=" + PID + "&&lty=" + letterType);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?pCustom2=true" + "&&lty=" + letterType);
                        }
                    }
                    else if (DropDownList1.SelectedIndex == 2)
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?pCustom3=true&&ref2=" + PID + "&&lty=" + letterType);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?pCustom3=true" + "&&lty=" + letterType);
                        }
                    }
                    else
                    {
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?pCustom4=true&&ref2=" + PID + "&&lty=" + letterType);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?pCustom4=true" + "&&lty=" + letterType);
                        }
                    }
                }
            }
        }
        protected string bind_heading_Custom()
        {
            string text = "";
            String PID = Convert.ToString(Request.QueryString["lty"]);
            letterType.InnerText = PID;
            letterType.Visible = true;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomLetter where letter_name='" + PID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading"].ToString();
                }
            }
            return text;
        }
        protected string bind_part1_Custom()
        {
            string text = "";
            String PID = Convert.ToString(Request.QueryString["lty"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomLetter where letter_name='" + PID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["part1"].ToString();
                }
            }
            return text;
        }
        protected string bind_part2_Custom()
        {
            string text = "";
            String PID = Convert.ToString(Request.QueryString["lty"]);
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomLetter where letter_name='" + PID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["part3"].ToString();
                }
            }
            return text;
        }
        protected string bind_part3_Custom()
        {
            string text = "";
            String PID = Convert.ToString(Request.QueryString["lty"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomLetter where letter_name='" + PID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["part5"].ToString();
                }
            }
            return text;
        }
        protected string bind_part4_Custom()
        {
            string text = "";
            String PID = Convert.ToString(Request.QueryString["lty"]);

            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomLetter where letter_name='" + PID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["part7"].ToString();
                }
            }
            return text;
        }
        //Heading Customization
        protected string bind_heading_first1()
        {
            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_first'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading_text"].ToString();
                }
            }
            return text;
        }
        protected string bind_heading_first()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";

            string fs_style = ""; string lh_style = "";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_first'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh; string text;
                    text = reader["heading_text"].ToString();
                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; bold.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; italic.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; underline.Checked = true; }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    txtFontsize.Text = fs;
                    txtLineHeight.Text = lh;
                    txtHeadingEdit.Text = text;
                    txtHeadingEdit.Style.Add("font-size:", fs);
                    txtHeadingEdit.Style.Add("line-height:", lh);
                    txtHeadingEdit.Style.Add("text-decoration:", underline_style);
                    txtHeadingEdit.Style.Add("font-style:", italic_style);
                    txtHeadingEdit.Style.Add("font-weight:", bold_style);
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + "\"";
            return style;
        }
        private Tuple<bool, bool, bool> return_checkbox_heading()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (bold.Checked == true)
            {
                bold1 = true;
            }
            if (underline.Checked == true)
            {
                underline1 = true;
            }
            if (italic.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }

        //FOOTER EDIT
        protected string bind_footer()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";

            string fs_style = ""; string lh_style = "";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_footer'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh; string text;
                    text = reader["heading_text"].ToString();
                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; fb.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; fi.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; fu.Checked = true; }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    txtFooterFontSize.Text = fs;
                    txtFooterLineHeight.Text = lh;
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + "\"";
            return style;
        }
        private Tuple<bool, bool, bool> return_checkbox_footer()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (fb.Checked == true)
            {
                bold1 = true;
            }
            if (fu.Checked == true)
            {
                underline1 = true;
            }
            if (fi.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }
        //END FOOTER
        ///HEADLINE <summary>
        protected string bind_headline_text()
        {
            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_headline'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading_text"].ToString();
                }
            }
            return text;
        }
        protected string bind_headline()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";

            string fs_style = ""; string lh_style = "";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_headline'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh; string text;
                    text = reader["heading_text"].ToString();
                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; bn.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; in1.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; un.Checked = true; }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    txtNameFontSize.Text = fs;
                    txtNameSize.Text = lh;
                    txtpart1.Style.Add("font-size:", fs);
                    txtpart1.Style.Add("line-height:", lh);
                    txtpart1.Style.Add("text-decoration:", underline_style);
                    txtpart1.Style.Add("font-style:", italic_style);
                    txtpart1.Style.Add("font-weight:", bold_style);
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + "\"";
            return style;
        }
        private Tuple<bool, bool, bool> return_checkbox_headline()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (bn.Checked == true)
            {
                bold1 = true;
            }
            if (un.Checked == true)
            {
                underline1 = true;
            }
            if (in1.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }
        /// </summary>
        /// 
        ///HEAGUDAYU <summary>
        protected string bind_headgudayu_text()
        {
            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_gudayu'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading_text"].ToString();
                }
            }
            return text;
        }
        protected string bind_headgudayu()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";

            string fs_style = ""; string lh_style = "";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_gudayu'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh; string text;
                    text = reader["heading_text"].ToString();
                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; bh.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; ih.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; uh.Checked = true; }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    txtHeadFontSize.Text = fs;
                    txtHeadlineLine.Text = lh;
                    txtHeadlineEdit.Text = text;
                    txtHeadingEdit.Style.Add("font-size:", fs);
                    txtHeadingEdit.Style.Add("line-height:", lh);
                    txtHeadingEdit.Style.Add("text-decoration:", underline_style);
                    txtHeadingEdit.Style.Add("font-style:", italic_style);
                    txtHeadingEdit.Style.Add("font-weight:", bold_style);
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + "\"";
            return style;
        }
        private Tuple<bool, bool, bool> return_checkbox_headgudayu()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (bh.Checked == true)
            {
                bold1 = true;
            }
            if (uh.Checked == true)
            {
                underline1 = true;
            }
            if (ih.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }
        /// </summary>
        /// 
        ///period <summary>
        protected string bind_period_text()
        {
            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_period'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading_text"].ToString();
                }
            }
            return text;
        }
        protected string bind_period()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";
            string display = "display: none";
            string fs_style = ""; string lh_style = "";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_period'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh; string text; string visibility;
                    visibility = reader["visibility"].ToString();
                    text = reader["heading_text"].ToString();
                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; bp.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; ip.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; up.Checked = true; }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    if (visibility == "True")
                    {
                        visPeriodSec.Checked = true;
                        display = "display: noblone";
                    }
                    txtPeriodSize.Text = fs;
                    txtPeriodLine.Text = lh;
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + ";" + display + "\"";
            return style;
        }
        private Tuple<bool, bool, bool> return_checkbox_period()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (bp.Checked == true)
            {
                bold1 = true;
            }
            if (up.Checked == true)
            {
                underline1 = true;
            }
            if (ip.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }
        /// </summary>
        /// 
        ///year <summary>
        protected string bind_year_text()
        {
            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_year'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading_text"].ToString();
                }
            }
            return text;
        }
        protected string bind_year()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";

            string fs_style = ""; string lh_style = "";
            string display = "display: none";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_year'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh; string text; string visibility;
                    visibility = reader["visibility"].ToString();
                    text = reader["heading_text"].ToString();
                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; by.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; iy.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; uy.Checked = true; }


                    if (visibility == "True")
                    {
                        visYearCheck.Checked = true;
                        display = "display: noblone";
                    }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    txtYearSize.Text = fs;
                    txtYearLine.Text = lh;
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + ";" + display + "\"";
            return style;
        }
        private Tuple<bool, bool, bool> return_checkbox_year()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (by.Checked == true)
            {
                bold1 = true;
            }
            if (uy.Checked == true)
            {
                underline1 = true;
            }
            if (iy.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }
        /// </summary>

        //Money Start
        protected string bind_money()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";

            string fs_style = ""; string lh_style = "";
            string display = "display: none";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_money'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh; string text; string visibility;
                    visibility = reader["visibility"].ToString();
                    text = reader["heading_text"].ToString();
                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; mb.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; mi.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; mu.Checked = true; }


                    if (visibility == "True")
                    {
                        visMonetCheck.Checked = true;
                        display = "display: noblone";
                    }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    txtMoneyFontSize.Text = fs;
                    txtMoneyLineHeight.Text = lh;
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + ";" + display + "\"";
            return style;
        }
        private Tuple<bool, bool, bool> return_checkbox_money()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (mb.Checked == true)
            {
                bold1 = true;
            }
            if (mu.Checked == true)
            {
                underline1 = true;
            }
            if (mi.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }
        //END Modey


        ///BODIES <summary>
        protected string bind_logo_visibility()
        {
            string text = "";
            string style_content = "display:";
            string style = "style=\"" + style_content;
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text = reader["visible"].ToString();
                    if (text == "block") { logoCheck.Checked = true; }
                    else { logoCheck.Checked = false; }
                    style += text + "\"";
                }
            }
            return style;
        }
        protected string bind_body1_text()
        {
            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_body1'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading_text"].ToString();
                    txtpart2.Text = text.Replace("<br/>", "*NewLine*"); ;
                }
            }
            return text;
        }
        protected string bind_body2_text()
        {

            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_body2'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading_text"].ToString();
                    txtpart4.Text = text.Replace("<br/>", "*NewLine*"); ;
                }
            }
            return text;
        }
        protected string bind_body3_text()
        {
            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_body3'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += reader["heading_text"].ToString();
                    txtpart6.Text = text.Replace("<br/>", "*NewLine*"); ;
                }
            }
            return text;
        }
        protected string bind_body4_text()
        {
            string text = "";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_body4'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    text += Convert.ToString(reader["heading_text"].ToString());
                    txtpart7.Text = text.Replace("<br/>", "*NewLine*");
                }
            }
            return text.ToString();
        }
        protected string bind_bodyies()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";

            string fs_style = ""; string lh_style = "";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_body4'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh;

                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; bodyB.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; bodyI.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; bodyU.Checked = true; }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    txtBodySize.Text = fs;
                    txtBodyLine.Text = lh;
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + "\"";
            return style;
        }
        protected string bind_address()
        {

            string bold_style = "font-weight:normal";
            string italic_style = "font-style:normal";
            string underline_style = " text-decoration: none";

            string fs_style = ""; string lh_style = "";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizingLetter where type='heading_header'", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bold1; string italic1; string underline1; string fs; string lh;

                    bold1 = reader["bold"].ToString();
                    italic1 = reader["italic"].ToString();
                    underline1 = reader["underline"].ToString();
                    fs = reader["fontsize"].ToString();
                    lh = reader["lineheight"].ToString();
                    if (bold1 == "True") { bold_style = "font-weight:bold"; ba.Checked = true; }
                    if (italic1 == "True") { italic_style = "font-style:italic"; ia.Checked = true; }
                    if (underline1 == "True") { underline_style = "text-decoration: underline"; ua.Checked = true; }
                    fs_style += "font-size:" + fs + "px";
                    lh_style += " line-height:" + lh + "px";
                    txtAddressFontSize.Text = fs;
                    txtAddressLineHeight.Text = lh;
                }
            }
            style += bold_style + ";" + italic_style + ";" + underline_style + ";" + fs_style + ";" + lh_style + "\"";
            return style;
        }
        protected void bind_border_all_precedence()
        {

            string border_style = "border-bottom:none;border-top:none";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bottom; string top;

                    bottom = reader["borderstylebotton"].ToString();
                    top = reader["borderstyletop"].ToString();
                    border_style = "border-bottom:" + bottom + ";" + "border-top:" + top;

                    ddlBordertype.Items.Insert(0, new ListItem(bottom, "0"));
                }
            }
        }
        protected string bind_border_all()
        {

            string border_style = "border-bottom:none;border-top:none";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bottom; string top;

                    bottom = reader["borderstylebotton"].ToString();
                    top = reader["borderstyletop"].ToString();
                    border_style = "border-bottom:" + bottom + ";" + "border-top:" + top;

                }
            }
            style += border_style + "\"";
            return style;
        }
        private void bind_text_alignment1()
        {

            string border_style = "class=\"col-8 text-right\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string alignment;

                    alignment = reader["textalignment"].ToString();
                    border_style = "class=\"col-8 text-" + alignment + "\"";
                    if (alignment == "left")
                    {
                        ddlTextAlignment.Items.Insert(0, new ListItem("center", "0"));
                        ddlTextAlignment.Items.Insert(1, new ListItem("right", "1"));
                    }
                    else
                    {
                        ddlTextAlignment.Items.Insert(0, new ListItem("right", "0"));
                        ddlTextAlignment.Items.Insert(1, new ListItem("center", "1"));

                    }

                }
            }

        }
        protected string bind_text_alignment()
        {

            string border_style = "class=\"col-8 text-right\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string alignment;

                    alignment = reader["textalignment"].ToString();
                    border_style = "class=\"col-8 text-" + alignment + "\"";


                }
            }
            return border_style;
        }
        protected string bind_borderbottom()
        {

            string border_style = "border-bottom:none";
            string style = "style=\"";
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select *from tblCustomizeLetterMarigin", con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string bottom;

                    bottom = reader["borderstylebotton"].ToString();
                    border_style = "border-bottom:" + bottom;

                }
            }
            style += border_style + ";height:1250px" + "\"";
            return style;
        }
        private Tuple<bool, bool, bool> return_checkbox_bodies()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (bodyB.Checked == true)
            {
                bold1 = true;
            }
            if (bodyU.Checked == true)
            {
                underline1 = true;
            }
            if (bodyI.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }
        /// </summary>
        private Tuple<bool, bool, bool> return_checkbox_address()
        {
            bool bold1 = false; bool italic1 = false; bool underline1 = false;
            if (ba.Checked == true)
            {
                bold1 = true;
            }
            if (ua.Checked == true)
            {
                underline1 = true;
            }
            if (ia.Checked == true)
            {
                italic1 = true;
            }
            return Tuple.Create(bold1, italic1, underline1);
        }
        private bool return_year_vis()
        {
            bool logic = false;
            if (visYearCheck.Checked == true)
            {
                logic = true;
            }
            return logic;
        }
        private bool return_money_vis()
        {
            bool logic = false;
            if (visMonetCheck.Checked == true)
            {
                logic = true;
            }
            return logic;
        }
        private bool return_period_vis()
        {
            bool isVisible = false;
            if (visPeriodSec.Checked == true)
            {
                isVisible = true;
            }
            return isVisible;
        }
        protected void btnHeaderSave_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //Updates for heading
                SqlCommand cmdh = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + txtHeadingEdit.Text + "',bold='" + return_checkbox_heading().Item1 + "',italic='" + return_checkbox_heading().Item2 + "',underline='" + return_checkbox_heading().Item3 + "',fontsize='" + txtFontsize.Text + "',lineheight='" + txtLineHeight.Text + "' where type='heading_first'", con);
                cmdh.ExecuteNonQuery();
                //Updates for headline
                SqlCommand cmdhl = new SqlCommand("update tblCustomizingLetter set bold='" + return_checkbox_address().Item1 + "',italic='" + return_checkbox_address().Item2 + "',underline='" + return_checkbox_address().Item3 + "',fontsize='" + txtAddressFontSize.Text + "',lineheight='" + txtAddressLineHeight.Text + "' where type='heading_header'", con);
                cmdhl.ExecuteNonQuery();
                //Updates for footer
                SqlCommand cmdf = new SqlCommand("update tblCustomizingLetter set bold='" + return_checkbox_footer().Item1 + "',italic='" + return_checkbox_footer().Item2 + "',underline='" + return_checkbox_footer().Item3 + "',fontsize='" + txtFooterFontSize.Text + "',lineheight='" + txtFooterLineHeight.Text + "' where type='heading_footer'", con);
                cmdf.ExecuteNonQuery();

                //Updates for headline
                SqlCommand cmdborder = new SqlCommand("update tblCustomizeLetterMarigin set borderstylebotton='" + ddlBordertype.SelectedItem.Text + "',borderstyletop='" + ddlBordertype.SelectedItem.Text + "'", con);
                cmdborder.ExecuteNonQuery();
                //Updates for headline
                if (ddlTextAlignment.SelectedItem.Text == "center")
                {
                    SqlCommand cmda = new SqlCommand("update tblCustomizeLetterMarigin set textalignment='left'", con);
                    cmda.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmda = new SqlCommand("update tblCustomizeLetterMarigin set textalignment='right'", con);
                    cmda.ExecuteNonQuery();
                }
                //Visibility
                if (logoCheck.Checked == true)
                {
                    SqlCommand cmdv = new SqlCommand("update tblCustomizeLetterMarigin set visible='block'", con);
                    cmdv.ExecuteNonQuery();


                }
                else
                {
                    SqlCommand cmdv = new SqlCommand("update tblCustomizeLetterMarigin set visible='none'", con);
                    cmdv.ExecuteNonQuery();

                }
                Response.Redirect(Request.RawUrl);

            }
        }
        protected void btnBodySave_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //Updates for headline
                SqlCommand cmdhl = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + txtpart1.Text + "',bold='" + return_checkbox_headline().Item1 + "',italic='" + return_checkbox_headline().Item2 + "',underline='" + return_checkbox_headline().Item3 + "',fontsize='" + txtNameFontSize.Text + "',lineheight='" + txtNameSize.Text + "' where type='heading_headline'", con);
                cmdhl.ExecuteNonQuery();
                //Updates for headgudayu
                SqlCommand cmdg = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + txtHeadlineEdit.Text + "',bold='" + return_checkbox_headgudayu().Item1 + "',italic='" + return_checkbox_headgudayu().Item2 + "',underline='" + return_checkbox_headgudayu().Item3 + "',fontsize='" + txtHeadFontSize.Text + "',lineheight='" + txtHeadlineLine.Text + "' where type='heading_gudayu'", con);
                cmdg.ExecuteNonQuery();
                //Updates for body1
                SqlCommand cmdb1 = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + Convert.ToString(txtpart2.Text).Replace("*NewLine*","<br/>") + "',bold='" + return_checkbox_bodies().Item1 + "',italic='" + return_checkbox_bodies().Item2 + "',underline='" + return_checkbox_bodies().Item3 + "',fontsize='" + txtBodySize.Text + "',lineheight='" + txtBodyLine.Text + "' where type='heading_body1'", con);
                cmdb1.ExecuteNonQuery();
                //Updates for body2
                SqlCommand cmdb2 = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + Convert.ToString(txtpart4.Text).Replace("*NewLine*", "<br/>") + "',bold='" + return_checkbox_bodies().Item1 + "',italic='" + return_checkbox_bodies().Item2 + "',underline='" + return_checkbox_bodies().Item3 + "',fontsize='" + txtBodySize.Text + "',lineheight='" + txtBodyLine.Text + "' where type='heading_body2'", con);
                cmdb2.ExecuteNonQuery();
                //Updates for body3
                SqlCommand cmdb3 = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + Convert.ToString(txtpart6.Text).Replace("*NewLine*", "<br/>") + "',bold='" + return_checkbox_bodies().Item1 + "',italic='" + return_checkbox_bodies().Item2 + "',underline='" + return_checkbox_bodies().Item3 + "',fontsize='" + txtBodySize.Text + "',lineheight='" + txtBodyLine.Text + "' where type='heading_body3'", con);
                cmdb3.ExecuteNonQuery();
                //Updates for body4
                SqlCommand cmdb4 = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + Convert.ToString(txtpart7.Text).Replace("*NewLine*", "<br/>") + "',bold='" + return_checkbox_bodies().Item1 + "',italic='" + return_checkbox_bodies().Item2 + "',underline='" + return_checkbox_bodies().Item3 + "',fontsize='" + txtBodySize.Text + "',lineheight='" + txtBodyLine.Text + "' where type='heading_body4'", con);
                cmdb4.ExecuteNonQuery();
                //Updates for period
                SqlCommand cmdp = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + txtpart3.Text + "',bold='" + return_checkbox_period().Item1 + "',italic='" + return_checkbox_period().Item2 + "',underline='" + return_checkbox_period().Item3 + "',fontsize='" + txtPeriodSize.Text + "',lineheight='" + txtPeriodLine.Text + "',visibility='" + return_period_vis() + "' where type='heading_period'", con);
                cmdp.ExecuteNonQuery();
                //Updates for year
                SqlCommand cmdy = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + txtpart5.Text + "',bold='" + return_checkbox_year().Item1 + "',italic='" + return_checkbox_year().Item2 + "',underline='" + return_checkbox_year().Item3 + "',fontsize='" + txtYearSize.Text + "',lineheight='" + txtYearLine.Text + "',visibility='" + return_year_vis() + "' where type='heading_year'", con);
                cmdy.ExecuteNonQuery();

                //Money
                SqlCommand cmdm1 = new SqlCommand("update tblCustomizingLetter set bold='" + return_checkbox_money().Item1 + "',italic='" + return_checkbox_money().Item2 + "',underline='" + return_checkbox_money().Item3 + "',fontsize='" + txtMoneyFontSize.Text + "',lineheight='" + txtFooterLineHeight.Text + "',visibility='" + return_money_vis() + "' where type='heading_money'", con);
                cmdm1.ExecuteNonQuery();

                SqlCommand cmdm = new SqlCommand("update tblCustomizeLetterMarigin set mariginleft='" + txtmariginleft.Text + "',mariginright='" + txtmariginright.Text + "'", con);
                cmdm.ExecuteNonQuery();

                //Edit Logo visibilty

            }
            Response.Redirect(Request.RawUrl);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd163 = new SqlCommand("select*from tblCustomizingLetter", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd163))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); int i3 = dt.Rows.Count;
                    if (i3 != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            SqlCommand cmdp = new SqlCommand("update tblCustomizingLetter set heading_text=N'" + dt.Rows[i][8].ToString() + "' where type='" + dt.Rows[i][1].ToString() + "'", con);
                            cmdp.ExecuteNonQuery();
                        }
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p2=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p2=true");
                        }
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p3=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p3=true");
                        }
                        if (Request.QueryString["ref2"] != null)
                        {
                            String PID = Convert.ToString(Request.QueryString["ref2"]);
                            Response.Redirect("NoticeLetter.aspx?p4=true&&ref2=" + PID);
                        }
                        else
                        {
                            Response.Redirect("NoticeLetter.aspx?p4=true");
                        }
                    }
                }
            }
        }

        protected void btnCreateCustomLetter_Click(object sender, EventArgs e)
        {
            if (txtLetterName.Text == "")
            {
                string message = "Please provide Letter Name to continue";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cvb = new SqlCommand("insert into tblCustomLetter values(N'" + txtLetterName.Text + "',N'" + txtHeading.Text + "',N'" + txtCustomePart1.Text + "',N'" + txtCustomePart3.Text + "',N'" + txtCustomePart5.Text + "',N'" + txtCustomePart7.Text + "')", con);
                    cvb.ExecuteNonQuery();
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void btnUpdateCustomLetter_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                //Updates for headline
                SqlCommand cmdhl = new SqlCommand("update tblCustomLetter set letter_name=N'" + txtLetterName.Text + "',heading=N'" + txtHeading.Text + "'" +
                    ",part1=N'" + txtCustomePart1.Text + "',part3=N'" + txtCustomePart3.Text + "',part5=N'" + txtCustomePart5.Text + "'" +
                    ",part7=N'" + txtCustomePart7.Text + "' where letter_name='" + ddlCustomLetterName.SelectedItem.Text + "'", con);
                cmdhl.ExecuteNonQuery();
                Response.Redirect("NoticeLetter.aspx");
            }
        }
    }
}