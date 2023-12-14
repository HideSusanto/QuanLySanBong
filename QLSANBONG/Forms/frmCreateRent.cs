using QLSANBONG.Models;
using QLSANBONG.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLSANBONG.DataHandle;
using System.Data.SqlClient;

namespace QLSANBONG.Forms
{
    public partial class frmCreateRent : Form
    {
        public Staff staff = new Staff();
        public int MaSB;
        public frmCreateRent(int MaSB, Staff staffInfo)
        {
            InitializeComponent();
            this.Text = "Tạo hóa đơn cho sân " + MaSB.ToString();
            this.staff= staffInfo;
            this.MaSB = MaSB;
            comboBox1.Text = "--Select--";
        }
        public void loadStaff()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string TenKH = textBox1.Text;
            string SDT = textBox2.Text;
            string GioiTinh = comboBox1.Text;
            if(TenKH == "" || SDT == "" || (GioiTinh != "Nam" & GioiTinh != "Nữ"))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin khách hàng");
                return;
            }
            try
            {
                CustomerDAO CD = new CustomerDAO();
                CD.addCustomer(TenKH, SDT, GioiTinh);
                MessageBox.Show("Thêm thành công khách hàng: " + TenKH + "\n Có số điện thoại " + SDT);
            }
            catch (SqlException ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int MaKH = CustomerDAO.GetCustomerIdByPhoneNum(textBox3.Text);
                try
                {
                    int MaNV = int.Parse(this.staff.MaNV);
                    BillDAO.addBill(DateTime.Now.ToString("yyyy-MM-dd"), "0", "0", MaKH.ToString(), MaNV.ToString());
                    int MaHD = BillDAO.GetNewestBillId();
                    RentDAO.addBillAndPitchToRent(MaHD.ToString(), this.MaSB.ToString());
                    MessageBox.Show("Thêm thành công hóa đơn cho sân " + this.MaSB.ToString());
                    this.Close();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch
            {
                MessageBox.Show("Số điện thoại không tồn tại");
            }       
        }
    }
}
