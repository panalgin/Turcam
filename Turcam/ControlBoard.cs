namespace Turcam
{
    public class ControlBoard : IControlBoard
    {
        public SerialConnection SerialConnection { get; set; }
        public string Name { get; set; }
        public virtual void Send(string command)
        {

        }
        public virtual string Read()
        {
            return null;
        }
    }
}