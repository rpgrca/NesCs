namespace NesCs.Logic.Cpu.Operations;

internal class AndFactory : IAndFactory
{
    public IOperation Memory { get; }
    public IOperation Accumulator { get; }

    public AndFactory()
    {
        Memory = new And((c, a, v) => c.WriteByteToMemory(a, v), (c, v) => { });
        Accumulator = new And((c, _, v) => c.SetValueToAccumulator(v), (c, v) => {
            c.SetZeroFlagBasedOn(v);
            c.SetNegativeFlagBasedOn(v);
        });
    }
}