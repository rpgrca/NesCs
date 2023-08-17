namespace NesCs.Logic.Cpu.Operations;

public class StoreFactory : IStoreFactory
{
    public IOperation Memory { get; } = new Store((c, a, v) => c.WriteByteToMemory(a, v));

    public IOperation Stack { get; } = new Store((c, _, v) => c.SetValueToStackPointer(v));
}