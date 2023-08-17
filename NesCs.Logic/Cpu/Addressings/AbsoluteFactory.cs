namespace NesCs.Logic.Cpu.Addressings;

public class AbsoluteFactory : IAbsoluteFactory
{
    public IAddressing Y { get; } = new Absolute((c, _) => c.ReadByteFromRegisterY());

    public IAddressing X { get; } = new Absolute((c, _) => c.ReadByteFromRegisterX());

    public IAddressing Memory { get; } = new Absolute((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Accumulator { get; } = new Absolute((c, _) => c.ReadByteFromAccumulator());
}