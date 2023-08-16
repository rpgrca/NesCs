using System.Net.Http.Headers;

namespace NesCs.Logic.Cpu.Addressings;

public class IndirectXIndexedFactory : IIndirectXIndexedFactory
{
    public IAddressing Accumulator => new IndirectXIndexed((c, _) => c.ReadByteFromAccumulator());

    public IAddressing Memory => new IndirectXIndexed((c, a) => c.ReadByteFromMemory(a));

    public IAddressing DoubleMemoryRead => new IndirectXIndexedDouble();

    public IAddressing X => new IndirectXIndexed((c, _) => c.ReadByteFromRegisterX());
}