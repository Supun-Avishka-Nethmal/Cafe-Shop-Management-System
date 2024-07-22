using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_shop_management_system
{
    internal class cashierodersdata
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
        public int OderId { get; set; }
        public int CustomerId {  get; set; }
        public string ProductId{ get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductPrice { get; set; }
        public string Quentity { get; set; }
        public string OderDate { get; set; }

       
        public List<cashierodersdata> Cashieroderdatadetails()
        {
            List<cashierodersdata> list = new List<cashierodersdata>();
            int CusId=0;
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select max(customer_id) from oderstab", con);
           
            object result= cmd1.ExecuteScalar();
            if (result != DBNull.Value)
            {
                int temp= Convert.ToInt32(result);
                if (temp == 0)
                {
                    CusId = 1;
                }
                else
                {
                    CusId = temp;
                }
            }
            
            
                SqlCommand cmd = new SqlCommand("select * from oderstab where customer_id=@cus_id", con);
                cmd.Parameters.AddWithValue("@cus_id",CusId);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                ;
                while (reader.Read())
                {
                    cashierodersdata csdata = new cashierodersdata();
                    csdata.OderId = (int)reader["id"];
                    csdata.CustomerId = (int)reader["customer_id"];
                    csdata.ProductId = reader["prod_id"].ToString();
                    csdata.ProductName = reader["prod_name"].ToString();
                    csdata.ProductType = reader["prod_type"].ToString();
                    csdata.ProductPrice = reader["prod_price"].ToString();
                    csdata.Quentity = reader["qty"].ToString();
                    csdata.OderDate = reader["oder_date"].ToString();
                    list.Add(csdata);
                }
                return list;

           con.Close();
            }
        

           

           


    }
}
