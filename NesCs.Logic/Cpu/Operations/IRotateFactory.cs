namespace NesCs.Logic.Cpu.Operations;

internal interface IRotateFactory
{
    IOperation OnAccumulator { get; }
    IOperation OnMemory { get; }
}