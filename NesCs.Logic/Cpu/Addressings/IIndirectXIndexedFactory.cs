namespace NesCs.Logic.Cpu.Addressings;

internal interface IIndirectXIndexedFactory
{
    IAddressing Accumulator { get; }
    IAddressing Memory { get; }
    IAddressing DoubleMemoryRead { get; }
    IAddressing X { get; }
}