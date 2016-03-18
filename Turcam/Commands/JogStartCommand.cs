using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Commands
{
    public class JogStartCommand : BaseCommand
    {
        public Motor AffectedMotor { get; set; }

        public JogStartCommand(Motor affected, long pulses)
        {
            this.Name = "JogStart";

            this.Parameters = string.Format("{0}:{1}", affected.Axis.ToString(), pulses.ToString());
        }
    }
}
