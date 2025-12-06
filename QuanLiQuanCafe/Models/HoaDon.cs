using System.Collections.Generic;
using System.Linq;

namespace QuanLiQuanCafe.Models
{
    public class HoaDon
    {
        public List<ChiTietHoaDon> ChiTiet { get; set; } = new List<ChiTietHoaDon>(); // Danh sách món trong hóa đơn
        public decimal TongTien { get; private set; }  // Tổng tiền hóa đơn
        public int SoLuongMon { get; private set; }    // Tổng số lượng món

        // Tính toán tổng tiền và tổng số lượng
        public void TinhTongTien()
        {
            TongTien = ChiTiet.Sum(x => x.Gia * x.SoLuong);
            SoLuongMon = ChiTiet.Sum(x => x.SoLuong);
        }
    }
}
