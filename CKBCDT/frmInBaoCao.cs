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
using System.Text;
using DrawingFont = System.Drawing.Font;
using DrawingRectangle = System.Drawing.Rectangle;
using System.Drawing.Printing;

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
        #endregion

        #region Properties
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }

        private readonly string _connectionString =
            @"Server=NGUYENNHI2407\SQLEXPRESS;Database=QuanLyQuanCafe2;Integrated Security=True;";
        #endregion

        #region Data Models
        private class ShiftData
        {
            public int OrderCount { get; set; }
            public decimal Revenue { get; set; }
            public string BestSellingItem { get; set; } = NO_SALES_MESSAGE;
            public int BestSellingQuantity { get; set; }
        }
        #endregion

        #region Constructor & Initialization
        public frmInBaoCao()
        {
            try
            {
                Logger.LogInfo("Initializing frmInBaoCao");
                InitializeComponent();
                this.Load += FrmInBaoCao_Load;
                this.btnXuatPDF.Click += BtnXuatPDF_Click;
                Logger.LogInfo("frmInBaoCao initialized successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to initialize frmInBaoCao", ex);
                throw;
            }
        }

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

        #region Logger Service
        public static class Logger
        {
            private static readonly string LogFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Logs",
                $"MiuCoffee_{DateTime.Now:yyyyMMdd}.log"
            );

            static Logger()
            {
                try
                {
                    var logDir = Path.GetDirectoryName(LogFilePath);
                    if (!Directory.Exists(logDir))
                    {
                        Directory.CreateDirectory(logDir);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Cannot create log directory: {ex.Message}");
                }
            }

            public static void LogInfo(string message)
            {
                Log("INFO", message);
            }

            public static void LogWarning(string message)
            {
                Log("WARNING", message);
            }

            public static void LogError(string message, Exception ex = null)
            {
                var logMessage = message;
                if (ex != null)
                {
                    logMessage += $"\nException: {ex.GetType().Name}\nMessage: {ex.Message}\nStackTrace: {ex.StackTrace}";
                }
                Log("ERROR", logMessage);
            }

            private static void Log(string level, string message)
            {
                try
                {
                    var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}";
                    File.AppendAllText(LogFilePath, logEntry, Encoding.UTF8);
                    Debug.WriteLine(logEntry);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Logging failed: {ex.Message}");
                }
            }
        }
        #endregion



        private void FrmInBaoCao_Load(object sender, EventArgs e)
        {
            try
            {
                Logger.LogInfo($"Loading report: From {TuNgay:yyyy-MM-dd} to {DenNgay:yyyy-MM-dd}");

                ValidateDateRange();
                this.Text = TITLE;
                LoadReportData();

                Logger.LogInfo("Report loaded successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to load report", ex);
                ShowErrorMessage("Không thể tải báo cáo", ex.Message);
            }
        }
        #endregion

        #region Validation
        private void ValidateDateRange()
        {
            if (TuNgay == DateTime.MinValue || DenNgay == DateTime.MinValue)
            {
                var ex = new ArgumentException("Ngày bắt đầu và ngày kết thúc không được để trống");
                Logger.LogError("Invalid date range", ex);
                throw ex;
            }

            if (DenNgay <= TuNgay)
            {
                var ex = new ArgumentException("Ngày kết thúc phải lớn hơn ngày bắt đầu");
                Logger.LogError("Invalid date range", ex);
                throw ex;
            }

            Logger.LogInfo($"Date range validated: {TuNgay:yyyy-MM-dd} to {DenNgay:yyyy-MM-dd}");
        }
        #endregion

        #region Data Loading
        private void LoadReportData()
        {
            ShiftData morningData = null;
            ShiftData eveningData = null;
            SqlConnection connection = null;

            try
            {
                Logger.LogInfo("Starting data load");

                connection = new SqlConnection(_connectionString);
                connection.Open();
                Logger.LogInfo("Database connection opened");

                morningData = LoadShiftData(connection, MORNING_SHIFT_START, MORNING_SHIFT_END, "Morning");
                eveningData = LoadShiftData(connection, EVENING_SHIFT_START, EVENING_SHIFT_END, "Evening");

                DisplayReportSummary(morningData, eveningData);
                DisplayShiftDetails(morningData, eveningData);
                LoadBieuDo8Ca();

                Logger.LogInfo("Data loaded successfully");
            }
            catch (SqlException sqlEx)
            {
                Logger.LogError("Database error while loading report data", sqlEx);
                throw new DatabaseConnectionException("Lỗi kết nối cơ sở dữ liệu", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading report data", ex);
                throw new DataLoadException("Lỗi tải dữ liệu báo cáo", ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Logger.LogInfo("Database connection closed");
                }
            }
        }

        private ShiftData LoadShiftData(SqlConnection connection, int startHour, int endHour, string shiftName)
        {
            var data = new ShiftData();

            try
            {
                Logger.LogInfo($"Loading {shiftName} shift data ({startHour}h-{endHour}h)");

                LoadShiftRevenue(connection, startHour, endHour, data);
                LoadBestSellingItem(connection, startHour, endHour, data);

                Logger.LogInfo($"{shiftName} shift: {data.OrderCount} orders, {data.Revenue:N0} VND");
                return data;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading {shiftName} shift data", ex);
                throw;
            }
        }

        private void LoadShiftRevenue(SqlConnection connection, int startHour, int endHour, ShiftData data)
        {
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

            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@tu", TuNgay.Date);
                command.Parameters.AddWithValue("@den", DenNgay.Date);
                command.Parameters.AddWithValue("@startHour", startHour);
                command.Parameters.AddWithValue("@endHour", endHour);

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.OrderCount = reader.GetInt32(0);
                    data.Revenue = reader.GetDecimal(1);
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.LogError($"SQL error loading shift revenue ({startHour}h-{endHour}h)", sqlEx);
                throw;
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
            }
        }

        private void LoadBestSellingItem(SqlConnection connection, int startHour, int endHour, ShiftData data)
        {
            const string sql = @"
                SELECT TOP 1 
                    m.TenMon, 
                    SUM(ct.SoLuong) AS SL
                FROM ChiTietHoaDon ct
                JOIN HoaDon hd ON ct.HoaDonId = hd.Id
                JOIN Mon m ON ct.MonId = m.Id
                WHERE hd.TrangThai = N'Đã thanh toán'
                  AND hd.NgayTao >= @tu 
                  AND hd.NgayTao < @den
                  AND DATEPART(HOUR, hd.NgayTao) >= @startHour 
                  AND DATEPART(HOUR, hd.NgayTao) < @endHour
                GROUP BY m.TenMon
                ORDER BY SUM(ct.SoLuong) DESC";

            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@tu", TuNgay.Date);
                command.Parameters.AddWithValue("@den", DenNgay.Date);
                command.Parameters.AddWithValue("@startHour", startHour);
                command.Parameters.AddWithValue("@endHour", endHour);

                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.BestSellingItem = reader.GetString(0);
                    data.BestSellingQuantity = reader.GetInt32(1);
                    Logger.LogInfo($"Best selling item ({startHour}h-{endHour}h): {data.BestSellingItem} - {data.BestSellingQuantity} portions");
                }
            }
            catch (SqlException sqlEx)
            {
                Logger.LogError($"SQL error loading best selling item ({startHour}h-{endHour}h)", sqlEx);
                throw;
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
            }
        }
        #endregion

        #region Display Methods
        private void DisplayReportSummary(ShiftData morning, ShiftData evening)
        {
            try
            {
                var totalRevenue = morning.Revenue + evening.Revenue;
                var totalOrders = morning.OrderCount + evening.OrderCount;

                lblThoiGian.Text = $"Từ ngày {TuNgay:dd/MM/yyyy} đến {DenNgay.AddDays(-1):dd/MM/yyyy}";
                lblTongDT.Text = $"Tổng doanh thu toàn kỳ: {totalRevenue:N0} VND";
                lblTongHD.Text = $"Tổng số hóa đơn: {totalOrders} hóa đơn";
                lblTongCa.Text = $"Tổng số ca làm: {totalOrders} ca";
                lblNhanXet.Text = GenerateAnalysis(morning.Revenue, evening.Revenue, totalRevenue);

                Logger.LogInfo($"Summary displayed: {totalOrders} orders, {totalRevenue:N0} VND");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error displaying report summary", ex);
                throw;
            }
        }

        private void DisplayShiftDetails(ShiftData morning, ShiftData evening)
        {
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

                Logger.LogInfo("Shift details displayed");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error displaying shift details", ex);
                throw;
            }
        }

        private string FormatBestSellingItem(ShiftData data)
        {
            return data.BestSellingQuantity > 0
                ? $"Món bán chạy: {data.BestSellingItem} ({data.BestSellingQuantity} phần)"
                : NO_SALES_MESSAGE;
        }

        private string GenerateAnalysis(decimal morningRevenue, decimal eveningRevenue, decimal totalRevenue)
        {
            if (totalRevenue == 0)
                return "Nhận xét:\nChưa có doanh thu trong kỳ.";

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
        #endregion

        #region Chart Methods
        private void LoadBieuDo8Ca()
        {
            SqlConnection connection = null;

            try
            {
                Logger.LogInfo("Loading 8-shift chart");

                InitializeChart();
                var series = CreateChartSeries();

                connection = new SqlConnection(_connectionString);
                connection.Open();

                PopulateChartData(series, connection);
                chartIn.Series.Add(series);

                Logger.LogInfo("Chart loaded successfully");
            }
            catch (SqlException sqlEx)
            {
                Logger.LogError("Database error loading chart", sqlEx);
                throw new DatabaseConnectionException("Lỗi tải dữ liệu biểu đồ", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading chart", ex);
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void InitializeChart()
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                Logger.LogError("Error initializing chart", ex);
                throw;
            }
        }

        private Series CreateChartSeries()
        {
            return new Series("Số đơn")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Font = new DrawingFont("Segoe UI", 11F, FontStyle.Bold),
                LabelForeColor = Color.Black
            };
        }

        private void PopulateChartData(Series series, SqlConnection connection)
        {
            var shiftLabels = new[] { "6h-8h", "8h-10h", "10h-12h", "12h-14h",
                                      "14h-16h", "16h-18h", "18h-20h", "20h-22h" };
            var colors = new[] { Color.CornflowerBlue, Color.Orange, Color.MediumPurple,
                                Color.LimeGreen, Color.Tomato, Color.HotPink,
                                Color.SaddleBrown, Color.Goldenrod };

            try
            {
                for (int i = 0; i < TOTAL_SHIFTS; i++)
                {
                    var orderCount = GetOrderCountForTimeSlot(connection, MORNING_SHIFT_START + i * SHIFT_INTERVAL);
                    AddChartPoint(series, shiftLabels[i], orderCount, colors[i]);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error populating chart data", ex);
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

            SqlCommand command = null;

            try
            {
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@tu", TuNgay.Date);
                command.Parameters.AddWithValue("@den", DenNgay.Date);
                command.Parameters.AddWithValue("@gio", startHour);
                command.Parameters.AddWithValue("@interval", SHIFT_INTERVAL);

                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (SqlException sqlEx)
            {
                Logger.LogError($"SQL error getting order count for {startHour}h", sqlEx);
                throw;
            }
            finally
            {
                command?.Dispose();
            }
        }

        private void AddChartPoint(Series series, string label, int value, Color color)
        {
            var point = series.Points.Add(value);
            point.AxisLabel = label;
            point.Color = color;
            point.Label = value > 0 ? value.ToString() : "";
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
                Logger.LogInfo("Starting PDF export");

                HideExportButton();

                var fileName = GetSaveFileName();
                if (string.IsNullOrEmpty(fileName))
                {
                    Logger.LogInfo("PDF export cancelled by user");
                    return;
                }

                Logger.LogInfo($"Exporting to: {fileName}");

                bitmap = CapturePanel();
                document = new iTextSharp.text.Document(PageSize.A4, 10, 10, 10, 10);
                fileStream = new FileStream(fileName, FileMode.Create);
                writer = PdfWriter.GetInstance(document, fileStream);

                document.Open();

                var pdfImage = ConvertBitmapToPdfImage(bitmap);
                pdfImage.ScaleToFit(document.PageSize.Width - 20, document.PageSize.Height - 20);
                pdfImage.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

                document.Add(pdfImage);

                Logger.LogInfo("PDF exported successfully");

                ShowSuccessMessage(fileName);
                PromptToOpenFile(fileName);
            }
            catch (IOException ioEx)
            {
                Logger.LogError("File IO error during PDF export", ioEx);
                ShowErrorMessage("Lỗi xuất file PDF", "Không thể ghi file. Vui lòng kiểm tra:\n- File có đang mở không?\n- Có quyền ghi vào thư mục không?");
            }
            catch (Exception ex)
            {
                Logger.LogError("Error exporting PDF", ex);
                throw new PdfExportException("Lỗi xuất PDF", ex);
            }
            finally
            {
                // Cleanup resources
                try
                {
                    document?.Close();
                    writer?.Close();
                    fileStream?.Close();
                    bitmap?.Dispose();

                    ShowExportButton();
                    Logger.LogInfo("PDF export resources cleaned up");
                }
                catch (Exception cleanupEx)
                {
                    Logger.LogError("Error during cleanup", cleanupEx);
                }
            }
        }

        private void HideExportButton()
        {
            btnXuatPDF.Visible = false;
            this.Refresh();
            Application.DoEvents();
            System.Threading.Thread.Sleep(200);
        }

        private void ShowExportButton()
        {
            btnXuatPDF.Visible = true;
        }

        private string GetSaveFileName()
        {
            SaveFileDialog saveDialog = null;

            try
            {
                saveDialog = new SaveFileDialog
                {
                    Filter = "PDF File|*.pdf",
                    FileName = $"MIUCOFFEE_BaoCao_{TuNgay:ddMMyyyy}_den_{DenNgay.AddDays(-1):ddMMyyyy}.pdf"
                };

                return saveDialog.ShowDialog() == DialogResult.OK ? saveDialog.FileName : null;
            }
            finally
            {
                saveDialog?.Dispose();
            }
        }

        private Bitmap CapturePanel()
        {
            try
            {
                var bitmap = new Bitmap(panelMain.Width, panelMain.Height);
                panelMain.DrawToBitmap(bitmap, new DrawingRectangle(0, 0, panelMain.Width, panelMain.Height));
                return bitmap;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error capturing panel", ex);
                throw;
            }
        }

        private iTextSharp.text.Image ConvertBitmapToPdfImage(Bitmap bitmap)
        {
            MemoryStream memoryStream = null;

            try
            {
                memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;
                return iTextSharp.text.Image.GetInstance(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                Logger.LogError("Error converting bitmap to PDF image", ex);
                throw;
            }
            finally
            {
                memoryStream?.Close();
            }
        }

        private void ShowSuccessMessage(string fileName)
        {
            MessageBox.Show($"✅ Xuất PDF thành công!\n\n{fileName}",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PromptToOpenFile(string fileName)
        {
            try
            {
                var result = MessageBox.Show("Bạn có muốn mở file PDF?", "Mở file",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Process.Start(fileName);
                    Logger.LogInfo($"Opened PDF file: {fileName}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error opening PDF file", ex);
                MessageBox.Show("Không thể mở file PDF. Vui lòng mở thủ công.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Helper Methods
        private void ShowErrorMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}

