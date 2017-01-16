using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Commands
{
    public class HelloCommand : BaseCommand
    {
        public string BoardName { get; set; }
        public HelloCommand(string boardName)
        {
            this.Name = "Hello";
            this.BoardName = boardName;
            this.Parameters = string.Format("{0}", this.BoardName);
        }
    }
}
