using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCafe.Models
{
    public enum RegisterResult
    {
        Success = 1,           // Đăng ký thành công
        Failed = 0,            // Thất bại chung
        UsernameExists = -1,   // Tên đăng nhập đã tồn tại
        PhoneExists = -2,      // Số điện thoại đã tồn tại
        InvalidInput = -3      // Thiếu thông tin hoặc không hợp lệ
    }
}
