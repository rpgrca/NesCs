namespace NesCs.Logic.File;

internal class Nes20File : OriginalINesFile
{
    private const int HeaderFlags10Index = 10;
    private const int Byte13Index = 13;
    private const int Byte15Index = 15;

    internal Nes20File(string filename, byte[] contents)
        : base(filename, contents)
    {
    }

    protected override void LoadFlags7() =>
        Flags7 = new Flags7ForNes20(_contents[HeaderFlags7Index]);

    protected override void LoadFlags10() =>
        Flags10 = new Flags10(_contents[HeaderFlags10Index]);

    protected override void LoadByte13() =>
        Byte13 = new Byte13(_contents[Byte13Index], Flags7);

    protected override void LoadByte15() =>
        Byte15 = new Byte15(_contents[Byte15Index]);

    public override string ToString() =>
        base.ToString() + "\n" +
            $"""

                Flags 10          : {Flags10}
                Byte 11           : {Byte13}
                Byte 15           : {Byte15}
            """;

}