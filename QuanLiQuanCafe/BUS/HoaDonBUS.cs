// File: HoaDonBUS.cs
// Namespace: QuanLiQuanCafe.BUS
// Mục đích: Lớp logic nghiệp vụ để xử lý các hoạt động liên quan đến hóa đơn (HoaDon).
// Lớp này tương tác với DAL để thực hiện các thao tác CRUD trên hóa đơn.

using System;
using System.Data;
using System.Data.SqlClient;
using QuanLiQuanCafe.DAL;

namespace QuanLiQuanCafe.BUS
{
    public class HoaDonBUS
    {
        /// <summary>
        /// Lấy danh sách các hóa đơn chưa thanh toán, sắp xếp theo ngày tạo giảm dần.
        /// </summary>
        /// <returns>Một DataTable chứa danh sách hóa đơn chưa thanh toán.</returns>
        public DataTable GetHoaDonChuaThanhToan()
        {
            string sql = "SELECT Id, NgayTao, TongTien, SoLuongMon, TrangThai " +
                         "FROM HoaDon WHERE TrangThai=N'Chưa thanh toán' ORDER BY NgayTao DESC";
            return DataAccess.GetDataTable(sql);
        }

        /// <summary>
        /// Thêm một hóa đơn mới cho nhân viên chỉ định và trả về ID hóa đơn vừa tạo.
        /// </summary>
        /// <param name="nhanVienId">ID của nhân viên tạo hóa đơn.</param>
        /// <returns>ID của hóa đơn vừa được chèn.</returns>
        public int ThemHoaDonMoi(int nhanVienId)
        {
            string sql = "INSERT INTO HoaDon (NhanVienId, NgayTao, TrangThai) VALUES (@nv, GETDATE(), N'Chưa thanh toán'); SELECT SCOPE_IDENTITY();";
            var param = new SqlParameter("@nv", nhanVienId);
            return Convert.ToInt32(DataAccess.ExecuteScalar(sql, param));
        }

        /// <summary>
        /// Xóa hóa đơn và các chi tiết liên quan theo ID.
        /// </summary>
        /// <param name="hoaDonId">ID của hóa đơn cần xóa.</param>
        public void XoaHoaDon(int hoaDonId)
        {
            DataAccess.ExecuteNonQuery($"DELETE FROM ChiTietHoaDon WHERE HoaDonId={hoaDonId}");
            DataAccess.ExecuteNonQuery($"DELETE FROM HoaDon WHERE Id={hoaDonId}");
        }
        public DataTable GetChiTietHoaDon(int hoaDonId)
        {
            string sql = $@"
        SELECT c.Id, m.TenMon, c.SoLuong, m.Gia, (m.Gia * c.SoLuong) AS ThanhTien
        FROM ChiTietHoaDon c
        INNER JOIN Mon m ON c.MonId = m.Id
        WHERE c.HoaDonId = {hoaDonId}";

            // Dùng GetDataTable thay vì ExecuteQuery
            return DataAccess.GetDataTable(sql);
        }

    }
}