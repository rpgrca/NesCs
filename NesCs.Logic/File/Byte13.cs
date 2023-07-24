namespace NesCs.Logic.File;

public readonly struct Byte13
{
    public ConsoleType ExtendedConsoleType { get; }
    public int PpuType { get; }
    public int HardwareType { get; }

    public Byte13(int flags, Flags7 flags7)
    {
        ExtendedConsoleType = (ConsoleType)(flags & 0b1111);
        if (((int)flags7.ConsoleType & 1) == 1)
        {
            PpuType = flags & 0b1111;
            HardwareType = flags >> 4 & 0b1111;
        }
    }

    public override string ToString() =>
        $"""

                PPU Type                  : {PpuType}
                Hardware Type             : {HardwareType}
        """;
}