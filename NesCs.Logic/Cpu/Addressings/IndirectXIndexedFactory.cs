namespace NesCs.Logic.Cpu.Addressings;

public class IndirectXIndexedFactory : IIndirectXIndexedFactory
{
    public IAddressing Accumulator => new IndirectXIndexed((c, a) => c.ReadByteFromAccumulator());

    public IAddressing Memory => new IndirectXIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing DoubleMemoryRead => new IndirectXIndexedDouble();
}