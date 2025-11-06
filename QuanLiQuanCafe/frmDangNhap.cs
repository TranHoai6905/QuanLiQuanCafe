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
                    string vaiTro = reader["VaiTro"].ToString();
                    reader.Close();

                    // Tạo hóa đơn mới cho nhân viên đăng nhập
                    int hoaDonId = TaoHoaDonMoi(conn, nhanVienId);

                    frmHoaDon frm = new frmHoaDon(nhanVienId, vaiTro, hoaDonId);
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
