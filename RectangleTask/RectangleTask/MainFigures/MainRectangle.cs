using Figures;
using Log;
using RectangleTask;

namespace MainFigures;
public class MainRectangle : Rectangle
{
    private ILog? _log;
    private readonly List<ColorRectangle> _rectangles = new();
    
    public MainRectangle(Point topLeft, double width, double height) : base(topLeft, width, height)
    {
    }

    public void SetLog(ILog log) => _log = log;
    public void AddRectangle(ColorRectangle rectangle)
    {
        SaveMessage($"Add rectangle {rectangle}");
        _rectangles.Add(rectangle);
    }
   
    public void AddRectangles(IEnumerable<ColorRectangle> rectangles)
    {
        SaveMessage($"Add rectangles {rectangles.JoinByNewLine()}");
        _rectangles.AddRange(rectangles);
    }
    
    
    public void SetExtremeRectangle(bool useExceptOuter = false, Predicate<ColorRectangle>? filter = null)
    {
        var rectangle = GetExtremeRectangle(useExceptOuter, filter);
        Set(rectangle);
    }

    public Rectangle GetExtremeRectangle(bool useExceptOuter = false, Predicate<ColorRectangle>? filter = null)
    {
        SaveMessage($"Find Extreme rectangle\n options:\n use except outer: {useExceptOuter}\n use filter:{filter != null}");
        IEnumerable<ColorRectangle> filtredRectangles = _rectangles;

        if (filter != null)
        {
            filtredRectangles = filtredRectangles.Where(rect => filter(rect)).ToArray();
            SaveMessage($"Rectangles after filtered {filtredRectangles.JoinByNewLine()}");
        }

        var filteredTruncatedRectangles = new List<ColorRectangle>(filtredRectangles.Count());
        
        if (useExceptOuter)
        {
            foreach (var rectangle in filtredRectangles)
            {
                if (IsIntersect(rectangle, out var truncatedRectangle))
                {
                    if (truncatedRectangle != null)
                    {
                        filteredTruncatedRectangles.Add(truncatedRectangle);
                    }
                }
            }
            SaveMessage($"Rectangles after trancated {filteredTruncatedRectangles.JoinByNewLine()}");
        }
        else
        {
            filteredTruncatedRectangles.AddRange(filtredRectangles);
            SaveMessage($"No use rectangles after trancated");
        }

        if (!filteredTruncatedRectangles.Any())
        {
            SaveMessage($"Error: no rectangles left");
            throw new ArgumentException();
        }
        var extremeRectangle = FindExtreme(filteredTruncatedRectangles);
        SaveMessage($"Extreme rectangle :{extremeRectangle}");
        return extremeRectangle;
    }

    private static Rectangle FindExtreme(IEnumerable<ColorRectangle> _rectangles)
    {
        var first = _rectangles.First();
        var topLeft = first.TopLeft;
        var bottomRight = first.BottomRight;
        var xTop = topLeft.X;
        var yTop = topLeft.Y;
        var xBottom = bottomRight.X;
        var yBottom = bottomRight.Y;

        foreach (var rectangle in _rectangles.Skip(1))
        {
            var topLeftRectangle = rectangle.TopLeft;
            if (topLeftRectangle.X < xTop)
            {
                xTop = topLeftRectangle.X;
            }
            if (topLeftRectangle.Y > yTop)
            {
                yTop = topLeftRectangle.Y;
            }
            var bottomRightRectangle = rectangle.BottomRight;
            if (bottomRightRectangle.X > xBottom)
            {
                xBottom = bottomRightRectangle.X;
            }
            if (bottomRightRectangle.Y < yBottom)
            {
                yBottom = bottomRightRectangle.Y;
            }
        }
        return CreateRectangle(new Point(xTop, yTop), new Point(xBottom, yBottom));
    }
    
    private void Set(Rectangle rectangle)
    {
        SaveMessage($"Setting the rectangle:{rectangle} to main");
        TopLeft = rectangle.TopLeft;
        Width = rectangle.Width;
        Height = rectangle.Height;
    }

    private void SaveMessage(string message) => _log?.Log(message);

}

