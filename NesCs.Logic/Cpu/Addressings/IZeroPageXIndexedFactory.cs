namespace NesCs.Logic.Cpu.Addressings;

internal interface IZeroPageXIndexedFactory
{
    IAddressing Memory { get; }
    IAddressing Y { get; }
    IAddressing Accumulator { get; }
}