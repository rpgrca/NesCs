namespace NesCs.Logic.Cpu.Operations;

internal interface IShiftLeftFactory
{
    IOperation Memory { get; }
    IOperation Accumulator { get; }
}