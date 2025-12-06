using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuanLiQuanCafe.Models;
using System;

namespace QuanLiQuanCafeTest
{
    [TestClass]
    public class IT103_ThemMonMoi
    {
        private MonDAO dao;

        [TestInitialize]
        public void Setup()
        {
            dao = new MonDAO();
        }

        [TestMethod]
        public void ThemMonMoi_HopLe_ReturnsTrue()
        {
            // Arrange: tạo tên món ngẫu nhiên để tránh trùng với món có sẵn
            string tenMonTest = "Trà sữa Test " + Guid.NewGuid().ToString().Substring(0, 8);
            Mon monMoi = new Mon
            {
                TenMon = tenMonTest,
                Gia = 30000,
                Loai = "Đồ uống"
            };

            // Act: thêm món
            bool ketQua = dao.ThemMon(monMoi);

            // Assert: kiểm tra thêm thành công
            Assert.IsTrue(ketQua, "Thêm món hợp lệ phải trả về true.");

            // Kiểm tra món tồn tại trong database
            Mon monTrongDB = dao.LayMonTheoTen(tenMonTest);
            Assert.IsNotNull(monTrongDB, "Món vừa thêm phải tồn tại trong database.");
            Assert.AreEqual(30000, monTrongDB.Gia);
            Assert.AreEqual("Đồ uống", monTrongDB.Loai);
        }
    }
}
