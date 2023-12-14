using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class Staff
    {
        public string MaNV;
        public string TenNV;
        public string NgaySinh;
        public string GioiTinh;
        public string DiaChi;
        public string SDT;
        public string MaCV;

        public Staff()
        {
        }

        public Staff(string MaNV, string TenNV, string NgaySinh, String GioiTinh, string DiaChi, string SDT, string MaCV) 
        {
            this.MaNV= MaNV;
            this.TenNV = TenNV;
            this.NgaySinh= NgaySinh;
            this.GioiTinh= GioiTinh;
            this.DiaChi= DiaChi;
            this.SDT= SDT;
            this.MaCV= MaCV;
        }
        public Staff(DataRow row)
        {
            this.MaNV = row["MaNV"].ToString();
            this.TenNV = row["TenNV"].ToString();
            this.NgaySinh = row["NgaySinh"].ToString();
            this.GioiTinh = row["GioiTinh"].ToString();
            this.DiaChi = row["DiaChi"].ToString();
            this.SDT = row["SDT"].ToString();
            this.MaCV = row["MaCV"].ToString();
        }

    }
}
