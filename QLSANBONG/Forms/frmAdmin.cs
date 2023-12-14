using Microsoft.Extensions.DependencyInjection;
using QLSANBONG.DAO;
using QLSANBONG.DataHandle;
using QLSANBONG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSANBONG.Forms
{
    public partial class frmAdmin : Form
    {
        BindingSource staffList = new BindingSource();
        BindingSource pitchList = new BindingSource();
        BindingSource productList = new BindingSource();
        BindingSource shiftList = new BindingSource();
        private frmLogin _loginform;
        string MaNVold;

        public frmAdmin(frmLogin loginForm)
        {  
            this._loginform = loginForm;
            InitializeComponent();
            Load();

        }
        #region methods
        public void Load()
        {
            LoadCustomer();
            dataGridView2.DataSource = staffList;
            LoadStaffList();
            AddStaffBinding();
            LoadJobIntoComboBox(comboBox1);
            dataGridView3.DataSource = pitchList;
            AddPitchBinding();
            LoadPitchList();
            dataGridView4.DataSource = productList;
            LoadProductList();
            AddProductBinding();
            LoadCategoryIntoComboBox(comboBox2);
            loadPayedBill(dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView6.DataSource = shiftList;
            LoadShiftList();
            AddShiftBinding();
            LoadAccountsList();
        }
        #endregion

        private void LoadCustomer()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getCustomer();
            dataGridView5.DataSource = dt;
            db.closeConnection();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }
       
        void AddStaffBinding()
        {

            textBox19.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "MaNV", true, DataSourceUpdateMode.Never));
            textBox2.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "TenNV", true, DataSourceUpdateMode.Never));
            textBox4.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "NgaySinh", true, DataSourceUpdateMode.Never));
            textBox3.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "GioiTinh", true, DataSourceUpdateMode.Never));
            textBox5.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "DiaChi", true, DataSourceUpdateMode.Never));
            textBox6.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "SDT", true, DataSourceUpdateMode.Never));
            //comboBox1.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "TenCV", true, DataSourceUpdateMode.Never));

        }
        void LoadStaffList()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getStaff();
            staffList.DataSource = dt;
            db.closeConnection();                 
        }
        void AddShiftBinding()
        {

            textBox20.DataBindings.Add(new Binding("Text", dataGridView6.DataSource,"Mã Ca", true, DataSourceUpdateMode.Never));
            textBox15.DataBindings.Add(new Binding("Text", dataGridView6.DataSource, "Ngày Làm", true, DataSourceUpdateMode.Never));
            textBox21.DataBindings.Add(new Binding("Text", dataGridView6.DataSource, "Mã Nhân Viên", true, DataSourceUpdateMode.Never));
         
            //comboBox1.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "TenCV", true, DataSourceUpdateMode.Never));

        }
        void LoadShiftList()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getDivision();
            shiftList.DataSource = dt;
            db.closeConnection();
        }
        void AddProductBinding()
        {
            textBox18.DataBindings.Add(new Binding("Text", dataGridView4.DataSource, "MaSP", true, DataSourceUpdateMode.Never));
            textBox16.DataBindings.Add(new Binding("Text", dataGridView4.DataSource, "TenSP", true, DataSourceUpdateMode.Never));
            textBox17.DataBindings.Add(new Binding("Text", dataGridView4.DataSource, "SoLuongKho", true, DataSourceUpdateMode.Never));
            textBox8.DataBindings.Add(new Binding("Text", dataGridView4.DataSource, "DonGia", true, DataSourceUpdateMode.Never));
            textBox14.DataBindings.Add(new Binding("Text", dataGridView4.DataSource, "MaNCC", true, DataSourceUpdateMode.Never));
            
        }
        void LoadProductList()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getProduct();
            productList.DataSource = dt;
            db.closeConnection();
        }
        void AddPitchBinding()
        {

            textBox12.DataBindings.Add(new Binding("Text", dataGridView3.DataSource, "MaSB", true, DataSourceUpdateMode.Never));
            comboBox3.DataBindings.Add(new Binding("Text", dataGridView3.DataSource, "TrangThai", true, DataSourceUpdateMode.Never));
            textBox10.DataBindings.Add(new Binding("Text", dataGridView3.DataSource, "KichThuoc", true, DataSourceUpdateMode.Never));

        }
        void LoadPitchList()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getPitch();
            pitchList.DataSource = dt;
            db.closeConnection();
        }
        void LoadAccountsList()
        {
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getAccounts();
            dataGridView7.DataSource = dt;
            db.closeConnection();
        }
        void LoadJobIntoComboBox(ComboBox cb)
        {
            JobDAO JD = new JobDAO();
            cb.DataSource = JD.loadJobList();
            cb.DisplayMember = "TenCV";
        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            CategoryDAO CD = new CategoryDAO();
            cb.DataSource = CD.loadCategoryList();
            cb.DisplayMember = "TenLoaiSP";
        }


        private void button14_Click(object sender, EventArgs e)
        {
            LoadStaffList();
        }
        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            if(dataGridView2.SelectedCells.Count > 0)
            {
                int MaCV = (int)dataGridView2.SelectedCells[0].OwningRow.Cells["MaCV"].Value;
                JobDAO JD = new JobDAO();
                Job job = JD.DAOgetJobByID(MaCV);
                comboBox1.SelectedItem = job;
                int index = -1;
                int i = 0;
                foreach (Job item in comboBox1.Items)
                {
                    if (item.MaCV == job.MaCV)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                comboBox1.SelectedIndex = index;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string TenNV = textBox2.Text;
            string NgaySinh = textBox4.Text;
            string GioiTinh = textBox3.Text;
            string DiaChi = textBox5.Text;
            string SDT = textBox6.Text;
            string JobId = (comboBox1.SelectedItem as Job).MaCV;
            StaffDAO SD = new StaffDAO();
            try
            {
                if (SD.addStaff(TenNV, NgaySinh, GioiTinh, DiaChi, SDT, JobId))
                {
                    MessageBox.Show("Thêm nhân viên thành công!");
                    LoadStaffList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string MaNV = textBox19.Text;
            StaffDAO SD = new StaffDAO();
            if (SD.deleteStaff(MaNV))
            {
                MessageBox.Show("Xóa nhân viên thành công!");
                LoadStaffList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string MaNV = textBox19.Text;
            string TenNV = textBox2.Text;
            string NgaySinh = textBox4.Text;
            string GioiTinh = textBox3.Text;
            string DiaChi = textBox5.Text;
            string SDT = textBox6.Text;
            string JobId = (comboBox1.SelectedItem as Job).MaCV;
            StaffDAO SD = new StaffDAO();
            if (SD.updateStaff(MaNV, TenNV, NgaySinh, GioiTinh, DiaChi, SDT, JobId))
            {
                MessageBox.Show("Sửa nhân viên thành công!");
                LoadStaffList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string TenNV = textBox1.Text;

            staffList.DataSource = StaffDAO.searchStaffByName(TenNV);          
        }

        private void button9_Click(object sender, EventArgs e)
        {   
            string KichThuoc = textBox10.Text.Trim();
            string TrangThai = comboBox3.Text.Trim();
            PitchDAO PD = new PitchDAO();
            if (PD.addPitch(KichThuoc, TrangThai))
            {
                MessageBox.Show("Thêm sân thành công!");
                LoadPitchList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadPitchList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string MaSB = textBox12.Text;
            PitchDAO PD = new PitchDAO();
            if (PD.deletePitch(MaSB))
            {
                MessageBox.Show("Xóa thành công!");
                LoadPitchList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string MaSB = textBox12.Text.Trim();
            string KichThuoc = textBox10.Text.Trim();
            string TrangThai = comboBox3.Text.Trim();
            PitchDAO PD = new PitchDAO();
            if (PD.updatePitch(MaSB, KichThuoc, TrangThai))
            {
                MessageBox.Show("Sửa thành công!");
                LoadPitchList();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa!");
            }
        }
        private void loadPayedBill(DateTime NgayBD, DateTime NgayKT)
        {
            DataTable dt = BillDAO.GetPayedBill(NgayBD, NgayKT);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadPayedBill(dateTimePicker1.Value, dateTimePicker2.Value);
            try
            {
                double doanhthu = BillDAO.GetRevenue(dateTimePicker1.Value, dateTimePicker2.Value);
                CultureInfo culture = new CultureInfo("vi-VN");
                textBox7.Text = Convert.ToSingle(doanhthu).ToString("c", culture);
            }
            catch
            {
                MessageBox.Show("Không có hóa đơn để thống kê");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button5.PerformClick();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            LoadProductList();
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedCells.Count > 0)
            {
                int MaLoaiSP = (int)dataGridView4.SelectedCells[0].OwningRow.Cells["MaLoaiSP"].Value;
                CategoryDAO CD = new CategoryDAO();
                Category category = CD.DAOgetCategoryByID(MaLoaiSP);
                comboBox2.SelectedItem = category;
                int index = -1;
                int i = 0;
                foreach (Category item in comboBox2.Items)
                {
                    if (item.MaLoaiSP == category.MaLoaiSP)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                comboBox2.SelectedIndex = index;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string TenSP = textBox16.Text;
            string SoLuong = textBox17.Text;
            string DonGia = textBox8.Text;
            int MaNCC = int.Parse(textBox14.Text);
            int MaLoaiSP = int.Parse((comboBox2.SelectedItem as Category).MaLoaiSP);
            ProductDAO PD = new ProductDAO();
            try
            {
                if (PD.addProduct(TenSP, DonGia, SoLuong, MaLoaiSP, MaNCC)) ;
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadProductList();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string MaSP = textBox18.Text;
            string TenSP = textBox16.Text;
            string SoLuong = textBox17.Text;
            string DonGia = textBox8.Text;
            int MaNCC = int.Parse(textBox14.Text);
            int MaLoaiSP = int.Parse((comboBox2.SelectedItem as Category).MaLoaiSP);
            ProductDAO PD = new ProductDAO();
            try
            {
                if (PD.updateProduct(MaSP, TenSP, DonGia, SoLuong, MaLoaiSP, MaNCC)) ;
                {
                    MessageBox.Show("Sửa sản phẩm thành công!");
                    LoadProductList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string TenNV = textBox1.Text;

            staffList.DataSource = StaffDAO.searchStaffByName(TenNV);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string SDT = textBox9.Text;

            dataGridView5.DataSource = CustomerDAO.searchCustomerByPhoneNum(SDT);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string SDT = textBox9.Text;
            dataGridView5.DataSource = CustomerDAO.searchCustomerByPhoneNum(SDT);
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button16.PerformClick();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int MaNV = int.Parse(textBox19.Text);
            CultureInfo culture = new CultureInfo("vi-VN");
            textBox11.Text = Calculator.getSalary(MaNV).ToString("c", culture);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string MaCa = textBox20.Text;
            string NgayLam = textBox15.Text;
            string MaNV = textBox21.Text;
            try
            {
                if (ShiftDivisionDAO.updateShiftDivision(MaCa, NgayLam, MaNV, MaNVold)) ;
                {
                    MessageBox.Show("Sửa ca thành công!");
                    LoadShiftList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            MaNVold = textBox21.Text;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string username = textBox22.Text;
            string userpassword = textBox23.Text;
            string MaNV = textBox24.Text;
            try
            {
                if (AccountDAO.addAccount(username, userpassword, MaNV)) ;
                {
                    MessageBox.Show("Thêm tài khoản cho nhân viên "+ MaNV + " thành công!");
                    LoadAccountsList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void frmAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            _loginform.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string username = textBox27.Text;
            string userpassword = textBox26.Text;
            string MaNV = textBox25.Text;
            try
            {
                if (AccountDAO.deleteAccount(username, userpassword, MaNV)) ;
                {
                    MessageBox.Show("Xóa tài khoản cho nhân viên " + MaNV + " thành công!");
                    LoadAccountsList();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string TenSP = textBox13.Text;

            productList.DataSource = ProductDAO.searchProductByName(TenSP);
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            string TenSP = textBox13.Text;

            productList.DataSource = ProductDAO.searchProductByName(TenSP);
        }
    }
}
