using System;
using System.Data;
using System.Data.SqlClient;
using QuanLiQuanCafe.Models;

namespace QuanLiQuanCafe.DAL
{
    public class TaiKhoanDAL : IDisposable
    {
        private const string CONNECTION_STRING =
            @"Server=.;Database=QuanLyQuanCafe1;Integrated Security=True";

        private SqlConnection _conn;

        public TaiKhoanDAL()
        {
            _conn = new SqlConnection(CONNECTION_STRING);
        }

        // ĐĂNG NHẬP
        public bool KiemTraTaiKhoanTonTai(string tenDangNhap)
        {
            const string sql = "SELECT COUNT(1) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            object result = ExecuteScalar(sql, cmd => cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap.Trim()));
            return SafeConvertToInt(result) > 0;
        }

        public bool KiemTraMatKhau(string tenDangNhap, string matKhau)
        {
            const string sql = "SELECT COUNT(1) FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p";
            object result = ExecuteScalar(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@u", tenDangNhap.Trim());
                cmd.Parameters.AddWithValue("@p", matKhau);
            });
            return SafeConvertToInt(result) > 0;
        }

        public bool KiemTraSoDienThoaiTonTai(string soDienThoai)
        {
            const string sql = "SELECT COUNT(1) FROM TaiKhoan WHERE SoDienThoai = @SoDienThoai";
            object result = ExecuteScalar(sql, cmd => cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai.Trim()));
            return SafeConvertToInt(result) > 0;
        }

        public RegisterResult DangKy(string hoTen, string matKhau, string sdt,
     string diaChi, DateTime ngaySinh, string vaiTro = "Nhân viên")
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                string tenDangNhap = hoTen.Trim();

                // Kiểm tra trước
                if (KiemTraTaiKhoanTonTai(tenDangNhap))
                    return RegisterResult.UsernameExists;

                if (KiemTraSoDienThoaiTonTai(sdt.Trim()))
                    return RegisterResult.PhoneExists;

                conn = new SqlConnection(CONNECTION_STRING);
                conn.Open();

                const string sql = @"
            INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, SoDienThoai, DiaChi, NgaySinh, VaiTro, TrangThai)
            VALUES (@TenDangNhap, @MatKhau, @HoTen, @SoDienThoai, @DiaChi, @NgaySinh, @VaiTro, N'Đang làm')";

                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd.Parameters.AddWithValue("@HoTen", hoTen.Trim());
                cmd.Parameters.AddWithValue("@SoDienThoai", sdt.Trim());
                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(diaChi) ? (object)DBNull.Value : diaChi.Trim());
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh == DateTime.MinValue ? (object)DBNull.Value : ngaySinh);
                cmd.Parameters.AddWithValue("@VaiTro", string.IsNullOrWhiteSpace(vaiTro) ? "Nhân viên" : vaiTro.Trim());

                int rows = cmd.ExecuteNonQuery();
                return rows > 0 ? RegisterResult.Success : RegisterResult.Failed;
            }
            catch (SqlException sqlEx)
            {
                // ✅ THÊM LOGGING CHI TIẾT
                System.Diagnostics.Debug.WriteLine($"❌ SQL Error: {sqlEx.Message}");
                System.Diagnostics.Debug.WriteLine($"❌ SQL Error Number: {sqlEx.Number}");
                System.Diagnostics.Debug.WriteLine($"❌ Stack: {sqlEx.StackTrace}");

                // Ghi ra file log hoặc console
                System.IO.File.AppendAllText(@"C:\temp\error_log.txt",
                    $"{DateTime.Now}: {sqlEx.Message}\n{sqlEx.StackTrace}\n\n");

                return RegisterResult.Failed;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ General Error: {ex.Message}");
                return RegisterResult.Failed;
            }
            finally
            {
                cmd?.Dispose();
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public bool DoiMatKhau(string tenDangNhap, string matKhauCu, string matKhauMoi)
        {
            const string sql = @"
        UPDATE TaiKhoan SET MatKhau = @MatKhauMoi
        WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhauCu";

            return ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap.Trim());
                cmd.Parameters.AddWithValue("@MatKhauCu", matKhauCu);
                cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
            }) > 0;
        }

        // LẤY LẠI MẬT KHẨU
        public bool LayLaiMatKhau(string tenDangNhap, string soDienThoai, string matKhauMoi)
        {
            const string sql = @"
                UPDATE TaiKhoan SET MatKhau = @MatKhauMoi
                WHERE TenDangNhap = @TenDangNhap AND SoDienThoai = @SoDienThoai";

            return ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap.Trim());
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai.Trim());
                cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
            }) > 0;
        }

        // LẤY VAI TRÒ
        public string LayVaiTro(string tenDangNhap)
        {
            const string sql = "SELECT VaiTro FROM TaiKhoan WHERE TenDangNhap = @user";
            object result = ExecuteScalar(sql, cmd => cmd.Parameters.AddWithValue("@user", tenDangNhap));
            return result as string ?? "Nhân viên";
        }

        // LẤY THÔNG TIN TÀI KHOẢN
        public TaiKhoan LayThongTinTaiKhoan(string tenDangNhap)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                conn = new SqlConnection(CONNECTION_STRING);
                const string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @user";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@user", tenDangNhap);
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new TaiKhoan
                    {
                        TenDangNhap = reader["TenDangNhap"].ToString(),
                        MatKhau = reader["MatKhau"].ToString(),  // LẤY MẬT KHẨU VÀO ĐÂY
                        HoTen = reader["HoTen"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString(),
                        DiaChi = reader["DiaChi"]?.ToString(),
                        NgaySinh = reader["NgaySinh"] as DateTime?,
                        VaiTro = reader["VaiTro"].ToString(),
                        TrangThai = reader["TrangThai"]?.ToString()
                    };
                }
                return null;
            }
            finally
            {
                reader?.Close();
                cmd?.Dispose();
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        // CẬP NHẬT THÔNG TIN CÁ NHÂN
        public bool CapNhatThongTinCaNhan(string tenDangNhap, string hoTen, string soDienThoai,
            string diaChi, DateTime ngaySinh, string vaiTro)
        {
            const string sql = @"
                UPDATE TaiKhoan
                SET HoTen = @HoTen, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi,
                    NgaySinh = @NgaySinh, VaiTro = @VaiTro
                WHERE TenDangNhap = @TenDangNhap";

            return ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrWhiteSpace(diaChi) ? (object)DBNull.Value : diaChi);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh == DateTime.MinValue ? (object)DBNull.Value : ngaySinh);
                cmd.Parameters.AddWithValue("@VaiTro", vaiTro);
            }) > 0;
        }

        // LẤY DANH SÁCH TÀI KHOẢN
        public DataTable LayDanhSachTaiKhoan(string trangThai = null)
        {
            // ✅ THÊM CỘT Id VÀO SELECT
            string sql = @"
        SELECT 
            Id,                 -- ✅ THÊM DÒNG NÀY
            TenDangNhap, 
            HoTen,              -- ✅ Đảm bảo có HoTen
            SoDienThoai, 
            DiaChi, 
            NgaySinh, 
            VaiTro, 
            TrangThai 
        FROM TaiKhoan";

            if (!string.IsNullOrEmpty(trangThai))
                sql += " WHERE TrangThai = @TrangThai";

            sql += " ORDER BY Id DESC"; // Sắp xếp theo Id mới nhất

            SqlDataAdapter adapter = null;
            DataTable dt = new DataTable();

            try
            {
                adapter = new SqlDataAdapter(sql, CONNECTION_STRING);
                if (!string.IsNullOrEmpty(trangThai))
                    adapter.SelectCommand.Parameters.AddWithValue("@TrangThai", trangThai);

                adapter.Fill(dt);
            }
            finally
            {
                adapter?.Dispose();
            }

            return dt;
        }

        // XÓA TÀI KHOẢN
        public bool XoaTaiKhoan(string tenDangNhap)
        {
            const string sql = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            return ExecuteNonQuery(sql, cmd => cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap)) > 0;
        }

        // HÀM HỖ TRỢ
        internal object ExecuteScalar(string query, Action<SqlCommand> setupParams)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(CONNECTION_STRING);
                cmd = new SqlCommand(query, conn);
                setupParams(cmd);
                conn.Open();
                return cmd.ExecuteScalar();
            }
            finally
            {
                cmd?.Dispose();
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public int ExecuteNonQuery(string query, Action<SqlCommand> setupParams)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(CONNECTION_STRING);
                cmd = new SqlCommand(query, conn);
                setupParams(cmd);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd?.Dispose();
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        internal int SafeConvertToInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;
            return Convert.ToInt32(value);
        }

        public void Dispose()
        {
            _conn?.Dispose();
        }
        public bool CapNhatTrangThai(string tenDangNhap, string trangThai)
        {
            const string sql = "UPDATE TaiKhoan SET TrangThai = @TrangThai WHERE TenDangNhap = @TenDangNhap";
            return ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
            }) > 0;
        }
    }
}