namespace NesCs.Logic.Cpu.Operations;

public class BranchFactory : IBranchFactory
{
    public IOperation WhenNegative => new Branch(c => c.IsNegativeFlagSet());
}