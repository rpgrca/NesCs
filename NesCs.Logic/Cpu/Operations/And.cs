namespace NesCs.Logic.Cpu.Operations;

public class And : IOperation
{
    void IOperation.Execute(Cpu6502 cpu, byte value, int _)
    {
        var result = (byte)(cpu.ReadByteFromAccumulator() & value);
        cpu.SetValueToAccumulator(result);
        cpu.SetZeroFlagBasedOn(result);
        cpu.SetNegativeFlagBasedOn(result);
    }
}