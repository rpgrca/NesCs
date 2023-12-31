namespace NesCs.Logic.Cpu.Addressings;

internal class ImpliedFactory : IImpliedFactory
{
    public IAddressing X { get; } = new Implied((c, _, _) => c.ReadByteFromRegisterX());

    public IAddressing Accumulator => new Implied((c, _, _) => c.ReadByteFromAccumulator());

    public IAddressing Memory => new Implied((c, a, v) => v);

    public IAddressing Y { get; } = new Implied((c, _, _) => c.ReadByteFromRegisterY());

    public IAddressing Stack { get; } = new Implied((c, _, _) => c.ReadByteFromStackPointer());
}