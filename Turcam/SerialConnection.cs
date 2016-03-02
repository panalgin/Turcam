using System.IO.Ports;

namespace Turcam
{
    public class SerialConnection : SerialPort
    {
        public SerialConnection(string portName, int baudRate)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;

            this.ErrorReceived += OnErrorReceived;
            this.DataReceived += OnDataReceived;
            this.PinChanged += OnPinChanged;
        }

        public virtual void OnPinChanged(object sender, SerialPinChangedEventArgs e)
        {
            
        }

        public virtual void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        public virtual void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            
        }
    }
}