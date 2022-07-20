using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace advtech.Finance.Accounta
{
    public class GeneralLedger
    {
        public static string accountName { get; set; }
        public static string explanation { get; set; }
        public static double amount { get; set; }

        public GeneralLedger(string accName, string expl, double val)
        {
            accountName = accName;
            explanation = expl;
            amount = val;
        }
        public GeneralLedger()
        {

        }
        public void increaseDebitAccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblGeneralLedger2 where Account='" + accountName + "'", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); long i2 = dt.Rows.Count;
                    //
                    if (i2 != 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string balance;
                            balance = reader["Balance"].ToString();
                            string accountType = reader["AccountType"].ToString();
                            reader.Close();
                            con.Close();
                            con.Open();
                            double deb = amount;
                            Double M1 = Convert.ToDouble(balance);
                            Double bl1 = M1 + deb;
                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + accountName + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + deb + "','0','" + bl1 + "','" + DateTime.Now.Date + "','" + accountName + "','','"+ accountType + "')", con);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public void decreaseDebitAccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblGeneralLedger2 where Account='" + accountName + "'", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); long i2 = dt.Rows.Count;
                    //
                    if (i2 != 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string balance;
                            balance = reader["Balance"].ToString();
                            string accountType = reader["AccountType"].ToString();
                            reader.Close();
                            con.Close();
                            con.Open();
                            double deb = amount;
                            Double M1 = Convert.ToDouble(balance);
                            Double bl1 = M1 - deb;
                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + accountName + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','0','" + deb + "','" + bl1 + "','" + DateTime.Now.Date + "','" + accountName + "','','" + accountType + "')", con);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        //
        public void increaseCreditAccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblGeneralLedger2 where Account='" + accountName + "'", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); long i2 = dt.Rows.Count;
                    //
                    if (i2 != 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string balance;
                            balance = reader["Balance"].ToString();
                            string accountType = reader["AccountType"].ToString();
                            reader.Close();
                            con.Close();
                            con.Open();
                            double deb = amount;
                            Double M1 = Convert.ToDouble(balance);
                            Double bl1 = M1 + deb;
                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + accountName + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','0','" + deb + "','" + bl1 + "','" + DateTime.Now.Date + "','" + accountName + "','','" + accountType + "')", con);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public DataTable GetAccountInfo()
        {
            DataTable dt = new DataTable();
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmds = new SqlCommand("select * from tblaccountinfo", con);
                using (SqlDataAdapter sdas = new SqlDataAdapter(cmds))
                {
                    sdas.Fill(dt);
                }
            }
            return dt;
        }
        public void decreaseCreditAccount()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblGeneralLedger2 where Account='" + accountName + "'", con);
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); long i2 = dt.Rows.Count;
                    //
                    if (i2 != 0)
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string balance;
                            balance = reader["Balance"].ToString();
                            string accountType = reader["AccountType"].ToString();
                            reader.Close();
                            con.Close();
                            con.Open();
                            double deb = amount;
                            Double M1 = Convert.ToDouble(balance);
                            Double bl1 = M1 - deb;
                            SqlCommand cmd45 = new SqlCommand("Update tblGeneralLedger2 set Balance='" + bl1 + "' where Account='" + accountName + "'", con);
                            cmd45.ExecuteNonQuery();
                            SqlCommand cmd1 = new SqlCommand("insert into tblGeneralLedger values('" + explanation + "','','" + deb + "','','" + bl1 + "','" + DateTime.Now.Date + "','" + accountName + "','','" + accountType + "')", con);
                            cmd1.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}