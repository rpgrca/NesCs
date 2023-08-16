namespace NesCs.Logic.Cpu.Operations;

public class ShiftRightFactory : IShiftRightFactory
{
    public IOperation Memory =>
        new ShiftRight((c, a, v) => c.WriteByteToMemory(a, v), (c, a, v) => c.WriteByteToMemory(a, v));

    public IOperation Accumulator =>
        new ShiftRight((c, a, v) => { }, (c, _, v) => c.SetValueToAccumulator(v));
}