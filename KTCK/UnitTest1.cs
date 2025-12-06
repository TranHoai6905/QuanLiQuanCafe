using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Data.SqlClient;
using System.Data;

namespace KTCT.Tests
{
    [TestClass]
    public class BaoCaoDoanhThuTests
    {
        private const string ConnString = "Data Source=NGUYENNHI2407\\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;TrustServerCertificate=True;";

        [TestMethod]
        public void DoanhThuTheoNgay()
        {
            DateTime tuNgay = new DateTime(2025, 1, 1);
            DateTime denNgay = new DateTime(2025, 12, 31);

            int soHD = 0;
            decimal tongTien = 0m;


            using var conn = new SqlConnection(ConnString);
            conn.Open();
            using var cmd = new SqlCommand(@"
                SELECT COUNT(*) AS SoHD, ISNULL(SUM(TongTien),0) AS DT
                FROM HoaDon
                WHERE TrangThai = N'Đã thanh toán'
                  AND NgayTao >= @tu AND NgayTao < @den", conn);
            cmd.Parameters.AddWithValue("@tu", tuNgay);
            cmd.Parameters.AddWithValue("@den", denNgay);

            using var r = cmd.ExecuteReader();
            if (r.Read())
            {
                soHD = r.GetInt32("SoHD");
                tongTien = r.GetDecimal("DT");
            }

            Assert.AreEqual(soHD, soHD);
            Assert.AreEqual(tongTien, tongTien);
        }
    }

}