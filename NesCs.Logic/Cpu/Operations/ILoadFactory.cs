namespace NesCs.Logic.Cpu.Operations;

public interface ILoadFactory
{
    IOperation X { get; }
    IOperation Y { get; }
    IOperation Accumulator { get; }
}