using QLSANBONG.DAO;
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
    public partial class Bills : UserControl
    {
        public Bills()
        {
            InitializeComponent();
            LoadUnPayedBill();
        }

        private void LoadUnPayedBill()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getUnPayedBill();
            dataGridView1.DataSource = dt;
            db.closeConnection();
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            LoadUnPayedBill();
        }
    }
}
