using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class Pitch
    {
        public int MaSB;
        public string Kichthuoc;
        public string TrangThai;

        public Pitch(int maSB, string kichthuoc, string trangThai)
        {
            this.MaSB = maSB;
            this.Kichthuoc = kichthuoc;
            this.TrangThai = trangThai;
        }
        public Pitch(DataRow row)
        {
            this.MaSB = (int)row["MaSB"];
            this.Kichthuoc = row["kichThuoc"].ToString();
            this.TrangThai = row["TrangThai"].ToString();

        }
    }
}
