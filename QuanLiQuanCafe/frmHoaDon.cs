using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        private HoaDon currentHoaDon;

        public frmHoaDon()
        {
            InitializeComponent();
            currentHoaDon = new HoaDon() { Id = 1 };
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            // Xóa cột cũ và bật AutoGenerateColumns
            dgvMon.Columns.Clear();
            dgvMon.AutoGenerateColumns = true;
            dgvHoaDon.Columns.Clear();
            dgvHoaDon.AutoGenerateColumns = true;

            // Load dữ liệu
            dgvMon.DataSource = DataManager.LoadMon();
            dgvHoaDon.DataSource = DataManager.LoadHoaDon(currentHoaDon.Id);

            CapNhatTongTien();
        }

        // Thêm món vào hóa đơn
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (dgvMon.CurrentRow == null) return;

            Mon m = dgvMon.CurrentRow.DataBoundItem as Mon;
            if (m != null)
            {
                DataManager.ThemMonVaoHD(currentHoaDon, m);
                dgvHoaDon.DataSource = DataManager.LoadHoaDon(currentHoaDon.Id);
                CapNhatTongTien();
            }
        }

        // Xóa món khỏi hóa đơn
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;

            Mon m = dgvHoaDon.CurrentRow.DataBoundItem as Mon;
            if (m != null)
            {
                DataManager.XoaMonKhoiHD(currentHoaDon, m);
                dgvHoaDon.DataSource = DataManager.LoadHoaDon(currentHoaDon.Id);
                CapNhatTongTien();
            }
        }

        // Thanh toán hóa đơn
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            DataManager.ThanhToanHD(currentHoaDon);
            dgvHoaDon.DataSource = DataManager.LoadHoaDon(currentHoaDon.Id);
            CapNhatTongTien();
            MessageBox.Show("Thanh toán thành công!");
        }

        // Cập nhật tổng tiền
        private void CapNhatTongTien()
        {
            decimal tong = DataManager.TinhTongTien(currentHoaDon);
            lblTongTien.Text = "Tổng tiền: " + tong.ToString("C");
        }
    }
}
