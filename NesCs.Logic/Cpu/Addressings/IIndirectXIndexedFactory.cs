namespace NesCs.Logic.Cpu.Addressings;

public interface IIndirectXIndexedFactory
{
    IAddressing Accumulator { get; }
    IAddressing Memory { get; }
}