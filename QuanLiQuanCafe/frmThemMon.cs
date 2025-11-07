using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmThemMon : Form
    {
        public frmThemMon()
        {
            InitializeComponent();
        }

        private void frmThemMon_Load(object sender, EventArgs e)
        {
            cmbLoai.Items.AddRange(new[] { "Đồ uống", "Đồ ăn" });
            cmbLoai.SelectedIndex = 0;
            LoadMon();
        }

        private void LoadMon()
        {
            string sql = "SELECT Id, TenMon, Gia, Loai FROM Mon ORDER BY TenMon";
            dgvMon.DataSource = DataAccess.GetDataTable(sql); // dùng GetDataTable
            dgvMon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMon.AllowUserToAddRows = false;
            if (dgvMon.Columns.Contains("Gia"))
                dgvMon.Columns["Gia"].DefaultCellStyle.Format = "N0";
        }

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
            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món!");
                return;
            }

            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá phải là số dương!");
                return;
            }

            int result = DataAccess.ExecuteNonQuery(
                "INSERT INTO Mon (TenMon, Gia, Loai) VALUES (@ten, @gia, @loai)",
                new SqlParameter("@ten", txtTenMon.Text.Trim()),
                new SqlParameter("@gia", gia),
                new SqlParameter("@loai", cmbLoai.SelectedItem.ToString())
            );

            if (result > 0)
            {
                LoadMon();
                ClearInputs();
                MessageBox.Show("✅ Thêm món thành công!");
            }
            else
            {
                MessageBox.Show("❌ Lỗi khi thêm món!");
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvMon.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvMon.SelectedRows[0].Cells["Id"].Value);
            try
            {
                int result = DataAccess.ExecuteNonQuery(
                    "DELETE FROM Mon WHERE Id=@id",
                    new SqlParameter("@id", id)
                );

                if (result > 0)
                {
                    LoadMon();
                    ClearInputs();
                    MessageBox.Show("✅ Xóa món thành công!");
                }
                else
                {
                    MessageBox.Show("❌ Không xóa được món (có thể bị ràng buộc FK)!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi xóa: " + ex.Message);
            }
        }


        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (dgvMon.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvMon.SelectedRows[0].Cells["Id"].Value);

            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá phải là số dương!");
                return;
            }

            int result = DataAccess.ExecuteNonQuery(
                "UPDATE Mon SET TenMon=@ten, Gia=@gia, Loai=@loai WHERE Id=@id",
                new SqlParameter("@ten", txtTenMon.Text.Trim()),
                new SqlParameter("@gia", gia),
                new SqlParameter("@loai", cmbLoai.SelectedItem.ToString()),
                new SqlParameter("@id", id)
            );

            if (result > 0)
            {
                LoadMon();
                ClearInputs();
                MessageBox.Show("✅ Sửa món thành công!");
            }
            else
            {
                MessageBox.Show("❌ Lỗi khi sửa món!");
            }
        }

        private void ClearInputs()
        {
            txtTenMon.Clear();
            txtGia.Clear();
            cmbLoai.SelectedIndex = 0;
            btnSuaMon.Enabled = false;
            btnXoaMon.Enabled = false;
        }
    }
}
