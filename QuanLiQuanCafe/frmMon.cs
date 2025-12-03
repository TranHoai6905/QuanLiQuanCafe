// File: frmMon.cs
// Namespace: QuanLiQuanCafe
// Mục đích: Form Windows để quản lý việc chọn và thêm món vào hóa đơn.
// Form này hỗ trợ chọn món, thêm vào hóa đơn chưa thanh toán, tạo/xóa hóa đơn mới.

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLiQuanCafe.BUS;

namespace QuanLiQuanCafe
{
    public partial class frmMon : Form
    {
        /// <summary>
        /// ID hóa đơn hiện tại (nếu có).
        /// </summary>
        private int hoaDonId;

        /// <summary>
        /// ID hóa đơn đang chọn để thêm món.
        /// </summary>
        private int? hoaDonDangChonId = null;

        /// <summary>
        /// Loại món đang chọn.
        /// </summary>
        private string loaiMonDangChon = "Tất cả";

        /// <summary>
        /// Danh sách ID món đã chọn.
        /// </summary>
        private List<int> danhSachMonDaChon = new List<int>();

        /// <summary>
        /// Chuỗi kết nối cơ sở dữ liệu.
        /// </summary>
        private readonly string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";

        /// <summary>
        /// Đối tượng BUS cho món.
        /// </summary>
        private MonBUS monBUS;

        /// <summary>
        /// Đối tượng BUS cho hóa đơn.
        /// </summary>
        private HoaDonBUS hoaDonBUS;

        /// <summary>
        /// Đối tượng BUS cho loại món.
        /// </summary>
        private LoaiMonBUS loaiBUS;

        /// <summary>
        /// Sự kiện khi món được thêm vào hóa đơn.
        /// </summary>
        public event Action<int> OnMonDaDuocThem;

        /// <summary>
        /// Constructor khi truyền ID hóa đơn.
        /// </summary>
        /// <param name="hoaDonId">ID hóa đơn.</param>
        public frmMon(int hoaDonId)
        {
            InitializeComponent();
            this.hoaDonId = hoaDonId;
        }

        /// <summary>
        /// Constructor mặc định.
        /// </summary>
        public frmMon()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Xử lý load form: Khởi tạo BUS và tải dữ liệu.
        /// </summary>
        private void frmMon_Load(object sender, EventArgs e)
        {
            monBUS = new MonBUS(connStr);
            hoaDonBUS = new HoaDonBUS();
            loaiBUS = new LoaiMonBUS(connStr); // Khởi tạo LoaiMonBUS
            LoadLoaiMon();
            LoadMon();
            LoadHoaDonChuaThanhToan();
        }

        /// <summary>
        /// Tải danh sách hóa đơn chưa thanh toán.
        /// </summary>
        private void LoadHoaDonChuaThanhToan()
        {
            dgvHoaDonChuaThanhToan.DataSource = hoaDonBUS.GetHoaDonChuaThanhToan();
            dgvHoaDonChuaThanhToan.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvHoaDonChuaThanhToan.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvHoaDonChuaThanhToan.Columns["SoLuongMon"].HeaderText = "Số lượng món";
            dgvHoaDonChuaThanhToan.Columns["TrangThai"].HeaderText = "Trạng thái";
        }

        /// <summary>
        /// Tải danh sách loại món vào FlowLayoutPanel.
        /// </summary>
        private void LoadLoaiMon()
        {
            flpLoaiMon.Controls.Clear();
            AddLoaiButton("Tất cả");
            DataTable dt = loaiBUS.GetLoai(); // Sử dụng loaiBUS.GetLoai() để lấy từ bảng LoaiMon
            foreach (DataRow row in dt.Rows)
            {
                AddLoaiButton(row["Loai"].ToString());
            }
        }

        /// <summary>
        /// Thêm nút loại món vào FlowLayoutPanel.
        /// </summary>
        /// <param name="tenLoai">Tên loại.</param>
        private void AddLoaiButton(string tenLoai)
        {
            var btn = new Guna.UI2.WinForms.Guna2Button
            {
                Text = tenLoai,
                Tag = tenLoai,
                Height = 40,
                BorderRadius = 8,
                FillColor = ColorTranslator.FromHtml("#D9C2A1"),
                ForeColor = Color.White,
                BorderThickness = 0,
                AutoSize = false,
                Padding = new Padding(20, 5, 20, 5)
            };
            Size textSize = TextRenderer.MeasureText(tenLoai, btn.Font);
            btn.Width = textSize.Width + 60;
            btn.HoverState.FillColor = btn.FillColor;
            btn.Click += BtnLoai_Click;
            flpLoaiMon.Controls.Add(btn);
        }

        /// <summary>
        /// Xử lý click nút loại: Cập nhật loại đang chọn và tải món.
        /// </summary>
        private void BtnLoai_Click(object sender, EventArgs e)
        {
            var clicked = sender as Guna.UI2.WinForms.Guna2Button;
            loaiMonDangChon = clicked.Tag.ToString();
            foreach (Control c in flpLoaiMon.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2Button btn)
                {
                    btn.FillColor = ColorTranslator.FromHtml("#D9C2A1");
                    btn.ForeColor = Color.White;
                    btn.BorderThickness = 0;
                }
            }
            clicked.FillColor = Color.White;
            clicked.ForeColor = Color.Black;
            clicked.BorderColor = Color.Black;
            clicked.BorderThickness = 2;
            LoadMon(txtTimKiem.Text.Trim(), loaiMonDangChon);
        }

        /// <summary>
        /// Tải danh sách món với bộ lọc.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm.</param>
        /// <param name="loai">Loại món.</param>
        private void LoadMon(string keyword = "", string loai = "Tất cả")
        {
            DataTable dt = monBUS.GetMon(keyword, loai);
            dgvMon.DataSource = dt;
            foreach (DataGridViewRow row in dgvMon.Rows)
            {
                int monId = Convert.ToInt32(row.Cells["Id"].Value);
                row.Selected = danhSachMonDaChon.Contains(monId);
                row.DefaultCellStyle.BackColor = danhSachMonDaChon.Contains(monId) ? Color.LightBlue : Color.White;
            }
            UpdateButtonStatus();
        }

        /// <summary>
        /// Xử lý thay đổi text tìm kiếm: Tải lại món.
        /// </summary>
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadMon(txtTimKiem.Text.Trim(), loaiMonDangChon);
        }

        /// <summary>
        /// Xử lý nút tìm món.
        /// </summary>
        private void btnTimMon_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa để tìm món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            LoadMon(keyword, loaiMonDangChon);
        }

        /// <summary>
        /// Chuyển đổi trạng thái chọn món.
        /// </summary>
        /// <param name="row">Hàng món.</param>
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

        /// <summary>
        /// Xử lý click ô món: Chuyển đổi chọn.
        /// </summary>
        private void dgvMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ToggleSelection(dgvMon.Rows[e.RowIndex]);
                UpdateButtonStatus();
            }
        }

        /// <summary>
        /// Cập nhật trạng thái nút thêm món.
        /// </summary>
        private void UpdateButtonStatus()
        {
            btnThemMonVaoDon.Enabled = danhSachMonDaChon.Count > 0 && hoaDonDangChonId.HasValue;
        }

        /// <summary>
        /// Xử lý thêm món vào hóa đơn.
        /// </summary>
        private void btnThemMonVaoDon_Click(object sender, EventArgs e)
        {
            if (hoaDonDangChonId == null || danhSachMonDaChon.Count == 0) return;
            try
            {
                monBUS.ThemMonVaoHoaDon(hoaDonDangChonId.Value, danhSachMonDaChon);
                OnMonDaDuocThem?.Invoke(hoaDonDangChonId.Value);
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

        /// <summary>
        /// Xử lý click ô hóa đơn: Cập nhật ID chọn.
        /// </summary>
        private void dgvHoaDonChuaThanhToan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                hoaDonDangChonId = Convert.ToInt32(dgvHoaDonChuaThanhToan.Rows[e.RowIndex].Cells["Id"].Value);
                UpdateButtonStatus();
            }
        }

        /// <summary>
        /// Xử lý tạo hóa đơn mới.
        /// </summary>
        private void btnThemHoaDonMoi_Click(object sender, EventArgs e)
        {
            try
            {
                int nhanVienId = 1; // Hoặc lấy từ session
                int newId = hoaDonBUS.ThemHoaDonMoi(nhanVienId);
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

        /// <summary>
        /// Xử lý xóa hóa đơn.
        /// </summary>
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
                    hoaDonBUS.XoaHoaDon(hoaDonDangChonId.Value);
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