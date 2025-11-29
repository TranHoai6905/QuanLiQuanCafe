using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCafe.Models
{
    public enum LoginResult
    {
        Success = 1,           // Đăng nhập thành công
        WrongPassword = 0,     // Sai mật khẩu
        AccountNotFound = -1,  // Không tìm thấy tài khoản
        InvalidInput = -2      // Thông tin đầu vào không hợp lệ (rỗng, sai định dạng...)
    }
}
