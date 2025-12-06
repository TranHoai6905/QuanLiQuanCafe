using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLiQuanCafe
{
    partial class FormMain
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
            this.panelMenu = new Guna.UI2.WinForms.Guna2Panel();
            this.btnQuanLyMon = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuanLyTaiKhoan = new Guna.UI2.WinForms.Guna2Button();
            this.btnBaoCao = new Guna.UI2.WinForms.Guna2Button();
            this.picLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnQuanLyHoaDon = new Guna.UI2.WinForms.Guna2Button();
            this.lblTenQuan = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnTrangChu = new Guna.UI2.WinForms.Guna2Button();
            this.panelContent = new Guna.UI2.WinForms.Guna2Panel();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(194)))), ((int)(((byte)(161)))));
            this.panelMenu.Controls.Add(this.btnQuanLyMon);
            this.panelMenu.Controls.Add(this.btnQuanLyTaiKhoan);
            this.panelMenu.Controls.Add(this.btnBaoCao);
            this.panelMenu.Controls.Add(this.picLogo);
            this.panelMenu.Controls.Add(this.btnQuanLyHoaDon);
            this.panelMenu.Controls.Add(this.lblTenQuan);
            this.panelMenu.Controls.Add(this.btnTrangChu);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(194)))), ((int)(((byte)(161)))));
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(274, 750);
            this.panelMenu.TabIndex = 0;
            // 
            // btnQuanLyMon
            // 
            this.btnQuanLyMon.BackColor = System.Drawing.Color.Transparent;
            this.btnQuanLyMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuanLyMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuanLyMon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(75)))), ((int)(((byte)(56)))));
            this.btnQuanLyMon.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQuanLyMon.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyMon.Location = new System.Drawing.Point(0, 428);
            this.btnQuanLyMon.Name = "btnQuanLyMon";
            this.btnQuanLyMon.Size = new System.Drawing.Size(274, 77);
            this.btnQuanLyMon.TabIndex = 4;
            this.btnQuanLyMon.Text = "Quản lý món";
            // 
            // btnQuanLyTaiKhoan
            // 
            this.btnQuanLyTaiKhoan.BackColor = System.Drawing.Color.Transparent;
            this.btnQuanLyTaiKhoan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyTaiKhoan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyTaiKhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuanLyTaiKhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuanLyTaiKhoan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(75)))), ((int)(((byte)(56)))));
            this.btnQuanLyTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQuanLyTaiKhoan.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyTaiKhoan.Location = new System.Drawing.Point(0, 672);
            this.btnQuanLyTaiKhoan.Name = "btnQuanLyTaiKhoan";
            this.btnQuanLyTaiKhoan.Size = new System.Drawing.Size(274, 77);
            this.btnQuanLyTaiKhoan.TabIndex = 3;
            this.btnQuanLyTaiKhoan.Text = "Quản lý tài khoản";
            this.btnQuanLyTaiKhoan.Click += new System.EventHandler(this.btnQuanLyTaiKhoan_Click);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.BackColor = System.Drawing.Color.Transparent;
            this.btnBaoCao.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBaoCao.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBaoCao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBaoCao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBaoCao.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(75)))), ((int)(((byte)(56)))));
            this.btnBaoCao.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnBaoCao.Location = new System.Drawing.Point(0, 548);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(274, 77);
            this.btnBaoCao.TabIndex = 2;
            this.btnBaoCao.Text = "Báo cáo &  Thống kê";
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::QuanLiQuanCafe.Properties.Resources.LogoCafe;
            this.picLogo.ImageRotate = 0F;
            this.picLogo.Location = new System.Drawing.Point(87, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(90, 90);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // btnQuanLyHoaDon
            // 
            this.btnQuanLyHoaDon.BackColor = System.Drawing.Color.Transparent;
            this.btnQuanLyHoaDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyHoaDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLyHoaDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuanLyHoaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuanLyHoaDon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(75)))), ((int)(((byte)(56)))));
            this.btnQuanLyHoaDon.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQuanLyHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyHoaDon.Location = new System.Drawing.Point(0, 308);
            this.btnQuanLyHoaDon.Name = "btnQuanLyHoaDon";
            this.btnQuanLyHoaDon.Size = new System.Drawing.Size(274, 77);
            this.btnQuanLyHoaDon.TabIndex = 1;
            this.btnQuanLyHoaDon.Text = "Quản lý hóa dơn";
            this.btnQuanLyHoaDon.Click += new System.EventHandler(this.btnQuanLyHoaDon_Click);
            // 
            // lblTenQuan
            // 
            this.lblTenQuan.BackColor = System.Drawing.Color.Transparent;
            this.lblTenQuan.Font = new System.Drawing.Font("Times New Roman", 22F, System.Drawing.FontStyle.Bold);
            this.lblTenQuan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(75)))), ((int)(((byte)(56)))));
            this.lblTenQuan.Location = new System.Drawing.Point(38, 108);
            this.lblTenQuan.Name = "lblTenQuan";
            this.lblTenQuan.Size = new System.Drawing.Size(198, 34);
            this.lblTenQuan.TabIndex = 1;
            this.lblTenQuan.Text = "MYU COFFEE";
            this.lblTenQuan.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.BackColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTrangChu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTrangChu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTrangChu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTrangChu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(75)))), ((int)(((byte)(56)))));
            this.btnTrangChu.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTrangChu.ForeColor = System.Drawing.Color.White;
            this.btnTrangChu.Location = new System.Drawing.Point(0, 188);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(274, 77);
            this.btnTrangChu.TabIndex = 0;
            this.btnTrangChu.Text = "Trang Chủ";
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.BackColor = System.Drawing.Color.Transparent;
            this.panelContent.BorderRadius = 35;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(274, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(109)))), ((int)(((byte)(92)))));
            this.panelContent.ShadowDecoration.Depth = 20;
            this.panelContent.ShadowDecoration.Enabled = true;
            this.panelContent.Size = new System.Drawing.Size(926, 750);
            this.panelContent.TabIndex = 2;
            // 
            // FormMain
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MYU COFFEE - Quản Lý Quán Cà Phê";
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna2Panel panelMenu;

        private Guna2PictureBox picLogo;
        private Guna2HtmlLabel lblTenQuan;
        private Guna2Button btnQuanLyTaiKhoan;
        private Guna2Button btnBaoCao;
        private Guna2Button btnQuanLyHoaDon;
        private Guna2Button btnTrangChu;
        private Guna2Button btnQuanLyMon;
        private Guna2Panel panelContent;
    }
}