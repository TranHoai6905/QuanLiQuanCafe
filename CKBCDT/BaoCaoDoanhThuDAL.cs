using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using log4net;

namespace CKBCDT
{
    public class BaoCaoDoanhThuDAL
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaoCaoDoanhThuDAL));

        private readonly string _connStr =
            "Data Source=NGUYENNHI2407\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Connection Timeout=30;";

        /// <summary>
        /// Lấy dữ liệu doanh thu theo nhiều tiêu chí lọc
        /// </summary>
        public DataTable LayDoanhThu(
            DateTime? tuNgay = null,
            DateTime? denNgay = null,
            int? nhanVienId = null,
            string loaiMon = null)
        {
            const string baseSql = @"
                SELECT
                    HD.Id AS MaHD,
                    CAST(HD.NgayThanhToan AS DATE) AS Ngay,
                    ISNULL(TK.TenNV, N'Không xác định') AS TenNhanVien,
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

            var sql = baseSql;
            var parameters = new List<SqlParameter>();

            try
            {
                Logger.Info("=== BẮT ĐẦU LẤY DỮ LIỆU DOANH THU TỪ DATABASE ===");
                Logger.Info($"Tiêu chí: Từ {tuNgay:dd/MM/yyyy} → Đến {denNgay:dd/MM/yyyy} | NV ID: {nhanVienId} | Loại món: {loaiMon}");

                // Xây dựng điều kiện động
                if (tuNgay.HasValue && denNgay.HasValue)
                {
                    sql += " AND CAST(HD.NgayThanhToan AS DATE) BETWEEN @TuNgay AND @DenNgay";
                    parameters.Add(new SqlParameter("@TuNgay", tuNgay.Value.Date));
                    parameters.Add(new SqlParameter("@DenNgay", denNgay.Value.Date));
                }

                if (nhanVienId.HasValue && nhanVienId.Value > 0)
                {
                    sql += " AND HD.NhanVienId = @NhanVienId";
                    parameters.Add(new SqlParameter("@NhanVienId", nhanVienId.Value));
                }

                if (!string.IsNullOrWhiteSpace(loaiMon))
                {
                    sql += " AND M.Loai = @LoaiMon";
                    parameters.Add(new SqlParameter("@LoaiMon", loaiMon.Trim()));
                }

                sql += " ORDER BY HD.NgayThanhToan DESC";

                using (var conn = new SqlConnection(_connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters.Count > 0)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    Logger.Debug($"SQL Query: {sql}");
                    Logger.Debug($"Parameters count: {parameters.Count}");

                    var dt = new DataTable();

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }

                    Logger.Info($"LẤY DỮ LIỆU THÀNH CÔNG | Số dòng: {dt.Rows.Count}");
                    return dt;
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"LỖI SQL KHI LẤY DOANH THU: {sqlEx.Message} | Error Number: {sqlEx.Number}", sqlEx);
                throw new Exception("Lỗi kết nối hoặc truy vấn cơ sở dữ liệu. Vui lòng kiểm tra lại server SQL.", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.Error($"LỖI HỆ THỐNG DAL - LẤY DOANH THU THẤT BẠI: {ex.Message}", ex);
                throw new Exception("Đã xảy ra lỗi khi lấy dữ liệu doanh thu. Vui lòng thử lại sau.", ex);
            }
        }
    }
}