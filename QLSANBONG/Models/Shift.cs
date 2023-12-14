using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class ShiftDivision
    {
        public string MaCa { get; set; }
        public string MaNV { get; set; }
        public string NgayLam { get; set; }
        public ShiftDivision(DataRow row)
        {
            this.MaCa = row["MaCa"].ToString();
            this.MaNV = row["MaNV"].ToString();
            this.NgayLam = row["NgayLam"].ToString();

        }
        public ShiftDivision() { }
    }
}
