using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Commands
{
    public class JogCommand : BaseCommand
    {
        public Motor AffectedMotor { get; set; }

        public JogCommand(Motor affected, long pulses)
        {
            this.Name = "Jog";

            this.Parameters = string.Format("{0}:{1}", affected.Axis.ToString(), pulses.ToString());
        }
    }
}
