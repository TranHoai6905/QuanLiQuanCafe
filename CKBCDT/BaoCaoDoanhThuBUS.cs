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
        private readonly BaoCaoDoanhThuDAL _dal;

        public BaoCaoDoanhThuBUS()
        {
            _dal = new BaoCaoDoanhThuDAL();
        }

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
            KetQuaBaoCao ketQua = new KetQuaBaoCao();

            // 1. Try-catch-finally đầu tiên: Log bắt đầu + kiểm tra tham số
            try
            {
                Logger.Info("=== BẮT ĐẦU TẠO BÁO CÁO DOANH THU ===");
                Logger.Debug($"Tham số: Từ ngày: {tuNgay:dd/MM/yyyy} | Đến ngày: {denNgay:dd/MM/yyyy} | NV: {nhanVienId} | Loại món: {loaiMon ?? "Tất cả"}");
            }
            catch (Exception ex)
            {
                Logger.Fatal("LỖI NGHIÊM TRỌNG: Không thể ghi log khởi động báo cáo!", ex);
                throw;
            }
            finally
            {
                Logger.Debug("Khối khởi động báo cáo đã hoàn thành (finally #1).");
            }

            // 2. Try-catch-finally thứ hai: Lấy dữ liệu từ DAL
            DataTable dt = null;
            try
            {
                dt = _dal.LayDoanhThu(tuNgay, denNgay, nhanVienId, loaiMon);
            }
            catch (Exception ex)
            {
                Logger.Error("LỖI khi gọi DAL để lấy dữ liệu doanh thu!", ex);
                throw new ReportDataException("Không thể lấy dữ liệu từ cơ sở dữ liệu.", ex);
            }
            finally
            {
                Logger.Debug("Hoàn tất gọi DAL (finally #2).");
            }

            // 3. Try-catch-finally thứ ba: Kiểm tra dữ liệu rỗng
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    Logger.Warn("Không tìm thấy dữ liệu doanh thu nào với điều kiện hiện tại.");
                    return ketQua; // Trả rỗng hợp lệ
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không mong muốn khi kiểm tra DataTable rỗng!", ex);
                throw;
            }
            finally
            {
                Logger.Debug("Kiểm tra dữ liệu rỗng hoàn tất (finally #3).");
            }

            // 4. Try-catch-finally thứ tư: Tính tổng doanh thu
            try
            {
                ketQua.TongDoanhThu = dt.AsEnumerable()
                    .Sum(row => row.Field<decimal>("ThanhTien"));

                Logger.Info($"Tính tổng doanh thu thành công: {ketQua.TongDoanhThu:N0} VNĐ");
            }
            catch (InvalidCastException icex)
            {
                Logger.Error("Lỗi ép kiểu cột ThanhTien trong DataTable!", icex);
                throw new ReportDataException("Dữ liệu cột ThanhTien không hợp lệ.", icex);
            }
            catch (Exception ex)
            {
                Logger.Fatal("LỖI NGHIÊM TRỌNG khi tính tổng doanh thu!", ex);
                throw new ReportDataException("Không thể tính tổng doanh thu.", ex);
            }
            finally
            {
                Logger.Debug("Khối tính tổng doanh thu đã chạy xong (finally #4).");
            }

            // 5. Try-catch-finally thứ năm: Tạo biểu đồ (ưu tiên loại món → nhân viên → ngày)
            try
            {
                ketQua.BieuDo = TaoBieuDoTuDong(dt, loaiMon, nhanVienId);
                ketQua.BieuDo.IsValueShownAsLabel = true;
                ketQua.BieuDo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                ketQua.BieuDo.LabelForeColor = System.Drawing.Color.Black;

                Logger.Info($"Tạo biểu đồ thành công: {ketQua.BieuDo.Name} | Số điểm: {ketQua.BieuDo.Points.Count}");
                Logger.Info($"=== HOÀN TẤT BÁO CÁO DOANH THU | Tổng: {ketQua.TongDoanhThu:N0} VNĐ | Dòng dữ liệu: {dt.Rows.Count:N0} ===");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi nghiêm trọng khi tạo biểu đồ doanh thu!", ex);
                ketQua.BieuDo.Points.Clear();
                ketQua.BieuDo.Points.AddXY("LỖI DỮ LIỆU", 0);
                ketQua.BieuDo.Name = "Biểu đồ lỗi";
            }
            finally
            {
                Logger.Debug("Khối tạo biểu đồ đã kết thúc (finally #5).");
            }

            return ketQua;
        }

        private Series TaoBieuDoTuDong(DataTable dt, string loaiMon, int? nhanVienId)
        {
            var series = new Series("Doanh thu") { ChartType = SeriesChartType.Column };

            if (!string.IsNullOrEmpty(loaiMon))
            {
                var data = dt.AsEnumerable()
                    .GroupBy(r => r.Field<string>("TenLoai") ?? "Khác")
                    .Select(g => new { Nhom = g.Key, Tong = g.Sum(r => r.Field<decimal>("ThanhTien")) })
                    .OrderBy(x => x.Nhom);

                foreach (var item in data)
                    series.Points.AddXY(item.Nhom, (double)item.Tong);
                series.Name = "Doanh thu theo loại món";
            }
            else if (nhanVienId.HasValue)
            {
                var data = dt.AsEnumerable()
                    .GroupBy(r => new {
                        MaNV = r.Field<int>("NhanVienId"),
                        TenNV = r.Field<string>("TenNhanVien") ?? "Không xác định"
                    })
                    .Select(g => new {
                        Nhom = $"{g.Key.MaNV:D3} - {g.Key.TenNV}",
                        Tong = g.Sum(r => r.Field<decimal>("ThanhTien"))
                    })
                    .OrderBy(x => x.Nhom);

                foreach (var item in data)
                    series.Points.AddXY(item.Nhom, (double)item.Tong);
                series.Name = "Doanh thu theo nhân viên";
            }
            else
            {
                var data = dt.AsEnumerable()
                    .GroupBy(r => r.Field<DateTime>("Ngay").Date)
                    .Select(g => new { Ngay = g.Key, Tong = g.Sum(r => r.Field<decimal>("ThanhTien")) })
                    .OrderBy(x => x.Ngay);

                foreach (var item in data)
                    series.Points.AddXY(item.Ngay.ToString("dd/MM"), (double)item.Tong);
                series.Name = "Doanh thu theo ngày";
            }

            return series;
        }
    }

    public class ReportDataException : Exception
    {
        public ReportDataException(string message) : base(message) { }
        public ReportDataException(string message, Exception inner) : base(message, inner) { }
    }
}