using System;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    static class Program
    {
        // Chạy ứng dụng
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (frmDangNhap frmLogin = new frmDangNhap())
            {
                Application.Run(frmLogin);
            }
        }
    }
}
