namespace NesCs.Logic.Cpu.Addressings;

public class IndirectYIndexedFactory : IIndirectYIndexedFactory
{
    public IAddressing Accumulator => new IndirectYIndexed((c, a) => c.ReadByteFromAccumulator());

    public IAddressing Memory => new IndirectYIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing DoubleMemoryRead => new IndirectYIndexedDouble();
}