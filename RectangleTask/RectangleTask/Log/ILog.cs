namespace Log;
public interface ILog : IDisposable
{
    void Log(string message);
}
