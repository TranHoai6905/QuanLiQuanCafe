using System;
using System.Data;
using System.Data.SqlClient;
using QuanLiQuanCafe.Models;

namespace QuanLiQuanCafe.DAL
{
    /// <summary>
    /// Data Access Layer cho Tài Khoản
    /// Áp dụng: Repository Pattern, Dispose Pattern, Extract Method, DRY
    /// </summary>
    public class TaiKhoanDAL : IDisposable
    {
        #region Constants
        /// <summary>
        /// Connection String - Centralized Configuration
        /// </summary>
        private const string CONNECTION_STRING = @"Server=.;Database=QuanLyQuanCafe1;Integrated Security=True";
        #endregion

        #region Fields
        private SqlConnection _conn;
        #endregion

        #region Constructor
        public TaiKhoanDAL()
        {
            _conn = new SqlConnection(CONNECTION_STRING);
        }
        #endregion

        #region Login Operations (Đăng nhập)
        /// <summary>
        /// Phương pháp: Query Method - Kiểm tra tài khoản tồn tại
        /// </summary>
        public bool KiemTraTaiKhoanTonTai(string tenDangNhap)
        {
            const string sql = "SELECT COUNT(1) FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

            object result = ExecuteScalar(sql, cmd =>
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap.Trim()));

            return SafeConvertToInt(result) > 0;
        }

        /// <summary>
        /// Phương pháp: Query Method - Kiểm tra mật khẩu đúng
        /// </summary>
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
        #endregion

        #region Registration Operations (Đăng ký)
        /// <summary>
        /// Phương pháp: Query Method - Kiểm tra số điện thoại tồn tại
        /// </summary>
        public bool KiemTraSoDienThoaiTonTai(string soDienThoai)
        {
            const string sql = "SELECT COUNT(1) FROM TaiKhoan WHERE SoDienThoai = @SoDienThoai";

            object result = ExecuteScalar(sql, cmd =>
                cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai.Trim()));

            return SafeConvertToInt(result) > 0;
        }

        /// <summary>
        /// Phương pháp: Command Method + Transaction Pattern - Đăng ký tài khoản mới
        /// Guard Clauses: Kiểm tra tồn tại trước khi insert
        /// </summary>
        public RegisterResult DangKy(string hoTen, string matKhau, string sdt,
            string diaChi, DateTime ngaySinh, string vaiTro = "Nhân viên")
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                string tenDangNhap = hoTen.Trim();

                // Guard Clause 1: Kiểm tra tên đăng nhập tồn tại
                if (KiemTraTaiKhoanTonTai(tenDangNhap))
                    return RegisterResult.UsernameExists;

                // Guard Clause 2: Kiểm tra SĐT tồn tại
                if (KiemTraSoDienThoaiTonTai(sdt.Trim()))
                    return RegisterResult.PhoneExists;

                // Thực hiện insert
                conn = TaoKetNoi();
                cmd = TaoCommandDangKy(conn, tenDangNhap, hoTen, matKhau, sdt, diaChi, ngaySinh, vaiTro);

                int rows = cmd.ExecuteNonQuery();
                return rows > 0 ? RegisterResult.Success : RegisterResult.Failed;
            }
            catch (SqlException sqlEx)
            {
                XuLyLoiSQL(sqlEx);
                return RegisterResult.Failed;
            }
            catch (Exception ex)
            {
                XuLyLoiChung(ex);
                return RegisterResult.Failed;
            }
            finally
            {
                DongKetNoi(conn, cmd);
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tạo command đăng ký
        /// Builder Pattern: Xây dựng command phức tạp
        /// </summary>
        private SqlCommand TaoCommandDangKy(SqlConnection conn, string tenDangNhap, string hoTen,
            string matKhau, string sdt, string diaChi, DateTime ngaySinh, string vaiTro)
        {
            const string sql = @"
                INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, SoDienThoai, DiaChi, NgaySinh, VaiTro, TrangThai)
                VALUES (@TenDangNhap, @MatKhau, @HoTen, @SoDienThoai, @DiaChi, @NgaySinh, @VaiTro, N'Đang làm')";

            var cmd = new SqlCommand(sql, conn);

            ThemParameterDangKy(cmd, tenDangNhap, hoTen, matKhau, sdt, diaChi, ngaySinh, vaiTro);

            return cmd;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Thêm parameters cho đăng ký
        /// DRY: Tách việc thêm parameters
        /// </summary>
        private void ThemParameterDangKy(SqlCommand cmd, string tenDangNhap, string hoTen,
            string matKhau, string sdt, string diaChi, DateTime ngaySinh, string vaiTro)
        {
            cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", matKhau);
            cmd.Parameters.AddWithValue("@HoTen", hoTen.Trim());
            cmd.Parameters.AddWithValue("@SoDienThoai", sdt.Trim());
            cmd.Parameters.AddWithValue("@DiaChi", XuLyGiaTriNull(diaChi));
            cmd.Parameters.AddWithValue("@NgaySinh", XuLyNgaySinhNull(ngaySinh));
            cmd.Parameters.AddWithValue("@VaiTro", string.IsNullOrWhiteSpace(vaiTro) ? "Nhân viên" : vaiTro.Trim());
        }
        #endregion

        #region Password Operations (Mật khẩu)
        /// <summary>
        /// Phương pháp: Command Method - Đổi mật khẩu
        /// </summary>
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

        /// <summary>
        /// Phương pháp: Command Method - Lấy lại mật khẩu
        /// </summary>
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
        #endregion

        #region Account Information (Thông tin tài khoản)
        /// <summary>
        /// Phương pháp: Query Method - Lấy vai trò
        /// </summary>
        public string LayVaiTro(string tenDangNhap)
        {
            const string sql = "SELECT VaiTro FROM TaiKhoan WHERE TenDangNhap = @user";

            object result = ExecuteScalar(sql, cmd =>
                cmd.Parameters.AddWithValue("@user", tenDangNhap));

            return result as string ?? "Nhân viên";
        }

        /// <summary>
        /// Phương pháp: Query Method + Mapper Pattern - Lấy thông tin tài khoản đầy đủ
        /// </summary>
        public TaiKhoan LayThongTinTaiKhoan(string tenDangNhap)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                conn = TaoKetNoi();
                cmd = TaoCommandLayThongTin(conn, tenDangNhap);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return MapReaderToTaiKhoan(reader);
                }

                return null;
            }
            finally
            {
                reader?.Close();
                DongKetNoi(conn, cmd);
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tạo command lấy thông tin
        /// </summary>
        private SqlCommand TaoCommandLayThongTin(SqlConnection conn, string tenDangNhap)
        {
            const string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @user";
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@user", tenDangNhap);
            return cmd;
        }

        /// <summary>
        /// Phương pháp: Mapper Pattern - Map SqlDataReader sang TaiKhoan object
        /// </summary>
        private TaiKhoan MapReaderToTaiKhoan(SqlDataReader reader)
        {
            return new TaiKhoan
            {
                TenDangNhap = reader["TenDangNhap"].ToString(),
                MatKhau = reader["MatKhau"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                DiaChi = reader["DiaChi"]?.ToString(),
                NgaySinh = reader["NgaySinh"] as DateTime?,
                VaiTro = reader["VaiTro"].ToString(),
                TrangThai = reader["TrangThai"]?.ToString()
            };
        }
        #endregion

        #region Update Operations (Cập nhật)
        /// <summary>
        /// Phương pháp: Command Method - Cập nhật thông tin cá nhân
        /// </summary>
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
                cmd.Parameters.AddWithValue("@DiaChi", XuLyGiaTriNull(diaChi));
                cmd.Parameters.AddWithValue("@NgaySinh", XuLyNgaySinhNull(ngaySinh));
                cmd.Parameters.AddWithValue("@VaiTro", vaiTro);
            }) > 0;
        }

        /// <summary>
        /// Phương pháp: Command Method - Cập nhật trạng thái
        /// </summary>
        public bool CapNhatTrangThai(string tenDangNhap, string trangThai)
        {
            const string sql = "UPDATE TaiKhoan SET TrangThai = @TrangThai WHERE TenDangNhap = @TenDangNhap";

            return ExecuteNonQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
            }) > 0;
        }
        #endregion

        #region Query Operations (Truy vấn)
        /// <summary>
        /// Phương pháp: Query Method - Lấy danh sách tài khoản
        /// </summary>
        public DataTable LayDanhSachTaiKhoan(string trangThai = null)
        {
            string sql = XayDungQueryDanhSach(trangThai);

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

        /// <summary>
        /// Phương pháp: Extract Method + Builder Pattern - Xây dựng query danh sách
        /// </summary>
        private string XayDungQueryDanhSach(string trangThai)
        {
            string sql = @"
                SELECT 
                    Id,
                    TenDangNhap, 
                    HoTen,
                    SoDienThoai, 
                    DiaChi, 
                    NgaySinh, 
                    VaiTro, 
                    TrangThai 
                FROM TaiKhoan";

            if (!string.IsNullOrEmpty(trangThai))
                sql += " WHERE TrangThai = @TrangThai";

            sql += " ORDER BY Id DESC";

            return sql;
        }
        #endregion

        #region Delete Operations (Xóa)
        /// <summary>
        /// Phương pháp: Command Method - Xóa tài khoản
        /// </summary>
        public bool XoaTaiKhoan(string tenDangNhap)
        {
            const string sql = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";

            return ExecuteNonQuery(sql, cmd =>
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap)) > 0;
        }
        #endregion

        #region Helper Methods - Database Operations
        /// <summary>
        /// Phương pháp: Template Method Pattern - Execute scalar query
        /// DRY: Tái sử dụng logic kết nối + execute scalar
        /// </summary>
        internal object ExecuteScalar(string query, Action<SqlCommand> setupParams)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = TaoKetNoi();
                cmd = new SqlCommand(query, conn);
                setupParams(cmd);

                return cmd.ExecuteScalar();
            }
            finally
            {
                DongKetNoi(conn, cmd);
            }
        }

        /// <summary>
        /// Phương pháp: Template Method Pattern - Execute non-query
        /// DRY: Tái sử dụng logic kết nối + execute
        /// </summary>
        public int ExecuteNonQuery(string query, Action<SqlCommand> setupParams)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;

            try
            {
                conn = TaoKetNoi();
                cmd = new SqlCommand(query, conn);
                setupParams(cmd);

                return cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi(conn, cmd);
            }
        }
        #endregion

        #region Helper Methods - Connection Management
        /// <summary>
        /// Phương pháp: Extract Method - Tạo và mở kết nối
        /// Factory Pattern: Tạo connection
        /// </summary>
        private SqlConnection TaoKetNoi()
        {
            var conn = new SqlConnection(CONNECTION_STRING);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Đóng kết nối và dispose
        /// Dispose Pattern: Giải phóng tài nguyên
        /// </summary>
        private void DongKetNoi(SqlConnection conn, SqlCommand cmd)
        {
            cmd?.Dispose();

            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }
        #endregion

        #region Helper Methods - Data Conversion
        /// <summary>
        /// Phương pháp: Safe Conversion - Chuyển đổi an toàn sang int
        /// Null Object Pattern: Trả về 0 thay vì throw exception
        /// </summary>
        internal int SafeConvertToInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return 0;

            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý giá trị null cho string
        /// </summary>
        private object XuLyGiaTriNull(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý ngày sinh null
        /// </summary>
        private object XuLyNgaySinhNull(DateTime ngaySinh)
        {
            return ngaySinh == DateTime.MinValue ? (object)DBNull.Value : ngaySinh;
        }
        #endregion

        #region Helper Methods - Error Handling
        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi SQL
        /// Logging Pattern: Ghi log chi tiết
        /// </summary>
        private void XuLyLoiSQL(SqlException sqlEx)
        {
            System.Diagnostics.Debug.WriteLine($"❌ SQL Error: {sqlEx.Message}");
            System.Diagnostics.Debug.WriteLine($"❌ SQL Error Number: {sqlEx.Number}");
            System.Diagnostics.Debug.WriteLine($"❌ Stack: {sqlEx.StackTrace}");

            try
            {
                System.IO.File.AppendAllText(@"C:\temp\error_log.txt",
                    $"{DateTime.Now}: {sqlEx.Message}\n{sqlEx.StackTrace}\n\n");
            }
            catch
            {
                // Ignore logging errors
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi chung
        /// </summary>
        private void XuLyLoiChung(Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"❌ General Error: {ex.Message}");
        }
        #endregion

        #region IDisposable Implementation
        /// <summary>
        /// Phương pháp: Dispose Pattern - Giải phóng tài nguyên
        /// </summary>
        public void Dispose()
        {
            _conn?.Dispose();
        }
        #endregion
    }
}