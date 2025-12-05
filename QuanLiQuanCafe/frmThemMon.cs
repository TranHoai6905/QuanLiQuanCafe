using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLiQuanCafe.BUS;
using QuanLiQuanCafe.Helpers;

namespace QuanLiQuanCafe
{
    public partial class frmThemMon : Form
    {
        private LoaiMonBUS loaiBUS;
        private MonBUS monBUS;

        private string loaiMonDangChon = "Tất cả";
        private string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";
        private string duongDanAnh = "";
        private MonCard monDangChon = null;

        public frmThemMon()
        {
            InitializeComponent();
            monBUS = new MonBUS(connStr);
            loaiBUS = new LoaiMonBUS(connStr);
        }

        private void frmThemMon_Load(object sender, EventArgs e)
        {
            LoadLoaiMon();
            LoadMon();
        }

        #region --- Loại món ---
        public void LoadLoaiMon()
        {
            flpLoaiMon.Controls.Clear();
            AddLoaiButton("Tất cả");

            DataTable dt = loaiBUS.GetLoai();
            foreach (DataRow row in dt.Rows)
                AddLoaiButton(row["Loai"].ToString());

            cmbLoai.Items.Clear();
            foreach (DataRow row in dt.Rows)
                cmbLoai.Items.Add(row["Loai"].ToString());

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
                if (c is Guna.UI2.WinForms.Guna2Button b)
                {
                    b.FillColor = ColorTranslator.FromHtml("#D9C2A1");
                    b.ForeColor = Color.White;
                    b.BorderThickness = 0;
                }

            clicked.FillColor = Color.White;
            clicked.ForeColor = Color.Black;
            clicked.BorderColor = Color.Black;
            clicked.BorderThickness = 2;

            LoadMon();
        }
        #endregion

        #region --- Món ---
        public void LoadMon(string keyword = "")
        {
            flpMon.Controls.Clear();
            DataTable dt = monBUS.GetMon(keyword, loaiMonDangChon);
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["Id"]);
                string ten = row["TenMon"].ToString();
                decimal gia = Convert.ToDecimal(row["Gia"]);
                string loai = row["Loai"].ToString();

                MonCard card = new MonCard(id, ten, gia, loai);
                card.OnSelect += MonCard_OnSelect;
                flpMon.Controls.Add(card);
            }

            // Reset món đang chọn
            monDangChon = null;
            ClearInputs();
        }

        private void MonCard_OnSelect(MonCard card)
        {
            // Bỏ chọn card cũ
            if (monDangChon != null)
                monDangChon.SetSelected(false);

            // Chọn card mới
            monDangChon = card;
            card.SetSelected(true);

            // Điền thông tin vào form
            txtTenMon.Text = card.TenMon;
            txtGia.Text = card.Gia.ToString();
            cmbLoai.Text = card.Loai;
            btnSuaMon.Enabled = true;
            btnXoaMon.Enabled = true;
        }
        #endregion

        #region --- Thêm / Sửa / Xóa ---
        private void btnThemMonMoi_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out decimal gia)) return;

            string loai = cmbLoai.Text.Trim();

            // Kiểm tra ảnh
            if (string.IsNullOrEmpty(duongDanAnh) || !File.Exists(duongDanAnh))
            {
                MessageBox.Show("❌ Vui lòng chọn ảnh món!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int result = monBUS.ThemMon(txtTenMon.Text.Trim(), gia, loai, duongDanAnh, connStr);

            if (result > 0)
            {
                LoadLoaiMon();
                loaiMonDangChon = loai;
                LoadMon();
                ClearInputs();
                duongDanAnh = "";
                MessageBox.Show("✅ Thêm món thành công!");
            }
            else MessageBox.Show("❌ Lỗi khi thêm món!");
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (monDangChon == null || !ValidateInputs(out decimal gia)) return;

            int result = monBUS.SuaMon(monDangChon.Id, txtTenMon.Text.Trim(), gia, cmbLoai.Text.Trim(), connStr);

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
            if (monDangChon == null) return;

            int result = monBUS.XoaMon(monDangChon.Id);
            if (result > 0)
            {
                LoadLoaiMon();
                LoadMon();
                ClearInputs();
                MessageBox.Show("✅ Xóa món thành công!");
            }
            else MessageBox.Show("❌ Không xóa được món!");
        }
        #endregion

        #region --- Xử lý ảnh ---
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Ảnh (*.jpg;*.png)|*.jpg;*.png";
                    ofd.Title = "Chọn ảnh món";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        duongDanAnh = ofd.FileName;

                        if (!File.Exists(duongDanAnh))
                        {
                            MessageBox.Show("❌ File ảnh không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        using (var img = Image.FromFile(duongDanAnh))
                        {
                            pbAnhMon.Image = new Bitmap(img);
                        }

                        pbAnhMon.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region --- Các hàm tiện ích ---
        private void txtTimKiem_TextChanged(object sender, EventArgs e) => LoadMon(txtTimKiem.Text.Trim());
        private void btnTimMon_Click(object sender, EventArgs e) => LoadMon(txtTimKiem.Text.Trim());

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
        #endregion

        #region --- Thêm / Xóa loại ---
        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            frmThemLoai frm = new frmThemLoai(this);
            frm.Show();
            this.Hide();
        }

        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            string loaiCanXoa = cmbLoai.Text.Trim();
            if (string.IsNullOrEmpty(loaiCanXoa) || loaiCanXoa == "Tất cả")
            {
                MessageBox.Show("❌ Không thể xóa loại 'Tất cả' hoặc loại trống!");
                return;
            }

            int soMon = monBUS.DemMonTheoLoai(loaiCanXoa);
            if (soMon > 0)
            {
                MessageBox.Show($"❌ Loại '{loaiCanXoa}' đang có {soMon} món.\nBạn phải xóa từng món trước khi xóa loại!",
                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa loại '{loaiCanXoa}' không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

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
        #endregion
    }
}
