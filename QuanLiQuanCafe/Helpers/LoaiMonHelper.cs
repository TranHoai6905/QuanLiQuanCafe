using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace QuanLiQuanCafe.Helpers
{
    public static class LoaiMonHelper
    {
        public static void LoadLoaiMon(FlowLayoutPanel flp, DataTable dtLoai, Action<string> onClick)
        {
            flp.Controls.Clear();

            // Nút "Tất cả"
            AddLoaiButton(flp, "Tất cả", onClick);

            if (dtLoai == null) return;

            foreach (DataRow row in dtLoai.Rows)
            {
                string tenLoai = row["Loai"].ToString();
                AddLoaiButton(flp, tenLoai, onClick);
            }
        }

        private static void AddLoaiButton(FlowLayoutPanel flp, string tenLoai, Action<string> onClick)
        {
            var btn = new Guna2Button
            {
                Text = tenLoai,
                Tag = tenLoai,
                Height = 40,
                BorderRadius = 8,
                FillColor = ColorTranslator.FromHtml("#D9C2A1"),
                ForeColor = Color.White,
                BorderThickness = 0,
                AutoSize = false,
                Padding = new Padding(20, 5, 20, 5)
            };

            Size textSize = TextRenderer.MeasureText(tenLoai, btn.Font);
            btn.Width = textSize.Width + 60;

            // Không đổi màu khi hover
            btn.HoverState.FillColor = btn.FillColor;

            btn.Click += (s, e) =>
            {
                // Reset màu tất cả nút
                foreach (Control c in flp.Controls)
                {
                    if (c is Guna2Button b)
                    {
                        b.FillColor = ColorTranslator.FromHtml("#D9C2A1");
                        b.ForeColor = Color.White;
                        b.BorderThickness = 0;
                    }
                }

                // Tô màu nút được chọn
                var clicked = s as Guna2Button;
                clicked.FillColor = Color.White;
                clicked.ForeColor = Color.Black;
                clicked.BorderColor = Color.Black;
                clicked.BorderThickness = 2;

                onClick?.Invoke(clicked.Tag.ToString());
            };

            flp.Controls.Add(btn);
        }
    }
}
