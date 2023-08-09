namespace NesCs.Logic.Cpu.Operations;

public interface IRotateRightFactory
{
    IOperation OnAccumulator { get; }
    IOperation OnMemory { get; }
}