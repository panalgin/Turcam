using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;
using System.IO;
using Turcam.Commands;
using Turcam.Controller;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

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
            EventSink.PositionChanged += EventSink_PositionChanged;
        }

        private void EventSink_PositionChanged(string axis, ulong position)
        {
            JavascriptInjector.Run(JavascriptInjector.ScriptAction.PositionChanged, axis, position.ToString());
        }

        public void Exit()
        {
            Application.Exit();
        }

        public bool ToggleWindowState()
        {
            bool isMaximized = false;

            MainForm m_Form = Application.OpenForms[0] as MainForm;

            m_Form.Invoke(new Action(() => {
                if (Application.OpenForms[0].WindowState == FormWindowState.Normal)
                {
                    m_Form.WindowState = FormWindowState.Maximized;

                    isMaximized = true;
                }
                else if (m_Form.WindowState == FormWindowState.Maximized)
                {
                    m_Form.WindowState = FormWindowState.Normal;
                    isMaximized = false;
                }

            }));

            return isMaximized;
        }

        public void MinimizeWindowState()
        {
            MainForm m_Form = Application.OpenForms[0] as MainForm;

            m_Form.Invoke(new Action(() =>
            {
                m_Form.WindowState = FormWindowState.Minimized;
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

        #region EventHooks

        private void EventSink_Disconnected(ControlBoard board)
        {
            JavascriptInjector.Run(JavascriptInjector.ScriptAction.Disconnected, string.Empty);
        }

        private void EventSink_Connected(ControlBoard board)
        {
            JavascriptInjector.Run(JavascriptInjector.ScriptAction.Connected, board.SerialConnection.PortName);
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

        #endregion

        /// <summary>
        /// Gets serial communications port available at current computer
        /// </summary>
        /// <returns>Returns the serial communication port names</returns>
        public string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }

        #region DrillBit Handling

        public string GetDrillBits()
        {
            return Controllers.DrillBitController.Read();
        }

        public string GetDrillBit(int id)
        {
            return Controllers.DrillBitController.Get(id);
        }

        public string EditDrillBit(string json)
        {
            return Controllers.DrillBitController.Update(json);
        }

        public string AddDrillBit(string json)
        {
            return Controllers.DrillBitController.Add(json);
        }

        public bool DeleteDrillBit(int id)
        {
            return Controllers.DrillBitController.Delete(id);
        }

        #endregion

        public string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void ShowDevTools()
        {
            MainForm m_Form = Application.OpenForms[0] as MainForm;

            m_Form.Invoke(new Action(() =>
            {
                m_Form.Browser.ShowDevTools();
            }));
        }

        public void JogStart(string parameter)
        {
            switch(parameter)
            {
                case "A:Right":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'A').FirstOrDefault();
                        long pulses = 5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }
                case "A:Left":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'A').FirstOrDefault();
                        long pulses = -5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }
                case "X:Right":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'X').FirstOrDefault();
                        long pulses = 5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }

                case "X:Left":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'X').FirstOrDefault();
                        long pulses = -5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }

                case "Y:Forward":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'Y').FirstOrDefault();
                        long pulses = 5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }

                case "Y:Backward":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'Y').FirstOrDefault();
                        long pulses = -5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }

                case "Z:Up":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'Z').FirstOrDefault();
                        long pulses = -5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }

                case "Z:Down":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'Z').FirstOrDefault();
                        long pulses = 5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }

                case "B:Up":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'B').FirstOrDefault();
                        long pulses = -5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }

                case "B:Down":
                    {
                        Motor motor = World.Motors.Where(q => q.Axis.Name == 'B').FirstOrDefault();
                        long pulses = 5000;

                        JogStartCommand command = new JogStartCommand(motor, pulses);
                        command.Send();

                        break;
                    }
            }
        }

        public void JogEnd()
        {
            JogEndCommand command = new JogEndCommand();
            command.Send();
        }
    }
}
