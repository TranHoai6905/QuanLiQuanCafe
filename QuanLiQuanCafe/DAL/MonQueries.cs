using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.DAL.Queries
{
    public static class MonQueries
    {
        // ================= MÓN =================
        public static DataTable GetLoaiMon(string connStr)
        {
            string sql = "SELECT DISTINCT Loai FROM Mon WHERE Loai IS NOT NULL ORDER BY Loai";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static DataTable GetMon(string keyword, string loai, string connStr)
        {
            string sql = "SELECT Id, TenMon, Gia, Loai FROM Mon WHERE TenMon LIKE @kw";
            if (!string.IsNullOrEmpty(loai) && loai != "Tất cả")
            {
                sql += " AND Loai = @loai";
            }
            sql += " ORDER BY TenMon";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                if (!string.IsNullOrEmpty(loai) && loai != "Tất cả")
                    cmd.Parameters.AddWithValue("@loai", loai);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static int ThemMon(string tenMon, decimal gia, string loai, string connStr)
        {
            string sql = "INSERT INTO Mon (TenMon, Gia, Loai) VALUES (@ten, @gia, @loai)";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ten", tenMon);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@loai", loai);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static int SuaMon(int id, string tenMon, decimal gia, string loai, string connStr)
        {
            string sql = "UPDATE Mon SET TenMon=@ten, Gia=@gia, Loai=@loai WHERE Id=@id";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ten", tenMon);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@loai", loai);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static int XoaMon(int id, string connStr)
        {
            string sql = "DELETE FROM Mon WHERE Id=@id";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // ================= XÓA LOẠI SANG "LƯU TRỮ" =================
        public static int ChuyenMonSangLuuTru(string loaiCu, string connStr)
        {
            string query = "UPDATE Mon SET Loai = N'Lưu trữ món' WHERE Loai = @LoaiCu";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LoaiCu", loaiCu);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static int XoaLoaiMon(string loai, string connStr)
        {
            // Vì không có bảng LoaiMon, chỉ return 1 = thành công
            return 1;
        }
    }
}
