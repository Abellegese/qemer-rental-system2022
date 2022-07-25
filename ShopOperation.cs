using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace advtech.Finance.Accounta
{
    public class ShopOperation : CustomerUtil
    {
        public string shopNo { get; set; }
        public string location { get; set; }
        public string price { get; set; }
        public string area { get; set; }
        public string status { get; set; }

        public string CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        public ShopOperation()
        {

        }
        public ShopOperation(string shopno)
        {
            this.shopNo = shopno;
        }
        public DataTable BindShop()
        {
            DataTable dt = new DataTable();
            String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from tblshop where status='Free'", con);
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            return dt;
        }
        public void UpdateShopByCustomerStatusToPrimary()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                ShopOperation shopOP = new ShopOperation(shopNo);
                //Update the infor in tblCustomer abd tblrent
                SqlCommand cmdre = new SqlCommand("Update tblrent set shopno='" + shopNo + "', area='" + shopOP.GetShopStatus().Item2 + "', price='" + shopOP.GetShopStatus().Item3 + "' where shopno='" + GetPrimaryShop() + "'", con);
                cmdre.ExecuteNonQuery();
                SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set shop='" + shopNo + "', location='" + shopOP.GetShopStatus().Item4 + "', area='" + shopOP.GetShopStatus().Item2 + "', price='" + shopOP.GetShopStatus().Item3 + "' where shop='" + GetPrimaryShop() + "'", con);
                cmdre2.ExecuteNonQuery();
                //Updating the first primary shop to secondary
                SqlCommand cmd = new SqlCommand("Update tblShopByCustomer set  status='Secondary' where customer='" + CustomerName + "'", con);
                cmd.ExecuteNonQuery();

                //Updating the new primary shop
                SqlCommand cmd2 = new SqlCommand("Update tblShopByCustomer set  status='Primary' where shopno='" + shopNo + "'", con);
                cmd2.ExecuteNonQuery();
            }
        }
        public Tuple<string, string, string, string> GetShopStatus()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2AC = new SqlCommand("select * from tblshop where shopno='" + shopNo + "'", con);
                SqlDataReader readerAC = cmd2AC.ExecuteReader();

                if (readerAC.Read())
                {
                    location = readerAC["location"].ToString();
                    area = readerAC["area"].ToString();
                    price = readerAC["monthlyprice"].ToString();
                    status = readerAC["status"].ToString();
                    con.Close();
                }
            }
            return Tuple.Create(status, area, price, location);
        }
        public void AddShop(string customer, string shopno, string status)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmdus = new SqlCommand("insert into tblShopByCustomer values('" + customer + "','" + shopno + "','" + status + "')", con);
                cmdus.ExecuteNonQuery();
                con.Close();
            }
        }
        public void UpdateShopPrice(double total,string shopno,string price)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("Update tblCustomers set  price='" + total + "' where shop='" + shopno + "'", con);
                cmd2.ExecuteNonQuery();

                SqlCommand cmd = new SqlCommand("Update tblshop set  monthlyprice='" + total + "',rate='" + price + "' where shopno='" + shopno + "'", con);
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteShopByCustomer()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete tblShopByCustomer  where shopno='" + shopNo + "'", con);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateShopByCustomerStatus()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update tblShopByCustomer set customer='"+CustomerName+"' where shopno='" + shopNo + "'", con);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateShopByCustomerNamesExchange(string customerUpdated,string shopcurrent)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update tblShopByCustomer set customer='" + customerUpdated + "' where shopno='"+shopcurrent+"'", con);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateShopByCustomerNamesExchange2(string customerUpdated, string shopcurrent)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update tblShopByCustomer set customer='" + customerUpdated + "' where shopno='" + shopcurrent + "'", con);
                cmd.ExecuteNonQuery();
            }
        }
        public void updateShopStatus(string status)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                ShopOperation shopOp = new ShopOperation();
                if (status == "Occupied")
                {
                    SqlCommand cmd = new SqlCommand("update tblshop set status='Occupied' where shopno='" + shopNo + "'", con);
                    cmd.ExecuteNonQuery();
                    shopOp.AddShop(CustomerName, shopNo, "Secondary");
                }
                if (status == "Free")
                {
                    SqlCommand cmd = new SqlCommand("update tblshop set status='Free' where shopno='" + shopNo + "'", con);
                    cmd.ExecuteNonQuery();

                    if(shopOp.GetPrimaryShop() =="" || shopOp.GetPrimaryShop() == null)
                    {
                        SqlCommand cmd_delete = new SqlCommand("delete tblShopByCustomer  where shopno='" + shopNo + "'", con);
                        cmd_delete.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd_delete = new SqlCommand("delete tblShopByCustomer  where shopno='" + shopNo + "'", con);
                        cmd_delete.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("select TOP 1 * from tblShopByCustomer where customer='" + CustomerName + "'", con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        SqlCommand cmd3 = new SqlCommand("update tblShopByCustomer set status='Primary' where shopno='" + dt.Rows[0]["shopno"].ToString() + "'", con);
                        cmd3.ExecuteNonQuery();
                        //
                        ShopOperation shopOP = new ShopOperation(dt.Rows[0]["shopno"].ToString());
                        SqlCommand cmdre = new SqlCommand("Update tblrent set shopno='" + dt.Rows[0]["shopno"].ToString() + "', area='" + shopOP.GetShopStatus().Item2 + "', price='" + shopOP.GetShopStatus().Item3 + "' where customer='" + CustomerName + "'", con);
                        cmdre.ExecuteNonQuery();
                        SqlCommand cmdre2 = new SqlCommand("Update tblCustomers set shop='" + dt.Rows[0]["shopno"].ToString() + "', location='" + shopOP.GetShopStatus().Item4 + "', area='" + shopOP.GetShopStatus().Item2 + "', price='" + shopOP.GetShopStatus().Item3 + "' where FllName='" + CustomerName + "'", con);
                        cmdre2.ExecuteNonQuery();
                    }
                }
                if (status == "SUSPENDED")
                {
                    SqlCommand cmd = new SqlCommand("update tblshop set status='SUSPENDED' where shopno='" + shopNo + "'", con);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
        public void AdjustPeriodPaymentForShopRemoved()
        {
            CustomerUtil getPaymentTerms = new CustomerUtil(Name);
            double oldBalance = Convert.ToDouble(GetCustomerRentInfo.Item2);
            double total = 0; double pricevat = 0;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select* from tblshop where shopno='" + shopNo + "'", con);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string shopAmount;
                    shopAmount = reader["monthlyprice"].ToString();  reader.Close();
                    total += Convert.ToDouble(shopAmount);
                    string pp = getPaymentTerms.GetCustomerName.Item2;
                    if (pp == "Monthly") { pricevat = oldBalance - ((total * 0.15) + total); }
                    else if (pp == "Every Three Month") { pricevat = oldBalance - ((3 * total * 0.15) + (3 * total)); }
                    else if (pp == "Every Six Month") { pricevat = oldBalance - ((total * 6 * 0.15) + (6 * total)); }
                    else { pricevat = oldBalance - ((total * 12 * 0.15) + (12 * total)); }
                    SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where customer='" + CustomerName + "'", con);
                    cmdre.ExecuteNonQuery();
                  
                }
            }
        }
        public string GetPrimaryShop()
        {
            string shopno = string.Empty;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select* from tblShopByCustomer where customer='" + CustomerName + "' and status='Primary'", con);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    
                    shopno = reader["shopno"].ToString();
                }
            }
            return shopno;
        }
        public void UpdateCustomerPeriodPayment()
        {
            CustomerUtil getPaymentTerms = new CustomerUtil(Name);
            double total = 0;double pricevat = 0;
            using (SqlConnection con = new SqlConnection(CS))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblShopByCustomer where customer='" + CustomerName + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd1 = new SqlCommand("select * from tblshop where shopno='" + dt.Rows[i]["shopno"].ToString() + "'", con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        total += Convert.ToDouble(dt1.Rows[j]["monthlyprice"]);
                    }
                }
                string pp = getPaymentTerms.GetCustomerName.Item2;
                if (pp == "Monthly") { pricevat = (total * 0.15) + total + (Convert.ToDouble(GetCustomerRentInfo.Item3)); }
                else if (pp == "Every Three Month") { pricevat = (3 * total * 0.15) + (3 * total) + (3 * Convert.ToDouble(GetCustomerRentInfo.Item3)); }
                else if (pp == "Every Six Month") { pricevat = (total * 6 * 0.15) + (6 * total) + (6 * Convert.ToDouble(GetCustomerRentInfo.Item3)); }
                else { pricevat = (total * 12 * 0.15) + (12 * total) + (12 * Convert.ToDouble(GetCustomerRentInfo.Item3)); }
                SqlCommand cmdre = new SqlCommand("Update tblrent set  price='" + total + "',monthlyvat='" + pricevat + "',currentperiodue='" + pricevat + "' where customer='"+ CustomerName + "'", con);
                cmdre.ExecuteNonQuery();
            }
        }
    }
}