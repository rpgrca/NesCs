namespace NesCs.Logic.Cpu.Operations;

internal interface ILoadFactory
{
    IOperation X { get; }
    IOperation Y { get; }
    IOperation Accumulator { get; }
}