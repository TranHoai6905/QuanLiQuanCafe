using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLiQuanCafe.BUS;
namespace QuanLiQuanCafe
{
    public partial class frmThemMon : Form
    {
        private MonBUS monBUS;
        private string loaiMonDangChon = "Tất cả";
        string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";
        public frmThemMon()
        {
            InitializeComponent();
            monBUS = new MonBUS(connStr); // khởi tạo BUS
        }
        private void frmThemMon_Load(object sender, EventArgs e)
        {
            LoadLoaiMon();
            LoadMon();
        }

        private void LoadLoaiMon()
        {
            // Chỉ xóa các nút Guna2Button, không xóa cmbLoai
            for (int i = flpLoaiMon.Controls.Count - 1; i >= 0; i--)
            {
                if (flpLoaiMon.Controls[i] is Guna.UI2.WinForms.Guna2Button)
                    flpLoaiMon.Controls.RemoveAt(i);
            }

            AddLoaiButton("Tất cả");

            DataTable dt = monBUS.GetLoaiMon();
            foreach (DataRow row in dt.Rows)
            {
                AddLoaiButton(row["Loai"].ToString());
            }

            // Đồng thời load lại ComboBox
            cmbLoai.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                cmbLoai.Items.Add(row["Loai"].ToString());
            }
            if (cmbLoai.Items.Count > 0)
                cmbLoai.SelectedIndex = 0;
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
                Padding = new Padding(20, 5, 20, 5) // tăng padding 2 bên
            };

            // --- ĐO CHỮ CHUẨN CHO GUNA2BUTTON ---
            Size textSize = TextRenderer.MeasureText(tenLoai, btn.Font);

            // Tăng thêm khoảng trống để chữ không bị ép
            btn.Width = textSize.Width + 60; // 60px = padding + biên + khoảng trống đẹp

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

            LoadMon();
        }

        private void LoadMon(string keyword = "")
        {
            dgvMon.DataSource = monBUS.GetMon(keyword, loaiMonDangChon);
            dgvMon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMon.AllowUserToAddRows = false;

            if (dgvMon.Columns.Contains("Gia"))
                dgvMon.Columns["Gia"].DefaultCellStyle.Format = "N0";
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e) => LoadMon(txtTimKiem.Text.Trim());
        private void btnTimMon_Click(object sender, EventArgs e) => LoadMon(txtTimKiem.Text.Trim());

        private void dgvMon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMon.SelectedRows.Count == 0) return;
            var row = dgvMon.SelectedRows[0];

            txtTenMon.Text = row.Cells["TenMon"].Value.ToString();
            txtGia.Text = row.Cells["Gia"].Value.ToString();
            cmbLoai.Text = row.Cells["Loai"].Value.ToString();

            btnSuaMon.Enabled = true;
            btnXoaMon.Enabled = true;
        }

        private void btnThemMonMoi_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out decimal gia)) return;

            string loai = cmbLoai.Text.Trim();
            int result = monBUS.ThemMon(txtTenMon.Text.Trim(), gia, loai);

            if (result > 0)
            {
                // Load lại danh sách loại để tạo thêm nút mới
                LoadLoaiMon();

                // Giữ loại vừa thêm là đã chọn
                loaiMonDangChon = loai;

                LoadMon();
                ClearInputs();

                MessageBox.Show("✅ Thêm món thành công!");
            }

            else MessageBox.Show("❌ Lỗi khi thêm món!");
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (dgvMon.SelectedRows.Count == 0 || !ValidateInputs(out decimal gia)) return;

            int id = Convert.ToInt32(dgvMon.SelectedRows[0].Cells["Id"].Value);
            int result = monBUS.SuaMon(id, txtTenMon.Text.Trim(), gia, cmbLoai.Text.Trim());

            if (result > 0)
            {
                LoadLoaiMon();
                LoadMon();
                ClearInputs();
                MessageBox.Show("✅ Sửa món thành công!");
            }
            else MessageBox.Show("❌ Lỗi khi sửa món!");
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvMon.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvMon.SelectedRows[0].Cells["Id"].Value);

            int result = monBUS.XoaMon(id);
            if (result > 0)
            {
                LoadLoaiMon();
                LoadMon();
                ClearInputs();
                MessageBox.Show("✅ Xóa món thành công!");
            }
            else MessageBox.Show("❌ Không xóa được món!");
        }

        private void ClearInputs()
        {
            txtTenMon.Clear();
            txtGia.Clear();
            cmbLoai.SelectedIndex = 0;
            btnSuaMon.Enabled = false;
            btnXoaMon.Enabled = false;
        }

        private bool ValidateInputs(out decimal gia)
        {
            gia = 0;
            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món!");
                return false;
            }

            if (!decimal.TryParse(txtGia.Text, out gia) || gia <= 0)
            {
                MessageBox.Show("Giá phải là số dương!");
                return false;
            }

            return true;
        }

        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            string loaiCanXoa = cmbLoai.Text.Trim();

            if (string.IsNullOrEmpty(loaiCanXoa) || loaiCanXoa == "Tất cả")
            {
                MessageBox.Show("❌ Không thể xóa loại 'Tất cả' hoặc loại trống!");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa loại '{loaiCanXoa}' không?\n" +
                "Các món thuộc loại này sẽ được chuyển sang 'Lưu trữ món'.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes) return;

            int chuyenResult = monBUS.ChuyenMonSangLuuTru(loaiCanXoa);
            if (chuyenResult < 0)
            {
                MessageBox.Show("❌ Lỗi khi chuyển món sang 'Lưu trữ món'!");
                return;
            }

            int xoaResult = monBUS.XoaLoaiMon(loaiCanXoa);
            if (xoaResult > 0)
            {
                MessageBox.Show($"✅ Loại '{loaiCanXoa}' đã được xóa và món thuộc loại này chuyển sang 'Lưu trữ món'.");
                LoadLoaiMon();

                cmbLoai.SelectedItem = "Tất cả";
                loaiMonDangChon = "Tất cả";
                LoadMon();
            }
            else
            {
                MessageBox.Show("❌ Xóa loại món thất bại!");
            }
        }
    }
}
