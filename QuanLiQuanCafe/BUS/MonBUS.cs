using QuanLiQuanCafe.DAL;
using QuanLiQuanCafe.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.BUS
{
    public class MonBUS
    {
        private readonly string connStr;

        public MonBUS(string connectionString)
        {
            connStr = connectionString;
        }

        // ---------------- LOẠI MÓN ----------------

        /// <summary>
        /// Lấy danh sách loại món duy nhất.
        /// </summary>
        public DataTable GetLoaiMon()
        {
            return MonQueries.GetLoaiMon(connStr);
        }

        /// <summary>
        /// Đếm số món thuộc 1 loại.
        /// </summary>
        public int DemMonTheoLoai(string loai)
        {
            return MonQueries.DemMonTheoLoai(loai, connStr);
        }

        // ---------------- CRUD MÓN ----------------

        public DataTable GetMon(string keyword = "", string loai = "Tất cả")
        {
            return MonQueries.GetMon(keyword, loai, connStr);
        }

        public int ThemMon(string tenMon, decimal gia, string loai)
        {
            return MonQueries.ThemMon(tenMon, gia, loai, connStr);
        }

        public int SuaMon(int id, string tenMon, decimal gia, string loai)
        {
            return MonQueries.SuaMon(id, tenMon, gia, loai, connStr);
        }

        public int XoaMon(int id)
        {
            return MonQueries.XoaMon(id, connStr);
        }

        // ---------------- HOÁ ĐƠN ----------------

        public int ThemMonVaoHoaDon(int hoaDonId, List<int> danhSachMon)
        {
            int rowsAffected = 0;
            string sql = "INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@hoaDonId, @monId, 1)";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (int monId in danhSachMon)
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@hoaDonId", hoaDonId);
                        cmd.Parameters.AddWithValue("@monId", monId);
                        rowsAffected += cmd.ExecuteNonQuery();
                    }
                }
            }

            return rowsAffected;
        }

        public DataTable GetMonTheoLoai(int loaiId)
        {
            string sql = "SELECT m.Id, m.TenMon, m.Gia, l.TenLoai AS Loai FROM Mon m " +
                         "INNER JOIN LoaiMon l ON m.LoaiMonId = l.Id " +
                         "WHERE l.Id=@loaiId";
            return DataAccess.GetDataTable(sql, new SqlParameter("@loaiId", loaiId));
        }

    }
}
