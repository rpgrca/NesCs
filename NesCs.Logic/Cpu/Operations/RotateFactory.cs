namespace NesCs.Logic.Cpu.Operations;

internal class RotateFactory : IRotateFactory
{
    private readonly Func<Action<Cpu6502, int, byte>, Action<Cpu6502, int, byte>, IOperation> _creator;

    public IOperation OnAccumulator { get; }
    public IOperation OnMemory { get; }

    public RotateFactory(Func<Action<Cpu6502, int, byte>, Action<Cpu6502, int, byte>, IOperation> creator)
    {
        _creator = creator;
        OnAccumulator = _creator((c, a, v) => {}, (c, _, v) => c.SetValueToAccumulator(v));
        OnMemory = _creator((c, a, v) => c.WriteByteToMemory(a, v), (c, a, v) => c.WriteByteToMemory(a, v));
    }
}