namespace NesCs.Logic.Cpu.Operations;

public class AutocrementFactory : IAutocrementFactory
{
    private readonly Func<byte, byte> _modifier;

    public AutocrementFactory(Func<byte, byte> modifier) => _modifier = modifier;

    public IOperation X => new Autocrement((c, a, _) =>
        c.ReadByteFromRegisterX(), (c, _, v) => c.SetValueToRegisterX(v), _modifier);

    public IOperation Y => new Autocrement((c, a, _) =>
        c.ReadByteFromRegisterY(), (c, _, v) => c.SetValueToRegisterY(v), _modifier);

    public IOperation Memory => new Autocrement((c, a, v) =>
        { c.WriteByteToMemory(a, v); return v; }, (c, a, v) => c.WriteByteToMemory(a, v), _modifier);
}