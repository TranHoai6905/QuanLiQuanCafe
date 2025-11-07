using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanCafe.Models
{
    public class MonDAO
    {
        public bool ThemMon(Mon mon)
        {
            string sql = "INSERT INTO Mon (TenMon, Gia, Loai) VALUES (@ten, @gia, @loai)";
            int result = DataAccess.ExecuteNonQuery(
                sql,
                new SqlParameter("@ten", mon.TenMon),
                new SqlParameter("@gia", mon.Gia),
                new SqlParameter("@loai", mon.Loai)
            );
            return result > 0;
        }

        public Mon LayMonTheoTen(string ten)
        {
            string sql = "SELECT TOP 1 * FROM Mon WHERE TenMon=@ten";
            DataTable dt = DataAccess.GetDataTable(sql, new SqlParameter("@ten", ten));
            if (dt.Rows.Count == 0) return null;
            DataRow r = dt.Rows[0];
            return new Mon
            {
                Id = (int)r["Id"],
                TenMon = r["TenMon"].ToString(),
                Gia = (decimal)r["Gia"],
                Loai = r["Loai"].ToString()
            };
        }
        public bool XoaMon(int id)
        {
            string sql = "DELETE FROM Mon WHERE Id=@id";
            int result = DataAccess.ExecuteNonQuery(sql, new SqlParameter("@id", id));
            return result > 0;
        }

    }
}
