using System;
using System.Text;
using Turcam.Commands;

namespace Turcam
{
    public class ControlBoard : IControlBoard, IDisposable
    {
        private bool isConnected = false;
        private SerialConnection serialConnection;
        private CommandHandler commandHandler;

        public SerialConnection SerialConnection {
            get
            {
                return serialConnection;
            }
            set
            {
                serialConnection = value;
                CheckHooks();
            }
        }
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

            set { isConnected = value; }
        }

        public ControlBoard(SerialConnection connection, string name = "Default")
        {
            commandHandler = new CommandHandler(this);

            this.Name = name;
            this.SerialConnection = connection;
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
                        this.Send(new HelloCommand(this.Name));

                    }
                    catch (Exception ex)
                    {
                        Logger.Enqueue(ex);
                    }
                }
            }
        }

        private void CheckHooks()
        {
            if (serialConnection == null)
            {
                serialConnection.DataReceived -= SerialConnection_DataReceived;
                serialConnection.ErrorReceived -= SerialConnection_ErrorReceived;
                serialConnection.PinChanged -= SerialConnection_PinChanged;
            }
            else
            {
                serialConnection.DataReceived += SerialConnection_DataReceived;
                serialConnection.ErrorReceived += SerialConnection_ErrorReceived;
                serialConnection.PinChanged += SerialConnection_PinChanged;
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
            commandHandler.Read();
        }

        public void Dispose()
        {
            this.SerialConnection.Dispose();
        }

        public void Send(BaseCommand command)
        {
            commandHandler.Send(command);
        }
    }
}