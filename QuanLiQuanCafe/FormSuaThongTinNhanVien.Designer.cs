namespace QuanLiQuanCafe
{
    partial class FormSuaThongTinNhanVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTrangThai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblHoTen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblDiaChi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSoDienThoai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblNgaySinh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblVaiTro = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMatKhau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblNhapLaiMK = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cmbVaiTro = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNhapLaiMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.lblTrangThai);
            this.panelMain.Controls.Add(this.cboTrangThai);
            this.panelMain.Controls.Add(this.picLogo);
            this.panelMain.Controls.Add(this.btnSua);
            this.panelMain.Controls.Add(this.btnHuy);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblHoTen);
            this.panelMain.Controls.Add(this.lblDiaChi);
            this.panelMain.Controls.Add(this.lblSoDienThoai);
            this.panelMain.Controls.Add(this.lblNgaySinh);
            this.panelMain.Controls.Add(this.lblVaiTro);
            this.panelMain.Controls.Add(this.lblMatKhau);
            this.panelMain.Controls.Add(this.lblNhapLaiMK);
            this.panelMain.Controls.Add(this.txtHoTen);
            this.panelMain.Controls.Add(this.txtDiaChi);
            this.panelMain.Controls.Add(this.txtSoDienThoai);
            this.panelMain.Controls.Add(this.dtpNgaySinh);
            this.panelMain.Controls.Add(this.cmbVaiTro);
            this.panelMain.Controls.Add(this.txtMatKhau);
            this.panelMain.Controls.Add(this.txtNhapLaiMatKhau);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.ShadowDecoration.BorderRadius = 40;
            this.panelMain.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelMain.ShadowDecoration.Enabled = true;
            this.panelMain.Size = new System.Drawing.Size(736, 494);
            this.panelMain.TabIndex = 0;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblTrangThai.Location = new System.Drawing.Point(221, 408);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(76, 23);
            this.lblTrangThai.TabIndex = 16;
            this.lblTrangThai.Text = "Trạng thái";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.cboTrangThai.BorderRadius = 20;
            this.cboTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FocusedColor = System.Drawing.Color.Empty;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboTrangThai.ItemHeight = 30;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Đang làm",
            "Nghỉ"});
            this.cboTrangThai.Location = new System.Drawing.Point(221, 437);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(140, 36);
            this.cboTrangThai.TabIndex = 15;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::QuanLiQuanCafe.Properties.Resources.LogoCafe;
            this.picLogo.ImageRotate = 0F;
            this.picLogo.Location = new System.Drawing.Point(32, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(90, 98);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 10;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(583, 428);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(108, 45);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "SỬA";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 10;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(454, 428);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(108, 45);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "HỦY";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(235, 43);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(282, 42);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SỬA NHÂN VIÊN";
            this.lblTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHoTen
            // 
            this.lblHoTen.BackColor = System.Drawing.Color.Transparent;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblHoTen.Location = new System.Drawing.Point(33, 131);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(73, 23);
            this.lblHoTen.TabIndex = 3;
            this.lblHoTen.Text = "Họ và tên";
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.BackColor = System.Drawing.Color.Transparent;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblDiaChi.Location = new System.Drawing.Point(434, 136);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(51, 23);
            this.lblDiaChi.TabIndex = 4;
            this.lblDiaChi.Text = "Địa chỉ";
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.BackColor = System.Drawing.Color.Transparent;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoDienThoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblSoDienThoai.Location = new System.Drawing.Point(434, 227);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(99, 23);
            this.lblSoDienThoai.TabIndex = 5;
            this.lblSoDienThoai.Text = "Số điện thoại";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.BackColor = System.Drawing.Color.Transparent;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblNgaySinh.Location = new System.Drawing.Point(434, 318);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(74, 23);
            this.lblNgaySinh.TabIndex = 6;
            this.lblNgaySinh.Text = "Ngày sinh";
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblVaiTro.Location = new System.Drawing.Point(32, 408);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(51, 23);
            this.lblVaiTro.TabIndex = 7;
            this.lblVaiTro.Text = "Vai trò";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblMatKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblMatKhau.Location = new System.Drawing.Point(32, 222);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(102, 23);
            this.lblMatKhau.TabIndex = 8;
            this.lblMatKhau.Text = "Mật khẩu mới";
            // 
            // lblNhapLaiMK
            // 
            this.lblNhapLaiMK.BackColor = System.Drawing.Color.Transparent;
            this.lblNhapLaiMK.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblNhapLaiMK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblNhapLaiMK.Location = new System.Drawing.Point(33, 313);
            this.lblNhapLaiMK.Name = "lblNhapLaiMK";
            this.lblNhapLaiMK.Size = new System.Drawing.Size(132, 23);
            this.lblNhapLaiMK.TabIndex = 9;
            this.lblNhapLaiMK.Text = "Nhập lại mật khẩu";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderRadius = 20;
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtHoTen.Location = new System.Drawing.Point(32, 165);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "Họ và tên";
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(330, 46);
            this.txtHoTen.TabIndex = 8;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BorderRadius = 20;
            this.txtDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiaChi.DefaultText = "";
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiaChi.Location = new System.Drawing.Point(419, 165);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.PlaceholderText = "Địa chỉ";
            this.txtDiaChi.SelectedText = "";
            this.txtDiaChi.Size = new System.Drawing.Size(285, 46);
            this.txtDiaChi.TabIndex = 9;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.BorderRadius = 20;
            this.txtSoDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoDienThoai.DefaultText = "";
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(419, 256);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PlaceholderText = "Số điện thoại";
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(285, 46);
            this.txtSoDienThoai.TabIndex = 10;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.BorderRadius = 20;
            this.dtpNgaySinh.Checked = true;
            this.dtpNgaySinh.FillColor = System.Drawing.Color.PapayaWhip;
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgaySinh.Location = new System.Drawing.Point(419, 347);
            this.dtpNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(285, 46);
            this.dtpNgaySinh.TabIndex = 11;
            this.dtpNgaySinh.Value = new System.DateTime(2025, 11, 26, 14, 6, 48, 892);
            // 
            // cmbVaiTro
            // 
            this.cmbVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.cmbVaiTro.BorderRadius = 20;
            this.cmbVaiTro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVaiTro.FocusedColor = System.Drawing.Color.Empty;
            this.cmbVaiTro.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cmbVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbVaiTro.ItemHeight = 30;
            this.cmbVaiTro.Items.AddRange(new object[] {
            "Nhân viên",
            "Quản lý"});
            this.cmbVaiTro.Location = new System.Drawing.Point(32, 437);
            this.cmbVaiTro.Name = "cmbVaiTro";
            this.cmbVaiTro.Size = new System.Drawing.Size(140, 36);
            this.cmbVaiTro.TabIndex = 12;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BorderRadius = 20;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "";
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMatKhau.Location = new System.Drawing.Point(32, 256);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '●';
            this.txtMatKhau.PlaceholderText = "Mật khẩu mới (để trống nếu không đổi)";
            this.txtMatKhau.SelectedText = "";
            this.txtMatKhau.Size = new System.Drawing.Size(329, 46);
            this.txtMatKhau.TabIndex = 13;
            // 
            // txtNhapLaiMatKhau
            // 
            this.txtNhapLaiMatKhau.BorderRadius = 20;
            this.txtNhapLaiMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhapLaiMatKhau.DefaultText = "";
            this.txtNhapLaiMatKhau.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNhapLaiMatKhau.Location = new System.Drawing.Point(33, 347);
            this.txtNhapLaiMatKhau.Name = "txtNhapLaiMatKhau";
            this.txtNhapLaiMatKhau.PasswordChar = '●';
            this.txtNhapLaiMatKhau.PlaceholderText = "Nhập lại mật khẩu";
            this.txtNhapLaiMatKhau.SelectedText = "";
            this.txtNhapLaiMatKhau.Size = new System.Drawing.Size(330, 46);
            this.txtNhapLaiMatKhau.TabIndex = 14;
            // 
            // FormSuaThongTinNhanVien
            // 
            this.ClientSize = new System.Drawing.Size(736, 494);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSuaThongTinNhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sửa Thông Tin Nhân Viên - MYU COFFEE";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHoTen;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDiaChi;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoDienThoai;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNgaySinh;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVaiTro;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMatKhau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNhapLaiMK;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox txtSoDienThoai;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgaySinh;
        private Guna.UI2.WinForms.Guna2ComboBox cmbVaiTro;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox txtNhapLaiMatKhau;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTrangThai;
        private Guna.UI2.WinForms.Guna2ComboBox cboTrangThai;
    }
}