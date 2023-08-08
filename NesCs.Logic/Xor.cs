namespace NesCs.Logic.Cpu.Operations;

public class Xor : IOperation
{
    public void Execute(Cpu6502 cpu, byte value, int address)
    {
        var result = (byte)(cpu.ReadByteFromAccumulator() ^ value);
        cpu.SetValueToAccumulator(result);
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}