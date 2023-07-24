namespace NesCs.Logic.File;

public record Flags7ForNes20 : Flags7
{
    private const int ConsoleTypeMask = 0b11;

    public Flags7ForNes20(int flags)
        : base(flags) =>
        ConsoleType = (ConsoleType)(flags & ConsoleTypeMask);
}