namespace NesCs.Logic.Cpu.Addressings;

internal interface IAbsoluteFactory
{
    IAddressing Y { get; }
    IAddressing X { get; }
    IAddressing Memory { get; }
    IAddressing Accumulator { get; }
    IAddressing Direct { get; }
}