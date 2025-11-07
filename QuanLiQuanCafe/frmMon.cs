using System;
using System.Collections.Generic;
using System.Data;           // ĐÃ THÊM
using System.Data.SqlClient;
using System.Drawing;        // ĐÃ THÊM
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmMon : Form
    {
        private int hoaDonId;
        private List<int> danhSachMonDaChon = new List<int>(); // LƯU ID MÓN ĐÃ CHỌN
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
            DataTable dt = DataAccess.GetDataTable(sql, new SqlParameter("@kw", "%" + keyword + "%"));
            dgvMon.DataSource = dt;

            // Tô lại màu cho món đã chọn (nếu reload)
            foreach (DataGridViewRow row in dgvMon.Rows)
            {
                int monId = Convert.ToInt32(row.Cells["Id"].Value);
                if (danhSachMonDaChon.Contains(monId))
                {
                    row.Selected = true;
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadMon(txtTimKiem.Text);
        }

        private void btnThemMonVaoDon_Click(object sender, EventArgs e)
        {
            if (danhSachMonDaChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string sqlInsert = "INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@hd, @m, 1)";
                        string sqlUpdate = @"
                    UPDATE HoaDon 
                    SET TongTien = ISNULL((SELECT SUM(ct.SoLuong * m.Gia) FROM ChiTietHoaDon ct JOIN Mon m ON ct.MonId = m.Id WHERE ct.HoaDonId = @hd), 0),
                        SoLuongMon = ISNULL((SELECT SUM(SoLuong) FROM ChiTietHoaDon WHERE HoaDonId = @hd), 0)
                    WHERE Id = @hd";

                        // SỬA: DÙNG AddWithValue
                        foreach (int monId in danhSachMonDaChon)
                        {
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn, transaction);
                            cmd.Parameters.AddWithValue("@hd", hoaDonId);
                            cmd.Parameters.AddWithValue("@m", monId);
                            cmd.ExecuteNonQuery();
                        }

                        // Cập nhật tổng tiền
                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction);
                        cmdUpdate.Parameters.AddWithValue("@hd", hoaDonId);
                        cmdUpdate.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }

                MessageBox.Show($"Đã thêm {danhSachMonDaChon.Count} món vào hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua header

            int monId = Convert.ToInt32(dgvMon.Rows[e.RowIndex].Cells["Id"].Value);

            if (danhSachMonDaChon.Contains(monId))
            {
                // ĐÃ CHỌN → BỎ CHỌN
                danhSachMonDaChon.Remove(monId);
                dgvMon.Rows[e.RowIndex].Selected = false;
                dgvMon.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                // CHƯA CHỌN → CHỌN
                danhSachMonDaChon.Add(monId);
                dgvMon.Rows[e.RowIndex].Selected = true;
                dgvMon.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
            }

            // Cập nhật nút "Thêm"
            btnThemMonVaoDon.Enabled = danhSachMonDaChon.Count > 0;
        }
    }
}