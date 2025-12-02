using System;
using System.Data;
using QuanLiQuanCafe.DAL;

namespace QuanLiQuanCafe.BUS
{
    public class HoaDonBUS
    {
        // Lấy danh sách hóa đơn chưa thanh toán
        public DataTable GetHoaDonChuaThanhToan()
        {
            string sql = "SELECT Id, NgayTao, TongTien, SoLuongMon, TrangThai " +
                         "FROM HoaDon WHERE TrangThai=N'Chưa thanh toán' ORDER BY NgayTao DESC";
            return DataAccess.GetDataTable(sql);
        }

        // Thêm hóa đơn mới, trả về Id vừa tạo
        public int ThemHoaDonMoi(int nhanVienId)
        {
            string sql = $"INSERT INTO HoaDon (NhanVienId, NgayTao, TrangThai) " +
                         $"VALUES ({nhanVienId}, GETDATE(), N'Chưa thanh toán'); " +
                         "SELECT SCOPE_IDENTITY()";
            return Convert.ToInt32(DataAccess.ExecuteScalar(sql));
        }

        // Xóa hóa đơn
        public void XoaHoaDon(int hoaDonId)
        {
            DataAccess.ExecuteNonQuery($"DELETE FROM ChiTietHoaDon WHERE HoaDonId={hoaDonId}");
            DataAccess.ExecuteNonQuery($"DELETE FROM HoaDon WHERE Id={hoaDonId}");
        }
    }
}
