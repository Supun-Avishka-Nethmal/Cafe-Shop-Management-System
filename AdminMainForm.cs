using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cafe_shop_management_system
{
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure Want to Logout","Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admindashboard admindash= new Admindashboard();
            admindash.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminAddUsers users = new AdminAddUsers();
            users.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminAddProduct products = new AdminAddProduct();   
            products.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            customerform cf = new customerform();
            cf.Show();
        }
    }
}
