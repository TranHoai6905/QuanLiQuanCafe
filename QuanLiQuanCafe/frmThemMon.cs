// File: frmThemMon.cs
// Namespace: QuanLiQuanCafe
// Mục đích của file:
//   - Form quản lý Món trong quán cafe.
//   - Hỗ trợ: Thêm – Sửa – Xóa món, Quản lý Loại món.
//   - Tổ chức theo mô hình BUS/DAL, không xử lý SQL trực tiếp.
//   - Dùng Guna UI để tối ưu giao diện.
// Phương pháp tổ chức form:
//   ✔ Tải loại món → Load button filter → Load món theo loại
//   ✔ Chọn loại → tô màu button → lọc món
//   ✔ Khi click món → đổ vào input → Unlock nút Sửa + Xóa
//   ✔ Khi thao tác CRUD → Reload toàn bộ loại + món để đồng bộ
//   ✔ Xóa loại: chỉ cho xóa nếu không có món (phiên bản refactor mới)
//   ✔ Mở form thêm loại: hide form hiện tại nhưng giữ ngữ cảnh cha

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLiQuanCafe.BUS;
using QuanLiQuanCafe.Helpers;

namespace QuanLiQuanCafe
{
    public partial class frmThemMon : Form
    {
        // Hai lớp BUS xử lý logic dữ liệu theo mô hình 3 tầng
        private LoaiMonBUS loaiBUS;
        private MonBUS monBUS;

        // Lưu loại món đang được chọn để filter món
        private string loaiMonDangChon = "Tất cả";

        // Chuỗi kết nối dùng chung—Form không xử lý Database trực tiếp
        string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";

        public frmThemMon()
        {
            InitializeComponent();

            // Khởi tạo BUS với connection string—đúng mô hình phân lớp
            monBUS = new MonBUS(connStr);
            loaiBUS = new LoaiMonBUS(connStr);
        }

        // ============================
        // ► LOAD FORM
        // Phương pháp:
        //   - Load loại trước (button filter + combobox)
        //   - Load món theo loại mặc định
        //   - Setup hiệu ứng UI
        // ============================
        private void frmThemMon_Load(object sender, EventArgs e)
        {
            LoadLoaiMon();
            LoadMon();
            ButtonHelper.EnableShadow(this);
            DataGridViewHelper.SetHeaderColor(dgvMon);
        }

        // ============================
        // ► LOAD LOẠI MÓN
        // Phương pháp:
        //   - Xóa các button cũ trong flowlayout
        //   - Thêm nút "Tất cả"
        //   - Thêm từng loại từ DB
        //   - Sync với ComboBox loại
        // ============================
        public void LoadLoaiMon()
        {
            // Xóa button cũ để tránh trùng lặp
            for (int i = flpLoaiMon.Controls.Count - 1; i >= 0; i--)
                if (flpLoaiMon.Controls[i] is Guna.UI2.WinForms.Guna2Button)
                    flpLoaiMon.Controls.RemoveAt(i);

            // Nút "All"
            AddLoaiButton("Tất cả");

            // Tải danh sách loại từ DB
            DataTable dt = loaiBUS.GetLoai();
            foreach (DataRow row in dt.Rows)
                AddLoaiButton(row["Loai"].ToString());

            // Load vào combobox
            cmbLoai.Items.Clear();
            foreach (DataRow row in dt.Rows)
                cmbLoai.Items.Add(row["Loai"].ToString());

            if (cmbLoai.Items.Count > 0)
                cmbLoai.SelectedIndex = 0;
        }

        // ============================
        // ► TẠO BUTTON LOẠI MÓN
        // Phương pháp:
        //   - Button tự co theo text
        //   - Giữ style UI Guna2 đồng bộ
        //   - Gắn sự kiện Click chung cho filter
        // ============================
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

            // Không đổi màu khi hover
            btn.HoverState.FillColor = btn.FillColor;

            btn.Click += BtnLoai_Click;

            flpLoaiMon.Controls.Add(btn);
        }

        // ============================
        // ► CHỌN LOẠI → TÔ MÀU → LOAD MÓN
        // Phương pháp:
        //   - Reset toàn bộ button về màu mặc định
        //   - Tô màu nút được chọn
        //   - Cập nhật biến loaiMonDangChon
        //   - Reload món theo loại
        // ============================
        private void BtnLoai_Click(object sender, EventArgs e)
        {
            var clicked = sender as Guna.UI2.WinForms.Guna2Button;
            loaiMonDangChon = clicked.Tag.ToString();

            // Reset màu của mọi btn loại
            foreach (Control c in flpLoaiMon.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2Button btn)
                {
                    btn.FillColor = ColorTranslator.FromHtml("#D9C2A1");
                    btn.ForeColor = Color.White;
                    btn.BorderThickness = 0;
                }
            }

            // Tô màu nút được chọn
            clicked.FillColor = Color.White;
            clicked.ForeColor = Color.Black;
            clicked.BorderColor = Color.Black;
            clicked.BorderThickness = 2;

            LoadMon();
        }

        // ============================
        // ► LOAD DANH SÁCH MÓN
        // Phương pháp:
        //   - Gọi BUS → trả DataTable
        //   - Set chế độ hiển thị
        //   - Format giá tiền
        // ============================
        private void LoadMon(string keyword = "")
        {
            dgvMon.DataSource = monBUS.GetMon(keyword, loaiMonDangChon);
            dgvMon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMon.AllowUserToAddRows = false;

            if (dgvMon.Columns.Contains("Gia"))
                dgvMon.Columns["Gia"].DefaultCellStyle.Format = "N0";
        }

        // Tìm kiếm realtime
        private void txtTimKiem_TextChanged(object sender, EventArgs e) => LoadMon(txtTimKiem.Text.Trim());

        private void btnTimMon_Click(object sender, EventArgs e) => LoadMon(txtTimKiem.Text.Trim());

        // ============================
        // ► CHỌN MÓN → ĐỔ VÀO INPUT
        // ============================
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

        // ============================
        // ► THÊM MÓN
        // Phương pháp:
        //   - Validate dữ liệu input
        //   - Gọi BUS thêm
        //   - Reload loại + món để sync UI
        // ============================
        private void btnThemMonMoi_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out decimal gia)) return;

            string loai = cmbLoai.Text.Trim();
            int result = monBUS.ThemMon(txtTenMon.Text.Trim(), gia, loai);

            if (result > 0)
            {
                LoadLoaiMon();
                loaiMonDangChon = loai;
                LoadMon();
                ClearInputs();
                MessageBox.Show("✅ Thêm món thành công!");
            }
            else MessageBox.Show("❌ Lỗi khi thêm món!");
        }

        // ============================
        // ► SỬA MÓN
        // ============================
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

        // ============================
        // ► XÓA MÓN
        // ============================
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

        // Reset input
        private void ClearInputs()
        {
            txtTenMon.Clear();
            txtGia.Clear();
            cmbLoai.SelectedIndex = 0;
            btnSuaMon.Enabled = false;
            btnXoaMon.Enabled = false;
        }

        // Kiểm tra tính hợp lệ của input (phương pháp ValidateForm chuẩn)
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

        // ============================
        // ► XÓA LOẠI MÓN (Phiên bản bạn yêu cầu)
        // Phương pháp:
        //   - Không cho xóa nếu loại đang chứa món
        //   - Chỉ cho xóa loại TRỐNG
        // ============================
        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            string loaiCanXoa = cmbLoai.Text.Trim();

            if (string.IsNullOrEmpty(loaiCanXoa) || loaiCanXoa == "Tất cả")
            {
                MessageBox.Show("❌ Không thể xóa loại 'Tất cả' hoặc loại trống!");
                return;
            }

            // Kiểm tra loại có món hay không
            int soMon = monBUS.DemMonTheoLoai(loaiCanXoa);

            if (soMon > 0)
            {
                MessageBox.Show(
                    $"❌ Loại '{loaiCanXoa}' đang có {soMon} món.\n" +
                    "Bạn phải xóa từng món trước khi xóa loại!",
                    "Không thể xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Nếu không có món → cho xóa
            if (MessageBox.Show(
                $"Bạn có chắc muốn xóa loại '{loaiCanXoa}' không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            int xoaResult = loaiBUS.XoaLoai(loaiCanXoa);
            if (xoaResult > 0)
            {
                MessageBox.Show("✅ Đã xóa loại thành công!");
                LoadLoaiMon();
                loaiMonDangChon = "Tất cả";
                LoadMon();
            }
            else MessageBox.Show("❌ Xóa loại thất bại!");
        }

        // ============================
        // ► MỞ FORM THÊM LOẠI
        // Phương pháp: giữ form cha để gọi load lại sau khi thêm loại
        // ============================
        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            frmThemLoai frm = new frmThemLoai(this);
            frm.Show();
            this.Hide();
        }
    }
}
