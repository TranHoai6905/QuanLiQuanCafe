// File: frmThemMon.cs
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLiQuanCafe.BUS;
using QuanLiQuanCafe.Helpers;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form thêm/sửa/xóa món.
    /// </summary>
    public partial class frmThemMon : Form
    {
        private LoaiMonBUS loaiBUS;
        private MonBUS monBUS;
        private string loaiMonDangChon = "Tất cả";
        private string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";
        private string duongDanAnh = "";
        private MonCard selectedCard = null;

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
            LoadDefaultImageToPictureBox();
        }

        /// <summary>
        /// Luôn load ảnh mặc định vào PictureBox lúc mở form.
        /// </summary>
        private void LoadDefaultImageToPictureBox()
        {
            try
            {
                MessageBox.Show("👉 Vào LoadDefaultImageToPictureBox()", "DEBUG");

                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                MessageBox.Show("Thư mục Images: " + folder, "DEBUG");

                if (!Directory.Exists(folder))
                {
                    MessageBox.Show("Thư mục Images CHƯA tồn tại -> tạo mới", "DEBUG");
                    Directory.CreateDirectory(folder);
                }

                string defaultPath = Path.Combine(folder, "Default.png");
                MessageBox.Show("Đường dẫn ảnh mặc định: " + defaultPath, "DEBUG");

                if (!File.Exists(defaultPath))
                {
                    MessageBox.Show("Chưa có file Default.png -> tạo từ Resource1.DefaultImage", "DEBUG");
                    Resource1.DefaultImage.Save(defaultPath, System.Drawing.Imaging.ImageFormat.Png);
                }

                pbAnhMon.Image = Image.FromFile(defaultPath);
                pbAnhMon.SizeMode = PictureBoxSizeMode.Zoom;

                duongDanAnh = "Images\\Default.png";

                MessageBox.Show("✅ LoadDefaultImageToPictureBox() hoàn tất", "DEBUG");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "🔥 LỖI trong LoadDefaultImageToPictureBox:\n\n" +
                    ex.Message + "\n\n" +
                    ex.StackTrace,
                    "EXCEPTION",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        #region --- Loại món ---

        public void LoadLoaiMon()
        {
            DataTable dt = loaiBUS.GetLoai();
            LoaiMonHelper.LoadLoaiMon(flpLoaiMon, dt, (loai) =>
            {
                loaiMonDangChon = loai;
                LoadMon();
            });

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

            foreach (DataRow row in dt.Rows)
            {
                string anh = row["Anh"].ToString();
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, anh);
                Image img = File.Exists(fullPath) ? Image.FromFile(fullPath) : Resource1.DefaultImage;

                var card = new MonCard(
                    Convert.ToInt32(row["Id"]),
                    row["TenMon"].ToString(),
                    Convert.ToDecimal(row["Gia"]),
                    row["Loai"].ToString(),
                    img,
                    anh
                );

                card.OnSelect += Card_OnSelect;
                flpMon.Controls.Add(card);
            }
        }

        private void Card_OnSelect(MonCard card)
        {
            if (selectedCard != null)
                selectedCard.SetSelected(false);

            selectedCard = card;
            card.SetSelected(true);

            txtTenMon.Text = card.TenMon;
            txtGia.Text = card.Gia.ToString();
            cmbLoai.Text = card.Loai;

            pbAnhMon.Image = card.MonImage;
            pbAnhMon.SizeMode = PictureBoxSizeMode.Zoom;

            btnSuaMon.Enabled = true;
            btnXoaMon.Enabled = true;
        }

        #endregion

        #region --- Thêm / Sửa / Xóa ---

        private void btnThemMonMoi_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("👉 Bắt đầu xử lý nút Thêm món", "DEBUG");

                // In ra các giá trị nhập
                MessageBox.Show(
                    $"Tên món: {txtTenMon.Text}\nGiá (text): {txtGia.Text}\nLoại: {cmbLoai.Text}",
                    "DEBUG - Giá trị input"
                );

                // Kiểm tra ValidateInputs
                if (!ValidateInputs(out decimal gia))
                {
                    MessageBox.Show("❗ ValidateInputs trả về FALSE. Dừng thêm món.", "DEBUG");
                    return;
                }

                MessageBox.Show($"✅ ValidateInputs OK. Giá parse được: {gia}", "DEBUG");

                string loai = cmbLoai.Text.Trim();

                // Kiểm tra biến đường dẫn ảnh hiện tại
                MessageBox.Show($"duongDanAnh hiện tại: '{duongDanAnh}'", "DEBUG - Ảnh");

                // Nếu không chọn ảnh, dùng ảnh mặc định
                string anhLuu;
                if (string.IsNullOrEmpty(duongDanAnh))
                {
                    anhLuu = "Images\\Default.png";
                    MessageBox.Show("Không có ảnh được chọn -> dùng ảnh mặc định: " + anhLuu, "DEBUG - Ảnh");
                }
                else
                {
                    anhLuu = duongDanAnh;
                    MessageBox.Show("Ảnh người dùng chọn: " + anhLuu, "DEBUG - Ảnh");
                }

                // Kiểm tra file ảnh có tồn tại không
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, anhLuu);
                bool fileTonTai = File.Exists(fullPath);
                MessageBox.Show(
                    $"FullPath ảnh: {fullPath}\nTồn tại file? {fileTonTai}",
                    "DEBUG - File ảnh"
                );

                // Nếu file không tồn tại, báo luôn
                if (!fileTonTai)
                {
                    MessageBox.Show("⚠ Ảnh không tồn tại trên ổ đĩa. Vẫn tiếp tục lưu DB nhưng bạn cần kiểm tra lại phần tạo ảnh mặc định.", "DEBUG");
                }

                // Gọi BUS thêm món
                MessageBox.Show("👉 Chuẩn bị gọi monBUS.ThemMon(...)", "DEBUG");

                int result = monBUS.ThemMon(
                    txtTenMon.Text.Trim(),
                    gia,
                    loai,
                    anhLuu
                );

                MessageBox.Show("Kết quả monBUS.ThemMon trả về: " + result, "DEBUG");

                if (result > 0)
                {
                    LoadLoaiMon();
                    loaiMonDangChon = loai;
                    LoadMon();
                    ClearInputs();
                    // Sau khi thêm xong, có thể load lại ảnh mặc định
                    duongDanAnh = "";
                    MessageBox.Show("✅ Thêm món thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("❌ monBUS.ThemMon trả về <= 0. Không thêm được món!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Bắt mọi lỗi nghiêm trọng và show ra để xem chi tiết
                MessageBox.Show(
                    "🔥 LỖI NGHIÊM TRỌNG trong btnThemMonMoi_Click:\n\n" +
                    ex.Message + "\n\n" +
                    ex.StackTrace,
                    "EXCEPTION",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (selectedCard == null || !ValidateInputs(out decimal gia)) return;

            // Nếu không chọn ảnh mới → giữ ảnh cũ
            string anhSua = string.IsNullOrEmpty(duongDanAnh)
                ? selectedCard.AnhPath
                : duongDanAnh;

            int result = monBUS.SuaMon(
                selectedCard.Id,
                txtTenMon.Text.Trim(),
                gia,
                cmbLoai.Text.Trim(),
                anhSua
            );

            if (result > 0)
            {
                LoadLoaiMon();
                LoadMon();
                ClearInputs();
                LoadDefaultImageToPictureBox();
                duongDanAnh = "";

                MessageBox.Show("✅ Sửa món thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("❌ Lỗi khi sửa món!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (selectedCard == null) return;

            int result = monBUS.XoaMon(selectedCard.Id);
            if (result > 0)
            {
                LoadLoaiMon();
                LoadMon();
                ClearInputs();
                LoadDefaultImageToPictureBox();

                MessageBox.Show("✅ Xóa món thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("❌ Không xóa được món!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region --- Chọn ảnh ---

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

                    duongDanAnh = "Images\\" + fileName;

                    pbAnhMon.Image = Image.FromFile(dest);
                    pbAnhMon.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        #endregion

        #region --- Tiện ích ---

        private void txtTimKiem_TextChanged(object sender, EventArgs e) => LoadMon(txtTimKiem.Text.Trim());

        private void btnTimMon_Click(object sender, EventArgs e) => LoadMon(txtTimKiem.Text.Trim());

        private void ClearInputs()
        {
            txtTenMon.Clear();
            txtGia.Clear();
            cmbLoai.SelectedIndex = 0;
            btnSuaMon.Enabled = false;
            btnXoaMon.Enabled = false;
            selectedCard = null;
            duongDanAnh = "";
        }

        private bool ValidateInputs(out decimal gia)
        {
            gia = 0;

            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtGia.Text, out gia) || gia <= 0)
            {
                MessageBox.Show("Giá phải là số dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        #endregion

        #region --- Loại ---

        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            frmThemLoai frm = new frmThemLoai(this);
            frm.Show();
            this.Hide();
        }

        #endregion
    }
}
