namespace Figures;
public class Point : ICloneable
{
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }
    public double Y { get; }

    public object Clone() => new Point(X, Y);

    public override bool Equals(object? obj)
    {
        return obj is Point point &&
               X == point.X &&
               Y == point.Y;
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);


    public override string ToString() => $"Point X:{X} Y:{Y}";

}
