namespace NesCs.Logic;

public struct Flags7
{
    private const int VsUnisystemFlag = 0x1;
    private const int PlayChoice10Flag = 0x2;

    public bool HasVsUnisystem { get; }
    public bool HasPlayChoice10 { get; }

    public Flags7(int flags)
    {
        HasVsUnisystem = (flags & VsUnisystemFlag) == VsUnisystemFlag;
        HasPlayChoice10 = (flags & PlayChoice10Flag) == PlayChoice10Flag;
    }
}