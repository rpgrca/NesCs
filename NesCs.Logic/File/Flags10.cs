namespace NesCs.Logic.File;

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

    public override string ToString() =>
        $"""

                Tv System                 : {TvSystem}
                Program RAM               : {HasProgramRam}
                Board with Bus Conflicts  : {HasBusConflicts}
        """;
}