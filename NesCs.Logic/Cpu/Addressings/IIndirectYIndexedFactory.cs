namespace NesCs.Logic.Cpu.Addressings;

public interface IIndirectYIndexedFactory
{
    IAddressing Memory { get; }
    IAddressing DoubleMemoryRead { get; }
    IAddressing Accumulator { get; }
}