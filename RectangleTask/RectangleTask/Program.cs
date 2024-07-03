using Figures;
using Log;
using MainFigures;

using (var logs = MainLog.CreateAllLog())
{
    var mainRectangle = new MainRectangle(new Point(2, 5), 5, 5);
    logs.Log($"Create main rectangle:{mainRectangle}");
    mainRectangle.SetLog(logs);

    var secondRedRectangle = new ColorRectangle(ConsoleColor.Red, new Point(3, 3), 1, 1);
    var secondBlueRectangle = new ColorRectangle(ConsoleColor.Blue, new Point(2, 2), 1, 1);
    var secondRedRectangle2 = new ColorRectangle(ConsoleColor.Red, new Point(3, 1), 3, 2);
    var secondBlueRectangle2 = new ColorRectangle(ConsoleColor.Red, new Point(8, 2), 1, 1);

    mainRectangle.AddRectangles(
       new[]{
                        secondRedRectangle,
                        secondBlueRectangle,
                        secondRedRectangle2,
                        secondBlueRectangle2
            }
        );

    mainRectangle.GetExtremeRectangle();

    Predicate<ColorRectangle> filter = x => x.Color == ConsoleColor.Red;
    logs.Log("Crete filter color = red");
    mainRectangle.GetExtremeRectangle(filter:filter);
    mainRectangle.GetExtremeRectangle(useExceptOuter:true);
    mainRectangle.SetExtremeRectangle(useExceptOuter: true, filter:filter);
}
    



