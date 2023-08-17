namespace NesCs.Logic.Cpu.Addressings;

public interface IAbsoluteXIndexedFactory
{
    IAddressing DoubleMemoryRead { get; }
    IAddressing Common { get; }
    IAddressing Accumulator { get; }
}