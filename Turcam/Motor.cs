namespace Turcam
{
    public class Motor
    {
        public Axis Axis { get; set; }
        public Motor(Axis axis)
        {
            this.Axis = axis;
        }
    }
}