using System;

namespace QuanLiQuanCafe.Queries
{
    public static class HoaDonQueries
    {
        public static readonly string SQL_LOAD_DANH_SACH = @"
SELECT h.Id, h.NgayTao, tk.HoTen AS NhanVien,
       h.TongTien, h.SoLuongMon, h.TrangThai
FROM HoaDon h
LEFT JOIN TaiKhoan tk ON h.NhanVienId = tk.Id
ORDER BY h.NgayTao DESC";

        public static readonly string SQL_LOAD_CHI_TIET = @"
SELECT c.Id, m.TenMon, c.SoLuong, m.Gia,
       (c.SoLuong * m.Gia) AS ThanhTien
FROM ChiTietHoaDon c
JOIN Mon m ON c.MonId = m.Id
WHERE c.HoaDonId = @id";

        public static readonly string SQL_GET_TRANG_THAI =
            "SELECT TrangThai FROM HoaDon WHERE Id = @id";

        public static readonly string SQL_DELETE_MON =
            "DELETE FROM ChiTietHoaDon WHERE Id = @id";

        public static readonly string SQL_THANH_TOAN = @"
UPDATE HoaDon
SET TrangThai = N'Đã thanh toán'
WHERE Id = @id";

        public static readonly string SQL_DELETE_HOA_DON =
            "DELETE FROM HoaDon WHERE Id = @id";

        public static readonly string SQL_INSERT_HOA_DON = @"
INSERT INTO HoaDon (NhanVienId, NgayTao, TrangThai)
VALUES (@nv, GETDATE(), N'Chưa thanh toán');
SELECT SCOPE_IDENTITY();";

        public static readonly string SQL_LOC_HOA_DON_BASE = @"
SELECT h.Id, h.NgayTao, tk.HoTen AS NhanVien,
       h.TongTien, h.SoLuongMon, h.TrangThai
FROM HoaDon h
LEFT JOIN TaiKhoan tk ON h.NhanVienId = tk.Id
WHERE 1 = 1";
    }
}
