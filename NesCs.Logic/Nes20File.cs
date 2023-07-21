namespace NesCs.Logic;

internal class Nes20File : OriginalINesFile
{
    private const int HeaderFlags10Index = 10;

    internal Nes20File(string filename, byte[] contents)
        : base(filename, contents)
    {
    }

    protected override void LoadFlags7() =>
        Flags7 = new Flags7ForNes20(_contents[HeaderFlags7Index]);

    protected override void LoadFlags10() =>
        Flags10 = new Flags10(_contents[HeaderFlags10Index]);
}