using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe
{
    public static class MonQueries
    {
        // ================= HÓA ĐƠN =================
        public static DataTable GetHoaDonChuaThanhToan()
        {
            string sql = "SELECT Id, NgayTao, TongTien, SoLuongMon, TrangThai FROM HoaDon WHERE TrangThai = N'Chưa thanh toán'";
            return DataAccess.GetDataTable(sql);
        }

        public static int ThemHoaDonMoi(int nhanVienId)
        {
            string sql = "INSERT INTO HoaDon (NhanVienId, NgayTao, TrangThai) VALUES (@nv, GETDATE(), N'Chưa thanh toán'); SELECT SCOPE_IDENTITY();";
            object result = DataAccess.ExecuteScalar(sql, new SqlParameter("@nv", nhanVienId));
            return Convert.ToInt32(result);
        }

        public static int XoaHoaDon(int hoaDonId)
        {
            string sql = "DELETE FROM HoaDon WHERE Id = @id";
            return DataAccess.ExecuteNonQuery(sql, new SqlParameter("@id", hoaDonId));
        }

        public static void ThemMonVaoHoaDon(int hoaDonId, List<int> danhSachMon)
        {
            using (SqlConnection conn = new SqlConnection(DataAccess.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlInsert = "INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@hd, @m, 1)";
                        string sqlUpdate = @"
                        UPDATE HoaDon 
                        SET TongTien = ISNULL((SELECT SUM(ct.SoLuong * m.Gia) 
                                               FROM ChiTietHoaDon ct 
                                               JOIN Mon m ON ct.MonId = m.Id 
                                               WHERE ct.HoaDonId = @hd), 0),
                            SoLuongMon = ISNULL((SELECT SUM(SoLuong) 
                                                 FROM ChiTietHoaDon 
                                                 WHERE HoaDonId = @hd), 0)
                        WHERE Id = @hd";

                        foreach (int monId in danhSachMon)
                        {
                            using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@hd", hoaDonId);
                                cmd.Parameters.AddWithValue("@m", monId);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction))
                        {
                            cmdUpdate.Parameters.AddWithValue("@hd", hoaDonId);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // ================= MÓN =================
        public static DataTable GetLoaiMon()
        {
            string sql = "SELECT DISTINCT Loai FROM Mon WHERE Loai IS NOT NULL ORDER BY Loai";
            return DataAccess.GetDataTable(sql);
        }

        public static DataTable GetMon(string keyword = "", string loai = "Tất cả")
        {
            string sql = "SELECT Id, TenMon, Gia, Loai FROM Mon WHERE TenMon LIKE @kw";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@kw", "%" + keyword + "%")
            };

            if (!string.IsNullOrEmpty(loai) && loai != "Tất cả")
            {
                sql += " AND Loai = @loai";
                parameters.Add(new SqlParameter("@loai", loai));
            }

            sql += " ORDER BY TenMon";
            return DataAccess.GetDataTable(sql, parameters.ToArray());
        }

        public static int ThemMon(string tenMon, decimal gia, string loai)
        {
            string sql = "INSERT INTO Mon (TenMon, Gia, Loai) VALUES (@ten, @gia, @loai)";
            return DataAccess.ExecuteNonQuery(sql,
                new SqlParameter("@ten", tenMon),
                new SqlParameter("@gia", gia),
                new SqlParameter("@loai", loai));
        }

        public static int SuaMon(int id, string tenMon, decimal gia, string loai)
        {
            string sql = "UPDATE Mon SET TenMon=@ten, Gia=@gia, Loai=@loai WHERE Id=@id";
            return DataAccess.ExecuteNonQuery(sql,
                new SqlParameter("@ten", tenMon),
                new SqlParameter("@gia", gia),
                new SqlParameter("@loai", loai),
                new SqlParameter("@id", id));
        }

        public static int XoaMon(int id)
        {
            string sql = "DELETE FROM Mon WHERE Id=@id";
            return DataAccess.ExecuteNonQuery(sql, new SqlParameter("@id", id));
        }
    }
}
