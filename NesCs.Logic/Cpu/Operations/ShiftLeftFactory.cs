namespace NesCs.Logic.Cpu.Operations;

internal class ShiftLeftFactory : IShiftLeftFactory
{
    public IOperation Memory { get; }
    public IOperation Accumulator { get; }

    public ShiftLeftFactory()
    {
        Memory = new ShiftLeft((c, a, v) => c.WriteByteToMemory(a, v), (c, a, v) => c.WriteByteToMemory(a, v));
        Accumulator = new ShiftLeft((c, a, v) => { }, (c, _, v) => c.SetValueToAccumulator(v));
    }
}