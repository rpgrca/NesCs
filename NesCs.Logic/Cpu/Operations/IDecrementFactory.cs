namespace NesCs.Logic.Cpu.Operations;

public interface IDecrementFactory
{
    IOperation X { get; }
    IOperation Y { get; }
    IOperation Memory { get; }
    IOperation Accumulator { get; }
}