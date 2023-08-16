namespace NesCs.Logic.Cpu.Addressings;

public interface IIndirectYIndexedFactory
{
    IAddressing Accumulator { get; }
    IAddressing Memory { get; }
    IAddressing DoubleMemoryRead { get; }
}