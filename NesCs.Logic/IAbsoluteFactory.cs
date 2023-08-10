namespace NesCs.Logic.Cpu.Addressings;

public interface IAbsoluteFactory
{
    IAddressing Y { get; }
    IAddressing X { get; }
    IAddressing Memory { get; }
}