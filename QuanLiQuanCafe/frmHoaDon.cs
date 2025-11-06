using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmHoaDon : Form
    {
        private DataTable dtHoaDon;
        private DataTable dtChiTiet;
        private string nhanVien = "NV01";

        public frmHoaDon()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dtHoaDon = new DataTable();
            dtHoaDon.Columns.Add("Mã HD");
            dtHoaDon.Columns.Add("Ngày");
            dtHoaDon.Columns.Add("Nhân viên");
            dtHoaDon.Columns.Add("Tổng tiền");
            dtHoaDon.Columns.Add("Số lượng món");

            dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("Loại");
            dtChiTiet.Columns.Add("Tên món");
            dtChiTiet.Columns.Add("Giá");
            dtChiTiet.Columns.Add("Số lượng");

            dgvHoaDon.DataSource = dtHoaDon;
            dgvChiTiet.DataSource = dtChiTiet;

            // Dữ liệu mẫu
            dtHoaDon.Rows.Add("HD001", DateTime.Now.ToString("dd/MM/yyyy"), nhanVien, 0, 0);
            dtHoaDon.Rows.Add("HD002", DateTime.Now.ToString("dd/MM/yyyy"), nhanVien, 0, 0);
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dtChiTiet.Clear();
            if (dgvHoaDon.CurrentRow != null)
            {
                // Lấy dữ liệu món (ví dụ mẫu)
                dtChiTiet.Rows.Add("Thức uống", "Cà phê sữa", 15000, 1);
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            frmMon frm = new frmMon(admin: false, fromHoaDon: true);
            frm.ShowDialog();
            // Sau khi chọn món, giả lập thêm vào chi tiết hóa đơn
            dtChiTiet.Rows.Add("Thức uống", "Trà đá", 5000, 1);
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow != null)
            {
                DataRow row = (dgvChiTiet.CurrentRow.DataBoundItem as DataRowView).Row;
                dtChiTiet.Rows.Remove(row);
            }
        }


        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null) return;

            decimal tong = 0;
            int soLuong = 0;
            foreach (DataRow r in dtChiTiet.Rows)
            {
                tong += Convert.ToDecimal(r["Giá"]) * Convert.ToInt32(r["Số lượng"]);
                soLuong += Convert.ToInt32(r["Số lượng"]);
            }

            dgvHoaDon.CurrentRow.Cells["Tổng tiền"].Value = tong;
            dgvHoaDon.CurrentRow.Cells["Số lượng món"].Value = soLuong;
            MessageBox.Show($"Thanh toán thành công: {tong} VND");
            dtChiTiet.Clear();
        }
    }
}
