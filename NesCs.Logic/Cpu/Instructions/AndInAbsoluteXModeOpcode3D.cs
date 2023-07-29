using NesCs.Logic.Cpu.Instructions.Modes;

namespace NesCs.Logic.Cpu.Instructions;

public class AndInAbsoluteXModeOpcode3D : AbsoluteIndexedMode
{
    protected override byte ObtainValueForIndex(Cpu6502 cpu) =>
        cpu.ReadByteFromRegisterX();

    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(cpu.ReadByteFromAccumulator() & value);
}