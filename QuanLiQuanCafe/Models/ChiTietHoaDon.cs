namespace QuanLiQuanCafe.Models
{
    public class ChiTietHoaDon
    {
        public int Id { get; set; }          // ID chi tiết
        public int HoaDonId { get; set; }    // ID hóa đơn
        public int MonId { get; set; }       // ID món
        public string TenMon { get; set; }   // Tên món (để hiển thị)
        public decimal Gia { get; set; }     // Giá món
        public int SoLuong { get; set; }     // Số lượng món
        public decimal ThanhTien { get; set; } // Giá * Số lượng
    }
}
