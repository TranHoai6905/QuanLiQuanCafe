using QuanLiQuanCafe.Models;
using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class FormDangKy : Form
    {
        private readonly TaiKhoanBUS _bus = new TaiKhoanBUS();

        public FormDangKy()
        {
            InitializeComponent();
        }

        private RegisterFormData LayDuLieuDangKy()
            => new RegisterFormData(
                txtHoTen.Text.Trim(),
                txtMatKhau.Text,
                txtNhapLaiMatKhau.Text,
                txtSoDienThoai.Text.Trim(),
                txtDiaChi.Text.Trim(),
                dtpNgaySinh.Value,
                txtVaiTro.Text.Trim()
            );

        private bool KiemTraHopLe(RegisterFormData data, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(data.FullName) ||
                string.IsNullOrWhiteSpace(data.Password) ||
                string.IsNullOrWhiteSpace(data.ConfirmPassword) ||
                string.IsNullOrWhiteSpace(data.Phone))
            {
                thongBao = "Vui lòng nhập đầy đủ các trường bắt buộc!";
                return false;
            }

            if (data.Password != data.ConfirmPassword)
            {
                thongBao = "Mật khẩu nhập lại không khớp!";
                return false;
            }

            if (!long.TryParse(data.Phone, out _))
            {
                thongBao = "Số điện thoại chỉ được chứa chữ số!";
                return false;
            }

            thongBao = string.Empty;
            return true;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            var data = LayDuLieuDangKy();

            if (!KiemTraHopLe(data, out string thongBao))
            {
                MessageBox.Show(thongBao, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string vaiTro = string.IsNullOrWhiteSpace(data.Role) ? "Nhân viên" : data.Role;

            RegisterResult ketQua = _bus.DangKy(
                data.FullName,
                data.Password,
                data.Phone,
                data.Address,
                data.DateOfBirth,
                vaiTro
            );

            switch (ketQua)
            {
                case RegisterResult.Success:
                    MessageBox.Show("Đăng ký thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    break;
                case RegisterResult.UsernameExists:
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case RegisterResult.PhoneExists:
                    MessageBox.Show("Số điện thoại đã được sử dụng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("Đăng ký thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void lnkDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}