namespace NesCs.Logic;

public readonly struct Flags10
{
    private const int TvSystemFlag = 0b11;
    private const int ProgramRamPresentFlag = 0b10000;
    private const int BoardHasBusConflictsFlag = 0b100000;

    public int TvSystem { get; }
    public bool HasProgramRam { get; }
    public bool HasBusConflicts { get; }

    public Flags10(int flags)
    {
        TvSystem = flags & TvSystemFlag;
        HasProgramRam = (flags & ProgramRamPresentFlag) == ProgramRamPresentFlag;
        HasBusConflicts = (flags & BoardHasBusConflictsFlag) == BoardHasBusConflictsFlag;
    }
}

public readonly struct Byte13
{
    public ConsoleType ExtendedConsoleType { get; }
    public int PpuType { get; }
    public int HardwareType { get; }

    public Byte13(int flags, Flags7 flags7)
    {
        ExtendedConsoleType  = (ConsoleType)(flags & 0b1111);
        if (((int)flags7.ConsoleType & 1) == 1)
        {
            PpuType = flags & 0b1111;
            HardwareType = (flags >> 4) & 0b1111;
        }
    }
}