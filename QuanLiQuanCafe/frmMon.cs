using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmMon : Form
    {
        private int hoaDonId;

        public frmMon(int hoaDonId)
        {
            InitializeComponent();
            this.hoaDonId = hoaDonId;
        }

        private void frmMon_Load(object sender, EventArgs e)
        {
            LoadMon();
        }

        private void LoadMon(string keyword = "")
        {
            string sql = "SELECT * FROM Mon WHERE TenMon LIKE @kw";
            dgvMon.DataSource = DataAccess.GetDataTable(sql, new SqlParameter("@kw", "%" + keyword + "%"));
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadMon(txtTimKiem.Text);
        }

        private void btnThemMonVaoDon_Click(object sender, EventArgs e)
        {
            if (dgvMon.SelectedRows.Count == 0) return;

            int monId = Convert.ToInt32(dgvMon.SelectedRows[0].Cells["Id"].Value);

            string sql = "INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@hd, @m, 1)";
            DataAccess.ExecuteNonQuery(sql,
                new SqlParameter("@hd", hoaDonId),
                new SqlParameter("@m", monId));

            MessageBox.Show("Đã thêm món vào hóa đơn!");
        }
    }
}
