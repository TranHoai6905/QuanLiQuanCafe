using Guna.UI2.WinForms;
using System.Drawing;

namespace QuanLiQuanCafe.Helpers
{
    public static class TabButtonManager
    {
        // MÀU CŨ – GIỮ NGUYÊN CHO CÁC FORM KHÁC
        private static readonly Color ActiveColor = Color.FromArgb(228, 109, 67);
        private static readonly Color ActiveForeColor = Color.White;
        private static readonly Color InactiveColor = Color.FromArgb(200, 200, 200);
        private static readonly Color InactiveForeColor = Color.Black;
        private static Guna2Button _currentActive;

        // MÀU MỚI CHO FORMMAIN
        private static readonly Color SelectedBg = Color.FromArgb(101, 75, 56);   // nâu khi chọn
        private static readonly Color SelectedText = Color.White;
        private static readonly Color UnselectedBg = Color.FromArgb(101, 75, 56); // be khi không chọn
        private static readonly Color UnselectedText = Color.White;

        // BO GÓC KHI KHÔNG CHỌN – ĐẸP NHƯ HÌNH BẠN GỬI
        private const int UnselectedBorderRadius = 35; // bo tròn đẹp
        private const int SelectedBorderRadius = 0;  // vuông góc khi click

        private static Guna2Button _mainActiveButton;

        // HÀM CŨ – KHÔNG ĐỘNG
        public static void Activate(Guna2Button button)
        {
            DeactivateCurrent();
            button.FillColor = ActiveColor;
            button.ForeColor = ActiveForeColor;
            _currentActive = button;
        }

        /// <summary>
        /// DÀNH RIÊNG CHO FORMMAIN – BO GÓC KHI KHÔNG CHỌN, VUÔNG KHI CLICK
        /// </summary>
        public static void SelectMain(Guna2Button button)
        {
            // Bỏ chọn nút cũ → trả về bo góc
            if (_mainActiveButton != null && _mainActiveButton != button)
            {
                _mainActiveButton.FillColor = UnselectedBg;
                _mainActiveButton.ForeColor = UnselectedText;
                _mainActiveButton.BorderRadius = UnselectedBorderRadius; // bo tròn lại
            }

            // Chọn nút mới → vuông góc
            button.FillColor = SelectedBg;
            button.ForeColor = SelectedText;
            button.BorderRadius = SelectedBorderRadius; // vuông góc

            _mainActiveButton = button;
        }

        public static void ResetMain(params Guna2Button[] buttons)
        {
            foreach (var btn in buttons)
            {
                btn.FillColor = UnselectedBg;
                btn.ForeColor = UnselectedText;
                btn.BorderRadius = UnselectedBorderRadius; // tất cả bo tròn
            }
            _mainActiveButton = null;
        }

        private static void DeactivateCurrent()
        {
            if (_currentActive != null)
            {
                _currentActive.FillColor = InactiveColor;
                _currentActive.ForeColor = InactiveForeColor;
            }
        }

        public static void Reset(params Guna2Button[] buttons)
        {
            foreach (var btn in buttons)
            {
                btn.FillColor = InactiveColor;
                btn.ForeColor = InactiveForeColor;
            }
            _currentActive = null;
        }
    }
}