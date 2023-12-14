using QLSANBONG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSANBONG.Forms
{
    public partial class frmStaffInfo : Form
    {
        Staff staff = new Staff();
        public frmStaffInfo(Staff staff)
        {
            InitializeComponent();
            this.staff = staff;
            label6.Text = staff.MaNV.ToString();
            label7.Text = staff.TenNV;
            label8.Text = staff.NgaySinh;
            label9.Text = staff.GioiTinh;
            label10.Text = staff.SDT;

        }
    }
}
