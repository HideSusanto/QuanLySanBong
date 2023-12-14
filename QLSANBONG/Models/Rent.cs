using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class Rent
    {
        public string ThoiGianBD { get; set; }
        public string ThoiGianKT { get; set; }



        public Rent(DataRow row)
        {
            this.ThoiGianBD = row["ThoiGianBD"].ToString();
            this.ThoiGianKT = row["ThoiGianKT"].ToString();

        }
        public Rent(string thoiGianBD, string thoiGianKT)
        {
            ThoiGianBD = thoiGianBD;
            ThoiGianKT = thoiGianKT;
        }
    }
}
