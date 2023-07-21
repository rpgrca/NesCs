namespace NesCs.Logic;

public readonly struct Flags9
{
    private const int TvSystemFlag = 0b1;

    public int TvSystem { get; }

    public Flags9(int flags) => TvSystem = flags & TvSystemFlag;
}