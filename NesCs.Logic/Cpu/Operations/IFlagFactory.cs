namespace NesCs.Logic.Cpu.Operations;

public interface IFlagFactory
{
    IOperation D { get; }
    IOperation V { get; }
    IOperation I { get; }
    IOperation C { get; }
}