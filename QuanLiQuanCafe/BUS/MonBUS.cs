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

        public DataTable GetLoaiMon()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT DISTINCT Loai FROM Mon";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable GetMon(string keyword = "", string loai = "Tất cả")
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT Id, TenMon, Gia, Loai FROM Mon WHERE 1=1";

                if (!string.IsNullOrEmpty(keyword))
                    sql += " AND TenMon LIKE @keyword";
                if (!string.IsNullOrEmpty(loai) && loai != "Tất cả")
                    sql += " AND Loai = @loai";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(keyword))
                        cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    if (!string.IsNullOrEmpty(loai) && loai != "Tất cả")
                        cmd.Parameters.AddWithValue("@loai", loai);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public int ThemMon(string tenMon, decimal gia, string loai)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "INSERT INTO Mon (TenMon, Gia, Loai) VALUES (@tenMon, @gia, @loai)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@tenMon", tenMon);
                    cmd.Parameters.AddWithValue("@gia", gia);
                    cmd.Parameters.AddWithValue("@loai", loai);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int SuaMon(int id, string tenMon, decimal gia, string loai)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "UPDATE Mon SET TenMon=@tenMon, Gia=@gia, Loai=@loai WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@tenMon", tenMon);
                    cmd.Parameters.AddWithValue("@gia", gia);
                    cmd.Parameters.AddWithValue("@loai", loai);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int XoaMon(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "DELETE FROM Mon WHERE Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int ChuyenMonSangLuuTru(string loai)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "UPDATE Mon SET Loai='LuuTruMon' WHERE Loai=@loai";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@loai", loai);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int XoaLoaiMon(string loai)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "DELETE FROM Mon WHERE Loai=@loai";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@loai", loai);
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // --- Thêm món vào hóa đơn ---
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
    }
}
