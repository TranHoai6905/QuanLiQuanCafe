using QuanLiQuanCafe.Models;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string sql = "SELECT * FROM TaiKhoan WHERE TaiKhoan=@tk AND MatKhau=@mk";

            using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@tk", taiKhoan);
                cmd.Parameters.AddWithValue("@mk", matKhau);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int nhanVienId = Convert.ToInt32(reader["Id"]);
                    string vaiTro = reader["VaiTro"].ToString().Trim().ToLower(); // Chuẩn hóa

                    reader.Close();

                    MessageBox.Show(
                        $"Đăng nhập thành công!\nVai trò: {(vaiTro == "admin" ? "Quản trị viên (chỉ xem)" : "Nhân viên (đầy đủ quyền)")}",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (vaiTro == "admin")
                    {
                        // Admin mở frmThemMon
                        frmThemMon frmAdmin = new frmThemMon();
                        this.Hide();
                        frmAdmin.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        // Nhân viên tạo hóa đơn và mở frmHoaDon
                        int hoaDonId = TaoHoaDonMoi(conn, nhanVienId);
                        frmHoaDon frm = new frmHoaDon(nhanVienId, vaiTro, hoaDonId);
                        this.Hide();
                        frm.ShowDialog();
                        this.Show();
                    }
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!", "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }


        private int TaoHoaDonMoi(SqlConnection conn, int nhanVienId)
        {
            string sql = @"INSERT INTO HoaDon (NhanVienId, TrangThai) 
                           OUTPUT INSERTED.Id
                           VALUES (@nv, N'Chưa thanh toán')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nv", nhanVienId);
            return (int)cmd.ExecuteScalar();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnXemMon_Click(object sender, EventArgs e)
        {
            frmMon frm = new frmMon();
            frm.ShowDialog();

        }
    }
}
