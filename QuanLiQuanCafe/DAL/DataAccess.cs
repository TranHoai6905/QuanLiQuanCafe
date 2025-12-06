using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.DAL
{
    public static class DataAccess
    {
        // Lấy DataTable
        public static DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (SqlConnection conn = DBConnect.GetConnection())
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

        // INSERT, UPDATE, DELETE
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = DBConnect.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        // SCALAR
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = DBConnect.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }
    }
}
