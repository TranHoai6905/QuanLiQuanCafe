using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuanLiQuanCafe;
using QuanLiQuanCafe.Models;
using System.Collections.Generic;

namespace QuanLiQuanCafeTest
{
    [TestClass]
    public class IT104_TinhTongTienHoaDon
    {
        [TestMethod]
        public void TinhTongTien_CorrectCalculation()
        {
            // Arrange
            HoaDon hd = new HoaDon();
            hd.ChiTiet = new List<ChiTietHoaDon>
            {
                new ChiTietHoaDon { TenMon = "Trà sữa", Gia = 30000, SoLuong = 2 },
                new ChiTietHoaDon { TenMon = "Bánh mì", Gia = 15000, SoLuong = 1 }
            };

            // Act
            hd.TinhTongTien();

            // Assert
            Assert.AreEqual(75000, hd.TongTien, "Tổng tiền phải bằng 75000.");
            Assert.AreEqual(3, hd.SoLuongMon, "Tổng số lượng món phải bằng 3.");
        }
    }
}
