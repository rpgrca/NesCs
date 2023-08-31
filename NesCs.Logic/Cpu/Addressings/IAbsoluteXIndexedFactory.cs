namespace NesCs.Logic.Cpu.Addressings;

internal interface IAbsoluteXIndexedFactory
{
    IAddressing DoubleMemoryRead { get; }
    IAddressing Common { get; }
    IAddressing Accumulator { get; }
}