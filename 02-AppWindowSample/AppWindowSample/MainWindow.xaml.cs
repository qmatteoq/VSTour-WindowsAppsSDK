using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Linq;
using System.Windows;
using System.Windows.Interop;

namespace AppWindowSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppWindow appWindow;
        private bool m_isBrandedTitleBar;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            appWindow = AppWindowExtensions.GetAppWindowFromWindowHandle(helper.Handle);
        }

        private void OnSetTitleBar(object sender, RoutedEventArgs e)
        {
            appWindow.Title = "This is a custom title";
        }

        private void TitlebarBrandingBtn_Click(object sender, RoutedEventArgs e)
        {
            appWindow.TitleBar.ResetToDefault();

            m_isBrandedTitleBar = !m_isBrandedTitleBar;
            if (m_isBrandedTitleBar)
            {
                appWindow.Title = "Default titlebar with custom color customization";
                appWindow.TitleBar.ForegroundColor = Colors.White;
                appWindow.TitleBar.BackgroundColor = Colors.DarkOrange;
                appWindow.TitleBar.InactiveBackgroundColor = Colors.Blue;
                appWindow.TitleBar.InactiveForegroundColor = Colors.White;

                //Buttons
                appWindow.TitleBar.ButtonBackgroundColor = Colors.DarkOrange;
                appWindow.TitleBar.ButtonForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Blue;
                appWindow.TitleBar.ButtonInactiveForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonHoverBackgroundColor = Colors.Green;
                appWindow.TitleBar.ButtonHoverForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonPressedBackgroundColor = Colors.DarkOrange;
                appWindow.TitleBar.ButtonPressedForegroundColor = Colors.White;
            }
            else
            {
                appWindow.Title = "Default titlebar";
            }
            MyTitleBar.Visibility = Visibility.Collapsed;
        }

        private void ResetTitlebarBtn_Click(object sender, RoutedEventArgs e)
        {
            MyTitleBar.Visibility = Visibility.Collapsed;
            appWindow.TitleBar.ResetToDefault();
            appWindow.Title = "MainWindow";
        }

        private void TitlebarCustomBtn_Click(object sender, RoutedEventArgs e)
        {
            appWindow.TitleBar.ExtendsContentIntoTitleBar = !appWindow.TitleBar.ExtendsContentIntoTitleBar;

            if (appWindow.TitleBar.ExtendsContentIntoTitleBar)
            {
                // Show the custom titlebar
                MyTitleBar.Visibility = Visibility.Visible;

                // Set Button colors to match the custom titlebar
                appWindow.TitleBar.ButtonBackgroundColor = Colors.LightBlue;
                appWindow.TitleBar.ButtonForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.LightBlue;
                appWindow.TitleBar.ButtonInactiveForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonHoverBackgroundColor = Colors.Green;
                appWindow.TitleBar.ButtonHoverForegroundColor = Colors.White;
                appWindow.TitleBar.ButtonPressedBackgroundColor = Colors.Green;
                appWindow.TitleBar.ButtonPressedForegroundColor = Colors.White;

                //Infer titlebar height
                int titleBarHeight = appWindow.TitleBar.Height;
                this.MyTitleBar.Height = titleBarHeight;

                // Get caption button occlusion information
                // Use LeftInset if you've explicitly set your window layout to RTL or if app language is a RTL language
                int CaptionButtonOcclusionWidth = appWindow.TitleBar.RightInset;

                // Define your drag Regions
                int windowIconWidthAndPadding = (int)MyWindowIcon.ActualWidth + (int)MyWindowIcon.Margin.Right;
                int dragRegionWidth = appWindow.Size.Width - (CaptionButtonOcclusionWidth + windowIconWidthAndPadding);

                Windows.Graphics.RectInt32[] dragRects = new Windows.Graphics.RectInt32[] { };
                Windows.Graphics.RectInt32 dragRect;

                dragRect.X = windowIconWidthAndPadding;
                dragRect.Y = 0;
                dragRect.Height = titleBarHeight;
                dragRect.Width = dragRegionWidth;

                var dragRectsArray = dragRects.Append(dragRect).ToArray();
                appWindow.TitleBar.SetDragRectangles(dragRectsArray);

            }
            else
            {
                // Bring back the default titlebar
                MyTitleBar.Visibility = Visibility.Collapsed;
                appWindow.TitleBar.ResetToDefault();
            }
        }

        private void OnGoFullScreen(object sender, RoutedEventArgs e)
        {
            if (appWindow.Presenter.Kind is not AppWindowPresenterKind.FullScreen)
            {
                appWindow.TrySetPresenter(AppWindowPresenterKind.FullScreen);
            }
            else
            {
                appWindow.TrySetPresenter(AppWindowPresenterKind.Default);
            }
        }

        private void OnGoCompactOverlay(object sender, RoutedEventArgs e)
        {
            if (appWindow.Presenter.Kind is not AppWindowPresenterKind.CompactOverlay)
            {
                appWindow.TrySetPresenter(AppWindowPresenterKind.CompactOverlay);
            }
            else
            {
                appWindow.TrySetPresenter(AppWindowPresenterKind.Default);
            }
        }
    }
}
