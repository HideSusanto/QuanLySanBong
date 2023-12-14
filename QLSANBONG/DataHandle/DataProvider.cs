using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.DAO
{
    public class DataProvider
    {
        private string connectionString;
        private SqlConnection conn;
        private string sql;
        private  DataTable dt;
        private SqlCommand cmd;
        public DataProvider(SqlConnection conn)
        {
            this.conn = conn;
        }
        public DataTable getStaff()
        {
            sql = "select * from V_ThongTinNhanVien";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable getShift()
        {
            sql = "select * from V_CaLamViec";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dt.Columns["MaCa"].ColumnName = "Mã Ca";
            dt.Columns["NgayLam"].ColumnName = "Thứ";
            dt.Columns["GioBatDau"].ColumnName = "Giờ bắt đầu";
            dt.Columns["GioKetThuc"].ColumnName = "Giờ kết thúc";
            return dt;
        }
        public DataTable getDivision()
        {
            sql = "select * from V_BangPhanCa";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dt.Columns["MaNV"].ColumnName = "Mã Nhân Viên";
            dt.Columns["TenNV"].ColumnName = "Tên Nhân Viên";
            dt.Columns["MaCa"].ColumnName = "Mã Ca";
            dt.Columns["NgayLam"].ColumnName = "Ngày Làm";
            return dt;
        }
        public DataTable getBill()
        {
            sql = "select * from V_DanhSachHoaDon";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dt.Columns["MaHD"].ColumnName = "Mã Hóa Đơn";
            dt.Columns["Ngay"].ColumnName = "Ngày";
            dt.Columns["TrangThai"].ColumnName = "Trạng Thái";
            dt.Columns["GiaTriHD"].ColumnName = "Tổng tiền";
            dt.Columns["MaKH"].ColumnName = "Khách Hàng";
            dt.Columns["MaNV"].ColumnName = "Nhân Viên";
            return dt;
        }
        public DataTable getUnPayedBill()
        {
            sql = "select * from V_DanhSachHoaDonChuaThanhToan";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dt.Columns["MaHD"].ColumnName = "Mã Hóa Đơn";
            dt.Columns["Ngay"].ColumnName = "Ngày";
            dt.Columns["MaSB"].ColumnName = "Sân bóng";
            dt.Columns["TenKH"].ColumnName = "Khách hàng";

            return dt;
        }
        public DataTable getProduct()
        {
            sql = "select * from V_DanhSachSanPham";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable getPitch()
        {
            sql = "select * from V_DanhSachSanBong";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }
        public DataTable getCustomer()
        {
            sql = "select * from V_DanhSachKhachHang";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dt.Columns["MaKH"].ColumnName = "Mã Khách";
            dt.Columns["TenKH"].ColumnName = "Tên Khách";
            dt.Columns["GioiTinh"].ColumnName = "Giới Tính";
            dt.Columns["SDT"].ColumnName = "Số Điện Thoại";
            return dt;
        }
        public DataTable getProvider()
        {
            sql = "select * from V_DanhSachNhaCungCap";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dt.Columns["MaNCC"].ColumnName = "Mã Nhà Cung Cấp";
            dt.Columns["TenNCC"].ColumnName = "Tên Nhà Cung Cấp";
            dt.Columns["DiaChi"].ColumnName = "Địa Chỉ";
            dt.Columns["SDT"].ColumnName = "Số Điện Thoại";
            return dt;
        }
        public DataTable getJob()
        {
            sql = "select * from V_DanhSachCongViec";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable getJobById(int MaCV)
        {
            sql = "proc_getJobById";
            cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaCV", SqlDbType.NChar, 10));
            cmd.Parameters["@MaCV"].Value = MaCV.ToString();
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable getCategoryById(int MaLoaiSP)
        {
            sql = "proc_getCategoryById";
            cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaLoaiSP", SqlDbType.NChar, 10));
            cmd.Parameters["@MaLoaiSP"].Value = MaLoaiSP.ToString();
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable getCategory()
        {
            sql = "select * from V_DanhSachLoaiSanPham";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable getAccounts()
        {
            sql = "select * from V_DanhSachTaiKhoan";
            cmd = new SqlCommand(sql, conn);
            dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
    }
}
