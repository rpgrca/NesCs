namespace NesCs.Logic.Cpu.Operations;

public class DecrementFactory : IDecrementFactory
{
    public IOperation X => new Decrement((c, a, _) => c.ReadByteFromRegisterX(), (c, _, v) => c.SetValueToRegisterX(v));

    public IOperation Y => new Decrement((c, a, _) => c.ReadByteFromRegisterY(), (c, _, v) => c.SetValueToRegisterY(v));

    public IOperation Memory =>
        new Decrement((c, a, v) => { c.WriteByteToMemory(a, v); return v; }, (c, a, v) => c.WriteByteToMemory(a, v));

    public IOperation Accumulator => new Decrement((c, a, _) => c.ReadByteFromAccumulator(), (c, _, v) => c.SetValueToAccumulator(v));
}