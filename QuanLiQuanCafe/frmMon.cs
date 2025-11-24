using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmMon : Form
    {
        private int hoaDonId; // ID hóa đơn hiện tại
        private List<int> danhSachMonDaChon = new List<int>(); // Danh sách món đã chọn
        private int? hoaDonDangChonId = null; // ID hóa đơn đang chọn từ dgvHoaDonChuaThanhToan

        public frmMon(int hoaDonId)
        {
            InitializeComponent();
            this.hoaDonId = hoaDonId;
        }

        public frmMon()
        {
            InitializeComponent();
        }

        private void frmMon_Load(object sender, EventArgs e)
        {
            LoadLoaiMon();
            LoadMon(); // Load tất cả món
            LoadHoaDonChuaThanhToan(); // Load danh sách hóa đơn chưa thanh toán
        }


        private void LoadHoaDonChuaThanhToan()
        {
            string sql = "SELECT Id, NgayTao, TongTien, SoLuongMon, TrangThai FROM HoaDon WHERE TrangThai = N'Chưa thanh toán'";
            DataTable dt = DataAccess.GetDataTable(sql);
            dgvHoaDonChuaThanhToan.DataSource = dt;

            // Tùy chỉnh hiển thị
            dgvHoaDonChuaThanhToan.Columns["Id"].Visible = false;
            dgvHoaDonChuaThanhToan.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvHoaDonChuaThanhToan.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvHoaDonChuaThanhToan.Columns["SoLuongMon"].HeaderText = "Số lượng món";
            dgvHoaDonChuaThanhToan.Columns["TrangThai"].HeaderText = "Trạng thái";
        }


        // ==========================
        // Load danh sách loại món ComboBox
        // ==========================
        private void LoadLoaiMon()
        {
            string sql = "SELECT DISTINCT Loai FROM Mon WHERE Loai IS NOT NULL";
            DataTable dt = DataAccess.GetDataTable(sql);

            // Thêm mục "Tất cả"
            DataRow row = dt.NewRow();
            row["Loai"] = "Tất cả";
            dt.Rows.InsertAt(row, 0);

            cmbLoaiMon.DataSource = dt;
            cmbLoaiMon.DisplayMember = "Loai";
            cmbLoaiMon.ValueMember = "Loai";
        }

        // ==========================
        // Load danh sách món
        // ==========================
        private void LoadMon(string keyword = "", string loai = "Tất cả")
        {
            string sql = "SELECT * FROM Mon WHERE TenMon LIKE @kw";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@kw", "%" + keyword + "%")
            };

            if (loai != "Tất cả")
            {
                sql += " AND Loai = @loai";
                parameters.Add(new SqlParameter("@loai", loai));
            }

            DataTable dt = DataAccess.GetDataTable(sql, parameters.ToArray());
            dgvMon.DataSource = dt;

            // Highlight món đã chọn
            foreach (DataGridViewRow row in dgvMon.Rows)
            {
                int monId = Convert.ToInt32(row.Cells["Id"].Value);
                row.Selected = danhSachMonDaChon.Contains(monId);
                row.DefaultCellStyle.BackColor = danhSachMonDaChon.Contains(monId) ? Color.LightBlue : Color.White;
            }

            UpdateButtonStatus();
        }

        // ==========================
        // TextBox tìm kiếm
        // ==========================
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string loai = cmbLoaiMon.SelectedValue?.ToString() ?? "Tất cả";
            LoadMon(txtTimKiem.Text.Trim(), loai);
        }

        private void btnTimMon_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa để tìm món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string loai = cmbLoaiMon.SelectedValue?.ToString() ?? "Tất cả";
            LoadMon(keyword, loai);
        }

        // ==========================
        // ComboBox chọn loại món
        // ==========================
        private void cmbLoaiMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLoaiMon.SelectedValue == null) return;
            string loai = cmbLoaiMon.SelectedValue.ToString();
            LoadMon(txtTimKiem.Text.Trim(), loai);
        }

        // ==========================
        // Toggle chọn món
        // ==========================
        private void ToggleSelection(DataGridViewRow row)
        {
            int monId = Convert.ToInt32(row.Cells["Id"].Value);
            if (danhSachMonDaChon.Contains(monId))
            {
                danhSachMonDaChon.Remove(monId);
                row.Selected = false;
                row.DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                danhSachMonDaChon.Add(monId);
                row.Selected = true;
                row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }

        private void dgvMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ToggleSelection(dgvMon.Rows[e.RowIndex]);
                UpdateButtonStatus();
            }
        }

        // ==========================
        // Cập nhật trạng thái nút thêm món
        // ==========================
        private void UpdateButtonStatus()
        {
            // Bật nút nếu đã chọn món và đã chọn hóa đơn
            btnThemMonVaoDon.Enabled = danhSachMonDaChon.Count > 0 && hoaDonDangChonId.HasValue;
        }

        // ==========================
        // Thêm món vào hóa đơn
        // ==========================
        private void btnThemMonVaoDon_Click(object sender, EventArgs e)
        {
            if (hoaDonDangChonId == null)
            {
                MessageBox.Show("Vui lòng chọn 1 hóa đơn chưa thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (danhSachMonDaChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string sqlInsert = "INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@hd, @m, 1)";
                        string sqlUpdate = @"
                    UPDATE HoaDon 
                    SET TongTien = ISNULL((SELECT SUM(ct.SoLuong * m.Gia) 
                                           FROM ChiTietHoaDon ct 
                                           JOIN Mon m ON ct.MonId = m.Id 
                                           WHERE ct.HoaDonId = @hd), 0),
                        SoLuongMon = ISNULL((SELECT SUM(SoLuong) 
                                             FROM ChiTietHoaDon 
                                             WHERE HoaDonId = @hd), 0)
                    WHERE Id = @hd";

                        foreach (int monId in danhSachMonDaChon)
                        {
                            using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@hd", hoaDonDangChonId.Value);
                                cmd.Parameters.AddWithValue("@m", monId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction))
                        {
                            cmdUpdate.Parameters.AddWithValue("@hd", hoaDonDangChonId.Value);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }

                MessageBox.Show($"Đã thêm {danhSachMonDaChon.Count} món vào hóa đơn {hoaDonDangChonId}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh hóa đơn chưa thanh toán
                LoadHoaDonChuaThanhToan();

                // Xóa danh sách món chọn
                danhSachMonDaChon.Clear();
                LoadMon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvHoaDonChuaThanhToan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                hoaDonDangChonId = Convert.ToInt32(dgvHoaDonChuaThanhToan.Rows[e.RowIndex].Cells["Id"].Value);
                UpdateButtonStatus(); // Enable nút thêm món nếu đã chọn hóa đơn
            }
        }

    }
}
