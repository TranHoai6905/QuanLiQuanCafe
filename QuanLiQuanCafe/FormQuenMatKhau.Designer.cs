using System.Drawing;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    partial class FormQuenMatKhau
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
            this.panelLogin = new Guna.UI2.WinForms.Guna2Panel();
            this.picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTenDangNhap = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTenDangNhap = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSoDienThoai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMatKhauMoi = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtMatKhauMoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNhapLai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtNhapLai = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLayLai = new Guna.UI2.WinForms.Guna2Button();
            this.lnkTroVe = new System.Windows.Forms.LinkLabel();
            this.panelBlur = new System.Windows.Forms.Panel();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.Transparent;
            this.panelLogin.BorderRadius = 40;
            this.panelLogin.Controls.Add(this.picLogo);
            this.panelLogin.Controls.Add(this.lblTitle);
            this.panelLogin.Controls.Add(this.lblTenDangNhap);
            this.panelLogin.Controls.Add(this.txtTenDangNhap);
            this.panelLogin.Controls.Add(this.lblSoDienThoai);
            this.panelLogin.Controls.Add(this.txtSoDienThoai);
            this.panelLogin.Controls.Add(this.lblMatKhauMoi);
            this.panelLogin.Controls.Add(this.txtMatKhauMoi);
            this.panelLogin.Controls.Add(this.lblNhapLai);
            this.panelLogin.Controls.Add(this.txtNhapLai);
            this.panelLogin.Controls.Add(this.btnLayLai);
            this.panelLogin.Controls.Add(this.lnkTroVe);
            this.panelLogin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelLogin.Location = new System.Drawing.Point(71, 39);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.ShadowDecoration.BorderRadius = 40;
            this.panelLogin.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelLogin.ShadowDecoration.Depth = 35;
            this.panelLogin.ShadowDecoration.Enabled = true;
            this.panelLogin.Size = new System.Drawing.Size(569, 622);
            this.panelLogin.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::QuanLiQuanCafe.Properties.Resources.LogoCafe;
            this.picLogo.ImageRotate = 0F;
            this.picLogo.Location = new System.Drawing.Point(224, 39);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(120, 121);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblTitle.Location = new System.Drawing.Point(167, 165);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(291, 53);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Quên mật khẩu";
            this.lblTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.BackColor = System.Drawing.Color.Transparent;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblTenDangNhap.Location = new System.Drawing.Point(57, 247);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(152, 30);
            this.lblTenDangNhap.TabIndex = 2;
            this.lblTenDangNhap.Text = "Tên đăng nhập :";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.BackColor = System.Drawing.Color.Transparent;
            this.txtTenDangNhap.BorderRadius = 25;
            this.txtTenDangNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDangNhap.DefaultText = "";
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTenDangNhap.Location = new System.Drawing.Point(203, 236);
            this.txtTenDangNhap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.PlaceholderText = "Vui lòng nhập tên...";
            this.txtTenDangNhap.SelectedText = "";
            this.txtTenDangNhap.Size = new System.Drawing.Size(308, 50);
            this.txtTenDangNhap.TabIndex = 3;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.BackColor = System.Drawing.Color.Transparent;
            this.lblSoDienThoai.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoDienThoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblSoDienThoai.Location = new System.Drawing.Point(57, 317);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(135, 30);
            this.lblSoDienThoai.TabIndex = 4;
            this.lblSoDienThoai.Text = "Số điện thoại :";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.BackColor = System.Drawing.Color.Transparent;
            this.txtSoDienThoai.BorderRadius = 25;
            this.txtSoDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoDienThoai.DefaultText = "";
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(203, 306);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PlaceholderText = "Nhập số điện thoại đã đăng ký...";
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(308, 50);
            this.txtSoDienThoai.TabIndex = 5;
            // 
            // lblMatKhauMoi
            // 
            this.lblMatKhauMoi.BackColor = System.Drawing.Color.Transparent;
            this.lblMatKhauMoi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblMatKhauMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblMatKhauMoi.Location = new System.Drawing.Point(57, 387);
            this.lblMatKhauMoi.Name = "lblMatKhauMoi";
            this.lblMatKhauMoi.Size = new System.Drawing.Size(141, 30);
            this.lblMatKhauMoi.TabIndex = 6;
            this.lblMatKhauMoi.Text = "Mật khẩu mới :";
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.BackColor = System.Drawing.Color.Transparent;
            this.txtMatKhauMoi.BorderRadius = 25;
            this.txtMatKhauMoi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhauMoi.DefaultText = "";
            this.txtMatKhauMoi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMatKhauMoi.Location = new System.Drawing.Point(203, 376);
            this.txtMatKhauMoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '●';
            this.txtMatKhauMoi.PlaceholderText = "Nhập mật khẩu mới...";
            this.txtMatKhauMoi.SelectedText = "";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(308, 50);
            this.txtMatKhauMoi.TabIndex = 7;
            // 
            // lblNhapLai
            // 
            this.lblNhapLai.BackColor = System.Drawing.Color.Transparent;
            this.lblNhapLai.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblNhapLai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lblNhapLai.Location = new System.Drawing.Point(57, 457);
            this.lblNhapLai.Name = "lblNhapLai";
            this.lblNhapLai.Size = new System.Drawing.Size(181, 30);
            this.lblNhapLai.TabIndex = 8;
            this.lblNhapLai.Text = "Nhập lại mật khẩu :";
            // 
            // txtNhapLai
            // 
            this.txtNhapLai.BackColor = System.Drawing.Color.Transparent;
            this.txtNhapLai.BorderRadius = 25;
            this.txtNhapLai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhapLai.DefaultText = "";
            this.txtNhapLai.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNhapLai.Location = new System.Drawing.Point(203, 446);
            this.txtNhapLai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNhapLai.Name = "txtNhapLai";
            this.txtNhapLai.PasswordChar = '●';
            this.txtNhapLai.PlaceholderText = "Nhập lại mật khẩu mới...";
            this.txtNhapLai.SelectedText = "";
            this.txtNhapLai.Size = new System.Drawing.Size(308, 50);
            this.txtNhapLai.TabIndex = 9;
            // 
            // btnLayLai
            // 
            this.btnLayLai.BackColor = System.Drawing.Color.Transparent;
            this.btnLayLai.BorderRadius = 27;
            this.btnLayLai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(109)))), ((int)(((byte)(67)))));
            this.btnLayLai.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnLayLai.ForeColor = System.Drawing.Color.White;
            this.btnLayLai.Location = new System.Drawing.Point(155, 518);
            this.btnLayLai.Name = "btnLayLai";
            this.btnLayLai.Size = new System.Drawing.Size(258, 51);
            this.btnLayLai.TabIndex = 10;
            this.btnLayLai.Text = "Lấy lại mật khẩu";
            this.btnLayLai.Click += new System.EventHandler(this.btnLayLai_Click);
            // 
            // lnkTroVe
            // 
            this.lnkTroVe.AutoSize = true;
            this.lnkTroVe.BackColor = System.Drawing.Color.Transparent;
            this.lnkTroVe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lnkTroVe.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(48)))), ((int)(((byte)(33)))));
            this.lnkTroVe.Location = new System.Drawing.Point(228, 590);
            this.lnkTroVe.Name = "lnkTroVe";
            this.lnkTroVe.Size = new System.Drawing.Size(121, 17);
            this.lnkTroVe.TabIndex = 11;
            this.lnkTroVe.TabStop = true;
            this.lnkTroVe.Text = "Trở về đăng nhập";
            this.lnkTroVe.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTroVe_LinkClicked);
            // 
            // panelBlur
            // 
            this.panelBlur.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelBlur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBlur.Location = new System.Drawing.Point(0, 0);
            this.panelBlur.Name = "panelBlur";
            this.panelBlur.Size = new System.Drawing.Size(711, 700);
            this.panelBlur.TabIndex = 1;
            // 
            // FormQuenMatKhau
            // 
            this.BackgroundImage = global::QuanLiQuanCafe.Properties.Resources.bgLogin;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(711, 700);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelBlur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormQuenMatKhau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quên mật khẩu - MYU COFFEE";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelLogin;
        private System.Windows.Forms.Panel panelBlur;
        private Guna.UI2.WinForms.Guna2PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTenDangNhap;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDangNhap;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblSoDienThoai;
        private Guna.UI2.WinForms.Guna2TextBox txtSoDienThoai;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblMatKhauMoi;
        private Guna.UI2.WinForms.Guna2TextBox txtMatKhauMoi;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblNhapLai;
        private Guna.UI2.WinForms.Guna2TextBox txtNhapLai;

        private Guna.UI2.WinForms.Guna2Button btnLayLai;
        private System.Windows.Forms.LinkLabel lnkTroVe;
    }
}