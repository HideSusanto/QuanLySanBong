using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class Customer
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string SDT;
        public string GioiTinh;
        public Customer(DataRow row)
        {
            this.MaKH = row["MaKH"].ToString();
            this.TenKH = row["TenKH"].ToString();
            this.SDT = row["SDT"].ToString();
            this.GioiTinh = row["GioiTinh"].ToString();
            ;
        }
        public Customer(string MaCV, string TenCV, string GioiTinh, string SDT)
        {
            this.MaKH = MaCV;
            this.TenKH = TenCV;
            this.SDT = SDT;
            this.GioiTinh = GioiTinh;
        }
        public Customer() { }
    }
}
