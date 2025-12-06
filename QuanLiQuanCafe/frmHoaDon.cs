// File: frmHoaDon.cs
using log4net;
using QuanLiQuanCafe.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLiQuanCafe.DAL;
using QuanLiQuanCafe.Helpers;
using Guna.UI2.WinForms;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        private int nhanVienId;
        private int hoaDonId;

        private static readonly ILog log = LogManager.GetLogger(typeof(frmHoaDon));

        // Biến trạng thái nút lọc
        private enum LoaiTrangThai { TatCa, ChuaThanhToan, DaThanhToan }
        private LoaiTrangThai trangThaiHienTai = LoaiTrangThai.TatCa;

        public frmHoaDon(int nhanVienId)
        {
            InitializeComponent();
            this.nhanVienId = nhanVienId;
            dgvHoaDon.SelectionChanged += dgvHoaDon_SelectionChanged;
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            ButtonHelper.EnableShadow(this);
            DataGridViewHelper.SetHeaderColor(dgvHoaDon);
            DataGridViewHelper.SetHeaderColor(dgvChiTietHoaDon);

            dgvHoaDon.AutoGenerateColumns = true;
            dgvChiTietHoaDon.AutoGenerateColumns = true;

            LoadCboNhanVien();

            // Mặc định hiển thị tất cả hóa đơn
            ResetFilterButtons();
            SetButtonSelected(btnTatCa);
            trangThaiHienTai = LoaiTrangThai.TatCa;
            cboNhanVien.SelectedValue = 0;

            ApplyCurrentFilters();
            UpdateButtonStatus();
        }

        #region Load dữ liệu
        private void LoadCboNhanVien()
        {
            try
            {
                DataTable dt = DataAccess.GetDataTable("SELECT Id, HoTen FROM TaiKhoan ORDER BY HoTen");

                DataRow allRow = dt.NewRow();
                allRow["Id"] = 0;
                allRow["HoTen"] = "Tất cả";
                dt.Rows.InsertAt(allRow, 0);

                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "Id";
                cboNhanVien.DataSource = dt;
                cboNhanVien.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                log.Error("Lỗi LoadCboNhanVien", ex);
            }
        }

        private void LoadDanhSachHoaDon(string trangThai = "", int nhanVienIdLoc = 0, int? ngay = null, int? thang = null, int? nam = null)
        {
            try
            {
                string sql = HoaDonQueries.SQL_LOC_HOA_DON_BASE;
                List<SqlParameter> paramList = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(trangThai))
                {
                    sql += " AND LTRIM(RTRIM(h.TrangThai)) = @tt";
                    paramList.Add(new SqlParameter("@tt", trangThai));
                }

                if (nhanVienIdLoc > 0)
                {
                    sql += " AND h.NhanVienId = @nv";
                    paramList.Add(new SqlParameter("@nv", nhanVienIdLoc));
                }

                if (ngay.HasValue)
                {
                    sql += " AND DAY(h.NgayTao) = @ngay";
                    paramList.Add(new SqlParameter("@ngay", ngay.Value));
                }
                if (thang.HasValue)
                {
                    sql += " AND MONTH(h.NgayTao) = @thang";
                    paramList.Add(new SqlParameter("@thang", thang.Value));
                }
                if (nam.HasValue)
                {
                    sql += " AND YEAR(h.NgayTao) = @nam";
                    paramList.Add(new SqlParameter("@nam", nam.Value));
                }

                sql += " ORDER BY h.NgayTao DESC";

                DataTable dt = DataAccess.GetDataTable(sql, paramList.ToArray());

                dgvHoaDon.SelectionChanged -= dgvHoaDon_SelectionChanged;
                dgvHoaDon.DataSource = dt;

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

                dgvHoaDon.SelectionChanged += dgvHoaDon_SelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi LoadDanhSachHoaDon: " + ex.Message);
            }
        }

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
                log.Error("Lỗi LoadChiTietHoaDon", ex);
            }
        }
        #endregion

        #region Trạng thái nút
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
            object kq = DataAccess.ExecuteScalar(HoaDonQueries.SQL_GET_TRANG_THAI,
                                                 new SqlParameter("@id", hoaDonId));
            return kq?.ToString().Trim() ?? "";
        }
        #endregion

        #region DataGridView
        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                try
                {
                    hoaDonId = Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["Id"].Value);
                    LoadChiTietHoaDon();
                    UpdateButtonStatus();
                }
                catch (Exception ex)
                {
                    log.Error("Lỗi lấy Id hóa đơn", ex);
                }
            }
            else
            {
                hoaDonId = 0;
                dgvChiTietHoaDon.DataSource = null;
            }
        }

        private void dgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvHoaDon.Columns["TrangThai"] == null) return;
            var row = dgvHoaDon.Rows[e.RowIndex];
            string trangThai = row.Cells["TrangThai"].Value?.ToString().Trim() ?? "";
            if (trangThai == "Chưa thanh toán")
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 150);
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (trangThai == "Đã thanh toán")
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(200, 255, 200);
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
        }
        #endregion

        #region Nút hành động
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (hoaDonId <= 0) return;
            using (frmMon frm = new frmMon(hoaDonId))
            {
                frm.OnMonDaDuocThem += (hdId) =>
                {
                    if (hdId == hoaDonId)
                    {
                        LoadChiTietHoaDon();
                        ApplyCurrentFilters();
                        UpdateButtonStatus();
                    }
                };
                frm.ShowDialog();
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHoaDon.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvChiTietHoaDon.SelectedRows[0].Cells["Id"].Value);
            DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_DELETE_MON, new SqlParameter("@id", id));
            LoadChiTietHoaDon();
            ApplyCurrentFilters();
            UpdateButtonStatus();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (hoaDonId <= 0) return;
            object kq = DataAccess.ExecuteScalar(HoaDonQueries.SQL_COUNT_MON,
                                                 new SqlParameter("@id", hoaDonId));
            if (Convert.ToInt32(kq) == 0)
            {
                MessageBox.Show("Hóa đơn chưa có món nào!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_THANH_TOAN, new SqlParameter("@id", hoaDonId));
            LoadChiTietHoaDon();
            ApplyCurrentFilters();
            UpdateButtonStatus();
        }

        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (hoaDonId <= 0) return;
            string trangThai = GetTrangThaiHoaDon();
            if (trangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không thể xóa hóa đơn đã thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Xóa hóa đơn #{hoaDonId}?", "Xác nhận",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_DELETE_HOA_DON,
                                           new SqlParameter("@id", hoaDonId));
                ApplyCurrentFilters();
                dgvChiTietHoaDon.DataSource = null;
            }
        }

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
                        if (result == null || result == DBNull.Value)
                        {
                            MessageBox.Show("Không nhận được ID hóa đơn từ SQL.\nSQL thiếu SELECT SCOPE_IDENTITY().",
                                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            trans.Rollback();
                            return;
                        }

                        hoaDonId = Convert.ToInt32(result);
                        trans.Commit();
                    }
                }

                // Bỏ mọi filter để hóa đơn mới hiện lên ngay
                ResetFilterButtons();
                SetButtonSelected(btnTatCa);
                trangThaiHienTai = LoaiTrangThai.TatCa;
                cboNhanVien.SelectedValue = 0;

                ApplyCurrentFilters();
                ChonDongHoaDon(hoaDonId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hóa đơn mới:\n" + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChonDongHoaDon(int id)
        {
            foreach (DataGridViewRow row in dgvHoaDon.Rows)
            {
                if (Convert.ToInt32(row.Cells["Id"].Value) == id)
                {
                    row.Selected = true;
                    dgvHoaDon.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }
        #endregion

        #region Nút lọc
        private void btnTatCa_Click(object sender, EventArgs e)
        {
            trangThaiHienTai = LoaiTrangThai.TatCa;
            SetButtonSelected(btnTatCa);
            ApplyCurrentFilters();
        }


        private void btnChuaThanhToan_Click(object sender, EventArgs e)
        {
            trangThaiHienTai = LoaiTrangThai.ChuaThanhToan;
            SetButtonSelected(btnChuaThanhToan);
            ApplyCurrentFilters();
        }

        private void btnDaThanhToan_Click(object sender, EventArgs e)
        {
            trangThaiHienTai = LoaiTrangThai.DaThanhToan;
            SetButtonSelected(btnDaThanhToan);
            ApplyCurrentFilters();
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCurrentFilters();
        }

        private void btnDanhSachDayDu_Click(object sender, EventArgs e)
        {
            // Load tất cả hóa đơn, bỏ qua filter
            ResetFilterButtons();
            SetButtonSelected(btnTatCa);
            trangThaiHienTai = LoaiTrangThai.TatCa;
            cboNhanVien.SelectedValue = 0;
            txtTimNgay.Text = "";
            txtTimThang.Text = "";
            txtTimNam.Text = "";
            ApplyCurrentFilters();
        }

        private void btnTimNgay_Click(object sender, EventArgs e)
        {
            ApplyCurrentFilters();
        }

        private void ApplyCurrentFilters()
        {
            try
            {
                // 1. Xác định trạng thái
                string trangThai = "";
                switch (trangThaiHienTai)
                {
                    case LoaiTrangThai.ChuaThanhToan: trangThai = "Chưa thanh toán"; break;
                    case LoaiTrangThai.DaThanhToan: trangThai = "Đã thanh toán"; break;
                    case LoaiTrangThai.TatCa: trangThai = ""; break;
                }

                // 2. ID nhân viên
                int nhanVienIdLoc = 0;
                if (cboNhanVien.SelectedValue != null)
                    nhanVienIdLoc = Convert.ToInt32(cboNhanVien.SelectedValue);

                // 3. Ngày/Tháng/Năm từ textbox
                int? ngay = string.IsNullOrWhiteSpace(txtTimNgay.Text) ? null : (int?)Convert.ToInt32(txtTimNgay.Text);
                int? thang = string.IsNullOrWhiteSpace(txtTimThang.Text) ? null : (int?)Convert.ToInt32(txtTimThang.Text);
                int? nam = string.IsNullOrWhiteSpace(txtTimNam.Text) ? null : (int?)Convert.ToInt32(txtTimNam.Text);

                // 4. Load danh sách
                LoadDanhSachHoaDon(trangThai, nhanVienIdLoc, ngay, thang, nam);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ApplyCurrentFilters:\n" + ex.Message);
            }
        }
        #endregion

        #region Định dạng nút lọc
        private void SetButtonSelected(Guna2Button btn)
        {
            ButtonHelper.SetButtonSelected(btn, btnTatCa, btnChuaThanhToan, btnDaThanhToan);
        }
        private void ResetFilterButtons()
        {
            ButtonHelper.ResetButtons(btnTatCa, btnChuaThanhToan, btnDaThanhToan);
        }
        private void ResetButton(Guna2Button btn)
        {
            btn.FillColor = Color.FromArgb(150, 75, 0);
            btn.ForeColor = Color.White;
            btn.BorderThickness = 0;
        }
        #endregion
    }
}
