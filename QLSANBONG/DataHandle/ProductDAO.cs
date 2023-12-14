using QLSANBONG.DAO;
using QLSANBONG.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;

namespace QLSANBONG.DataHandle
{
    public class ProductDAO
    {
        public List<Product> loadProductList()
        {
            List<Product> ProductList = new List<Product>();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getProduct();
            foreach (DataRow item in dt.Rows)
            {
                Product Product = new Product(item);
                ProductList.Add(Product);
            }
            db.closeConnection();
            return ProductList;
        }
        public List<Product> GetProductByCategoryId(string MaLoaiSP)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.getProductByCategoryId(@MaLoaiSP)", connection))
                {

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaLoaiSP", SqlDbType.Int)).Value = MaLoaiSP;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    // Đổ dữ liệu vào DataTable
                    adapter.Fill(dataTable);
                    List<Product> listProduct = new List<Product>();
                    foreach (DataRow item in dataTable.Rows)
                    {
                        Product Product = new Product(item);
                        listProduct.Add(Product);
                    }
                    return listProduct;
                }

            }
        }
        public bool addProduct(string TenSP, string DonGia, string SoLuongKho, int MaLoaiSP, int MaNCC)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_AddProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure

                    cmd.Parameters.Add(new SqlParameter("@TenSP", SqlDbType.NVarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@DonGia", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@SoLuongKho", SqlDbType.NVarChar, 3));
                    cmd.Parameters.Add(new SqlParameter("@MaLoaiSP", SqlDbType.NVarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@MaNCC", SqlDbType.Int));


                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@TenSP"].Value = TenSP;
                    cmd.Parameters["@DonGia"].Value = DonGia;
                    cmd.Parameters["@SoLuongKho"].Value = SoLuongKho;
                    cmd.Parameters["@MaLoaiSP"].Value = MaLoaiSP;
                    cmd.Parameters["@MaNCC"].Value = MaNCC;

                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public static DataTable searchProductByName(string TenSP)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_SearchProductByName", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@TenSP", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@TenSP"].Value = TenSP; // Tên muốn tìm

                    // Thực hiện stored procedure
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
            
        }
        public bool updateProduct(string MaSP, string TenSP, string DonGia, string SoLuongKho, int MaLoaiSP, int MaNCC)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_UpdateProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@TenSP", SqlDbType.NVarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@DonGia", SqlDbType.NVarChar, 10));
                    cmd.Parameters.Add(new SqlParameter("@SoLuongKho", SqlDbType.NVarChar, 3));
                    cmd.Parameters.Add(new SqlParameter("@MaLoaiSP", SqlDbType.NVarChar, 100));
                    cmd.Parameters.Add(new SqlParameter("@MaNCC", SqlDbType.Int));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@MaSP"].Value = MaSP;
                    cmd.Parameters["@TenSP"].Value = TenSP;
                    cmd.Parameters["@DonGia"].Value = DonGia;
                    cmd.Parameters["@SoLuongKho"].Value = SoLuongKho;
                    cmd.Parameters["@MaLoaiSP"].Value = MaLoaiSP;
                    cmd.Parameters["@MaNCC"].Value = MaNCC;

                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }
        public bool deleteProduct(string MaSP)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_deleteProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.NVarChar));

                    // Thiết lập giá trị cho tham số
                    cmd.Parameters["@MaSP"].Value = MaSP; // Mã nhân viên bạn muốn xóa

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
        public bool addProductToBillDetail(string MaHD, string MaSP, int SL)
        {
            Database db = new Database();
            using (SqlConnection connection = db.getConnection)
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("proc_AddProductToBillDetail", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@MaHD", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@MaSP", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@SL", SqlDbType.Int));

                    // Thiết lập giá trị cho các tham số
                    cmd.Parameters["@MaHD"].Value = MaHD;
                    cmd.Parameters["@MaSP"].Value = MaSP;
                    cmd.Parameters["@SL"].Value = SL;
                    // Thực hiện stored procedure
                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }

            }
        }

    }
}
