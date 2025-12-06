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

        private void LoadDefaultImageToPictureBox()
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string defaultPath = Path.Combine(folder, "Default.png");

                if (!File.Exists(defaultPath))
                    Resource1.DefaultImage.Save(defaultPath, System.Drawing.Imaging.ImageFormat.Png);

                pbAnhMon.Image = Image.FromFile(defaultPath);
                pbAnhMon.SizeMode = PictureBoxSizeMode.Zoom;

                duongDanAnh = "Images\\Default.png";
            }
            catch
            {
                // Không hiển thị lỗi debug
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
                if (!ValidateInputs(out decimal gia))
                    return;

                string loai = cmbLoai.Text.Trim();
                string anhLuu = string.IsNullOrEmpty(duongDanAnh) ? "Images\\Default.png" : duongDanAnh;

                monBUS.ThemMon(txtTenMon.Text.Trim(), gia, loai, anhLuu);

                LoadLoaiMon();
                loaiMonDangChon = loai;
                LoadMon();
                ClearInputs();
                duongDanAnh = "";

                MessageBox.Show("Thêm món thành công!", "Thành công");
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm món!", "Lỗi");
            }
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (selectedCard == null || !ValidateInputs(out decimal gia)) return;

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
                MessageBox.Show("Sửa món thành công!");
            }
            else
            {
                MessageBox.Show("Lỗi khi sửa món!");
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (selectedCard == null) return;

            if (monBUS.XoaMon(selectedCard.Id) > 0)
            {
                LoadLoaiMon();
                LoadMon();
                ClearInputs();
                LoadDefaultImageToPictureBox();

                MessageBox.Show("Xóa món thành công!");
            }
            else
            {
                MessageBox.Show("Không xóa được món!");
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
