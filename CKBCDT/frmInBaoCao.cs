using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Diagnostics;
using DrawingFont = System.Drawing.Font;
using DrawingRectangle = System.Drawing.Rectangle;

namespace CKBCDT
{
    public partial class frmInBaoCao : Form
    {
        #region Constants
        private const string TITLE = "BÁO CÁO DOANH THU THEO CA BÁN - MIU COFFEE";
        private const int MORNING_SHIFT_START = 6;
        private const int MORNING_SHIFT_END = 14;
        private const int EVENING_SHIFT_START = 14;
        private const int EVENING_SHIFT_END = 22;
        private const int SHIFT_INTERVAL = 2;
        private const int TOTAL_SHIFTS = 8;
        private const string NO_SALES_MESSAGE = "Chưa có bán";
        private const int PANEL_CAPTURE_DELAY = 200;
        #endregion

        #region Properties
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }

        private readonly string _connectionString;
        #endregion

        #region Data Models
        private class ShiftData
        {
            public int OrderCount { get; set; }
            public decimal Revenue { get; set; }
            public string BestSellingItem { get; set; } = NO_SALES_MESSAGE;
            public int BestSellingQuantity { get; set; }

            public override string ToString()
            {
                return $"Orders: {OrderCount}, Revenue: {Revenue:N0}đ, Best: {BestSellingItem}";
            }
        }
        #endregion

        #region Custom Exceptions
        public class DatabaseConnectionException : Exception
        {
            public DatabaseConnectionException(string message, Exception innerException)
                : base(message, innerException) { }
        }

        public class DataLoadException : Exception
        {
            public DataLoadException(string message, Exception innerException)
                : base(message, innerException) { }
        }

        public class PdfExportException : Exception
        {
            public PdfExportException(string message, Exception innerException)
                : base(message, innerException) { }
        }
        #endregion

        #region Constructor & Initialization
        public frmInBaoCao()
        {
            try
            {
                Logger.Info("Khởi tạo form In Báo cáo");
                InitializeComponent();

                _connectionString = @"Server=NGUYENNHI2407\SQLEXPRESS;Database=QuanLyQuanCafe2;Integrated Security=True;";

                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    throw new ArgumentException("Connection string không được để trống");
                }

                InitializeEventHandlers();
                Logger.Info("Khởi tạo form In Báo cáo thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khởi tạo form In Báo cáo", ex);
                MessageBox.Show(
                    "Không thể khởi tạo form in báo cáo. Vui lòng kiểm tra cấu hình.",
                    "Lỗi khởi tạo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                throw;
            }
        }

        private void InitializeEventHandlers()
        {
            try
            {
                this.Load += FrmInBaoCao_Load;
                this.btnXuatPDF.Click += BtnXuatPDF_Click;
                Logger.Debug("Đăng ký event handlers thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi đăng ký event handlers", ex);
                throw new InvalidOperationException("Không thể đăng ký event handlers", ex);
            }
        }

        private void FrmInBaoCao_Load(object sender, EventArgs e)
        {
            try
            {
                Logger.Info($"Tải báo cáo: {TuNgay:dd/MM/yyyy} - {DenNgay:dd/MM/yyyy}");

                ValidateDateRange();
                this.Text = TITLE;
                LoadReportData();

                Logger.Info("Tải báo cáo thành công");
            }
            catch (ArgumentException argEx)
            {
                Logger.Warn($"Dữ liệu đầu vào không hợp lệ: {argEx.Message}");
                ShowErrorMessage("Dữ liệu không hợp lệ", argEx.Message);
            }
            catch (DatabaseConnectionException dbEx)
            {
                Logger.Error("Lỗi kết nối database", dbEx);
                ShowErrorMessage("Lỗi kết nối", "Không thể kết nối đến cơ sở dữ liệu. Vui lòng kiểm tra kết nối.");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi tải báo cáo", ex);
                ShowErrorMessage("Lỗi", "Không thể tải báo cáo. Vui lòng thử lại.");
            }
        }
        #endregion

        #region Validation
        private void ValidateDateRange()
        {
            if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue)
            {
                var ex = new ArgumentException("Ngày bắt đầu và ngày kết thúc không được để trống");
                Logger.Error("Ngày không hợp lệ", ex);
                throw ex;
            }

            if (DenNgay <= TuNgay)
            {
                var ex = new ArgumentException("Ngày kết thúc phải lớn hơn ngày bắt đầu");
                Logger.Error("Khoảng thời gian không hợp lệ", ex);
                throw ex;
            }

            if (TuNgay > DateTime.Now)
            {
                var ex = new ArgumentException("Ngày bắt đầu không thể trong tương lai");
                Logger.Error("Ngày trong tương lai", ex);
                throw ex;
            }

            Logger.Debug($"Validation thành công: {TuNgay:dd/MM/yyyy} - {DenNgay:dd/MM/yyyy}");
        }
        #endregion

        #region Data Loading
        private void LoadReportData()
        {
            ShiftData morningData = null;
            ShiftData eveningData = null;

            try
            {
                Logger.Info("Bắt đầu tải dữ liệu báo cáo");

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    Logger.Debug("Kết nối database thành công");

                    morningData = LoadShiftData(connection, MORNING_SHIFT_START, MORNING_SHIFT_END, "Sáng");
                    eveningData = LoadShiftData(connection, EVENING_SHIFT_START, EVENING_SHIFT_END, "Tối");
                }

                if (morningData == null || eveningData == null)
                {
                    throw new DataLoadException("Không thể tải dữ liệu ca làm việc", null);
                }

                DisplayReportSummary(morningData, eveningData);
                DisplayShiftDetails(morningData, eveningData);
                LoadBieuDo8Ca();

                Logger.Info("Tải dữ liệu báo cáo thành công");
            }
            catch (SqlException sqlEx)
            {
                Logger.Error("Lỗi SQL khi tải báo cáo", sqlEx);
                throw new DatabaseConnectionException("Lỗi truy vấn cơ sở dữ liệu", sqlEx);
            }
            catch (DataLoadException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không xác định khi tải báo cáo", ex);
                throw new DataLoadException("Lỗi tải dữ liệu báo cáo", ex);
            }
        }

        private ShiftData LoadShiftData(SqlConnection connection, int startHour, int endHour, string shiftName)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Kết nối database chưa được mở");
            }

            var data = new ShiftData();

            try
            {
                Logger.Debug($"Tải dữ liệu ca {shiftName} ({startHour}h-{endHour}h)");

                LoadShiftRevenue(connection, startHour, endHour, data);
                LoadBestSellingItem(connection, startHour, endHour, data);

                Logger.Info($"Ca {shiftName}: {data}");
                return data;
            }
            catch (Exception ex)
            {
                Logger.Error($"Lỗi khi tải dữ liệu ca {shiftName}", ex);
                throw;
            }
        }

        private void LoadShiftRevenue(SqlConnection connection, int startHour, int endHour, ShiftData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            const string sql = @"
                SELECT 
                    COUNT(*) AS SoDon,
                    ISNULL(SUM(TongTien), 0) AS DoanhThu
                FROM HoaDon
                WHERE TrangThai = N'Đã thanh toán'
                  AND NgayTao >= @tu 
                  AND NgayTao < @den
                  AND DATEPART(HOUR, NgayTao) >= @startHour 
                  AND DATEPART(HOUR, NgayTao) < @endHour";

            try
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandTimeout = 30;
                    command.Parameters.AddWithValue("@tu", TuNgay.Date);
                    command.Parameters.AddWithValue("@den", DenNgay.Date);
                    command.Parameters.AddWithValue("@startHour", startHour);
                    command.Parameters.AddWithValue("@endHour", endHour);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.OrderCount = SafeGetInt32(reader, 0);
                            data.Revenue = SafeGetDecimal(reader, 1);
                        }
                    }
                }

                Logger.Debug($"Doanh thu ca {startHour}h-{endHour}h: {data.OrderCount} đơn, {data.Revenue:N0}đ");
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"Lỗi SQL khi lấy doanh thu ca {startHour}h-{endHour}h", sqlEx);
                throw;
            }
        }

        private void LoadBestSellingItem(SqlConnection connection, int startHour, int endHour, ShiftData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            const string sql = @"
                SELECT TOP 1 
                    m.TenMon, 
                    SUM(ct.SoLuong) AS SL
                FROM ChiTietHoaDon ct
                JOIN HoaDon hd ON ct.HoaDonId = hd.Id
                JOIN Mon m ON ct.MonId = m.Id
                WHERE hd.TrangThai = N'Đá thanh toán'
                  AND hd.NgayTao >= @tu 
                  AND hd.NgayTao < @den
                  AND DATEPART(HOUR, hd.NgayTao) >= @startHour 
                  AND DATEPART(HOUR, hd.NgayTao) < @endHour
                GROUP BY m.TenMon
                ORDER BY SUM(ct.SoLuong) DESC";

            try
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandTimeout = 30;
                    command.Parameters.AddWithValue("@tu", TuNgay.Date);
                    command.Parameters.AddWithValue("@den", DenNgay.Date);
                    command.Parameters.AddWithValue("@startHour", startHour);
                    command.Parameters.AddWithValue("@endHour", endHour);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.BestSellingItem = SafeGetString(reader, 0);
                            data.BestSellingQuantity = SafeGetInt32(reader, 1);
                            Logger.Debug($"Món bán chạy {startHour}h-{endHour}h: {data.BestSellingItem} ({data.BestSellingQuantity})");
                        }
                        else
                        {
                            Logger.Debug($"Không có món bán chạy trong ca {startHour}h-{endHour}h");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"Lỗi SQL khi lấy món bán chạy {startHour}h-{endHour}h", sqlEx);
                throw;
            }
        }
        #endregion

        #region Display Methods
        private void DisplayReportSummary(ShiftData morning, ShiftData evening)
        {
            if (morning == null)
            {
                throw new ArgumentNullException(nameof(morning));
            }

            if (evening == null)
            {
                throw new ArgumentNullException(nameof(evening));
            }

            try
            {
                var totalRevenue = morning.Revenue + evening.Revenue;
                var totalOrders = morning.OrderCount + evening.OrderCount;

                lblThoiGian.Text = $"Từ ngày {TuNgay:dd/MM/yyyy} đến {DenNgay.AddDays(-1):dd/MM/yyyy}";
                lblTongDT.Text = $"Tổng doanh thu toàn kỳ: {totalRevenue:N0} VND";
                lblTongHD.Text = $"Tổng số hóa đơn: {totalOrders} hóa đơn";
                lblTongCa.Text = $"Tổng số ca làm: 2 ca";
                lblNhanXet.Text = GenerateAnalysis(morning.Revenue, evening.Revenue, totalRevenue);

                Logger.Debug($"Hiển thị tổng hợp: {totalOrders} đơn, {totalRevenue:N0}đ");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi hiển thị tổng hợp báo cáo", ex);
                throw;
            }
        }

        private void DisplayShiftDetails(ShiftData morning, ShiftData evening)
        {
            if (morning == null)
            {
                throw new ArgumentNullException(nameof(morning));
            }

            if (evening == null)
            {
                throw new ArgumentNullException(nameof(evening));
            }

            try
            {
                // Grid
                dgvCa.Rows.Clear();
                dgvCa.Rows.Add("1", "Sáng", "06h00 - 14h00", morning.OrderCount, $"{morning.Revenue:N0}");
                dgvCa.Rows.Add("2", "Tối", "14h00 - 22h00", evening.OrderCount, $"{evening.Revenue:N0}");

                // Morning shift
                lblDTSang.Text = $"Tổng doanh thu: {morning.Revenue:N0} VND";
                lblHDSang.Text = $"Số hóa đơn: {morning.OrderCount}";
                lblMonSang.Text = FormatBestSellingItem(morning);

                // Evening shift
                lblDTToi.Text = $"Tổng doanh thu: {evening.Revenue:N0} VND";
                lblHDToi.Text = $"Số hóa đơn: {evening.OrderCount}";
                lblMonToi.Text = FormatBestSellingItem(evening);

                Logger.Debug("Hiển thị chi tiết ca làm việc thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi hiển thị chi tiết ca", ex);
                throw;
            }
        }

        private string FormatBestSellingItem(ShiftData data)
        {
            try
            {
                if (data == null)
                {
                    return NO_SALES_MESSAGE;
                }

                return data.BestSellingQuantity > 0
                    ? $"Món bán chạy: {data.BestSellingItem} ({data.BestSellingQuantity} phần)"
                    : NO_SALES_MESSAGE;
            }
            catch
            {
                Logger.Warn("Lỗi khi format món bán chạy");
                return NO_SALES_MESSAGE;
            }
        }

        private string GenerateAnalysis(decimal morningRevenue, decimal eveningRevenue, decimal totalRevenue)
        {
            try
            {
                if (totalRevenue == 0)
                {
                    Logger.Warn("Tổng doanh thu bằng 0");
                    return "Nhận xét:\nChưa có doanh thu trong kỳ.";
                }

                var analysis = "Nhận xét:\n";

                if (eveningRevenue >= morningRevenue)
                {
                    var percentage = (eveningRevenue / totalRevenue * 100);
                    analysis += $"Ca tối có doanh thu cao hơn, chiếm {percentage:N1}% tổng doanh thu.\n";
                }
                else
                {
                    var percentage = (morningRevenue / totalRevenue * 100);
                    analysis += $"Ca sáng có doanh thu cao hơn, chiếm {percentage:N1}% tổng doanh thu.\n";
                }

                analysis += "Khuyến khích đẩy mạnh món bán chạy vào khung giờ cao điểm.";
                return analysis;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tạo phân tích", ex);
                return "Nhận xét:\nKhông thể tạo phân tích.";
            }
        }
        #endregion

        #region Chart Methods
        private void LoadBieuDo8Ca()
        {
            try
            {
                Logger.Info("Tải biểu đồ 8 ca");

                InitializeChart();
                var series = CreateChartSeries();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    PopulateChartData(series, connection);
                }

                chartIn.Series.Add(series);
                Logger.Info("Tải biểu đồ thành công");
            }
            catch (SqlException sqlEx)
            {
                Logger.Error("Lỗi SQL khi tải biểu đồ", sqlEx);
                throw new DatabaseConnectionException("Lỗi tải dữ liệu biểu đồ", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải biểu đồ", ex);
                throw;
            }
        }

        private void InitializeChart()
        {
            try
            {
                if (chartIn == null)
                {
                    throw new InvalidOperationException("Chart control không tồn tại");
                }

                chartIn.Series.Clear();
                chartIn.ChartAreas.Clear();
                chartIn.Titles.Clear();

                var area = new ChartArea
                {
                    AxisX = { MajorGrid = { Enabled = false } },
                    AxisY = { MajorGrid = { LineColor = Color.LightGray } }
                };
                chartIn.ChartAreas.Add(area);

                var title = chartIn.Titles.Add("Biểu đồ số hóa đơn theo ca 2h");
                title.Font = new DrawingFont("Segoe UI", 14F, FontStyle.Bold);

                Logger.Debug("Khởi tạo biểu đồ thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi khởi tạo biểu đồ", ex);
                throw;
            }
        }

        private Series CreateChartSeries()
        {
            try
            {
                return new Series("Số đơn")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true,
                    Font = new DrawingFont("Segoe UI", 11F, FontStyle.Bold),
                    LabelForeColor = Color.Black
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tạo series biểu đồ", ex);
                throw;
            }
        }

        private void PopulateChartData(Series series, SqlConnection connection)
        {
            if (series == null)
            {
                throw new ArgumentNullException(nameof(series));
            }

            if (connection == null || connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Kết nối database không hợp lệ");
            }

            var shiftLabels = new[] { "6h-8h", "8h-10h", "10h-12h", "12h-14h",
                                      "14h-16h", "16h-18h", "18h-20h", "20h-22h" };
            var colors = new[] {
                Color.CornflowerBlue, Color.Orange, Color.MediumPurple,
                Color.LimeGreen, Color.Tomato, Color.HotPink,
                Color.SaddleBrown, Color.Goldenrod
            };

            try
            {
                for (int i = 0; i < TOTAL_SHIFTS; i++)
                {
                    var orderCount = GetOrderCountForTimeSlot(connection, MORNING_SHIFT_START + i * SHIFT_INTERVAL);
                    AddChartPoint(series, shiftLabels[i], orderCount, colors[i]);
                }

                Logger.Debug($"Đã thêm {TOTAL_SHIFTS} điểm dữ liệu vào biểu đồ");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi điền dữ liệu biểu đồ", ex);
                throw;
            }
        }

        private int GetOrderCountForTimeSlot(SqlConnection connection, int startHour)
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM HoaDon
                WHERE TrangThai = N'Đã thanh toán'
                  AND NgayTao >= @tu 
                  AND NgayTao < @den
                  AND DATEPART(HOUR, NgayTao) >= @gio 
                  AND DATEPART(HOUR, NgayTao) < @gio + @interval";

            try
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    command.CommandTimeout = 30;
                    command.Parameters.AddWithValue("@tu", TuNgay.Date);
                    command.Parameters.AddWithValue("@den", DenNgay.Date);
                    command.Parameters.AddWithValue("@gio", startHour);
                    command.Parameters.AddWithValue("@interval", SHIFT_INTERVAL);

                    object result = command.ExecuteScalar();
                    return SafeConvertToInt32(result);
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"Lỗi SQL khi lấy số đơn cho ca {startHour}h", sqlEx);
                throw;
            }
        }

        private void AddChartPoint(Series series, string label, int value, Color color)
        {
            try
            {
                if (series == null)
                {
                    throw new ArgumentNullException(nameof(series));
                }

                var point = series.Points.Add(value);
                point.AxisLabel = label;
                point.Color = color;
                point.Label = value > 0 ? value.ToString() : "";
            }
            catch
            {
                Logger.Warn($"Lỗi khi thêm điểm biểu đồ {label}");
            }
        }
        #endregion

        #region PDF Export
        private void BtnXuatPDF_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = null;
            iTextSharp.text.Document document = null;
            FileStream fileStream = null;
            PdfWriter writer = null;

            try
            {
                Logger.Info("Bắt đầu xuất PDF");

                HideExportButton();

                var fileName = GetSaveFileName();
                if (string.IsNullOrEmpty(fileName))
                {
                    Logger.Info("Người dùng hủy xuất PDF");
                    return;
                }

                Logger.Info($"Xuất PDF: {fileName}");

                bitmap = CapturePanel();

                if (bitmap == null)
                {
                    throw new PdfExportException("Không thể capture panel", null);
                }

                document = new iTextSharp.text.Document(PageSize.A4, 10, 10, 10, 10);
                fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                writer = PdfWriter.GetInstance(document, fileStream);

                document.Open();

                var pdfImage = ConvertBitmapToPdfImage(bitmap);

                if (pdfImage == null)
                {
                    throw new PdfExportException("Không thể chuyển đổi hình ảnh", null);
                }

                pdfImage.ScaleToFit(document.PageSize.Width - 20, document.PageSize.Height - 20);
                pdfImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                document.Add(pdfImage);

                Logger.Info("Xuất PDF thành công");
                ShowSuccessMessage(fileName);
                PromptToOpenFile(fileName);
            }
            catch (IOException ioEx)
            {
                Logger.Error("Lỗi I/O khi xuất PDF", ioEx);
                ShowErrorMessage("Lỗi xuất file",
                    "Không thể ghi file. Vui lòng kiểm tra:\n" +
                    "- File có đang mở không?\n" +
                    "- Có quyền ghi vào thư mục không?");
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Logger.Error("Không có quyền truy cập file", uaEx);
                ShowErrorMessage("Lỗi phân quyền", "Không có quyền ghi file vào thư mục này.");
            }
            catch (PdfExportException pdfEx)
            {
                Logger.Error("Lỗi xuất PDF", pdfEx);
                ShowErrorMessage("Lỗi xuất PDF", pdfEx.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không xác định khi xuất PDF", ex);
                ShowErrorMessage("Lỗi", "Không thể xuất PDF. Vui lòng thử lại.");
            }
            finally
            {
                try
                {
                    document?.Close();
                    writer?.Close();
                    fileStream?.Dispose();
                    bitmap?.Dispose();
                    ShowExportButton();

                    Logger.Debug("Đã giải phóng tài nguyên PDF");
                }
                catch (Exception cleanupEx)
                {
                    Logger.Error("Lỗi khi dọn dẹp tài nguyên", cleanupEx);
                }
            }
        }

        private void HideExportButton()
        {
            try
            {
                btnXuatPDF.Visible = false;
                this.Refresh();
                Application.DoEvents();
                System.Threading.Thread.Sleep(PANEL_CAPTURE_DELAY);
            }
            catch
            {
                Logger.Warn("Lỗi khi ẩn nút xuất PDF");
            }
        }

        private void ShowExportButton()
        {
            try
            {
                btnXuatPDF.Visible = true;
            }
            catch
            {
                Logger.Warn("Lỗi khi hiện nút xuất PDF");
            }
        }

        private string GetSaveFileName()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                try
                {
                    saveDialog.Filter = "PDF File|*.pdf";
                    saveDialog.FileName = $"MIUCOFFEE_BaoCao_{TuNgay:ddMMyyyy}_den_{DenNgay.AddDays(-1):ddMMyyyy}.pdf";
                    saveDialog.DefaultExt = "pdf";
                    saveDialog.AddExtension = true;

                    return saveDialog.ShowDialog() == DialogResult.OK ? saveDialog.FileName : null;
                }
                catch (Exception ex)
                {
                    Logger.Error("Lỗi khi mở hộp thoại lưu file", ex);
                    return null;
                }
            }
        }

        private Bitmap CapturePanel()
        {
            try
            {
                if (panelMain == null)
                {
                    throw new InvalidOperationException("Panel không tồn tại");
                }

                var bitmap = new Bitmap(panelMain.Width, panelMain.Height);
                panelMain.DrawToBitmap(bitmap, new DrawingRectangle(0, 0, panelMain.Width, panelMain.Height));

                Logger.Debug($"Đã capture panel: {panelMain.Width}x{panelMain.Height}");
                return bitmap;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi capture panel", ex);
                throw;
            }
        }

        private iTextSharp.text.Image ConvertBitmapToPdfImage(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            MemoryStream memoryStream = null;

            try
            {
                memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;

                var pdfImage = iTextSharp.text.Image.GetInstance(memoryStream.ToArray());
                Logger.Debug("Chuyển đổi bitmap sang PDF image thành công");

                return pdfImage;
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi chuyển đổi bitmap sang PDF image", ex);
                throw;
            }
            finally
            {
                memoryStream?.Dispose();
            }
        }

        private void ShowSuccessMessage(string fileName)
        {
            try
            {
                MessageBox.Show(
                    $"✅ Xuất PDF thành công!\n\n{fileName}",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch
            {
                Logger.Warn("Lỗi khi hiển thị thông báo thành công");
            }
        }

        private void PromptToOpenFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            try
            {
                var result = MessageBox.Show(
                    "Bạn có muốn mở file PDF?",
                    "Mở file",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (!File.Exists(fileName))
                    {
                        Logger.Warn($"File không tồn tại: {fileName}");
                        MessageBox.Show("File không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Process.Start(fileName);
                    Logger.Info($"Đã mở file PDF: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi mở file PDF", ex);
                MessageBox.Show(
                    "Không thể mở file PDF. Vui lòng mở thủ công.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Helper Methods
        private void ShowErrorMessage(string title, string message)
        {
            try
            {
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                Logger.Error("Lỗi khi hiển thị thông báo lỗi");
            }
        }

        private int SafeGetInt32(SqlDataReader reader, int ordinal)
        {
            try
            {
                if (reader == null)
                {
                    return 0;
                }

                if (reader.IsDBNull(ordinal))
                {
                    return 0;
                }

                return reader.GetInt32(ordinal);
            }
            catch
            {
                Logger.Warn($"Lỗi khi đọc Int32 tại cột {ordinal}");
                return 0;
            }
        }

        private decimal SafeGetDecimal(SqlDataReader reader, int ordinal)
        {
            try
            {
                if (reader == null)
                {
                    return 0m;
                }

                if (reader.IsDBNull(ordinal))
                {
                    return 0m;
                }

                return reader.GetDecimal(ordinal);
            }
            catch
            {
                Logger.Warn($"Lỗi khi đọc Decimal tại cột {ordinal}");
                return 0m;
            }
        }

        private string SafeGetString(SqlDataReader reader, int ordinal)
        {
            try
            {
                if (reader == null)
                {
                    return string.Empty;
                }

                if (reader.IsDBNull(ordinal))
                {
                    return string.Empty;
                }

                return reader.GetString(ordinal);
            }
            catch
            {
                Logger.Warn($"Lỗi khi đọc String tại cột {ordinal}");
                return string.Empty;
            }
        }

        private int SafeConvertToInt32(object value)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                {
                    return 0;
                }

                return Convert.ToInt32(value);
            }
            catch
            {
                Logger.Warn($"Lỗi khi chuyển đổi '{value}' sang Int32");
                return 0;
            }
        }
        #endregion
    }
}