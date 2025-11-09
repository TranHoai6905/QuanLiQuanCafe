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
    }
}
