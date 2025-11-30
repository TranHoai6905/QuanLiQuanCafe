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
                                              // Thông báo món vừa được thêm vào hóa đơn
        public event Action<int> OnMonDaDuocThem; // int = Id của hóa đơn vừa thêm món

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
            dgvHoaDonChuaThanhToan.DataSource = MonQueries.GetHoaDonChuaThanhToan();

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
            DataTable dt = MonQueries.GetLoaiMon();

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
            DataTable dt = MonQueries.GetMon(keyword, loai);
            dgvMon.DataSource = dt;

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
            if (hoaDonDangChonId == null || danhSachMonDaChon.Count == 0)
                return;

            try
            {
                MonQueries.ThemMonVaoHoaDon(hoaDonDangChonId.Value, danhSachMonDaChon);

                // Thông báo frmHoaDon reload dữ liệu ngay
                OnMonDaDuocThem?.Invoke(hoaDonDangChonId.Value);

                // Xóa danh sách món đã chọn, load lại DataGridView món
                danhSachMonDaChon.Clear();
                LoadMon();
                LoadHoaDonChuaThanhToan();

                MessageBox.Show("Đã thêm món vào hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // ===========================
        // Thêm hóa đơn mới vào dgvHoaDonChuaThanhToan
        // ===========================
        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            try
            {
                int nhanVienId = 1; // Lấy từ session/login nếu có
                int newId = MonQueries.ThemHoaDonMoi(nhanVienId);
                MessageBox.Show($"Đã tạo hóa đơn mới #{newId}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadHoaDonChuaThanhToan();
                foreach (DataGridViewRow row in dgvHoaDonChuaThanhToan.Rows)
                {
                    if (Convert.ToInt32(row.Cells["Id"].Value) == newId)
                    {
                        row.Selected = true;
                        hoaDonDangChonId = newId;
                        UpdateButtonStatus();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hóa đơn mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===========================
        // Xóa hóa đơn đã chọn từ dgvHoaDonChuaThanhToan
        // ===========================
        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (hoaDonDangChonId == null)
            {
                MessageBox.Show("Vui lòng chọn 1 hóa đơn để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThai = dgvHoaDonChuaThanhToan.SelectedRows[0].Cells["TrangThai"].Value?.ToString().Trim();
            if (trangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không thể xóa hóa đơn đã thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn #{hoaDonDangChonId}?\nTất cả món sẽ bị xóa!",
                                          "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    MonQueries.XoaHoaDon(hoaDonDangChonId.Value);
                    MessageBox.Show($"Đã xóa hóa đơn #{hoaDonDangChonId}!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadHoaDonChuaThanhToan();
                    hoaDonDangChonId = null;
                    UpdateButtonStatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
