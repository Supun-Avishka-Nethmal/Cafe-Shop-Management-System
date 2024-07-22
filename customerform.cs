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
    public partial class customerform : Form
    {
        public customerform()
        {
            InitializeComponent();
            viewdetails();

        }
        public void refreshdata()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(refreshdata));
                return;
            }

            InitializeComponent();
            viewdetails();
        }

        public void viewdetails()
        {
            customerdetails cd = new customerdetails();
            List<customerdetails> list = cd.details();

            dataGridView1.DataSource= list;
        }
    }
}
