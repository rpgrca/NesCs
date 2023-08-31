namespace NesCs.Logic.Cpu.Operations;

internal interface ICompareFactory
{
    IOperation X { get; }
    IOperation Y { get; }
    IOperation Accumulator { get; }
}