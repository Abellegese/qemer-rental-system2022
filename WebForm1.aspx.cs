using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;

namespace advtech.Finance.Accounta
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<string> GetEmployeeName(string empName)
        {
            string s = "l-";
            string s1 = "c-";
            string s3 = "s-";
            string s4 = "i-";
            string s5 = "m-";
            bool isContaincommandLedgerAccount = empName.Contains(s);
            bool isContaincommandCreateInvoice = empName.Contains(s1);
            bool isContaincommandShop = empName.Contains(s3);
            bool isContaincommandInvoice = empName.Contains(s4);
            bool isContaincommandMakePaid = empName.Contains(s5);
            List<string> empResult = new List<string>();

            String myConnection = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ToString();
            using (SqlConnection con = new SqlConnection(@myConnection))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (isContaincommandMakePaid == true)
                    {
                        string sq = empName.Substring(2);
                        cmd.CommandText = "select FllName from tblCustomers where FllName LIKE ''+@SearchEmpName+'%' and status='Active'";
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@SearchEmpName", sq);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            empResult.Add("m-" + dr["FllName"].ToString());
                        }
                        con.Close();

                    }
                    if (isContaincommandLedgerAccount == true)
                    {
                        string sq = empName.Substring(2);
                        cmd.CommandText = "select Name from tblLedgAccTyp where Name LIKE ''+@SearchEmpName+'%'";
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@SearchEmpName", sq);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            empResult.Add("l-" + dr["Name"].ToString());
                        }
                        con.Close();

                    }
                    if (isContaincommandCreateInvoice == true)
                    {
                        string sq = empName.Substring(2);
                        cmd.CommandText = "select FllName from tblCustomers where FllName LIKE ''+@SearchEmpName+'%' and status='Active'";
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@SearchEmpName", sq);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            empResult.Add("c-" + dr["FllName"].ToString());
                        }
                        con.Close();

                    }
                    if (isContaincommandShop == true)
                    {
                        string sq = empName.Substring(2);
                        cmd.CommandText = "select shopno from tblshop where shopno LIKE ''+@SearchEmpName+'%'";
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@SearchEmpName", sq);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            empResult.Add("s-" + dr["shopno"].ToString());
                        }
                        con.Close();

                    }
                    if (isContaincommandInvoice == true)
                    {
                        string sq = empName.Substring(2);
                        cmd.CommandText = "select id2 from tblrentreceipt where id2 LIKE ''+@SearchEmpName+'%' or references1 LIKE ''+@SearchEmpName+'%'";
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@SearchEmpName", sq);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            empResult.Add("i-" + dr["id2"].ToString());
                        }
                        con.Close();

                    }
                    if (isContaincommandInvoice == false && isContaincommandCreateInvoice == false && isContaincommandLedgerAccount == false && isContaincommandShop == false && isContaincommandMakePaid == false)
                    {
                        cmd.CommandText = "select FllName from tblCustomers where FllName LIKE ''+@SearchEmpName+'%'";
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@SearchEmpName", empName);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            empResult.Add(dr["FllName"].ToString());
                        }
                    }
                    return empResult;
                }
            }
        }
    }
}