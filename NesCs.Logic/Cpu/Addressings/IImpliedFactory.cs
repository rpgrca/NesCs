namespace NesCs.Logic.Cpu.Addressings;

internal interface IImpliedFactory
{
    IAddressing X { get; }
    IAddressing Y { get; }
    IAddressing Accumulator { get; }
    IAddressing Memory { get; }
    IAddressing Stack { get; }
}