using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cafe_shop_management_system
{
    internal class AdminAddProductData
    {
        public int Id { get; set; }
        public string productId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductType { get; set; }
        public string productStock { get; set; }
        public string ProductPrice { get; set; }
        public string ProductStatus { get; set; }
        public string Date_insert { get; set; }
        public string Date_update { get; set; }
        public string Date_delete { get; set; }

        public List<AdminAddProductData> ProductList()
        {
            List<AdminAddProductData> list = new List<AdminAddProductData>();
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from producttab date_delete=@date ", con);
            cmd.Parameters.AddWithValue("@date",);

            cmd.ExecuteNonQuery();  
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AdminAddProductData productData = new AdminAddProductData();
                productData.Id = (int)reader["id"];
                productData.productId = (string)reader["prod_id"];
                productData.ProductName = (string)reader["prod_name"];
                productData.ProductImage = (string)reader["prod_image"];
                productData.ProductType = (string)reader["prod_type"];
                productData.productStock = reader["prod_stock"].ToString();
                productData.ProductPrice = reader["prod_price"].ToString();
                productData.ProductStatus = (string)reader["prod_status"];
                productData.Date_insert = reader["date_insert"].ToString();
                productData.Date_update = reader["date_update"].ToString();
                productData.Date_delete = reader["date_delete"].ToString();
                list.Add(productData);

            }
            return list;

        }
        public List<AdminAddProductData> availableproduct()
        {
            List<AdminAddProductData> list = new List<AdminAddProductData>();
            SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from producttab where prod_status=@status", con);
            cmd.Parameters.AddWithValue("@status", "Available");
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AdminAddProductData aproduct = new AdminAddProductData();
                aproduct.Id = (int)reader["id"];
                aproduct.productId = (string)reader["prod_id"];
                aproduct.ProductName = (string)reader["prod_name"];
                aproduct.ProductImage = (string)reader["prod_image"];
                aproduct.ProductType = (string)reader["prod_type"];
                aproduct.productStock = reader["prod_stock"].ToString();
                aproduct.ProductPrice = reader["prod_price"].ToString();
                aproduct.ProductStatus = (string)reader["prod_status"];
                aproduct.Date_insert = reader["date_insert"].ToString();
                aproduct.Date_update = reader["date_update"].ToString();
                aproduct.Date_delete = reader["date_delete"].ToString();
                list.Add(aproduct);

            }
            return list;
        }
    }
}
