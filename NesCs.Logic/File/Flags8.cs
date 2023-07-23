namespace NesCs.Logic.File;

public readonly struct Flags8
{
    public int ProgramRamSize { get; }

    public Flags8(int flags) => ProgramRamSize = flags;
}