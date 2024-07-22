using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_shop_management_system
{
  
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Please Fill All Required Fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select * from registertab where username=@username and password=@password and roll=@roll", con);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);
                    cmd.Parameters.AddWithValue("@roll", "Admin");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        AdminMainForm adminmain = new AdminMainForm();
                        adminmain.Show();

                    }
                    else 
                    {

                        SqlCommand cmd1 = new SqlCommand("select * from registertab where username=@uname and password=@pword and roll=@rolle", con);
                        cmd1.Parameters.AddWithValue("@uname", textBox1.Text);
                        cmd1.Parameters.AddWithValue("@pword", textBox2.Text);
                        cmd1.Parameters.AddWithValue("@rolle", "cashier");
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        if (dt1.Rows.Count != 0)
                        {
                            CashierMainForm cashier = new CashierMainForm();
                            cashier.Show();

                        }
                        else { 
                        MessageBox.Show("Invalid User Name Or Password","Error Message",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   

                    }
                    }
                    
                

            }
            catch(Exception ex)
            {
                MessageBox.Show("Connection Failer"+ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1= new Form1();
            form1.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBox1.Checked;

            switch (check)
            {
                case true:
                    textBox2.UseSystemPasswordChar = false;
                    break;
              default:
                    textBox2.UseSystemPasswordChar = true;
                    break;  
            }
        }
    }
}
