using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CKBCDT
{
    /// <summary>
    /// Data Access Layer cho báo cáo doanh thu
    /// Xử lý tất cả các thao tác truy vấn cơ sở dữ liệu
    /// </summary>
    public class BaoCaoDoanhThuDAL
    {
        #region Constants
        private const int COMMAND_TIMEOUT = 30;
        private const string DEFAULT_EMPLOYEE_NAME = "Không xác định";
        #endregion

        #region Fields
        private readonly string _connectionString;
        #endregion

        #region Constructor
        public BaoCaoDoanhThuDAL()
        {
            try
            {
                _connectionString = "Data Source=NGUYENNHI2407\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Connection Timeout=30;";

                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    throw new ArgumentException("Connection string không được để trống");
                }

                Logger.Info("Khởi tạo BaoCaoDoanhThuDAL thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khởi tạo BaoCaoDoanhThuDAL", ex);
                throw;
            }
        }

        public BaoCaoDoanhThuDAL(string connectionString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new ArgumentException("Connection string không được để trống", nameof(connectionString));
                }

                _connectionString = connectionString;
                Logger.Info($"Khởi tạo BaoCaoDoanhThuDAL với custom connection string");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khởi tạo BaoCaoDoanhThuDAL với custom connection", ex);
                throw;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Lấy dữ liệu doanh thu theo nhiều tiêu chí lọc
        /// </summary>
        /// <param name="tuNgay">Ngày bắt đầu</param>
        /// <param name="denNgay">Ngày kết thúc</param>
        /// <param name="nhanVienId">ID nhân viên (null = tất cả)</param>
        /// <param name="loaiMon">Loại món (null = tất cả)</param>
        /// <returns>DataTable chứa dữ liệu doanh thu</returns>
        /// <exception cref="ArgumentException">Khi tham số không hợp lệ</exception>
        /// <exception cref="SqlException">Khi có lỗi SQL</exception>
        /// <exception cref="TimeoutException">Khi hết thời gian chờ</exception>
        public DataTable LayDoanhThu(
            DateTime? tuNgay = null,
            DateTime? denNgay = null,
            int? nhanVienId = null,
            string loaiMon = null)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                Logger.Info("=== BẮT ĐẦU LẤY DỮ LIỆU DOANH THU ===");
                LogFilterCriteria(tuNgay, denNgay, nhanVienId, loaiMon);

                // Validate input
                ValidateInput(tuNgay, denNgay, nhanVienId);

                // Build SQL query
                var sqlQuery = BuildSqlQuery(tuNgay, denNgay, nhanVienId, loaiMon, out List<SqlParameter> parameters);

                Logger.Debug($"SQL Query:\n{sqlQuery}");
                Logger.Debug($"Số tham số: {parameters.Count}");

                // Execute query
                connection = new SqlConnection(_connectionString);
                command = new SqlCommand(sqlQuery, connection);
                command.CommandTimeout = COMMAND_TIMEOUT;
                command.CommandType = CommandType.Text;

                if (parameters.Count > 0)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    LogParameters(parameters);
                }

                connection.Open();
                Logger.Debug("Kết nối database thành công");

                var dataTable = new DataTable();
                reader = command.ExecuteReader();
                dataTable.Load(reader);

                Logger.Info($"✅ LẤY DỮ LIỆU THÀNH CÔNG | Số bản ghi: {dataTable.Rows.Count:N0}");

                return dataTable;
            }
            catch (ArgumentException argEx)
            {
                Logger.Warn($"Tham số đầu vào không hợp lệ: {argEx.Message}");
                throw;
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"❌ LỖI SQL | Mã lỗi: {sqlEx.Number} | Server: {sqlEx.Server}", sqlEx);
                throw new DataAccessException($"Lỗi truy vấn cơ sở dữ liệu (Mã: {sqlEx.Number})", sqlEx);
            }
            catch (TimeoutException timeoutEx)
            {
                Logger.Error("⏱️ HẾT THỜI GIAN CHỜ khi truy vấn database", timeoutEx);
                throw new DataAccessException("Truy vấn mất quá nhiều thời gian. Vui lòng thu hẹp khoảng thời gian.", timeoutEx);
            }
            catch (InvalidOperationException invOpEx)
            {
                Logger.Error("❌ LỖI VẬN HÀNH | Kết nối database không hợp lệ", invOpEx);
                throw new DataAccessException("Không thể kết nối đến cơ sở dữ liệu. Kiểm tra cấu hình.", invOpEx);
            }
            catch (Exception ex)
            {
                Logger.Error("❌ LỖI KHÔNG XÁC ĐỊNH khi lấy dữ liệu doanh thu", ex);
                throw new DataAccessException("Lỗi hệ thống khi lấy dữ liệu báo cáo. Vui lòng liên hệ IT.", ex);
            }
            finally
            {
                // Cleanup resources
                try
                {
                    reader?.Close();
                    reader?.Dispose();
                    command?.Dispose();

                    if (connection != null)
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        connection.Dispose();
                    }

                    Logger.Debug("Đã giải phóng tài nguyên database");
                }
                catch (Exception cleanupEx)
                {
                    Logger.Warn("Lỗi khi giải phóng tài nguyên", cleanupEx);
                }
            }
        }

        /// <summary>
        /// Kiểm tra kết nối database
        /// </summary>
        /// <returns>True nếu kết nối thành công</returns>
        public bool TestConnection()
        {
            SqlConnection connection = null;

            try
            {
                Logger.Info("Kiểm tra kết nối database...");

                connection = new SqlConnection(_connectionString);
                connection.Open();

                Logger.Info("✅ Kết nối database thành công");
                return true;
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"❌ Lỗi kết nối SQL: {sqlEx.Message}", sqlEx);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error("❌ Lỗi không xác định khi kiểm tra kết nối", ex);
                return false;
            }
            finally
            {
                try
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
                catch (Exception cleanupEx)
                {
                    Logger.Warn("Lỗi khi đóng kết nối test", cleanupEx);
                }
            }
        }
        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Validate các tham số đầu vào
        /// </summary>
        private void ValidateInput(DateTime? tuNgay, DateTime? denNgay, int? nhanVienId)
        {
            // Validate date range
            if (tuNgay.HasValue && denNgay.HasValue)
            {
                if (tuNgay.Value > denNgay.Value)
                {
                    var ex = new ArgumentException("Ngày bắt đầu không được lớn hơn ngày kết thúc");
                    Logger.Warn($"Validation failed: {ex.Message}");
                    throw ex;
                }

                if (tuNgay.Value > DateTime.Now)
                {
                    var ex = new ArgumentException("Ngày bắt đầu không được trong tương lai");
                    Logger.Warn($"Validation failed: {ex.Message}");
                    throw ex;
                }

                var daysDiff = (denNgay.Value - tuNgay.Value).Days;
                if (daysDiff > 365)
                {
                    Logger.Warn($"Khoảng thời gian quá lớn: {daysDiff} ngày");
                }
            }

            // Validate employee ID
            if (nhanVienId.HasValue && nhanVienId.Value < 0)
            {
                var ex = new ArgumentException("ID nhân viên không hợp lệ");
                Logger.Warn($"Validation failed: {ex.Message}");
                throw ex;
            }

            Logger.Debug("✅ Validation input thành công");
        }

        /// <summary>
        /// Xây dựng câu truy vấn SQL động
        /// </summary>
        private string BuildSqlQuery(
            DateTime? tuNgay,
            DateTime? denNgay,
            int? nhanVienId,
            string loaiMon,
            out List<SqlParameter> parameters)
        {
            try
            {
                Logger.Debug("Bắt đầu xây dựng SQL query...");

                const string baseSql = @"
                    SELECT
                        HD.Id AS MaHD,
                        CAST(HD.NgayThanhToan AS DATE) AS Ngay,
                        ISNULL(TK.TenNV, N'{0}') AS TenNhanVien,
                        ISNULL(TK.Id, 0) AS NhanVienId,
                        M.TenMon,
                        M.Loai AS TenLoai,
                        CTD.SoLuong,
                        M.Gia,
                        (CTD.SoLuong * M.Gia) AS ThanhTien
                    FROM HoaDon HD
                    INNER JOIN ChiTietHoaDon CTD ON HD.Id = CTD.HoaDonId
                    INNER JOIN Mon M ON CTD.MonId = M.Id
                    LEFT JOIN TaiKhoan TK ON HD.NhanVienId = TK.Id
                    WHERE HD.TrangThai = N'Đã thanh toán'";

                var sql = string.Format(baseSql, DEFAULT_EMPLOYEE_NAME);
                parameters = new List<SqlParameter>();

                // Add date range filter
                if (tuNgay.HasValue && denNgay.HasValue)
                {
                    sql += " AND CAST(HD.NgayThanhToan AS DATE) BETWEEN @TuNgay AND @DenNgay";
                    parameters.Add(new SqlParameter("@TuNgay", SqlDbType.Date) { Value = tuNgay.Value.Date });
                    parameters.Add(new SqlParameter("@DenNgay", SqlDbType.Date) { Value = denNgay.Value.Date });
                    Logger.Debug($"Thêm filter ngày: {tuNgay.Value:dd/MM/yyyy} - {denNgay.Value:dd/MM/yyyy}");
                }

                // Add employee filter
                if (nhanVienId.HasValue && nhanVienId.Value > 0)
                {
                    sql += " AND HD.NhanVienId = @NhanVienId";
                    parameters.Add(new SqlParameter("@NhanVienId", SqlDbType.Int) { Value = nhanVienId.Value });
                    Logger.Debug($"Thêm filter nhân viên ID: {nhanVienId.Value}");
                }

                // Add category filter
                if (!string.IsNullOrWhiteSpace(loaiMon))
                {
                    sql += " AND M.Loai = @LoaiMon";
                    parameters.Add(new SqlParameter("@LoaiMon", SqlDbType.NVarChar, 100) { Value = loaiMon.Trim() });
                    Logger.Debug($"Thêm filter loại món: {loaiMon}");
                }

                // Add ordering
                sql += " ORDER BY HD.NgayThanhToan DESC";

                Logger.Debug($"✅ Xây dựng SQL query thành công với {parameters.Count} tham số");
                return sql;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi xây dựng SQL query", ex);
                throw;
            }
        }

        /// <summary>
        /// Ghi log các tiêu chí lọc
        /// </summary>
        private void LogFilterCriteria(DateTime? tuNgay, DateTime? denNgay, int? nhanVienId, string loaiMon)
        {
            try
            {
                var criteria = new System.Text.StringBuilder();
                criteria.AppendLine("Tiêu chí lọc:");
                criteria.AppendLine($"  • Từ ngày: {(tuNgay.HasValue ? tuNgay.Value.ToString("dd/MM/yyyy") : "Không giới hạn")}");
                criteria.AppendLine($"  • Đến ngày: {(denNgay.HasValue ? denNgay.Value.ToString("dd/MM/yyyy") : "Không giới hạn")}");
                criteria.AppendLine($"  • Nhân viên ID: {(nhanVienId.HasValue && nhanVienId.Value > 0 ? nhanVienId.Value.ToString() : "Tất cả")}");
                criteria.AppendLine($"  • Loại món: {(string.IsNullOrWhiteSpace(loaiMon) ? "Tất cả" : loaiMon)}");

                Logger.Info(criteria.ToString());
            }
            catch
            {
                // Không throw exception nếu logging fail
            }
        }

        /// <summary>
        /// Ghi log các tham số SQL
        /// </summary>
        private void LogParameters(List<SqlParameter> parameters)
        {
            try
            {
                if (parameters == null || parameters.Count == 0)
                {
                    return;
                }

                var paramLog = new System.Text.StringBuilder();
                paramLog.AppendLine("Tham số SQL:");

                foreach (var param in parameters)
                {
                    paramLog.AppendLine($"  • {param.ParameterName} = {param.Value} (Type: {param.SqlDbType})");
                }

                Logger.Debug(paramLog.ToString());
            }
            catch
            {
                // Không throw exception nếu logging fail
            }
        }
        #endregion
    }

    #region Custom Exceptions

    /// <summary>
    /// Exception tùy chỉnh cho Data Access Layer
    /// </summary>
    public class DataAccessException : Exception
    {
        public DataAccessException(string message) : base(message)
        {
        }

        public DataAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    #endregion
}