// File: LoaiMonBUS.cs
// Namespace: QuanLiQuanCafe.BUS (giả sử, vì code gốc không có namespace)
// Mục đích: Lớp logic nghiệp vụ để xử lý các hoạt động liên quan đến loại món (LoaiMon).
// Lớp này sử dụng kết nối cơ sở dữ liệu để thực hiện CRUD trên bảng LoaiMon.

using System;
using System.Data;
using System.Data.SqlClient;

public class LoaiMonBUS
{
    /// <summary>
    /// Chuỗi kết nối cơ sở dữ liệu.
    /// </summary>
    private string connStr;

    /// <summary>
    /// Constructor để khởi tạo với chuỗi kết nối.
    /// </summary>
    /// <param name="connectionString">Chuỗi kết nối SQL.</param>
    public LoaiMonBUS(string connectionString)
    {
        connStr = connectionString;
    }

    /// <summary>
    /// Lấy tất cả loại món.
    /// </summary>
    /// <returns>DataTable chứa các loại.</returns>
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
    /// Thêm loại mới.
    /// </summary>
    /// <param name="loai">Tên loại.</param>
    /// <returns>Số hàng bị ảnh hưởng (1 nếu thành công).</returns>
    public int ThemLoai(string loai)
    {
        string query = "INSERT INTO LoaiMon(Loai) VALUES(@Loai)";
        using (SqlConnection conn = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Loai", loai);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Xóa loại và tất cả món thuộc loại.
    /// </summary>
    /// <param name="loai">Tên loại cần xóa.</param>
    /// <returns>Tổng số hàng bị ảnh hưởng.</returns>
    public int XoaLoai(string loai)
    {
        int totalAffected = 0;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    string queryXoaMon = "DELETE FROM Mon WHERE Loai = @Loai";
                    using (SqlCommand cmdXoaMon = new SqlCommand(queryXoaMon, conn, trans))
                    {
                        cmdXoaMon.Parameters.AddWithValue("@Loai", loai);
                        totalAffected += cmdXoaMon.ExecuteNonQuery();
                    }
                    string queryXoaLoai = "DELETE FROM LoaiMon WHERE Loai = @Loai";
                    using (SqlCommand cmdXoaLoai = new SqlCommand(queryXoaLoai, conn, trans))
                    {
                        cmdXoaLoai.Parameters.AddWithValue("@Loai", loai);
                        totalAffected += cmdXoaLoai.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return totalAffected;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }

    /// <summary>
    /// Lấy danh sách loại kèm số lượng món.
    /// </summary>
    /// <returns>DataTable chứa STT, Loai, SoLuongMon.</returns>
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