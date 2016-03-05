using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam
{
    public static class EventSink
    {
        public delegate void OnCommandSent(CommandEventArgs args);
        public delegate void OnCommandFailed(CommandEventArgs args);
        public delegate void OnCommandReceived(CommandEventArgs args);

        public static event OnCommandSent CommandSent;
        public static event OnCommandFailed CommandFailed;
        public static event OnCommandReceived CommandReceived;

        public static void InvokeCommandSent(CommandEventArgs args)
        {
            if (CommandSent != null)
                CommandSent(args);
        }

        public static void InvokeCommandFailed(CommandEventArgs args)
        {
            if (CommandFailed != null)
                CommandFailed(args);
        }

        public static void InvokeCommandReceived(CommandEventArgs args)
        {
            if (CommandReceived != null)
                CommandReceived(args);
        }
    }

    public class CommandEventArgs
    {
        public ControlBoard ControlBoard { get; set; }
        public string Command { get; set; }

        public CommandEventArgs(ControlBoard board, string command)
        {
            this.ControlBoard = board;
            this.Command = command;
        }
    }
}
