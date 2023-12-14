using Microsoft.Extensions.DependencyInjection;
using QLSANBONG.Forms;
using QLSANBONG.Models;
using QLSANBONG.UserControllers;
using System.Data;

namespace QLSANBONG
{
    public partial class frmMain : Form
    {
        NavigationControl navigationControl;
        Staff staffInfo = new Staff();
        private frmLogin _loginform;
        public frmMain(frmLogin loginform,Staff staffInfo)
        {
            InitializeComponent();
            InitializeNavigationControl();
            this.staffInfo= staffInfo;
            this._loginform= loginform;
            iconButton4.Text = staffInfo.TenNV;
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToLongDateString();
        }
        private void InitializeNavigationControl()
        {
            List<UserControl> userControls = new List<UserControl>(){ new Home(), new Pitches(), new Shifts(), new Bills()};
            navigationControl = new NavigationControl(userControls, panel1);
            navigationControl.Display(0);

        }
        private void frmMain_FormClosing(Object sender, FormClosingEventArgs e)
        {
            
        }
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            navigationControl.Display(0);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            navigationControl.Display(1);
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            navigationControl.Display(2);
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _loginform.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            using (frmStaffInfo frmstaffinfo = new frmStaffInfo(staffInfo))
            {
                frmstaffinfo.ShowDialog(this);
            } // Dispose form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _loginform.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            navigationControl.Display(3);
        }
        
    }
}