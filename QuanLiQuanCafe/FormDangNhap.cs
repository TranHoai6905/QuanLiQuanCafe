using log4net;
using QuanLiQuanCafe.Models;
using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class FormDangNhap : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FormDangNhap));
        private readonly TaiKhoanBUS _taiKhoanBus = new TaiKhoanBUS();

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private LoginFormData LayDuLieuDangNhap()
            => new LoginFormData(txtTenDangNhap.Text.Trim(), txtMatKhau.Text.Trim());

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

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            var data = LayDuLieuDangNhap();

            if (!KiemTraHopLe(data, out string thongBao))
            {
                ShowWarning(thongBao);
                return;
            }

            try
            {
                LoginResult ketQua = _taiKhoanBus.DangNhap(data.Username, data.Password);

                switch (ketQua)
                {
                    case LoginResult.Success:
                        DangNhapThanhCong(data.Username);
                        break;
                    case LoginResult.WrongPassword:
                        ShowError("Sai mật khẩu!");
                        log.Warn($"Đăng nhập thất bại - Sai mật khẩu: '{data.Username}'");
                        break;
                    case LoginResult.AccountNotFound:
                        ShowError("Tài khoản không tồn tại!");
                        log.Warn($"Đăng nhập thất bại - Tài khoản không tồn tại: '{data.Username}'");
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                log.Error("Lỗi trong quá trình đăng nhập", ex);
            }
        }

        private void DangNhapThanhCong(string tenDangNhap)
        {
            MessageBox.Show("Đăng nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            log.Info($"Người dùng '{tenDangNhap}' đã đăng nhập thành công.");

            // TODO: Mở form chính
            // new FormMain().Show();
            this.Hide();
        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new FormDangKy().ShowDialog();
            Show();
        }

        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Chức năng quên mật khẩu đang được phát triển.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string msg)
            => MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void ShowWarning(string msg)
            => MessageBox.Show(msg, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}