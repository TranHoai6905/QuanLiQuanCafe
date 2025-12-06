using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using QuanLiQuanCafe.Queries;

namespace QuanLiQuanCafe.Services
{
    public class HoaDonService
    {
        public DataTable LoadDanhSach(string trangThai = "")
        {
            string sql = HoaDonQueries.SQL_LOC_HOA_DON_BASE;
            var list = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                sql += " AND LTRIM(RTRIM(h.TrangThai)) = @tt";
                list.Add(new SqlParameter("@tt", trangThai));
            }

            sql += " ORDER BY h.NgayTao DESC";
            return DataAccess.GetDataTable(sql, list.ToArray());
        }

        public DataTable LoadChiTiet(int hoaDonId)
        {
            return DataAccess.GetDataTable(
                HoaDonQueries.SQL_LOAD_CHI_TIET,
                new SqlParameter("@id", hoaDonId));
        }

        public string GetTrangThai(int id)
        {
            object kq = DataAccess.ExecuteScalar(
                HoaDonQueries.SQL_GET_TRANG_THAI,
                new SqlParameter("@id", id));

            return kq?.ToString().Trim() ?? "";
        }

        public int CreateHoaDon(int nhanVienId)
        {
            object result = DataAccess.ExecuteScalar(
                HoaDonQueries.SQL_INSERT_HOA_DON,
                new SqlParameter("@nv", nhanVienId));

            return Convert.ToInt32(result);
        }

        public void XoaMon(int chiTietId)
        {
            DataAccess.ExecuteNonQuery(
                HoaDonQueries.SQL_DELETE_MON,
                new SqlParameter("@id", chiTietId));
        }

        public void ThanhToan(int hoaDonId)
        {
            DataAccess.ExecuteNonQuery(
                HoaDonQueries.SQL_THANH_TOAN,
                new SqlParameter("@id", hoaDonId));
        }

        public void XoaHoaDon(int hoaDonId)
        {
            DataAccess.ExecuteNonQuery(
                HoaDonQueries.SQL_DELETE_HOA_DON,
                new SqlParameter("@id", hoaDonId));
        }

        public bool HoaDonCoMon(int id)
        {
            object kq = DataAccess.ExecuteScalar(
                "SELECT COUNT(*) FROM ChiTietHoaDon WHERE HoaDonId=@id",
                new SqlParameter("@id", id));

            return Convert.ToInt32(kq) > 0;
        }
    }
}
