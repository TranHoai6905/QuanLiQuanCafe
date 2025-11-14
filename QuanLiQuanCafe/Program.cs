using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{

    internal static class Program
    {
        // Khai báo một logger cho Program.cs 
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        [STAThread]
        static void Main()
        {
            // Yêu cầu Log4net đọc file config 
            XmlConfigurator.Configure(new FileInfo("Log4netconfig.xml")); // đặt tên giống file xml
            // Ghi một dòng log test ngay khi app khởi động
            log.Info("--- UNG DUNG BAT DAU---");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormDangNhap());
           
            
        }
        
    }
}
