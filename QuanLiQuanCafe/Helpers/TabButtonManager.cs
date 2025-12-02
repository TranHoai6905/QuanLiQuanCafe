using Guna.UI2.WinForms;
using System.Drawing;

namespace QuanLiQuanCafe.Helpers
{
    /// <summary>
    /// Tab Button Manager - Quản lý trạng thái active/inactive của tab buttons
    /// Áp dụng: State Pattern, Singleton Pattern (static), Strategy Pattern
    /// </summary>
    public static class TabButtonManager
    {
        #region Color Constants - Standard Tabs (Tab thông thường)
        /// <summary>
        /// Phương pháp: Constants Pattern - Màu cho tab thông thường
        /// Sử dụng cho: FormQuanLyTaiKhoan và các form có tab filter
        /// </summary>
        private static readonly Color StandardActiveBackground = Color.FromArgb(228, 109, 67);  // Cam
        private static readonly Color StandardActiveForeground = Color.White;
        private static readonly Color StandardInactiveBackground = Color.FromArgb(200, 200, 200); // Xám
        private static readonly Color StandardInactiveForeground = Color.Black;
        #endregion

        #region Color Constants - Navigation Buttons (FormMain)
        /// <summary>
        /// Phương pháp: Constants Pattern - Màu cho navigation buttons (FormMain)
        /// Selected: Nút đang được chọn (vuông góc)
        /// Unselected: Nút không được chọn (bo tròn)
        /// </summary>
        private static readonly Color NavigationSelectedBackground = Color.FromArgb(101, 75, 56);   // Nâu đậm
        private static readonly Color NavigationSelectedForeground = Color.White;
        private static readonly Color NavigationUnselectedBackground = Color.FromArgb(101, 75, 56); // Nâu (giống selected)
        private static readonly Color NavigationUnselectedForeground = Color.White;
        #endregion

        #region Border Radius Constants
        /// <summary>
        /// Phương pháp: Constants Pattern - Bo góc cho navigation buttons
        /// Unselected: Bo tròn đẹp (35px)
        /// Selected: Vuông góc (0px)
        /// </summary>
        private const int NavigationUnselectedBorderRadius = 35; // Bo tròn khi không chọn
        private const int NavigationSelectedBorderRadius = 0;    // Vuông góc khi chọn
        #endregion

        #region State Tracking
        /// <summary>
        /// Phương pháp: State Pattern - Theo dõi button active hiện tại
        /// </summary>
        private static Guna2Button _currentStandardActive;      // Tab thông thường đang active
        private static Guna2Button _currentNavigationActive;    // Navigation button đang active
        #endregion

        #region Standard Tab Methods (Tab thông thường)
        /// <summary>
        /// Phương pháp: State Pattern - Activate tab thông thường (cam/xám)
        /// Sử dụng cho: FormQuanLyTaiKhoan - btnTatCa, btnDangLam, btnNghi
        /// Flow: Deactivate current → Activate new → Track state
        /// </summary>
        public static void Activate(Guna2Button button)
        {
            DeactivateCurrentStandard();
            SetStandardActive(button);
            TrackStandardActive(button);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Set button về trạng thái active (cam)
        /// </summary>
        private static void SetStandardActive(Guna2Button button)
        {
            button.FillColor = StandardActiveBackground;
            button.ForeColor = StandardActiveForeground;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Set button về trạng thái inactive (xám)
        /// </summary>
        private static void SetStandardInactive(Guna2Button button)
        {
            button.FillColor = StandardInactiveBackground;
            button.ForeColor = StandardInactiveForeground;
        }

        /// <summary>
        /// Phương pháp: Extract Method - Deactivate button standard hiện tại
        /// </summary>
        private static void DeactivateCurrentStandard()
        {
            if (_currentStandardActive != null)
            {
                SetStandardInactive(_currentStandardActive);
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Track button standard đang active
        /// </summary>
        private static void TrackStandardActive(Guna2Button button)
        {
            _currentStandardActive = button;
        }

        /// <summary>
        /// Phương pháp: Batch Operation - Reset tất cả standard tabs về inactive
        /// Sử dụng: Khi cần reset toàn bộ trạng thái
        /// </summary>
        public static void Reset(params Guna2Button[] buttons)
        {
            foreach (var btn in buttons)
            {
                SetStandardInactive(btn);
            }

            _currentStandardActive = null;
        }
        #endregion

        #region Navigation Button Methods (FormMain)
        /// <summary>
        /// Phương pháp: State Pattern + Animation - Select navigation button (FormMain)
        /// Sử dụng cho: FormMain - btnTrangChu, btnQuanLyTaiKhoan, btnQuanLyDoUong, etc.
        /// Flow: Deselect current (bo tròn) → Select new (vuông góc) → Track state
        /// Visual: Unselected = Bo tròn (35px), Selected = Vuông góc (0px)
        /// </summary>
        public static void SelectMain(Guna2Button button)
        {
            DeselectCurrentNavigation();
            SetNavigationSelected(button);
            TrackNavigationActive(button);
        }

        /// <summary>
        /// Phương pháp: Extract Method - Set navigation button về trạng thái selected
        /// Visual: Vuông góc, nền nâu đậm, chữ trắng
        /// </summary>
        private static void SetNavigationSelected(Guna2Button button)
        {
            button.FillColor = NavigationSelectedBackground;
            button.ForeColor = NavigationSelectedForeground;
            button.BorderRadius = NavigationSelectedBorderRadius; // Vuông góc
        }

        /// <summary>
        /// Phương pháp: Extract Method - Set navigation button về trạng thái unselected
        /// Visual: Bo tròn, nền nâu, chữ trắng
        /// </summary>
        private static void SetNavigationUnselected(Guna2Button button)
        {
            button.FillColor = NavigationUnselectedBackground;
            button.ForeColor = NavigationUnselectedForeground;
            button.BorderRadius = NavigationUnselectedBorderRadius; // Bo tròn
        }

        /// <summary>
        /// Phương pháp: Extract Method - Deselect navigation button hiện tại
        /// </summary>
        private static void DeselectCurrentNavigation()
        {
            if (_currentNavigationActive != null)
            {
                SetNavigationUnselected(_currentNavigationActive);
            }
        }

        /// <summary>
        /// Phương pháp: Extract Method - Track navigation button đang active
        /// </summary>
        private static void TrackNavigationActive(Guna2Button button)
        {
            _currentNavigationActive = button;
        }

        /// <summary>
        /// Phương pháp: Batch Operation - Reset tất cả navigation buttons
        /// Sử dụng: Khi khởi tạo FormMain hoặc cần reset toàn bộ
        /// </summary>
        public static void ResetMain(params Guna2Button[] buttons)
        {
            foreach (var btn in buttons)
            {
                SetNavigationUnselected(btn);
            }

            _currentNavigationActive = null;
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// Phương pháp: Factory Method - Tạo style cho button type cụ thể
        /// Extension: Có thể thêm các button types khác (Success, Warning, Danger)
        /// </summary>
        public static void ApplyStyle(Guna2Button button, ButtonStyle style)
        {
            switch (style)
            {
                case ButtonStyle.StandardActive:
                    SetStandardActive(button);
                    break;

                case ButtonStyle.StandardInactive:
                    SetStandardInactive(button);
                    break;

                case ButtonStyle.NavigationSelected:
                    SetNavigationSelected(button);
                    break;

                case ButtonStyle.NavigationUnselected:
                    SetNavigationUnselected(button);
                    break;
            }
        }
        #endregion
    }

    #region Button Style Enum
    /// <summary>
    /// Phương pháp: Enum Pattern - Định nghĩa các loại style button
    /// Extension Point: Dễ dàng thêm style mới
    /// </summary>
    public enum ButtonStyle
    {
        StandardActive,           // Tab thông thường - Active (cam)
        StandardInactive,         // Tab thông thường - Inactive (xám)
        NavigationSelected,       // Navigation - Selected (vuông góc)
        NavigationUnselected      // Navigation - Unselected (bo tròn)
    }
    #endregion
}