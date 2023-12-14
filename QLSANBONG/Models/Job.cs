using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG.Models
{
    public class Job
    {
        public string MaCV { get; set; }
        public string TenCV { get; set; }
        public double Luong;
        public Job(DataRow row)
        {
            this.MaCV = row["MaCV"].ToString();
            this.TenCV = row["TenCV"].ToString();
            this.Luong = (double)row["Luong"];
            ;
        }
        public Job(string MaCV, string TenCV, double Luong)  
        {
            this.MaCV = MaCV;
            this.TenCV = TenCV;
            this.Luong = Luong;
        }
        public Job() { }
    }
}
