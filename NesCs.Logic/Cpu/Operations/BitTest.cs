namespace NesCs.Logic.Cpu.Operations;

public class BitTest : IOperation
{
    public void Execute(Cpu6502 cpu, byte value)
    {
        var result = (byte)(value & cpu.ReadByteFromAccumulator());
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetOverflowFlagBasedOn(value);
    }
}