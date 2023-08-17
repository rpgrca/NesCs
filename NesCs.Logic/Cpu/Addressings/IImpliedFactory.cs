namespace NesCs.Logic.Cpu.Addressings;

public interface IImpliedFactory
{
    IAddressing X { get; }
    IAddressing Accumulator { get; }
    IAddressing Memory { get; }
}