using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Commands
{
    public class JogMoveCommand : BaseCommand
    {
        public Motor AffectedMotor { get; set; }

        public JogMoveCommand()
        {

        }
    }
}
