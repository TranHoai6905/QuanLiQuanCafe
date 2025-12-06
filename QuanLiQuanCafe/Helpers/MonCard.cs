using Guna.UI2.WinForms;
using QuanLiQuanCafe;
using static Guna.UI2.WinForms.Suite.Descriptions;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System;

public class MonCard : Guna2Panel
{
    public int Id { get; }
    public string TenMon { get; }
    public decimal Gia { get; }
    public string Loai { get; }
    public string AnhPath { get; set; } // cho phép cập nhật khi chọn ảnh mới
    public Image MonImage { get; set; } // cho phép cập nhật ảnh khi chọn ảnh mới
    // thêm thuộc tính lưu đường dẫn
    public bool IsSelected { get; private set; } = false;
    public event Action<MonCard> OnSelect;
    private readonly Color MauChon = ColorTranslator.FromHtml("#A67C52");
    private readonly Color MauChuaChon = ColorTranslator.FromHtml("#D9C2A1");

    public MonCard(int id, string ten, decimal gia, string loai, Image anhMon = null, string anhPath = null)
    {
        Id = id;
        TenMon = ten;
        Gia = gia;
        Loai = loai;
        AnhPath = anhPath;
        MonImage = CropToSquare(anhMon ?? Resource1.DefaultImage); // Crop ảnh thành vuông trước khi gán
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
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 120)); // ô ảnh vuông 120x120
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F)); // tên
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F)); // giá
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F)); // loại
        var pb = new PictureBox
        {
            Dock = DockStyle.Fill, // Đặt Dock.Fill để PictureBox lấp đầy hàng
            Image = MonImage,
            SizeMode = PictureBoxSizeMode.StretchImage, // Stretch để lấp đầy, nhưng vì crop vuông và ô vuông nên không méo
        };
        layout.Controls.Add(pb, 0, 0);
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

    // Phương thức crop ảnh thành vuông, giữ trung tâm
    private Image CropToSquare(Image originalImage)
    {
        if (originalImage == null) return null;

        int size = Math.Min(originalImage.Width, originalImage.Height);
        Bitmap cropped = new Bitmap(size, size);
        using (Graphics g = Graphics.FromImage(cropped))
        {
            g.DrawImage(originalImage,
                new Rectangle(0, 0, size, size),
                new Rectangle((originalImage.Width - size) / 2, (originalImage.Height - size) / 2, size, size),
                GraphicsUnit.Pixel);
        }
        return cropped;
    }
}