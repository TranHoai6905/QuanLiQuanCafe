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

        private void LoadDanhSachHoaDon(string trangThai = "", int nhanVienIdLoc = 0, DateTime? ngay = null)
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
                    sql += " AND CAST(h.NgayTao AS DATE) = @ngay";
                    paramList.Add(new SqlParameter("@ngay", ngay.Value.Date));
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
                log.Error("Lỗi LoadDanhSachHoaDon", ex);
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

        #region Xử lý DataGridView
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
            if (trangThai == "Đã thanh toán") return;

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
                        trans.Commit();
                        ApplyCurrentFilters();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Lỗi btnThemHoaDonMoi_Click", ex);
            }
        }
        #endregion

        #region Nút lọc
        private void btnTatCa_Click(object sender, EventArgs e)
        {
            ResetFilterButtons();
            SetButtonSelected(btnTatCa);
            ApplyCurrentFilters();
        }

        private void btnChuaThanhToan_Click(object sender, EventArgs e)
        {
            ResetFilterButtons();
            SetButtonSelected(btnChuaThanhToan);
            ApplyCurrentFilters();
        }

        private void btnDaThanhToan_Click(object sender, EventArgs e)
        {
            ResetFilterButtons();
            SetButtonSelected(btnDaThanhToan);
            ApplyCurrentFilters();
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCurrentFilters();
        }

        private void dtpNgayTaoHoaDon_ValueChanged(object sender, EventArgs e)
        {
            ApplyCurrentFilters();
        }

        private void ApplyCurrentFilters()
        {
            string trangThai = "";
            if (btnChuaThanhToan.FillColor == Color.Transparent) trangThai = "Chưa thanh toán";
            else if (btnDaThanhToan.FillColor == Color.Transparent) trangThai = "Đã thanh toán";

            int nhanVienIdLoc = cboNhanVien.SelectedValue != null ? Convert.ToInt32(cboNhanVien.SelectedValue) : 0;
            DateTime? ngay = dtpNgayTaoHoaDon.Checked ? (DateTime?)dtpNgayTaoHoaDon.Value.Date : null;

            LoadDanhSachHoaDon(trangThai, nhanVienIdLoc, ngay);
        }
        #endregion

        #region Định dạng nút lọc
        private void SetButtonSelected(Guna2Button btn)
        {
            btn.FillColor = Color.Transparent;
            btn.ForeColor = Color.Black;
            btn.BorderThickness = 2;
            btn.BorderColor = Color.FromArgb(150, 75, 0);
            btn.HoverState.FillColor = Color.Transparent;
            btn.HoverState.ForeColor = Color.Black;
        }

        private void ResetFilterButtons()
        {
            ResetButton(btnTatCa);
            ResetButton(btnDaThanhToan);
            ResetButton(btnChuaThanhToan);
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
