using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLiQuanCafe.Helpers;
using QuanLiQuanCafe.BUS;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form quản lý thêm/xóa loại món, hiển thị danh sách loại kèm số lượng món.
    /// </summary>
    public partial class frmThemLoai : Form
    {
        private LoaiMonBUS loaiBUS;
        private string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";
        private frmThemMon _parentForm;
        public frmThemLoai(frmThemMon parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }
        private void frmThemLoai_Load(object sender, EventArgs e)
        {
            loaiBUS = new LoaiMonBUS(connStr);
            SetupDGV();
            LoadLoai();
            ButtonHelper.EnableShadow(this);
            DataGridViewHelper.SetHeaderColor(dgvLoai);
        }
        /// <summary>
        /// Thiết lập cột cho DataGridView.
        /// </summary>
        private void SetupDGV()
        {
            dgvLoai.Columns.Clear();
            dgvLoai.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "STT", Name = "STT", DataPropertyName = "STT", Width = 50 });
            dgvLoai.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên loại món", Name = "Loai", DataPropertyName = "Loai", Width = 200 });
            dgvLoai.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số lượng món", Name = "SoLuongMon", DataPropertyName = "SoLuongMon", Width = 100 });
            dgvLoai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoai.AllowUserToAddRows = false;
            dgvLoai.ReadOnly = true;
            dgvLoai.AutoGenerateColumns = false;
        }
        /// <summary>
        /// Tải danh sách loại kèm số lượng món.
        /// </summary>
        private void LoadLoai()
        {
            try
            {
                DataTable dt = loaiBUS.GetLoaiWithCount();
                dgvLoai.DataSource = dt;
                dgvLoai.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi load dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string loai = txtLoai.Text.Trim();
            if (string.IsNullOrEmpty(loai))
            {
                MessageBox.Show("Vui lòng nhập tên loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int kq = loaiBUS.ThemLoai(loai);
                if (kq > 0)
                {
                    MessageBox.Show("✅ Thêm loại thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLoai.Clear();
                    LoadLoai();
                }
                else
                {
                    MessageBox.Show("❌ Thêm loại thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLoai.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string loai = dgvLoai.SelectedRows[0].Cells["Loai"].Value.ToString();
            int soLuongMon = loaiBUS.SoLuongMon(loai);
            if (soLuongMon > 0)
            {
                MessageBox.Show(
                    $"❌ Không thể xóa loại '{loai}' vì đang có {soLuongMon} món.\n" +
                    "Vui lòng xóa các món thuộc loại này trước khi xóa loại.",
                    "Lỗi xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa loại '{loai}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes) return;
            try
            {
                int kq = loaiBUS.XoaLoai(loai);
                if (kq > 0)
                {
                    MessageBox.Show($"✅ Loại '{loai}' đã được xóa!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLoai();
                }
                else
                {
                    MessageBox.Show("❌ Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            _parentForm.LoadLoaiMon();
            _parentForm.Show();
            this.Close();
        }
    }
}