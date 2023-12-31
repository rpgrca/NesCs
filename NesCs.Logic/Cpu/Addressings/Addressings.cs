namespace NesCs.Logic.Cpu.Addressings;

internal class Addressings
{
    public IAddressing Accumulator { get; }
    public IAbsoluteFactory Absolute { get; }
    public IAbsoluteXIndexedFactory AbsoluteXIndexed { get; }
    public IAbsoluteYIndexedFactory AbsoluteYIndexed { get; }
    public IAddressing Immediate { get; }
    public IImpliedFactory Implied { get; }
    public IAddressing Indirect { get; }
    public IIndirectXIndexedFactory IndirectXIndexed { get; }
    public IIndirectYIndexedFactory IndirectYIndexed { get; }
    public IAddressing Relative { get; }
    public IZeroPageFactory ZeroPage { get; }
    public IZeroPageXIndexedFactory ZeroPageXIndexed { get; }
    public IZeroPageYIndexedFactory ZeroPageYIndexed { get; }

    public Addressings()
    {
        Accumulator = new Accumulator();
        Absolute = new AbsoluteFactory();
        AbsoluteXIndexed = new AbsoluteXIndexedFactory();
        AbsoluteYIndexed = new AbsoluteYIndexedFactory();
        Immediate = new Immediate();
        Implied = new ImpliedFactory();
        Indirect = new Indirect();
        IndirectXIndexed = new IndirectXIndexedFactory();
        IndirectYIndexed = new IndirectYIndexedFactory();
        Relative = new Relative();
        ZeroPage = new ZeroPageFactory();
        ZeroPageXIndexed = new ZeroPageXIndexedFactory();
        ZeroPageYIndexed = new ZeroPageYIndexedFactory();
    }
}