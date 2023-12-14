using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class Bill
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string SDT;
        public string GioiTinh;
        public Bill(DataRow row)
        {
            this.MaKH = row["MaKH"].ToString();
            this.TenKH = row["TenKH"].ToString();
            this.SDT = row["SDT"].ToString();
            this.GioiTinh = row["GioiTinh"].ToString();
            ;
        }
        public Bill(string MaCV, string TenCV, string SDT, string GioiTinh)
        {
            this.MaKH = MaCV;
            this.TenKH = TenCV;
            this.SDT = SDT;
            this.GioiTinh = GioiTinh;
        }
        public Bill() { }
    }
}
