using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        private int nhanVienId;
        private string vaiTro;
        private int hoaDonId;

        public frmHoaDon(int nhanVienId, string vaiTro, int hoaDonId)
        {
            InitializeComponent();
            this.nhanVienId = nhanVienId;
            this.vaiTro = vaiTro;
            this.hoaDonId = hoaDonId;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadChiTietHoaDon();
            KiemTraTrangThaiHoaDon();
        }

        private void LoadChiTietHoaDon()
        {
            string sql = @"SELECT c.Id, m.TenMon, c.SoLuong, m.Gia, (c.SoLuong * m.Gia) AS ThanhTien
                           FROM ChiTietHoaDon c
                           JOIN Mon m ON c.MonId = m.Id
                           WHERE c.HoaDonId = @id";
            dgvChiTietHoaDon.DataSource = DataAccess.GetDataTable(sql, new SqlParameter("@id", hoaDonId));
        }

        private void KiemTraTrangThaiHoaDon()
        {
            string sql = "SELECT TrangThai FROM HoaDon WHERE Id=@id";
            object kq = DataAccess.ExecuteScalar(sql, new SqlParameter("@id", hoaDonId));
            string trangThai = kq?.ToString();
            bool chuaThanhToan = trangThai == "Chưa thanh toán";

            btnThemMon.Enabled = chuaThanhToan;
            btnXoaMon.Enabled = chuaThanhToan;
            btnThanhToan.Enabled = chuaThanhToan;
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            frmMon frm = new frmMon(hoaDonId);
            frm.ShowDialog();
            LoadChiTietHoaDon();
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHoaDon.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvChiTietHoaDon.SelectedRows[0].Cells["Id"].Value);
                string sql = "DELETE FROM ChiTietHoaDon WHERE Id=@id";
                DataAccess.ExecuteNonQuery(sql, new SqlParameter("@id", id));
                LoadChiTietHoaDon();
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE HoaDon SET TrangThai=N'Đã thanh toán' WHERE Id=@id";
            DataAccess.ExecuteNonQuery(sql, new SqlParameter("@id", hoaDonId));
            MessageBox.Show("Thanh toán thành công!");
            KiemTraTrangThaiHoaDon();
        }


        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo hóa đơn mới
                string sql = @"INSERT INTO HoaDon (NhanVienId, NgayTao, TrangThai, TongTien, SoLuongMon)
                       OUTPUT INSERTED.Id
                       VALUES (@nv, GETDATE(), N'Chưa thanh toán', 0, 0)";
                int newHoaDonId = Convert.ToInt32(DataAccess.ExecuteScalar(sql, new SqlParameter("@nv", nhanVienId)));

                // Cập nhật hoaDonId hiện tại
                hoaDonId = newHoaDonId;

                // Load lại danh sách chi tiết (rỗng)
                dgvChiTietHoaDon.DataSource = null;
                LoadChiTietHoaDon();

                // Bật/ tắt nút theo trạng thái
                KiemTraTrangThaiHoaDon();

                MessageBox.Show("✅ Tạo hóa đơn mới thành công!");

                // ✅ **Load lại DataGridView hiển thị tất cả hóa đơn**
                LoadDanhSachHoaDon(); // <-- Hàm này phải có trong form quản lý hóa đơn
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tạo hóa đơn: " + ex.Message);
            }
        }
        private void LoadDanhSachHoaDon()
        {
            string sql = "SELECT Id, NhanVienId, NgayTao, TongTien, SoLuongMon, TrangThai FROM HoaDon ORDER BY NgayTao DESC";
            dgvHoaDon.DataSource = DataAccess.GetDataTable(sql);
        }

    }
}
