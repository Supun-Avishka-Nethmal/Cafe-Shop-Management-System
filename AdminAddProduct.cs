using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace cafe_shop_management_system
{
    public partial class AdminAddProduct : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
        public AdminAddProduct()
        {
            InitializeComponent();
            displaydata();
        }
        public void refreshdata()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(refreshdata));
                return;
            }

            InitializeComponent();
            displaydata();
            displaydata();
        }

            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files(*.jpg;*.png)|*.jpg;*.png";
                String filepath = "";
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    filepath = dialog.FileName;
                    pictureBox1.ImageLocation = filepath;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(" Error :" +ex,"Error Message",MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == null || comboBox2.Text == null)
                {
                    MessageBox.Show("Please Filed All Required Feild", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select * from producttab where prod_id=@prod_id", con);
                    cmd.Parameters.AddWithValue("prod_id", textBox1.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Product Id Already Taken.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DateTime today = DateTime.Today;
                        string path = Path.Combine(@"D:\C#_Projects\Basic\Cafe_Shop_Management_System\cafe_shop_management_system\product_directory\" + textBox1.Text + ".jpg");
                        String directorypath = Path.GetDirectoryName(path);
                        if (!Directory.Exists(directorypath))
                        {
                            Directory.CreateDirectory(directorypath);

                        }
                        File.Copy(pictureBox1.ImageLocation, path, true);

                        SqlCommand cmd1 = new SqlCommand("insert into producttab(prod_id,prod_name,prod_image,prod_type,prod_stock,prod_price,prod_status,date_insert,) values(@p_id,@p_name,@p_image,@p_type,@p_stock,@p_price,@p_status,@p_insert)", con);
                        cmd1.Parameters.AddWithValue("@p_id", textBox1.Text);
                        cmd1.Parameters.AddWithValue("@p_name", textBox2.Text);
                        cmd1.Parameters.AddWithValue("@p_image", path);
                        cmd1.Parameters.AddWithValue("@p_type", comboBox1.Text);
                        cmd1.Parameters.AddWithValue("@p_stock", textBox4.Text);
                        cmd1.Parameters.AddWithValue("@p_price", textBox3.Text);
                        cmd1.Parameters.AddWithValue("@p_status", comboBox2.Text);
                        cmd1.Parameters.AddWithValue("@p_insert", today);
                        cmd1.ExecuteNonQuery();
                        displaydata();

                        MessageBox.Show("Record Add Success", "Suucess Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        pictureBox1.Image = null;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Connection Fail"+ex,"Error Message",MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
            finally
            {
                con.Close();
            }
            
           
              

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            pictureBox1.Image = null;
        }

        public void displaydata()
        {
            AdminAddProductData productData = new AdminAddProductData();
            List<AdminAddProductData> list = productData.ProductList();
            dataGridView1.DataSource = list;
        }
        private int getid = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            getid = (int)row.Cells[0].Value;
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text= row.Cells[2].Value.ToString();
            comboBox1.Text= row.Cells[4].Value.ToString();
            textBox4.Text= row.Cells[5].Value.ToString();
            textBox3.Text= row.Cells[6].Value.ToString();
            comboBox2.Text = row.Cells[7].Value.ToString();
            pictureBox1.ImageLocation = row.Cells[3].Value.ToString();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            try
            {
                con.Open();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == null || comboBox2.Text == null)
                {
                    MessageBox.Show("Please Filed All Required Feild", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are You Sure Want to Update Record", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        DateTime today = DateTime.Today;

                        SqlCommand cmd1 = new SqlCommand("update producttab set prod_id=@p_id,prod_name=@p_name,prod_image=@p_image,prod_type=@p_type,prod_stock=@p_stock,prod_price=@p_price,prod_status=@p_status,date_update=@p_update where id=@id", con);
                        cmd1.Parameters.AddWithValue("@id", getid);
                        cmd1.Parameters.AddWithValue("@p_id", textBox1.Text);
                        cmd1.Parameters.AddWithValue("@p_name", textBox2.Text);
                        cmd1.Parameters.AddWithValue("@p_image", pictureBox1.ImageLocation);
                        cmd1.Parameters.AddWithValue("@p_type", comboBox1.Text);
                        cmd1.Parameters.AddWithValue("@p_stock", textBox4.Text);
                        cmd1.Parameters.AddWithValue("@p_price", textBox3.Text);
                        cmd1.Parameters.AddWithValue("@p_status", comboBox2.Text);
                        cmd1.Parameters.AddWithValue("@p_update", today);
                        cmd1.ExecuteNonQuery();
                        displaydata();

                        MessageBox.Show("Record Updatesucces", "succes Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        pictureBox1.Image = null;
                    }
                }
               


            }
            catch(Exception ex)
            {
                MessageBox.Show("connection fail"+ex,"Error Message",MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }
            finally
            {
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == null || comboBox2.Text == null)
                {
                    MessageBox.Show("Please Filed All Required Feild", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (MessageBox.Show("Are You Sure Want to Delete Record", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        DateTime today = DateTime.Today;

                        SqlCommand cmd1 = new SqlCommand("update producttab set date_delete=@date_delete where prod_id=@p_id", con);
                        cmd1.Parameters.AddWithValue("@p_id", textBox1.Text.Trim());
                        cmd1.Parameters.AddWithValue("@date_delete", today);
                        
                        cmd1.ExecuteNonQuery();
                       
                        
                       

                        MessageBox.Show("Removed Success", "succes Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        displaydata();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        pictureBox1.Image = null;
                        
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("connection fail" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

        }
    }
}
