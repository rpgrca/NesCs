namespace NesCs.Logic.Cpu.Operations;

public class And : IOperation
{
    public void Execute(Cpu6502 cpu, byte value)
    {
        var result = (byte)(cpu.ReadByteFromAccumulator() & value);
        cpu.SetValueToAccumulator(result);
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}