namespace NesCs.Logic.Cpu.Operations;

public interface IRotateFactory
{
    IOperation OnAccumulator { get; }
    IOperation OnMemory { get; }
}