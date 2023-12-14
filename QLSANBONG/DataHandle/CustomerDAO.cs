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
    public class CustomerDAO
    {
        public List<Customer> loadCustomerList()
        {
            List<Customer> CustomerList = new List<Customer>();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getCustomer();
            foreach (DataRow item in dt.Rows)
            {
                Customer Customer = new Customer(item);
                CustomerList.Add(Customer);
            }
            db.closeConnection();


            return CustomerList;
        }
        public bool addCustomer(string TenKH, string SDT, string GioiTinh)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_AddCustomer", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                        
                    cmd.Parameters.Add(new SqlParameter("@TenKH", SqlDbType.NVarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@SDT", SqlDbType.NChar, 11));
                    cmd.Parameters.Add(new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 3));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@TenKH"].Value = TenKH;
                    cmd.Parameters["@SDT"].Value = SDT;
                    cmd.Parameters["@GioiTinh"].Value = GioiTinh;


                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public static DataTable searchCustomerByPhoneNum(string SDT)
        {
            Database db = new Database();
            List<Staff> staffList = new List<Staff>();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_SearchCustomerByPhoneNumber", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@SDT", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@SDT"].Value = SDT; // Tên muốn tìm

                    // Thực hiện stored procedure
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    dt.Columns["MaKH"].ColumnName = "Mã Khách";
                    dt.Columns["TenKH"].ColumnName = "Tên Khách";
                    dt.Columns["GioiTinh"].ColumnName = "Giới Tính";
                    dt.Columns["SDT"].ColumnName = "Số Điện Thoại";
                    return dt;
                }
            }
        }

        public bool updateCustomer(string MaNV, string TenNV, string NgaySinh, String GioiTinh, String DiaChi, String SDT, String MaCV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_UpdateCustomer", connection))
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
        public bool deleteCustomer(string MaNV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_deleteCustomer", connection))
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
                    catch (Exception e)
                    {
                        // Rollback giao dịch nếu có lỗi
                        transaction.Rollback();
                        MessageBox.Show(e.ToString());
                        return false;
                    }
                }

            }
        }
        public Customer GetCustomerInfoByPitchId(int MaSB)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.getCustomerInfoByPitchId(@MaSB)", connection))
                {

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaSB", SqlDbType.Int)).Value = MaSB;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string MaKH = reader["MaKH"].ToString();
                        string TenKH = reader["TenKH"].ToString();
                        string GioiTinh = reader["GioiTinh"].ToString();
                        string SDT = reader["SDT"].ToString();
                        Customer customerInfo = new Customer(MaKH.Trim(), TenKH.Trim(), GioiTinh.Trim(), SDT.Trim());
                        // Sử dụng các giá trị trong các biến MaNV, TenNV, NgaySinh, ...
                        return customerInfo;
                    }
                }
                return null;

            }
        }
        public static int GetCustomerIdByPhoneNum(string SDT)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.getCustomerIdByPhoneNum", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Thêm tham số cho function
                    cmd.Parameters.AddWithValue("@SDT", SDT);  // Thay đổi giá trị cho phù hợp
                    // Thêm tham số đối với giá trị trả về
                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    // Thực hiện command
                    cmd.ExecuteNonQuery();

                    // Lấy giá trị trả về từ tham số
                    int result = (int)(cmd.Parameters["@ReturnValue"].Value);
                    return result;
                }

            }
        }

    }
}
