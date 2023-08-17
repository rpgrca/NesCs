namespace NesCs.Logic.Cpu.Addressings;

public interface IImpliedFactory
{
    IAddressing X { get; }
    IAddressing Y { get; }
    IAddressing Accumulator { get; }
    IAddressing Memory { get; }
    IAddressing Stack { get; }
}