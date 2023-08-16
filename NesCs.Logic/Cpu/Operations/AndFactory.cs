namespace NesCs.Logic.Cpu.Operations;

public class AndFactory : IAndFactory
{
    public IOperation Memory =>
        new And((c, a, v) => c.WriteByteToMemory(a, v), (c, v) => { });

    public IOperation Accumulator =>
        new And((c, _, v) => c.SetValueToAccumulator(v), (c, v) => {
            c.SetZeroFlagBasedOn(v);
            c.SetNegativeFlagBasedOn(v);
        });
}