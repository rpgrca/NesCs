namespace NesCs.Logic.Cpu.Addressings;

public class Addressings
{
    public IAddressing Accumulator { get; }
    public IAddressing Absolute { get; }
    public IAbsoluteXIndexedFactory AbsoluteXIndexed { get; }
    public IAddressing AbsoluteYIndexed { get; }
    public IAddressing Immediate { get; }
    public IAddressing Implied { get; }
    public IAddressing Indirect { get; }
    public IAddressing IndirectXIndexed { get; }
    public IAddressing IndirectYIndexed { get; }
    public IAddressing Relative { get; }
    public IAddressing ZeroPage { get; }
    public IAddressing ZeroPageXIndexed { get; }
    public IAddressing ZeroPageYIndexed { get; }

    public Addressings()
    {
        Accumulator = new Accumulator();
        Absolute = new Absolute();
        AbsoluteXIndexed = new AbsoluteXIndexedFactory();
        AbsoluteYIndexed = new AbsoluteYIndexed();
        Immediate = new Immediate();
        Implied = new Implied();
        Indirect = new Indirect();
        IndirectXIndexed = new IndirectXIndexed();
        IndirectYIndexed = new IndirectYIndexed();
        Relative = new Relative();
        ZeroPage = new ZeroPage();
        ZeroPageXIndexed = new ZeroPageXIndexed();
        ZeroPageYIndexed = new ZeroPageYIndexed();
    }
}