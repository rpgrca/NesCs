namespace NesCs.Logic.Cpu.Addressings;

internal class AbsoluteXIndexedFactory : IAbsoluteXIndexedFactory
{
    public IAddressing DoubleMemoryRead { get; } = new AbsoluteXIndexed((c, a, _) => c.ReadByteFromMemory(a), (c, a) => c.ReadByteFromMemory(a));

    public IAddressing Common { get; } = new AbsoluteXIndexed((c, a, v) => c.ReadByteFromMemory(a), (c, a) => { });

    public IAddressing Accumulator { get; } = new AbsoluteXIndexedAccumulator();
}