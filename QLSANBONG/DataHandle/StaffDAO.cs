using QLSANBONG.DAO;
using QLSANBONG.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.DataHandle
{
    public class StaffDAO
    {
        public List<Staff> loadStaffList()
        {
            List<Staff> staffList = new List<Staff>();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getStaff();
            foreach (DataRow item in dt.Rows)
            {
                Staff staff = new Staff(item);
                staffList.Add(staff);
            }
            db.closeConnection();


            return staffList;
        }
        public bool addStaff(string TenNV, string NgaySinh, String GioiTinh, String DiaChi, String SDT, String MaCV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_AddStaff", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure

                    cmd.Parameters.Add(new SqlParameter("@TenNV", SqlDbType.NVarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@NgaySinh", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 3));
                    cmd.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@SDT", SqlDbType.NChar, 11));
                    cmd.Parameters.Add(new SqlParameter("@MaCV", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@SoCa", SqlDbType.Int, 11));
                    cmd.Parameters.Add(new SqlParameter("@Thuong", SqlDbType.Float, 10));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@TenNV"].Value = TenNV;
                    cmd.Parameters["@NgaySinh"].Value = NgaySinh;
                    cmd.Parameters["@GioiTinh"].Value = GioiTinh;
                    cmd.Parameters["@DiaChi"].Value = DiaChi;
                    cmd.Parameters["@SDT"].Value = SDT;
                    cmd.Parameters["@MaCV"].Value = MaCV;
                    cmd.Parameters["@SoCa"].Value = 0;
                    cmd.Parameters["@Thuong"].Value = 0;

                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;



                }
                
            }
        }
        public static DataTable searchStaffByName(string TenNV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_SearchStaffByName", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@TenNV", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@TenNV"].Value = TenNV; // Tên muốn tìm

                    // Thực hiện stored procedure
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
        }
        public static DataTable searchStaffByNameInDivision(string TenNV)
        {
            Database db = new Database();
            List<Staff> staffList = new List<Staff>();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_SearchStaffByNameInDivision", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@TenNV", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@TenNV"].Value = TenNV; // Tên muốn tìm

                    // Thực hiện stored procedure
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
        }
        public bool updateStaff(string MaNV, string TenNV, string NgaySinh, String GioiTinh, String DiaChi, String SDT, String MaCV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_UpdateStaff", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@TenNV", SqlDbType.NChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@NgaySinh", SqlDbType.NChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@GioiTinh", SqlDbType.NChar, 3));
                    cmd.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@SDT", SqlDbType.NChar, 11));
                    cmd.Parameters.Add(new SqlParameter("@MaCV", SqlDbType.NChar, 10));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@MaNV"].Value = MaNV;
                    cmd.Parameters["@TenNV"].Value = TenNV;
                    cmd.Parameters["@NgaySinh"].Value = NgaySinh;
                    cmd.Parameters["@GioiTinh"].Value = GioiTinh;
                    cmd.Parameters["@DiaChi"].Value = DiaChi;
                    cmd.Parameters["@SDT"].Value = SDT;
                    cmd.Parameters["@MaCV"].Value = MaCV;

                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public bool deleteStaff(string MaNV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_deleteStaffAndShifts", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.NChar));

                    // Thiết lập giá trị cho tham số
                    cmd.Parameters["@MaNV"].Value = MaNV; // Mã nhân viên bạn muốn xóa

                    // Bắt đầu giao dịch
                    SqlTransaction transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;

                    try
                    {
                        int result = cmd.ExecuteNonQuery(); // Thực hiện stored procedure để xóa nhân viên và ca làm việc

                        // Commit giao dịch nếu thành công
                        transaction.Commit();
                        return result > 0;

                    }
                    catch (SqlException e)
                    {
                        // Rollback giao dịch nếu có lỗi
                        transaction.Rollback();
                        MessageBox.Show(e.ToString());
                        return false;
                    }
                }

            }
        }

    }
}
