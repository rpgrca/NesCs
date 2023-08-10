namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageXIndexedFactory : IZeroPageXIndexedFactory
{
    public IAddressing Memory =>
        new ZeroPageXIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Y =>
        new ZeroPageXIndexed((c, _) => c.ReadByteFromRegisterY());
}