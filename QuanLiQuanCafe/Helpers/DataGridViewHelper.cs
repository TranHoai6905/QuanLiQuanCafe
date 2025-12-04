using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLiQuanCafe.Helpers
{
    public static class DataGridViewHelper
    {
        private static readonly Color MauNauDam = ColorTranslator.FromHtml("#A67C52"); // Nâu đậm cho header
        private static readonly Color MauPastel = ColorTranslator.FromHtml("#D9C2A1"); // Pastel nâu nhạt cho selected rows
        private static readonly Color MauText = Color.Black; // Màu chữ default
        private static readonly Color MauTextSelected = Color.White; // Màu chữ khi selected
        private static readonly Color MauGridLine = ColorTranslator.FromHtml("#C4A484"); // Màu grid line nâu trung bình

        /// <summary>
        /// Thiết lập kiểu đẹp cho Guna2DataGridView với tông trắng/pastel/nâu đậm.
        /// - Header: Nâu đậm, chữ trắng (không đổi màu khi chọn cột)
        /// - Rows: Nền trắng, chữ đen (không alternating)
        /// - Selected rows: Pastel nâu nhạt, chữ trắng in đậm
        /// - Grid lines: Nâu trung bình
        /// </summary>
        /// <param name="dgv">DataGridView cần thiết lập</param>
        public static void SetBrownTheme(Guna2DataGridView dgv)
        {
            // Quan trọng: Disable visual styles để override màu header
            dgv.EnableHeadersVisualStyles = false;

            // Thiết lập chung
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = MauGridLine;

            // ThemeStyle tổng quát
            dgv.ThemeStyle.BackColor = Color.White; // Background chung trắng

            // Header style (sử dụng cả ThemeStyle và DefaultCellStyle để chắc chắn, không đổi khi chọn cột)
            dgv.ThemeStyle.HeaderStyle.BackColor = MauNauDam;
            dgv.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgv.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = MauNauDam;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = MauNauDam; // Giữ nguyên khi chọn

            // Rows style (nền trắng)
            dgv.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgv.ThemeStyle.RowsStyle.ForeColor = MauText;
            dgv.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9);
            dgv.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ThemeStyle.RowsStyle.Height = 28; // Tăng chiều cao row cho đẹp
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = MauText;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            // Alternating rows (cũng trắng để không phân biệt)
            dgv.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgv.ThemeStyle.AlternatingRowsStyle.ForeColor = MauText;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = MauText;

            // Selected rows (pastel, chữ trắng)
            dgv.ThemeStyle.RowsStyle.SelectionBackColor = MauPastel;
            dgv.ThemeStyle.RowsStyle.SelectionForeColor = MauTextSelected;
            dgv.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = MauPastel;
            dgv.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = MauTextSelected;
            dgv.DefaultCellStyle.SelectionBackColor = MauPastel;
            dgv.DefaultCellStyle.SelectionForeColor = MauTextSelected;

            // Grid lines
            dgv.ThemeStyle.GridColor = MauGridLine;

            // Cột tự động điều chỉnh kích thước
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Làm chữ in đậm khi row được chọn (sử dụng CellFormatting)
            dgv.CellFormatting += (sender, e) =>
            {
                if (e.RowIndex >= 0 && dgv.Rows[e.RowIndex].Selected)
                {
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    e.CellStyle.ForeColor = MauTextSelected;
                    e.CellStyle.BackColor = MauPastel;
                }
            };
        }

        // Phương thức cũ nếu có (từ mã trước)
        public static void SetHeaderColor(Guna2DataGridView dgv)
        {
            // Có thể hợp nhất hoặc giữ riêng nếu cần
            SetBrownTheme(dgv); // Gọi theme đầy đủ
        }
    }
}