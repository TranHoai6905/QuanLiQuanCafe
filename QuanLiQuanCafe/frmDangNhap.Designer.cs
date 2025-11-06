// frmDangNhap.Designer.cs
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    partial class frmDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 60);
            this.label1.Text = "Tài khoản:";

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 110);
            this.label2.Text = "Mật khẩu:";

            this.txtTaiKhoan.Location = new System.Drawing.Point(160, 57);
            this.txtTaiKhoan.Size = new System.Drawing.Size(180, 23);

            this.txtMatKhau.Location = new System.Drawing.Point(160, 107);
            this.txtMatKhau.Size = new System.Drawing.Size(180, 23);
            this.txtMatKhau.UseSystemPasswordChar = true;

            this.btnDangNhap.Location = new System.Drawing.Point(160, 160);
            this.btnDangNhap.Size = new System.Drawing.Size(85, 30);
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);

            this.btnThoat.Location = new System.Drawing.Point(255, 160);
            this.btnThoat.Size = new System.Drawing.Size(85, 30);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            this.ClientSize = new System.Drawing.Size(420, 230);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.txtTaiKhoan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Text = "Đăng nhập - Quản lý quán cafe";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTaiKhoan;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnThoat;
    }
}