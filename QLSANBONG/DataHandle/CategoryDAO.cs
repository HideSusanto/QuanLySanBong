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
    public class CategoryDAO
    {
        public string MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public List<Category> loadCategoryList()
        {
            List<Category> CategoryList = new List<Category>();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getCategory();
            foreach (DataRow item in dt.Rows)
            {
                Category Category = new Category(item);
                CategoryList.Add(Category);
            }
            db.closeConnection();
            return CategoryList;
        }
        public Category DAOgetCategoryByID(int id)
        {
            Category category = new Category();
            Database db = new Database();
            SqlConnection connection = db.getConnection;
            db.openConnection();
            DataProvider dp = new DataProvider(connection);
            DataTable dt = dp.getCategoryById(id);
            foreach (DataRow item in dt.Rows)
            {
                category = new Category(item);
                return category;
            }
            db.closeConnection();
            return category;

        }
    }
}
