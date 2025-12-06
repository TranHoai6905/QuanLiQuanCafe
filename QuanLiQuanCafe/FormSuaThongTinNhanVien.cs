using System;
using System.Windows.Forms;
using QuanLiQuanCafe.Models;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form sửa thông tin nhân viên (dành cho Quản lý)
    /// Áp dụng: Single Responsibility Principle, Guard Clauses, Extract Method
    /// </summary>
    public partial class FormSuaThongTinNhanVien : Form
    {
        #region Fields
        private readonly TaiKhoanBUS _bus = new TaiKhoanBUS();
        private readonly string _tenDangNhap;
        #endregion

        #region Constructor
        public FormSuaThongTinNhanVien(string tenDangNhap)
        {
            InitializeComponent();
            _tenDangNhap = tenDangNhap;
            LoadThongTin();
        }
        #endregion

        #region Data Loading
        /// <summary>
        /// Phương pháp: Template Method Pattern - Load thông tin nhân viên
        /// 1. Lấy thông tin từ BUS
        /// 2. Hiển thị lên form
        /// </summary>
        private void LoadThongTin()
        {
            var taiKhoan = LayThongTinTaiKhoan();

            if (taiKhoan != null)
            {
                HienThiThongTinLenForm(taiKhoan);
            }
            else
            {
                XuLyKhongTimThayThongTin();
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Lấy thông tin tài khoản từ BUS
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private TaiKhoan LayThongTinTaiKhoan()
        {
            return _bus.LayThongTinTaiKhoan(_tenDangNhap);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông tin lên form
        /// </summary>
        private void HienThiThongTinLenForm(TaiKhoan taiKhoan)
        {
            HienThiThongTinCoBan(taiKhoan);
            HienThiVaiTro(taiKhoan);
            HienThiTrangThai(taiKhoan);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông tin cơ bản
        /// </summary>
        private void HienThiThongTinCoBan(TaiKhoan taiKhoan)
        {
            txtHoTen.Text = taiKhoan.HoTen;
            txtSoDienThoai.Text = taiKhoan.SoDienThoai;
            txtDiaChi.Text = taiKhoan.DiaChi ?? "";
            dtpNgaySinh.Value = taiKhoan.NgaySinh ?? DateTime.Today;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị vai trò
        /// </summary>
        private void HienThiVaiTro(TaiKhoan taiKhoan)
        {
            cmbVaiTro.Text = taiKhoan.VaiTro;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị trạng thái
        /// </summary>
        private void HienThiTrangThai(TaiKhoan taiKhoan)
        {
            if (!string.IsNullOrEmpty(taiKhoan.TrangThai))
            {
                cboTrangThai.Text = taiKhoan.TrangThai;
            }
            else
            {
                cboTrangThai.SelectedIndex = 0; // Mặc định "Đang làm"
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý không tìm thấy thông tin
        /// </summary>
        private void XuLyKhongTimThayThongTin()
        {
            MessageBox.Show(
                "Không tìm thấy thông tin nhân viên!",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        #endregion

        #region Data Models
        /// <summary>
        /// Phương pháp: Value Object Pattern - Đóng gói dữ liệu cập nhật
        /// </summary>
        private class UpdateEmployeeData
        {
            public string HoTen { get; set; }
            public string SoDienThoai { get; set; }
            public string DiaChi { get; set; }
            public DateTime NgaySinh { get; set; }
            public string VaiTro { get; set; }
            public string TrangThai { get; set; }
            public string MatKhauMoi { get; set; }

            public UpdateEmployeeData(string hoTen, string sdt, string diaChi, DateTime ngaySinh,
                                     string vaiTro, string trangThai, string matKhauMoi)
            {
                HoTen = hoTen;
                SoDienThoai = sdt;
                DiaChi = diaChi;
                NgaySinh = ngaySinh;
                VaiTro = vaiTro;
                TrangThai = trangThai;
                MatKhauMoi = matKhauMoi;
            }
        }
        #endregion

        #region Data Collection
        /// <summary>
        /// Phương pháp: Extract Method - Thu thập dữ liệu từ form
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private UpdateEmployeeData LayDuLieuForm()
        {
            string matKhauMoi = LayMatKhauMoi();

            return new UpdateEmployeeData(
                txtHoTen.Text.Trim(),
                txtSoDienThoai.Text.Trim(),
                txtDiaChi.Text.Trim(),
                dtpNgaySinh.Value,
                cmbVaiTro.Text,
                cboTrangThai.Text,
                matKhauMoi
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Lấy mật khẩu mới (nếu có)
        /// </summary>
        private string LayMatKhauMoi()
        {
            return string.IsNullOrWhiteSpace(txtMatKhau.Text)
                ? null
                : txtMatKhau.Text.Trim();
        }
        #endregion

        #region Validation
        /// <summary>
        /// Phương pháp: Chain of Responsibility Pattern - Chuỗi validation
        /// Guard Clauses: Kiểm tra từng điều kiện và return sớm
        /// </summary>
        private bool KiemTraHopLe(UpdateEmployeeData data)
        {
            if (!KiemTraHoTen(data.HoTen))
                return false;

            if (!KiemTraVaiTro())
                return false;

            if (!KiemTraTrangThai())
                return false;

            if (!KiemTraMatKhauKhop(data.MatKhauMoi))
                return false;

            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra họ tên
        /// </summary>
        private bool KiemTraHoTen(string hoTen)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                HienThiLoiValidation("Vui lòng nhập Họ và tên!", txtHoTen);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra vai trò
        /// </summary>
        private bool KiemTraVaiTro()
        {
            if (cmbVaiTro.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn Vai trò!",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra trạng thái
        /// </summary>
        private bool KiemTraTrangThai()
        {
            if (cboTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn Trạng thái!",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra mật khẩu khớp
        /// </summary>
        private bool KiemTraMatKhauKhop(string matKhauMoi)
        {
            // Nếu không nhập mật khẩu mới thì bỏ qua validation
            if (matKhauMoi == null)
                return true;

            if (matKhauMoi != txtNhapLaiMatKhau.Text.Trim())
            {
                MessageBox.Show(
                    "Mật khẩu nhập lại không khớp!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtNhapLaiMatKhau.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method + DRY - Hiển thị lỗi validation
        /// </summary>
        private void HienThiLoiValidation(string message, Control control)
        {
            MessageBox.Show(
                message,
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            control.Focus();
        }
        #endregion

        #region Business Logic
        /// <summary>
        /// Phương pháp: Extract Method - Thực hiện cập nhật nhân viên
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private bool ThucHienCapNhatNhanVien(UpdateEmployeeData data)
        {
            return _bus.SuaNhanVien(
                _tenDangNhap,
                data.HoTen,
                data.SoDienThoai,
                data.DiaChi,
                data.NgaySinh,
                data.VaiTro,
                data.MatKhauMoi
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Cập nhật trạng thái riêng
        /// </summary>
        private bool ThucHienCapNhatTrangThai(string trangThai)
        {
            return _bus.CapNhatTrangThai(_tenDangNhap, trangThai);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Thực hiện cả 2 cập nhật
        /// Composite Pattern: Gộp 2 operations
        /// </summary>
        private bool ThucHienCapNhatDayDu(UpdateEmployeeData data)
        {
            bool ketQuaNhanVien = ThucHienCapNhatNhanVien(data);
            bool ketQuaTrangThai = ThucHienCapNhatTrangThai(data.TrangThai);

            return ketQuaNhanVien || ketQuaTrangThai;
        }
        #endregion

        #region Result Handlers
        /// <summary>
        /// Phương pháp: Extract Method - Xử lý cập nhật thành công
        /// </summary>
        private void XuLyCapNhatThanhCong()
        {
            MessageBox.Show(
                "Cập nhật thông tin nhân viên thành công!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý cập nhật thất bại
        /// </summary>
        private void XuLyCapNhatThatBai()
        {
            MessageBox.Show(
                "Cập nhật thất bại! Vui lòng thử lại.",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng cập nhật
        /// 1. Thu thập dữ liệu
        /// 2. Validate
        /// 3. Thực hiện cập nhật
        /// 4. Xử lý kết quả
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Bước 1: Thu thập dữ liệu (Extract Method)
            var data = LayDuLieuForm();

            // Bước 2: Validate (Guard Clauses + Chain of Responsibility)
            if (!KiemTraHopLe(data))
            {
                return; // Early return - Fail Fast
            }

            // Bước 3: Thực hiện cập nhật (Extract Method + Composite Pattern)
            bool ketQua = ThucHienCapNhatDayDu(data);

            // Bước 4: Xử lý kết quả (Strategy Pattern)
            if (ketQua)
            {
                XuLyCapNhatThanhCong();
            }
            else
            {
                XuLyCapNhatThatBai();
            }
        }

        /// <summary>
        /// Event handler: Hủy và đóng form
        /// </summary>
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}