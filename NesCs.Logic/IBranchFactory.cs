namespace NesCs.Logic.Cpu.Operations;

public interface IBranchFactory
{
    IOperation WhenNegative { get; }
    IOperation WhenPositive { get; }
    IOperation WhenNotOverflow { get; }
}