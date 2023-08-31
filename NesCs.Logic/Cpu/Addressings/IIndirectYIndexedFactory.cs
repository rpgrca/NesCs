namespace NesCs.Logic.Cpu.Addressings;

internal interface IIndirectYIndexedFactory
{
    IAddressing Memory { get; }
    IAddressing DoubleMemoryRead { get; }
    IAddressing Accumulator { get; }
}