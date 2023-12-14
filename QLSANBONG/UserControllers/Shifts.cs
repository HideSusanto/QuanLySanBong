using QLSANBONG.DAO;
using QLSANBONG.DataHandle;
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

namespace QLSANBONG.UserControllers
{
    public partial class Shifts : UserControl
    {
        public Shifts()
        {
            InitializeComponent();
            LoadShift();
            LoadShiftDivision();
        }
        private void LoadShift()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getShift();
            dataGridView1.DataSource = dt;
            db.closeConnection();
        }
        private void LoadShiftDivision()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getDivision();
            dataGridView2.DataSource = dt;
            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TenNV = textBox1.Text;

            dataGridView2.DataSource = StaffDAO.searchStaffByNameInDivision(TenNV);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string TenNV = textBox1.Text;

            dataGridView2.DataSource = StaffDAO.searchStaffByNameInDivision(TenNV);
        }
    }
}
