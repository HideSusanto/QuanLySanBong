using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class Category
    {
        public string MaLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public Category(DataRow row)
        {
            this.MaLoaiSP = row["MaLoaiSP"].ToString();
            this.TenLoaiSP = row["TenLoaiSP"].ToString();

        }
        public Category() { }
    }
}
