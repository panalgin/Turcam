using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Commands
{
    public class JogEndCommand : BaseCommand
    {
        public JogEndCommand()
        {
            this.Name = "JogEnd";
            this.Parameters = string.Format("{0}", "All");
        }
    }
}
