using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace cafe_shop_management_system
{
    public partial class AdminAddUsers : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");

        public AdminAddUsers()
        {
            InitializeComponent();
            displadata();
        }

        public void displadata()
        {
            AddminAddUserData userData = new AddminAddUserData();
            List<AddminAddUserData> listdata = userData.userListData();
            dataGridView1.DataSource = listdata;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Items == null || comboBox2.Items == null)
                {
                    MessageBox.Show("Please Fill All Feilds", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {


                    SqlCommand cmd = new SqlCommand("select * from registertab where username=@username", con);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Username Already Excists", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("insert into registertab (username,password,status,roll,date_insert) values(@username,@password,@status,@roll,@date) ", con);
                        cmd1.Parameters.AddWithValue("username", textBox1.Text);
                        cmd1.Parameters.AddWithValue("@password", textBox2.Text);
                        cmd1.Parameters.AddWithValue("@roll", comboBox1.Text);
                        cmd1.Parameters.AddWithValue("@status", comboBox2.Text);
                        DateTime today = DateTime.Today;
                        cmd1.Parameters.AddWithValue("@date", today);
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("Record Add Success", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";

                        displadata();

                    }
                    }
                    }
            catch(Exception ex)
            {
                MessageBox.Show("Connection Failer"+ex,"Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
            finally 
            {
                con.Close();
            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Items == null || comboBox2.Items == null)
                {
                    MessageBox.Show("Please Cliclk Cell First", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

               


                    else

                    {
                    if (MessageBox.Show("Are You Sure Want to Update Record", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
                    {
                        SqlCommand cmd1 = new SqlCommand("update registertab set username=@username,password=@password,roll=@roll,status=@status where id=@id ", con);
                        cmd1.Parameters.AddWithValue("@id", getId);
                        cmd1.Parameters.AddWithValue("username", textBox1.Text);
                        cmd1.Parameters.AddWithValue("@password", textBox2.Text);
                        cmd1.Parameters.AddWithValue("@roll", comboBox1.Text);
                        cmd1.Parameters.AddWithValue("@status", comboBox2.Text);
                        DateTime today = DateTime.Today;
                        cmd1.Parameters.AddWithValue("@date", today);
                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("Record Update Success", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";

                        displadata();
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failer" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                con.Close();
            }
        
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
          
        }
        private int getId=0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            getId = (int)row.Cells[0].Value;
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            comboBox1.Text = row.Cells[4].Value.ToString();
            comboBox2.Text = row.Cells[3].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";


        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Items == null || comboBox2.Items == null)
                {
                    MessageBox.Show("Please Cliclk Cell First", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               

                 else
                    {
                    if (MessageBox.Show("Are You Sure Want to Delete Record", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        SqlCommand cmd1 = new SqlCommand("delete  from registertab where id=@id ", con);
                        cmd1.Parameters.AddWithValue("@id", getId);

                        cmd1.ExecuteNonQuery();
                        MessageBox.Show("Record Delete Success", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                    }


                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failer" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                con.Close();
            }
            displadata();
        }
    }
}
