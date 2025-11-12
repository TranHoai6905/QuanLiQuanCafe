using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLiQuanCafe.Models;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        private int nhanVienId;       // ID nhân viên đăng nhập
        private string vaiTro;        // Vai trò (nhanvien/admin)
        private int hoaDonId;         // ID hóa đơn đang chọn
        private bool isNhanVien;      // Biến phân quyền

        public frmHoaDon(int nhanVienId, string vaiTro, int hoaDonId)
        {
            InitializeComponent();
            this.nhanVienId = nhanVienId;
            this.vaiTro = vaiTro.Trim().ToLower();
            this.hoaDonId = hoaDonId;
            this.isNhanVien = this.vaiTro == "nhanvien"; // Dùng 1 biến duy nhất cho phân quyền
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon(); // Load danh sách hóa đơn
            LoadChiTietHoaDon();   // Load chi tiết hóa đơn
            UpdateButtonStatus();  // Cập nhật trạng thái nút theo phân quyền và trạng thái

            // Nút chỉ hiển thị cho nhân viên
            btnThemHoaDonMoi.Visible = isNhanVien;
            btnThemHoaDonMoi.Enabled = isNhanVien;
            btnXoaHoaDon.Visible = isNhanVien;
            btnXoaHoaDon.Enabled = isNhanVien && KiemTraCoTheXoa();

            // Admin không được thao tác thêm/xóa món
            if (!isNhanVien)
            {
                btnThemMon.Enabled = false;
                btnXoaMon.Enabled = false;
                btnThanhToan.Enabled = false;
            }
        }

        // ===========================
        // LOAD DATA
        // ===========================

        // Load chi tiết hóa đơn hiện tại
        private void LoadChiTietHoaDon()
        {
            string sql = @"SELECT c.Id, m.TenMon, c.SoLuong, m.Gia, (c.SoLuong * m.Gia) AS ThanhTien
                           FROM ChiTietHoaDon c
                           JOIN Mon m ON c.MonId = m.Id
                           WHERE c.HoaDonId = @id";
            dgvChiTietHoaDon.DataSource = DataAccess.GetDataTable(sql, new SqlParameter("@id", hoaDonId));
        }

        // Load danh sách hóa đơn
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

            // Format tiền
            if (dgvHoaDon.Columns["TongTien"] != null)
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
        }

        // ===========================
        // BUTTON / EVENT HANDLER
        // ===========================

        // Phân quyền: chỉ nhân viên + chưa thanh toán mới thao tác được
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

        // Kiểm tra xem hóa đơn có thể xóa hay không
        private bool KiemTraCoTheXoa()
        {
            if (dgvHoaDon.SelectedRows.Count == 0) return false;
            string trangThai = dgvHoaDon.SelectedRows[0].Cells["TrangThai"].Value?.ToString().Trim();
            return trangThai == "Chưa thanh toán";
        }

        // Click chọn hóa đơn
        private void dgvHoaDon_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                hoaDonId = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["Id"].Value);
                LoadChiTietHoaDon();
                UpdateButtonStatus();
            }
        }

        // Format màu cột trạng thái
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

        // Chọn dòng trong dgvHoaDon
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

        // Thêm món vào hóa đơn
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
                LoadChiTietHoaDon();
                LoadDanhSachHoaDon();
                UpdateButtonStatus();
                MessageBox.Show("Đã thêm món vào hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Xóa món từ hóa đơn
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
                string sql = "DELETE FROM ChiTietHoaDon WHERE Id=@id";
                DataAccess.ExecuteNonQuery(sql, new SqlParameter("@id", id));
                LoadChiTietHoaDon();
                UpdateButtonStatus();
            }
        }

        // ===========================
        // THANH TOÁN / XÓA HÓA ĐƠN
        // ===========================

        // Thanh toán hóa đơn
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
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
            LoadDanhSachHoaDon(); // Cập nhật lại danh sách
        }

        // Xóa hóa đơn
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
                        MessageBox.Show("Xóa hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            // Chỉ nhân viên mới được thêm hóa đơn
            if (!isNhanVien)
            {
                MessageBox.Show("Chỉ nhân viên mới được thêm hóa đơn!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Mở kết nối và transaction
                using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Tạo hóa đơn mới
                            string sqlInsert = "INSERT INTO HoaDon (NhanVienId, NgayTao, TrangThai) VALUES (@nv, GETDATE(), N'Chưa thanh toán'); SELECT SCOPE_IDENTITY();";
                            SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, transaction);
                            cmdInsert.Parameters.AddWithValue("@nv", nhanVienId);
                            object result = cmdInsert.ExecuteScalar();
                            int newHoaDonId = Convert.ToInt32(result);

                            // 2. Commit transaction
                            transaction.Commit();

                            // 3. Thông báo thành công
                            MessageBox.Show($"Đã tạo hóa đơn mới #{newHoaDonId}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 4. Cập nhật danh sách hóa đơn
                            LoadDanhSachHoaDon();

                            // 5. Chọn hóa đơn mới trong DataGridView
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
                MessageBox.Show("Lỗi khi tạo hóa đơn mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
