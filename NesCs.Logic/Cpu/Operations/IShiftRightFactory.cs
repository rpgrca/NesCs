namespace NesCs.Logic.Cpu.Operations;

internal interface IShiftRightFactory
{
    IOperation Memory { get; }
    IOperation Accumulator { get; }
}