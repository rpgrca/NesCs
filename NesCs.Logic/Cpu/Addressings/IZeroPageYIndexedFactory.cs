namespace NesCs.Logic.Cpu.Addressings;

public interface IZeroPageYIndexedFactory
{
    IAddressing DoubleMemoryRead { get; }
    IAddressing Memory { get; }
    IAddressing X { get; }
    //IAddressing Y1 { get; }
    IAddressing Common { get; }
}