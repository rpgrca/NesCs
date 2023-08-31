namespace NesCs.Logic.Cpu.Operations;

internal interface ITransferFactory
{
    IOperation Accumulator { get; }
    IOperation X { get; }
    IOperation Y { get; }
}