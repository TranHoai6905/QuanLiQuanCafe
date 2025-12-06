using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void btnXemMon_Click(object sender, EventArgs e)
        {
            frmMon frm = new frmMon(); // mở form Mon
            frm.ShowDialog();
        }
        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon(1); // nhanVienId =1, hoaDonId mặc định
            frm.ShowDialog();
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            frmThemMon frm = new frmThemMon(); // mở form thêm món
            frm.ShowDialog();
        }


    }
}
