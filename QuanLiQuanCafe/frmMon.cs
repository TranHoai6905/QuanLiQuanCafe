using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmMon : Form
    {
        private int hoaDonId; // Lưu ID hóa đơn hiện tại
        private List<int> danhSachMonDaChon = new List<int>(); // Lưu ID món đã chọn

        public frmMon(int hoaDonId)
        {
            InitializeComponent();
            this.hoaDonId = hoaDonId;
        }
        public frmMon()
        {
            InitializeComponent();
        }


        private void frmMon_Load(object sender, EventArgs e) => LoadMon(); // Load danh sách món khi form load

        // Load danh sách món, có thể filter theo keyword
        private void LoadMon(string keyword = "")
        {
            string sql = "SELECT * FROM Mon WHERE TenMon LIKE @kw";
            DataTable dt = DataAccess.GetDataTable(sql, new SqlParameter("@kw", "%" + keyword + "%"));
            dgvMon.DataSource = dt;

            // Highlight món đã chọn
            foreach (DataGridViewRow row in dgvMon.Rows)
            {
                int monId = Convert.ToInt32(row.Cells["Id"].Value);
                row.Selected = danhSachMonDaChon.Contains(monId);
                row.DefaultCellStyle.BackColor = danhSachMonDaChon.Contains(monId) ? Color.LightBlue : Color.White;
            }

            UpdateButtonStatus(); // Cập nhật nút "Thêm món"
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e) => LoadMon(txtTimKiem.Text); // search realtime

        private void dgvMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Bỏ qua header
            ToggleSelection(dgvMon.Rows[e.RowIndex]); // Refactor: tách hàm toggle chọn món
            UpdateButtonStatus();
        }

        // Toggle chọn hoặc bỏ chọn món
        private void ToggleSelection(DataGridViewRow row)
        {
            int monId = Convert.ToInt32(row.Cells["Id"].Value);
            if (danhSachMonDaChon.Contains(monId))
            {
                danhSachMonDaChon.Remove(monId);
                row.Selected = false;
                row.DefaultCellStyle.BackColor = Color.White;
            }
            else
            {
                danhSachMonDaChon.Add(monId);
                row.Selected = true;
                row.DefaultCellStyle.BackColor = Color.LightBlue;
            }
        }

        // Cập nhật trạng thái nút "Thêm món"
        private void UpdateButtonStatus() => btnThemMonVaoDon.Enabled = danhSachMonDaChon.Count > 0;

        private void btnThemMonVaoDon_Click(object sender, EventArgs e)
        {
            if (danhSachMonDaChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Sử dụng cú pháp using truyền thống cho mọi phiên bản C#
                using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string sqlInsert = "INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@hd, @m, 1)";
                        string sqlUpdate = @"
                            UPDATE HoaDon 
                            SET TongTien = ISNULL((SELECT SUM(ct.SoLuong * m.Gia) 
                                                   FROM ChiTietHoaDon ct 
                                                   JOIN Mon m ON ct.MonId = m.Id 
                                                   WHERE ct.HoaDonId = @hd), 0),
                                SoLuongMon = ISNULL((SELECT SUM(SoLuong) 
                                                     FROM ChiTietHoaDon 
                                                     WHERE HoaDonId = @hd), 0)
                            WHERE Id = @hd";

                        // Thêm từng món đã chọn
                        foreach (int monId in danhSachMonDaChon)
                        {
                            using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@hd", hoaDonId);
                                cmd.Parameters.AddWithValue("@m", monId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Cập nhật tổng tiền và số lượng món
                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction))
                        {
                            cmdUpdate.Parameters.AddWithValue("@hd", hoaDonId);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }

                MessageBox.Show($"Đã thêm {danhSachMonDaChon.Count} món vào hóa đơn!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
