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
            InitializeComponent();
            string connectionString = @"Server=NGUYENNHI2407\SQLEXPRESS;Database=QuanLyQuanCafe2;Integrated Security=true;";
            _reportService = new ReportService(connectionString);
            _chartConfigService = new ChartConfigService();
            _gridConfigService = new DataGridConfigService();
            InitializeEventHandlers();
            _gridConfigService.ConfigureDataGridView(dgvHoaDon);
            LoadDefaultReport();
        }
        private void InitializeEventHandlers()
        {
            btnNgay.Click += BtnNgay_Click;
            btnThang.Click += BtnThang_Click;
            btnNam.Click += BtnNam_Click;
            btnRefresh.Click += (s, e) => BtnNgay_Click(s, e);
            btnInBaoCao.Click += BtnInBaoCao_Click;
        }
        private void LoadDefaultReport()
        {
            _tuNgay = DateTime.Today;
            _denNgay = _tuNgay.AddDays(1);
            LoadBaoCao($"Ngày {_tuNgay:dd/MM/yyyy}");
        }
        #region Event Handlers
        private void BtnNgay_Click(object sender, EventArgs e)
        {
            _tuNgay = DateTime.Today;
            _denNgay = _tuNgay.AddDays(1);
            LoadBaoCao($"Ngày {_tuNgay:dd/MM/yyyy}");
        }
        private void BtnThang_Click(object sender, EventArgs e)
        {
            _tuNgay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            _denNgay = _tuNgay.AddMonths(1);
            LoadBaoCao($"Tháng {_tuNgay:MM/yyyy}");
        }
        private void BtnNam_Click(object sender, EventArgs e)
        {
            _tuNgay = new DateTime(DateTime.Today.Year, 1, 1);
            _denNgay = _tuNgay.AddYears(1);
            LoadBaoCao($"Năm {_tuNgay:yyyy}");
        }
        private void BtnInBaoCao_Click(object sender, EventArgs e)
        {
            var tuNgay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var denNgay = tuNgay.AddMonths(1);
            var f = new frmInBaoCao
            {
                TuNgay = tuNgay,
                DenNgay = denNgay
            };
            f.ShowDialog();
        }
        #endregion
        private void LoadBaoCao(string tieuDe)
        {
            this.Text = $"Báo cáo doanh thu - {tieuDe}";
            var dateRange = new DateRange(_tuNgay, _denNgay);
            LoadTongHop(dateRange);
            LoadDoanhThuTheoCa(dateRange);
            LoadMonBanChay(dateRange);
            LoadTyLeMon(dateRange);
            LoadDanhSachHD(dateRange);
        }
        private void LoadTongHop(DateRange dateRange)
        {
            var summary = _reportService.GetRevenueSummary(dateRange);
            lblTongHD.Text = summary.TotalOrders.ToString();
            lblTongDT.Text = $"{summary.TotalRevenue:N0} đ";
        }
        private void LoadDoanhThuTheoCa(DateRange dateRange)
        {
            var revenueByShift = _reportService.GetRevenueByShift(dateRange);
            _chartConfigService.ConfigureShiftChart(chartDoanhThuCa, revenueByShift);
        }
        private void LoadMonBanChay(DateRange dateRange)
        {
            var topItems = _reportService.GetTopSellingItems(dateRange, 5);
            _chartConfigService.ConfigureTopSellingChart(chartMonBanChay, topItems);
        }
        private void LoadTyLeMon(DateRange dateRange)
        {
            var categoryRatio = _reportService.GetCategoryRatio(dateRange);
            _chartConfigService.ConfigurePieChart(chartTyLe, categoryRatio);
        }
        private void LoadDanhSachHD(DateRange dateRange)
        {
            var orders = _reportService.GetOrderList(dateRange);
            dgvHoaDon.DataSource = orders;
        }
    }
    #region Models
    public class DateRange
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
    public class RevenueSummary
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
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
        public ReportService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public RevenueSummary GetRevenueSummary(DateRange dateRange)
        {
            const string sql = @"
                SELECT COUNT(*) AS SoHD, ISNULL(SUM(TongTien), 0) AS DT
                FROM HoaDon
                WHERE TrangThai = N'Đã thanh toán'
                  AND NgayTao >= @tu AND NgayTao < @den";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@tu", dateRange.StartDate);
                command.Parameters.AddWithValue("@den", dateRange.EndDate);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new RevenueSummary
                        {
                            TotalOrders = Convert.ToInt32(reader["SoHD"]),
                            TotalRevenue = Convert.ToDecimal(reader["DT"])
                        };
                    }
                }
            }
            return new RevenueSummary();
        }
        public List<ShiftRevenue> GetRevenueByShift(DateRange dateRange)
        {
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
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                for (int i = 0; i < 8; i++)
                {
                    int hour = 6 + i * 2;
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@tu", SqlDbType.DateTime2).Value = dateRange.StartDate;
                        command.Parameters.Add("@den", SqlDbType.DateTime2).Value = dateRange.EndDate;
                        command.Parameters.Add("@h", SqlDbType.Int).Value = hour;
                        int orderCount = Convert.ToInt32(command.ExecuteScalar());
                        shifts.Add(new ShiftRevenue
                        {
                            TimeRange = $"{hour:D2}h00 - {hour + 2:D2}h00",
                            OrderCount = orderCount,
                            Color = shiftColors[i]
                        });
                    }
                }
            }
            return shifts;
        }
        public List<TopSellingItem> GetTopSellingItems(DateRange dateRange, int topCount)
        {
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
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@top", topCount);
                command.Parameters.Add("@tu", SqlDbType.DateTime).Value = dateRange.StartDate;
                command.Parameters.Add("@den", SqlDbType.DateTime).Value = dateRange.EndDate;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new TopSellingItem
                        {
                            ItemName = reader["TenMon"].ToString(),
                            Quantity = Convert.ToInt32(reader["SL"])
                        });
                    }
                }
            }
            // Đảm bảo luôn có đủ 5 items
            while (items.Count < topCount)
            {
                items.Add(new TopSellingItem
                {
                    ItemName = "Chưa có dữ liệu",
                    Quantity = 0
                });
            }
            return items;
        }
        public List<CategoryRatio> GetCategoryRatio(DateRange dateRange)
        {
            var categories = new List<CategoryRatio>();
            const string sql = @"
                SELECT ISNULL(m.Loai, N'Khác') AS Loai, SUM(ct.SoLuong) AS SL
                FROM ChiTietHoaDon ct
                JOIN HoaDon hd ON ct.HoaDonId = hd.Id
                JOIN Mon m ON ct.MonId = m.Id
                WHERE hd.TrangThai = N'Đã thanh toán'
                  AND hd.NgayTao >= @tu AND hd.NgayTao < @den
                GROUP BY ISNULL(m.Loai, N'Khác')";
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@tu", dateRange.StartDate);
                command.Parameters.AddWithValue("@den", dateRange.EndDate);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoryRatio
                        {
                            CategoryName = reader["Loai"].ToString(),
                            Quantity = Convert.ToInt32(reader["SL"])
                        });
                    }
                }
            }
            return categories;
        }
        public DataTable GetOrderList(DateRange dateRange)
        {
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
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@tu", dateRange.StartDate);
                command.Parameters.AddWithValue("@den", dateRange.EndDate);
                var adapter = new SqlDataAdapter(command);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataTable.Columns.Add("STT", typeof(int));
                for (int i = 0; i < dataTable.Rows.Count; i++)
                    dataTable.Rows[i]["STT"] = i + 1;
                return dataTable;
            }
        }
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
                var point = new DataPoint(0, shift.OrderCount)
                {
                    AxisLabel = shift.TimeRange,
                    Color = shift.Color,
                    Label = shift.OrderCount > 0 ? shift.OrderCount.ToString() : ""
                };
                series.Points.Add(point);
            }
            chart.Series.Add(series);
        }
        public void ConfigureTopSellingChart(Chart chart, List<TopSellingItem> data)
        {
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
        }
        public void ConfigurePieChart(Chart chart, List<CategoryRatio> data)
        {
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
                    total += item.Quantity;
                for (int i = 0; i < data.Count; i++)
                {
                    var item = data[i];
                    double percentage = item.Quantity * 100.0 / total;
                    int pointIndex = series.Points.AddY(item.Quantity);
                    series.Points[pointIndex].Color = _pieColors[i % _pieColors.Length];
                    series.Points[pointIndex].Label = $"{percentage:0}%";
                    series.Points[pointIndex].LegendText = item.CategoryName;
                }
            }
            chart.Series.Add(series);
        }
    }
    public class DataGridConfigService
    {
        public void ConfigureDataGridView(DataGridView grid)
        {
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
            grid.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0 && grid.Columns[e.ColumnIndex].Name == "STT")
                {
                    e.Value = (e.RowIndex + 1).ToString();
                    e.FormattingApplied = true;
                }
            };
            grid.BackgroundColor = Color.White;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersVisible = false;
        }
    }
    #endregion
}