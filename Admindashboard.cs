using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_shop_management_system
{
    public partial class Admindashboard : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
        public Admindashboard()
        {
            InitializeComponent();
            caltotalchashiers();
            totalcustomers();
            todayincome();
            totalincome();
        }

        public void refreshdata()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(refreshdata));
                return;
            }

            InitializeComponent();
            caltotalchashiers();
            totalcustomers();
            todayincome();
            totalincome();
        }

        public void caltotalchashiers()
        {
            int totalcashiers = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(username) from registertab where roll=@roll", con);
            cmd.Parameters.AddWithValue("@roll", "cashier");
            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value)
            {
                totalcashiers = (int)Convert.ToSingle(result);
            }
            label2.Text = totalcashiers.ToString();
            con.Close();
        }

        public void totalcustomers()
        {
            int totalcustomer = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(customer_id) from customertab ", con);
            
            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value)
            {
                totalcustomer = (int)Convert.ToSingle(result);
            }
            label7.Text = totalcustomer.ToString();
            con.Close();
        }

        public void todayincome()
        {
            float todayincome = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand("select sum(total_price) from customertab where date=@date", con);
            DateTime today = DateTime.Today;
            cmd.Parameters.AddWithValue("@date",today);
            object result= cmd.ExecuteScalar();
            if(result != DBNull.Value)
            {
                todayincome= Convert.ToSingle(result);
            }

            label5.Text = todayincome.ToString("c");
            con.Close();
        }

        public void totalincome()
        {

            float totalincome = 0;
            con.Open();

            SqlCommand cmd = new SqlCommand("select sum(total_price) from customertab ", con);

            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value)
            {
                totalincome = Convert.ToSingle(result);
            }
            label3.Text = (totalincome.ToString("c"));
            con.Close();
        }
    }
}
