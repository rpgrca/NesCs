namespace NesCs.Logic.Cpu.Operations;

public class ShiftLeftFactory : IShiftLeftFactory
{
    public IOperation Memory =>
        new ShiftLeft((c, a, v) => c.WriteByteToMemory(a, v), (c, a, v) => c.WriteByteToMemory(a, v));

    public IOperation Accumulator =>
        new ShiftLeft((c, a, v) => { }, (c, _, v) => c.SetValueToAccumulator(v));
}