namespace NesCs.Logic.Cpu.Instructions;

public class OraInIndirectYModeOpcode11 : IndirectYMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(cpu.ReadByteFromAccumulator() | value);
}