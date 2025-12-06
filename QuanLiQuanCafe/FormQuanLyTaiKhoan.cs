using log4net;
using QuanLiQuanCafe.Helpers;
using QuanLiQuanCafe.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form quản lý tài khoản nhân viên
    /// Áp dụng: Single Responsibility Principle, Extract Method, DRY (Don't Repeat Yourself)
    /// </summary>
    public partial class FormQuanLyTaiKhoan : Form
    {
        #region Fields
        private static readonly ILog log = LogManager.GetLogger(typeof(FormQuanLyTaiKhoan));
        private readonly TaiKhoanBUS _bus = new TaiKhoanBUS();
        private DataTable _dtTaiKhoan;
        private readonly string _currentUser;
        private readonly string _currentRole;
        #endregion

        #region Constructor & Initialization
        public FormQuanLyTaiKhoan()
        {
            InitializeComponent();
            KhoiTaoForm();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách logic khởi tạo
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private void KhoiTaoForm()
        {
            LoadDanhSachTaiKhoan();
            GanSuKienChoTab();
        }

        /// <summary>
        /// Phương pháp: Event Binding - Gán sự kiện cho các tab filter
        /// Sử dụng Lambda Expression để code ngắn gọn
        /// </summary>
        private void GanSuKienChoTab()
        {
            // Reset màu tất cả về xám khi mở form
            TabButtonManager.Reset(btnTatCa, btnDangLam, btnNghi);

            // Mặc định chọn "Tất cả" + đổi màu cam
            TabButtonManager.Activate(btnTatCa);

            // Gắn sự kiện click
            btnTatCa.Click += (s, e) =>
            {
                TabButtonManager.Activate(btnTatCa);
                LoadDanhSachTaiKhoan(); // null = tất cả
            };

            btnDangLam.Click += (s, e) =>
            {
                TabButtonManager.Activate(btnDangLam);
                LoadDanhSachTaiKhoan("Đang làm");
            };

            btnNghi.Click += (s, e) =>
            {
                TabButtonManager.Activate(btnNghi);
                LoadDanhSachTaiKhoan("Nghỉ");
            };
        }
        #endregion

        #region Data Loading
        /// <summary>
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng load dữ liệu
        /// 1. Lấy dữ liệu từ BUS
        /// 2. Clear grid
        /// 3. Thêm từng row vào grid
        /// 4. Format màu sắc
        /// </summary>
        private void LoadDanhSachTaiKhoan(string trangThai = null)
        {
            try
            {
                // Bước 1: Lấy dữ liệu
                _dtTaiKhoan = _bus.LayDanhSachTaiKhoan(trangThai);

                // Bước 2: Xóa dữ liệu cũ
                dgvTaiKhoan.Rows.Clear();

                // Bước 3: Thêm dữ liệu mới
                ThemDuLieuVaoGrid(_dtTaiKhoan);
            }
            catch (Exception ex)
            {
                XuLyLoiLoadDuLieu(ex);
            }
            // Tự động đổi màu tab đúng với dữ liệu đang hiển thị
            if (trangThai == "Đang làm")
                TabButtonManager.Activate(btnDangLam);
            else if (trangThai == "Nghỉ")
                TabButtonManager.Activate(btnNghi);
            else
                TabButtonManager.Activate(btnTatCa);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách logic thêm dữ liệu vào grid
        /// Nguyên tắc: DRY - Tránh lặp code giữa Load và Search
        /// </summary>
        private void ThemDuLieuVaoGrid(DataTable data)
        {
            int stt = 1;
            foreach (DataRow row in data.Rows)
            {
                int rowIndex = ThemRowVaoGrid(row, stt++);
                ApDungMauChoRow(rowIndex, row);
                LuuThongTinVaoTag(rowIndex, row);
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách logic thêm 1 row
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private int ThemRowVaoGrid(DataRow row, int stt)
        {
            return dgvTaiKhoan.Rows.Add(
                stt,
                row["Id"],
                row["HoTen"],
                row["SoDienThoai"],
                row["DiaChi"],
                row["VaiTro"],
                row["TrangThai"]
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách logic áp dụng màu sắc
        /// Nguyên tắc: Single Responsibility - chỉ làm việc format màu
        /// </summary>
        private void ApDungMauChoRow(int rowIndex, DataRow data)
        {
            string trangThai = data["TrangThai"]?.ToString();
            Color mauChu = LayMauTheoTrangThai(trangThai);
            dgvTaiKhoan.Rows[rowIndex].DefaultCellStyle.ForeColor = mauChu;
        }

        /// <summary>
        /// Phương pháp: Strategy Pattern - Xác định màu theo trạng thái
        /// Nguyên tắc: Open/Closed - dễ mở rộng thêm màu mới
        /// </summary>
        private Color LayMauTheoTrangThai(string trangThai)
        {
            if (trangThai == "Nghỉ việc" || trangThai == "Nghỉ")
                return Color.Red;

            if (trangThai == "Đang làm")
                return Color.Green;

            return Color.Black; // Màu mặc định
        }

        /// <summary>
        /// Phương pháp: Extract Method - Lưu TenDangNhap vào Tag
        /// Mục đích: Dùng cho các thao tác Sửa/Xóa sau này
        /// </summary>
        private void LuuThongTinVaoTag(int rowIndex, DataRow data)
        {
            dgvTaiKhoan.Rows[rowIndex].Tag = data["TenDangNhap"];
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi load dữ liệu
        /// </summary>
        private void XuLyLoiLoadDuLieu(Exception ex)
        {
            MessageBox.Show(
                $"Không thể tải danh sách tài khoản!\n\nLỗi: {ex.Message}",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            log.Error("Lỗi load danh sách tài khoản", ex);
        }
        #endregion

        #region Search
        /// <summary>
        /// Phương pháp: Filter Pattern - Lọc dữ liệu theo keyword
        /// Nguyên tắc: Guard Clause - return sớm nếu không có keyword
        /// </summary>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();

            // Guard Clause: Nếu rỗng thì load lại toàn bộ
            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadDanhSachTaiKhoan();
                return;
            }

            TimKiemVaHienThi(keyword);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách logic tìm kiếm
        /// Sử dụng lại ThemDuLieuVaoGrid() - áp dụng DRY
        /// </summary>
        private void TimKiemVaHienThi(string keyword)
        {
            dgvTaiKhoan.Rows.Clear();

            DataTable ketQua = LocDuLieuTheoKeyword(keyword);

            if (ketQua.Rows.Count == 0)
            {
                HienThiThongBaoKhongTimThay();
                return;
            }

            ThemDuLieuVaoGrid(ketQua);
        }

        /// <summary>
        /// Phương pháp: Filter Pattern - Lọc DataTable theo keyword
        /// Nguyên tắc: Single Responsibility - chỉ làm việc lọc
        /// </summary>
        private DataTable LocDuLieuTheoKeyword(string keyword)
        {
            DataTable ketQua = _dtTaiKhoan.Clone(); // Copy cấu trúc

            foreach (DataRow row in _dtTaiKhoan.Rows)
            {
                if (RowMatchKeyword(row, keyword))
                {
                    ketQua.ImportRow(row);
                }
            }

            return ketQua;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra row có khớp keyword không
        /// </summary>
        private bool RowMatchKeyword(DataRow row, string keyword)
        {
            string hoTen = row["HoTen"]?.ToString().ToLower() ?? "";
            string tenDangNhap = row["TenDangNhap"]?.ToString().ToLower() ?? "";
            string sdt = row["SoDienThoai"]?.ToString() ?? "";

            return hoTen.Contains(keyword) ||
                   tenDangNhap.Contains(keyword) ||
                   sdt.Contains(keyword);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông báo không tìm thấy
        /// </summary>
        private void HienThiThongBaoKhongTimThay()
        {
            MessageBox.Show(
                "Không tìm thấy kết quả nào!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        #endregion

        #region CRUD Operations
        /// <summary>
        /// Phương pháp: Command Pattern - Thực hiện lệnh Refresh
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadDanhSachTaiKhoan();
            HienThiThongBaoLamMoi();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông báo làm mới
        /// </summary>
        private void HienThiThongBaoLamMoi()
        {
            MessageBox.Show(
                "Đã làm mới danh sách!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Phương pháp: Command Pattern - Thực hiện lệnh Thêm
        /// Sử dụng using statement để tự động dispose form
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var formThem = new FormThemNhanVien())
            {
                if (formThem.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachTaiKhoan();
                    HienThiThongBaoThemThanhCong();
                }
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông báo thêm thành công
        /// </summary>
        private void HienThiThongBaoThemThanhCong()
        {
            MessageBox.Show(
                "Thêm nhân viên thành công!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Phương pháp: Command Pattern + Guard Clauses - Thực hiện lệnh Sửa
        /// Guard Clause: Kiểm tra có chọn row không
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Guard Clause 1: Kiểm tra có chọn row
            if (!KiemTraDaChonRow())
            {
                HienThiThongBaoChonNhanVien("sửa");
                return;
            }

            // Guard Clause 2: Kiểm tra lấy được thông tin
            string tenDangNhap = LayTenDangNhapDaChon();
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                HienThiLoiKhongLayDuocThongTin();
                return;
            }

            // Happy path: Mở form sửa
            MoFormSuaThongTin(tenDangNhap);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra có chọn row không
        /// </summary>
        private bool KiemTraDaChonRow()
        {
            return dgvTaiKhoan.SelectedRows.Count > 0;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Lấy TenDangNhap từ row đã chọn
        /// </summary>
        private string LayTenDangNhapDaChon()
        {
            return dgvTaiKhoan.SelectedRows[0].Tag?.ToString();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông báo chọn nhân viên
        /// </summary>
        private void HienThiThongBaoChonNhanVien(string hanhDong)
        {
            MessageBox.Show(
                $"Vui lòng chọn một nhân viên để {hanhDong}!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị lỗi không lấy được thông tin
        /// </summary>
        private void HienThiLoiKhongLayDuocThongTin()
        {
            MessageBox.Show(
                "Không thể lấy thông tin tài khoản!",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Mở form sửa thông tin
        /// </summary>
        private void MoFormSuaThongTin(string tenDangNhap)
        {
            using (var formSua = new FormSuaThongTinNhanVien(tenDangNhap))
            {
                if (formSua.ShowDialog() == DialogResult.OK)
                {
                    LoadDanhSachTaiKhoan();
                    HienThiThongBaoCapNhatThanhCong();
                }
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông báo cập nhật thành công
        /// </summary>
        private void HienThiThongBaoCapNhatThanhCong()
        {
            MessageBox.Show(
                "Cập nhật thông tin thành công!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Phương pháp: Command Pattern + Guard Clauses - Thực hiện lệnh Xóa
        /// Confirmation Dialog: Yêu cầu xác nhận trước khi xóa
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Guard Clause 1: Kiểm tra có chọn row
            if (!KiemTraDaChonRow())
            {
                HienThiThongBaoChonNhanVien("xóa");
                return;
            }

            // Guard Clause 2: Kiểm tra lấy được thông tin
            string tenDangNhap = LayTenDangNhapDaChon();
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                HienThiLoiKhongLayDuocThongTin();
                return;
            }

            // Guard Clause 3: Yêu cầu xác nhận
            if (!XacNhanXoa(tenDangNhap))
            {
                return;
            }

            // Happy path: Thực hiện xóa
            ThucHienXoaTaiKhoan(tenDangNhap);
        }

        /// <summary>
        /// Phương pháp: Confirmation Dialog - Yêu cầu xác nhận xóa
        /// </summary>
        private bool XacNhanXoa(string tenDangNhap)
        {
            return MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản:\n{tenDangNhap}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) == DialogResult.Yes;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Thực hiện xóa tài khoản
        /// Strategy Pattern: Xử lý thành công/thất bại khác nhau
        /// </summary>
        private void ThucHienXoaTaiKhoan(string tenDangNhap)
        {
            bool ketQua = _bus.XoaTaiKhoan(tenDangNhap);

            if (ketQua)
            {
                XuLyXoaThanhCong(tenDangNhap);
            }
            else
            {
                XuLyXoaThatBai();
            }

            LoadDanhSachTaiKhoan();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý xóa thành công
        /// </summary>
        private void XuLyXoaThanhCong(string tenDangNhap)
        {
            MessageBox.Show(
                "Xóa tài khoản thành công!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            log.Info($"Đã xóa tài khoản: {tenDangNhap}");
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý xóa thất bại
        /// </summary>
        private void XuLyXoaThatBai()
        {
            MessageBox.Show(
                "Xóa thất bại! Có thể tài khoản đang được sử dụng.",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Event Handler: Double click để sửa nhanh
        /// </summary>
        private void dgvTaiKhoan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Guard Clause: Kiểm tra index hợp lệ
            if (e.RowIndex < 0)
                return;

            string tenDangNhap = dgvTaiKhoan.Rows[e.RowIndex].Tag?.ToString();

            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                MoFormSuaThongTin(tenDangNhap);
            }
        }

        /// <summary>
        /// Event Handler: Click vào cell (có thể mở rộng sau)
        /// </summary>
        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Để trống - có thể xử lý checkbox hoặc button column sau này
        }
        #endregion
        /// <summary>
        /// Xử lý đăng xuất người dùng
        /// - Ghi log đăng xuất
        /// - Hỏi xác nhận
        /// - Khởi động lại ứng dụng (cách đăng xuất phổ biến trong WinForms)
        /// </summary>
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Bước 1: Xác nhận người dùng có chắc chắn muốn đăng xuất không
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return; // Người dùng hủy → không làm gì

            try
            {
                // Bước 2: Ghi log đăng xuất
                log.Info($"Người dùng đã đăng xuất: {_currentUser} (Vai trò: {_currentRole})");

                // Bước 3: Hiển thị thông báo đăng xuất thành công
                MessageBox.Show(
                    "Đăng xuất thành công!\nỨng dụng sẽ khởi động lại.",
                    "Đăng xuất",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Bước 4: Khởi động lại ứng dụng (cách đăng xuất chuẩn WinForms)
                Application.Restart();
                // Application.Exit(); // Nếu dùng cái này sẽ thoát luôn, không quay lại form đăng nhập
            }
            catch (Exception ex)
            {
                // Hiếm khi xảy ra, nhưng vẫn nên bắt lỗi
                MessageBox.Show(
                    "Đã xảy ra lỗi khi đăng xuất. Ứng dụng sẽ khởi động lại.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                log.Error("Lỗi khi đăng xuất", ex);
                Application.Restart(); // Vẫn khởi động lại dù có lỗi
            }
        }
    }
}