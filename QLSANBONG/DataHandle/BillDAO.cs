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
    public class BillDAO
    {
        public List<Bill> loadBillList()
        {
            List<Bill> BillList = new List<Bill>();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getBill();
            foreach (DataRow item in dt.Rows)
            {
                Bill Bill = new Bill(item);
                BillList.Add(Bill);
            }
            db.closeConnection();


            return BillList;
        }
        public static bool addBill(string Ngay, string TrangThai, string GiaTriHD, string MaKH, string MaNV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_AddBill", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure

                    cmd.Parameters.Add(new SqlParameter("@Ngay", SqlDbType.NVarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@GiaTriHD", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@MaKH", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.NVarChar, 10));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@Ngay"].Value = Ngay;
                    cmd.Parameters["@TrangThai"].Value = TrangThai;
                    cmd.Parameters["@GiaTriHD"].Value = GiaTriHD;
                    cmd.Parameters["@MaKH"].Value = MaKH;
                    cmd.Parameters["@MaNV"].Value = MaNV;

                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;

                }

            }
        }
        public List<Bill> searchBillByDate(DateTime date)
        {
            Database db = new Database();
            List<Bill> BillList = new List<Bill>();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_SearchBillByDate", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@Ngay", SqlDbType.DateTime));
                    cmd.Parameters["@Ngay"].Value = date;

                    // Thực hiện stored procedure
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    foreach (DataRow item in dt.Rows)
                    {
                        Bill Bill = new Bill(item);
                        BillList.Add(Bill);
                    }
                }
            }
            return BillList;
        }
        public bool updateBill(string MaNV, string TenNV, string NgaySinh, String GioiTinh, String DiaChi, String SDT, String MaCV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_UpdateBill", connection))
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
        public bool deleteBill(string MaNV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_deleteBill", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaHD", SqlDbType.NChar));

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
                    catch (Exception e)
                    {
                        // Rollback giao dịch nếu có lỗi
                        transaction.Rollback();
                        return false;
                    }
                }

            }
        }
        public DataTable GetBillDetailByPitchId(int MaSB)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.getBillDetailByPitchId(@MaSB)", connection))
                {

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaSB", SqlDbType.Int)).Value = MaSB;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    adapter.Fill(dataTable);
                    return dataTable;
                }

            }
        }
        public static int GetBillIdByPitchId(int MaSB)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.getBillIdByPitchId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho function
                    cmd.Parameters.AddWithValue("@MaSB", MaSB);  // Thay đổi giá trị cho phù hợp
                    // Thêm tham số đối với giá trị trả về
                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    // Thực hiện command
                    cmd.ExecuteNonQuery();

                    // Lấy giá trị trả về từ tham số
                    int result = (int)cmd.Parameters["@ReturnValue"].Value;
                    return result;
                }

            }
        }
        public static int GetNewestBillId()
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.getNewestBillId", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số đối với giá trị trả về
                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    // Thực hiện command
                    cmd.ExecuteNonQuery();

                    // Lấy giá trị trả về từ tham số
                    int result = (int)cmd.Parameters["@ReturnValue"].Value;
                    return result;
                }

            }
        }
        public static bool PayBill(int MaHD, int MaSB, float GiaTriHD)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_PayBill", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure

                    cmd.Parameters.Add(new SqlParameter("@MaHD", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@MaSB", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@GiaTriHD", SqlDbType.Float));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@MaHD"].Value = MaHD;
                    cmd.Parameters["@MaSB"].Value = MaSB;
                    cmd.Parameters["@GiaTriHD"].Value = GiaTriHD;

                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;

                }

            }
        }
        public static DataTable GetPayedBill(DateTime NgayBD, DateTime NgayKT)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.getPayedBill(@NgayBD, @NgayKT)", connection))
                {

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@NgayBD", SqlDbType.Date)).Value = NgayBD;
                    cmd.Parameters.Add(new SqlParameter("@NgayKT", SqlDbType.Date)).Value = NgayKT;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    dt.Load(cmd.ExecuteReader());
                    dt.Columns["Ngay"].ColumnName = "Ngày thanh toán";
                    dt.Columns["GiaTriHD"].ColumnName = "Giá trị";
                    dt.Columns["TenKH"].ColumnName = "Khách hàng";
                    dt.Columns["SDTKH"].ColumnName = "Số điện thoại";
                    dt.Columns["MaNV"].ColumnName = "Mã nhân viên";
                    dt.Columns["TenNV"].ColumnName = "Nhân viên";
                    return dt;
                }

            }
        }
        public static double GetRevenue(DateTime NgayBD, DateTime NgayKT)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.getRevenue", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NgayBD", NgayBD);
                    cmd.Parameters.AddWithValue("@NgayKT", NgayKT);

                    // Thêm tham số đối với giá trị trả về
                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Float);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    // Thực hiện command
                    cmd.ExecuteNonQuery();

                    // Lấy giá trị trả về từ tham số
                    double result = (double)cmd.Parameters["@ReturnValue"].Value;
                    return result;
                }

            }
        }

    }
}
