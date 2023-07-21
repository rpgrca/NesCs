namespace NesCs.Logic;

public struct Flags7
{
    private const int VsUnisystemFlag = 0x1;

    public bool HasVsUnisystem { get; }

    public Flags7(int flags)
    {
        HasVsUnisystem = (flags & VsUnisystemFlag) == VsUnisystemFlag;
    }
}