using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms; // Guna UI2 namespace

namespace QuanLiQuanCafe.Helpers
{
    public static class ButtonHelper
    {
        public static void EnableHoverShadow(Form form)
        {
            AddHoverEffectToButtons(form);
        }

        private static void AddHoverEffectToButtons(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Guna2Button btn)
                {
                    btn.MouseEnter += Button_MouseEnter;
                    btn.MouseLeave += Button_MouseLeave;
                }

                if (ctrl.HasChildren)
                    AddHoverEffectToButtons(ctrl);
            }
        }

        private static void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2Button btn)
            {
                // Màu nền khi hover
                btn.FillColor = Color.LightBlue;

                // Bật shadow
                btn.ShadowDecoration.Enabled = true;
                btn.ShadowDecoration.Shadow = new Padding(5); // độ lớn đổ bóng
                btn.ShadowDecoration.Color = Color.Gray;
            }
        }

        private static void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Guna2Button btn)
            {
                // Trả về màu gốc
                btn.FillColor = Color.White;

                // Tắt shadow
                btn.ShadowDecoration.Enabled = false;
            }
        }
    }
}
