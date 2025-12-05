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

            AddLoaiButton(flp, "Tất cả", onClick);

            if (dtLoai == null) return;

            foreach (DataRow row in dtLoai.Rows)
                AddLoaiButton(flp, row["Loai"].ToString(), onClick);
        }

        private static void AddLoaiButton(FlowLayoutPanel flp, string tenLoai, Action<string> onClick)
        {
            var btn = new Guna2Button()
            {
                Tag = tenLoai,
                Text = tenLoai,

                Width = 110,      // Nhỏ gọn hơn
                Height = 32,      // Thấp hơn
                BorderRadius = 17, // Bo tròn nhiều

                FillColor = ColorTranslator.FromHtml("#D9C2A1"),
                ForeColor = Color.White,
                BorderThickness = 0,

                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                AutoSize = false,
                Padding = new Padding(5, 2, 5, 2), // Chữ không bị che
                TextAlign = HorizontalAlignment.Center
            };

            // Không đổi màu khi hover
            btn.HoverState.FillColor = btn.FillColor;

            btn.Click += (s, e) =>
            {
                // Reset tất cả
                foreach (Control c in flp.Controls)
                    if (c is Guna2Button b)
                    {
                        b.FillColor = ColorTranslator.FromHtml("#D9C2A1");
                        b.ForeColor = Color.White;
                        b.BorderThickness = 0;  // Không viền
                    }

                // Nút được chọn (KHÔNG VIỀN)
                var clicked = (Guna2Button)s;
                clicked.FillColor = ColorTranslator.FromHtml("#C3A27A"); 
                clicked.ForeColor = Color.Black;
                clicked.BorderThickness = 0; // Không viền

                onClick?.Invoke(clicked.Tag.ToString());
            };


            flp.Controls.Add(btn);
        }

    }
}
