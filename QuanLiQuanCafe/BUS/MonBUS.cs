// File: MonBUS.cs
using QuanLiQuanCafe.DAL.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace QuanLiQuanCafe.BUS
{
    /// <summary>
    /// Lớp xử lý business logic cho món.
    /// </summary>
    public class MonBUS
    {
        private readonly string connStr;

        /// <summary>
        /// Constructor với chuỗi kết nối.
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối SQL.</param>
        public MonBUS(string connectionString)
        {
            connStr = connectionString;
        }

        // ================= LOẠI MÓN =================

        /// <summary>
        /// Lấy danh sách loại món.
        /// </summary>
        /// <returns>DataTable loại món.</returns>
        public DataTable GetLoaiMon() => MonQueries.GetLoaiMon(connStr);

        /// <summary>
        /// Đếm số món theo loại.
        /// </summary>
        /// <param name="loai">Tên loại.</param>
        /// <returns>Số món.</returns>
        public int DemMonTheoLoai(string loai) => MonQueries.DemMonTheoLoai(loai, connStr);

        /// <summary>
        /// Xóa loại món.
        /// </summary>
        /// <param name="loai">Tên loại.</param>
        /// <returns>Số hàng ảnh hưởng.</returns>
        public int XoaLoaiMon(string loai) => MonQueries.XoaLoaiMon(loai, connStr);

        // ================= CRUD MÓN =================

        /// <summary>
        /// Lấy danh sách món theo từ khóa và loại.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm.</param>
        /// <param name="loai">Loại món (Tất cả để lấy hết).</param>
        /// <returns>DataTable món.</returns>
        public DataTable GetMon(string keyword = "", string loai = "Tất cả") =>
            MonQueries.GetMon(keyword, loai, connStr);

        /// <summary>
        /// Thêm món mới.
        /// </summary>
        /// <param name="ten">Tên món.</param>
        /// <param name="gia">Giá.</param>
        /// <param name="loai">Loại.</param>
        /// <param name="anh">Đường dẫn ảnh.</param>
        /// <returns>Số hàng ảnh hưởng.</returns>
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

        /// <summary>
        /// Sửa thông tin món.
        /// </summary>
        /// <param name="id">ID món.</param>
        /// <param name="tenMon">Tên món.</param>
        /// <param name="gia">Giá.</param>
        /// <param name="loai">Loại.</param>
        /// <param name="duongDanAnh">Đường dẫn ảnh (rỗng để giữ nguyên).</param>
        /// <returns>Số hàng ảnh hưởng.</returns>
        public int SuaMon(int id, string tenMon, decimal gia, string loai, string duongDanAnh = "")
        {
            return MonQueries.SuaMon(id, tenMon, gia, loai, duongDanAnh, connStr);
        }

        /// <summary>
        /// Xóa món theo ID.
        /// </summary>
        /// <param name="id">ID món.</param>
        /// <returns>Số hàng ảnh hưởng.</returns>
        public int XoaMon(int id) => MonQueries.XoaMon(id, connStr);

        /// <summary>
        /// Thêm danh sách món vào hóa đơn.
        /// </summary>
        /// <param name="hoaDonId">ID hóa đơn.</param>
        /// <param name="danhSachMon">Danh sách ID món.</param>
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
    }
}