namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageFactory : IZeroPageFactory
{
    public IAddressing Memory => new ZeroPage((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Y => new ZeroPage((c, _) => c.ReadByteFromRegisterY());

    public IAddressing X => new ZeroPage((c, _) => c.ReadByteFromRegisterX());

    public IAddressing Accumulator => new ZeroPage((c, _) => c.ReadByteFromAccumulator());
}