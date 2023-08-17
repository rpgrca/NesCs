namespace NesCs.Logic.Cpu.Addressings;

public class ZeroPageYIndexedFactory : IZeroPageYIndexedFactory
{
    public IAddressing Memory { get; } =
        new ZeroPageYIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing X { get; } =
        new ZeroPageYIndexed((c, _) => c.ReadByteFromRegisterX());

    public IAddressing DoubleMemoryRead { get; } = new ZeroPageYIndexedDouble();
}