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
        public string  CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        private string duedate { get; set; }

        public CustomerUtil (string customerName)
        {
            this.CustomerName = customerName;
        }        
        public Tuple<string,string> GetCustomerName
        {
            get
            {
                string name = ""; string paymemtDueDate = "";

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select FllName,PaymentDuePeriod from tblCustomers where FllName LIKE '%" + Name + "%'", con);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt); int i = dt.Rows.Count;
                        if (i != 0)
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                name = reader["FllName"].ToString();
                                paymemtDueDate = reader["PaymentDuePeriod"].ToString();
                                reader.Close();
                            }
                        }
                    }
                }
                return Tuple.Create(name, paymemtDueDate);
            }
        }
        public string GetCustomerRentInfo
        {
            get
            {
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select duedate from tblrent where customer = '" + Name + "'", con);
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
                                reader.Close();
                            }
                        }
                    }
                }
                return duedate;
            }
        }
        public string Name
        {
            get { return CustomerName; }
        }
    }
}