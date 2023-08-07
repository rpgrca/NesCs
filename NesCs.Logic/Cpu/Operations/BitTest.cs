namespace NesCs.Logic.Cpu.Operations;

public class BitTest : IOperation
{
    void IOperation.Execute(Cpu6502 cpu, byte value, int _)
    {
        var result = (byte)(value & cpu.ReadByteFromAccumulator());
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetOverflowFlagBasedOn(value);
    }
}