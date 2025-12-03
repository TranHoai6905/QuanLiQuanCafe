// File: HoaDonQueries.cs
// Namespace: QuanLiQuanCafe.DAL.Queries
// Mục đích: Chứa các truy vấn SQL được định nghĩa sẵn cho các hoạt động liên quan đến hóa đơn.
// Lớp tĩnh này tổ chức các truy vấn để cải thiện tính đọc và bảo trì code.

namespace QuanLiQuanCafe.DAL.Queries
{
    public static class HoaDonQueries
    {
        /// <summary>
        /// Truy vấn SQL để tải toàn bộ danh sách hóa đơn kèm tên nhân viên, sắp xếp theo ngày tạo giảm dần.
        /// </summary>
        public static readonly string SQL_LOAD_DANH_SACH = @"
SELECT h.Id, h.NgayTao, tk.HoTen AS NhanVien,
       h.TongTien, h.SoLuongMon, h.TrangThai
FROM HoaDon h
LEFT JOIN TaiKhoan tk ON h.NhanVienId = tk.Id
ORDER BY h.NgayTao DESC";

        /// <summary>
        /// Truy vấn SQL cơ bản để lọc hóa đơn, có thể mở rộng với điều kiện WHERE.
        /// </summary>
        public static readonly string SQL_LOC_HOA_DON_BASE = @"
SELECT h.Id, h.NgayTao, tk.HoTen AS NhanVien,
       h.TongTien, h.SoLuongMon, h.TrangThai
FROM HoaDon h
LEFT JOIN TaiKhoan tk ON h.NhanVienId = tk.Id
WHERE 1=1";

        /// <summary>
        /// Truy vấn SQL để tải chi tiết hóa đơn theo ID.
        /// </summary>
        public static readonly string SQL_LOAD_CHI_TIET = @"
SELECT c.Id, m.TenMon, c.SoLuong, m.Gia,
       (c.SoLuong * m.Gia) AS ThanhTien
FROM ChiTietHoaDon c
JOIN Mon m ON c.MonId = m.Id
WHERE c.HoaDonId = @id";

        /// <summary>
        /// Truy vấn SQL để lấy trạng thái hóa đơn theo ID.
        /// </summary>
        public static readonly string SQL_GET_TRANG_THAI =
            "SELECT TrangThai FROM HoaDon WHERE Id = @id";

        /// <summary>
        /// Lệnh SQL để xóa một món trong chi tiết hóa đơn theo ID.
        /// </summary>
        public static readonly string SQL_DELETE_MON =
            "DELETE FROM ChiTietHoaDon WHERE Id = @id";

        /// <summary>
        /// Lệnh SQL để cập nhật hóa đơn sang trạng thái 'Đã thanh toán' theo ID.
        /// </summary>
        public static readonly string SQL_THANH_TOAN = @"
UPDATE HoaDon
SET TrangThai = N'Đã thanh toán'
WHERE Id = @id";

        /// <summary>
        /// Lệnh SQL để xóa hóa đơn theo ID.
        /// </summary>
        public static readonly string SQL_DELETE_HOA_DON =
            "DELETE FROM HoaDon WHERE Id = @id";

        /// <summary>
        /// Lệnh SQL để chèn hóa đơn mới và trả về ID.
        /// </summary>
        public static readonly string SQL_INSERT_HOA_DON = @"
INSERT INTO HoaDon (NhanVienId, NgayTao, TrangThai)
VALUES (@nv, GETDATE(), N'Chưa thanh toán');
SELECT SCOPE_IDENTITY();";

        /// <summary>
        /// Truy vấn SQL để đếm số món trong hóa đơn theo ID.
        /// </summary>
        public static readonly string SQL_COUNT_MON = @"
SELECT COUNT(*) FROM ChiTietHoaDon WHERE HoaDonId = @id";
    }
}