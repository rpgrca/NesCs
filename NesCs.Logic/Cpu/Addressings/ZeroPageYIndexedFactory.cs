namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageYIndexedFactory : IZeroPageYIndexedFactory
{
    public IAddressing Memory =>
        new ZeroPageYIndexed((c, a) => c.ReadByteFromMemory(a));

    /*public IAddressing Y1 =>
        new ZeroPageYIndexed((c, _) => c.ReadByteFromRegisterY());*/

    public IAddressing X =>
        new ZeroPageYIndexed((c, _) => c.ReadByteFromRegisterX());

    public IAddressing Common =>
        new ZeroPageYIndexed((_, v) => 0);

    public IAddressing DoubleMemoryRead => new ZeroPageYIndexedDouble();
}