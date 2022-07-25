using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.ComponentModel.DataAnnotations;

namespace advtech.Finance.Accounta
{
    public partial class CustomerUtil
    {
        public string CustomerName { get; set; }
        public string refernce { get; set; }
        public string paidAmount { get; set; }
        public string paymemtDueDate { get; set; }
        public string totalAmount { get; set; }
        public string creditLimit { get; set; }
        public string ServiceChargeAmount { get; set; }
        public string CN { get; set; }
        public bool isChecked { get; set; }
        [Required]
        public string amharicName { get; set; }

        public string  CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        private string duedate { get; set; }
        public CustomerUtil()
        {
  
        }
        public CustomerUtil(string customerName)
        {
            this.Name = customerName;
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
                con.Close();
            }
        }
        public int CustomerChecker()
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblCustomers where FllName='" + CustomerName + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt); i = dt.Rows.Count;
            }
            return i;
        }
        public void ServiceChargeBulkUpdater()
        {
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd4 = new SqlCommand("select * from tblCustomers where Status='Active'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd4);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double total = Convert.ToDouble(dt.Rows[i][16].ToString());
                    if (dt.Rows[i][10].ToString() == "Every Three Month")
                    {
                        double pricevat = 3 * total + 3 * Convert.ToDouble(ServiceChargeAmount) + 3 * total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + pricevat + "', servicecharge='" + ServiceChargeAmount + "' where customer='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + ServiceChargeAmount + "' where FllName='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                    else if (dt.Rows[i][10].ToString() == "Every Six Month")
                    {
                        double pricevat = 6 * total + 6 * Convert.ToDouble(ServiceChargeAmount) + 6 * total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set currentperiodue='" + pricevat + "', servicecharge='" + ServiceChargeAmount + "' where customer='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + ServiceChargeAmount + "' where FllName='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                    else if (dt.Rows[i][10].ToString() == "Monthly")
                    {
                        double pricevat = total + Convert.ToDouble(ServiceChargeAmount) + total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set  currentperiodue='" + pricevat + "', servicecharge='" + ServiceChargeAmount + "' where customer='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + ServiceChargeAmount + "' where FllName='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                    else
                    {
                        double pricevat = 12 * total + 12 * Convert.ToDouble(ServiceChargeAmount) + 12 * total * 0.15;
                        SqlCommand cmdre = new SqlCommand("Update tblrent set  currentperiodue='" + pricevat + "', servicecharge='" + ServiceChargeAmount + "' where customer='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set servicesharge='" + ServiceChargeAmount + "' where FllName='" + dt.Rows[i][2].ToString() + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                    if (isChecked == true)
                    {
                        if (dt.Rows[i][6].ToString() != "")
                        {

                            string accountSid = "AC329d632a8b8854e5beaefc39b3eb935b";
                            string authToken = "82f56bdc6e44bae7bc8764f4af4bea95";

                            TwilioClient.Init(accountSid, authToken);

                            var message = MessageResource.Create(
                                body: "Dear customer, Monthly service charge updated to ETB " + Convert.ToDouble(ServiceChargeAmount).ToString("#,#0.0"),
                                from: new Twilio.Types.PhoneNumber("(267) 613-9786"),
                                to: new Twilio.Types.PhoneNumber("+251" + dt.Rows[i][6].ToString())
                            );
                        }
                    }
                }
                Response.Redirect("Customer.aspx");
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
                con.Close();
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
                    con.Close();
                }
                return Tuple.Create(CustomerName, paymemtDueDate,creditLimit);
            }
        }
        public Tuple<string,string,string> GetCustomerRentInfo
        {
            get
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select servicecharge,duedate,currentperiodue from tblrent where customer = '" + Name + "'", con);
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
                                this.ServiceChargeAmount = reader["servicecharge"].ToString();
                                reader.Close();
                            }
                        }
                    }
                    con.Close();
                }
                return Tuple.Create(duedate,totalAmount,ServiceChargeAmount);
            }
        }
        public void PriceCalculator()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblCustomers where FllName='" + CustomerName + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][10].ToString() == "Every Three Month")
                    {
                        if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][21].ToString() == "" || dt.Rows[i][21].ToString() == null)
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 3 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + amharicName + "')", con);
                            cmdin.ExecuteNonQuery();
                        }
                        else
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15 + Convert.ToDouble(dt.Rows[i][21].ToString());
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 3 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + amharicName + "')", con);
                            cmdin.ExecuteNonQuery();
                        }
                    }
                    else if (dt.Rows[i][10].ToString() == "Every Six Month")
                    {
                        if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][21].ToString() == "" || dt.Rows[i][21].ToString() == null)
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 6 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + amharicName + "')", con);
                            cmdin.ExecuteNonQuery();
                        }
                        else
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15 + Convert.ToDouble(dt.Rows[i][21].ToString());
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 6 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + amharicName + "')", con);
                            cmdin.ExecuteNonQuery();
                        }
                    }
                    else if (dt.Rows[i][10].ToString() == "Monthly")
                    {
                        if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][21].ToString() == "" || dt.Rows[i][21].ToString() == null)
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 1 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + amharicName + "')", con);
                            cmdin.ExecuteNonQuery();
                        }
                        else
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15 + Convert.ToDouble(dt.Rows[i][21].ToString());
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 1 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + amharicName + "')", con);
                            cmdin.ExecuteNonQuery();
                        }
                    }
                    else if (dt.Rows[i][10].ToString() == "Yearly")
                    {
                        if (dt.Rows[i][21].ToString() == "0" || dt.Rows[i][21].ToString() == "" || dt.Rows[i][21].ToString() == null)
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15;
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 12 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + amharicName + "')", con);
                            cmdin.ExecuteNonQuery();
                        }
                        else
                        {
                            double pricevat = Convert.ToDouble(dt.Rows[i][16].ToString()) + Convert.ToDouble(dt.Rows[i][16].ToString()) * 0.15 + Convert.ToDouble(dt.Rows[i][21].ToString());
                            SqlCommand cmdin = new SqlCommand("insert into tblrent values('" + dt.Rows[i][13].ToString() + "',N'" + dt.Rows[i][2].ToString() + "',N'" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][15].ToString() + "','" + dt.Rows[i][16].ToString() + "','" + pricevat + "','" + pricevat * 12 + "','" + dt.Rows[i][17].ToString() + "','" + dt.Rows[i][19].ToString() + "','Active','" + dt.Rows[i][21].ToString() + "',N'" + amharicName + "')", con);
                            cmdin.ExecuteNonQuery();
                        }
                    }
                }
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
                con.Close();
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
                    con.Close();
                }
            }
        }
        public string Name
        {
            
            get { return CustomerName; }
            set { CustomerName = value; }
        }
    }
}