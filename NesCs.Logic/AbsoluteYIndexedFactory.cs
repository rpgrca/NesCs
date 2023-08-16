namespace NesCs.Logic.Cpu.Addressings;

public class AbsoluteYIndexedFactory : IAbsoluteYIndexedFactory
{
    public IAddressing DoubleMemoryRead => new AbsoluteYIndexedDouble();

    public IAddressing Common => new AbsoluteYIndexed();
}