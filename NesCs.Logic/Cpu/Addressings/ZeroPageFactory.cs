namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageFactory : IZeroPageFactory
{
    public IAddressing Memory { get; } = new ZeroPage((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Y { get; } = new ZeroPage((c, _) => c.ReadByteFromRegisterY());

    public IAddressing X { get; } = new ZeroPage((c, _) => c.ReadByteFromRegisterX());

    public IAddressing Accumulator { get; } = new ZeroPage((c, _) => c.ReadByteFromAccumulator());
}