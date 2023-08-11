namespace NesCs.Logic.Cpu.Addressings;

public class AbsoluteFactory : IAbsoluteFactory
{
    public IAddressing Y => new Absolute((c, _) => c.ReadByteFromRegisterY());

    public IAddressing X => new Absolute((c, _) => c.ReadByteFromRegisterX());

    public IAddressing Memory => new Absolute((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Accumulator => new Absolute((c, _) => c.ReadByteFromAccumulator());
}