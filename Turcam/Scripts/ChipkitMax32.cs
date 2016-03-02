using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam
{
    public class ChipkitMax32 : ControlBoard
    {
        public ChipkitMax32()
        {
            this.Name = "chipKIT Max32";
        }

        public ChipkitMax32(string name)
        {
            this.Name = name;
        }

        public override void Send(string command)
        {

        }

        public override string Read()
        {
            return base.Read();
        }
    }
}
