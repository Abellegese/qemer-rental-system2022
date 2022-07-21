using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace advtech.Finance.Accounta
{
    public partial class CustomerUtil
    {
        public string CustomerName;
        public string refernce { get; set; }
        public string paidAmount { get; set; }
        public string paymemtDueDate { get; set; }
        public string totalAmount { get; set; }
        public string creditLimit { get; set; }
        public string CN { get; set; }
        public string  CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        private string duedate { get; set; }
        public CustomerUtil()
        {
  
        }
        public CustomerUtil(string customerName)
        {
            this.CustomerName = customerName;
        }
        public CustomerUtil(string customerName,string creditNumber,string amount)
        {
            this.CustomerName = customerName;
            this.CN = creditNumber;
            this.totalAmount = amount;
        }
        public CustomerUtil(string customerName, string refere, string paid, string amount)
        {
            this.CustomerName = customerName;
            this.refernce = refere;
            this.paidAmount = paid;
            this.totalAmount = amount;
        }
        public void UpdateStatement()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + CustomerName + "' ORDER BY CSID DESC", con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string oldBalance;

                    oldBalance = reader["Balance"].ToString();
                    double newBalance = Convert.ToDouble(oldBalance) + Convert.ToDouble(paidAmount);
                    reader.Close();

                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + refernce + "','','" + totalAmount + "','" + paidAmount + "','" + newBalance + "','" + CustomerName + "')", con);
                    custcmd.ExecuteNonQuery();
                }
            }
        }
        public void DcreaseStatementWriteOff()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + CustomerName + "' ORDER BY CSID DESC", con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string oldBalance;

                    oldBalance = reader["Balance"].ToString();
                    double newBalance = Convert.ToDouble(oldBalance) - Convert.ToDouble(totalAmount);
                    reader.Close();

                    SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + refernce + "','','0','" + totalAmount + "','" + newBalance + "','" + CustomerName + "')", con);
                    custcmd.ExecuteNonQuery();
                }
            }
        }
        public Tuple<string,string,string> GetCustomerName
        {
            get
            {

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select FllName,PaymentDuePeriod,CreditLimit from tblCustomers where FllName LIKE '%" + Name + "%'", con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                this.paymemtDueDate = reader["PaymentDuePeriod"].ToString();
                                this.creditLimit = reader["CreditLimit"].ToString();
                                reader.Close();
                            }
                        }
                    }
                }
                return Tuple.Create(CustomerName, paymemtDueDate,creditLimit);
            }
        }
        public Tuple<string,string> GetCustomerRentInfo
        {
            get
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select duedate,currentperiodue from tblrent where customer = '" + Name + "'", con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                this.duedate = reader["duedate"].ToString();
                                this.totalAmount = reader["currentperiodue"].ToString();
                                reader.Close();
                            }
                        }
                    }
                }
                return Tuple.Create(duedate,totalAmount);
            }
        }
        public void UpdateDueDate()
        {
            CustomerUtil getPaymentTerms = new CustomerUtil(Name);
            CustomerUtil getdueDates = new CustomerUtil(Name);
            string pp = getPaymentTerms.GetCustomerName.Item2;
            string dueDate = getdueDates.GetCustomerRentInfo.Item1;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                if (pp == "Monthly")
                {
                    DateTime duedate1 = Convert.ToDateTime(dueDate);
                    DateTime newduedate = duedate1.AddDays(30);
                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + Name + "'", con);
                    cmdrent.ExecuteNonQuery();
                }
                else if (pp == "Every Three Month")
                {
                    DateTime duedate1 = Convert.ToDateTime(dueDate);
                    DateTime newduedate = duedate1.AddDays(90);
                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + Name + "'", con);
                    cmdrent.ExecuteNonQuery();
                }
                else if (pp == "Every Six Month")
                {
                    DateTime duedate1 = Convert.ToDateTime(dueDate);
                    DateTime newduedate = duedate1.AddDays(180);
                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + Name + "'", con);
                    cmdrent.ExecuteNonQuery();
                }
                else
                {
                    DateTime duedate1 = Convert.ToDateTime(dueDate);
                    DateTime newduedate = duedate1.AddDays(365);
                    SqlCommand cmdrent = new SqlCommand("Update tblrent set duedate='" + newduedate + "' where customer='" + Name + "'", con);
                    cmdrent.ExecuteNonQuery();
                }
            }
        }
        public void BindcustomerStatement(string customer, double NetIncome1, string expense_name, string references)
        {
            if (customer != "-Select")
            {
                String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmdreadb = new SqlCommand("select TOP 1 * from tblCustomerStatement  where Customer='" + customer + "' ORDER BY CSID DESC", con);
                    SqlDataReader readerbcustb = cmdreadb.ExecuteReader();

                    if (readerbcustb.Read())
                    {
                        string bala;

                        bala = readerbcustb["Balance"].ToString();
                        readerbcustb.Close();

                        SqlCommand custcmd = new SqlCommand("insert into tblCustomerStatement values('" + DateTime.Now + "','" + references + "','','" + NetIncome1 + "','" + NetIncome1 + "','" + bala + "','" + customer + "')", con);
                        custcmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public string Name
        {
            get { return CustomerName; }
        }
    }
}