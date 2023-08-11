namespace NesCs.Logic.Cpu.Addressings;

public interface IZeroPageYIndexedFactory
{
    IAddressing Memory { get; }
    IAddressing X { get; }
    IAddressing Y { get; }
}