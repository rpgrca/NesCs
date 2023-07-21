namespace NesCs.Logic;

public struct Flags6
{
    private const int MirroringFlag = 0x1;
    private const int BatteryBackedProgramRamFlag = 0x2;

    public Mirroring Mirroring { get; }
    public bool HasBatteryBackedProgramRam { get; }

    public Flags6(int flags)
    {
        Mirroring = (Mirroring)(flags & MirroringFlag);
        HasBatteryBackedProgramRam = (flags & BatteryBackedProgramRamFlag) == BatteryBackedProgramRamFlag;
    }
}
