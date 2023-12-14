using QLSANBONG.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.DAO
{
    public class PitchDAO
    {
        public PitchDAO() { }
        public List<Pitch> loadPitchList()
        {
            List<Pitch> pitchList = new List<Pitch>();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            
                db.openConnection();
                DataProvider dp = new DataProvider(connection);
                DataTable dt = dp.getPitch();
                foreach(DataRow item in dt.Rows)
                {
                    Pitch pitch = new Pitch(item);
                    pitchList.Add(pitch);
                }
                db.closeConnection();
            
            
            return pitchList;
        }
        public bool addPitch(string KichThuoc, string TrangThai)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_AddPitch", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure

                    cmd.Parameters.Add(new SqlParameter("@KichThuoc", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 20));


                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@KichThuoc"].Value = KichThuoc;
                    cmd.Parameters["@TrangThai"].Value = TrangThai;

                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public bool updatePitch(string MaSB, string KichThuoc, string TrangThai)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_UpdatePitch", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaSB", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@KichThuoc", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 20));


                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@MaSB"].Value = MaSB;
                    cmd.Parameters["@KichThuoc"].Value = KichThuoc;
                    cmd.Parameters["@TrangThai"].Value = TrangThai;

                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public bool deletePitch(string MaSB)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_DeletePitch", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaSB", SqlDbType.NVarChar));

                    // Thiết lập giá trị cho tham số
                    cmd.Parameters["@MaSB"].Value = MaSB; // Mã nhân viên bạn muốn xóa

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
    }
}
