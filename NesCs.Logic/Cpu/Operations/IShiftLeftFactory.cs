namespace NesCs.Logic.Cpu.Operations;

public interface IShiftLeftFactory
{
    IOperation Memory { get; }
    IOperation Accumulator { get; }
}