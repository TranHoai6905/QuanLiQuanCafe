namespace QuanLiQuanCafe.Models
{
    public class Mon
    {
        public int Id { get; set; }         // ID món
        public string TenMon { get; set; }  // Tên món
        public decimal Gia { get; set; }    // Giá món
        public string Loai { get; set; }    // Loại món: "Đồ uống" / "Đồ ăn"
    }
}
