using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class Product
    {
        public int MaSP { get; set; }    
        public string TenSP { get; set; }
        public double DonGia { get; set; }
        public int SoLuongKho { get; set; }
        public string MaLoaiSP { get; set; }
        public string MaNCC { get; set; }


        public Product(DataRow row)
        {
            this.MaSP = (int)row["MaSP"];
            this.TenSP = row["TenSP"].ToString();
            this.DonGia = (double)row["DonGia"];
            this.SoLuongKho = (int)row["SoLuongKho"];
            this.MaLoaiSP = row["MaLoaiSP"].ToString();
            this.MaNCC = row["MaNCC"].ToString();
        }
        public Product() { }
    }
}

