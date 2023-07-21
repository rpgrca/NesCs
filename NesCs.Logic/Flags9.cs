namespace NesCs.Logic;

public readonly struct Flags9
{
    private const int TvSystemFlag = 0b1;

    public int TvSystem { get; }

    public Flags9(int flags) => TvSystem = flags & TvSystemFlag;
}

public readonly struct Flags10
{
    private const int TvSystemFlag = 0b11;

    public int TvSystem { get; }

    public Flags10(int flags)
    {
        TvSystem = flags & TvSystemFlag;
    }
}