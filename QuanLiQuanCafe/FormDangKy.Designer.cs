using System.Drawing;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    partial class FormDangKy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.panelLogin = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSoDienThoai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblNhapLaiMK = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMatKhau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblVaiTro = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblNgaySinh = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblDiachi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblHoTen = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblDangKy = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cmbVaiTro = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNhapLaiMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnDangKy = new Guna.UI2.WinForms.Guna2Button();
            this.lnkDangNhap = new System.Windows.Forms.LinkLabel();
            this.panelBlur = new System.Windows.Forms.Panel();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.Transparent;
            this.panelLogin.BorderRadius = 40;
            this.panelLogin.Controls.Add(this.lblSoDienThoai);
            this.panelLogin.Controls.Add(this.lblNhapLaiMK);
            this.panelLogin.Controls.Add(this.lblMatKhau);
            this.panelLogin.Controls.Add(this.lblVaiTro);
            this.panelLogin.Controls.Add(this.lblNgaySinh);
            this.panelLogin.Controls.Add(this.lblDiachi);
            this.panelLogin.Controls.Add(this.lblHoTen);
            this.panelLogin.Controls.Add(this.picLogo);
            this.panelLogin.Controls.Add(this.lblDangKy);
            this.panelLogin.Controls.Add(this.txtHoTen);
            this.panelLogin.Controls.Add(this.txtDiaChi);
            this.panelLogin.Controls.Add(this.txtSoDienThoai);
            this.panelLogin.Controls.Add(this.dtpNgaySinh);
            this.panelLogin.Controls.Add(this.cmbVaiTro);
            this.panelLogin.Controls.Add(this.txtMatKhau);
            this.panelLogin.Controls.Add(this.txtNhapLaiMatKhau);
            this.panelLogin.Controls.Add(this.btnDangKy);
            this.panelLogin.Controls.Add(this.lnkDangNhap);
            this.panelLogin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelLogin.Location = new System.Drawing.Point(80, 34);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.ShadowDecoration.BorderRadius = 40;
            this.panelLogin.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelLogin.ShadowDecoration.Depth = 35;
            this.panelLogin.ShadowDecoration.Enabled = true;
            this.panelLogin.Size = new System.Drawing.Size(547, 700);
            this.panelLogin.TabIndex = 0;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.BackColor = System.Drawing.Color.Transparent;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSoDienThoai.Location = new System.Drawing.Point(59, 331);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(99, 23);
            this.lblSoDienThoai.TabIndex = 17;
            this.lblSoDienThoai.Text = "Số điện thoại";
            // 
            // lblNhapLaiMK
            // 
            this.lblNhapLaiMK.BackColor = System.Drawing.Color.Transparent;
            this.lblNhapLaiMK.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNhapLaiMK.Location = new System.Drawing.Point(59, 559);
            this.lblNhapLaiMK.Name = "lblNhapLaiMK";
            this.lblNhapLaiMK.Size = new System.Drawing.Size(132, 23);
            this.lblNhapLaiMK.TabIndex = 16;
            this.lblNhapLaiMK.Text = "Nhập lại mật khẩu";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMatKhau.Location = new System.Drawing.Point(59, 502);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(70, 23);
            this.lblMatKhau.TabIndex = 15;
            this.lblMatKhau.Text = "Mật khẩu";
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblVaiTro.Location = new System.Drawing.Point(59, 445);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(51, 23);
            this.lblVaiTro.TabIndex = 14;
            this.lblVaiTro.Text = "Vai trò";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.BackColor = System.Drawing.Color.Transparent;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNgaySinh.Location = new System.Drawing.Point(59, 388);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(74, 23);
            this.lblNgaySinh.TabIndex = 13;
            this.lblNgaySinh.Text = "Ngày sinh";
            // 
            // lblDiachi
            // 
            this.lblDiachi.BackColor = System.Drawing.Color.Transparent;
            this.lblDiachi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDiachi.Location = new System.Drawing.Point(59, 274);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(51, 23);
            this.lblDiachi.TabIndex = 12;
            this.lblDiachi.Text = "Địa chỉ";
            // 
            // lblHoTen
            // 
            this.lblHoTen.BackColor = System.Drawing.Color.Transparent;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblHoTen.Location = new System.Drawing.Point(59, 217);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(73, 23);
            this.lblHoTen.TabIndex = 11;
            this.lblHoTen.Text = "Họ và tên";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::QuanLiQuanCafe.Properties.Resources.LogoCafe;
            this.picLogo.ImageRotate = 0F;
            this.picLogo.Location = new System.Drawing.Point(228, 32);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(90, 98);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblDangKy
            // 
            this.lblDangKy.BackColor = System.Drawing.Color.Transparent;
            this.lblDangKy.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold);
            this.lblDangKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblDangKy.Location = new System.Drawing.Point(205, 134);
            this.lblDangKy.Name = "lblDangKy";
            this.lblDangKy.Size = new System.Drawing.Size(137, 42);
            this.lblDangKy.TabIndex = 1;
            this.lblDangKy.Text = "Đăng Ký";
            this.lblDangKy.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHoTen
            // 
            this.txtHoTen.BackColor = System.Drawing.Color.Transparent;
            this.txtHoTen.BorderRadius = 20;
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtHoTen.Location = new System.Drawing.Point(202, 206);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "Họ và tên";
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(285, 41);
            this.txtHoTen.TabIndex = 2;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BackColor = System.Drawing.Color.Transparent;
            this.txtDiaChi.BorderRadius = 20;
            this.txtDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiaChi.DefaultText = "";
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiaChi.Location = new System.Drawing.Point(202, 263);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.PlaceholderText = "Địa chỉ";
            this.txtDiaChi.SelectedText = "";
            this.txtDiaChi.Size = new System.Drawing.Size(285, 41);
            this.txtDiaChi.TabIndex = 3;
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.BackColor = System.Drawing.Color.Transparent;
            this.txtSoDienThoai.BorderRadius = 20;
            this.txtSoDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoDienThoai.DefaultText = "";
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(202, 320);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PlaceholderText = "Số điện thoại";
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(285, 41);
            this.txtSoDienThoai.TabIndex = 4;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.BorderRadius = 20;
            this.dtpNgaySinh.Checked = true;
            this.dtpNgaySinh.FillColor = System.Drawing.Color.AntiqueWhite;
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgaySinh.Location = new System.Drawing.Point(202, 377);
            this.dtpNgaySinh.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgaySinh.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(285, 44);
            this.dtpNgaySinh.TabIndex = 5;
            this.dtpNgaySinh.Value = new System.DateTime(2025, 11, 20, 18, 13, 47, 398);
            // 
            // cmbVaiTro
            // 
            this.cmbVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.cmbVaiTro.BorderRadius = 20;
            this.cmbVaiTro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVaiTro.FocusedColor = System.Drawing.Color.Empty;
            this.cmbVaiTro.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cmbVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.cmbVaiTro.ItemHeight = 30;
            this.cmbVaiTro.Items.AddRange(new object[] {
            "Nhân viên",
            "Quản lý"});
            this.cmbVaiTro.Location = new System.Drawing.Point(202, 437);
            this.cmbVaiTro.Name = "cmbVaiTro";
            this.cmbVaiTro.Size = new System.Drawing.Size(285, 36);
            this.cmbVaiTro.TabIndex = 6;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.txtMatKhau.BorderRadius = 20;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "";
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMatKhau.Location = new System.Drawing.Point(202, 489);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '●';
            this.txtMatKhau.PlaceholderText = "Mật khẩu";
            this.txtMatKhau.SelectedText = "";
            this.txtMatKhau.Size = new System.Drawing.Size(285, 41);
            this.txtMatKhau.TabIndex = 7;
            // 
            // txtNhapLaiMatKhau
            // 
            this.txtNhapLaiMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.txtNhapLaiMatKhau.BorderRadius = 20;
            this.txtNhapLaiMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhapLaiMatKhau.DefaultText = "";
            this.txtNhapLaiMatKhau.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNhapLaiMatKhau.Location = new System.Drawing.Point(202, 546);
            this.txtNhapLaiMatKhau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNhapLaiMatKhau.Name = "txtNhapLaiMatKhau";
            this.txtNhapLaiMatKhau.PasswordChar = '●';
            this.txtNhapLaiMatKhau.PlaceholderText = "Nhập lại mật khẩu";
            this.txtNhapLaiMatKhau.SelectedText = "";
            this.txtNhapLaiMatKhau.Size = new System.Drawing.Size(285, 41);
            this.txtNhapLaiMatKhau.TabIndex = 8;
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.Transparent;
            this.btnDangKy.BorderRadius = 25;
            this.btnDangKy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(109)))), ((int)(((byte)(67)))));
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(131, 613);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(285, 45);
            this.btnDangKy.TabIndex = 9;
            this.btnDangKy.Text = "Đăng Ký";
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // lnkDangNhap
            // 
            this.lnkDangNhap.AutoSize = true;
            this.lnkDangNhap.BackColor = System.Drawing.Color.Transparent;
            this.lnkDangNhap.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lnkDangNhap.Location = new System.Drawing.Point(201, 674);
            this.lnkDangNhap.Name = "lnkDangNhap";
            this.lnkDangNhap.Size = new System.Drawing.Size(145, 13);
            this.lnkDangNhap.TabIndex = 10;
            this.lnkDangNhap.TabStop = true;
            this.lnkDangNhap.Text = "Đã có tài khoản? Đăng nhập";
            this.lnkDangNhap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDangNhap_LinkClicked);
            // 
            // panelBlur
            // 
            this.panelBlur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelBlur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBlur.Location = new System.Drawing.Point(0, 0);
            this.panelBlur.Name = "panelBlur";
            this.panelBlur.Size = new System.Drawing.Size(706, 769);
            this.panelBlur.TabIndex = 1;
            // 
            // FormDangKy
            // 
            this.BackgroundImage = global::QuanLiQuanCafe.Properties.Resources.bgLogin;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(706, 769);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelBlur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Ký - MYU COFFEE";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelLogin;
        private System.Windows.Forms.Panel panelBlur;
        private Guna.UI2.WinForms.Guna2PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDangKy;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox txtSoDienThoai;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgaySinh;
        private Guna.UI2.WinForms.Guna2ComboBox cmbVaiTro;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox txtNhapLaiMatKhau;
        private Guna.UI2.WinForms.Guna2Button btnDangKy;
        private System.Windows.Forms.LinkLabel lnkDangNhap;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHoTen;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoDienThoai;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNhapLaiMK;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMatKhau;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblVaiTro;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNgaySinh;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblDiachi;
    }
}