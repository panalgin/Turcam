using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turcam.Commands
{
    public class CommandHandler
    {
        public ControlBoard Board { get; private set; }
        public CommandHandler(ControlBoard board)
        {
            this.Board = board;
        }

        private void Send(string command)
        { 
            try
            {
                this.Board.SerialConnection.Write(command);

                CommandEventArgs args = new CommandEventArgs(this.Board, command);
                EventSink.InvokeCommandSent(args);
            }
            catch (Exception ex)
            {
                Logger.Enqueue(ex);
                CommandEventArgs args = new CommandEventArgs(this.Board, command);
                EventSink.InvokeCommandFailed(args);
            }
        }

        public void Send(BaseCommand command)
        {
            if (command is HelloCommand)
                this.Send(command.ToString());
            else
            {
                if (this.Board.IsConnected)
                    this.Send(command.ToString());
            }
        }

        public void Parse(string data)
        {
            if (data.EndsWith(Environment.NewLine) || data.EndsWith("\0"))
            {
                data = data.Replace("\r\n", "").Replace("\n", "").Replace("\0", "");

                EventSink.InvokeCommandReceived(new CommandEventArgs(this.Board, data));

                if (data.Contains(string.Format("Hello: {0}", this.Board.Name)))
                {
                    this.Board.IsConnected = true;
                    EventSink.InvokeConnected(this.Board);
                }

                else if (data.StartsWith("Pos: ")) // Format: Pos: A:5000,B:-500,C:123
                {
                    data = data.Replace("Pos: ", "");

                    string[] parsed = data.Split(',');

                    foreach (string i in parsed)
                    {
                        string[] item = i.Split(':');
                        string axis = item[0];
                        string value = item[1];

                        ulong pos = 0;
                        bool isNumber = ulong.TryParse(value, out pos);

                        if (isNumber)
                            EventSink.InvokePositionChanged(axis, pos);

                    }
                }
            }
        }

        public void Read()
        {
            int length = this.Board.SerialConnection.BytesToRead;
            byte[] buffer = new byte[length];
            this.Board.SerialConnection.Read(buffer, 0, length);

            string data = Encoding.UTF8.GetString(buffer);

            this.Parse(data);
        }
    }
}
