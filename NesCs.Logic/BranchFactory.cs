namespace NesCs.Logic.Cpu.Operations;

public class BranchFactory : IBranchFactory
{
    public IOperation WhenNegative => new Branch(c => c.IsNegativeFlagSet());

    public IOperation WhenPositive => new Branch(c => !c.IsNegativeFlagSet());

    public IOperation WhenNotOverflow => new Branch(c => !c.IsOverflowFlagSet());

    public IOperation WhenOverflow => new Branch(c => c.IsOverflowFlagSet());

    public IOperation WhenNotCarry => new Branch(c => !c.IsCarryFlagSet());

    public IOperation WhenCarry => new Branch(c => c.IsCarryFlagSet());

    public IOperation WhenNotZero => new Branch(c => !c.IsZeroFlagSet());

    public IOperation WhenZero => new Branch(c => c.IsZeroFlagSet());
}