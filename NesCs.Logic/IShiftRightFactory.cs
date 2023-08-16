namespace NesCs.Logic.Cpu.Operations;

public interface IShiftRightFactory
{
    IOperation Memory { get; }
    IOperation Accumulator { get; }
}