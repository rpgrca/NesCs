namespace NesCs.Logic.Cpu.Operations;

internal interface IAutocrementFactory
{
    IOperation X { get; }
    IOperation Y { get; }
    IOperation Memory { get; }
}