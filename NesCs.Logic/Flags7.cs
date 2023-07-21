namespace NesCs.Logic;

public readonly struct Flags7
{
    private const int VsUnisystemFlag = 0b1;
    private const int PlayChoice10Flag = 0b10;
    private const int NesFormatVersion = 0b1100;
    private const int UpperMapperNybbleFlag = 0b11110000;

    public bool HasVsUnisystem { get; }
    public bool HasPlayChoice10 { get; }
    public bool HasVersion2Format { get; }
    internal int UpperMapperNybble { get; }

    public Flags7(int flags)
    {
        HasVsUnisystem = (flags & VsUnisystemFlag) == VsUnisystemFlag;
        HasPlayChoice10 = (flags & PlayChoice10Flag) == PlayChoice10Flag;
        HasVersion2Format = ((flags & NesFormatVersion) >> 2) == 2;
        UpperMapperNybble = (flags & UpperMapperNybbleFlag) >> 4;
    }
}