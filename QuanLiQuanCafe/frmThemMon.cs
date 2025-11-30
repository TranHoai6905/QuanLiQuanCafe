using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmThemMon : Form
    {
        public frmThemMon() => InitializeComponent();

        private void frmThemMon_Load(object sender, EventArgs e)
        {
            LoadLoaiMon();
            LoadMon();
        }

        private void LoadLoaiMon()
        {
            DataTable dt = MonQueries.GetLoaiMon();
            cmbLoaiMon.Items.Clear();
            cmbLoaiMon.Items.Add("Tất cả");

            foreach (DataRow row in dt.Rows)
                cmbLoaiMon.Items.Add(row["Loai"].ToString());

            cmbLoaiMon.SelectedIndex = 0;
        }

        private void LoadMon()
        {
            dgvMon.DataSource = MonQueries.GetMon(txtTimKiem.Text.Trim(), cmbLoaiMon.Text.Trim());
            dgvMon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMon.AllowUserToAddRows = false;

            if (dgvMon.Columns.Contains("Gia"))
                dgvMon.Columns["Gia"].DefaultCellStyle.Format = "N0";
        }

        private void LocMon()
        {
            dgvMon.DataSource = MonQueries.GetMon(txtTimKiem.Text.Trim(), cmbLoaiMon.Text.Trim());
        }

        private void btnTimMon_Click(object sender, EventArgs e) => LocMon();
        private void txtTimKiem_TextChanged(object sender, EventArgs e) => LocMon();
        private void cmbLoaiMon_SelectedIndexChanged(object sender, EventArgs e) => LocMon();

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
            int result = MonQueries.ThemMon(txtTenMon.Text.Trim(), gia, loai);

            if (result > 0)
            {
                if (!cmbLoai.Items.Contains(loai)) cmbLoai.Items.Add(loai);
                LoadLoaiMon();
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
            int result = MonQueries.SuaMon(id, txtTenMon.Text.Trim(), gia, cmbLoai.Text.Trim());

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
            int result = MonQueries.XoaMon(id);

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
    }
}
