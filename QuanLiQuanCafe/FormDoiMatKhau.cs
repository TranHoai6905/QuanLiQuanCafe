using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class FormDoiMatKhau : Form
    {
        private readonly TaiKhoanBUS _taiKhoanBUS;

        public FormDoiMatKhau()
        {
            InitializeComponent();
            _taiKhoanBUS = new TaiKhoanBUS(); // tạo instance BUS
        }

        // ===================== Lấy dữ liệu từ form =====================
        private (string tenDangNhap, string matKhauCu, string matKhauMoi) LayDuLieuTuForm()
        {
            return (
                txtTenDangNhap.Text.Trim(),
                txtMatKhauCu.Text,
                txtMatKhauMoi.Text
            );
        }

        // ===================== Validate dữ liệu =====================
        private bool KiemTraDuLieu((string tenDangNhap, string matKhauCu, string matKhauMoi) data, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(data.tenDangNhap) ||
                string.IsNullOrWhiteSpace(data.matKhauCu) ||
                string.IsNullOrWhiteSpace(data.matKhauMoi))
            {
                thongBao = "Vui lòng nhập đầy đủ thông tin!";
                return false;
            }

            if (data.matKhauCu == data.matKhauMoi)
            {
                thongBao = "Mật khẩu mới phải khác mật khẩu cũ!";
                return false;
            }

            thongBao = "";
            return true;
        }

        // ===================== Sự kiện nút Đổi mật khẩu =====================
        //private void btnDoiMatKhau_Click(object sender, EventArgs e)
        //{
        //    var data = LayDuLieuTuForm();

        //    if (!KiemTraDuLieu(data, out string thongBao))
        //    {
        //        MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    bool ketQua = _taiKhoanBUS.DoiMatKhau(data.tenDangNhap, data.matKhauCu, data.matKhauMoi);

        //    if (ketQua)
        //    {
        //        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        this.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Đổi mật khẩu thất bại! Kiểm tra lại tài khoản hoặc mật khẩu cũ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // ===================== Sự kiện nút Hủy =====================
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
