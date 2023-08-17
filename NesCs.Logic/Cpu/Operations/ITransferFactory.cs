namespace NesCs.Logic.Cpu.Operations;

public interface ITransferFactory
{
    IOperation Accumulator { get; }
    IOperation X { get; }
    IOperation Y { get; }
}