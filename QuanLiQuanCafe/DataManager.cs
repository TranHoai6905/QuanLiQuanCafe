using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public static class DataManager
    {
        private static string connectionString = @"Server=HOAI\MSSQLSERVER01;Database=QuanLyQuanCafe1;Integrated Security=True;";

        // ==================== MON ====================
        public static List<Mon> LoadMon()
        {
            List<Mon> ds = new List<Mon>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, TenMon, Gia, Loai FROM Mon";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ds.Add(new Mon()
                            {
                                Id = reader.GetInt32(0),
                                TenMon = reader.GetString(1),
                                Gia = reader.GetDecimal(2),
                                Loai = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi LoadMon: " + ex.Message);
            }
            return ds;
        }

        public static void ThemMon(Mon m)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Mon (TenMon, Gia, Loai) VALUES (@TenMon, @Gia, @Loai)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMon", m.TenMon);
                    cmd.Parameters.AddWithValue("@Gia", m.Gia);
                    cmd.Parameters.AddWithValue("@Loai", m.Loai);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void SuaMon(Mon m)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Mon SET TenMon=@TenMon, Gia=@Gia, Loai=@Loai WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMon", m.TenMon);
                    cmd.Parameters.AddWithValue("@Gia", m.Gia);
                    cmd.Parameters.AddWithValue("@Loai", m.Loai);
                    cmd.Parameters.AddWithValue("@Id", m.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void XoaMon(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Mon WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ==================== HOA DON ====================
        public static List<Mon> LoadHoaDon(int hoaDonId)
        {
            List<Mon> ds = new List<Mon>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT m.Id, m.TenMon, m.Gia, m.Loai, c.SoLuong
                        FROM ChiTietHoaDon c
                        JOIN Mon m ON c.MonId = m.Id
                        WHERE c.HoaDonId = @HoaDonId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HoaDonId", hoaDonId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ds.Add(new Mon()
                            {
                                Id = reader.GetInt32(0),
                                TenMon = reader.GetString(1),
                                Gia = reader.GetDecimal(2),
                                Loai = reader.GetString(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi LoadHoaDon: " + ex.Message);
            }
            return ds;
        }

        public static void ThemMonVaoHD(HoaDon hd, Mon m)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO ChiTietHoaDon (HoaDonId, MonId, SoLuong) VALUES (@HoaDonId, @MonId, 1)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoaDonId", hd.Id);
                    cmd.Parameters.AddWithValue("@MonId", m.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void XoaMonKhoiHD(HoaDon hd, Mon m)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE TOP(1) FROM ChiTietHoaDon WHERE HoaDonId=@HoaDonId AND MonId=@MonId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoaDonId", hd.Id);
                    cmd.Parameters.AddWithValue("@MonId", m.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void ThanhToanHD(HoaDon hd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM ChiTietHoaDon WHERE HoaDonId=@HoaDonId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoaDonId", hd.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static decimal TinhTongTien(HoaDon hd)
        {
            decimal tong = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT SUM(c.SoLuong * m.Gia) 
                    FROM ChiTietHoaDon c
                    JOIN Mon m ON c.MonId = m.Id
                    WHERE c.HoaDonId = @HoaDonId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoaDonId", hd.Id);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        tong = Convert.ToDecimal(result);
                }
            }
            return tong;
        }
    }

    public class Mon
    {
        public int Id { get; set; }
        public string TenMon { get; set; }
        public decimal Gia { get; set; }
        public string Loai { get; set; }
    }

    public class HoaDon
    {
        public int Id { get; set; }
        public List<Mon> MonTrongHD { get; set; } = new List<Mon>();
    }
}
