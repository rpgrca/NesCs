namespace NesCs.Logic.File;

public readonly struct Flags6
{
    private const int MirroringFlag = 0b1;
    private const int BatteryBackedProgramRamFlag = 0b10;
    private const int TrainerFlag = 0b100;
    private const int IgnoreMirroringFlag = 0b1000;
    private const int LowerMapperNybbleFlag = 0b11110000;

    public Mirroring Mirroring { get; }
    public bool HasBatteryBackedProgramRam { get; }
    public bool HasTrainer { get; }
    public bool IgnoreMirroring { get; }
    internal int LowerMapperNybble { get; }

    public Flags6(int flags)
    {
        Mirroring = (Mirroring)(flags & MirroringFlag);
        HasBatteryBackedProgramRam = (flags & BatteryBackedProgramRamFlag) == BatteryBackedProgramRamFlag;
        HasTrainer = (flags & TrainerFlag) == TrainerFlag;
        IgnoreMirroring = (flags & IgnoreMirroringFlag) == IgnoreMirroringFlag;
        LowerMapperNybble = (flags & LowerMapperNybbleFlag) >> 4;
    }

    public override string ToString() =>
        $"""

                Mirroring                 : {Mirroring}
                Battery Backed Program RAM: {HasBatteryBackedProgramRam}
                Has Trainer               : {HasTrainer}
                Ignore Mirroring          : {IgnoreMirroring}
                Lower Mapper Nybble       : {LowerMapperNybble}
        """;
}