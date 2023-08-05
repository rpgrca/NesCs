using NesCs.Logic.Cpu.Instructions.Modes;

namespace NesCs.Logic.Cpu.Instructions;

public class XorInIndirectXModeOpcode41 : IndirectXMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(cpu.ReadByteFromAccumulator() ^ value);
}