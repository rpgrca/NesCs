namespace NesCs.Logic;

internal class OriginalINesFile : NesFile
{
    internal OriginalINesFile(string filename, byte[] contents)
        : base(filename, contents)
    {
    }
}