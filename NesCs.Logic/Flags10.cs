namespace NesCs.Logic;

public readonly struct Flags10
{
    private const int TvSystemFlag = 0b11;
    private const int ProgramRamPresentFlag = 0b10000;

    public int TvSystem { get; }
    public bool HasProgramRam { get; }

    public Flags10(int flags)
    {
        TvSystem = flags & TvSystemFlag;
        HasProgramRam = (flags & ProgramRamPresentFlag) == ProgramRamPresentFlag;
    }
}