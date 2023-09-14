namespace NesCs.Logic.Cpu.Operations;

internal class AutocrementFactory : IAutocrementFactory
{
    private readonly Func<byte, byte> _modifier;

    public IOperation X { get; }
    public IOperation Y { get; }
    public IOperation Memory { get; }

    public AutocrementFactory(Func<byte, byte> modifier)
    {
        _modifier = modifier;
        X = new Autocrement((c, a, _) => c.ReadByteFromRegisterX(), (c, _, v) => c.SetValueToRegisterX(v), _modifier);
        Y = new Autocrement((c, a, _) => c.ReadByteFromRegisterY(), (c, _, v) => c.SetValueToRegisterY(v), _modifier);
        Memory = new Autocrement((c, a, v) => { c.WriteByteToMemory(a, v); return v; }, (c, a, v) => c.WriteByteToMemory(a, v), _modifier);
    }
}