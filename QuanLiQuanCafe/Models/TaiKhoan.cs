using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCafe.Models
{
    public class TaiKhoan
    {
        public int? Id { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; } 
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
    }
}

