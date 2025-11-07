using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        private int nhanVienId;
        private string vaiTro;
        private int hoaDonId;
        private bool isNhanVien;
        public frmHoaDon(int nhanVienId, string vaiTro, int hoaDonId)
        {
            InitializeComponent();
            this.nhanVienId = nhanVienId;
            this.vaiTro = vaiTro.Trim().ToLower();
            this.hoaDonId = hoaDonId;
            this.isNhanVien = this.vaiTro == "nhanvien"; // QUAN TRỌNG
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
            LoadChiTietHoaDon();
            KiemTraTrangThaiHoaDon();

            btnThemHoaDonMoi.Visible = isNhanVien;
            btnThemHoaDonMoi.Enabled = isNhanVien;
            btnXoaHoaDon.Visible = isNhanVien;

            // SỬA DÒNG NÀY: Gọi hàm kiểm tra xóa
            btnXoaHoaDon.Enabled = isNhanVien && KiemTraCoTheXoa();

            if (!isNhanVien) // Admin
            {
                btnThemMon.Enabled = false;
                btnXoaMon.Enabled = false;
                btnThanhToan.Enabled = false;
            }
        }
        private bool KiemTraCoTheXoa()
        {
            if (dgvHoaDon.SelectedRows.Count == 0) return false;

            string trangThai = dgvHoaDon.SelectedRows[0].Cells["TrangThai"].Value?.ToString().Trim();
            return trangThai == "Chưa thanh toán";
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
            string sql = "SELECT TrangThai FROM HoaDon WHERE Id = @id";
            object kq = DataAccess.ExecuteScalar(sql, new SqlParameter("@id", hoaDonId));
            string trangThai = kq?.ToString() ?? "";

            bool chuaThanhToan = trangThai == "Chưa thanh toán";
            bool isNhanVien = vaiTro.Trim().ToLower() == "nhanvien";

            // Chỉ nhân viên + hóa đơn chưa thanh toán mới được thao tác
            btnThemMon.Enabled = isNhanVien && chuaThanhToan;
            btnXoaMon.Enabled = isNhanVien && chuaThanhToan;
            btnThanhToan.Enabled = isNhanVien && chuaThanhToan;
        }
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
                KiemTraTrangThaiHoaDon();

                // CÁCH 2: HIỆN THÔNG BÁO CỐ ĐỊNH – KHÔNG DÙNG dgvMon
                MessageBox.Show("Đã thêm món vào hóa đơn!",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (vaiTro.Trim().ToLower() != "nhanvien")
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
                KiemTraTrangThaiHoaDon();
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string sqlUpdate = @"UPDATE HoaDon 
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
            KiemTraTrangThaiHoaDon();
            LoadDanhSachHoaDon(); // CẬP NHẬT LẠI DANH SÁCH
        }



        private void LoadDanhSachHoaDon()
        {
            // XÓA HOÀN TOÀN DỮ LIỆU CŨ
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

            // Định dạng cột
            if (dgvHoaDon.Columns["TongTien"] != null)
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
        }
        private void dgvHoaDon_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectedHoaDonId = Convert.ToInt32(dgvHoaDon.Rows[e.RowIndex].Cells["Id"].Value);
                hoaDonId = selectedHoaDonId;

                LoadChiTietHoaDon();
                KiemTraTrangThaiHoaDon();
            }
        }

        private void dgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra cột "TrangThai" và có giá trị
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
        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            // 1. Phân quyền: chỉ nhân viên
            if (vaiTro.Trim().ToLower() != "nhanvien")
            {
                MessageBox.Show("Chỉ nhân viên mới được xóa hóa đơn!", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra có chọn dòng không
            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Chọn hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedId = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["Id"].Value);
            string trangThai = dgvHoaDon.SelectedRows[0].Cells["TrangThai"].Value.ToString().Trim();

            // 3. Không cho xóa nếu đã thanh toán
            if (trangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không thể xóa hóa đơn đã thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // 4. Xác nhận xóa
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa hóa đơn #{selectedId}?\nTất cả món sẽ bị xóa!",
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
                        LoadDanhSachHoaDon(); // Cập nhật lại bảng

                        // Nếu hóa đơn đang xem bị xóa → reset
                        if (hoaDonId == selectedId)
                        {
                            hoaDonId = -1;
                            dgvChiTietHoaDon.DataSource = null;
                            KiemTraTrangThaiHoaDon();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                hoaDonId = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["Id"].Value);
                LoadChiTietHoaDon();
                KiemTraTrangThaiHoaDon();

                btnXoaHoaDon.Enabled = isNhanVien && KiemTraCoTheXoa();
            }
        }

        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHoaDon.SelectedRows.Count == 0) return;

            int monId = Convert.ToInt32(dgvChiTietHoaDon.SelectedRows[0].Cells["Id"].Value);

            using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Thêm món vào chi tiết
                        string sqlInsert = "INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@hd, @m, 1)";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, transaction);
                        cmdInsert.Parameters.AddWithValue("@hd", hoaDonId);
                        cmdInsert.Parameters.AddWithValue("@m", monId);
                        cmdInsert.ExecuteNonQuery();

                        // 2. CẬP NHẬT TỔNG TIỀN & SỐ LƯỢNG MÓN
                        string sqlUpdate = @"
                            UPDATE HoaDon 
                            SET 
                                TongTien = ISNULL((SELECT SUM(ct.SoLuong * m.Gia) 
                                                   FROM ChiTietHoaDon ct 
                                                   JOIN Mon m ON ct.MonId = m.Id 
                                                   WHERE ct.HoaDonId = @hd), 0),
                                SoLuongMon = ISNULL((SELECT SUM(SoLuong) 
                                                     FROM ChiTietHoaDon 
                                                     WHERE HoaDonId = @hd), 0)
                            WHERE Id = @hd";

                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction);
                        cmdUpdate.Parameters.AddWithValue("@hd", hoaDonId);
                        cmdUpdate.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            MessageBox.Show("Đã thêm món vào hóa đơn!");
            this.DialogResult = DialogResult.OK; // Đóng form
        }
    }
}
