using QuanLiQuanCafe.DAL;
using QuanLiQuanCafe.Models;
using System;
using System.Data;

namespace QuanLiQuanCafe
{
    public class TaiKhoanBUS
    {
        private readonly TaiKhoanDAL _dal = new TaiKhoanDAL();

        // ĐĂNG NHẬP
        public LoginResult DangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
                return LoginResult.InvalidInput;

            if (!_dal.KiemTraTaiKhoanTonTai(tenDangNhap))
                return LoginResult.AccountNotFound;

            return _dal.KiemTraMatKhau(tenDangNhap, matKhau)
                ? LoginResult.Success
                : LoginResult.WrongPassword;
        }

        // ĐĂNG KÝ
        public RegisterResult DangKy(string hoTen, string matKhau, string sdt,
            string diaChi, DateTime ngaySinh, string vaiTro = "Nhân viên")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(matKhau) ||
                    string.IsNullOrWhiteSpace(sdt))
                    return RegisterResult.InvalidInput;

                if (_dal.KiemTraSoDienThoaiTonTai(sdt))
                    return RegisterResult.PhoneExists;

                return _dal.DangKy(hoTen, matKhau, sdt, diaChi, ngaySinh, vaiTro);
            }
            catch (Exception)
            {
                return RegisterResult.Failed;
            }
        }

        // ĐỔI MẬT KHẨU
        public bool DoiMatKhau(string tenDangNhap, string matKhauCu, string matKhauMoi)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhauCu) ||
                string.IsNullOrWhiteSpace(matKhauMoi))
                return false;

            return _dal.DoiMatKhau(tenDangNhap, matKhauCu, matKhauMoi);
        }

        // LẤY LẠI MẬT KHẨU (QUÊN MẬT KHẨU)
        public bool LayLaiMatKhau(string tenDangNhap, string soDienThoai, string matKhauMoi)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(soDienThoai) ||
                string.IsNullOrWhiteSpace(matKhauMoi))
                return false;

            return _dal.LayLaiMatKhau(tenDangNhap, soDienThoai, matKhauMoi);
        }

        // KIỂM TRA TÊN ĐĂNG NHẬP TỒN TẠI (dùng cho đăng ký)
        public bool KiemTraTenDangNhapTonTai(string tenDangNhap)
        {
            return _dal.KiemTraTaiKhoanTonTai(tenDangNhap);
        }
        // LẤY THÔNG TIN TÀI KHOẢN
        public TaiKhoan LayThongTinTaiKhoan(string tenDangNhap)
        {
            return _dal.LayThongTinTaiKhoan(tenDangNhap);
        }

        // CẬP NHẬT THÔNG TIN CÁ NHÂN
        public bool CapNhatThongTinCaNhan(string tenDangNhap, string hoTen, string soDienThoai,
            string diaChi, DateTime ngaySinh, string vaiTro)
        {
            return _dal.CapNhatThongTinCaNhan(tenDangNhap, hoTen, soDienThoai, diaChi, ngaySinh, vaiTro);
        }
        public DataTable LayDanhSachTaiKhoan(string trangThai = null)
    => _dal.LayDanhSachTaiKhoan(trangThai);

        public bool XoaTaiKhoan(string tenDangNhap)
            => _dal.XoaTaiKhoan(tenDangNhap);

        public string LayVaiTro(string tenDangNhap)
        {
            return _dal.LayVaiTro(tenDangNhap); // trả về "Quản lý" hoặc "Nhân viên"
        }
        // ←←← THÊM 2 HÀM NÀY VÀO CUỐI CLASS TaiKhoanBUS CỦA BẠN (giữ nguyên hết code cũ)

        /// <summary>
        /// Dùng cho FormThemNhanVien - Thêm nhân viên mới
        /// </summary>
        public RegisterResult ThemNhanVien(string hoTen, string matKhau, string sdt, string diaChi, DateTime ngaySinh, string vaiTro = "Nhân viên")
        {
            return DangKy(hoTen, matKhau, sdt, diaChi, ngaySinh, vaiTro);
        }

        /// <summary>
        /// Dùng cho FormSuaThongTinNhanVien - Cập nhật thông tin (không đổi mật khẩu nếu để trống)
        /// </summary>
        public bool SuaNhanVien(string tenDangNhap, string hoTen, string soDienThoai, string diaChi, DateTime ngaySinh, string vaiTro, string matKhauMoi = null)
        {
            // Cập nhật thông tin cơ bản
            bool kq = CapNhatThongTinCaNhan(tenDangNhap, hoTen, soDienThoai, diaChi, ngaySinh, vaiTro);

            // Nếu có nhập mật khẩu mới → đổi luôn
            if (!string.IsNullOrWhiteSpace(matKhauMoi))
            {
                // Dùng mật khẩu cũ là gì cũng được vì DAL hiện tại không kiểm tra mật khẩu cũ khi quản lý sửa
                DoiMatKhau(tenDangNhap, "anything", matKhauMoi);
            }

            return kq;
        }
        public bool CapNhatTrangThai(string tenDangNhap, string trangThai)
        {
            return _dal.CapNhatTrangThai(tenDangNhap, trangThai);
        }
    }
}