namespace NesCs.Logic;

public class NesFile
{
    public NesFile(string filename)
    {
        if (string.IsNullOrEmpty(filename)) throw new ArgumentException("Invalid filename", nameof(filename));
    }
}
