// File: MonBUS.cs
// Namespace: QuanLiQuanCafe.BUS
// Mục đích: Lớp logic nghiệp vụ để xử lý các hoạt động liên quan đến món (Mon).
// Lớp này sử dụng kết nối cơ sở dữ liệu để thực hiện CRUD trên bảng Mon và liên quan.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.BUS
{
    public class MonBUS
    {
        /// <summary>
        /// Chuỗi kết nối cơ sở dữ liệu.
        /// </summary>
        private readonly string connStr;

        /// <summary>
        /// Constructor để khởi tạo với chuỗi kết nối.
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối SQL.</param>
        public MonBUS(string connectionString)
        {
            connStr = connectionString;
        }

        /// <summary>
        /// Lấy danh sách các loại món duy nhất từ bảng Mon.
        /// </summary>
        /// <returns>DataTable chứa các loại món.</returns>
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

        /// <summary>
        /// Lấy danh sách món với bộ lọc từ khóa và loại.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm tên món (tùy chọn).</param>
        /// <param name="loai">Loại món (mặc định "Tất cả").</param>
        /// <returns>DataTable chứa danh sách món.</returns>
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

        /// <summary>
        /// Thêm món mới vào bảng Mon.
        /// </summary>
        /// <param name="tenMon">Tên món.</param>
        /// <param name="gia">Giá món.</param>
        /// <param name="loai">Loại món.</param>
        /// <returns>Số hàng bị ảnh hưởng (1 nếu thành công).</returns>
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

        /// <summary>
        /// Sửa thông tin món theo ID.
        /// </summary>
        /// <param name="id">ID món.</param>
        /// <param name="tenMon">Tên món mới.</param>
        /// <param name="gia">Giá mới.</param>
        /// <param name="loai">Loại mới.</param>
        /// <returns>Số hàng bị ảnh hưởng (1 nếu thành công).</returns>
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

        /// <summary>
        /// Xóa món theo ID.
        /// </summary>
        /// <param name="id">ID món cần xóa.</param>
        /// <returns>Số hàng bị ảnh hưởng (1 nếu thành công).</returns>
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

        /// <summary>
        /// Chuyển tất cả món thuộc loại sang 'LuuTruMon'.
        /// </summary>
        /// <param name="loai">Loại cần chuyển.</param>
        /// <returns>Số hàng bị ảnh hưởng.</returns>
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

        /// <summary>
        /// Xóa tất cả món thuộc loại.
        /// </summary>
        /// <param name="loai">Loại cần xóa món.</param>
        /// <returns>Số hàng bị ảnh hưởng.</returns>
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

        /// <summary>
        /// Thêm danh sách món vào hóa đơn.
        /// </summary>
        /// <param name="hoaDonId">ID hóa đơn.</param>
        /// <param name="danhSachMon">Danh sách ID món.</param>
        /// <returns>Tổng số hàng bị ảnh hưởng.</returns>
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