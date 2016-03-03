using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam
{
    interface IControlBoard
    {
        SerialConnection SerialConnection { get; set; }
        string Name { get; set; }
        void Send(string command);
        void Read();
    }
}
