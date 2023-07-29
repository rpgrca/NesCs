using NesCs.Logic.Cpu.Instructions.Modes;

namespace NesCs.Logic.Cpu.Instructions;

public class OraInAbsoluteXModeOpcode0D : AbsoluteIndexedMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(value | cpu.ReadByteFromAccumulator());

    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterX();
}