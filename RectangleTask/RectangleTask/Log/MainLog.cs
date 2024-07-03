namespace Log;

public class MainLog : ILog
{
    private List<ILog> _logs = new();

    public MainLog(IEnumerable<ILog> logs) => _logs = logs.ToList();

    public static ILog CreateAllLog()
    {
        return new MainLog(
                new ILog[]
                {
                    new ConsoleLog(),
                    new FileLog("log.txt")
                }
            );
    }

    public void Dispose() => _logs.ForEach(l => l.Dispose());

    public void Log(string message) => _logs.ForEach(l => l.Log(message));
  
}