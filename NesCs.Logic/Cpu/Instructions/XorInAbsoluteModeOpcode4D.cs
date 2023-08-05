using NesCs.Logic.Cpu.Instructions.Modes;

namespace NesCs.Logic.Cpu.Instructions;

public class XorInAbsoluteModeOpcode4D : AbsoluteMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(cpu.ReadByteFromAccumulator() ^ value);
}