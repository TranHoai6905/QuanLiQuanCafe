using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLiQuanCafe.Helpers
{
    /// <summary>
    /// Card hiển thị một món: Tên - Giá - Loại
    /// Dùng chung cho các form: frmThemMon, frmMon,...
    /// </summary>
    public class MonCard : Guna2Panel
    {
        // Các thông tin món để form cha đọc lại
        public int Id { get; }
        public string TenMon { get; }
        public decimal Gia { get; }
        public string Loai { get; }
        // Trạng thái đã được chọn (cho UI)
        public bool IsSelected { get; private set; } = false;
        // Sự kiện cho form cha đăng ký (click vào card)
        public event Action<MonCard> OnSelect;
        // Màu
        private readonly Color MauChon = ColorTranslator.FromHtml("#A67C52"); // nâu đậm
        private readonly Color MauChuaChon = ColorTranslator.FromHtml("#D9C2A1"); // nâu nhạt pastel
        public MonCard(int id, string ten, decimal gia, string loai)
        {
            Id = id;
            TenMon = ten;
            Gia = gia;
            Loai = loai;
            Width = 120;
            Height = 150;
            BorderRadius = 10;
            BackColor = MauChuaChon;
            Margin = new Padding(8);
            Cursor = Cursors.Hand;
            // Layout 3 dòng: Tên - Giá - Loại
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 34));
            layout.Controls.Add(TaoLabel(ten, FontStyle.Bold));
            layout.Controls.Add(TaoLabel(gia.ToString("N0") + " đ", FontStyle.Regular));
            layout.Controls.Add(TaoLabel(loai, FontStyle.Italic));
            Controls.Add(layout);
            // Bấm vào card hoặc bất kỳ label nào đều gọi OnSelect
            Click += (s, e) => RaiseSelect();
            foreach (Control c in layout.Controls)
                c.Click += (s, e) => RaiseSelect();
        }
        private Label TaoLabel(string text, FontStyle fontStyle)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, fontStyle),
            };
        }
        private void RaiseSelect()
        {
            OnSelect?.Invoke(this);
        }
        /// <summary>
        /// Form cha gọi để set trạng thái chọn/bỏ chọn + đổi màu
        /// </summary>
        public void SetSelected(bool value)
        {
            IsSelected = value;
            BackColor = value ? MauChon : MauChuaChon;
        }
    }
}