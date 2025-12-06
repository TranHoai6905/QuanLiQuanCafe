using Guna.UI2.WinForms;
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

        /// <summary>
        /// Sets the button to its normal state: orange-brown background, white text, with border.
        /// </summary>
        /// <param name="button">The button to update.</param>
        public static void SetFilterButtonNormal(Guna2Button button)
        {
            if (button == null) return;

            // Normal state: orange-brown background (using SaddleBrown as example, adjust RGB if needed)
            button.FillColor = Color.SaddleBrown; // Or Color.FromArgb(139, 69, 19) for custom orange-brown
            button.ForeColor = Color.White;
            button.BorderThickness = 1; // Default border
            button.BorderColor = Color.Black; // Optional border color
        }

        /// <summary>
        /// Sets the button to its selected state: beige background, dark brown text, no border.
        /// </summary>
        /// <param name="button">The button to update.</param>
        public static void SetFilterButtonSelected(Guna2Button button)
        {
            if (button == null) return;

            // Selected state: beige background
            button.FillColor = Color.Beige;
            button.ForeColor = Color.Chocolate; // Dark brown text (or Color.FromArgb(139, 69, 19) for darker)
            button.BorderThickness = 0; // No border
        }

        /// <summary>
        /// Resets all provided filter buttons to their normal state.
        /// </summary>
        /// <param name="buttons">The buttons to reset.</param>
        public static void ResetFilterButtons(params Guna2Button[] buttons)
        {
            foreach (var button in buttons)
            {
                SetFilterButtonNormal(button);
            }
        }
    }
        
}
