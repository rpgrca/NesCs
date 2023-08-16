namespace NesCs.Logic.Cpu.Operations;

public interface IAndFactory
{
    IOperation Memory { get; }
    IOperation Accumulator { get; }
}