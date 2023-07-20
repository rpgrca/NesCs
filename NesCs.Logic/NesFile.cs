namespace NesCs.Logic;

public class NesFile : INesFile
{
    public string Filename { get; }

    internal NesFile(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Invalid filename", nameof(filename));
        Filename = filename;
    }
}