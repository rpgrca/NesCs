namespace NesCs.Logic.Cpu.Addressings;

internal class ZeroPageXIndexedFactory : IZeroPageXIndexedFactory
{
    public IAddressing Memory { get; } =
        new ZeroPageXIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing Y { get; } =
        new ZeroPageXIndexed((c, _) => c.ReadByteFromRegisterY());

    public IAddressing Accumulator { get; } =
        new ZeroPageXIndexed((c, _) => c.ReadByteFromAccumulator());
}