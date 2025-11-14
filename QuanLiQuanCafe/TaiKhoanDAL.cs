using QuanLiQuanCafe.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe
{
    public class TaiKhoanDAL
    {
        private const string CONNECTION_STRING =
            @"Server=.;Database=QuanLyQuanCafe1;Integrated Security=True";

        public bool KiemTraTaiKhoanTonTai(string tenDangNhap)
        {
            const string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @user";
            return ExecuteScalar(sql, cmd => cmd.Parameters.AddWithValue("@user", tenDangNhap.Trim())) > 0;
        }

        public bool KiemTraMatKhau(string tenDangNhap, string matKhau)
        {
            const string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p";
            return ExecuteScalar(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@u", tenDangNhap.Trim());
                cmd.Parameters.AddWithValue("@p", matKhau.Trim());
            }) > 0;
        }

        public RegisterResult DangKy(string hoTen, string matKhau, string sdt,
            string diaChi, DateTime ngaySinh, string vaiTro)
        {
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                // Kiểm tra trùng tên đăng nhập
                if (ExecuteScalar("SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @user",
                    cmd => cmd.Parameters.AddWithValue("@user", hoTen.Trim()), conn) > 0)
                    return RegisterResult.UsernameExists;

                // Kiểm tra trùng số điện thoại
                if (ExecuteScalar("SELECT COUNT(*) FROM TaiKhoan WHERE SoDienThoai = @phone",
                    cmd => cmd.Parameters.AddWithValue("@phone", sdt.Trim()), conn) > 0)
                    return RegisterResult.PhoneExists;

                // Insert
                const string insert = @"
                    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, SoDienThoai, DiaChi, NgaySinh, VaiTro)
                    VALUES (@TenDangNhap, @MatKhau, @SoDienThoai, @DiaChi, @NgaySinh, @VaiTro);
                    SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(insert, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", hoTen.Trim());
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    cmd.Parameters.AddWithValue("@SoDienThoai", sdt.Trim());
                    cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(diaChi) ? (object)DBNull.Value : diaChi.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh == DateTime.MinValue ? (object)DBNull.Value : ngaySinh);
                    cmd.Parameters.AddWithValue("@VaiTro", string.IsNullOrEmpty(vaiTro) ? "Nhân viên" : vaiTro.Trim());

                    return cmd.ExecuteScalar() != null ? RegisterResult.Success : RegisterResult.Failed;
                }
            }
        }

        // HÀM CUỐI CÙNG – KHÔNG BAO GIỜ LỖI CONNECTION NỮA!
        private static int ExecuteScalar(string query, Action<SqlCommand> setupParams, SqlConnection existingConnection = null)
        {
            SqlConnection conn = existingConnection;
            bool shouldDispose = false;

            if (conn == null)
            {
                conn = new SqlConnection(CONNECTION_STRING);
                shouldDispose = true;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    setupParams(cmd);

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    object result = cmd.ExecuteScalar();
                    return result is int i ? i : (result is long l ? (int)l : 0);
                }
            }
            finally
            {
                if (shouldDispose && conn != null)
                    conn.Dispose();
            }
        }
    }
}