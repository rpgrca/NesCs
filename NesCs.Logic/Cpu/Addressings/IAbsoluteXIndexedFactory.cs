namespace NesCs.Logic.Cpu.Addressings;

public interface IAbsoluteXIndexedFactory
{
    IAddressing WithExtraRead { get; }
    IAddressing Common { get; }
    IAddressing Common1 { get; }
}