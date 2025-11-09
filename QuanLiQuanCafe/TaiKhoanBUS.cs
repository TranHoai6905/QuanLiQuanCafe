using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCafe
{
    public class TaiKhoanBUS
    {
        public static bool KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
                return false;

            return (tenDangNhap == "admin" && matKhau == "123456");
        }
        // Dữ liệu tạm để test
        private static Dictionary<string, (string matKhau, string sdt)> taiKhoans =
     new Dictionary<string, (string matKhau, string sdt)>()

         {
            { "admin", ("123", "0909123456") },
            { "user", ("456", "0987654321") }
        };

        public static bool DoiMatKhau(string tenDangNhap, string matKhauCu, string sdt, string matKhauMoi)
        {
            // Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(tenDangNhap) ||
                string.IsNullOrWhiteSpace(matKhauCu) ||
                string.IsNullOrWhiteSpace(sdt) ||
                string.IsNullOrWhiteSpace(matKhauMoi))
                return false;

            // Không tồn tại tài khoản
            if (!taiKhoans.ContainsKey(tenDangNhap))
                return false;

            var tk = taiKhoans[tenDangNhap];

            // Sai mật khẩu cũ hoặc số điện thoại
            if (tk.matKhau != matKhauCu || tk.sdt != sdt)
                return false;

            // Đổi mật khẩu
            taiKhoans[tenDangNhap] = (matKhauMoi, tk.sdt);
            return true;
        }

        // Dùng cho Unit Test để lấy mật khẩu hiện tại
        public static string LayMatKhau(string tenDangNhap)
        {
            return taiKhoans.ContainsKey(tenDangNhap) ? taiKhoans[tenDangNhap].matKhau : null;
        }
    }
}
