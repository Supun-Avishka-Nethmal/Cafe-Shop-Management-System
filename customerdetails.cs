using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cafe_shop_management_system
{

    internal class customerdetails
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");

        public int BillId {  get; set; }
        public int CustomerId { get; set; }
         public string TotalPrice { get; set; }
        public string Cash { get; set; }
        public string change {  get; set; }
        public DateTime Date { get; set; }

        public List<customerdetails> details()
        {
            List<customerdetails> list = new List<customerdetails>();

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from customertab", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customerdetails cd = new customerdetails();
                cd.BillId = (int)reader["bill_id"];
                cd.CustomerId = (int)reader["customer_id"];
                cd.TotalPrice = reader["total_price"].ToString();
                cd.Cash = reader["cash"].ToString();
                cd.change = reader["change"].ToString();
                cd.Date = (DateTime)reader["date"];
                list.Add(cd);

            }
            return list;
            con.Close();
        }


    }
}
