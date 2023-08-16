namespace NesCs.Logic.Cpu.Addressings;

public interface IZeroPageYIndexedFactory
{
    IAddressing DoubleMemoryRead { get; }
    IAddressing Memory { get; }
    IAddressing X { get; }
    IAddressing Y { get; }
}