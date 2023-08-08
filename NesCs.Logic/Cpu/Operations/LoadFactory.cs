namespace NesCs.Logic.Cpu.Operations;

public class LoadFactory : ILoadFactory
{
    public IOperation X => new Load((c, v) => c.SetValueToRegisterX(v));

    public IOperation Y => new Load((c, v) => c.SetValueToRegisterY(v));

    public IOperation Accumulator => new Load((c, v) => c.SetValueToAccumulator(v));
}