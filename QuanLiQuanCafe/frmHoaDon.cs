// File: frmHoaDon.cs
// Namespace: QuanLiQuanCafe
// Mục đích: Form Windows để quản lý hóa đơn (HoaDon).
// Form này xử lý việc tải, hiển thị, lọc và thực hiện các hành động trên hóa đơn và chi tiết của chúng.
// Lưu ý: Đã refactoring để cải thiện tính đọc, thêm chú thích, và đảm bảo không thay đổi chức năng.
// Form sử dụng DAL trực tiếp cho các truy vấn, nhưng BUS có sẵn cho việc trừu tượng hóa sau này.

using log4net;
using QuanLiQuanCafe.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLiQuanCafe.DAL;
using System.Drawing;
using QuanLiQuanCafe.Helpers;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        /// <summary>
        /// ID nhân viên của người dùng hiện tại.
        /// </summary>
        private int nhanVienId;

        /// <summary>
        /// ID hóa đơn đang được chọn.
        /// </summary>
        private int hoaDonId;

        /// <summary>
        /// Logger cho các lỗi thông thường.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(frmHoaDon));

        /// <summary>
        /// Logger cho các lỗi nghiêm trọng.
        /// </summary>
        private static readonly ILog fatalLog = LogManager.GetLogger("FatalLogger");

        /// <summary>
        /// Constructor cho form hóa đơn.
        /// </summary>
        /// <param name="nhanVienId">ID của nhân viên sử dụng form.</param>
        public frmHoaDon(int nhanVienId)
        {
            InitializeComponent();
            this.nhanVienId = nhanVienId;
        }

        /// <summary>
        /// Xử lý sự kiện load form: Tải danh sách hóa đơn, chọn hóa đơn đầu tiên, và cập nhật trạng thái nút.
        /// </summary>
        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            ButtonHelper.EnableShadow(this);
            DataGridViewHelper.SetHeaderColor(dgvHoaDon);
            DataGridViewHelper.SetHeaderColor(dgvChiTietHoaDon);
            LoadDanhSachHoaDon();
            SelectFirstHoaDon();
            UpdateButtonStatus();
        }

        #region Các phương thức tải dữ liệu
        /// <summary>
        /// Tải danh sách hóa đơn, có thể lọc theo trạng thái.
        /// </summary>
        /// <param name="trangThai">Bộ lọc trạng thái tùy chọn (ví dụ: "Chưa thanh toán").</param>
        private void LoadDanhSachHoaDon(string trangThai = "")
        {
            try
            {
                string sql = HoaDonQueries.SQL_LOAD_DANH_SACH;
                List<SqlParameter> paramList = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(trangThai))
                {
                    sql = HoaDonQueries.SQL_LOC_HOA_DON_BASE + " AND LTRIM(RTRIM(h.TrangThai)) = @tt ORDER BY h.NgayTao DESC";
                    paramList.Add(new SqlParameter("@tt", trangThai));
                }
                DataTable dt = DataAccess.GetDataTable(sql, paramList.ToArray());
                dgvHoaDon.DataSource = dt;
                SelectFirstHoaDon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong LoadDanhSachHoaDon: " + ex.Message);
                log.Error("Lỗi trong LoadDanhSachHoaDon", ex);
                fatalLog.Fatal("Lỗi nghiêm trọng trong LoadDanhSachHoaDon", ex);
            }
        }

        /// <summary>
        /// Tải chi tiết của hóa đơn đang chọn.
        /// </summary>
        private void LoadChiTietHoaDon()
        {
            if (hoaDonId <= 0)
            {
                dgvChiTietHoaDon.DataSource = null;
                return;
            }
            try
            {
                DataTable dt = DataAccess.GetDataTable(HoaDonQueries.SQL_LOAD_CHI_TIET,
                    new SqlParameter("@id", hoaDonId));
                dgvChiTietHoaDon.DataSource = dt;
                if (dgvChiTietHoaDon.Columns["Gia"] != null)
                    dgvChiTietHoaDon.Columns["Gia"].DefaultCellStyle.Format = "N0";
                if (dgvChiTietHoaDon.Columns["ThanhTien"] != null)
                    dgvChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                log.Error("Lỗi trong LoadChiTietHoaDon", ex);
            }
        }

        /// <summary>
        /// Chọn hóa đơn đầu tiên trong danh sách nếu có.
        /// </summary>
        private void SelectFirstHoaDon()
        {
            if (dgvHoaDon.Rows.Count > 0)
            {
                dgvHoaDon.ClearSelection();
                dgvHoaDon.Rows[0].Selected = true;
                hoaDonId = Convert.ToInt32(dgvHoaDon.Rows[0].Cells["Id"].Value);
                LoadChiTietHoaDon();
            }
            else
            {
                hoaDonId = 0;
                dgvChiTietHoaDon.DataSource = null;
            }
        }
        #endregion

        #region Các phương thức trạng thái nút
        /// <summary>
        /// Cập nhật trạng thái kích hoạt của các nút hành động dựa trên trạng thái hóa đơn hiện tại.
        /// </summary>
        private void UpdateButtonStatus()
        {
            bool chuaThanhToan = GetTrangThaiHoaDon() == "Chưa thanh toán";
            btnThemMon.Enabled = chuaThanhToan;
            btnXoaMon.Enabled = chuaThanhToan;
            btnThanhToan.Enabled = chuaThanhToan;
            btnXoaHoaDon.Enabled = chuaThanhToan;
        }

        /// <summary>
        /// Lấy trạng thái của hóa đơn hiện tại.
        /// </summary>
        /// <returns>Chuỗi trạng thái, đã trim.</returns>
        private string GetTrangThaiHoaDon()
        {
            if (hoaDonId <= 0) return "";
            object kq = DataAccess.ExecuteScalar(HoaDonQueries.SQL_GET_TRANG_THAI,
                new SqlParameter("@id", hoaDonId));
            return kq?.ToString().Trim() ?? "";
        }
        #endregion

        #region Các xử lý sự kiện DataGridView
        /// <summary>
        /// Xử lý click ô trên lưới hóa đơn: Chọn hóa đơn từ hàng được click.
        /// </summary>
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectHoaDonFromRowIndex(e.RowIndex);
        }

        /// <summary>
        /// Xử lý thay đổi lựa chọn trên lưới hóa đơn: Cập nhật ID hóa đơn, tải chi tiết, và cập nhật nút.
        /// </summary>
        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                hoaDonId = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["Id"].Value);
                LoadChiTietHoaDon();
                UpdateButtonStatus();
            }
        }

        /// <summary>
        /// Chọn hóa đơn dựa trên chỉ số hàng và tải chi tiết.
        /// </summary>
        /// <param name="rowIndex">Chỉ số hàng cần chọn.</param>
        private void SelectHoaDonFromRowIndex(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgvHoaDon.Rows.Count)
            {
                hoaDonId = Convert.ToInt32(dgvHoaDon.Rows[rowIndex].Cells["Id"].Value);
                LoadChiTietHoaDon();
                UpdateButtonStatus();
            }
        }
        #endregion

        #region Các xử lý hành động nút
        /// <summary>
        /// Mở form thêm món cho hóa đơn hiện tại.
        /// </summary>
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            using (frmMon frm = new frmMon(hoaDonId))
            {
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
        }

        /// <summary>
        /// Xóa món được chọn từ chi tiết hóa đơn.
        /// </summary>
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHoaDon.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvChiTietHoaDon.SelectedRows[0].Cells["Id"].Value);
            DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_DELETE_MON, new SqlParameter("@id", id));
            LoadChiTietHoaDon();
            LoadDanhSachHoaDon();
            UpdateButtonStatus();
        }

        /// <summary>
        /// Xử lý thanh toán cho hóa đơn hiện tại nếu có món.
        /// </summary>
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            object kq = DataAccess.ExecuteScalar(HoaDonQueries.SQL_COUNT_MON, new SqlParameter("@id", hoaDonId));
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

        /// <summary>
        /// Xóa hóa đơn hiện tại sau khi xác nhận, nếu chưa thanh toán.
        /// </summary>
        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (hoaDonId <= 0) return;
            string trangThai = GetTrangThaiHoaDon();
            if (trangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không thể xóa hóa đơn đã thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            var confirm = MessageBox.Show($"Xóa hóa đơn #{hoaDonId}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_DELETE_HOA_DON,
                    new SqlParameter("@id", hoaDonId));
                LoadDanhSachHoaDon();
                dgvChiTietHoaDon.DataSource = null;
            }
        }

        /// <summary>
        /// Tạo hóa đơn mới sử dụng transaction để đảm bảo tính toàn vẹn dữ liệu.
        /// </summary>
        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                log.Error("Lỗi trong btnThemHoaDonMoi_Click", ex);
                MessageBox.Show("Tạo hóa đơn mới thất bại: " + ex.Message);
            }
        }
        #endregion

        #region Các xử lý nút lọc
        /// <summary>
        /// Tải tất cả hóa đơn và đặt nút 'Tất cả' là đã chọn.
        /// </summary>
        private void btnTatCa_Click(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
            ResetFilterButtons();
            SetButtonSelected(btnTatCa);
        }

        /// <summary>
        /// Tải hóa đơn đã thanh toán và đặt nút 'Đã thanh toán' là đã chọn.
        /// </summary>
        private void btnDaThanhToan_Click_1(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon("Đã thanh toán");
            ResetFilterButtons();
            SetButtonSelected(btnDaThanhToan);
        }

        /// <summary>
        /// Tải hóa đơn chưa thanh toán và đặt nút 'Chưa thanh toán' là đã chọn.
        /// </summary>
        private void btnChuaThanhToan_Click_1(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon("Chưa thanh toán");
            ResetFilterButtons();
            SetButtonSelected(btnChuaThanhToan);
        }
        #endregion

        /// <summary>
        /// Định dạng ô trong lưới hóa đơn, thay đổi màu hàng dựa trên trạng thái.
        /// </summary>
        private void dgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DoiMauHoaDon(e);
        }

        /// <summary>
        /// Áp dụng định dạng màu cho hàng hóa đơn dựa trên trạng thái.
        /// </summary>
        /// <param name="e">Tham số sự kiện định dạng.</param>
        private void DoiMauHoaDon(DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvHoaDon.Columns["TrangThai"] == null) return;
            var row = dgvHoaDon.Rows[e.RowIndex];
            string trangThai = row.Cells["TrangThai"].Value?.ToString().Trim() ?? "";
            // Chưa thanh toán → nền cam nhạt
            if (trangThai == "Chưa thanh toán")
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 150);
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            // Đã thanh toán → nền xanh nhạt
            else if (trangThai == "Đã thanh toán")
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 200);
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Đặt kiểu hiển thị cho nút lọc được chọn.
        /// </summary>
        /// <param name="btn">Nút cần đặt kiểu chọn.</param>
        private void SetButtonSelected(Guna.UI2.WinForms.Guna2Button btn)
        {
            btn.FillColor = Color.Transparent; // Nền trong suốt
            btn.ForeColor = Color.Black; // Chữ đen
            btn.BorderThickness = 2; // Viền hiển thị
            btn.BorderColor = Color.FromArgb(150, 75, 0); // Viền nâu
            btn.HoverState.FillColor = Color.Transparent;
            btn.HoverState.ForeColor = Color.Black;
        }

        /// <summary>
        /// Reset kiểu hiển thị của tất cả nút lọc về mặc định.
        /// </summary>
        private void ResetFilterButtons()
        {
            ResetButton(btnTatCa);
            ResetButton(btnDaThanhToan);
            ResetButton(btnChuaThanhToan);
        }

        /// <summary>
        /// Reset kiểu hiển thị của một nút về mặc định.
        /// </summary>
        /// <param name="btn">Nút cần reset.</param>
        private void ResetButton(Guna.UI2.WinForms.Guna2Button btn)
        {
            btn.FillColor = Color.FromArgb(150, 75, 0); // Nền nâu
            btn.ForeColor = Color.White; // Chữ trắng
            btn.BorderThickness = 0; // Không viền
        }
    }
}