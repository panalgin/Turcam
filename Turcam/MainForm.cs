using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Turcam.Logic;

namespace Turcam
{
    public partial class MainForm : Form
    {
        public IWinFormsWebBrowser Browser { get; set; }

        public MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitBrowser();
            World.Initialize();
        }

        void InitBrowser()
        {
            Cef.Initialize(new CefSettings()
            {

            });

            string page = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "View\\index.html");

            var browser = new ChromiumWebBrowser(page)
            {
                Dock = DockStyle.Fill
            };

            browser.BrowserSettings = new BrowserSettings()
            {
                FileAccessFromFileUrls = CefState.Enabled,
                UniversalAccessFromFileUrls = CefState.Enabled,
            };

            this.Browser = browser;
            this.Controls.Add(browser);

            this.Browser.RegisterJsObject("windowsApp", new JavascriptInteractionController());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += string.Format(" - Version: {0}", version);
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        #region Resizing & Dragging
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public void ForceDrag()
        {
            this.Invoke(new Action(() =>
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, IntPtr.Zero);
            }));
        }

        public void ForceResize(ResizeDirection dir)
        {
            this.Invoke(new Action(() =>
            {
                ReleaseCapture();

                switch (dir)
                {
                    case ResizeDirection.Left: SendMessage(this.Handle, 0x112, (IntPtr)61441, IntPtr.Zero); break;
                    case ResizeDirection.Right: SendMessage(this.Handle, 0x112, (IntPtr)61442, IntPtr.Zero); break;
                    case ResizeDirection.Top: SendMessage(this.Handle, 0x112, (IntPtr)61443, IntPtr.Zero); break;
                    case ResizeDirection.Bottom: SendMessage(this.Handle, 0x112, (IntPtr)61446, IntPtr.Zero); break;
                    case ResizeDirection.TopLeft: SendMessage(this.Handle, 0x112, (IntPtr)61444, IntPtr.Zero); break;
                    case ResizeDirection.BottomLeft: SendMessage(this.Handle, 0x112, (IntPtr)61447, IntPtr.Zero); break;
                    case ResizeDirection.TopRight: SendMessage(this.Handle, 0x112, (IntPtr)61445, IntPtr.Zero); break;
                    case ResizeDirection.BottomRight: SendMessage(this.Handle, 0x112, (IntPtr)61448, IntPtr.Zero); break;
                }
            }));
        }
        #endregion
    }
}
