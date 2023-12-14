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
    public class ShiftDivisionDAO
    {
        public List<ShiftDivision> loadShiftDivisionList()
        {
            List<ShiftDivision> ShiftDivisionList = new List<ShiftDivision>();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getDivision();
            foreach (DataRow item in dt.Rows)
            {
                ShiftDivision ShiftDivision = new ShiftDivision(item);
                ShiftDivisionList.Add(ShiftDivision);
            }
            db.closeConnection();


            return ShiftDivisionList;
        }
        public static bool updateShiftDivision(string MaCa, string NgayLam, string MaNV, string  MaNVold)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_UpdateShiftDivision", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaCa", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@NgayLam", SqlDbType.NVarChar, 20));
                    cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@MaNVold", SqlDbType.Int));


                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@MaCa"].Value = MaCa;
                    cmd.Parameters["@NgayLam"].Value = NgayLam;
                    cmd.Parameters["@MaNV"].Value = MaNV;
                    cmd.Parameters["@MaNVold"].Value = MaNVold;


                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
    }
}
