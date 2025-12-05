using QuanLiQuanCafe.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace QuanLiQuanCafe.BUS
{
    public class MonBUS
    {
        private readonly string connStr;

        public MonBUS(string connectionString)
        {
            connStr = connectionString;
        }

        // ================= LOẠI MÓN =================
        public DataTable GetLoaiMon() => MonQueries.GetLoaiMon(connStr);
        public int DemMonTheoLoai(string loai) => MonQueries.DemMonTheoLoai(loai, connStr);
        public int XoaLoaiMon(string loai) => MonQueries.XoaLoaiMon(loai, connStr);

        // ================= CRUD MÓN =================
        public DataTable GetMon(string keyword = "", string loai = "Tất cả") =>
            MonQueries.GetMon(keyword, loai, connStr);
        public int SuaMon(int id, string tenMon, decimal gia, string loai, string duongDanAnh = "")
        {
            return MonQueries.SuaMon(id, tenMon, gia, loai, duongDanAnh, connStr);
        }
        public int XoaMon(int id) => MonQueries.XoaMon(id, connStr);

        // ================== CHƯA CÓ ==================
        public void ThemMonVaoHoaDon(int hoaDonId, List<int> danhSachMon)
        {
            foreach (var monId in danhSachMon)
            {
                string sql = @"INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@hoaDonId, @monId, 1)";
                using (var conn = new SqlConnection(connStr))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@hoaDonId", hoaDonId);
                    cmd.Parameters.AddWithValue("@monId", monId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int ThemMon(string ten, decimal gia, string loai, string anh)
        {
            string sql = "INSERT INTO Mon (TenMon, Gia, Loai, Anh) VALUES (@TenMon, @Gia, @Loai, @Anh)";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TenMon", ten);
                cmd.Parameters.AddWithValue("@Gia", gia);
                cmd.Parameters.AddWithValue("@Loai", loai);
                cmd.Parameters.AddWithValue("@Anh", anh);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
