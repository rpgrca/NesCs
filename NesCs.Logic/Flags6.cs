namespace NesCs.Logic;

public struct Flags6
{
    private const int MirroringFlag = 0x1;
    private const int BatteryBackedProgramRamFlag = 0x2;
    private const int TrainerFlag = 0x4;
    private const int IgnoreMirroringFlag = 0x8;

    public Mirroring Mirroring { get; }
    public bool HasBatteryBackedProgramRam { get; }
    public bool HasTrainer { get; }
    public bool IgnoreMirroring { get; }

    public Flags6(int flags)
    {
        Mirroring = (Mirroring)(flags & MirroringFlag);
        HasBatteryBackedProgramRam = (flags & BatteryBackedProgramRamFlag) == BatteryBackedProgramRamFlag;
        HasTrainer = (flags & TrainerFlag) == TrainerFlag;
        IgnoreMirroring = (flags & IgnoreMirroringFlag) == IgnoreMirroringFlag;
    }
}