using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public class BankOperation
    {
        public string AccName { get; set; }
        public string Voucher { get; set; }
        public string Refernce { get; set; }
        public string CustomerName { get; set; }
        public double Amount { get; set; }
        public string CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        public BankOperation()
        {

        }
        public BankOperation(string accountName,string vc,string Ref,string customerName,double amount)
        {
            this.AccName = accountName;
            this.Voucher = vc;
            this.Refernce = Ref;
            this.CustomerName = customerName;
            this.Amount = amount;
        }
        public void increaseBankAccount()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdbank = new SqlCommand("select * from tblBankAccounting where AccountName='" + AccName + "' ", con);
                SqlDataReader readerbank = cmdbank.ExecuteReader();

                if (readerbank.Read())
                {
                    string bankno;
                    bankno = readerbank["AccountNumber"].ToString();
                    readerbank.Close();
                    SqlCommand cmdbank1 = new SqlCommand("select * from tblbanktrans1 where account='" + AccName + "'", con);
                    using (SqlDataAdapter sda221 = new SqlDataAdapter(cmdbank1))
                    {
                        string totalannounc = CustomerName + " Paid through bank with ref# " + Refernce;
                        DataTable dt1 = new DataTable();
                        sda221.Fill(dt1); long j = dt1.Rows.Count;
                        //
                        if (j != 0)
                        {
                            double t = Convert.ToDouble(dt1.Rows[0][5].ToString()) + Convert.ToDouble(Amount);
                            SqlCommand cmdday = new SqlCommand("Update tblbanktrans1 set balance='" + t + "' where account='" + AccName + "'", con);
                            cmdday.ExecuteNonQuery();

                            SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + Voucher + "','" + Voucher + "','" + Amount + "','0','" + t + "','" + AccName + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                            cvb.ExecuteNonQuery();
                        }
                        else
                        {
                            double t = Convert.ToDouble(AccName);
                            SqlCommand cvb = new SqlCommand("insert into tblbanktrans values('" + Voucher + "','" + Voucher + "','" + Amount + "','0','" + t + "','" + AccName + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                            cvb.ExecuteNonQuery();
                            SqlCommand cvb1 = new SqlCommand("insert into tblbanktrans values('" + Voucher + "','" + Voucher + "','" + Amount + "','0','" + t + "','" + AccName + "','','" + totalannounc + "','" + DateTime.Now.Date + "')", con);
                            cvb1.ExecuteNonQuery();

                        }
                    }
                }
            }
        }
        public void decreaseBankAccount()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();

            }
        }
    }
}