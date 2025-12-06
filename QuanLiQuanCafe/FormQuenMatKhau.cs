using log4net;
using QuanLiQuanCafe.Models;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form quên mật khẩu - Lấy lại mật khẩu qua SĐT xác thực
    /// Áp dụng: Single Responsibility Principle, Guard Clauses, Extract Method
    /// </summary>
    public partial class FormQuenMatKhau : Form
    {
        #region Fields
        private static readonly ILog log = LogManager.GetLogger(typeof(FormQuenMatKhau));
        private readonly TaiKhoanBUS _bus = new TaiKhoanBUS();
        #endregion

        #region Constructor
        public FormQuenMatKhau()
        {
            InitializeComponent();
        }
        #endregion

        #region Data Models
        /// <summary>
        /// Phương pháp: Value Object Pattern - Đóng gói dữ liệu form
        /// </summary>
        private class ForgotPasswordData
        {
            public string TenDangNhap { get; set; }
            public string SoDienThoai { get; set; }
            public string MatKhauMoi { get; set; }
            public string NhapLaiMatKhau { get; set; }

            public ForgotPasswordData(string tenDangNhap, string soDienThoai, string matKhauMoi, string nhapLai)
            {
                TenDangNhap = tenDangNhap;
                SoDienThoai = soDienThoai;
                MatKhauMoi = matKhauMoi;
                NhapLaiMatKhau = nhapLai;
            }
        }
        #endregion

        #region Data Collection
        /// <summary>
        /// Phương pháp: Extract Method - Thu thập và chuẩn hóa dữ liệu
        /// Nguyên tắc: Single Responsibility - chỉ làm việc lấy dữ liệu
        /// </summary>
        private ForgotPasswordData LayDuLieuForm()
        {
            return new ForgotPasswordData(
                txtTenDangNhap.Text.Trim(),
                txtSoDienThoai.Text.Trim(),
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
        private bool KiemTraHopLe(ForgotPasswordData data)
        {
            if (!KiemTraTenDangNhap(data.TenDangNhap))
                return false;

            if (!KiemTraSoDienThoaiKhongRong(data.SoDienThoai))
                return false;

            if (!KiemTraDinhDangSoDienThoai(data.SoDienThoai))
                return false;

            if (!KiemTraMatKhauMoiKhongRong(data.MatKhauMoi))
                return false;

            if (!KiemTraDoDaiMatKhau(data.MatKhauMoi))
                return false;

            if (!KiemTraMatKhauKhop(data.MatKhauMoi, data.NhapLaiMatKhau))
                return false;

            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra tên đăng nhập
        /// </summary>
        private bool KiemTraTenDangNhap(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                HienThiLoiThieuThongTin("Vui lòng nhập tên đăng nhập!", txtTenDangNhap);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra SĐT không rỗng
        /// </summary>
        private bool KiemTraSoDienThoaiKhongRong(string soDienThoai)
        {
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                HienThiLoiThieuThongTin("Vui lòng nhập số điện thoại!", txtSoDienThoai);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra định dạng SĐT
        /// </summary>
        private bool KiemTraDinhDangSoDienThoai(string soDienThoai)
        {
            if (!long.TryParse(soDienThoai, out _))
            {
                MessageBox.Show(
                    "Số điện thoại chỉ được chứa chữ số!",
                    "Sai định dạng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                FocusVaChonTatCa(txtSoDienThoai);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra mật khẩu không rỗng
        /// </summary>
        private bool KiemTraMatKhauMoiKhongRong(string matKhauMoi)
        {
            if (string.IsNullOrWhiteSpace(matKhauMoi))
            {
                HienThiLoiThieuThongTin("Vui lòng nhập mật khẩu mới!", txtMatKhauMoi);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra độ dài mật khẩu
        /// </summary>
        private bool KiemTraDoDaiMatKhau(string matKhauMoi)
        {
            if (matKhauMoi.Length < 6)
            {
                MessageBox.Show(
                    "Mật khẩu mới phải có ít nhất 6 ký tự!",
                    "Yêu cầu bảo mật",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                FocusVaChonTatCa(txtMatKhauMoi);
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
                    "Mật khẩu nhập lại không khớp!",
                    "Không khớp",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                FocusVaChonTatCa(txtNhapLai);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị lỗi thiếu thông tin
        /// DRY: Tránh lặp code MessageBox + Focus
        /// </summary>
        private void HienThiLoiThieuThongTin(string message, Guna.UI2.WinForms.Guna2TextBox textBox)
        {
            MessageBox.Show(
                message,
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            textBox.Focus();
        }
        #endregion

        #region Business Logic
        /// <summary>
        /// Phương pháp: Extract Method - Thực hiện lấy lại mật khẩu
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private bool ThucHienLayLaiMatKhau(ForgotPasswordData data)
        {
            return _bus.LayLaiMatKhau(data.TenDangNhap, data.SoDienThoai, data.MatKhauMoi);
        }
        #endregion

        #region UI Handlers
        /// <summary>
        /// Phương pháp: Extract Method - Xử lý thành công
        /// </summary>
        private void XuLyThanhCong(ForgotPasswordData data)
        {
            MessageBox.Show(
                "Lấy lại mật khẩu thành công!\n\n" +
                "Bạn có thể đăng nhập bằng mật khẩu mới ngay bây giờ.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            log.Info($"Lấy lại mật khẩu thành công - Tài khoản: {data.TenDangNhap} - SĐT: {data.SoDienThoai}");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý thất bại
        /// </summary>
        private void XuLyThatBai(ForgotPasswordData data)
        {
            MessageBox.Show(
                "Không thể lấy lại mật khẩu!\n\n" +
                "→ Tên đăng nhập hoặc số điện thoại không chính xác.\n" +
                "→ Vui lòng kiểm tra lại thông tin.",
                "Thông tin sai",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            log.Warn($"Lấy lại mật khẩu thất bại - Sai thông tin: {data.TenDangNhap} | {data.SoDienThoai}");
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi SQL
        /// </summary>
        private void XuLyLoiSQL(SqlException sqlEx)
        {
            MessageBox.Show(
                "Lỗi kết nối cơ sở dữ liệu!\n" +
                "Không thể xử lý yêu cầu lấy lại mật khẩu.\n" +
                "Vui lòng kiểm tra mạng và thử lại sau.",
                "Lỗi mạng/CSDL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            log.Error("Lỗi SQL khi lấy lại mật khẩu", sqlEx);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi nghiêm trọng
        /// </summary>
        private void XuLyLoiNghiemTrong(Exception ex)
        {
            MessageBox.Show(
                "Đã xảy ra lỗi không mong muốn!\n" +
                "Vui lòng liên hệ quản trị viên.\n\n" +
                $"Chi tiết: {ex.Message}",
                "Lỗi hệ thống",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop
            );

            log.Fatal("Lỗi nghiêm trọng khi lấy lại mật khẩu", ex);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Dọn dẹp sau khi xử lý
        /// Nguyên tắc: Security - xóa mật khẩu khỏi bộ nhớ
        /// </summary>
        private void DonDepSauXuLy(string tenDangNhap)
        {
            log.Debug($"Hoàn tất xử lý lấy lại mật khẩu - User: {tenDangNhap}");

            txtMatKhauMoi.Clear();
            txtNhapLai.Clear();

            if (txtTenDangNhap.CanFocus)
                txtTenDangNhap.Focus();
        }

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
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng xử lý
        /// 1. Thu thập dữ liệu
        /// 2. Validate
        /// 3. Xử lý business logic
        /// 4. Xử lý kết quả
        /// 5. Xử lý lỗi (nếu có)
        /// 6. Cleanup
        /// </summary>
        private void btnLayLai_Click(object sender, EventArgs e)
        {
            ForgotPasswordData data = null;

            try
            {
                data = LayDuLieuForm();

                if (!KiemTraHopLe(data))
                {
                    return;
                }

                bool ketQua = ThucHienLayLaiMatKhau(data);

                if (ketQua)
                {
                    XuLyThanhCong(data);
                }
                else
                {
                    XuLyThatBai(data);
                }
            }
            catch (SqlException sqlEx)
            {
                XuLyLoiSQL(sqlEx);
                throw;
            }
            catch (Exception ex)
            {
                XuLyLoiNghiemTrong(ex);
                throw;
            }
            finally
            {
                if (data != null)
                {
                    DonDepSauXuLy(data.TenDangNhap);
                }
            }
        }

        /// <summary>
        /// Event handler: Đóng form khi click link Trở về
        /// </summary>
        private void lnkTroVe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}