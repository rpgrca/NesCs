namespace NesCs.Logic.Cpu.Operations;

public class StoreFactory : IStoreFactory
{
    public IOperation X { get; } = new Store((c, _, v) => c.SetValueToRegisterX(v));

    public IOperation Y { get; } = new Store((c, _, v) => c.SetValueToRegisterY(v));

    public IOperation Accumulator { get; } = new Store((c, _, v) => c.SetValueToAccumulator(v));

    public IOperation Memory { get; } = new Store((c, a, v) => c.WriteByteToMemory(a, v));

    public IOperation Stack { get; } = new Store((c, _, v) => c.SetValueToStackPointer(v));
}