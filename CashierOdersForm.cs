using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_shop_management_system
{
    public partial class CashierOdersForm : Form
    {
        public static int getcusId;
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-B67GD14I\SQLEXPRESS;Initial Catalog=cafeshopdb;Integrated Security=True");
        public CashierOdersForm()
        {
            InitializeComponent();
            displayavilableproduct();
            displaydata();
            gettotalprice();
        }
        public void refreshdata()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(refreshdata));
                return;
            }

            InitializeComponent();
            displayavilableproduct();
            displaydata();
            gettotalprice();
        }
        public void displayavilableproduct()
        {
            cashieroderdata csdata = new cashieroderdata();
            List<cashieroderdata> list = csdata.cashieroderdetails();

            dataGridView1.DataSource = list;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void displayprod_id()
        {
            try


            {
               
                comboBox2.Items.Clear();
                
                con.Open();
                SqlCommand cmd = new SqlCommand("select prod_id from producttab where prod_type=@type and prod_status=@status and date_delete IS NULL", con);
                cmd.Parameters.AddWithValue("@type", "Meal");
                cmd.Parameters.AddWithValue("@status", "Available");
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    comboBox2.Items.Add(dr["prod_id"].ToString());
                    
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro" + ex, "Error Messaage", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
       

        private void CashierOdersForm_Load(object sender, EventArgs e)
        {
            displayprod_id();
        }
        private int getid=0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            comboBox1.Text = row.Cells[3].Value.ToString();
            comboBox2.Text = row.Cells[1].Value.ToString();
            label5.Text = row.Cells[2].Value.ToString();
            label8.Text = row.Cells[5].Value.ToString();
            
        }
        private float totalprice;
        public void gettotalprice()
        {
            IDgenarate();
            con.Open();
            SqlCommand cmd = new SqlCommand("select sum(prod_price) from oderstab where customer_id=@id", con);
            cmd.Parameters.AddWithValue("@id",idgen);

            object result= cmd.ExecuteScalar(); 
            if(result != DBNull.Value )
            {
                totalprice = Convert.ToSingle(result);
            }
            
           
            label9.Text = totalprice.ToString();
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IDgenarate();
           
            if (comboBox1.Text=="" || comboBox2.Text=="" || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Please Click Cell Value First", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                try
                {
                    con.Open();

                    float getprice = 0;
                    SqlCommand cmd1 = new SqlCommand("select * from producttab where prod_id=@prod_id", con);
                    cmd1.Parameters.AddWithValue("@prod_id", comboBox2.Text);
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.Read())
                    {
                        object rawvalue = reader["prod_price"];
                        if (rawvalue != DBNull.Value)
                        {
                            getprice = Convert.ToSingle(rawvalue);
                        }

                    }
                    con.Close();

                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into oderstab(customer_id,prod_id,prod_name,prod_type,prod_price,qty,oder_date) values(@cus_id,@p_id,@p_name,@p_type,@p_price,@qty,@o_date)", con);
                    cmd.Parameters.AddWithValue("@cus_id",idgen);
                    cmd.Parameters.AddWithValue("@p_id", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@p_name", label5.Text);
                    cmd.Parameters.AddWithValue("@p_type", comboBox1.Text);
                    float totalprice =getprice* ((int)numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@p_price", totalprice);
                    cmd.Parameters.AddWithValue("@qty", numericUpDown1.Value);
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("@o_date", today);
                  
                    cmd.ExecuteNonQuery();
                   
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    label5.Text = "Test Product";
                    label8.Text = "0";
                    numericUpDown1.Value = 0;   



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {

                    con.Close();
                    gettotalprice();
                }
                displaydata();
            }
        }
         int idgen = 0;
        private void IDgenarate()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select MAX(customer_id) from customertab",con);
            object result= cmd.ExecuteScalar();

            if (result != DBNull.Value)
            {
                int temp = Convert.ToInt32(result);
                if (temp != 0)
                {
                    idgen = temp+1;
                }
                else
                {
                    idgen = 1;
                }
            }
           
          
            con.Close();
        }

        public void displaydata()
        {
            cashierodersdata csdata = new cashierodersdata();
            List<cashierodersdata> list = csdata.Cashieroderdatadetails();

            dataGridView2.DataSource = list;    
        }
      

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private float getchange;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    float getAmount = float.Parse(textBox1.Text);
                    float getchange = (getAmount - totalprice);
                  
                    if (getchange <= -1)
                    {
                        label13.Text = "";
                        textBox1.Text = "";

                    }
                    else
                    {
                        label13.Text = getchange.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid","Error Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    label13.Text = "";
                    textBox1.Text = "";
                }
            }
        }
       private int getoderid;
        private int customerId;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
            getoderid = (int)row.Cells[0].Value;
            customerId= (int)row.Cells[1].Value;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                if(getoderid == 0)
                {
                    MessageBox.Show("Select Remove Item First","Error Message",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("delete from oderstab where id=@id", con);
                    cmd.Parameters.AddWithValue("@id", getoderid);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Removed", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            displaydata();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                if (getoderid == 0)
                {
                    MessageBox.Show("Select Remove Item First", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("delete from oderstab where customer_id=@id", con);
                    cmd.Parameters.AddWithValue("@id", customerId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("All Cart Items Removed", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            displaydata();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                gettotalprice();
              
                con.Open();
                if (textBox1.Text == "" || dataGridView2.Rows.Count == 0)
                {
                    MessageBox.Show("Something Went Wrong","Error Message",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(MessageBox.Show("Are You sure pay Bill","Confirm Message",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
                {
                    SqlCommand cmd = new SqlCommand("insert into customertab(customer_id,total_price,cash,change,date) values(@cus_id,@total_price,@cash,@change,@date)", con);
                    cmd.Parameters.AddWithValue("@cus_id",idgen );
                    cmd.Parameters.AddWithValue("@total_price", totalprice);
                    cmd.Parameters.AddWithValue("@cash", textBox1.Text);
                    cmd.Parameters.AddWithValue("@change", label13.Text);
                    DateTime today = DateTime.Today;
                    cmd.Parameters.AddWithValue("date", today);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("payment Success", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label9.Text = "";
                    label13.Text = "";
                    textBox1.Text = "";
                   
                        
                   
                    
                }
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error"+ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
