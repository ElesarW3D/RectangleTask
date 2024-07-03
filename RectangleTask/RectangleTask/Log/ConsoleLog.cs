namespace Log;
public class ConsoleLog : ILog
{
    public void Dispose(){}

    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
