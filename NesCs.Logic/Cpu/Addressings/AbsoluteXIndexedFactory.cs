namespace NesCs.Logic.Cpu.Addressings;

public class AbsoluteXIndexedFactory : IAbsoluteXIndexedFactory
{
    public IAddressing WithExtraRead => new AbsoluteXIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Common => new AbsoluteXIndexed((c, a) => { });
}