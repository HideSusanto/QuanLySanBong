using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.DataHandle
{
    public class AccountDAO
    {
        public static bool addAccount(string username, string userpassword, string MaNV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnectionAdmin)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_AddAccount", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 12));
                    cmd.Parameters.Add(new SqlParameter("@UserPassWord", SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@UserName"].Value = username;
                    cmd.Parameters["@UserPassWord"].Value = userpassword;
                    cmd.Parameters["@MaNV"].Value = MaNV;


                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public static bool deleteAccount(string username, string userpassword, string MaNV)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnectionAdmin)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_DeleteAccount", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 12));
                    cmd.Parameters.Add(new SqlParameter("@UserPassWord", SqlDbType.VarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@MaNV", SqlDbType.Int));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@UserName"].Value = username;
                    cmd.Parameters["@UserPassWord"].Value = userpassword;
                    cmd.Parameters["@MaNV"].Value = MaNV;


                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
    }
}
