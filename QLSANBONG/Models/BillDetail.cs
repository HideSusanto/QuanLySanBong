using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class BillDetail
    {
        public string MaHD;
        public string MaSP;
        public string SoLuong;
        public float DonGia;
        public float TongTien;
        public BillDetail(string maHD, string maSP, string soLuong, float donGia, float tongTien)
        {
            MaHD = maHD;
            MaSP = maSP;
            SoLuong = soLuong;
            DonGia = donGia;
            TongTien = tongTien;
        }

    }
}
