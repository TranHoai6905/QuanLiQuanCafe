// File: DBConnect.cs
// Namespace: QuanLiQuanCafe.DAL

using System.Configuration;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.DAL
{
    public static class DBConnect
    {
        /// <summary>
        /// Chuỗi kết nối dùng chung cho toàn bộ dự án.
        /// </summary>
        public static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

        /// <summary>
        /// Tạo kết nối mới khi cần.
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
