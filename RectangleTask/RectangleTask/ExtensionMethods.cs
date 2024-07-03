namespace RectangleTask;

public static class ExtensionMethods
{
    public static string JoinByComma(this IEnumerable<object> items)
    {
        return string.Join(", ", items);
    }
    public static string JoinByNewLine(this IEnumerable<object> items)
    {
        return string.Join("\n ", items);
    }
}

