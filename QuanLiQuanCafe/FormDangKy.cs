using log4net;
using QuanLiQuanCafe.Models;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form đăng ký tài khoản mới
    /// Áp dụng: Single Responsibility Principle, Extract Method, Guard Clauses
    /// </summary>
    public partial class FormDangKy : Form
    {
        #region Fields
        private static readonly ILog log = LogManager.GetLogger(typeof(FormDangKy));
        private readonly TaiKhoanBUS _bus = new TaiKhoanBUS();
        #endregion

        #region Constructor
        public FormDangKy()
        {
            InitializeComponent();
            KhoiTaoGiaTriMacDinh();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách logic khởi tạo ra method riêng
        /// </summary>
        private void KhoiTaoGiaTriMacDinh()
        {
            cmbVaiTro.SelectedIndex = 0; // Mặc định là Nhân viên
        }
        #endregion

        #region Data Collection
        /// <summary>
        /// Phương pháp: Extract Method - Thu thập dữ liệu từ form
        /// Nguyên tắc: Single Responsibility - chỉ làm 1 việc là lấy dữ liệu
        /// </summary>
        private RegisterFormData LayDuLieuDangKy()
        {
            return new RegisterFormData(
                txtHoTen.Text.Trim(),
                txtMatKhau.Text,
                txtNhapLaiMatKhau.Text,
                txtSoDienThoai.Text.Trim(),
                txtDiaChi.Text.Trim(),
                dtpNgaySinh.Value,
                cmbVaiTro.SelectedItem?.ToString() ?? "Nhân viên"
            );
        }
        #endregion

        #region Validation
        /// <summary>
        /// Phương pháp: Guard Clauses - Kiểm tra điều kiện sớm và return ngay
        /// Nguyên tắc: Fail Fast - phát hiện lỗi càng sớm càng tốt
        /// </summary>
        private bool KiemTraHopLe(RegisterFormData data, out string thongBao)
        {
            // Guard Clause 1: Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(data.FullName))
            {
                thongBao = "Vui lòng nhập họ và tên!";
                return false;
            }

            // Guard Clause 2: Kiểm tra số điện thoại rỗng
            if (string.IsNullOrWhiteSpace(data.Phone))
            {
                thongBao = "Vui lòng nhập số điện thoại!";
                return false;
            }

            // Guard Clause 3: Kiểm tra định dạng số điện thoại
            if (!long.TryParse(data.Phone, out _))
            {
                thongBao = "Số điện thoại chỉ được chứa chữ số!";
                return false;
            }

            // Guard Clause 4: Kiểm tra mật khẩu rỗng
            if (string.IsNullOrWhiteSpace(data.Password))
            {
                thongBao = "Vui lòng nhập mật khẩu!";
                return false;
            }

            // Guard Clause 5: Kiểm tra độ dài mật khẩu
            if (data.Password.Length < 6)
            {
                thongBao = "Mật khẩu phải có ít nhất 6 ký tự!";
                return false;
            }

            // Guard Clause 6: Kiểm tra mật khẩu khớp
            if (data.Password != data.ConfirmPassword)
            {
                thongBao = "Mật khẩu nhập lại không khớp!";
                return false;
            }

            // Guard Clause 7: Kiểm tra vai trò được chọn
            if (cmbVaiTro.SelectedIndex == -1)
            {
                thongBao = "Vui lòng chọn vai trò!";
                return false;
            }

            // Happy path - tất cả validation đều pass
            thongBao = string.Empty;
            return true;
        }
        #endregion

        #region Business Logic
        /// <summary>
        /// Phương pháp: Extract Method - Tách logic đăng ký ra method riêng
        /// Nguyên tắc: Single Responsibility - chỉ xử lý đăng ký
        /// </summary>
        private RegisterResult ThucHienDangKy(RegisterFormData data)
        {
            return _bus.DangKy(
                data.FullName,
                data.Password,
                data.Phone,
                data.Address,
                data.DateOfBirth,
                data.Role
            );
        }
        #endregion

        #region UI Handlers
        /// <summary>
        /// Phương pháp: Strategy Pattern (implicit) - Xử lý từng trường hợp kết quả khác nhau
        /// Nguyên tắc: Tell, Don't Ask - gọi method xử lý thay vì kiểm tra điều kiện
        /// </summary>
        private void XuLyKetQuaDangKy(RegisterResult ketQua, RegisterFormData data)
        {
            switch (ketQua)
            {
                case RegisterResult.Success:
                    XuLyDangKyThanhCong(data);
                    break;

                case RegisterResult.PhoneExists:
                    XuLyLoiSoDienThoaiTrung();
                    break;

                case RegisterResult.UsernameExists:
                    XuLyLoiTenDangNhapTrung();
                    break;

                case RegisterResult.InvalidInput:
                    XuLyLoiThieuThongTin();
                    break;

                default:
                    XuLyLoiKhongXacDinh(data);
                    break;
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách xử lý thành công
        /// </summary>
        private void XuLyDangKyThanhCong(RegisterFormData data)
        {
            MessageBox.Show(
                "Đăng ký thành công!\nVui lòng chờ quản lý duyệt tài khoản.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            log.Info($"Đăng ký thành công: {data.FullName} - SĐT: {data.Phone} - Vai trò: {data.Role}");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách xử lý lỗi SĐT trùng
        /// </summary>
        private void XuLyLoiSoDienThoaiTrung()
        {
            MessageBox.Show(
                "Số điện thoại này đã được sử dụng!",
                "Số điện thoại trùng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            txtSoDienThoai.Focus();
            txtSoDienThoai.SelectAll();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách xử lý lỗi tên đăng nhập trùng
        /// </summary>
        private void XuLyLoiTenDangNhapTrung()
        {
            MessageBox.Show(
                "Tên đăng nhập đã tồn tại!",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            txtHoTen.Focus();
            txtHoTen.SelectAll();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách xử lý lỗi thiếu thông tin
        /// </summary>
        private void XuLyLoiThieuThongTin()
        {
            MessageBox.Show(
                "Vui lòng điền đầy đủ thông tin!",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách xử lý lỗi không xác định
        /// </summary>
        private void XuLyLoiKhongXacDinh(RegisterFormData data)
        {
            MessageBox.Show(
                "Đăng ký thất bại. Vui lòng thử lại sau!",
                "Lỗi hệ thống",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            log.Error($"Đăng ký thất bại không xác định: {data.FullName} - SĐT: {data.Phone}");
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách xử lý lỗi validation
        /// </summary>
        private void XuLyLoiValidation(string errorMsg, RegisterFormData data)
        {
            MessageBox.Show(errorMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            log.Warn($"Đăng ký thất bại: {errorMsg} - Tên: {data.FullName}, SĐT: {data.Phone}");
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách xử lý lỗi SQL
        /// </summary>
        private void XuLyLoiSQL(SqlException sqlEx)
        {
            MessageBox.Show(
                $"Lỗi cơ sở dữ liệu:\n{sqlEx.Message}",
                "Lỗi CSDL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            log.Error("Lỗi SQL khi đăng ký", sqlEx);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Tách xử lý lỗi nghiêm trọng
        /// </summary>
        private void XuLyLoiNghiemTrong(Exception ex)
        {
            MessageBox.Show(
                $"Đã xảy ra lỗi không mong muốn:\n{ex.Message}",
                "Lỗi nghiêm trọng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop
            );
            log.Fatal("Lỗi nghiêm trọng khi đăng ký", ex);
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng xử lý chính
        /// 1. Thu thập dữ liệu
        /// 2. Validate
        /// 3. Xử lý business logic
        /// 4. Xử lý kết quả
        /// 5. Xử lý lỗi (nếu có)
        /// </summary>
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                // Bước 1: Thu thập dữ liệu (Extract Method)
                var data = LayDuLieuDangKy();

                // Bước 2: Validate (Guard Clauses)
                if (!KiemTraHopLe(data, out string errorMsg))
                {
                    XuLyLoiValidation(errorMsg, data);
                    return; // Early return - Fail Fast
                }

                // Bước 3: Thực hiện đăng ký (Extract Method)
                var ketQua = ThucHienDangKy(data);

                // Bước 4: Xử lý kết quả (Strategy Pattern)
                XuLyKetQuaDangKy(ketQua, data);
            }
            catch (SqlException sqlEx)
            {
                // Bước 5a: Xử lý lỗi SQL (Extract Method)
                XuLyLoiSQL(sqlEx);
            }
            catch (Exception ex)
            {
                // Bước 5b: Xử lý lỗi nghiêm trọng (Extract Method)
                XuLyLoiNghiemTrong(ex);
            }
            finally
            {
                // Bước 6: Cleanup (luôn chạy)
                log.Debug("Hoàn tất xử lý đăng ký");
            }
        }

        /// <summary>
        /// Event handler: Đóng form khi click link Đăng nhập
        /// </summary>
        private void lnkDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}