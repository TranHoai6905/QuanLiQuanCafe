using System;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace QuanLiQuanCafe
{
    internal static class Program
    {
        // Logger chinh
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        // Logger rieng cho FATAL
        private static readonly ILog fatalLog = LogManager.GetLogger("FatalLogger");

        [STAThread]
        static void Main()
        {
            // ==============================
            // 1) Load file log4net.xml
            // ==============================
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.xml"));

            // ==============================
            // 2) Ghi log test de kiem tra
            // ==============================
            log.Debug("Ung dung bat dau chay (DEBUG)");
            log.Info("Ung dung bat dau chay (INFO)");
            fatalLog.Fatal("TEST FATAL: Ghi log FATAL ngay khi khoi chay");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // ==============================
                // 3) Chay form dau tien
                // ==============================
                Application.Run(new frmDangNhap());
            }
            catch (Exception ex)
            {
                // Ghi log loi nghiem trong
                fatalLog.Fatal("FATAL: Loi khong xu ly trong Main()", ex);

                MessageBox.Show(
                    "Co loi nghiem trong xay ra trong ung dung.\nHay kiem tra file Fatal.log!",
                    "Loi nghiem trong",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // ==============================
                // 4) Log khi ung dung thoat
                // ==============================
                log.Info("Ung dung da thoat");
            }
        }
    }
}
