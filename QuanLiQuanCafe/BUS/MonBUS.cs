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

        public int ThemMon(string tenMon, decimal gia, string loai, string duongDanAnh = "")
        {
            byte[] anh = null;
            if (!string.IsNullOrEmpty(duongDanAnh))
                anh = System.IO.File.ReadAllBytes(duongDanAnh);

            return MonQueries.ThemMon(tenMon, gia, loai, connStr); // chỉ 4 tham số
        }

        public int SuaMon(int id, string tenMon, decimal gia, string loai, string duongDanAnh = "")
        {
            byte[] anh = null;
            if (!string.IsNullOrEmpty(duongDanAnh))
                anh = System.IO.File.ReadAllBytes(duongDanAnh);

            return MonQueries.SuaMon(id, tenMon, gia, loai, connStr); // chỉ 5 tham số
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
        public int ThemMon(string ten, decimal gia, string loai, string duongDanAnh, string connStr)
        {
            // copy ảnh vào thư mục Images của project
            string tenFile = Path.GetFileName(duongDanAnh);
            string thuMuc = Path.Combine(Application.StartupPath, "Images");
            if (!Directory.Exists(thuMuc)) Directory.CreateDirectory(thuMuc);
            string pathSave = Path.Combine(thuMuc, tenFile);
            File.Copy(duongDanAnh, pathSave, true); // ghi đè nếu trùng

            // Lưu tên file vào CSDL thay vì đường dẫn gốc
            string sql = "INSERT INTO Mon (TenMon, Gia, Loai, Anh) VALUES (@ten, @gia, @loai, @anh)";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@loai", loai);
                cmd.Parameters.AddWithValue("@anh", tenFile);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

    }

}
