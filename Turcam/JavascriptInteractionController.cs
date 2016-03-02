using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
            SerialConnection serialConnection = new SerialConnection(port, baud);
            serialConnection.Open();
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
