// File: frmThemLoai.cs
// Namespace: QuanLiQuanCafe
// Mục đích: Form Windows để quản lý thêm/xóa loại món.
// Form này hiển thị danh sách loại kèm số lượng món và hỗ trợ CRUD loại.

using QuanLiQuanCafe.Helpers;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmThemLoai : Form
    {
        /// <summary>
        /// Đối tượng BUS cho loại món.
        /// </summary>
        private LoaiMonBUS loaiBUS;

        /// <summary>
        /// Chuỗi kết nối cơ sở dữ liệu.
        /// </summary>
        private string connStr = @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";

        private frmThemMon _parentForm;
        /// <summary>
        /// Constructor cho form.
        /// </summary>
        public frmThemLoai(frmThemMon parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        /// <summary>
        /// Xử lý load form: Khởi tạo BUS, setup grid, tải dữ liệu.
        /// </summary>
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
            var colSTT = new DataGridViewTextBoxColumn
            {
                HeaderText = "STT",
                Name = "STT",
                DataPropertyName = "STT",
                Width = 50
            };
            dgvLoai.Columns.Add(colSTT);
            var colLoai = new DataGridViewTextBoxColumn
            {
                HeaderText = "Tên loại món",
                Name = "Loai",
                DataPropertyName = "Loai",
                Width = 200
            };
            dgvLoai.Columns.Add(colLoai);
            var colSoLuong = new DataGridViewTextBoxColumn
            {
                HeaderText = "Số lượng món",
                Name = "SoLuongMon",
                DataPropertyName = "SoLuongMon",
                Width = 100
            };
            dgvLoai.Columns.Add(colSoLuong);
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
                MessageBox.Show("❌ Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý thêm loại mới.
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            string loai = txtLoai.Text.Trim();
            if (string.IsNullOrEmpty(loai))
            {
                MessageBox.Show("Vui lòng nhập tên loại!");
                return;
            }
            try
            {
                int kq = loaiBUS.ThemLoai(loai);
                if (kq > 0)
                {
                    MessageBox.Show("✅ Thêm loại thành công!");
                    txtLoai.Clear();
                    LoadLoai();
                }
                else
                {
                    MessageBox.Show("❌ Thêm loại thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý xóa loại.
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLoai.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn loại cần xóa!");
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
                    MessageBox.Show($"✅ Loại '{loai}' đã được xóa!");
                    LoadLoai();
                }
                else
                {
                    MessageBox.Show("❌ Xóa thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message);
            }
        }

        // Quay lại và refresh frmThemMon
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            _parentForm.LoadLoaiMon(); // Load lại danh sách loại
            _parentForm.Show();
            this.Close();
        }

    }
}