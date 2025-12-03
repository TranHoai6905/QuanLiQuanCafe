using System.Drawing;
using Guna.UI2.WinForms;

namespace QuanLiQuanCafe.Helpers
{
    public static class DataGridViewHelper
    {
        public static void SetHeaderColor(Guna2DataGridView dgv)
        {
            // Bắt buộc tắt theme style để màu tùy chỉnh hiển thị
            dgv.ThemeStyle.HeaderStyle.BackColor = ColorTranslator.FromHtml("#D9BCA5"); // pastel nhạt
            dgv.ThemeStyle.HeaderStyle.ForeColor = ColorTranslator.FromHtml("#5C4033"); // chữ nâu đậm
            dgv.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            dgv.EnableHeadersVisualStyles = false; // tắt visual style mặc định
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D9BCA5");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#5C4033");
        }
    }
}
