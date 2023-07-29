namespace NesCs.Logic.Cpu.Instructions;

public class OraInIndirectXModeOpcode01 : IndirectXMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(cpu.ReadByteFromAccumulator() | value);
}