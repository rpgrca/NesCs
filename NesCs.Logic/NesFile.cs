namespace NesCs.Logic;

public class NesFile : INesFile
{
    public static readonly byte[] HeaderSignature = new byte[] { 0x4e, 0x45, 0x53, 0x1A };

    private readonly byte[] _contents;

    public string Filename { get; }

    internal NesFile(string filename, byte[] contents)
    {
        Filename = filename;
        _contents = contents.ToArray();

        ParseHeader();
    }

    private void ParseHeader()
    {
        if (_contents.Length < 16) throw new ArgumentException("Could not find header", nameof(_contents));
        ValidateSignature();
    }

    private void ValidateSignature()
    {
        for (var index = 0; index < HeaderSignature.Length; index++)
        {
            if (_contents[index] != HeaderSignature[index])
            {
                throw new ArgumentException("Signature not found", nameof(_contents));
            }
        }
    }
}