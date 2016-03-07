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
            EventSink.CommandSent += EventSink_CommandSent;
            EventSink.CommandFailed += EventSink_CommandFailed;
            EventSink.CommandReceived += EventSink_CommandReceived;
            EventSink.Connected += EventSink_Connected;
            EventSink.Disconnected += EventSink_Disconnected;
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
                m_Control.Connect();

                World.ControlBoard = m_Control;
            }
            else
            {
                if (!World.ControlBoard.IsConnected)
                {
                    World.ControlBoard.SerialConnection.Dispose();

                    SerialConnection serialConnection = new SerialConnection(port, baud);
                    World.ControlBoard.SerialConnection = serialConnection;
                    World.ControlBoard.Connect();
                }
                else
                {
                    //TODO add already connected feature
                }
            }
        }

        public void Disconnect()
        {
            if (World.ControlBoard != null)
                World.ControlBoard.Disconnect();
        }


        private void EventSink_Disconnected(ControlBoard board)
        {
            JavascriptInjector.Run(JavascriptInjector.ScriptAction.Disconnected, string.Empty);
        }

        private void EventSink_Connected(ControlBoard board)
        {
            JavascriptInjector.Run(JavascriptInjector.ScriptAction.Connected, string.Empty);
        }

        private void EventSink_CommandReceived(CommandEventArgs args)
        {
            JavascriptInjector.Run(JavascriptInjector.ScriptAction.CommandReceived, args.Command);
        }

        private void EventSink_CommandFailed(CommandEventArgs args)
        {
            JavascriptInjector.Run(JavascriptInjector.ScriptAction.CommandFailed, args.Command);
        }

        private void EventSink_CommandSent(CommandEventArgs args)
        {
            JavascriptInjector.Run(JavascriptInjector.ScriptAction.CommandSent, args.Command); // Sent: Hello Default;
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
