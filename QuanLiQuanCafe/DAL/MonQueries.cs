using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.DAL.Queries
{
    public static class MonQueries
    {
        // ==================== HELPER ====================

        // Tạo SqlConnection – gom chung để không lặp lại code
        private static SqlConnection CreateConn(string connStr)
        {
            return new SqlConnection(connStr);
        }

        // Thêm tham số vào SqlCommand – gọn hơn AddWithValue lặp lại nhiều lần
        private static void AddParam(SqlCommand cmd, string name, object value)
        {
            cmd.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }

        // ==================== LẤY LOẠI MÓN ====================

        // Lấy danh sách loại (DISTINCT) từ bảng Mon
        public static DataTable GetLoaiMon(string connStr)
        {
            const string sql = @"
                SELECT DISTINCT Loai 
                FROM Mon 
                WHERE Loai IS NOT NULL 
                ORDER BY Loai";

            using (var conn = CreateConn(connStr))          // mở kết nối
            using (var cmd = new SqlCommand(sql, conn))      // tạo command truy vấn
            using (var da = new SqlDataAdapter(cmd))         // dùng DataAdapter để fill DataTable
            {
                var dt = new DataTable();                    // tạo bảng dữ liệu
                da.Fill(dt);                                 // fill kết quả vào dt
                return dt;                                   // trả dt về cho BUS/Form
            }
        }

        // ==================== LẤY DANH SÁCH MÓN ====================

        public static DataTable GetMon(string keyword, string loai, string connStr)
        {
            // Query mặc định: tìm theo tên món (LIKE)
            string sql = @"
                SELECT Id, TenMon, Gia, Loai 
                FROM Mon 
                WHERE TenMon LIKE @kw";

            // Kiểm tra có lọc theo loại hay không
            bool locLoai = (!string.IsNullOrEmpty(loai) && loai != "Tất cả");

            // Nếu cần lọc loại → thêm điều kiện
            if (locLoai)
                sql += " AND Loai = @loai";

            sql += " ORDER BY TenMon";   // sắp xếp theo tên món

            using (var conn = CreateConn(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                AddParam(cmd, "@kw", "%" + keyword + "%");    // gán từ khóa tìm kiếm

                if (locLoai)
                    AddParam(cmd, "@loai", loai);             // thêm tham số loại nếu cần

                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();                 // tạo DataTable
                    da.Fill(dt);                              // fill dữ liệu
                    return dt;                                // trả kết quả
                }
            }
        }

        // ==================== THÊM MÓN ====================

        public static int ThemMon(string tenMon, decimal gia, string loai, string connStr)
        {
            const string sql = @"
                INSERT INTO Mon (TenMon, Gia, Loai) 
                VALUES (@ten, @gia, @loai)";

            using (var conn = CreateConn(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                AddParam(cmd, "@ten", tenMon);        // gán tên món
                AddParam(cmd, "@gia", gia);           // gán giá
                AddParam(cmd, "@loai", loai);         // gán loại

                conn.Open();                          // mở DB
                return cmd.ExecuteNonQuery();         // trả số dòng bị ảnh hưởng (1 = OK)
            }
        }

        // ==================== SỬA MÓN ====================

        public static int SuaMon(int id, string tenMon, decimal gia, string loai, string connStr)
        {
            const string sql = @"
                UPDATE Mon 
                SET TenMon=@ten, Gia=@gia, Loai=@loai 
                WHERE Id=@id";

            using (var conn = CreateConn(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                AddParam(cmd, "@ten", tenMon);        // tên mới
                AddParam(cmd, "@gia", gia);           // giá mới
                AddParam(cmd, "@loai", loai);         // loại mới
                AddParam(cmd, "@id", id);             // ID món

                conn.Open();
                return cmd.ExecuteNonQuery();         // trả kết quả
            }
        }

        // ==================== XÓA MÓN ====================

        public static int XoaMon(int id, string connStr)
        {
            const string sql = @"DELETE FROM Mon WHERE Id=@id";

            using (var conn = CreateConn(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                AddParam(cmd, "@id", id);             // ID của món cần xóa

                conn.Open();
                return cmd.ExecuteNonQuery();         // xóa 1 dòng
            }
        }

        // ==================== CHUYỂN LOẠI → LƯU TRỮ ====================

        // Dùng khi người dùng chọn: "Chuyển toàn bộ món của loại này sang Lưu trữ món"
        public static int ChuyenMonSangLuuTru(string loaiCu, string connStr)
        {
            const string sql = @"
                UPDATE Mon 
                SET Loai = N'Lưu trữ món' 
                WHERE Loai = @LoaiCu";

            using (var conn = CreateConn(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                AddParam(cmd, "@LoaiCu", loaiCu);     // loại cũ

                conn.Open();
                return cmd.ExecuteNonQuery();         // số món chuyển sang lưu trữ
            }
        }

        // ==================== XÓA LOẠI ====================

        // Vì không có bảng riêng LoaiMon → luôn trả về 1 như là đã xóa
        public static int XoaLoaiMon(string loai, string connStr)
        {
            return 1;   // để BUS xử lý tiếp
        }
        public static int DemMonTheoLoai(string loai, string connStr)
        {
            string sql = "SELECT COUNT(*) FROM Mon WHERE Loai = @loai";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@loai", loai);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

    }
}
