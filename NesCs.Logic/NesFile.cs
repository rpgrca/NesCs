namespace NesCs.Logic;

public class NesFile
{
    public NesFile(string filename, FileSystemWrapper fileSystem)
    {
        if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Invalid filename", nameof(filename));
    }
}