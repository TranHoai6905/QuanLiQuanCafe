using log4net;
using QuanLiQuanCafe.Models;
using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form đăng nhập hệ thống
    /// Áp dụng: Single Responsibility Principle, Strategy Pattern, Extract Method
    /// </summary>
    public partial class FormDangNhap : Form
    {
        #region Fields
        private static readonly ILog log = LogManager.GetLogger(typeof(FormDangNhap));
        private readonly TaiKhoanBUS _taiKhoanBus = new TaiKhoanBUS();
        #endregion

        #region Constructor
        public FormDangNhap()
        {
            InitializeComponent();
        }
        #endregion

        #region Data Collection
        /// <summary>
        /// Phương pháp: Extract Method - Thu thập dữ liệu đăng nhập
        /// Nguyên tắc: Single Responsibility - chỉ làm việc lấy dữ liệu
        /// </summary>
        private LoginFormData LayDuLieuDangNhap()
            => new LoginFormData(txtTenDangNhap.Text.Trim(), txtMatKhau.Text.Trim());
        #endregion

        #region Validation
        /// <summary>
        /// Phương pháp: Guard Clauses - Kiểm tra dữ liệu hợp lệ
        /// Nguyên tắc: Fail Fast - phát hiện lỗi sớm
        /// </summary>
        private bool KiemTraHopLe(LoginFormData data, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(data.Username) || string.IsNullOrWhiteSpace(data.Password))
            {
                thongBao = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!";
                return false;
            }
            thongBao = string.Empty;
            return true;
        }
        #endregion

        #region Business Logic
        /// <summary>
        /// Phương pháp: Extract Method - Thực hiện đăng nhập
        /// Nguyên tắc: Single Responsibility - chỉ gọi BUS
        /// </summary>
        private LoginResult ThucHienDangNhap(LoginFormData data)
        {
            return _taiKhoanBus.DangNhap(data.Username, data.Password);
        }
        #endregion

        #region Result Handlers
        /// <summary>
        /// Phương pháp: Strategy Pattern - Xử lý kết quả đăng nhập
        /// Switch-case để phân luồng xử lý theo kết quả
        /// </summary>
        private void XuLyKetQuaDangNhap(LoginResult ketQua, LoginFormData data)
        {
            switch (ketQua)
            {
                case LoginResult.Success:
                    XuLyDangNhapThanhCong(data.Username);
                    break;

                case LoginResult.WrongPassword:
                    XuLyLoiSaiMatKhau(data.Username);
                    break;

                case LoginResult.AccountNotFound:
                    XuLyLoiTaiKhoanKhongTonTai(data.Username);
                    break;

                default:
                    XuLyLoiKhongXacDinh();
                    break;
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý đăng nhập thành công
        /// </summary>
        private void XuLyDangNhapThanhCong(string tenDangNhap)
        {
            log.Info($"Đăng nhập thành công: {tenDangNhap}");
            DangNhapThanhCong(tenDangNhap);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi sai mật khẩu
        /// </summary>
        private void XuLyLoiSaiMatKhau(string tenDangNhap)
        {
            log.Warn($"Đăng nhập thất bại - Sai mật khẩu: '{tenDangNhap}'");
            MessageBox.Show(
                "Sai mật khẩu!",
                "Lỗi đăng nhập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            FocusVaChonTatCa(txtMatKhau);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi tài khoản không tồn tại
        /// </summary>
        private void XuLyLoiTaiKhoanKhongTonTai(string tenDangNhap)
        {
            log.Warn($"Đăng nhập thất bại - Tài khoản không tồn tại: '{tenDangNhap}'");
            MessageBox.Show(
                "Tài khoản không tồn tại!",
                "Lỗi đăng nhập",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            FocusVaChonTatCa(txtTenDangNhap);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi không xác định
        /// </summary>
        private void XuLyLoiKhongXacDinh()
        {
            log.Error("Kết quả đăng nhập không xác định!");
            MessageBox.Show(
                "Lỗi hệ thống. Vui lòng thử lại sau!",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý validation thất bại
        /// </summary>
        private void XuLyValidationThatBai(string thongBao, string tenDangNhap)
        {
            log.Warn($"Đăng nhập thất bại - dữ liệu không hợp lệ: '{tenDangNhap}'");
            MessageBox.Show(
                thongBao,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi exception
        /// </summary>
        private void XuLyLoiException(Exception ex)
        {
            log.Error("Lỗi nghiêm trọng trong quá trình đăng nhập", ex);
            MessageBox.Show(
                "Đã xảy ra lỗi hệ thống!",
                "Lỗi nghiêm trọng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        #endregion

        #region UI Helpers
        /// <summary>
        /// Phương pháp: Extract Method + DRY - Focus và select all Guna2TextBox
        /// Tránh lặp code Focus() + SelectAll()
        /// </summary>
        private void FocusVaChonTatCa(Guna.UI2.WinForms.Guna2TextBox textBox)
        {
            textBox.Focus();
            textBox.SelectAll();
        }
        #endregion

        #region Navigation
        /// <summary>
        /// Phương pháp: Extract Method - Xử lý đăng nhập thành công và mở FormMain
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private void DangNhapThanhCong(string tenDangNhap)
        {
            try
            {
                string vaiTro = LayVaiTro(tenDangNhap);
                AnFormDangNhap();
                MoFormMain(tenDangNhap, vaiTro);
            }
            catch (Exception ex)
            {
                XuLyLoiMoFormMain(ex);
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Lấy vai trò người dùng
        /// </summary>
        private string LayVaiTro(string tenDangNhap)
        {
            return _taiKhoanBus.LayVaiTro(tenDangNhap);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Ẩn form đăng nhập
        /// </summary>
        private void AnFormDangNhap()
        {
            this.Hide();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Mở FormMain
        /// </summary>
        private void MoFormMain(string tenDangNhap, string vaiTro)
        {
            var formMain = new FormMain(tenDangNhap, vaiTro);
            formMain.FormClosed += (s, e) => this.Close();
            formMain.Show();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi mở FormMain
        /// </summary>
        private void XuLyLoiMoFormMain(Exception ex)
        {
            log.Error("Lỗi khi mở FormMain", ex);
            MessageBox.Show(
                "Không thể mở giao diện chính!",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng đăng nhập
        /// 1. Thu thập dữ liệu
        /// 2. Validate
        /// 3. Thực hiện đăng nhập
        /// 4. Xử lý kết quả
        /// 5. Xử lý lỗi (nếu có)
        /// </summary>
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                var data = LayDuLieuDangNhap();

                if (!KiemTraHopLe(data, out string thongBao))
                {
                    XuLyValidationThatBai(thongBao, data.Username);
                    return;
                }

                var ketQua = ThucHienDangNhap(data);
                XuLyKetQuaDangNhap(ketQua, data);
            }
            catch (Exception ex)
            {
                XuLyLoiException(ex);
            }
            finally
            {
                log.Info("Kết thúc quá trình đăng nhập.");
            }
        }

        /// <summary>
        /// Event handler: Mở form đăng ký
        /// </summary>
        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoFormDangKy();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Mở form đăng ký
        /// </summary>
        private void MoFormDangKy()
        {
            Hide();
            new FormDangKy().ShowDialog();
            Show();
        }

        /// <summary>
        /// Event handler: Mở form quên mật khẩu
        /// </summary>
        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoFormQuenMatKhau();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Mở form quên mật khẩu
        /// </summary>
        private void MoFormQuenMatKhau()
        {
            this.Hide();

            using (var formQuenMatKhau = new FormQuenMatKhau())
            {
                var result = formQuenMatKhau.ShowDialog();

                if (result == DialogResult.OK)
                {
                    HienThiThongBaoLayLaiMatKhauThanhCong();
                }
            }

            this.Show();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông báo lấy lại mật khẩu thành công
        /// </summary>
        private void HienThiThongBaoLayLaiMatKhauThanhCong()
        {
            MessageBox.Show(
                "Lấy lại mật khẩu thành công! Vui lòng đăng nhập lại.",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
        }
        #endregion
    }
}