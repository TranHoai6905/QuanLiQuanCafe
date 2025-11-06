using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanCafe
{

    // Class để truyền dữ liệu món khi chọn từ frmMon về frmHoaDon
    public class MonInfo
    {
        public string TenMon { get; set; }
        public decimal Gia { get; set; }
        public string Loai { get; set; }
    }
}
