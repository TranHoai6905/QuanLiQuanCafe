using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace CKBCDT
{
    public partial class frmBaoCaoDoanhThu : Form
    {
        private readonly ReportService _reportService;
        private readonly ChartConfigService _chartConfigService;
        private readonly DataGridConfigService _gridConfigService;
        private DateTime _tuNgay;
        private DateTime _denNgay;

        public frmBaoCaoDoanhThu()
        {
            try
            {
                Logger.Info("Khởi tạo form Báo cáo Doanh thu");
                InitializeComponent();

                string connectionString = @"Server=NGUYENNHI2407\SQLEXPRESS;Database=QuanLyQuanCafe2;Integrated Security=true;";

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new ArgumentException("Connection string không được để trống");
                }

                _reportService = new ReportService(connectionString);
                _chartConfigService = new ChartConfigService();
                _gridConfigService = new DataGridConfigService();

                InitializeEventHandlers();
                _gridConfigService.ConfigureDataGridView(dgvHoaDon);
                LoadDefaultReport();

                Logger.Info("Khởi tạo form Báo cáo Doanh thu thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khởi tạo form Báo cáo Doanh thu", ex);
                MessageBox.Show(
                    "Không thể khởi tạo form báo cáo. Vui lòng kiểm tra kết nối cơ sở dữ liệu.",
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
                btnNgay.Click += BtnNgay_Click;
                btnThang.Click += BtnThang_Click;
                btnNam.Click += BtnNam_Click;
                btnRefresh.Click += (s, e) => BtnNgay_Click(s, e);
                btnInBaoCao.Click += BtnInBaoCao_Click;

                Logger.Debug("Đăng ký event handlers thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi đăng ký event handlers", ex);
                throw new InvalidOperationException("Không thể đăng ký event handlers", ex);
            }
        }

        private void LoadDefaultReport()
        {
            try
            {
                Logger.Info("Tải báo cáo mặc định");
                _tuNgay = DateTime.Today;
                _denNgay = _tuNgay.AddDays(1);
                LoadBaoCao($"Ngày {_tuNgay:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải báo cáo mặc định", ex);
                MessageBox.Show(
                    "Không thể tải báo cáo mặc định. Vui lòng thử lại.",
                    "Lỗi tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        #region Event Handlers
        private void BtnNgay_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Người dùng chọn báo cáo theo ngày");
                _tuNgay = DateTime.Today;
                _denNgay = _tuNgay.AddDays(1);
                LoadBaoCao($"Ngày {_tuNgay:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải báo cáo theo ngày", ex);
                ShowErrorMessage("Không thể tải báo cáo theo ngày");
            }
        }

        private void BtnThang_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Người dùng chọn báo cáo theo tháng");
                _tuNgay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                _denNgay = _tuNgay.AddMonths(1);
                LoadBaoCao($"Tháng {_tuNgay:MM/yyyy}");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải báo cáo theo tháng", ex);
                ShowErrorMessage("Không thể tải báo cáo theo tháng");
            }
        }

        private void BtnNam_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Người dùng chọn báo cáo theo năm");
                _tuNgay = new DateTime(DateTime.Today.Year, 1, 1);
                _denNgay = _tuNgay.AddYears(1);
                LoadBaoCao($"Năm {_tuNgay:yyyy}");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải báo cáo theo năm", ex);
                ShowErrorMessage("Không thể tải báo cáo theo năm");
            }
        }

        private void BtnInBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info("Người dùng mở form in báo cáo");
                var tuNgay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var denNgay = tuNgay.AddMonths(1);

                var f = new frmInBaoCao
                {
                    TuNgay = tuNgay,
                    DenNgay = denNgay
                };
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi mở form in báo cáo", ex);
                ShowErrorMessage("Không thể mở form in báo cáo");
            }
        }
        #endregion

        private void LoadBaoCao(string tieuDe)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Logger.Info($"Bắt đầu tải báo cáo: {tieuDe}");

                if (string.IsNullOrWhiteSpace(tieuDe))
                {
                    throw new ArgumentException("Tiêu đề báo cáo không được để trống", nameof(tieuDe));
                }

                if (_tuNgay >= _denNgay)
                {
                    throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
                }

                this.Text = $"Báo cáo doanh thu - {tieuDe}";
                var dateRange = new DateRange(_tuNgay, _denNgay);

                LoadTongHop(dateRange);
                LoadDoanhThuTheoCa(dateRange);
                LoadMonBanChay(dateRange);
                LoadTyLeMon(dateRange);
                LoadDanhSachHD(dateRange);

                Logger.Info($"Tải báo cáo thành công: {tieuDe}");
            }
            catch (ArgumentException argEx)
            {
                Logger.Warn($"Dữ liệu đầu vào không hợp lệ: {argEx.Message}");
                ShowErrorMessage($"Dữ liệu không hợp lệ: {argEx.Message}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Lỗi khi tải báo cáo: {tieuDe}", ex);
                ShowErrorMessage("Không thể tải báo cáo. Vui lòng thử lại.");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadTongHop(DateRange dateRange)
        {
            try
            {
                ValidateDateRange(dateRange);
                var summary = _reportService.GetRevenueSummary(dateRange);

                if (summary == null)
                {
                    throw new InvalidOperationException("Không thể lấy dữ liệu tổng hợp");
                }

                lblTongHD.Text = summary.TotalOrders.ToString();
                lblTongDT.Text = $"{summary.TotalRevenue:N0} đ";

                Logger.Debug($"Tải tổng hợp: {summary.TotalOrders} đơn, {summary.TotalRevenue:N0}đ");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải dữ liệu tổng hợp", ex);
                lblTongHD.Text = "0";
                lblTongDT.Text = "0 đ";
                throw;
            }
        }

        private void LoadDoanhThuTheoCa(DateRange dateRange)
        {
            try
            {
                ValidateDateRange(dateRange);
                var revenueByShift = _reportService.GetRevenueByShift(dateRange);

                if (revenueByShift == null || revenueByShift.Count == 0)
                {
                    Logger.Warn("Không có dữ liệu doanh thu theo ca");
                    revenueByShift = new List<ShiftRevenue>();
                }

                _chartConfigService.ConfigureShiftChart(chartDoanhThuCa, revenueByShift);
                Logger.Debug($"Tải doanh thu theo ca: {revenueByShift.Count} ca");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải doanh thu theo ca", ex);
                throw;
            }
        }

        private void LoadMonBanChay(DateRange dateRange)
        {
            try
            {
                ValidateDateRange(dateRange);
                var topItems = _reportService.GetTopSellingItems(dateRange, 5);

                if (topItems == null)
                {
                    throw new InvalidOperationException("Không thể lấy dữ liệu món bán chạy");
                }

                _chartConfigService.ConfigureTopSellingChart(chartMonBanChay, topItems);
                Logger.Debug($"Tải món bán chạy: {topItems.Count} món");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải món bán chạy", ex);
                throw;
            }
        }

        private void LoadTyLeMon(DateRange dateRange)
        {
            try
            {
                ValidateDateRange(dateRange);
                var categoryRatio = _reportService.GetCategoryRatio(dateRange);

                if (categoryRatio == null)
                {
                    Logger.Warn("Không có dữ liệu tỷ lệ món");
                    categoryRatio = new List<CategoryRatio>();
                }

                _chartConfigService.ConfigurePieChart(chartTyLe, categoryRatio);
                Logger.Debug($"Tải tỷ lệ món: {categoryRatio.Count} loại");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải tỷ lệ món", ex);
                throw;
            }
        }

        private void LoadDanhSachHD(DateRange dateRange)
        {
            try
            {
                ValidateDateRange(dateRange);
                var orders = _reportService.GetOrderList(dateRange);

                if (orders == null)
                {
                    throw new InvalidOperationException("Không thể lấy danh sách hóa đơn");
                }

                dgvHoaDon.DataSource = orders;
                Logger.Debug($"Tải danh sách hóa đơn: {orders.Rows.Count} hóa đơn");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi tải danh sách hóa đơn", ex);
                dgvHoaDon.DataSource = null;
                throw;
            }
        }

        private void ValidateDateRange(DateRange dateRange)
        {
            if (dateRange == null)
            {
                throw new ArgumentNullException(nameof(dateRange), "DateRange không được null");
            }

            if (dateRange.StartDate >= dateRange.EndDate)
            {
                throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
            }

            if (dateRange.StartDate > DateTime.Now)
            {
                throw new ArgumentException("Ngày bắt đầu không thể trong tương lai");
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(
                message,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    #region Models
    public class DateRange
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        public override string ToString()
        {
            return $"{StartDate:dd/MM/yyyy} - {EndDate:dd/MM/yyyy}";
        }
    }

    public class RevenueSummary
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }

        public override string ToString()
        {
            return $"Đơn: {TotalOrders}, Doanh thu: {TotalRevenue:N0}đ";
        }
    }

    public class ShiftRevenue
    {
        public string TimeRange { get; set; }
        public int OrderCount { get; set; }
        public Color Color { get; set; }
    }

    public class TopSellingItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }

    public class CategoryRatio
    {
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
    }
    #endregion

    #region Services
    public class ReportService
    {
        private readonly string _connectionString;
        private const int CommandTimeout = 30;

        public ReportService(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Connection string không được để trống", nameof(connectionString));
            }

            _connectionString = connectionString;
            Logger.Info("Khởi tạo ReportService");
        }

        public RevenueSummary GetRevenueSummary(DateRange dateRange)
        {
            ValidateDateRange(dateRange);

            const string sql = @"
                SELECT COUNT(*) AS SoHD, ISNULL(SUM(TongTien), 0) AS DT
                FROM HoaDon
                WHERE TrangThai = N'Đã thanh toán'
                  AND NgayTao >= @tu AND NgayTao < @den";

            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                Logger.Debug($"Lấy tổng hợp doanh thu: {dateRange}");

                connection = new SqlConnection(_connectionString);
                command = new SqlCommand(sql, connection);
                command.CommandTimeout = CommandTimeout;
                command.Parameters.AddWithValue("@tu", dateRange.StartDate);
                command.Parameters.AddWithValue("@den", dateRange.EndDate);

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var summary = new RevenueSummary
                    {
                        TotalOrders = SafeConvertToInt32(reader["SoHD"]),
                        TotalRevenue = SafeConvertToDecimal(reader["DT"])
                    };

                    Logger.Info($"Lấy tổng hợp thành công: {summary}");
                    return summary;
                }

                Logger.Warn("Không có dữ liệu tổng hợp");
                return new RevenueSummary();
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"Lỗi SQL khi lấy tổng hợp doanh thu: {sqlEx.Message}", sqlEx);
                throw new DataException("Lỗi truy vấn cơ sở dữ liệu khi lấy tổng hợp doanh thu", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không xác định khi lấy tổng hợp doanh thu", ex);
                throw;
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        public List<ShiftRevenue> GetRevenueByShift(DateRange dateRange)
        {
            ValidateDateRange(dateRange);

            var shifts = new List<ShiftRevenue>();
            var shiftColors = new[]
            {
                Color.CornflowerBlue, Color.Orange, Color.BlueViolet, Color.LimeGreen,
                Color.Tomato, Color.DeepPink, Color.Brown, Color.Yellow
            };

            const string sql = @"
                SELECT COUNT(*)
                FROM HoaDon
                WHERE TrangThai = N'Đã thanh toán'
                  AND NgayTao >= @tu AND NgayTao < @den
                  AND DATEPART(HOUR, NgayTao) >= @h AND DATEPART(HOUR, NgayTao) < @h + 2";

            SqlConnection connection = null;

            try
            {
                Logger.Debug($"Lấy doanh thu theo ca: {dateRange}");

                connection = new SqlConnection(_connectionString);
                connection.Open();

                for (int i = 0; i < 8; i++)
                {
                    SqlCommand command = null;
                    try
                    {
                        int hour = 6 + i * 2;
                        command = new SqlCommand(sql, connection);
                        command.CommandTimeout = CommandTimeout;
                        command.Parameters.Add("@tu", SqlDbType.DateTime2).Value = dateRange.StartDate;
                        command.Parameters.Add("@den", SqlDbType.DateTime2).Value = dateRange.EndDate;
                        command.Parameters.Add("@h", SqlDbType.Int).Value = hour;

                        object result = command.ExecuteScalar();
                        int orderCount = SafeConvertToInt32(result);

                        shifts.Add(new ShiftRevenue
                        {
                            TimeRange = $"{hour:D2}h00 - {hour + 2:D2}h00",
                            OrderCount = orderCount,
                            Color = shiftColors[i]
                        });
                    }
                    finally
                    {
                        command?.Dispose();
                    }
                }

                Logger.Info($"Lấy doanh thu theo ca thành công: {shifts.Count} ca");
                return shifts;
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"Lỗi SQL khi lấy doanh thu theo ca: {sqlEx.Message}", sqlEx);
                throw new DataException("Lỗi truy vấn cơ sở dữ liệu khi lấy doanh thu theo ca", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không xác định khi lấy doanh thu theo ca", ex);
                throw;
            }
            finally
            {
                connection?.Close();
                connection?.Dispose();
            }
        }

        public List<TopSellingItem> GetTopSellingItems(DateRange dateRange, int topCount)
        {
            ValidateDateRange(dateRange);

            if (topCount <= 0)
            {
                throw new ArgumentException("Số lượng món phải lớn hơn 0", nameof(topCount));
            }

            var items = new List<TopSellingItem>();
            const string sql = @"
                SELECT TOP (@top)
                    m.TenMon,
                    ISNULL(SUM(ct.SoLuong), 0) AS SL
                FROM Mon m
                LEFT JOIN ChiTietHoaDon ct ON m.Id = ct.MonId
                LEFT JOIN HoaDon hd ON ct.HoaDonId = hd.Id
                WHERE (hd.Id IS NULL OR (hd.TrangThai = N'Đã thanh toán'
                    AND hd.NgayTao >= @tu
                    AND hd.NgayTao < @den))
                GROUP BY m.Id, m.TenMon
                ORDER BY SL DESC";

            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                Logger.Debug($"Lấy top {topCount} món bán chạy: {dateRange}");

                connection = new SqlConnection(_connectionString);
                command = new SqlCommand(sql, connection);
                command.CommandTimeout = CommandTimeout;
                command.Parameters.AddWithValue("@top", topCount);
                command.Parameters.Add("@tu", SqlDbType.DateTime).Value = dateRange.StartDate;
                command.Parameters.Add("@den", SqlDbType.DateTime).Value = dateRange.EndDate;

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(new TopSellingItem
                    {
                        ItemName = SafeGetString(reader, "TenMon"),
                        Quantity = SafeConvertToInt32(reader["SL"])
                    });
                }

                // Đảm bảo luôn có đủ items
                while (items.Count < topCount)
                {
                    items.Add(new TopSellingItem
                    {
                        ItemName = "Chưa có dữ liệu",
                        Quantity = 0
                    });
                }

                Logger.Info($"Lấy món bán chạy thành công: {items.Count} món");
                return items;
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"Lỗi SQL khi lấy món bán chạy: {sqlEx.Message}", sqlEx);
                throw new DataException("Lỗi truy vấn cơ sở dữ liệu khi lấy món bán chạy", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không xác định khi lấy món bán chạy", ex);
                throw;
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        public List<CategoryRatio> GetCategoryRatio(DateRange dateRange)
        {
            ValidateDateRange(dateRange);

            var categories = new List<CategoryRatio>();
            const string sql = @"
                SELECT ISNULL(m.Loai, N'Khác') AS Loai, SUM(ct.SoLuong) AS SL
                FROM ChiTietHoaDon ct
                JOIN HoaDon hd ON ct.HoaDonId = hd.Id
                JOIN Mon m ON ct.MonId = m.Id
                WHERE hd.TrangThai = N'Đã thanh toán'
                  AND hd.NgayTao >= @tu AND hd.NgayTao < @den
                GROUP BY ISNULL(m.Loai, N'Khác')";

            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                Logger.Debug($"Lấy tỷ lệ món: {dateRange}");

                connection = new SqlConnection(_connectionString);
                command = new SqlCommand(sql, connection);
                command.CommandTimeout = CommandTimeout;
                command.Parameters.AddWithValue("@tu", dateRange.StartDate);
                command.Parameters.AddWithValue("@den", dateRange.EndDate);

                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new CategoryRatio
                    {
                        CategoryName = SafeGetString(reader, "Loai"),
                        Quantity = SafeConvertToInt32(reader["SL"])
                    });
                }

                Logger.Info($"Lấy tỷ lệ món thành công: {categories.Count} loại");
                return categories;
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"Lỗi SQL khi lấy tỷ lệ món: {sqlEx.Message}", sqlEx);
                throw new DataException("Lỗi truy vấn cơ sở dữ liệu khi lấy tỷ lệ món", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không xác định khi lấy tỷ lệ món", ex);
                throw;
            }
            finally
            {
                reader?.Close();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        public DataTable GetOrderList(DateRange dateRange)
        {
            ValidateDateRange(dateRange);

            const string sql = @"
                SELECT
                    hd.Id,
                    'HD' + RIGHT('000' + CAST(hd.Id AS VARCHAR(3)), 3) AS MaHD,
                    ISNULL(tk.TenNV, N'Không xác định') AS NhanVien,
                    hd.TongTien,
                    CONVERT(varchar, hd.NgayTao, 103) + ' ' + CONVERT(varchar, hd.NgayTao, 108) AS NgayThanhToan
                FROM HoaDon hd
                LEFT JOIN TaiKhoan tk ON hd.NhanVienId = tk.Id
                WHERE hd.TrangThai = N'Đã thanh toán'
                  AND hd.NgayTao >= @tu AND hd.NgayTao < @den
                ORDER BY hd.NgayTao DESC";

            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataAdapter adapter = null;

            try
            {
                Logger.Debug($"Lấy danh sách hóa đơn: {dateRange}");

                connection = new SqlConnection(_connectionString);
                command = new SqlCommand(sql, connection);
                command.CommandTimeout = CommandTimeout;
                command.Parameters.AddWithValue("@tu", dateRange.StartDate);
                command.Parameters.AddWithValue("@den", dateRange.EndDate);

                adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataTable.Columns.Add("STT", typeof(int));
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable.Rows[i]["STT"] = i + 1;
                }

                Logger.Info($"Lấy danh sách hóa đơn thành công: {dataTable.Rows.Count} hóa đơn");
                return dataTable;
            }
            catch (SqlException sqlEx)
            {
                Logger.Error($"Lỗi SQL khi lấy danh sách hóa đơn: {sqlEx.Message}", sqlEx);
                throw new DataException("Lỗi truy vấn cơ sở dữ liệu khi lấy danh sách hóa đơn", sqlEx);
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi không xác định khi lấy danh sách hóa đơn", ex);
                throw;
            }
            finally
            {
                adapter?.Dispose();
                command?.Dispose();
                connection?.Close();
                connection?.Dispose();
            }
        }

        #region Helper Methods
        private void ValidateDateRange(DateRange dateRange)
        {
            if (dateRange == null)
            {
                throw new ArgumentNullException(nameof(dateRange), "DateRange không được null");
            }

            if (dateRange.StartDate >= dateRange.EndDate)
            {
                throw new ArgumentException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
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
                Logger.Warn($"Không thể chuyển đổi giá trị '{value}' sang Int32");
                return 0;
            }
        }

        private decimal SafeConvertToDecimal(object value)
        {
            try
            {
                if (value == null || value == DBNull.Value)
                {
                    return 0m;
                }
                return Convert.ToDecimal(value);
            }
            catch
            {
                Logger.Warn($"Không thể chuyển đổi giá trị '{value}' sang Decimal");
                return 0m;
            }
        }

        private string SafeGetString(SqlDataReader reader, string columnName)
        {
            try
            {
                int ordinal = reader.GetOrdinal(columnName);
                if (reader.IsDBNull(ordinal))
                {
                    return string.Empty;
                }
                return reader.GetString(ordinal);
            }
            catch
            {
                Logger.Warn($"Không thể lấy giá trị cột '{columnName}'");
                return string.Empty;
            }
        }
        #endregion
    }

    public class ChartConfigService
    {
        private readonly Color[] _topSellingColors =
        {
            Color.FromArgb(255, 99, 71),
            Color.FromArgb(255, 165, 0),
            Color.FromArgb(255, 215, 0),
            Color.FromArgb(50, 205, 50),
            Color.FromArgb(30, 144, 255)
        };

        private readonly Color[] _pieColors =
        {
            Color.FromArgb(88, 114, 170),
            Color.FromArgb(152, 196, 191),
            Color.FromArgb(246, 178, 84),
            Color.FromArgb(194, 100, 90)
        };

        public void ConfigureShiftChart(Chart chart, List<ShiftRevenue> data)
        {
            if (chart == null)
            {
                throw new ArgumentNullException(nameof(chart));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            try
            {
                Logger.Debug("Cấu hình biểu đồ doanh thu theo ca");

                chart.Series.Clear();
                var area = chart.ChartAreas[0];
                area.AxisY.Title = "Đơn";
                area.AxisY.TitleAlignment = StringAlignment.Far;
                area.AxisY.Minimum = 0;
                area.AxisY.Maximum = 40;
                area.AxisY.Interval = 5;

                var series = new Series("DoanhThu")
                {
                    Color = Color.Transparent,
                    ChartType = SeriesChartType.Bar,
                    IsValueShownAsLabel = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    LabelForeColor = Color.Black
                };
                series["BarLabelStyle"] = "Outside";
                series["PointWidth"] = "0.7";

                foreach (var shift in data)
                {
                    if (shift == null) continue;

                    var point = new DataPoint(0, shift.OrderCount)
                    {
                        AxisLabel = shift.TimeRange,
                        Color = shift.Color,
                        Label = shift.OrderCount > 0 ? shift.OrderCount.ToString() : ""
                    };
                    series.Points.Add(point);
                }

                chart.Series.Add(series);
                Logger.Debug("Cấu hình biểu đồ doanh thu theo ca thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi cấu hình biểu đồ doanh thu theo ca", ex);
                throw new InvalidOperationException("Không thể cấu hình biểu đồ doanh thu theo ca", ex);
            }
        }

        public void ConfigureTopSellingChart(Chart chart, List<TopSellingItem> data)
        {
            if (chart == null)
            {
                throw new ArgumentNullException(nameof(chart));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            try
            {
                Logger.Debug("Cấu hình biểu đồ món bán chạy");

                chart.Series.Clear();
                chart.Titles.Clear();
                chart.BorderlineWidth = 1;
                chart.BorderlineColor = Color.Black;
                chart.BorderlineDashStyle = ChartDashStyle.Solid;

                var series = new Series("SoLuong")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    LabelForeColor = Color.Black
                };
                series["PointWidth"] = "0.5";

                for (int i = 0; i < data.Count; i++)
                {
                    var item = data[i];
                    if (item == null) continue;

                    int pointIndex = series.Points.AddXY(item.ItemName, item.Quantity);
                    series.Points[pointIndex].Color = item.Quantity > 0
                        ? _topSellingColors[i % _topSellingColors.Length]
                        : Color.LightGray;
                    series.Points[pointIndex].Label = item.Quantity > 0 ? item.Quantity.ToString() : "";
                }

                chart.Series.Add(series);

                var title = chart.Titles.Add("Biểu đồ món bán chạy");
                title.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                title.ForeColor = Color.SaddleBrown;
                title.BackColor = Color.FromArgb(255, 192, 192);
                title.BorderColor = Color.Black;
                title.BorderWidth = 1;
                title.Docking = Docking.Top;
                title.Alignment = ContentAlignment.MiddleCenter;

                chart.BackColor = Color.FromArgb(255, 250, 240);
                Logger.Debug("Cấu hình biểu đồ món bán chạy thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi cấu hình biểu đồ món bán chạy", ex);
                throw new InvalidOperationException("Không thể cấu hình biểu đồ món bán chạy", ex);
            }
        }

        public void ConfigurePieChart(Chart chart, List<CategoryRatio> data)
        {
            if (chart == null)
            {
                throw new ArgumentNullException(nameof(chart));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            try
            {
                Logger.Debug("Cấu hình biểu đồ tỷ lệ món");

                chart.Series.Clear();
                chart.Titles.Clear();
                chart.Legends.Clear();
                chart.BorderlineWidth = 1;
                chart.BorderlineColor = Color.Black;

                var chartArea = chart.ChartAreas[0];
                chartArea.Position = new ElementPosition(0, 15, 100, 85);
                chartArea.InnerPlotPosition = new ElementPosition(5, 5, 60, 90);
                chartArea.BackColor = Color.Transparent;

                var title = new Title("Tỷ lệ món bán chạy", Docking.Top,
                    new Font("Segoe UI", 11F, FontStyle.Bold), Color.White)
                {
                    BackColor = Color.FromArgb(178, 102, 102),
                    BorderColor = Color.Black,
                    BorderWidth = 1
                };
                chart.Titles.Add(title);

                var legend = new Legend
                {
                    Docking = Docking.Right,
                    Alignment = StringAlignment.Center,
                    Font = new Font("Segoe UI", 9F)
                };
                chart.Legends.Add(legend);

                var series = new Series("TyLe")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    LabelForeColor = Color.White,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold)
                };
                series["PieLabelStyle"] = "Inside";
                series["PieLineColor"] = "Black";

                if (data.Count == 0)
                {
                    series.Points.AddY(1);
                    series.Points[0].Color = Color.LightGray;
                    series.Points[0].Label = "Chưa có dữ liệu";
                    series.Points[0].LegendText = "Chưa có dữ liệu";
                }
                else
                {
                    int total = 0;
                    foreach (var item in data)
                    {
                        if (item != null)
                            total += item.Quantity;
                    }

                    if (total == 0)
                    {
                        Logger.Warn("Tổng số lượng bằng 0");
                        total = 1;
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        var item = data[i];
                        if (item == null) continue;

                        double percentage = item.Quantity * 100.0 / total;
                        int pointIndex = series.Points.AddY(item.Quantity);
                        series.Points[pointIndex].Color = _pieColors[i % _pieColors.Length];
                        series.Points[pointIndex].Label = $"{percentage:0}%";
                        series.Points[pointIndex].LegendText = item.CategoryName;
                    }
                }

                chart.Series.Add(series);
                Logger.Debug("Cấu hình biểu đồ tỷ lệ món thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi cấu hình biểu đồ tỷ lệ món", ex);
                throw new InvalidOperationException("Không thể cấu hình biểu đồ tỷ lệ món", ex);
            }
        }
    }

    public class DataGridConfigService
    {
        public void ConfigureDataGridView(DataGridView grid)
        {
            if (grid == null)
            {
                throw new ArgumentNullException(nameof(grid));
            }

            try
            {
                Logger.Debug("Cấu hình DataGridView");

                grid.AutoGenerateColumns = false;
                grid.Columns.Clear();
                grid.Columns.AddRange(new DataGridViewColumn[]
                {
                    new DataGridViewTextBoxColumn
                    {
                        Name = "STT",
                        HeaderText = "STT",
                        Width = 60
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "MaHD",
                        HeaderText = "Mã HD",
                        Width = 90,
                        DataPropertyName = "MaHD"
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "NhanVien",
                        HeaderText = "Nhân viên",
                        Width = 140,
                        DataPropertyName = "NhanVien"
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "TongTien",
                        HeaderText = "Tổng tiền",
                        Width = 120,
                        DataPropertyName = "TongTien",
                        DefaultCellStyle = new DataGridViewCellStyle
                        {
                            Format = "N0",
                            ForeColor = Color.Black
                        }
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "NgayThanhToan",
                        HeaderText = "Ngày thanh toán",
                        Width = 160,
                        DataPropertyName = "NgayThanhToan"
                    }
                });

                grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(91, 155, 213);
                grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                grid.EnableHeadersVisualStyles = false;

                grid.RowsDefaultCellStyle.BackColor = Color.White;
                grid.RowsDefaultCellStyle.ForeColor = Color.Black;
                grid.RowsDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
                grid.RowsDefaultCellStyle.SelectionBackColor = Color.White;
                grid.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

                grid.CellFormatting += Grid_CellFormatting;

                grid.BackgroundColor = Color.White;
                grid.ReadOnly = true;
                grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grid.AllowUserToAddRows = false;
                grid.RowHeadersVisible = false;

                Logger.Debug("Cấu hình DataGridView thành công");
            }
            catch (Exception ex)
            {
                Logger.Error("Lỗi khi cấu hình DataGridView", ex);
                throw new InvalidOperationException("Không thể cấu hình DataGridView", ex);
            }
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                var grid = sender as DataGridView;
                if (grid == null || e.RowIndex < 0) return;

                if (grid.Columns[e.ColumnIndex].Name == "STT")
                {
                    e.Value = (e.RowIndex + 1).ToString();
                    e.FormattingApplied = true;
                }
            }
            catch
            {
                Logger.Warn($"Lỗi khi format cell tại dòng {e.RowIndex}");
            }
        }
    }
    #endregion
}