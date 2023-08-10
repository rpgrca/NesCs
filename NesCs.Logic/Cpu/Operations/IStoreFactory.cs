namespace NesCs.Logic.Cpu.Operations;

public interface IStoreFactory
{
    IOperation X { get; }
    IOperation Y { get; }
    IOperation Accumulator { get; }
    IOperation Memory { get; }
}