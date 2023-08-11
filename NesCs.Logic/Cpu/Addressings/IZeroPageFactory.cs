namespace NesCs.Logic.Cpu.Addressings;

public interface IZeroPageFactory
{
    IAddressing Memory { get; }
    IAddressing Y { get; }
    IAddressing X { get; }
    IAddressing Accumulator { get; }
}