using QuanLiQuanCafe.Models;
using System;

namespace QuanLiQuanCafe
{
    public class TaiKhoanBUS
    {
        private readonly TaiKhoanDAL _dal = new TaiKhoanDAL();

        public LoginResult DangNhap(string tenDangNhap, string matKhau)
        {
            if (!_dal.KiemTraTaiKhoanTonTai(tenDangNhap))
                return LoginResult.AccountNotFound;

            return _dal.KiemTraMatKhau(tenDangNhap, matKhau)
                ? LoginResult.Success
                : LoginResult.WrongPassword;
        }

        public RegisterResult DangKy(string hoTen, string matKhau, string sdt,
            string diaChi, DateTime ngaySinh, string vaiTro)
        {
            if (string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(sdt))
                return RegisterResult.Failed;

            return _dal.DangKy(hoTen, matKhau, sdt, diaChi, ngaySinh, vaiTro);
        }
    }
}