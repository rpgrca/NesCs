namespace NesCs.Logic.Cpu.Instructions;

public class AndInAbsoluteModeOpcode2D : AbsoluteMode
{
    protected override byte ExecuteOperation(Cpu6502 cpu, byte value) =>
        (byte)(cpu.ReadByteFromAccumulator() & value);
}