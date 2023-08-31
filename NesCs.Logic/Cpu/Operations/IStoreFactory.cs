namespace NesCs.Logic.Cpu.Operations;

internal interface IStoreFactory
{
    IOperation Memory { get; }
    IOperation Stack { get; }
}