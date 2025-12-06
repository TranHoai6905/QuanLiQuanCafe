// File: BaoCaoDoanhThuBUS.cs
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using log4net;

namespace CKBCDT
{
    public class BaoCaoDoanhThuBUS
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaoCaoDoanhThuBUS));
        private readonly BaoCaoDoanhThuDAL _dal = new BaoCaoDoanhThuDAL();

        public class KetQuaBaoCao
        {
            public DataTable DuLieu { get; set; } = new DataTable();
            public decimal TongDoanhThu { get; set; }
            public Series BieuDo { get; set; } = new Series("Doanh thu") { ChartType = SeriesChartType.Column };
        }

        public KetQuaBaoCao LayBaoCao(
            DateTime? tuNgay = null,
            DateTime? denNgay = null,
            int? nhanVienId = null,
            string loaiMon = null)
        {
            try
            {
                Logger.Info($"=== BẮT ĐẦU LẤY BÁO CÁO DOANH THU ===");
                Logger.Info($"Tham số: Từ {tuNgay:dd/MM/yyyy} → Đến {denNgay:dd/MM/yyyy} | NV: {nhanVienId} | Loại món: {loaiMon}");

                var dt = _dal.LayDoanhThu(tuNgay, denNgay, nhanVienId, loaiMon);

                if (dt == null || dt.Rows.Count == 0)
                {
                    Logger.Warn("Không có dữ liệu doanh thu với tiêu chí đã chọn.");
                    return new KetQuaBaoCao(); // Trả về rỗng, không lỗi
                }

                decimal tongDoanhThu = dt.AsEnumerable()
                    .Sum(row => row.Field<decimal>("ThanhTien"));

                var series = TaoBieuDoTuDong(dt, loaiMon, nhanVienId);
                series.IsValueShownAsLabel = true;
                series.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                series.LabelForeColor = System.Drawing.Color.Black;

                Logger.Info($"TẢI BÁO CÁO THÀNH CÔNG | {dt.Rows.Count} dòng | Tổng DT: {tongDoanhThu:N0}đ");

                return new KetQuaBaoCao
                {
                    DuLieu = dt,
                    TongDoanhThu = tongDoanhThu,
                    BieuDo = series
                };
            }
            catch (Exception ex)
            {
                Logger.Error("LỖI NGHIÊM TRỌNG KHI LẤY BÁO CÁO DOANH THU: " + ex.ToString());
                throw new ReportDataException("Không thể tải dữ liệu báo cáo doanh thu. Vui lòng kiểm tra lại kết nối hoặc tiêu chí lọc.", ex);
            }
        }

        /// <summary>
        /// Tự động tạo biểu đồ theo thứ tự ưu tiên: Loại món → Nhân viên → Ngày
        /// </summary>
        private Series TaoBieuDoTuDong(DataTable dt, string loaiMon, int? nhanVienId)
        {
            var series = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            try
            {
                // 1. Ưu tiên: Loại món
                if (!string.IsNullOrEmpty(loaiMon))
                {
                    var data = dt.AsEnumerable()
                        .GroupBy(r => r.Field<string>("TenLoai") ?? "Khác")
                        .Select(g => new
                        {
                            Nhom = g.Key,
                            Tong = g.Sum(r => r.Field<decimal>("ThanhTien"))
                        })
                        .OrderBy(x => x.Nhom);

                    foreach (var item in data)
                    {
                        series.Points.AddXY(item.Nhom, (double)item.Tong);
                    }
                    series.Name = "Doanh thu theo loại món";
                    Logger.Info("Tạo biểu đồ: Doanh thu theo loại món");
                    return series;
                }

                // 2. Nhân viên
                if (nhanVienId.HasValue)
                {
                    var data = dt.AsEnumerable()
                        .GroupBy(r => new
                        {
                            MaNV = r.Field<int>("NhanVienId"),
                            TenNV = r.Field<string>("TenNhanVien") ?? "Không xác định"
                        })
                        .Select(g => new
                        {
                            Nhom = $"{g.Key.MaNV:D3} - {g.Key.TenNV}",
                            Tong = g.Sum(r => r.Field<decimal>("ThanhTien"))
                        })
                        .OrderBy(x => x.Nhom);

                    foreach (var item in data)
                    {
                        series.Points.AddXY(item.Nhom, (double)item.Tong);
                    }
                    series.Name = "Doanh thu theo nhân viên";
                    Logger.Info("Tạo biểu đồ: Doanh thu theo nhân viên");
                    return series;
                }

                // 3. Mặc định: Theo ngày
                var groupedByDate = dt.AsEnumerable()
                    .GroupBy(r => r.Field<DateTime>("Ngay").Date)
                    .Select(g => new
                    {
                        Ngay = g.Key,
                        Tong = g.Sum(r => r.Field<decimal>("ThanhTien"))
                    })
                    .OrderBy(x => x.Ngay);

                foreach (var item in groupedByDate)
                {
                    string label = item.Ngay.ToString("dd/MM");
                    series.Points.AddXY(label, (double)item.Tong);
                }
                series.Name = "Doanh thu theo ngày";
                Logger.Info("Tạo biểu đồ: Doanh thu theo ngày");
                return series;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tạo biểu đồ tự động: " + ex.Message);
                // Trả về biểu đồ rỗng thay vì crash
                series.Points.AddXY("Lỗi dữ liệu", 0);
                return series;
            }
        }
    }

    // Dùng chung với form báo cáo khác
    public class ReportDataException : Exception
    {
        public ReportDataException(string message) : base(message) { }
        public ReportDataException(string message, Exception inner) : base(message, inner) { }
    }
}