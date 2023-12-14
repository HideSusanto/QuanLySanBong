using QLSANBONG.DAO;
using QLSANBONG.DataHandle;
using QLSANBONG.Forms;
using QLSANBONG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace QLSANBONG.UserControllers
{
    public partial class Pitches : UserControl
    {
        double totalPrice = 0;
        double totalRentPrice = 0;
        List<Pitch> pitchList = new List<Pitch>();
        string kichthuoc;
        int MaSB;
        public Pitches()
        {
            InitializeComponent();
            LoadPitch();
            loadCategoryListToComboBox(comboBox1);


        }
        #region Method
        public void LoadCategory()
        {

        }
        public void LoadProductListByCategoryId(string MaLoaiSP)
        {
            ProductDAO PD = new ProductDAO();
            comboBox2.DataSource = PD.GetProductByCategoryId(MaLoaiSP);
            comboBox2.DisplayMember = "TenSP";

        }
        public void LoadPitch()
        {
            flowLayoutPanel1.Controls.Clear();
            PitchDAO pitch = new PitchDAO();
            this.pitchList = pitch.loadPitchList();
            foreach (Pitch p in this.pitchList)
            {
            
                Button btn = new Button() { Width = 180, Height = 220};
                btn.Click += btn_Click;
                btn.Text = "Sân số: " + p.MaSB.ToString() + "\n" + p.Kichthuoc.ToString() + "\n" + p.TrangThai.ToString();
                btn.ForeColor = Color.White;
                btn.Font = new Font("French Script MT", 14);
                btn.Margin = new Padding(20);
                if(p.TrangThai == "Trống")
                {
                    btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pitch));
                }
                else 
                {
                    btn.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pitch_unavailable));
                }
                
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.Tag = p;
                flowLayoutPanel1.Controls.Add(btn);

            }
        }
        #endregion
        private void showBillProduct(int MaSB)
        {
            LoadPitch();
            listView1.Items.Clear();
            BillDAO DB = new BillDAO();
            DataTable dt = DB.GetBillDetailByPitchId(MaSB);
            totalPrice = 0;
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());       
                item.SubItems.Add(row[1].ToString());
                item.SubItems.Add(row[2].ToString());             
                totalPrice += (double)row[2];
                listView1.Items.Add(item);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            textBox1.Text = totalPrice.ToString("c", culture);
        }
        private void showBillCusomer(int MaSB)
        {
            CustomerDAO CD = new CustomerDAO();
            Customer customer = new Customer();
            customer = CD.GetCustomerInfoByPitchId(MaSB);
            if(customer == null)
            {
                label10.Text = "";
                label11.Text = "";
            }
            else
            {
                label10.Text = customer.TenKH;
                label11.Text = customer.SDT;
            }
        }
        private void showBillRent(int MaSB)
        {
            Rent rent = RentDAO.GetRentInfoByPitchId(MaSB);
            if(rent == null)
            {
                label4.Text = "";
            }
            else
            {
                if(rent.ThoiGianBD == null)
                {
                    label4.Text = DateTime.Now.ToString("HH:mm");
                }
                else
                {
                    DateTime dateTime = DateTime.Parse(rent.ThoiGianBD);
                    label4.Text = dateTime.ToString("HH:mm");
                    label6.Text = DateTime.Now.ToString("HH:mm");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                int MaSP;
                Product selected = comboBox2.SelectedItem as Product;
                MaSP = (int)selected.MaSP;
                int MaHD = BillDAO.GetBillIdByPitchId(MaSB);
                ProductDAO PD = new ProductDAO();
                int SoLuong = (int)numericUpDown1.Value;
                PD.addProductToBillDetail(MaHD.ToString(), MaSP.ToString(), SoLuong);
                numericUpDown1.Value = 1; 
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex1)
            {
                MessageBox.Show("Chưa chọn hóa đơn để thêm");
            }
            showBillProduct(MaSB);
        }
        void btn_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1;
            MaSB = ((sender as Button).Tag as Pitch).MaSB;
            string trangthai = ((sender as Button).Tag as Pitch).TrangThai;
            kichthuoc = ((sender as Button).Tag as Pitch).Kichthuoc;
            if(trangthai == "Đang bảo trì")
            {
                MessageBox.Show("Sân đang bảo trì không thể tạo hóa đơn");
                return;
            }
            if(trangthai == "Trống")
            {
                frmCreateRent frmcreaterent = new frmCreateRent(MaSB,frmLogin.getStaffInfo(frmLogin.Currusername));
                frmcreaterent.ShowDialog();
                LoadPitch();
                textBox2.Text = "0";
                showBillProduct(MaSB);
                showBillCusomer(MaSB);
                showBillRent(MaSB);
            }
            else
            {
                CultureInfo culture = new CultureInfo("vi-VN");
                textBox2.Text = 0.ToString("c", culture);
                showBillProduct(MaSB);
                showBillCusomer(MaSB);
                showBillRent(MaSB);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MaLoaiSP = "";
            ComboBox cb = sender as ComboBox;
            if(cb.SelectedItem == null)
            {
                return;
            }
            Category selected = cb.SelectedItem as Category;
            MaLoaiSP = selected.MaLoaiSP;
            LoadProductListByCategoryId(MaLoaiSP);
        }
        void loadCategoryListToComboBox(ComboBox cb)
        {
            CategoryDAO CD = new CategoryDAO();
            cb.DataSource = CD.loadCategoryList();
            cb.DisplayMember = "TenLoaiSP";
            cb.ValueMember = "MaLoaiSP";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                totalRentPrice = Calculator.getTotalCash(1, label4.Text, label6.Text, kichthuoc);
                CultureInfo culture = new CultureInfo("vi-VN");
                textBox2.Text = totalRentPrice.ToString("c", culture);
                textBox1.Text = (totalRentPrice + totalPrice).ToString("c",culture);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Không thể tính tiền" +ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int MaHD = BillDAO.GetBillIdByPitchId(MaSB);
                float GiaTriHD = float.Parse(Regex.Replace(textBox1.Text, "[^0-9]", ""));
                if (MessageBox.Show("Bạn có chắc muốn thanh toán hóa đơn cho sân " + MaSB.ToString(), "Thông Báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        if (BillDAO.PayBill(MaHD, MaSB, GiaTriHD))
                        {
                            MessageBox.Show("Thanh toán thành công!");
                            showBillProduct(MaSB);
                            LoadPitch();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi khi thanh toán!" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thanh toán hóa đơn không tồn tại!");
            }
        }
    }
}
