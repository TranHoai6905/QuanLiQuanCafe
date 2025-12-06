using QuanLiQuanCafe.Models;
using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form thêm nhân viên mới (dành cho Quản lý)
    /// Áp dụng: Single Responsibility Principle, Guard Clauses, Extract Method
    /// </summary>
    public partial class FormThemNhanVien : Form
    {
        #region Fields
        private readonly TaiKhoanBUS _bus = new TaiKhoanBUS();
        #endregion

        #region Constructor
        public FormThemNhanVien()
        {
            InitializeComponent();
        }
        #endregion

        #region Data Models
        /// <summary>
        /// Phương pháp: Value Object Pattern - Đóng gói dữ liệu nhân viên mới
        /// </summary>
        private class NewEmployeeData
        {
            public string HoTen { get; set; }
            public string MatKhau { get; set; }
            public string NhapLaiMatKhau { get; set; }
            public string SoDienThoai { get; set; }
            public string DiaChi { get; set; }
            public DateTime NgaySinh { get; set; }
            public string VaiTro { get; set; }

            public NewEmployeeData(string hoTen, string matKhau, string nhapLai, string sdt,
                                  string diaChi, DateTime ngaySinh, string vaiTro)
            {
                HoTen = hoTen;
                MatKhau = matKhau;
                NhapLaiMatKhau = nhapLai;
                SoDienThoai = sdt;
                DiaChi = diaChi;
                NgaySinh = ngaySinh;
                VaiTro = vaiTro;
            }
        }
        #endregion

        #region Data Collection
        /// <summary>
        /// Phương pháp: Extract Method - Thu thập dữ liệu từ form
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private NewEmployeeData LayDuLieuForm()
        {
            return new NewEmployeeData(
                txtHoTen.Text.Trim(),
                txtMatKhau.Text,
                txtNhapLaiMatKhau.Text,
                txtSoDienThoai.Text.Trim(),
                txtDiaChi.Text.Trim(),
                dtpNgaySinh.Value,
                LayVaiTro()
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Lấy vai trò (có giá trị mặc định)
        /// </summary>
        private string LayVaiTro()
        {
            return cmbVaiTro.SelectedItem?.ToString() ?? "Nhân viên";
        }
        #endregion

        #region Validation
        /// <summary>
        /// Phương pháp: Chain of Responsibility Pattern - Chuỗi validation
        /// Guard Clauses: Kiểm tra từng điều kiện và return sớm
        /// </summary>
        private bool KiemTraHopLe(NewEmployeeData data)
        {
            if (!KiemTraThongTinBatBuoc(data))
                return false;

            if (!KiemTraMatKhauKhop(data.MatKhau, data.NhapLaiMatKhau))
                return false;

            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra thông tin bắt buộc
        /// </summary>
        private bool KiemTraThongTinBatBuoc(NewEmployeeData data)
        {
            if (string.IsNullOrWhiteSpace(data.HoTen) ||
                string.IsNullOrWhiteSpace(data.MatKhau) ||
                string.IsNullOrWhiteSpace(data.SoDienThoai))
            {
                HienThiLoiThieuThongTin();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra mật khẩu khớp
        /// </summary>
        private bool KiemTraMatKhauKhop(string matKhau, string nhapLai)
        {
            if (matKhau != nhapLai)
            {
                HienThiLoiMatKhauKhongKhop();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị lỗi thiếu thông tin
        /// </summary>
        private void HienThiLoiThieuThongTin()
        {
            MessageBox.Show(
                "Vui lòng nhập đầy đủ thông tin bắt buộc!",
                "Thiếu thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị lỗi mật khẩu không khớp
        /// </summary>
        private void HienThiLoiMatKhauKhongKhop()
        {
            MessageBox.Show(
                "Mật khẩu không khớp!",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        #endregion

        #region Business Logic
        /// <summary>
        /// Phương pháp: Extract Method - Thực hiện thêm nhân viên
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private RegisterResult ThucHienThemNhanVien(NewEmployeeData data)
        {
            return _bus.ThemNhanVien(
                data.HoTen,
                data.MatKhau,
                data.SoDienThoai,
                data.DiaChi,
                data.NgaySinh,
                data.VaiTro
            );
        }
        #endregion

        #region Result Handlers
        /// <summary>
        /// Phương pháp: Strategy Pattern - Xử lý kết quả thêm nhân viên
        /// </summary>
        private void XuLyKetQua(RegisterResult ketQua)
        {
            if (ketQua == RegisterResult.Success)
            {
                XuLyThemThanhCong();
            }
            else
            {
                XuLyThemThatBai(ketQua);
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý thêm thành công
        /// </summary>
        private void XuLyThemThanhCong()
        {
            MessageBox.Show(
                "Thêm nhân viên thành công!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý thêm thất bại
        /// Strategy Pattern: Xử lý từng loại lỗi khác nhau
        /// </summary>
        private void XuLyThemThatBai(RegisterResult ketQua)
        {
            string thongBao = LayThongBaoLoi(ketQua);

            MessageBox.Show(
                thongBao,
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Phương pháp: Strategy Pattern - Lấy thông báo lỗi theo kết quả
        /// </summary>
        private string LayThongBaoLoi(RegisterResult ketQua)
        {
            switch (ketQua)
            {
                case RegisterResult.PhoneExists:
                    return "Số điện thoại đã tồn tại!";

                case RegisterResult.UsernameExists:
                    return "Tên đăng nhập đã tồn tại!";

                case RegisterResult.InvalidInput:
                    return "Thông tin không hợp lệ!";

                default:
                    return "Thêm thất bại! Vui lòng thử lại.";
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng thêm nhân viên
        /// 1. Thu thập dữ liệu
        /// 2. Validate
        /// 3. Thực hiện thêm
        /// 4. Xử lý kết quả
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Bước 1: Thu thập dữ liệu (Extract Method)
            var data = LayDuLieuForm();

            // Bước 2: Validate (Guard Clauses + Chain of Responsibility)
            if (!KiemTraHopLe(data))
            {
                return; // Early return - Fail Fast
            }

            // Bước 3: Thực hiện thêm (Extract Method)
            RegisterResult ketQua = ThucHienThemNhanVien(data);

            // Bước 4: Xử lý kết quả (Strategy Pattern)
            XuLyKetQua(ketQua);
        }

        /// <summary>
        /// Event handler: Hủy và đóng form
        /// </summary>
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}