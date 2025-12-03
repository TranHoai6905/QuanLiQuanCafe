// File: DataAccess.cs
// Namespace: QuanLiQuanCafe.DAL
// Mục đích: Lớp truy cập dữ liệu cung cấp các phương thức tĩnh để thực hiện các hoạt động trên cơ sở dữ liệu SQL Server.
// Lớp này xử lý kết nối, thực thi truy vấn và lấy dữ liệu.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.DAL
{
    public static class DataAccess
    {
        /// <summary>
        /// Chuỗi kết nối đến cơ sở dữ liệu SQL Server.
        /// </summary>
        public static string ConnectionString =
            @"Data Source=HOAI\MSSQLSERVER01;Initial Catalog=QuanLyQuanCafe1;Integrated Security=True";

        /// <summary>
        /// Thực thi truy vấn SQL và trả về kết quả dưới dạng DataTable.
        /// </summary>
        /// <param name="sql">Truy vấn SQL cần thực thi.</param>
        /// <param name="parameters">Các tham số SQL tùy chọn.</param>
        /// <returns>Một DataTable chứa kết quả truy vấn.</returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Thực thi lệnh SQL không trả về dữ liệu (như INSERT, UPDATE, DELETE) và trả về số hàng bị ảnh hưởng.
        /// </summary>
        /// <param name="sql">Lệnh SQL cần thực thi.</param>
        /// <param name="parameters">Các tham số SQL tùy chọn.</param>
        /// <returns>Số hàng bị ảnh hưởng.</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Thực thi truy vấn SQL và trả về một giá trị đơn lẻ (scalar).
        /// </summary>
        /// <param name="sql">Truy vấn SQL cần thực thi.</param>
        /// <param name="parameters">Các tham số SQL tùy chọn.</param>
        /// <returns>Giá trị scalar từ truy vấn.</returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }
    }
}