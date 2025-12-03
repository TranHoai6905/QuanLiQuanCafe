using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLiQuanCafe.Helpers
{
    public static class ButtonHelper
    {
        public static void EnableShadow(Form form)
        {
            foreach (Control ctrl in form.Controls)
            {
                AddShadow(ctrl);
            }
        }

        private static void AddShadow(Control control)
        {
            if (control is Button)
            {
                control.Paint += Control_PaintShadow;
            }

            foreach (Control child in control.Controls)
            {
                AddShadow(child);
            }
        }

        private static void Control_PaintShadow(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int shadowWidth = btn.Width - 4;
            int shadowHeight = 6;
            int shadowX = 2;
            int shadowY = btn.Height - shadowHeight + 1;

            // Tạo gradient ellipse để bóng mềm, mờ dần
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(shadowX, shadowY, shadowWidth, shadowHeight);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(100, 0, 0, 0); // Đen mờ
                    brush.SurroundColors = new Color[] { Color.FromArgb(0, 0, 0, 0) }; // Trong suốt ra ngoài
                    e.Graphics.FillEllipse(brush, shadowX, shadowY, shadowWidth, shadowHeight);
                }
            }
        }
    }
}
