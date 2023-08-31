namespace NesCs.Logic.Cpu.Addressings;

internal interface IZeroPageYIndexedFactory
{
    IAddressing DoubleMemoryRead { get; }
    IAddressing Memory { get; }
    IAddressing X { get; }
}