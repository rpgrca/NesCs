namespace NesCs.Logic.Cpu.Addressings;

internal interface IAbsoluteYIndexedFactory
{
    IAddressing DoubleMemoryRead { get; }
    IAddressing Common { get; }
    IAddressing Accumulator { get; }
}