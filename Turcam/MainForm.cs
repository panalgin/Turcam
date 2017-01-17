using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turcam
{
    public partial class MainForm : Form
    {
        public IWinFormsWebBrowser Browser { get; set; }
        public Dictionary<uint, Panel> Thumbs = new Dictionary<uint, Panel>();

        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitBrowser();
            InitThumbs();

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

        void InitThumbs()
        {
            TransparentPanel m_Panel = new TransparentPanel();
            m_Panel.Height = 25;
            m_Panel.Width = 100;
            m_Panel.Dock = DockStyle.Top;

            this.Controls.Add(m_Panel);
            m_Panel.BringToFront();
            m_Panel.MouseDown += Panel_MouseDown;

            const UInt32 HTLEFT = 10;
            const UInt32 HTRIGHT = 11;
            const UInt32 HTBOTTOMRIGHT = 17;
            const UInt32 HTBOTTOM = 15;
            const UInt32 HTBOTTOMLEFT = 16;
            const UInt32 HTTOP = 12;
            const UInt32 HTTOPLEFT = 13;
            const UInt32 HTTOPRIGHT = 14;

            this.Thumbs = new Dictionary<uint, Panel>()
            {
                { HTBOTTOMLEFT, this.BL_Thumb },
                { HTBOTTOM, this.B_Thumb },
                { HTBOTTOMRIGHT, this.BR_Thumb },
                { HTRIGHT, this.R_Thumb },
                { HTTOPRIGHT, this.TR_Thumb },
                { HTTOP, this.T_Thumb },
                { HTTOPLEFT, this.TL_Thumb },
                { HTLEFT, this.L_Thumb }
            };

            this.Thumbs.All(delegate (KeyValuePair<uint, Panel> kvp)
            {
                kvp.Value.BringToFront();
                //kvp.Value.Visible = false;

                return true;
            });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += string.Format(" - Version: {0}", version);
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks >= 2)
            {
                if (this.WindowState == FormWindowState.Normal)
                    this.WindowState = FormWindowState.Maximized;
                else
                    this.WindowState = FormWindowState.Normal;

                return;
            }
            else if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #region Native

        protected override void WndProc(ref Message m)
        {
            const UInt32 WM_NCHITTEST = 0x0084;
            const UInt32 WM_MOUSEMOVE = 0x0200;

            bool handled = false;

            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Size formSize = this.Size;
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                foreach (KeyValuePair<UInt32, Panel> kvp in this.Thumbs)
                {
                    if (kvp.Value.Bounds.Contains(clientPoint))
                    {
                        m.Result = (IntPtr)kvp.Key;
                        handled = true;
                        break;
                    }
                }
            }

            if (!handled)
                base.WndProc(ref m);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
