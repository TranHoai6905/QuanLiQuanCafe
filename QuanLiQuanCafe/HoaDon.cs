using System.Collections.Generic;
using System.Linq;

namespace QuanLiQuanCafe.Models
{
    public class HoaDon
    {
        public List<ChiTietHoaDon> ChiTiet { get; set; } = new List<ChiTietHoaDon>();
        public decimal TongTien { get; private set; }
        public int SoLuongMon { get; private set; }

        public void TinhTongTien()
        {
            TongTien = ChiTiet.Sum(x => x.Gia * x.SoLuong);
            SoLuongMon = ChiTiet.Sum(x => x.SoLuong);
        }
    }
}
