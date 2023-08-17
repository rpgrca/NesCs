namespace NesCs.Logic.Cpu.Addressings;

public interface IAbsoluteYIndexedFactory
{
    IAddressing DoubleMemoryRead { get; }
    IAddressing Common { get; }
    IAddressing Accumulator { get; }
}