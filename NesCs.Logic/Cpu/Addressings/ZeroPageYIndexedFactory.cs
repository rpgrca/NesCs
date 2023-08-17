namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageYIndexedFactory : IZeroPageYIndexedFactory
{
    public IAddressing Memory =>
        new ZeroPageYIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing X =>
        new ZeroPageYIndexed((c, _) => c.ReadByteFromRegisterX());

    public IAddressing DoubleMemoryRead => new ZeroPageYIndexedDouble();
}