namespace NesCs.Logic.Cpu.Operations;

internal interface IAndFactory
{
    IOperation Memory { get; }
    IOperation Accumulator { get; }
}