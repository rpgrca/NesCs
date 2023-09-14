namespace NesCs.Logic.Cpu.Operations;

internal class LoadFactory : ILoadFactory
{
    public IOperation X { get; }
    public IOperation Y { get; }
    public IOperation Accumulator { get; }

    public LoadFactory()
    {
        X = new Load((c, v) => c.SetValueToRegisterX(v));
        Y = new Load((c, v) => c.SetValueToRegisterY(v));
        Accumulator = new Load((c, v) => c.SetValueToAccumulator(v));
    }
}