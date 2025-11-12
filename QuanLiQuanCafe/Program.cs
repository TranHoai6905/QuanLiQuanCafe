using System;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using static System.Environment;


using System.Configuration;
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
