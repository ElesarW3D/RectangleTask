using RectangleTask;

namespace Figures;
public class Rectangle
{
    public Rectangle(Point topLeft, double width, double height)
    {
        TopLeft = topLeft;
        Width = width > 0 ? width : throw new ArgumentException(nameof(Width));
        Height = height > 0 ? height : throw new ArgumentException(nameof(Height));
    }
    public Point TopLeft { get; protected set; }
    public double Width { get; protected set; }
    public double Height { get; protected set; }
    public Point TopRight => new(TopLeft.X + Width, TopLeft.Y);
    public Point BottomRight => new(TopLeft.X + Width, TopLeft.Y - Height);
    public Point BottomLeft => new(TopLeft.X, TopLeft.Y - Height);
    public IEnumerable<Point> GetPointsСlockwise()
    {
        yield return (Point)TopLeft.Clone();
        yield return TopRight;
        yield return BottomRight;
        yield return BottomLeft;
    }
    public bool IsInside(Rectangle other) 
        => other.GetPointsСlockwise()
            .All(p => IsPointInside(p));


    public bool IsOutside(Rectangle other)
    {
        var isLeft = other.BottomRight.X < TopLeft.X;
        var isRight = other.TopLeft.X > BottomRight.X;
        bool isAbove = other.BottomRight.Y > TopLeft.Y;
        bool isBelow = other.TopLeft.Y < BottomRight.Y;
        return isLeft || isRight || isAbove || isBelow;
    }

    public bool IsIntersect(ColorRectangle other, out ColorRectangle? truncatedRectangle)
    {
        truncatedRectangle = null;
        if  (IsInside(other))
        {
            truncatedRectangle = other;
            return true;
        }
        if (IsOutside(other))
        {
            return false;
        }
        var newTopLeftX = Math.Max(TopLeft.X, other.TopLeft.X);
        var newTopLeftY = Math.Min(TopLeft.Y, other.TopLeft.Y);
        var newBottomRightX = Math.Min(BottomRight.X, other.BottomRight.X);
        var newBottomRightY = Math.Max(BottomRight.Y, other.BottomRight.Y);
        truncatedRectangle = CreateRectangle(new Point(newTopLeftX, newTopLeftY), new Point(newBottomRightX, newBottomRightY), other.Color);
        return true;

    }

    public override bool Equals(object? obj)
    {
        return obj is Rectangle rectangle &&
               EqualityComparer<Point>.Default.Equals(TopLeft, rectangle.TopLeft) &&
               Width == rectangle.Width &&
               Height == rectangle.Height;
    }

    public override int GetHashCode() => HashCode.Combine(TopLeft, Width, Height);
    public override string ToString() => $"Rectangle: {GetPointsСlockwise().JoinByComma()}";
    private bool IsPointInside(Point p)
    {
        bool inside = p.X >= TopLeft.X && p.X<= BottomRight.X && p.Y >= TopLeft.Y && p.Y <= BottomRight.Y;
        return inside;
    }
    public static Rectangle CreateRectangle(Point topLeft, Point bottomRight)
    {
        var width = bottomRight.X - topLeft.X;
        var height = topLeft.Y - bottomRight.Y;
        return new Rectangle(topLeft, width, height);
    }
    public static ColorRectangle CreateRectangle(Point topLeft, Point bottomRight, ConsoleColor color)
    {
        var width = bottomRight.X - topLeft.X;
        var height = topLeft.Y - bottomRight.Y;
        return new ColorRectangle(color, topLeft, width, height);
    }
}
