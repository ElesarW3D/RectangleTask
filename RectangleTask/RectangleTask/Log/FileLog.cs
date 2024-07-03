namespace Log;
public class FileLog : ILog
{
    private BinaryWriter _fileStrem;
    public FileLog(string fileName)
    {
        _fileStrem = new BinaryWriter(new FileStream(fileName, FileMode.Create));
    }

    public void Dispose()
    {
        _fileStrem?.Dispose();
    }

    public void Log(string message)
    {
       _fileStrem.Write(message);
    }
}