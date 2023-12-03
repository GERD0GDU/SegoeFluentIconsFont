using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSegoeFluentIconsFont
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HwndSource m_hWndSource = null;
        private HwndSourceHook m_hWndSourceHook = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeEx();
            InitializeUserSettings();
        }

        private void InitializeEx()
        {
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Loaded += new RoutedEventHandler(MainWindow_Loaded);

            Activated += (s, e) =>
            {
                m_mainBorder.BorderBrush = SystemColors.HighlightBrush;
                m_borderTitleBar.Background = (Brush)FindResource("TitleBarBackBrush");
            };
            Deactivated += (s, e) =>
            {
                m_mainBorder.BorderBrush = (Brush)FindResource("TitleBarBackBrush");
            };
            StateChanged += (s, e) =>
            {
                m_mainBorder.BorderThickness = WindowState == WindowState.Maximized
                    ? new Thickness(7)
                    : new Thickness(2);
            };
        }

        private void TerminateEx()
        {
            Loaded -= new RoutedEventHandler(MainWindow_Loaded);
        }

        private void InitializeUserSettings()
        {
            WindowStartupLocation = WindowStartupLocation.Manual;
            this.SetAndRestoreBounds(Properties.Settings.Default.MainWndLocation, Properties.Settings.Default.MainWndSize);
            WindowState = Properties.Settings.Default.MainWndState;

            double dSplitterRatio = Properties.Settings.Default.SplitterPosition.Range(0d, 1.0d);
            double dblWidth = Properties.Settings.Default.MainWndState == WindowState.Maximized
                ? SystemParameters.PrimaryScreenWidth
                : Width;

            m_colLeft.Width = new GridLength(dSplitterRatio, GridUnitType.Star);
            m_colRight.Width = new GridLength(1.0d - dSplitterRatio, GridUnitType.Star);
            if (m_colLeft.MinWidth > 0)
            {
                double dComputeW = dblWidth * dSplitterRatio;
                if (dComputeW < m_colLeft.MinWidth)
                {
                    m_colLeft.Width = new GridLength(1d, GridUnitType.Star);
                }
            }
        }

        private void TerminateUserSettings()
        {
            Grid grid = (Grid)m_colLeft.Parent;
            Properties.Settings.Default.SplitterPosition = Math.Max(m_colLeft.ActualWidth, m_colLeft.MinWidth) / grid.ActualWidth;
            Properties.Settings.Default.MainWndState = (WindowState == WindowState.Minimized)
                ? WindowState.Normal
                : WindowState;
            if (WindowState == WindowState.Normal)
            {
                Properties.Settings.Default.MainWndLocation = new System.Drawing.Point((int)Left, (int)Top);
                Properties.Settings.Default.MainWndSize = new System.Drawing.Size((int)Width, (int)Height);
            }
            Properties.Settings.Default.Save();
        }

        private void AddSourceHook()
        {
            m_hWndSource = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            m_hWndSourceHook = new HwndSourceHook(WndProc);

            m_hWndSource.AddHook(m_hWndSourceHook);
        }

        private void RemoveSourceHook()
        {
            if (m_hWndSource != null && m_hWndSourceHook != null)
            {
                m_hWndSource.RemoveHook(m_hWndSourceHook);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddSourceHook();
        }

        protected override void OnClosed(EventArgs e)
        {
            RemoveSourceHook();
            TerminateEx();
            TerminateUserSettings();

            base.OnClosed(e);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //  do stuff
            if (msg == App.WM_USER_MESSAGE)
            {
                if (wParam == new IntPtr(User32Native.SW_SHOW))
                {
                    this.ForceShow();
                }
                else if (wParam == new IntPtr(User32Native.SW_EXIT))
                {
                    Application.Current.Shutdown();
                }
            }

            return IntPtr.Zero;
        }

        #region Window Operations

        private bool m_bRestoreIfMove = false;
        private double m_dMousePosRatioX = 0;
        private double m_dMousePosY = 0;

        private void Execute_Close(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                if ((ResizeMode == ResizeMode.CanResize) ||
                    (ResizeMode == ResizeMode.CanResizeWithGrip))
                {
                    switch (WindowState)
                    {
                        case WindowState.Maximized:
                            WindowState = WindowState.Normal;
                            break;
                        case WindowState.Normal:
                            WindowState = WindowState.Maximized;
                            break;
                    }
                }
            }
            else
            {
                if (WindowState == WindowState.Maximized)
                {
                    m_bRestoreIfMove = true;

                    Point point = PointToScreen(e.MouseDevice.GetPosition(this));
                    m_dMousePosRatioX = point.X / SystemParameters.PrimaryScreenWidth;
                    m_dMousePosY = point.Y;
                }

                DragMove();
            }
        }

        private void TitleBar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            m_bRestoreIfMove = false;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_bRestoreIfMove)
            {
                m_bRestoreIfMove = false;

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point point = PointToScreen(e.MouseDevice.GetPosition(this));

                    //Left = point.X - (RestoreBounds.Width * 0.5);
                    Left = point.X - (RestoreBounds.Width * m_dMousePosRatioX);
                    Top = point.Y - m_dMousePosY;

                    WindowState = WindowState.Normal;

                    DragMove();
                }
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                Close();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void RestoreDownButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion // Window Operations

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow()
            {
                Owner = this
            }.ShowDialog();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this,
                "This feature is not supported yet.",
                "Information",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        public bool SearchTextFilter(object item)
        {
            string searchText1 = m_txtSearchText.Text ?? "";
            string searchText2 = searchText1.RemoveDiacritics();
            return (!string.IsNullOrEmpty(searchText1)) && (item is ListItemModel lvItemModel)
                ? (((lvItemModel.Name ?? "").IndexOf(searchText1, StringComparison.InvariantCultureIgnoreCase) != -1)
                || ((lvItemModel.Name ?? "").IndexOf(searchText1, StringComparison.CurrentCultureIgnoreCase) != -1)
                || ((lvItemModel.Name ?? "").IndexOf(searchText2, StringComparison.InvariantCultureIgnoreCase) != -1)
                || ((lvItemModel.UnicodeValue ?? "").IndexOf(searchText1, StringComparison.InvariantCultureIgnoreCase) == 0))
                : true;

            
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CollectionViewSource.GetDefaultView(m_lvItems.ItemsSource) is ListCollectionView view)
            {
                if (view.Filter is null)
                {
                    view.Filter = SearchTextFilter;
                }
                else
                {
                    view.Refresh();
                }
            }
        }

        private void m_lvItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_txtCode.Document = (FlowDocument)new Converters.ListItemModel2FlowDocument()
                .Convert(m_lvItems.SelectedItem, typeof(FlowDocument), null, System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}
