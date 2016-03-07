using System;
using System.Text;

namespace Turcam
{
    public class ControlBoard : IControlBoard, IDisposable
    {
        private bool isConnected = false;

        public SerialConnection SerialConnection { get; set; }
        public string Name { get; set; }
        public bool IsConnected
        {
            get
            {
                if (isConnected == true && this.SerialConnection != null && this.SerialConnection.IsOpen)
                    return true;
                else
                    return false;
            }
            private set { isConnected = value; }
        }


        public ControlBoard(SerialConnection connection, string name = "Default")
        {
            this.Name = name;
            this.SerialConnection = connection;
            this.SerialConnection.DataReceived += SerialConnection_DataReceived;
            this.SerialConnection.ErrorReceived += SerialConnection_ErrorReceived;
            this.SerialConnection.PinChanged += SerialConnection_PinChanged;
        }

        public virtual void Connect()
        {
            if (this.SerialConnection != null)
            {
                if (this.SerialConnection.PortName != string.Empty && this.SerialConnection.BaudRate > 0)
                {
                    try
                    {
                        if (this.SerialConnection.IsOpen)
                            this.SerialConnection.Close();

                        this.SerialConnection.NewLine = Environment.NewLine;
                        this.SerialConnection.Encoding = Encoding.UTF8;
                        this.SerialConnection.Open();
                        this.Send(string.Format("Hello {0};", this.Name));

                    }
                    catch (Exception ex)
                    {
                        Logger.Enqueue(ex);
                    }
                }
            }
        }

        public virtual void Disconnect()
        {
            if (this.SerialConnection != null)
            {
                if (this.SerialConnection.IsOpen)
                    this.SerialConnection.Close();
            }

            EventSink.InvokeDisconnected(this);

            this.Dispose();
        }

        public virtual void SerialConnection_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
        {

        }

        public virtual void SerialConnection_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            
        }

        public virtual void SerialConnection_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Read();
        }

        public virtual void Parse(string data)
        {
            if (data.EndsWith(Environment.NewLine))
            {
                data = data.Replace("\r\n", "");

                EventSink.InvokeCommandReceived(new CommandEventArgs(this, data));

                if (data == string.Format("Hello {0}", this.Name))
                {
                    this.IsConnected = true;
                    EventSink.InvokeConnected(this);
                }
            }
        }

        public virtual void Send(string command)
        {
            if (this.SerialConnection != null && this.SerialConnection.IsOpen)
            {
                try {
                    this.SerialConnection.Write(command);

                    CommandEventArgs args = new CommandEventArgs(this, command);
                    EventSink.InvokeCommandSent(args);
                }
                catch(Exception ex)
                {
                    Logger.Enqueue(ex);
                    CommandEventArgs args = new CommandEventArgs(this, command);
                    EventSink.InvokeCommandFailed(args);
                }
            }
        }

        public virtual void Read()
        {
            int length = this.SerialConnection.BytesToRead;
            byte[] buffer = new byte[length];
            this.SerialConnection.Read(buffer, 0, length);

            string data = Encoding.UTF8.GetString(buffer);

            Parse(data);
        }

        public void Dispose()
        {
            this.SerialConnection.Dispose();
        }
    }
}