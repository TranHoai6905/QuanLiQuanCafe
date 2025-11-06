using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmMon : Form
    {
        private DataTable dtMon;
        private bool isAdmin;
        private bool isFromHoaDon = false; // Quyền đặc biệt từ frmHoaDon

        public frmMon(bool admin = false, bool fromHoaDon = false)
        {
            InitializeComponent();
            isAdmin = admin;
            isFromHoaDon = fromHoaDon;
            LoadData();
            ApplyPermissions();
        }

        private void LoadData()
        {
            // Tạo dữ liệu mẫu
            dtMon = new DataTable();
            dtMon.Columns.Add("Tên món");
            dtMon.Columns.Add("Giá");
            dtMon.Columns.Add("Loại");

            dtMon.Rows.Add("Cà phê sữa", 15000, "Thức uống");
            dtMon.Rows.Add("Bánh mì", 12000, "Ăn sáng");
            dtMon.Rows.Add("Trà đá", 5000, "Thức uống");

            dgvMon.DataSource = dtMon;

            // Load các loại món vào combobox
            cmbLoai.Items.Clear();
            foreach (var loai in dtMon.AsEnumerable().Select(r => r["Loại"].ToString()).Distinct())
            {
                cmbLoai.Items.Add(loai);
            }
        }

        private void ApplyPermissions()
        {
            btnThemMonMoi.Visible = isAdmin || isFromHoaDon;
            btnSuaMon.Visible = isAdmin;
            btnXoaMon.Visible = isAdmin;

            txtTenMon.ReadOnly = !(isAdmin || isFromHoaDon);
            txtGia.ReadOnly = !(isAdmin || isFromHoaDon);
            cmbLoai.Enabled = isAdmin || isFromHoaDon;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.ToLower();
            dgvMon.DataSource = dtMon.AsEnumerable()
                .Where(r => r["Tên món"].ToString().ToLower().Contains(keyword) || r["Loại"].ToString().ToLower().Contains(keyword))
                .CopyToDataTable();
        }

        private void dgvMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isAdmin && dgvMon.CurrentRow != null)
            {
                txtTenMon.Text = dgvMon.CurrentRow.Cells["Tên món"].Value.ToString();
                txtGia.Text = dgvMon.CurrentRow.Cells["Giá"].Value.ToString();
                cmbLoai.SelectedItem = dgvMon.CurrentRow.Cells["Loại"].Value.ToString();
            }
        }

        private void btnThemMonMoi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenMon.Text) || string.IsNullOrWhiteSpace(txtGia.Text) || cmbLoai.SelectedItem == null)
            {
                MessageBox.Show("Nhập đầy đủ thông tin!");
                return;
            }

            dtMon.Rows.Add(txtTenMon.Text, txtGia.Text, cmbLoai.SelectedItem.ToString());
            if (!cmbLoai.Items.Contains(cmbLoai.SelectedItem))
                cmbLoai.Items.Add(cmbLoai.SelectedItem);
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (dgvMon.CurrentRow != null)
            {
                dgvMon.CurrentRow.Cells["Tên món"].Value = txtTenMon.Text;
                dgvMon.CurrentRow.Cells["Giá"].Value = txtGia.Text;
                dgvMon.CurrentRow.Cells["Loại"].Value = cmbLoai.SelectedItem.ToString();
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvMon.CurrentRow != null)
            {
                dgvMon.Rows.Remove(dgvMon.CurrentRow);
            }
        }
    }
}
