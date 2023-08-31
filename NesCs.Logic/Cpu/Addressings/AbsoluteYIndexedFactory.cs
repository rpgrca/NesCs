namespace NesCs.Logic.Cpu.Addressings;

internal class AbsoluteYIndexedFactory : IAbsoluteYIndexedFactory
{
    public IAddressing DoubleMemoryRead { get; } = new AbsoluteYIndexedDouble();

    public IAddressing Common { get; } = new AbsoluteYIndexed();

    public IAddressing Accumulator => new AbsoluteYIndexedAccumulator();
}