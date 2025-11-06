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

        private void btnMon_Click(object sender, EventArgs e)
        {
            frmMon f = new frmMon();
            f.ShowDialog();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon f = new frmHoaDon();
            f.ShowDialog();
        }
    }
}
