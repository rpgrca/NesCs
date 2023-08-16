namespace NesCs.Logic.Cpu.Addressings;

public class AbsoluteXIndexedFactory : IAbsoluteXIndexedFactory
{
    public IAddressing DoubleMemoryRead => new AbsoluteXIndexed((c, a, _) => c.ReadByteFromMemory(a), (c, a) => c.ReadByteFromMemory(a));

    public IAddressing Common => new AbsoluteXIndexed((c, a, v) => c.ReadByteFromMemory(a), (c, a) => { });

    public IAddressing Common1 => new AbsoluteXIndexed((c, a, v) => v, (c, a) => { });
}