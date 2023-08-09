namespace NesCs.Logic.Cpu.Operations;

public class RotateFactory : IRotateFactory
{
    private readonly Func<Action<Cpu6502, int, byte>, Action<Cpu6502, int, byte>, IOperation> _creator;

    public RotateFactory(Func<Action<Cpu6502, int, byte>, Action<Cpu6502, int, byte>, IOperation> creator) =>
        _creator = creator;

    public IOperation OnAccumulator =>
        _creator((c, a, v) => {}, (c, _, v) => c.SetValueToAccumulator(v));

    public IOperation OnMemory =>
        _creator((c, a, v) => c.WriteByteToMemory(a, v), (c, a, v) => c.WriteByteToMemory(a, v));
}