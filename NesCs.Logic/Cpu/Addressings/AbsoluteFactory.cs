namespace NesCs.Logic.Cpu.Addressings;

internal class AbsoluteFactory : IAbsoluteFactory
{
    public IAddressing Y { get; } = new Absolute((c, _) => c.ReadByteFromRegisterY());

    public IAddressing X { get; } = new Absolute((c, _) => c.ReadByteFromRegisterX());

    public IAddressing Memory { get; } = new Absolute((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Direct { get; } = new Absolute((c, a) => 0);

    public IAddressing Accumulator { get; } = new Absolute((c, _) => c.ReadByteFromAccumulator());
}