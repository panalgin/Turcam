using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam
{
    public static class CommandHandler
    {
        public static void Send(ControlBoard board, Command command)
        {
            if (board.IsConnected)
            {
                //
            }
        }

        public static void Send(Command command)
        {
            if (World.ControlBoard != null)
                Send(World.ControlBoard, command);
        }
    }
}
