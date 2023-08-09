namespace NesCs.Logic.Cpu.Operations;

public class RotateRightFactory : IRotateRightFactory
{
    public IOperation OnAccumulator =>
        new RotateRight((c, a, v) => {}, (c, _, v) => c.SetValueToAccumulator(v));

    public IOperation OnMemory =>
        new RotateRight((c, a, v) => c.WriteByteToMemory(a, v), (c, a, v) => c.WriteByteToMemory(a, v));
}