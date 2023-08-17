namespace NesCs.Logic.Cpu.Addressings;

public interface IZeroPageXIndexedFactory
{
    IAddressing Memory { get; }
    IAddressing Y { get; }
    IAddressing Accumulator { get; }
}