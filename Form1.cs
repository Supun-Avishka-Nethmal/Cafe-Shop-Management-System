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
    public partial class Form1 : Form
    {
        SqlConnection con= new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
        
        public Form1()
        {
            InitializeComponent();
        }

        private byte[] encryptionkey;
        private byte[] encryptionIv;
       
     
        private void button1_Click(object sender, EventArgs e)
        {
            string en;
            try
            {
                
                    con.Open();
                if(textBox1.Text==""|| textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Please Fill All Required Fields","Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    if (textBox2.Text != textBox3.Text)
                    {
                        MessageBox.Show("Password Does Not Match", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {

                       
                        SqlCommand cmd = new SqlCommand("select * from registertab where username=@username", con);
                        cmd.Parameters.AddWithValue("@username", textBox1.Text);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if(dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Username Already Excists", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            byte[] data = Encoding.UTF8.GetBytes(textBox2.Text);
                            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
                            {
                                aes.GenerateKey();
                                aes.GenerateIV();
                                encryptionkey = aes.Key;
                                encryptionIv = aes.IV;
                                using (ICryptoTransform crypto = aes.CreateEncryptor())
                                {
                                    byte[] encryptdata = crypto.TransformFinalBlock(data, 0, data.Length);
                                     en = Convert.ToBase64String(encryptdata);

                                }
                            }
                            SqlCommand cmd1 = new SqlCommand("insert into registertab (username,password,status,roll,date_insert) values(@username,@password,@status,@roll,@date) ", con);
                            cmd1.Parameters.AddWithValue("username", textBox1.Text);
                            cmd1.Parameters.AddWithValue("@password",en);
                            cmd1.Parameters.AddWithValue("@status", "Approval");
                            cmd1.Parameters.AddWithValue("@roll", "cashier");
                            DateTime today = DateTime.Today;
                            cmd1.Parameters.AddWithValue("@date", today);
                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Register Success", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Login login = new Login();
                            login.Show();
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Conection Failer"+ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool check = checkBox1.Checked;

            switch (check)
            {
                case false:
                    textBox2.UseSystemPasswordChar = true;
                    textBox3.UseSystemPasswordChar = true;
                    break;

                default:
                    textBox2.UseSystemPasswordChar = false;
                    textBox3.UseSystemPasswordChar = false;
                    break;

            }
        }
    }
}
