using System;
using System.Windows.Forms;
using QuanLiQuanCafe.Models;
using log4net;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form đổi mật khẩu
    /// Áp dụng: Single Responsibility Principle, Guard Clauses, Extract Method
    /// </summary>
    public partial class FormDoiMatKhau : Form
    {
        #region Fields
        private static readonly ILog log = LogManager.GetLogger(typeof(FormDoiMatKhau));
        private readonly TaiKhoanBUS _bus = new TaiKhoanBUS();
        private readonly string _currentUser;
        #endregion

        #region Constructor
        public FormDoiMatKhau(string tenDangNhap)
        {
            InitializeComponent();
            _currentUser = tenDangNhap;
            KhoiTaoForm();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Khởi tạo form
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private void KhoiTaoForm()
        {
            HienThiTenNguoiDung();
            KhoaTextBoxTenNguoiDung();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị tên người dùng
        /// </summary>
        private void HienThiTenNguoiDung()
        {
            txtHoTen.Text = _currentUser;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Khóa textbox tên người dùng
        /// </summary>
        private void KhoaTextBoxTenNguoiDung()
        {
            txtHoTen.ReadOnly = true;
        }
        #endregion

        #region Data Models
        /// <summary>
        /// Phương pháp: Value Object Pattern - Đóng gói dữ liệu đổi mật khẩu
        /// </summary>
        private class ChangePasswordData
        {
            public string MatKhauCu { get; set; }
            public string MatKhauMoi { get; set; }
            public string NhapLaiMatKhau { get; set; }

            public ChangePasswordData(string matKhauCu, string matKhauMoi, string nhapLai)
            {
                MatKhauCu = matKhauCu;
                MatKhauMoi = matKhauMoi;
                NhapLaiMatKhau = nhapLai;
            }
        }
        #endregion

        #region Data Collection
        /// <summary>
        /// Phương pháp: Extract Method - Thu thập dữ liệu từ form
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private ChangePasswordData LayDuLieuForm()
        {
            return new ChangePasswordData(
                txtMatKhauCu.Text.Trim(),
                txtMatKhauMoi.Text.Trim(),
                txtNhapLai.Text.Trim()
            );
        }
        #endregion

        #region Validation
        /// <summary>
        /// Phương pháp: Chain of Responsibility Pattern - Chuỗi validation
        /// Guard Clauses: Kiểm tra từng điều kiện và return sớm
        /// </summary>
        private bool KiemTraHopLe(ChangePasswordData data)
        {
            if (!KiemTraMatKhauCuKhongRong(data.MatKhauCu))
                return false;

            if (!KiemTraMatKhauMoiKhongRong(data.MatKhauMoi))
                return false;

            if (!KiemTraDoDaiMatKhauMoi(data.MatKhauMoi))
                return false;

            if (!KiemTraMatKhauKhop(data.MatKhauMoi, data.NhapLaiMatKhau))
                return false;

            if (!KiemTraMatKhauMoiKhacCu(data.MatKhauMoi, data.MatKhauCu))
                return false;

            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra mật khẩu cũ không rỗng
        /// </summary>
        private bool KiemTraMatKhauCuKhongRong(string matKhauCu)
        {
            if (string.IsNullOrWhiteSpace(matKhauCu))
            {
                HienThiLoiValidation("Vui lòng nhập mật khẩu cũ!", txtMatKhauCu);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra mật khẩu mới không rỗng
        /// </summary>
        private bool KiemTraMatKhauMoiKhongRong(string matKhauMoi)
        {
            if (string.IsNullOrWhiteSpace(matKhauMoi))
            {
                HienThiLoiValidation("Vui lòng nhập mật khẩu mới!", txtMatKhauMoi);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra độ dài mật khẩu mới
        /// </summary>
        private bool KiemTraDoDaiMatKhauMoi(string matKhauMoi)
        {
            if (matKhauMoi.Length < 6)
            {
                MessageBox.Show(
                    "Mật khẩu mới phải có ít nhất 6 ký tự!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtMatKhauMoi.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra mật khẩu khớp
        /// </summary>
        private bool KiemTraMatKhauKhop(string matKhauMoi, string nhapLai)
        {
            if (matKhauMoi != nhapLai)
            {
                MessageBox.Show(
                    "Nhập lại mật khẩu không khớp!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                FocusVaChonTatCa(txtNhapLai);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra mật khẩu mới khác cũ
        /// </summary>
        private bool KiemTraMatKhauMoiKhacCu(string matKhauMoi, string matKhauCu)
        {
            if (matKhauMoi == matKhauCu)
            {
                MessageBox.Show(
                    "Mật khẩu mới không được trùng với mật khẩu cũ!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtMatKhauMoi.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method + DRY - Hiển thị lỗi validation
        /// </summary>
        private void HienThiLoiValidation(string message, Guna.UI2.WinForms.Guna2TextBox textBox)
        {
            MessageBox.Show(
                message,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            textBox.Focus();
        }
        #endregion

        #region Business Logic
        /// <summary>
        /// Phương pháp: Extract Method - Thực hiện đổi mật khẩu
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private bool ThucHienDoiMatKhau(ChangePasswordData data)
        {
            return _bus.DoiMatKhau(_currentUser, data.MatKhauCu, data.MatKhauMoi);
        }
        #endregion

        #region Result Handlers
        /// <summary>
        /// Phương pháp: Extract Method - Xử lý đổi mật khẩu thành công
        /// </summary>
        private void XuLyDoiMatKhauThanhCong()
        {
            MessageBox.Show(
                "Đổi mật khẩu thành công!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            log.Info($"Người dùng '{_currentUser}' đã đổi mật khẩu thành công.");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý mật khẩu cũ sai
        /// </summary>
        private void XuLyMatKhauCuSai()
        {
            MessageBox.Show(
                "Mật khẩu cũ không đúng!\nVui lòng kiểm tra lại.",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            FocusVaChonTatCa(txtMatKhauCu);
            log.Warn($"Đổi mật khẩu thất bại: Mật khẩu cũ sai - User: {_currentUser}");
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi exception
        /// </summary>
        private void XuLyLoiException(Exception ex)
        {
            MessageBox.Show(
                "Đã xảy ra lỗi hệ thống. Vui lòng thử lại!",
                "Lỗi nghiêm trọng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            log.Error("Lỗi khi đổi mật khẩu", ex);
        }
        #endregion

        #region UI Helpers
        /// <summary>
        /// Phương pháp: Extract Method + DRY - Focus và select all Guna2TextBox
        /// </summary>
        private void FocusVaChonTatCa(Guna.UI2.WinForms.Guna2TextBox textBox)
        {
            textBox.Focus();
            textBox.SelectAll();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng đổi mật khẩu
        /// 1. Thu thập dữ liệu
        /// 2. Validate
        /// 3. Thực hiện đổi mật khẩu
        /// 4. Xử lý kết quả
        /// 5. Xử lý lỗi (nếu có)
        /// </summary>
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // Bước 1: Thu thập dữ liệu (Extract Method)
                var data = LayDuLieuForm();

                // Bước 2: Validate (Guard Clauses + Chain of Responsibility)
                if (!KiemTraHopLe(data))
                {
                    return; // Early return - Fail Fast
                }

                // Bước 3: Thực hiện đổi mật khẩu (Extract Method)
                bool ketQua = ThucHienDoiMatKhau(data);

                // Bước 4: Xử lý kết quả (Strategy Pattern)
                if (ketQua)
                {
                    XuLyDoiMatKhauThanhCong();
                }
                else
                {
                    XuLyMatKhauCuSai();
                }
            }
            catch (Exception ex)
            {
                // Bước 5: Xử lý lỗi (Extract Method)
                XuLyLoiException(ex);
            }
        }

        /// <summary>
        /// Event handler: Đóng form
        /// </summary>
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}