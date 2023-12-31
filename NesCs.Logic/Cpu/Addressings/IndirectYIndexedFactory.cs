namespace NesCs.Logic.Cpu.Addressings;

internal class IndirectYIndexedFactory : IIndirectYIndexedFactory
{
    public IAddressing Memory { get; } = new IndirectYIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing DoubleMemoryRead { get; } = new IndirectYIndexedDouble();

    public IAddressing Accumulator { get; } = new IndirectYIndexedAccumulator();
}