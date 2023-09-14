namespace NesCs.Logic.Cpu.Operations;

internal class CompareFactory : ICompareFactory
{
    public IOperation X { get; }
    public IOperation Y { get; }
    public IOperation Accumulator { get; }

    public CompareFactory()
    {
        X = new Compare(c => c.ReadByteFromRegisterX());
        Y = new Compare(c => c.ReadByteFromRegisterY());
        Accumulator = new Compare(c => c.ReadByteFromAccumulator());
    }
}