using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuanLiQuanCafe.Properties; // nhớ import namespace Resources

namespace QuanLiQuanCafe.Helpers
{
    public class MonCard : Guna2Panel
    {
        public int Id { get; }
        public string TenMon { get; }
        public decimal Gia { get; }
        public string Loai { get; }
        public bool IsSelected { get; private set; } = false;
        public event Action<MonCard> OnSelect;

        private readonly Color MauChon = ColorTranslator.FromHtml("#A67C52");
        private readonly Color MauChuaChon = ColorTranslator.FromHtml("#D9C2A1");

        public MonCard(int id, string ten, decimal gia, string loai, Image anhMon = null)
        {
            Id = id;
            TenMon = ten;
            Gia = gia;
            Loai = loai;
            Width = 120;
            Height = 180;
            BorderRadius = 10;
            BackColor = MauChuaChon;
            Margin = new Padding(8);
            Cursor = Cursors.Hand;

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40)); // ảnh
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // tên
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // giá
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20)); // loại

            // PictureBox ảnh
            var pb = new PictureBox
            {
                Dock = DockStyle.None,
                Width = 100,       // cố định vuông
                Height = 100,
                Image = anhMon ?? Resource1.DefaultImage,
                SizeMode = PictureBoxSizeMode.Zoom,
                Anchor = AnchorStyles.None
            };
            layout.Controls.Add(pb, 0, 0);

            // Label tên - giá - loại
            layout.Controls.Add(TaoLabel(ten, FontStyle.Bold));
            layout.Controls.Add(TaoLabel(gia.ToString("N0") + " đ", FontStyle.Regular));
            layout.Controls.Add(TaoLabel(loai, FontStyle.Italic));

            Controls.Add(layout);

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

        private void RaiseSelect() => OnSelect?.Invoke(this);

        public void SetSelected(bool value)
        {
            IsSelected = value;
            BackColor = value ? MauChon : MauChuaChon;
        }
    }
}
