using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLiQuanCafe
{
    partial class FormCapNhatTTCN
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDoiMatKhau = new Guna.UI2.WinForms.Guna2Button();
            this.panelInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblThongTinTaiKhoan = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboVaiTro = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblHoTen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiaChi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNgaySinh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtpNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblVaiTro = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSoDienThoai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTenTK = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMatKhau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnDangXuat = new Guna.UI2.WinForms.Guna2Button();
            this.panelMain.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnDoiMatKhau);
            this.panelMain.Controls.Add(this.panelInfo);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblTenTK);
            this.panelMain.Controls.Add(this.lblMatKhau);
            this.panelMain.Controls.Add(this.btnDangXuat);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(910, 711);
            this.panelMain.TabIndex = 0;
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.BorderRadius = 27;
            this.btnDoiMatKhau.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDoiMatKhau.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDoiMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDoiMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDoiMatKhau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(194)))), ((int)(((byte)(161)))));
            this.btnDoiMatKhau.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnDoiMatKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.btnDoiMatKhau.Location = new System.Drawing.Point(726, 157);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(150, 52);
            this.btnDoiMatKhau.TabIndex = 16;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(109)))), ((int)(((byte)(92)))));
            this.panelInfo.BorderThickness = 3;
            this.panelInfo.Controls.Add(this.lblThongTinTaiKhoan);
            this.panelInfo.Controls.Add(this.cboVaiTro);
            this.panelInfo.Controls.Add(this.lblHoTen);
            this.panelInfo.Controls.Add(this.txtHoTen);
            this.panelInfo.Controls.Add(this.lblDiaChi);
            this.panelInfo.Controls.Add(this.txtDiaChi);
            this.panelInfo.Controls.Add(this.lblNgaySinh);
            this.panelInfo.Controls.Add(this.dtpNgaySinh);
            this.panelInfo.Controls.Add(this.lblVaiTro);
            this.panelInfo.Controls.Add(this.lblSoDienThoai);
            this.panelInfo.Controls.Add(this.txtSoDienThoai);
            this.panelInfo.Controls.Add(this.btnXacNhan);
            this.panelInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.panelInfo.Location = new System.Drawing.Point(66, 253);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(785, 455);
            this.panelInfo.TabIndex = 0;
            // 
            // lblThongTinTaiKhoan
            // 
            this.lblThongTinTaiKhoan.AutoSize = false;
            this.lblThongTinTaiKhoan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(194)))), ((int)(((byte)(161)))));
            this.lblThongTinTaiKhoan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblThongTinTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold);
            this.lblThongTinTaiKhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblThongTinTaiKhoan.Location = new System.Drawing.Point(3, 3);
            this.lblThongTinTaiKhoan.Name = "lblThongTinTaiKhoan";
            this.lblThongTinTaiKhoan.Size = new System.Drawing.Size(779, 61);
            this.lblThongTinTaiKhoan.TabIndex = 16;
            this.lblThongTinTaiKhoan.Text = "Thông tin tài khoản";
            this.lblThongTinTaiKhoan.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboVaiTro
            // 
            this.cboVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.cboVaiTro.BorderRadius = 20;
            this.cboVaiTro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaiTro.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboVaiTro.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboVaiTro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboVaiTro.ItemHeight = 30;
            this.cboVaiTro.Items.AddRange(new object[] {
            "Nhân viên ",
            "Quản lý"});
            this.cboVaiTro.Location = new System.Drawing.Point(105, 328);
            this.cboVaiTro.Name = "cboVaiTro";
            this.cboVaiTro.Size = new System.Drawing.Size(156, 36);
            this.cboVaiTro.TabIndex = 16;
            // 
            // lblHoTen
            // 
            this.lblHoTen.BackColor = System.Drawing.Color.Transparent;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblHoTen.Location = new System.Drawing.Point(44, 94);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(77, 23);
            this.lblHoTen.TabIndex = 4;
            this.lblHoTen.Text = "Họ và tên:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BackColor = System.Drawing.Color.Transparent;
            this.txtHoTen.BorderRadius = 25;
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtHoTen.Location = new System.Drawing.Point(35, 124);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "";
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(382, 50);
            this.txtHoTen.TabIndex = 5;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.BackColor = System.Drawing.Color.Transparent;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblDiaChi.Location = new System.Drawing.Point(44, 207);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(55, 23);
            this.lblDiaChi.TabIndex = 6;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BackColor = System.Drawing.Color.Transparent;
            this.txtDiaChi.BorderRadius = 25;
            this.txtDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiaChi.DefaultText = "";
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiaChi.Location = new System.Drawing.Point(35, 237);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.PlaceholderText = "";
            this.txtDiaChi.SelectedText = "";
            this.txtDiaChi.Size = new System.Drawing.Size(382, 50);
            this.txtDiaChi.TabIndex = 7;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.BackColor = System.Drawing.Color.Transparent;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblNgaySinh.Location = new System.Drawing.Point(491, 208);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(78, 23);
            this.lblNgaySinh.TabIndex = 8;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.BackColor = System.Drawing.Color.Transparent;
            this.dtpNgaySinh.BorderRadius = 20;
            this.dtpNgaySinh.Checked = true;
            this.dtpNgaySinh.FillColor = System.Drawing.Color.PapayaWhip;
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgaySinh.Location = new System.Drawing.Point(491, 237);
            this.dtpNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(258, 50);
            this.dtpNgaySinh.TabIndex = 9;
            this.dtpNgaySinh.Value = new System.DateTime(2025, 11, 24, 15, 44, 30, 515);
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblVaiTro.Location = new System.Drawing.Point(44, 328);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(55, 23);
            this.lblVaiTro.TabIndex = 10;
            this.lblVaiTro.Text = "Vai trò:";
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.BackColor = System.Drawing.Color.Transparent;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblSoDienThoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblSoDienThoai.Location = new System.Drawing.Point(491, 94);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(103, 23);
            this.lblSoDienThoai.TabIndex = 12;
            this.lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.BackColor = System.Drawing.Color.Transparent;
            this.txtSoDienThoai.BorderRadius = 25;
            this.txtSoDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoDienThoai.DefaultText = "";
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(491, 124);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PlaceholderText = "";
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(258, 50);
            this.txtSoDienThoai.TabIndex = 13;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.Transparent;
            this.btnXacNhan.BorderRadius = 27;
            this.btnXacNhan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(347, 391);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(157, 55);
            this.btnXacNhan.TabIndex = 14;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(66, 37);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(406, 38);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "> Cập nhật thông tin cá nhân";
            this.lblTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTenTK
            // 
            this.lblTenTK.BackColor = System.Drawing.Color.Transparent;
            this.lblTenTK.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenTK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblTenTK.Location = new System.Drawing.Point(69, 128);
            this.lblTenTK.Name = "lblTenTK";
            this.lblTenTK.Size = new System.Drawing.Size(183, 24);
            this.lblTenTK.TabIndex = 2;
            this.lblTenTK.Text = "Tên tài khoản: nv1";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.lblMatKhau.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMatKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblMatKhau.Location = new System.Drawing.Point(69, 185);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(133, 24);
            this.lblMatKhau.TabIndex = 3;
            this.lblMatKhau.Text = "Mật khẩu: 123";
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.BackColor = System.Drawing.Color.Transparent;
            this.btnDangXuat.BorderRadius = 27;
            this.btnDangXuat.FillColor = System.Drawing.Color.Gray;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.Location = new System.Drawing.Point(726, 86);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(150, 55);
            this.btnDangXuat.TabIndex = 15;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // FormCapNhatTTCN
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(910, 711);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormCapNhatTTCN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thông tin cá nhân - MYU COFFEE";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna2Panel panelMain;
        private Guna2Panel panelInfo;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblTenTK;
        private Guna2HtmlLabel lblMatKhau;

        private Guna2HtmlLabel lblHoTen;
        private Guna2TextBox txtHoTen;
        private Guna2HtmlLabel lblDiaChi;
        private Guna2TextBox txtDiaChi;
        private Guna2HtmlLabel lblNgaySinh;
        private Guna2DateTimePicker dtpNgaySinh;
        private Guna2HtmlLabel lblVaiTro;
        private Guna2HtmlLabel lblSoDienThoai;
        private Guna2TextBox txtSoDienThoai;

        private Guna2Button btnXacNhan;
        private Guna2Button btnDangXuat;
        private Guna2ComboBox cboVaiTro;
        private Guna2HtmlLabel lblThongTinTaiKhoan;
        private Guna2Button btnDoiMatKhau;
    }
}