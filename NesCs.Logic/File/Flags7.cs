namespace NesCs.Logic.File;

public record Flags7
{
    private const int VsUnisystemFlag = 0b1;
    private const int PlayChoice10Flag = 0b10;
    private const int NesFormatVersion = 0b1100;
    private const int UpperMapperNybbleFlag = 0b11110000;
    public ConsoleType ConsoleType { get; init; }
    public bool HasVersion2Format { get; init; }
    internal int UpperMapperNybble { get; }

    public Flags7(int flags)
    {
        if ((flags & VsUnisystemFlag) == VsUnisystemFlag)
        {
            ConsoleType = ConsoleType.NintendoVersusSystem;
        }

        if ((flags & PlayChoice10Flag) == PlayChoice10Flag)
        {
            ConsoleType = ConsoleType.NintendoPlaychoice10;
        }

        HasVersion2Format = ((flags & NesFormatVersion) >> 2) == 2;
        UpperMapperNybble = (flags & UpperMapperNybbleFlag) >> 4;
    }

    public override string ToString() =>
        $"""

                Console Type              : {ConsoleType}
                Has 2.0 Format            : {HasVersion2Format}
                Upper Mapper Nybble       : {UpperMapperNybble}
        """;
}