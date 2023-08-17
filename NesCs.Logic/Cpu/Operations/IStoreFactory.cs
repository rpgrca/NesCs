namespace NesCs.Logic.Cpu.Operations;

public interface IStoreFactory
{
    IOperation Memory { get; }
    IOperation Stack { get; }
}