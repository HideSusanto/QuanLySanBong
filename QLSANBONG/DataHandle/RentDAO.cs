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
    public class RentDAO
    {
        public RentDAO() { }
        public static Rent GetRentInfoByPitchId(int MaSB)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.getRentPitchInfoByPitchId(@MaSB)", connection))
                {

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaSB", SqlDbType.Int)).Value = MaSB;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string ThoiGianBD = reader["ThoiGianBD"].ToString();
                        string ThoiGianKT = reader["ThoiGianKT"].ToString();

                        Rent rentInfo = new Rent(ThoiGianBD.Trim(), ThoiGianKT.Trim());
                        // Sử dụng các giá trị trong các biến MaNV, TenNV, NgaySinh, ...
                        return rentInfo;
                    }
                }
                return null;

            }
        }
        public static bool addBillAndPitchToRent(string MaHD, string MaSB)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_AddBillAndPitchToRent", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure

                    cmd.Parameters.Add(new SqlParameter("@MaHD", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@MaSB", SqlDbType.Int));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@MaHD"].Value = MaHD;
                    cmd.Parameters["@MaSB"].Value = MaSB;


                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
    }
}
