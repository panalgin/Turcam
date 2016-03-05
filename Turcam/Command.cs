using System.Collections.Generic;

namespace Turcam
{
    public enum CommandType
    {
        Hello,
        Homing,
        LinearMove,
        CircularMove,
        ArcMove,
        JogMove,
        SetFeedrate,
        GetPosition
    }

    public class Command
    {
        public CommandType Type { get; set; }
        public KeyValuePair<string, string> Parameters { get; set; }
    }
}