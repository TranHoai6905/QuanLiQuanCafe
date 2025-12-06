using Guna.UI2.WinForms;
using QuanLiQuanCafe.Helpers;
using QuanLiQuanCafe.Models;
using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    /// <summary>
    /// Form chính của ứng dụng (MDI Container)
    /// Áp dụng: Single Responsibility Principle, Strategy Pattern, Extract Method
    /// </summary>
    public partial class FormMain : Form
    {
        #region Fields
        private readonly string _currentUser;
        private readonly string _currentRole;
        private Form activeForm = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor nhận thông tin đăng nhập từ FormDangNhap
        /// </summary>
        public FormMain(string tenDangNhap, string vaiTro)
        {
            InitializeComponent();
            _currentUser = tenDangNhap ?? "";
            _currentRole = (vaiTro ?? "").Trim();
            KhoiTaoForm();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Khởi tạo form
        /// Nguyên tắc: Single Responsibility
        /// </summary>
        private void KhoiTaoForm()
        {
            CauHinhNutTaiKhoan();
            HienThiTrangChu();
            // Reset màu menu về nâu đậm
            // Reset menu về màu nâu + chữ trắng
            TabButtonManager.ResetMain(btnTrangChu, btnQuanLyTaiKhoan, btnQuanLyHoaDon, btnQuanLyMon, btnBaoCao);

            // Mặc định chọn Trang chủ
            TabButtonManager.SelectMain(btnTrangChu);
        }
        /// <summary>
        /// Đổi màu nút menu khi click – dùng riêng cho FormMain
        /// </summary>


        /// <summary>
        /// Phương pháp: Extract Method - Cấu hình nút tài khoản
        /// </summary>
        private void CauHinhNutTaiKhoan()
        {
            btnQuanLyTaiKhoan.Visible = true;
            btnQuanLyTaiKhoan.Text = "Tài khoản";
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị trang chủ ban đầu
        /// </summary>
        private void HienThiTrangChu()
        {
            CapNhatTieuDeForm("Trang chủ");
        }
        #endregion

        #region MDI Child Management
        /// <summary>
        /// Phương pháp: Template Method Pattern - Mở form con (MDI)
        /// 1. Đóng form con đang active
        /// 2. Set form mới là active
        /// 3. Cấu hình form con
        /// 4. Thêm vào panel
        /// 5. Hiển thị
        /// 6. Cập nhật tiêu đề
        /// </summary>
        private void OpenChildForm(Form childForm)
        {
            DongFormConHienTai();
            SetFormConMoi(childForm);
            CauHinhFormCon(childForm);
            ThemVaoPanel(childForm);
            HienThiFormCon(childForm);
            CapNhatTieuDeTheoFormCon(childForm);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Đóng form con hiện tại
        /// </summary>
        private void DongFormConHienTai()
        {
            activeForm?.Close();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Set form con mới
        /// </summary>
        private void SetFormConMoi(Form childForm)
        {
            activeForm = childForm;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Cấu hình form con (MDI)
        /// </summary>
        private void CauHinhFormCon(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Thêm form vào panel
        /// </summary>
        private void ThemVaoPanel(Form childForm)
        {
            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị form con
        /// </summary>
        private void HienThiFormCon(Form childForm)
        {
            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Cập nhật tiêu đề theo form con
        /// </summary>
        private void CapNhatTieuDeTheoFormCon(Form childForm)
        {
            CapNhatTieuDeForm(childForm.Text);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Đóng tất cả form con và xóa panel
        /// </summary>
        private void DongTatCaFormCon()
        {
            activeForm?.Close();
            activeForm = null;
            panelContent.Controls.Clear();
        }
        #endregion

        #region UI Updates
        /// <summary>
        /// Phương pháp: Extract Method - Cập nhật tiêu đề form chính
        /// Hiển thị: Tên ứng dụng - User - Role - Form hiện tại
        /// </summary>
        private void CapNhatTieuDeForm(string tenFormCon)
        {
            this.Text = $"MYU COFFEE - Xin chào {_currentUser} ({_currentRole}) - {tenFormCon}";
        }
        #endregion

        #region Role-Based Navigation
        /// <summary>
        /// Phương pháp: Strategy Pattern - Mở form tài khoản theo vai trò
        /// Quản lý: FormQuanLyTaiKhoan (quản lý tất cả tài khoản)
        /// Nhân viên: FormCapNhatTTCN (chỉ xem/sửa thông tin cá nhân)
        /// </summary>
        private void MoFormTaiKhoan()
        {
            if (LaQuanLy())
            {
                MoFormQuanLyTaiKhoan();
            }
            else
            {
                MoFormCapNhatThongTinCaNhan();
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Kiểm tra có phải quản lý không
        /// </summary>
        private bool LaQuanLy()
        {
            return string.Equals(_currentRole, "Quản lý", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Mở form quản lý tài khoản
        /// </summary>
        private void MoFormQuanLyTaiKhoan()
        {
            OpenChildForm(new FormQuanLyTaiKhoan());
        }

        /// <summary>
        /// Phương pháp: Extract Method - Mở form cập nhật thông tin cá nhân
        /// </summary>
        private void MoFormCapNhatThongTinCaNhan()
        {
            OpenChildForm(new FormCapNhatTTCN(_currentUser));
        }
        #endregion

        #region Feature Placeholders
        /// <summary>
        /// Phương pháp: Extract Method - Hiển thị thông báo chức năng đang phát triển
        /// DRY: Tránh lặp MessageBox ở nhiều nơi
        /// </summary>
        private void HienThiThongBaoDangPhatTrien()
        {
            MessageBox.Show(
                "Chức năng đang phát triển...",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        #endregion

        #region Event Handlers - Navigation
        /// <summary>
        /// Event handler: Nút Trang chủ
        /// Đóng tất cả form con và hiển thị trang chủ trống
        /// </summary>
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            TabButtonManager.SelectMain(btnTrangChu);
            VeTrangChu();
        }

        /// <summary>
        /// Phương pháp: Extract Method - Về trang chủ
        /// </summary>
        private void VeTrangChu()
        {
            DongTatCaFormCon();
            CapNhatTieuDeForm("Trang chủ");
        }

        /// <summary>
        /// Event handler: Nút Tài khoản
        /// Hiển thị form khác nhau theo vai trò (Strategy Pattern)
        /// </summary>
        private void btnQuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            TabButtonManager.SelectMain(btnQuanLyTaiKhoan);
            MoFormTaiKhoan();
        }

        /// <summary>
        /// Event handler: Nút Quản lý đồ uống (placeholder)
        /// </summary>


        /// <summary>
        /// Event handler: Nút Báo cáo (placeholder)
        /// </summary>
        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            TabButtonManager.SelectMain(btnBaoCao);
            HienThiThongBaoDangPhatTrien();
            // TODO: OpenChildForm(new FormBaoCao());
        }

        /// <summary>
        /// Event handler: Nút Quản lý hóa đơn (placeholder)
        /// </summary>
        private void btnQuanLyHoaDon_Click(object sender, EventArgs e)
        {
            TabButtonManager.SelectMain(btnQuanLyHoaDon);
            HienThiThongBaoDangPhatTrien();
            // TODO: OpenChildForm(new FormQuanLyHoaDon());
        }

        /// <summary>
        /// Event handler: Nút Quản lý món (đồ uống)
        /// Mở form danh sách món theo đúng quy trình MDI child
        /// </summary>
        private void btnQuanLyMon_Click(object sender, EventArgs e)
        {
            TabButtonManager.SelectMain(btnQuanLyMon);
            HienThiThongBaoDangPhatTrien();
            // TODO: OpenChildForm(new FormDanhSachMon());
        }
        #endregion
    }
}