using log4net;
using QuanLiQuanCafe.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLiQuanCafe.DAL;

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
            SelectFirstHoaDon();
            UpdateButtonStatus();
        }

        #region Load Data

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
                MessageBox.Show("Error in LoadDanhSachHoaDon: " + ex.Message);
                log.Error("Error in LoadDanhSachHoaDon", ex);
                fatalLog.Fatal("Fatal error in LoadDanhSachHoaDon", ex);
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
                log.Error("Error in LoadChiTietHoaDon", ex);
            }
        }

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

        #region Button Status

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

        #region Sự kiện dgvHoaDon

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectHoaDonFromRowIndex(e.RowIndex);
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

        #region Actions

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

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHoaDon.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvChiTietHoaDon.SelectedRows[0].Cells["Id"].Value);
            DataAccess.ExecuteNonQuery(HoaDonQueries.SQL_DELETE_MON, new SqlParameter("@id", id));
            LoadChiTietHoaDon();
            LoadDanhSachHoaDon();
            UpdateButtonStatus();
        }

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
                log.Error("Error in btnThemHoaDonMoi_Click", ex);
                MessageBox.Show("Tạo hóa đơn mới thất bại: " + ex.Message);
            }
        }

        #endregion

        #region Lọc hóa đơn

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
        }

        private void btnDaThanhToan_Click_1(object sender, EventArgs e)
        {
            log.Info("btnDaThanhToan clicked");
            LoadDanhSachHoaDon("Đã thanh toán");
        }

        private void btnChuaThanhToan_Click_1(object sender, EventArgs e)
        {
            log.Info("btnChuaThanhToan clicked");
            LoadDanhSachHoaDon("Chưa thanh toán");
        }

        #endregion
    }
}
