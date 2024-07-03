namespace Figures;
public class ColorRectangle : Rectangle
{

    public ColorRectangle(ConsoleColor color, Point topLeft, double width, double height) : base(topLeft, width, height)
    {
        Color = color;
    }

    public ConsoleColor Color { get; }

    public override string ToString() => $"{base.ToString()} color:{Color}";
}


