namespace NesCs.Logic.Cpu.Operations;

public class BitTest : IOperation
{
    (int, byte) IOperation.Execute(Cpu6502 cpu, byte value, int address)
    {
        var result = (byte)(value & cpu.ReadByteFromAccumulator());
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(value);
        cpu.SetOverflowFlagBasedOn(value);

        return (address, result);
    }
}