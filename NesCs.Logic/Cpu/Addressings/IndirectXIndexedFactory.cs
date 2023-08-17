namespace NesCs.Logic.Cpu.Addressings;

public class IndirectXIndexedFactory : IIndirectXIndexedFactory
{
    public IAddressing Accumulator { get; } = new IndirectXIndexed((c, _) => c.ReadByteFromAccumulator());

    public IAddressing Memory { get; } = new IndirectXIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing DoubleMemoryRead { get; } = new IndirectXIndexedDouble();

    public IAddressing X { get; } = new IndirectXIndexed((c, _) => c.ReadByteFromRegisterX());
}