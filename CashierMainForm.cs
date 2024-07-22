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
    public partial class CashierMainForm : Form
    {
        public CashierMainForm()
        {
            InitializeComponent();
        }

        private void CashierMainForm_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You sure want to logout","Confirm Message",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admindashboard ds= new Admindashboard();
            ds.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminAddProduct pr= new AdminAddProduct();
            pr.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CashierOdersForm cso= new CashierOdersForm();
            cso.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            customerform cf= new customerform();
            cf.Show();
        }
    }
}
