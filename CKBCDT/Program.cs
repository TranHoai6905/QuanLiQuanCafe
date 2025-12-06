using log4net.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CKBCDT
{
    internal static class Program
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        [STAThread]
        static void Main()
        {
            // 1. PHẢI ĐẶT 2 DÒNG NÀY LÊN ĐẦU TIÊN – TRƯỚC KHI TẠO FORM NÀO HẾT!
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 2. KHỞI TẠO LOG4NET
            try
            {
                var configFile = new FileInfo("log4net.config");
                if (configFile.Exists)
                    XmlConfigurator.Configure(configFile);
                else
                    BasicConfigurator.Configure();

                log.Info("=== UNG DUNG KHOI DONG THANH CONG ===");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo log4net: " + ex.Message, "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 3. BẮT LỖI TOÀN CỤC (UI Thread)
            Application.ThreadException += (sender, e) =>
            {
                log.Error("Loi khong duoc xu ly trong UI Thread", e.Exception);
                MessageBox.Show($"Đã xảy ra lỗi:\n{e.Exception.Message}\n\nỨng dụng vẫn tiếp tục chạy.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };

            // 4. BẮT LỖI TOÀN CỤC (Background Thread)
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                log.Fatal("LOI NGHIEM TRONG - UNG DUNG SAP TAT!", ex);
                MessageBox.Show("Ứng dụng gặp lỗi nghiêm trọng và sẽ thoát!\nXem file logs để biết chi tiết.",
                    "Lỗi Nghiêm Trọng", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Environment.Exit(1);
            };

            // 5. CHẠY FORM CHÍNH – CHỈ GỌI 1 LẦN DUY NHẤT!
            try
            {
                Application.Run(new frmBaoCaoDoanhThu());
                log.Info("=== UNG DUNG KET THUC BINH THUONG ===");
            }
            catch (Exception ex)
            {
                log.Fatal("Loi khi chay Application.Run()", ex);
                MessageBox.Show("Không thể khởi động ứng dụng!", "Lỗi Fatal",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            // ===> BỎ HẾT MẤY DÒNG DƯ THỪA Ở DƯỚI NÀY ĐI EM ƠI <===
            // Không gọi lại EnableVisualStyles + SetCompatibleTextRenderingDefault
            // Không gọi BaoCaoDoanhThu_Tester.Run() ở đây (nếu cần test thì để riêng)
            // Không gọi Application.Run lần thứ 2
        }
    }
}
