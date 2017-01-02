using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using WinInterop = System.Windows.Interop;
using CefSharp;
using System.Reflection;

namespace Turcam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();          

            this.Browser.BrowserSettings = new CefSharp.BrowserSettings
            {
                OffScreenTransparentBackground = false,
                FileAccessFromFileUrls = CefState.Enabled,
                UniversalAccessFromFileUrls = CefState.Enabled

            };

            World.Initialize();

            this.Browser.RegisterJsObject("windowsApp", new JavascriptInteractionController(), true);
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(Native.WindowProc));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Title += string.Format(" - Version: {0}", version);
            string page = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "View\\index.html");
            this.Browser.Load(page);
        }

        private void Browser_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void GripBottomRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61448, IntPtr.Zero);
        }

        private void GripBottomLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61447, IntPtr.Zero);
        }

        private void GripTopLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61444, IntPtr.Zero);
        }

        private void GripTopRight_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61445, IntPtr.Zero);
        }

        private void BottomGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61446, IntPtr.Zero);
        }

        private void TopGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61443, IntPtr.Zero);
        }

        private void LeftGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61441, IntPtr.Zero);
        }

        private void RightGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61442, IntPtr.Zero);
        }

        private void TitleArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                0xA1, (IntPtr)0x2, (IntPtr)0);
        }

        Brush m_BorderBrush = null;

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                m_BorderBrush = this.BrowserHolder.BorderBrush;

                this.BrowserHolder.BorderBrush = null;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.BrowserHolder.BorderBrush = m_BorderBrush;
            }
        }

        private void TitleArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            }
        }
    }
}
