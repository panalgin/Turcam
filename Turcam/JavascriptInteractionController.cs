using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CefSharp;
using System.IO;

namespace Turcam
{
    public class JavascriptInteractionController
    {
        public JavascriptInteractionController()
        {

        }

        public void Exit()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action( delegate ()
            {
                Application.Current.Shutdown();
            }));
        }

        public bool ToggleWindowState()
        {
            bool isMaximized = false;
            Application.Current.Dispatcher.Invoke(new Action(() => {
                if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;

                    isMaximized = true;
                }
                else if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                    isMaximized = false;
                }

            }), DispatcherPriority.ContextIdle);

            return isMaximized;
        }

        public void MinimizeWindowState()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }));
        }

        public void Connect(string port, int baud)
        {
            if (World.ControlBoard == null)
            {
                SerialConnection serialConnection = new SerialConnection(port, baud);
                ControlBoard m_Control = new ControlBoard(serialConnection);
                EventSink.CommandSent += EventSink_CommandSent;
                EventSink.CommandFailed += EventSink_CommandFailed;
                EventSink.CommandReceived += EventSink_CommandReceived;
                m_Control.Connect();

                World.ControlBoard = m_Control;
            }
            else
            {
                if (!World.ControlBoard.IsConnected)
                    World.ControlBoard.Connect();
                else
                {
                    //TODO add already connected feature
                }
            }
        }

        private void EventSink_CommandReceived(CommandEventArgs args)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MainWindow m_Window = Application.Current.MainWindow as MainWindow;
                string m_Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "View\\js\\async\\command-received.js");
                string m_Script = string.Empty;

                if (File.Exists(m_Path))
                {
                    using (StreamReader reader = new StreamReader(m_Path))
                    {
                        m_Script = reader.ReadToEnd();
                    }

                    m_Window.Browser.ExecuteScriptAsync(string.Format(m_Script, args.Command));
                }
                else
                    throw new FileNotFoundException(m_Path + " bulunamadı.");
            }));
        }

        private void EventSink_CommandFailed(CommandEventArgs args)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MainWindow m_Window = Application.Current.MainWindow as MainWindow;
                string m_Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "View\\js\\async\\command-failed.js");
                string m_Script = string.Empty;

                if (File.Exists(m_Path))
                {
                    using (StreamReader reader = new StreamReader(m_Path))
                    {
                        m_Script = reader.ReadToEnd();
                    }

                    m_Window.Browser.ExecuteScriptAsync(string.Format(m_Script, args.Command));
                }
                else
                    throw new FileNotFoundException(m_Path + " bulunamadı.");
            }));
        }

        private void EventSink_CommandSent(CommandEventArgs args)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MainWindow m_Window = Application.Current.MainWindow as MainWindow;
                string m_Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "View\\js\\async\\command-sent.js");
                string m_Script = string.Empty;

                if (File.Exists(m_Path))
                {
                    using (StreamReader reader = new StreamReader(m_Path))
                    {
                        m_Script = reader.ReadToEnd();
                    }

                    m_Window.Browser.ExecuteScriptAsync(string.Format(m_Script, args.Command));
                }
                else
                {
                    throw new FileNotFoundException(m_Path + " bulunamadı.");
                }
            }));
        }

        /// <summary>
        /// Gets serial communications port available at current computer
        /// </summary>
        /// <returns>Returns the serial communication port names</returns>
        public string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }
    }
}
