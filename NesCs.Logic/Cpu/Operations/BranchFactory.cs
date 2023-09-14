namespace NesCs.Logic.Cpu.Operations;

internal class BranchFactory : IBranchFactory
{
    public IOperation WhenNegative { get; }
    public IOperation WhenPositive { get; }
    public IOperation WhenNotOverflow { get; }
    public IOperation WhenOverflow { get; }
    public IOperation WhenNotCarry { get; }
    public IOperation WhenCarry { get; }
    public IOperation WhenNotZero { get; }
    public IOperation WhenZero { get; }

    public BranchFactory()
    {
        WhenNegative = new Branch(c => c.IsNegativeFlagSet());
        WhenPositive = new Branch(c => !c.IsNegativeFlagSet());
        WhenNotOverflow = new Branch(c => !c.IsOverflowFlagSet());
        WhenOverflow = new Branch(c => c.IsOverflowFlagSet());
        WhenNotCarry = new Branch(c => !c.IsCarryFlagSet());
        WhenCarry = new Branch(c => c.IsCarryFlagSet());
        WhenNotZero = new Branch(c => !c.IsZeroFlagSet());
        WhenZero = new Branch(c => c.IsZeroFlagSet());
    }
}