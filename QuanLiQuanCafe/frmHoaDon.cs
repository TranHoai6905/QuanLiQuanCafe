using log4net;
using QuanLiQuanCafe.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        private int nhanVienId;
        private int hoaDonId;
        private static readonly ILog log = LogManager.GetLogger(typeof(frmHoaDon));
        private static readonly ILog fatalLog = LogManager.GetLogger("FatalLogger");

        public frmHoaDon(int nhanVienId)
        {
            InitializeComponent();
            this.nhanVienId = nhanVienId;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
            if (dgvHoaDon.Rows.Count > 0)
            {
                dgvHoaDon.ClearSelection();
                dgvHoaDon.Rows[0].Selected = true;
                if (dgvHoaDon.Rows.Count > 0)
                {
                    var row = dgvHoaDon.Rows[0];
                    hoaDonId = Convert.ToInt32(row.Cells[0].Value);  // Lấy cột đầu tiên
                }

                LoadChiTietHoaDon();
            }
            UpdateButtonStatus();
        }

        // =============================
        // Load danh sách hóa đơn
        // =============================
        private void LoadDanhSachHoaDon(string trangThai = "")
        {
            string sql = @"
SELECT
    h.Id,
    h.NgayTao,
    tk.HoTen AS NhanVien,
    h.TongTien,
    h.SoLuongMon,
    h.TrangThai
FROM HoaDon h
LEFT JOIN TaiKhoan tk ON h.NhanVienId = tk.Id
WHERE 1 = 1";
            List<SqlParameter> paramList = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(trangThai))
            {
                sql += " AND REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(h.TrangThai)), '  ', ' '), '  ', ' '), '  ', ' ') = @tt";
                paramList.Add(new SqlParameter("@tt", trangThai));
            }
            sql += " ORDER BY h.NgayTao DESC";
            try
            {
                DataTable dt = DataAccess.GetDataTable(sql, paramList.ToArray());
                dgvHoaDon.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    dgvHoaDon.ClearSelection();
                    dgvHoaDon.Rows[0].Selected = true;
                    hoaDonId = Convert.ToInt32(dgvHoaDon.Rows[0].Cells[0].Value);
                    LoadChiTietHoaDon();
                    UpdateButtonStatus();
                }
                else
                {
                    dgvChiTietHoaDon.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in LoadDanhSachHoaDon: " + ex.Message); // Giữ lại để hiển thị lỗi thực tế nếu có
                log.Error("Error in LoadDanhSachHoaDon", ex);
                fatalLog.Fatal("Fatal error in LoadDanhSachHoaDon", ex);
            }
        }
        // =============================
        // Load chi tiết hóa đơn
        // =============================
        private void LoadChiTietHoaDon()
        {
            string sql = "SELECT * FROM vw_ChiTietHoaDonDayDu WHERE MaHoaDon = @id";
            DataTable dt = DataAccess.GetDataTable(sql, new SqlParameter("@id", hoaDonId));
            dgvChiTietHoaDon.DataSource = dt;
            if (dgvChiTietHoaDon.Columns["DonGia"] != null)
                dgvChiTietHoaDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dgvChiTietHoaDon.Columns["ThanhTien"] != null)
                dgvChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        // =============================
        // Update trạng thái nút
        // =============================
        private void UpdateButtonStatus()
        {
            bool chuaThanhToan = GetTrangThaiHoaDon() == "Chưa thanh toán";
            btnThemMon.Enabled = chuaThanhToan;
            btnXoaMon.Enabled = chuaThanhToan;
            btnThanhToan.Enabled = chuaThanhToan;
            btnXoaHoaDon.Enabled = chuaThanhToan;
        }

        private string GetTrangThaiHoaDon()
        {
            if (hoaDonId <= 0) return "";
            object kq = DataAccess.ExecuteScalar(
    "SELECT REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(TrangThai)), '  ', ' '), '  ', ' '), '  ', ' ') FROM HoaDon WHERE Id=@id",
    new SqlParameter("@id", hoaDonId));
            return kq?.ToString().Trim() ?? "";
        }

        // =============================
        // Sự kiện chọn hóa đơn
        // =============================
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                hoaDonId = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["Id"].Value);
                LoadChiTietHoaDon();
                UpdateButtonStatus();
            }
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                hoaDonId = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["Id"].Value);
                LoadChiTietHoaDon();
                UpdateButtonStatus();
            }
        }

        // =============================
        // Thêm món
        // =============================
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            frmMon frm = new frmMon(hoaDonId);
            frm.OnMonDaDuocThem += (hdId) =>
            {
                if (hdId == hoaDonId)
                {
                    LoadChiTietHoaDon();
                    LoadDanhSachHoaDon();
                    UpdateButtonStatus();
                }
            };
            frm.ShowDialog();
        }

        // =============================
        // Xóa món
        // =============================
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHoaDon.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvChiTietHoaDon.SelectedRows[0].Cells["Id"].Value);
                DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_DELETE_MON, new SqlParameter("@id", id));
                LoadChiTietHoaDon();
                LoadDanhSachHoaDon();
                UpdateButtonStatus();
            }
        }

        // =============================
        // Thanh toán
        // =============================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            object kq = DataAccess.ExecuteScalar(
                "SELECT COUNT(*) FROM ChiTietHoaDon WHERE HoaDonId=@id",
                new SqlParameter("@id", hoaDonId));
            if (Convert.ToInt32(kq) == 0)
            {
                MessageBox.Show("Hóa đơn chưa có món nào!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_THANH_TOAN, new SqlParameter("@id", hoaDonId));
            MessageBox.Show("Thanh toán thành công!");
            LoadChiTietHoaDon();
            LoadDanhSachHoaDon();
            UpdateButtonStatus();
        }

        // =============================
        // Xóa hóa đơn
        // =============================
        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (hoaDonId <= 0) return;
            string trangThai = GetTrangThaiHoaDon();
            if (trangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không thể xóa hóa đơn đã thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            var confirm = MessageBox.Show($"Xóa hóa đơn #{hoaDonId}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_DELETE_HOA_DON, new SqlParameter("@id", hoaDonId));
                LoadDanhSachHoaDon();
                dgvChiTietHoaDon.DataSource = null;
            }
        }

        // =============================
        // Thêm hóa đơn mới
        // =============================
        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand(HoaDonQueries.SQL_INSERT_HOA_DON, conn, trans);
                    cmd.Parameters.AddWithValue("@nv", nhanVienId);
                    object result = cmd.ExecuteScalar();
                    trans.Commit();
                    int newId = Convert.ToInt32(result);
                    MessageBox.Show($"Đã tạo hóa đơn mới #{newId}!");
                    LoadDanhSachHoaDon();
                }
            }
        }

        // =============================
        // Lọc hóa đơn theo trạng thái
        // =============================
        private void btnTatCa_Click(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
        }
        private void btnDaThanhToan_Click_1(object sender, EventArgs e)
        {
            log.Info("btnDaThanhToan clicked");  // Thêm log
            LoadDanhSachHoaDon("Đã thanh toán");
        }

        private void btnChuaThanhToan_Click_1(object sender, EventArgs e)
        {
            log.Info("btnChuaThanhToan clicked");  // Thêm log
            LoadDanhSachHoaDon("Chưa thanh toán");
        }
    }
}