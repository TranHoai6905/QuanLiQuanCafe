using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace QuanLiQuanCafe
{
    internal static class Program
    {
        // Logger chính
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        // Logger riêng cho FATAL
        private static readonly ILog fatalLog = LogManager.GetLogger("FatalLogger");

        [STAThread]
        static void Main()
        {
            // 1) Load file log4net.xml nếu có
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            string logFile = "log4net.xml";

            if (File.Exists(logFile))
            {
                XmlConfigurator.Configure(logRepository, new FileInfo(logFile));
            }
            else
            {
                MessageBox.Show($"Không tìm thấy file log4net.xml!\nỨng dụng vẫn chạy nhưng không ghi log.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 2) Ghi log test để kiểm tra
            log.Debug("Ứng dụng bắt đầu chạy (DEBUG)");
            log.Info("Ứng dụng bắt đầu chạy (INFO)");
            fatalLog.Fatal("TEST FATAL: Ghi log FATAL ngay khi khởi chạy");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // 3) Chạy form đăng nhập đầu tiên
                Application.Run(new FormDangNhap());
            }
            catch (Exception ex)
            {
                fatalLog.Fatal("FATAL: Lỗi không xử lý trong Main()", ex);

                MessageBox.Show(
                    "Có lỗi nghiêm trọng xảy ra trong ứng dụng.\nHãy kiểm tra file Fatal.log nếu có!",
                    "Lỗi nghiêm trọng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                log.Info("Ứng dụng đã thoát");
            }
        }
    }
}
