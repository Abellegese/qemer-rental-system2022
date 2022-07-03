using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace advtech.Finance.Accounta
{
    public partial class Account : System.Web.UI.Page
    {
        OleDbConnection Econ;
        SqlConnection con;
        string strConnString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string str;
        SqlCommand com;
        SqlDataAdapter sqlda;
        DataSet ds;
        string constr, Query, sqlconn;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["USERNAME"] != null)
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd2 = new SqlCommand("select * from Users where Username='" + Session["USERNAME"].ToString() + "' ", con);
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        string kc;
                        kc = reader["Utype"].ToString();
                        reader.Close();
                        con.Close();
                        if (kc == "Au")
                        {

                        }
                        else if (kc == "FH")
                        {

                        }
                        else
                        {

                        }
                    }
                }
                if (!IsPostBack)
                {

                    BindBrandsRptr();
                }
            }
            else
            {
                Response.Redirect("~/Login/LogIn1.aspx");
            }
        }
        private void BindBrandsRptr()
        {
            SqlConnection con = new SqlConnection(strConnString);
            con.Open();
            str = "select * from tblLedgAccTyp ";
            com = new SqlCommand(str, con);
            sqlda = new SqlDataAdapter(com);
            ds = new DataSet();
            sqlda.Fill(ds, "Cat");
            PagedDataSource Pds1 = new PagedDataSource();
            Pds1.DataSource = ds.Tables[0].DefaultView;
            Pds1.AllowPaging = true;
            Pds1.PageSize = 45;
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

            BindBrandsRptr();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindBrandsRptr();
        }
        protected void btnUpdate2_Click(object sender, EventArgs e)
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                if (RadioButton5.Text == "Dr.")
                {
                    if (RadioButton1.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','Dr.','" + DropDownList1.SelectedItem.Text + "','Income','" + DropDownList2.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','" + DropDownList3.SelectedItem.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                    if (RadioButton2.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','Dr.','" + DropDownList1.SelectedItem.Text + "','Deposit','" + DropDownList2.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','" + DropDownList3.SelectedItem.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                    if (RadioButton3.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','Dr.','" + DropDownList1.SelectedItem.Text + "','Payment','" + DropDownList2.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','" + DropDownList3.SelectedItem.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                    if (RadioButton4.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','Dr.','" + DropDownList1.SelectedItem.Text + "','None','" + DropDownList2.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','" + DropDownList3.SelectedItem.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);

                        cmd111.ExecuteNonQuery();
                    }

                }
                if (RadioButton6.Text == "Cr.")
                {
                    if (RadioButton1.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','Cr.','" + DropDownList1.SelectedItem.Text + "','Income','" + DropDownList2.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','" + DropDownList3.SelectedItem.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                    if (RadioButton2.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','Cr.','" + DropDownList1.SelectedItem.Text + "','Deposit','" + DropDownList2.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','" + DropDownList3.SelectedItem.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                    if (RadioButton3.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','Cr.','" + DropDownList1.SelectedItem.Text + "','Payment','" + DropDownList2.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','" + DropDownList3.SelectedItem.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                    if (RadioButton4.Checked == true)
                    {
                        SqlCommand cmd111 = new SqlCommand("insert into tblLedgAccTyp values('" + txtAccounttype.Text + "','" + txtAccountNo.Text + "','Cr.','" + DropDownList1.SelectedItem.Text + "','None','" + DropDownList2.SelectedItem.Text + "','" + DropDownList2.SelectedItem.Text + "','" + DropDownList3.SelectedItem.Text + "','" + DropDownList4.SelectedItem.Text + "')", con);

                        cmd111.ExecuteNonQuery();
                    }
                }
                Response.Redirect("Account.aspx");
            }
        }
        protected void Change(object sender, EventArgs e)
        {

            DropDownList2.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        private void connection()
        {
            sqlconn = ConfigurationManager.ConnectionStrings["SqlCom"].ConnectionString;
            con = new SqlConnection(sqlconn);
        }
        private void ExcelConn(string FilePath)
        {
            constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
            Econ = new OleDbConnection(constr);
        }
        private void InsertExcelRecords(string FilePath)
        {
            ExcelConn(FilePath);
            Query = string.Format("Select [Name],[No],[AccountType],[Status] ,[Remark] FROM [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(Query, Econ);
            Econ.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
            Econ.Close();
            oda.Fill(ds);
            DataTable Exceldt = ds.Tables[0];
            connection();
            //creating object of SqlBulkCopy      
            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            //assigning Destination table name      
            objbulk.DestinationTableName = "tblLedgAccTyp";
            //Mapping Table column      
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("No", "No");
            objbulk.ColumnMappings.Add("AccountType", "AccountType");
            objbulk.ColumnMappings.Add("Status", "Status");
            objbulk.ColumnMappings.Add("Remark", "Remark");
            //inserting Datatable Records to DataBase      
            con.Open();
            objbulk.WriteToServer(Exceldt);
            con.Close();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string CurrentFilePath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
            InsertExcelRecords(CurrentFilePath);
        }
    }
}