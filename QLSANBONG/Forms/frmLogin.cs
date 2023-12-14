using QLSANBONG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QLSANBONG.Forms
{

    public partial class frmLogin : Form
    {
        public static string Currusername;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string userpassword = textBox2.Text;
            UserLoginInfo.username = username;
            UserLoginInfo.password = userpassword;
            Database.updateConnectionString();
            Currusername = username;
            try
            {
                Database db = new Database();
                using (SqlConnection connection = db.getConnection)
                {
                    connection.Open();
                }
                if (UserLoginInfo.isAdmin(username))
                {
                    frmAdmin adminForm = new frmAdmin(this);
                    adminForm.Show();
                    this.Hide();
                }
                else
                {
                    frmMain mainForm = new frmMain(this,getStaffInfo(username));
                    mainForm.Show();
                    this.Hide();
                }
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
            }
        }
        public static Staff getStaffInfo(string username)
        {
            Database db = new Database();
            string cmd = "SELECT * FROM getStaffInfoByUserName(\'"+ username +"\')";
            SqlCommand sql = new SqlCommand(cmd, db.getConnection);
            db.openConnection();
            //sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 10)).Value = username;
            SqlDataReader reader = sql.ExecuteReader();
            if (reader.Read()){
                string MaNV = reader["MaNV"].ToString();
                string TenNV = reader["TenNV"].ToString();
                string NgaySinh = reader["NgaySinh"].ToString();
                string GioiTinh = reader["GioiTinh"].ToString();
                string DiaChi = reader["DiaChi"].ToString();
                string SDT = reader["SDT"].ToString();
                string MaCV = reader["MaCV"].ToString();
                Staff staffInfo = new Staff(MaNV.Trim(), TenNV.Trim(), NgaySinh.Trim(), GioiTinh.Trim(), DiaChi.Trim(), SDT.Trim(), MaCV.Trim());
                // Sử dụng các giá trị trong các biến MaNV, TenNV, NgaySinh, ...
                return staffInfo;
            }

            return null;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
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
    }
}
