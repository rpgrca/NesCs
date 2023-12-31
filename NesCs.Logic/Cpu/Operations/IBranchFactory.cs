namespace NesCs.Logic.Cpu.Operations;

internal interface IBranchFactory
{
    IOperation WhenNegative { get; }
    IOperation WhenPositive { get; }
    IOperation WhenNotOverflow { get; }
    IOperation WhenOverflow { get; }
    IOperation WhenNotCarry { get; }
    IOperation WhenCarry { get; }
    IOperation WhenNotZero { get; }
    IOperation WhenZero { get; }
}