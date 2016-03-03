using System;

namespace Turcam
{
    public class ControlBoard : IControlBoard
    {
        public SerialConnection SerialConnection { get; set; }
        public string Name { get; set; }
        public bool IsConnected { get { return this.SerialConnection != null ? this.SerialConnection.IsOpen : false; } }


        public ControlBoard()
        {
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
                        if (!this.SerialConnection.IsOpen)
                            this.SerialConnection.Open();
                    }
                    catch(Exception ex)
                    {
                        Logger.Enqueue(ex);
                    }
                }
            }
        }

        public virtual void SerialConnection_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
        {

        }

        public virtual void SerialConnection_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            
        }

        public virtual void SerialConnection_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (e.EventType == System.IO.Ports.SerialData.Eof)
            {
                Read();
            }
        }

        public virtual void Parse(string data)
        {

        }


        public virtual void Send(string command)
        {

        }

        public virtual void Read()
        {
            int length = this.SerialConnection.BytesToRead;
            byte[] buffer = new byte[length];
            this.SerialConnection.Read(buffer, 0, length);

            string data = BitConverter.ToString(buffer);

            Parse(data);
        }
    }
}