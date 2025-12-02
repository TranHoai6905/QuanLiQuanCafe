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
        private int hoaDonId;
        private int? hoaDonDangChonId = null;
        private string loaiMonDangChon = "Tất cả";
        private List<int> danhSachMonDaChon = new List<int>();

        private readonly string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";

        // Khai báo BUS
        private MonBUS monBUS;
        private HoaDonBUS hoaDonBUS;

        public event Action<int> OnMonDaDuocThem;

        // Constructor khi truyền hoaDonId
        public frmMon(int hoaDonId)
        {
            InitializeComponent();
            this.hoaDonId = hoaDonId;
        }

        // Constructor mặc định
        public frmMon()
        {
            InitializeComponent();
        }

        private void frmMon_Load(object sender, EventArgs e)
        {
            // Khởi tạo BUS đúng cách
            monBUS = new MonBUS(connStr);

            // Nếu HoaDonBUS không có constructor nhận connStr, dùng mặc định
            hoaDonBUS = new HoaDonBUS();

            // Load dữ liệu
            LoadLoaiMon();
            LoadMon();
            LoadHoaDonChuaThanhToan();
        }

        private void LoadHoaDonChuaThanhToan()
        {
            dgvHoaDonChuaThanhToan.DataSource = hoaDonBUS.GetHoaDonChuaThanhToan();
            dgvHoaDonChuaThanhToan.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvHoaDonChuaThanhToan.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvHoaDonChuaThanhToan.Columns["SoLuongMon"].HeaderText = "Số lượng món";
            dgvHoaDonChuaThanhToan.Columns["TrangThai"].HeaderText = "Trạng thái";
        }

        private void LoadLoaiMon()
        {
            flpLoaiMon.Controls.Clear();

            DataTable dt = monBUS.GetLoaiMon();

            AddLoaiButton("Tất cả");

            foreach (DataRow row in dt.Rows)
            {
                AddLoaiButton(row["Loai"].ToString());
            }
        }

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

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadMon(txtTimKiem.Text.Trim(), loaiMonDangChon);
        }

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

        private void UpdateButtonStatus()
        {
            btnThemMonVaoDon.Enabled = danhSachMonDaChon.Count > 0 && hoaDonDangChonId.HasValue;
        }

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

        private void dgvHoaDonChuaThanhToan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                hoaDonDangChonId = Convert.ToInt32(dgvHoaDonChuaThanhToan.Rows[e.RowIndex].Cells["Id"].Value);
                UpdateButtonStatus();
            }
        }

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
