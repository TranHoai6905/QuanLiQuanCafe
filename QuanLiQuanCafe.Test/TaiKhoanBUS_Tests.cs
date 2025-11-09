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
    }
}
