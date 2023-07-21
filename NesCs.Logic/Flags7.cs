namespace NesCs.Logic;

public struct Flags7
{
    private const int VsUnisystemFlag = 0b00000001;
    private const int PlayChoice10Flag = 0b00000010;
    private const int NesFormatVersion = 0b00001100;

    public bool HasVsUnisystem { get; }
    public bool HasPlayChoice10 { get; }
    public bool HasVersion2Format { get; }

    public Flags7(int flags)
    {
        HasVsUnisystem = (flags & VsUnisystemFlag) == VsUnisystemFlag;
        HasPlayChoice10 = (flags & PlayChoice10Flag) == PlayChoice10Flag;
        HasVersion2Format = ((flags & NesFormatVersion) >> 2) == 2;
    }
}