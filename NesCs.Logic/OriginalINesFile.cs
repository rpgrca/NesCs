namespace NesCs.Logic;

internal class OriginalINesFile : ArchaicINesFile
{
    private const int HeaderFlags7Index = 7;
    private const int HeaderFlags8Index = 8;
    private const int HeaderFlags9Index = 9;

    internal OriginalINesFile(string filename, byte[] contents)
        : base(filename, contents)
    {
    }

    protected override void LoadFlags7() =>
        Flags7 = new Flags7(_contents[HeaderFlags7Index]);

    protected override void LoadMapperNumber() =>
        MapperNumber = (Flags7.UpperMapperNybble << 4) | Flags6.LowerMapperNybble;

    protected override void LoadFlags8() =>
        Flags8 = new Flags8(_contents[HeaderFlags8Index]);

    protected override void LoadFlags9() =>
        Flags9 = new Flags9(_contents[HeaderFlags9Index]);
}