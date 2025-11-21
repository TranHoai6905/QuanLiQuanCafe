using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLiQuanCafe.Models;
using log4net;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        private int nhanVienId;       // ID nhân viên đăng nhập
        private string vaiTro;        // Vai trò (nhanvien/admin)
        private int hoaDonId;         // ID hóa đơn đang chọn
        private bool isNhanVien;      // Biến phân quyền

        // =============================
        // LOGGER
        // =============================
        private static readonly ILog log = LogManager.GetLogger(typeof(frmHoaDon));     // Logger chính
        private static readonly ILog fatalLog = LogManager.GetLogger("FatalLogger");     // Logger FATAL

        public frmHoaDon(int nhanVienId, string vaiTro, int hoaDonId)
        {
            InitializeComponent();
            this.nhanVienId = nhanVienId;
            this.vaiTro = vaiTro.Trim().ToLower();
            this.hoaDonId = hoaDonId;
            this.isNhanVien = this.vaiTro == "nhanvien";
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            log.Debug("frmHoaDon_Load bắt đầu"); // DEBUG log 1
            LoadDanhSachHoaDon();
            LoadChiTietHoaDon();
            UpdateButtonStatus();

            btnThemHoaDonMoi.Visible = isNhanVien;
            btnThemHoaDonMoi.Enabled = isNhanVien;
            btnXoaHoaDon.Visible = isNhanVien;
            btnXoaHoaDon.Enabled = isNhanVien && KiemTraCoTheXoa();

            if (!isNhanVien)
            {
                btnThemMon.Enabled = false;
                btnXoaMon.Enabled = false;
                btnThanhToan.Enabled = false;
            }
            log.Debug("frmHoaDon_Load kết thúc"); // DEBUG log 2
        }

        // ===========================
        // LOAD DATA
        // ===========================
        private void LoadChiTietHoaDon()
        {
            log.Debug($"LoadChiTietHoaDon cho hóa đơn #{hoaDonId}"); // DEBUG log 3
            string sql = @"SELECT c.Id, m.TenMon, c.SoLuong, m.Gia, (c.SoLuong * m.Gia) AS ThanhTien
                           FROM ChiTietHoaDon c
                           JOIN Mon m ON c.MonId = m.Id
                           WHERE c.HoaDonId = @id";
            dgvChiTietHoaDon.DataSource = DataAccess.GetDataTable(sql, new SqlParameter("@id", hoaDonId));
        }

        private void LoadDanhSachHoaDon()
        {
            dgvHoaDon.DataSource = null;
            dgvHoaDon.Rows.Clear();
            dgvHoaDon.Columns.Clear();

            string sql = @"
        SELECT 
            h.Id,
            h.NgayTao,
            tk.TenNV AS NhanVien,
            ISNULL((SELECT SUM(ct.SoLuong * m.Gia) FROM ChiTietHoaDon ct JOIN Mon m ON ct.MonId = m.Id WHERE ct.HoaDonId = h.Id), 0) AS TongTien,
            ISNULL((SELECT SUM(SoLuong) FROM ChiTietHoaDon WHERE HoaDonId = h.Id), 0) AS SoLuongMon,
            h.TrangThai
        FROM HoaDon h
        LEFT JOIN TaiKhoan tk ON h.NhanVienId = tk.Id
        ORDER BY h.NgayTao DESC";

            DataTable dt = DataAccess.GetDataTable(sql);
            dgvHoaDon.DataSource = dt;

            if (dgvHoaDon.Columns["TongTien"] != null)
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
        }

        // ===========================
        // BUTTON / EVENT HANDLER
        // ===========================
        private void UpdateButtonStatus()
        {
            bool chuaThanhToan = GetTrangThaiHoaDon() == "Chưa thanh toán";
            btnThemMon.Enabled = isNhanVien && chuaThanhToan;
            btnXoaMon.Enabled = isNhanVien && chuaThanhToan;
            btnThanhToan.Enabled = isNhanVien && chuaThanhToan;
            btnXoaHoaDon.Enabled = isNhanVien && KiemTraCoTheXoa();
        }

        private string GetTrangThaiHoaDon()
        {
            string sql = "SELECT TrangThai FROM HoaDon WHERE Id = @id";
            object kq = DataAccess.ExecuteScalar(sql, new SqlParameter("@id", hoaDonId));
            return kq?.ToString().Trim() ?? "";
        }

        private bool KiemTraCoTheXoa()
        {
            if (dgvHoaDon.SelectedRows.Count == 0) return false;
            string trangThai = dgvHoaDon.SelectedRows[0].Cells["TrangThai"].Value?.ToString().Trim();
            return trangThai == "Chưa thanh toán";
        }

        private void dgvHoaDon_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                hoaDonId = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["Id"].Value);
                log.Debug($"Người dùng chọn hóa đơn ID = {hoaDonId}"); // DEBUG log 4
                LoadChiTietHoaDon();
                UpdateButtonStatus();
            }
        }

        private void dgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString().Trim();
                if (trangThai == "Đã thanh toán")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font(dgvHoaDon.Font, FontStyle.Bold);
                }
                else if (trangThai == "Chưa thanh toán")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvHoaDon.Font, FontStyle.Bold);
                }
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

        // ===========================
        // THÊM / XÓA MÓN
        // ===========================
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (!isNhanVien)
            {
                MessageBox.Show("Chỉ nhân viên mới được thêm món!");
                return;
            }

            frmMon frm = new frmMon(hoaDonId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                log.Debug($"Đã thêm món vào hóa đơn #{hoaDonId}"); // DEBUG log 5
                LoadChiTietHoaDon();
                LoadDanhSachHoaDon();
                UpdateButtonStatus();
                MessageBox.Show("Đã thêm món vào hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (!isNhanVien)
            {
                MessageBox.Show("Chỉ nhân viên mới được xóa món!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvChiTietHoaDon.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvChiTietHoaDon.SelectedRows[0].Cells["Id"].Value);
                try
                {
                    string sql = "DELETE FROM ChiTietHoaDon WHERE Id=@id";
                    DataAccess.ExecuteNonQuery(sql, new SqlParameter("@id", id));
                    log.Debug($"Đã xóa món chi tiết ID = {id}"); // DEBUG log 6
                    LoadChiTietHoaDon();
                    UpdateButtonStatus();
                }
                catch (Exception ex)
                {
                    log.Fatal($"FATAL – Lỗi xóa món ID = {id}", ex); // FATAL log 1
                    fatalLog.Fatal($"FATAL – Lỗi xóa món ID = {id}", ex);
                    MessageBox.Show("Lỗi khi xóa món!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ===========================
        // THANH TOÁN / XÓA HÓA ĐƠN
        // ===========================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                log.Info($"Thanh toán hóa đơn #{hoaDonId}"); // INFO
                string sqlUpdate = @"
                    UPDATE HoaDon 
                    SET TrangThai = N'Đã thanh toán',
                        TongTien = ISNULL((SELECT SUM(ct.SoLuong * m.Gia) 
                                           FROM ChiTietHoaDon ct 
                                           JOIN Mon m ON ct.MonId = m.Id 
                                           WHERE ct.HoaDonId = @id), 0),
                        SoLuongMon = ISNULL((SELECT SUM(SoLuong) 
                                             FROM ChiTietHoaDon 
                                             WHERE HoaDonId = @id), 0)
                    WHERE Id = @id";

                DataAccess.ExecuteNonQuery(sqlUpdate, new SqlParameter("@id", hoaDonId));
                MessageBox.Show("Thanh toán thành công!");
                LoadChiTietHoaDon();
                UpdateButtonStatus();
                LoadDanhSachHoaDon();
            }
            catch (Exception ex)
            {
                log.Fatal($"FATAL – Lỗi thanh toán hóa đơn #{hoaDonId}", ex); // FATAL log 2
                fatalLog.Fatal($"FATAL – Lỗi thanh toán hóa đơn #{hoaDonId}", ex);
                MessageBox.Show("Lỗi thanh toán nghiêm trọng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (!isNhanVien)
            {
                MessageBox.Show("Chỉ nhân viên mới được xóa hóa đơn!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Chọn hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["Id"].Value);
            string trangThai = dgvHoaDon.SelectedRows[0].Cells["TrangThai"].Value.ToString().Trim();

            if (trangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không thể xóa hóa đơn đã thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn #{selectedId}?\nTất cả món sẽ bị xóa!",
                                          "Xác nhận xóa",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM HoaDon WHERE Id = @id";
                    int result = DataAccess.ExecuteNonQuery(sql, new SqlParameter("@id", selectedId));
                    if (result > 0)
                    {
                        log.Debug($"Đã xóa hóa đơn ID = {selectedId}"); // DEBUG log 7
                        LoadDanhSachHoaDon();
                        if (hoaDonId == selectedId)
                        {
                            hoaDonId = -1;
                            dgvChiTietHoaDon.DataSource = null;
                            UpdateButtonStatus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal($"FATAL – Lỗi xóa hóa đơn ID = {selectedId}", ex); // FATAL log 3
                    fatalLog.Fatal($"FATAL – Lỗi xóa hóa đơn ID = {selectedId}", ex);
                    MessageBox.Show("Lỗi khi xóa hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            if (!isNhanVien)
            {
                MessageBox.Show("Chỉ nhân viên mới được thêm hóa đơn!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string sqlInsert = "INSERT INTO HoaDon (NhanVienId, NgayTao, TrangThai) VALUES (@nv, GETDATE(), N'Chưa thanh toán'); SELECT SCOPE_IDENTITY();";
                            SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, transaction);
                            cmdInsert.Parameters.AddWithValue("@nv", nhanVienId);
                            object result = cmdInsert.ExecuteScalar();
                            int newHoaDonId = Convert.ToInt32(result);

                            transaction.Commit();

                            log.Debug($"Đã tạo hóa đơn mới #{newHoaDonId}"); // DEBUG log 8
                            MessageBox.Show($"Đã tạo hóa đơn mới #{newHoaDonId}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadDanhSachHoaDon();

                            foreach (DataGridViewRow row in dgvHoaDon.Rows)
                            {
                                if (Convert.ToInt32(row.Cells["Id"].Value) == newHoaDonId)
                                {
                                    row.Selected = true;
                                    hoaDonId = newHoaDonId;
                                    LoadChiTietHoaDon();
                                    GetTrangThaiHoaDon();
                                    break;
                                }
                            }
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal("FATAL – Tạo hóa đơn mới thất bại", ex); // FATAL log 4
                fatalLog.Fatal("FATAL – Tạo hóa đơn mới thất bại", ex);
                MessageBox.Show("Lỗi khi tạo hóa đơn mới!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
