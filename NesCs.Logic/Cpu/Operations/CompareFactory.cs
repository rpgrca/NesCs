namespace NesCs.Logic.Cpu.Operations;

public class CompareFactory : ICompareFactory
{
    public IOperation X => new Compare(c => c.ReadByteFromRegisterX());

    public IOperation Y => new Compare(c => c.ReadByteFromRegisterY());

    public IOperation Accumulator => new Compare(c => c.ReadByteFromAccumulator());
}