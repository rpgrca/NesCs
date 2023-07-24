namespace NesCs.Logic.File;

internal class Nes20File : OriginalINesFile
{
    internal Nes20File(string filename, byte[] contents, NesFileOptions options)
        : base(filename, contents, options)
    {
    }

    protected override void LoadFlags7() =>
        Flags7 = new Flags7ForNes20(_contents[_index++]);

    protected override void LoadFlags10() =>
        Flags10 = new Flags10(_contents[_index++]);

    protected override void LoadByte13() =>
        Byte13 = new Byte13(_contents[_index++], Flags7);

    protected override void LoadByte15() =>
        Byte15 = new Byte15(_contents[_index++]);

    public override string ToString() =>
        base.ToString() + "\n" +
            $"""

                Flags 10          : {Flags10}
                Byte 11           : {Byte13}
                Byte 15           : {Byte15}
            """;

}