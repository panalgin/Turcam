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

        public virtual void Send()
        {
            ControlBoard board = World.ControlBoard;

            if (board != null)
                board.Send(this);
            else
                Logger.Enqueue(new System.Exception("Control board is null"));
        }
    }
}