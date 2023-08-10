namespace NesCs.Logic.Cpu.Addressings;

public class Addressings
{
    public IAddressing Accumulator { get; }
    public IAbsoluteFactory Absolute { get; }
    public IAbsoluteXIndexedFactory AbsoluteXIndexed { get; }
    public IAddressing AbsoluteYIndexed { get; }
    public IAddressing Immediate { get; }
    public IAddressing Implied { get; }
    public IAddressing Indirect { get; }
    public IAddressing IndirectXIndexed { get; }
    public IAddressing IndirectYIndexed { get; }
    public IAddressing Relative { get; }
    public IZeroPageFactory ZeroPage { get; }
    public IZeroPageXIndexedFactory ZeroPageXIndexed { get; }
    public IZeroPageYIndexedFactory ZeroPageYIndexed { get; }

    public Addressings()
    {
        Accumulator = new Accumulator();
        Absolute = new AbsoluteFactory();
        AbsoluteXIndexed = new AbsoluteXIndexedFactory();
        AbsoluteYIndexed = new AbsoluteYIndexed();
        Immediate = new Immediate();
        Implied = new Implied();
        Indirect = new Indirect();
        IndirectXIndexed = new IndirectXIndexed();
        IndirectYIndexed = new IndirectYIndexed();
        Relative = new Relative();
        ZeroPage = new ZeroPageFactory();
        ZeroPageXIndexed = new ZeroPageXIndexedFactory();
        ZeroPageYIndexed = new ZeroPageYIndexedFactory();
    }
}