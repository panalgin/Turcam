using System.IO.Ports;

namespace Turcam
{
    public class SerialConnection : SerialPort
    {
        public SerialConnection(string portName, int baudRate)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
        }
    }
}