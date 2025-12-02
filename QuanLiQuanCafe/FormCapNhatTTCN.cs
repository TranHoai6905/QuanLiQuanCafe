using log4net;
using QuanLiQuanCafe.Models;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form cập nhật thông tin cá nhân
    /// Áp dụng: Single Responsibility Principle, Guard Clauses, Extract Method
    /// </summary>
    public partial class FormCapNhatTTCN : Form
    {
        #region Fields
        private static readonly ILog log = LogManager.GetLogger(typeof(FormCapNhatTTCN));
        private readonly TaiKhoanBUS _bus = new TaiKhoanBUS();
        private readonly string _currentUser;
        private TaiKhoan _taiKhoanHienTai;
        #endregion

        #region Constructor
        public FormCapNhatTTCN(string tenDangNhap)
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
            KhoiTaoComboBoxVaiTro();
            LoadThongTinCaNhan();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Khởi tạo ComboBox vai trò
        /// </summary>
        private void KhoiTaoComboBoxVaiTro()
        {
            if (cboVaiTro.Items.Count == 0)
            {
                cboVaiTro.Items.Add("Nhân viên");
                cboVaiTro.Items.Add("Quản lý");
            }
        }
        #endregion

        #region Data Loading
        /// <summary>
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng load dữ liệu
        /// 1. Lấy dữ liệu từ BUS
        /// 2. Hiển thị lên form
        /// 3. Xử lý lỗi (nếu có)
        /// 4. Cleanup
        /// </summary>
        private void LoadThongTinCaNhan()
        {
            try
            {
                _taiKhoanHienTai = LayThongTinTaiKhoan();

                if (_taiKhoanHienTai != null)
                {
                    HienThiThongTinLenForm(_taiKhoanHienTai);
                    log.Info($"Tải thông tin cá nhân thành công: {_currentUser}");
                }
                else
                {
                    XuLyKhongTimThayTaiKhoan();
                }
            }
            catch (SqlException sqlEx)
            {
                XuLyLoiSQL(sqlEx);
            }
            catch (Exception ex)
            {
                XuLyLoiKhongXacDinh(ex);
            }
            finally
            {
                DonDepSauLoad();
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Lấy thông tin tài khoản từ BUS
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private TaiKhoan LayThongTinTaiKhoan()
        {
            return _bus.LayThongTinTaiKhoan(_currentUser);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông tin lên form
        /// </summary>
        private void HienThiThongTinLenForm(TaiKhoan taiKhoan)
        {
            HienThiThongTinLabel(taiKhoan);
            HienThiThongTinTextBox(taiKhoan);
            HienThiVaiTro(taiKhoan);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông tin trên Label
        /// </summary>
        private void HienThiThongTinLabel(TaiKhoan taiKhoan)
        {
            lblTenTK.Text = $"Tên tài khoản: {taiKhoan.TenDangNhap}";
            lblMatKhau.Text = $"Mật khẩu: {taiKhoan.MatKhau ?? "(Chưa đặt)"}";
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông tin trên TextBox
        /// </summary>
        private void HienThiThongTinTextBox(TaiKhoan taiKhoan)
        {
            txtHoTen.Text = taiKhoan.HoTen ?? "";
            txtDiaChi.Text = taiKhoan.DiaChi ?? "";
            txtSoDienThoai.Text = taiKhoan.SoDienThoai ?? "";
            dtpNgaySinh.Value = taiKhoan.NgaySinh ?? DateTime.Today;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị vai trò
        /// </summary>
        private void HienThiVaiTro(TaiKhoan taiKhoan)
        {
            if (!string.IsNullOrEmpty(taiKhoan.VaiTro) && cboVaiTro.Items.Contains(taiKhoan.VaiTro))
            {
                cboVaiTro.SelectedItem = taiKhoan.VaiTro;
            }
            else
            {
                cboVaiTro.SelectedIndex = 0; // Mặc định "Nhân viên"
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý không tìm thấy tài khoản
        /// </summary>
        private void XuLyKhongTimThayTaiKhoan()
        {
            MessageBox.Show(
                "Không tìm thấy thông tin tài khoản!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            log.Warn($"Không tìm thấy tài khoản: {_currentUser}");
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi SQL
        /// </summary>
        private void XuLyLoiSQL(SqlException sqlEx)
        {
            MessageBox.Show(
                "Lỗi kết nối cơ sở dữ liệu!\nVui lòng kiểm tra lại mạng hoặc liên hệ quản trị viên.",
                "Lỗi CSDL",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            log.Error("Lỗi SQL khi tải thông tin cá nhân", sqlEx);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi không xác định
        /// </summary>
        private void XuLyLoiKhongXacDinh(Exception ex)
        {
            MessageBox.Show(
                "Đã xảy ra lỗi khi tải thông tin cá nhân!",
                "Lỗi hệ thống",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            log.Error("Lỗi không xác định khi tải thông tin cá nhân", ex);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Cleanup sau khi load
        /// </summary>
        private void DonDepSauLoad()
        {
            log.Debug($"Hoàn tất LoadThongTinCaNhan() cho tài khoản: {_currentUser}");

            if (txtHoTen.CanFocus)
                txtHoTen.Focus();
        }
        #endregion

        #region Data Collection
        /// <summary>
        /// Phương pháp: Value Object Pattern - Đóng gói dữ liệu cập nhật
        /// </summary>
        private class UpdateProfileData
        {
            public string HoTen { get; set; }
            public string DiaChi { get; set; }
            public string SoDienThoai { get; set; }
            public DateTime NgaySinh { get; set; }
            public string VaiTro { get; set; }

            public UpdateProfileData(string hoTen, string diaChi, string soDienThoai, DateTime ngaySinh, string vaiTro)
            {
                HoTen = hoTen;
                DiaChi = diaChi;
                SoDienThoai = soDienThoai;
                NgaySinh = ngaySinh;
                VaiTro = vaiTro;
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Thu thập dữ liệu từ form
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private UpdateProfileData LayDuLieuForm()
        {
            return new UpdateProfileData(
                txtHoTen.Text.Trim(),
                txtDiaChi.Text.Trim(),
                txtSoDienThoai.Text.Trim(),
                dtpNgaySinh.Value,
                cboVaiTro.Text
            );
        }
        #endregion

        #region Validation
        /// <summary>
        /// Phương pháp: Chain of Responsibility Pattern - Chuỗi validation
        /// Guard Clauses: Kiểm tra từng điều kiện và return sớm
        /// </summary>
        private bool KiemTraHopLe(UpdateProfileData data)
        {
            if (!KiemTraHoTen(data.HoTen))
                return false;

            if (!KiemTraSoDienThoaiKhongRong(data.SoDienThoai))
                return false;

            if (!KiemTraDinhDangSoDienThoai(data.SoDienThoai))
                return false;

            if (!KiemTraVaiTro())
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
                HienThiLoiValidation("Vui lòng nhập họ và tên!", txtHoTen);
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
                HienThiLoiValidation("Vui lòng nhập số điện thoại!", txtSoDienThoai);
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
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtSoDienThoai.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra vai trò
        /// </summary>
        private bool KiemTraVaiTro()
        {
            if (cboVaiTro.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Vui lòng chọn vai trò!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                cboVaiTro.Focus();
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
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            control.Focus();
        }
        #endregion

        #region Business Logic
        /// <summary>
        /// Phương pháp: Extract Method - Thực hiện cập nhật
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private bool ThucHienCapNhat(UpdateProfileData data)
        {
            return _bus.CapNhatThongTinCaNhan(
                _currentUser,
                data.HoTen,
                data.SoDienThoai,
                data.DiaChi,
                data.NgaySinh,
                data.VaiTro
            );
        }
        #endregion

        #region Result Handlers
        /// <summary>
        /// Phương pháp: Extract Method - Xử lý cập nhật thành công
        /// </summary>
        private void XuLyCapNhatThanhCong()
        {
            MessageBox.Show(
                "Cập nhật thông tin thành công!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            log.Info($"Cập nhật thông tin thành công: {_currentUser}");
            LoadThongTinCaNhan();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý cập nhật thất bại
        /// </summary>
        private void XuLyCapNhatThatBai()
        {
            MessageBox.Show(
                "Cập nhật thất bại. Vui lòng thử lại!",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý lỗi exception khi cập nhật
        /// </summary>
        private void XuLyLoiCapNhat(Exception ex)
        {
            MessageBox.Show(
                "Đã xảy ra lỗi hệ thống!",
                "Lỗi nghiêm trọng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            log.Error("Lỗi khi cập nhật thông tin cá nhân", ex);
        }
        #endregion

        #region Navigation
        /// <summary>
        /// Phương pháp: Extract Method - Mở form đổi mật khẩu
        /// </summary>
        private void MoFormDoiMatKhau()
        {
            this.Hide();

            using (var formDoiMatKhau = new FormDoiMatKhau(_currentUser))
            {
                formDoiMatKhau.ShowDialog();
            }

            this.Show();
            LoadThongTinCaNhan();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xử lý đăng xuất
        /// Confirmation Dialog: Yêu cầu xác nhận trước khi đăng xuất
        /// </summary>
        private void XuLyDangXuat()
        {
            if (XacNhanDangXuat())
            {
                log.Info($"Người dùng {_currentUser} đã đăng xuất.");
                Application.Restart();
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Xác nhận đăng xuất
        /// </summary>
        private bool XacNhanDangXuat()
        {
            return MessageBox.Show(
                "Bạn có chắc muốn đăng xuất?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) == DialogResult.Yes;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Phương pháp: Template Method Pattern - Định nghĩa luồng cập nhật
        /// 1. Thu thập dữ liệu
        /// 2. Validate
        /// 3. Thực hiện cập nhật
        /// 4. Xử lý kết quả
        /// 5. Xử lý lỗi (nếu có)
        /// </summary>
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Bước 1: Thu thập dữ liệu (Extract Method)
                var data = LayDuLieuForm();

                // Bước 2: Validate (Guard Clauses)
                if (!KiemTraHopLe(data))
                {
                    return; // Early return - Fail Fast
                }

                // Bước 3: Thực hiện cập nhật (Extract Method)
                bool ketQua = ThucHienCapNhat(data);

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
            catch (Exception ex)
            {
                // Bước 5: Xử lý lỗi (Extract Method)
                XuLyLoiCapNhat(ex);
            }
        }

        /// <summary>
        /// Event handler: Mở form đổi mật khẩu
        /// </summary>
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            MoFormDoiMatKhau();
        }

        /// <summary>
        /// Event handler: Đăng xuất
        /// </summary>
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            XuLyDangXuat();
        }
        #endregion

        
    }
}