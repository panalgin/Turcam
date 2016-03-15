using System.Collections.Generic;

namespace Turcam.Commands
{
    public abstract class BaseCommand
    {
        public string Name { get; set; }
        public string Parameters { get; set; }

        public BaseCommand()
        {

        }

        public override string ToString()
        {
            return string.Format("{0}: {1};", this.Name, this.Parameters);
        }
    }
}