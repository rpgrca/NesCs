namespace NesCs.Logic.Cpu.Operations;

public interface IAutocrementFactory
{
    IOperation X { get; }
    IOperation Y { get; }
    IOperation Memory { get; }
    IOperation Accumulator { get; }
}