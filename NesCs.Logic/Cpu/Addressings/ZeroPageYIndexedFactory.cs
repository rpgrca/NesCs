namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageYIndexedFactory : IZeroPageYIndexedFactory
{
    public IAddressing Memory =>
        new ZeroPageYIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Y =>
        new ZeroPageYIndexed((c, _) => c.ReadByteFromRegisterY());

    public IAddressing X =>
        new ZeroPageYIndexed((c, _) => c.ReadByteFromRegisterX());
}