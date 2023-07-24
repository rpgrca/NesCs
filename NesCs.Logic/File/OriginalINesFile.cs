namespace NesCs.Logic.File;

internal class OriginalINesFile : ArchaicINesFile
{
    internal OriginalINesFile(string filename, byte[] contents, NesFileOptions options)
        : base(filename, contents, options)
    {
    }

    protected override void LoadFlags7() =>
        Flags7 = new Flags7(_contents[_index++]);

    protected override void LoadMapperNumber() =>
        MapperNumber = (Flags7.UpperMapperNybble << 4) | Flags6.LowerMapperNybble;

    protected override void LoadFlags8() =>
        Flags8 = new Flags8(_contents[_index++]);

    protected override void LoadFlags9() =>
        Flags9 = new Flags9(_contents[_index++]);

    public override string ToString() =>
        base.ToString() + "\n" +
            $"""
                Flags 7           : {Flags7}
                Flags 8           : {Flags8}
                Flags 9           : {Flags9}
            """;
}