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
        private string anhHienTai = "";

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
            DataTable dt = loaiBUS.GetLoai();

            // Load vào FlowLayoutPanel bằng Helper
            LoaiMonHelper.LoadLoaiMon(flpLoaiMon, dt, (loai) =>
            {
                loaiMonDangChon = loai;
                LoadMon();
            });

            // Load vào ComboBox
            cmbLoai.Items.Clear();
            foreach (DataRow row in dt.Rows)
                cmbLoai.Items.Add(row["Loai"].ToString());

            if (cmbLoai.Items.Count > 0)
                cmbLoai.SelectedIndex = 0;
        }
        #endregion

        #region --- Món ---
        private void LoadMon(string keyword = "")
        {
            flpMon.Controls.Clear();
            DataTable dt = monBUS.GetMon(keyword, loaiMonDangChon);
            if (!dt.Columns.Contains("Anh"))
            {
                MessageBox.Show("❌ Lỗi: DataTable không có cột 'Anh'.\n" +
                                "Vui lòng kiểm tra lại câu lệnh SELECT trong MonBUS.GetMon()!",
                                "Lỗi cơ sở dữ liệu",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                // Lấy đường dẫn ảnh từ DB hoặc null
                string duongDanAnh = row["Anh"].ToString();
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, duongDanAnh);

                Image img = null;

                if (File.Exists(fullPath))
                    img = Image.FromFile(fullPath);


                var card = new MonCard(
                    Convert.ToInt32(row["Id"]),
                    row["TenMon"].ToString(),
                    Convert.ToDecimal(row["Gia"]),
                    row["Loai"].ToString(),
                    img
                );
                card.OnSelect += Card_OnSelect;
                flpMon.Controls.Add(card);
            }
        }
        private MonCard selectedCard = null;

        private void Card_OnSelect(MonCard card)
        {
            if (selectedCard != null)
                selectedCard.SetSelected(false);

            selectedCard = card;
            card.SetSelected(true);

            txtTenMon.Text = card.TenMon;
            txtGia.Text = card.Gia.ToString();
            cmbLoai.Text = card.Loai;

            btnSuaMon.Enabled = true;
            btnXoaMon.Enabled = true;
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

            int result = monBUS.ThemMon(txtTenMon.Text.Trim(), gia, loai, duongDanAnh);

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

            int result = monBUS.SuaMon(
                monDangChon.Id,
                txtTenMon.Text.Trim(),
                gia,
                cmbLoai.Text.Trim(),
                duongDanAnh   // ảnh mới (nếu có)
            );

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
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string source = ofd.FileName;
                    string fileName = Path.GetFileName(source);

                    string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string dest = Path.Combine(folder, fileName);

                    File.Copy(source, dest, true);

                    // Lưu đường dẫn tương đối vào DB
                    duongDanAnh = Path.Combine("Images", fileName);

                    pbAnhMon.Image = Image.FromFile(dest);
                    pbAnhMon.SizeMode = PictureBoxSizeMode.Zoom;
                }
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