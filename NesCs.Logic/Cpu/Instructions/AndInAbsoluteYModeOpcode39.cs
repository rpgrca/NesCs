namespace NesCs.Logic.Cpu.Instructions;

public class AndInAbsoluteYModeOpcode39 : AbsoluteIndexedMode
{
    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterY();

    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(cpu.ReadByteFromAccumulator() & value);
}