using NesCs.Logic.Cpu.Instructions.Modes;

namespace NesCs.Logic.Cpu.Instructions;

public class AndInIndirectXModeOpcode21 : IndirectXMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(value & cpu.ReadByteFromAccumulator());
}