using System;
using System.Data;
using System.Data.SqlClient;

public class LoaiMonBUS
{
    private string connStr;

    public LoaiMonBUS(string connectionString)
    {
        connStr = connectionString;
    }

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
    /// Kiểm tra số lượng món của loại
    /// </summary>
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
    /// Xóa loại, chỉ cho phép nếu không có món nào
    /// </summary>
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
