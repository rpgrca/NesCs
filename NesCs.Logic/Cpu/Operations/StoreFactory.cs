namespace NesCs.Logic.Cpu.Operations;

public class StoreFactory : IStoreFactory
{
    public IOperation X => new Store((c, _, v) => c.SetValueToRegisterX(v));

    public IOperation Y => new Store((c, _, v) => c.SetValueToRegisterY(v));

    public IOperation Accumulator => new Store((c, _, v) => c.SetValueToAccumulator(v));

    public IOperation Memory => new Store((c, a, v) => c.WriteByteToMemory(a, v));
}