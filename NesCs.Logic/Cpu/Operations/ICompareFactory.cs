namespace NesCs.Logic.Cpu.Operations;

public interface ICompareFactory
{
    IOperation X { get; }
    IOperation Y { get; }
    IOperation Accumulator { get; }
}