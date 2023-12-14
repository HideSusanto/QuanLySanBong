using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLSANBONG.DataHandle
{
    public class Calculator
    {
        public static float getTotalCash(int NgayThue, string KhungGioBD, string KhungGioKT, string KichThuoc)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.getRentCashByTime", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    NgayThue = 1;
                    DateTime today = DateTime.Today;
                    if(today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                    {
                        NgayThue = 0;
                    }
                    // Thêm tham số cho function
                    cmd.Parameters.AddWithValue("@NgayThue", NgayThue);  // Thay đổi giá trị cho phù hợp
                    cmd.Parameters.AddWithValue("@GioBDThue", KhungGioBD);  // Thay đổi giá trị cho phù hợp
                    cmd.Parameters.AddWithValue("@GioKTThue", KhungGioKT);
                    cmd.Parameters.AddWithValue("@KichThuoc", KichThuoc);
                    // Thêm tham số đối với giá trị trả về
                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Float);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    // Thực hiện command
                    cmd.ExecuteNonQuery();

                    // Lấy giá trị trả về từ tham số
                    float result = Convert.ToSingle(cmd.Parameters["@ReturnValue"].Value);
                    return Math.Abs(result);
                }

            }
        }
        public static float getSalary(int MaNV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.CalculateSalary", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
               
                    // Thêm tham số cho function
                    cmd.Parameters.AddWithValue("@MaNV", MaNV);  // Thay đổi giá trị cho phù hợp
                    // Thêm tham số đối với giá trị trả về
                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Float);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnValue);

                    // Thực hiện command
                    cmd.ExecuteNonQuery();

                    // Lấy giá trị trả về từ tham số
                    float result = Convert.ToSingle(cmd.Parameters["@ReturnValue"].Value);
                    return Math.Abs(result);
                }

            }
        }
    }
}
