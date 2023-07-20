namespace NesCs.Logic;

public class NesFile
{
    public NesFile(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Invalid filename", nameof(filename));
    }
}
