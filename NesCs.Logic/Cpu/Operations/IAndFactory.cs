using NesCs.Logic.Cpu.Operations;

public interface IAndFactory
{
    IOperation Accumulator { get; }
    IOperation Memory { get; }
}