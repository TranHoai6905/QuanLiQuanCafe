using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaiKhoanBUS = QuanLiQuanCafe.TaiKhoanBUS;

namespace QuanLiQuanCafe
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkGhiNho_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            bool ketQua = TaiKhoanBUS.KiemTraDangNhap(txtTenDangNhap.Text, txtMatKhau.Text);

            if (ketQua)
                MessageBox.Show("Đăng nhập thành công!");
            else
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
        }

        private void lnkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void lnkQuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
