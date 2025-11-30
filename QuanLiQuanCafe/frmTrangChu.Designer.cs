// frmDangNhap.Designer.cs
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    partial class frmTrangChu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnXemMon = new Guna.UI2.WinForms.Guna2Button();
            this.btnHoaDon = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemMon = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // btnXemMon
            // 
            this.btnXemMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXemMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXemMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXemMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXemMon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXemMon.ForeColor = System.Drawing.Color.White;
            this.btnXemMon.Location = new System.Drawing.Point(83, 40);
            this.btnXemMon.Name = "btnXemMon";
            this.btnXemMon.Size = new System.Drawing.Size(257, 39);
            this.btnXemMon.TabIndex = 6;
            this.btnXemMon.Text = "Xem món ";
            this.btnXemMon.Click += new System.EventHandler(this.btnXemMon_Click);
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHoaDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHoaDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHoaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnHoaDon.Location = new System.Drawing.Point(80, 96);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(257, 39);
            this.btnHoaDon.TabIndex = 7;
            this.btnHoaDon.Text = "Quản lý hóa đơn ";
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnThemMon
            // 
            this.btnThemMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemMon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemMon.ForeColor = System.Drawing.Color.White;
            this.btnThemMon.Location = new System.Drawing.Point(83, 155);
            this.btnThemMon.Name = "btnThemMon";
            this.btnThemMon.Size = new System.Drawing.Size(257, 39);
            this.btnThemMon.TabIndex = 8;
            this.btnThemMon.Text = "Thêm món ";
            this.btnThemMon.Click += new System.EventHandler(this.btnThemMon_Click);
            // 
            // frmTrangChu
            // 
            this.ClientSize = new System.Drawing.Size(417, 230);
            this.Controls.Add(this.btnThemMon);
            this.Controls.Add(this.btnHoaDon);
            this.Controls.Add(this.btnXemMon);
            this.Name = "frmTrangChu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Quản lý quán cafe";
            this.ResumeLayout(false);

        }
        private Guna.UI2.WinForms.Guna2Button btnXemMon;
        private Guna.UI2.WinForms.Guna2Button btnHoaDon;
        private Guna.UI2.WinForms.Guna2Button btnThemMon;
    }
}