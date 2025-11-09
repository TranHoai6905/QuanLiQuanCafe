using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuanLiQuanCafe;

namespace QuanLiQuanCafe.Test
{
    [TestClass]
    public class TaiKhoanBUS_Tests
    {
        [TestMethod]
        public void DangNhap_DungTaiKhoanVaMatKhau()
        {
            bool result = TaiKhoanBUS.KiemTraDangNhap("admin", "123456");
            Assert.IsTrue(result); // mong đợi là TRUE
        }

        [TestMethod]
        public void DangNhap_SaiMatKhau()
        {
            bool result = TaiKhoanBUS.KiemTraDangNhap("admin", "abcdef");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DangNhap_BoTrongTaiKhoan()
        {
            bool result = TaiKhoanBUS.KiemTraDangNhap("", "123456");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DangNhap_BoTrongMatKhau()
        {
            bool result = TaiKhoanBUS.KiemTraDangNhap("admin", "");
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void DoiMatKhau_ThanhCong()
        {
            // Arrange
            string tenDN = "admin";
            string mkCu = "123";
            string sdt = "0909123456";
            string mkMoi = "789";

            // Act
            bool ketQua = TaiKhoanBUS.DoiMatKhau(tenDN, mkCu, sdt, mkMoi);

            // Assert
            Assert.IsTrue(ketQua);
            Assert.AreEqual("789", TaiKhoanBUS.LayMatKhau(tenDN));
        }

        [TestMethod]
        public void DoiMatKhau_SaiMatKhauCu()
        {
            bool ketQua = TaiKhoanBUS.DoiMatKhau("admin", "saimk", "0909123456", "999");
            Assert.IsFalse(ketQua);
        }

        [TestMethod]
        public void DoiMatKhau_SaiSoDienThoai()
        {
            bool ketQua = TaiKhoanBUS.DoiMatKhau("admin", "123", "0000000000", "999");
            Assert.IsFalse(ketQua);
        }

        [TestMethod]
        public void DoiMatKhau_TenDangNhapKhongTonTai()
        {
            bool ketQua = TaiKhoanBUS.DoiMatKhau("khongco", "123", "0909123456", "789");
            Assert.IsFalse(ketQua);
        }

        [TestMethod]
        public void DoiMatKhau_ThieuThongTin()
        {
            bool ketQua = TaiKhoanBUS.DoiMatKhau("", "", "", "");
            Assert.IsFalse(ketQua);
        }
    }
}

