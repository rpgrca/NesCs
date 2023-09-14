namespace NesCs.Logic.Cpu.Operations;

internal class ShiftRightFactory : IShiftRightFactory
{
    public IOperation Memory { get; }
    public IOperation Accumulator { get; }

    public ShiftRightFactory()
    {
        Memory = new ShiftRight((c, a, v) => c.WriteByteToMemory(a, v), (c, a, v) => c.WriteByteToMemory(a, v));
        Accumulator = new ShiftRight((c, a, v) => { }, (c, _, v) => c.SetValueToAccumulator(v));
    }
}