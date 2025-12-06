using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuanLiQuanCafe;
using QuanLiQuanCafe.Models;

namespace QuanLiQuanCafe.Test
{
    [TestClass]
    public class TaiKhoanBUS_SimpleTests
    {
        private TaiKhoanBUS _bus;

        [TestInitialize]
        public void Setup()
        {
            _bus = new TaiKhoanBUS();
        }

        [TestMethod]
        public void DangNhap_ThieuThongTin()
        {
            var result = _bus.DangNhap("", "123");
            Assert.AreEqual(LoginResult.InvalidInput, result);
        }

        [TestMethod]
        public void DangNhap_ThieuThongTin2()
        {
            var result = _bus.DangNhap("admin", "");
            Assert.AreEqual(LoginResult.InvalidInput, result);
        }

        [TestMethod]
        public void DangNhap_KhongTonTai()
        {
            var result = _bus.DangNhap("user_khong_co", "123");
            Assert.AreEqual(LoginResult.AccountNotFound, result);
        }

        [TestMethod]
        public void KiemTraTenDangNhapTonTai_Thieu()
        {
            bool kq = _bus.KiemTraTenDangNhapTonTai("");
            Assert.IsFalse(kq);
        }

        [TestMethod]
        public void KiemTraTenDangNhapTonTai_KhongTonTai()
        {
            bool kq = _bus.KiemTraTenDangNhapTonTai("random_user");
            Assert.IsFalse(kq);
        }

        [TestMethod]
        public void DoiMatKhau_ThieuDuLieu_TraVeFalse()
        {
            Assert.IsFalse(_bus.DoiMatKhau("", "123", "456"));        // tên rỗng
            Assert.IsFalse(_bus.DoiMatKhau("admin", "", "456"));      // mật khẩu cũ rỗng
            Assert.IsFalse(_bus.DoiMatKhau("admin", "123", ""));      // mật khẩu mới rỗng
        }
    }
}
