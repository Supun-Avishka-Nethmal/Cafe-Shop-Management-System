using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cafe_shop_management_system
{
    internal class cashieroderdata
    {
        public string Id {get; set; }
        public string ProductId { get; set; }
        public string ProductName {  get; set; }
        public string productType {  get; set; }       
        public string productstock { get; set; }
        public string productPrice {  get; set; }
        public string productStatus { get; set; }

        public List<cashieroderdata> cashieroderdetails()
        {
            List<cashieroderdata> list = new List<cashieroderdata>();

            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select id,prod_id,prod_name,prod_type,prod_stock,prod_price,prod_status from producttab", con);
            cmd.ExecuteNonQuery();
           SqlDataReader reader = cmd.ExecuteReader();  
            while (reader.Read())
            {
                cashieroderdata csdata = new cashieroderdata();
                csdata.Id = reader["id"].ToString();
                csdata.ProductId = reader["prod_id"].ToString();
                csdata.ProductName = reader["prod_name"].ToString();
                csdata.productType = reader["prod_type"].ToString();
                csdata.productstock = reader["prod_stock"].ToString();
                csdata.productPrice = reader["prod_price"].ToString();
                csdata.productStatus = reader["prod_status"].ToString();
                list.Add(csdata);


            }
            return list;    
            
        }

    }
}
