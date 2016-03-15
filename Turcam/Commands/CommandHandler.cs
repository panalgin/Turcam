using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Commands
{
    public static class CommandHandler
    {
        public static void Send(ControlBoard board, BaseCommand command)
        {
            if (board.IsConnected)
            {
                board.Send(command.ToString());
            }
        }

        public static void Send(BaseCommand command)
        {
            if (World.ControlBoard != null)
                Send(World.ControlBoard, command);
        }
    }
}
