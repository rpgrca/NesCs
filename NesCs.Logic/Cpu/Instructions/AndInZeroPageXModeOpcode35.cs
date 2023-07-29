namespace NesCs.Logic.Cpu.Instructions;

public class AndInZeroPageXModeOpcode35 : ZeroIndexedMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(cpu.ReadByteFromAccumulator() & value);

    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterX();
}