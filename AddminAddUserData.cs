using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cafe_shop_management_system
{
    
    internal class AddminAddUserData
    {
        public int Id {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string roll{ get; set; }

        public string Date_insert {  get; set; }

        public List<AddminAddUserData> userListData()
        {
            List<AddminAddUserData> listdata = new List<AddminAddUserData>();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from registertab",con);
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AddminAddUserData userData = new AddminAddUserData();
                    userData.Id = (int)reader["id"];
                    userData.Username = (string)reader["username"];
                    userData.Password = (string)reader["password"];
                    userData.Status = (string)reader["status"];
                    userData.roll = (string)reader["roll"];
                    userData.Date_insert = reader["date_insert"].ToString();
                    listdata.Add(userData);
                        
                }
                con.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
             
            }
            return listdata;
        }
        
    }

    
}
