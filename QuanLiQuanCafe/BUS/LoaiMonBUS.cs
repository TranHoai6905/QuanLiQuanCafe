// File: LoaiMonBUS.cs
using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.BUS
{
    /// <summary>
    /// Lớp xử lý business logic cho loại món.
    /// </summary>
    public class LoaiMonBUS
    {
        private readonly string connStr;

        /// <summary>
        /// Constructor với chuỗi kết nối.
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối SQL.</param>
        public LoaiMonBUS(string connectionString)
        {
            connStr = connectionString;
        }

        /// <summary>
        /// Lấy danh sách loại món sắp xếp theo tên.
        /// </summary>
        /// <returns>DataTable loại món.</returns>
        public DataTable GetLoai()
        {
            string query = "SELECT Loai FROM LoaiMon ORDER BY Loai";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Thêm loại món mới.
        /// </summary>
        /// <param name="loai">Tên loại.</param>
        /// <returns>Số hàng ảnh hưởng (1 nếu thành công).</returns>
        public int ThemLoai(string loai)
        {
            string query = "INSERT INTO LoaiMon(Loai) VALUES(@Loai)";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 100).Value = loai;
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Đếm số lượng món thuộc loại.
        /// </summary>
        /// <param name="loai">Tên loại.</param>
        /// <returns>Số lượng món.</returns>
        public int SoLuongMon(string loai)
        {
            string query = "SELECT COUNT(*) FROM Mon WHERE Loai=@Loai";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 100).Value = loai;
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Xóa loại món nếu không có món nào thuộc loại đó.
        /// </summary>
        /// <param name="loai">Tên loại.</param>
        /// <returns>Số hàng ảnh hưởng (1 nếu thành công).</returns>
        public int XoaLoai(string loai)
        {
            string query = "DELETE FROM LoaiMon WHERE Loai=@Loai";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 100).Value = loai;
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lấy danh sách loại món kèm số lượng món.
        /// </summary>
        /// <returns>DataTable loại món với STT và số lượng.</returns>
        public DataTable GetLoaiWithCount()
        {
            string query = @"
                SELECT
                    ROW_NUMBER() OVER(ORDER BY lm.Loai) AS STT,
                    lm.Loai,
                    COUNT(m.Id) AS SoLuongMon
                FROM LoaiMon lm
                LEFT JOIN Mon m ON lm.Loai = m.Loai
                GROUP BY lm.Loai
                ORDER BY lm.Loai
            ";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}