using NesCs.Logic.Cpu;

namespace NesCs.Logic.Cpu.Addressings;

public interface IAddressing
{
    byte ObtainValue(Cpu6502 cpu) => 0;
}

public class Accumulator : IAddressing
{

}

public class Absolute : IAddressing
{
    byte IAddressing.ObtainValue(Cpu6502 cpu)
    {
        cpu.ReadyForNextInstruction();
        var low = cpu.ReadByteFromProgram();

        cpu.ReadyForNextInstruction();
        var high = cpu.ReadByteFromProgram();

        return cpu.ReadByteFromMemory(high << 8 | low);
    }
}

public class AbsoluteXIndexed : IAddressing
{
    
}

public class AbsoluteYIndexed : IAddressing
{

}

public class Immediate : IAddressing
{

}

public class Implied : IAddressing
{

}

public class Indirect : IAddressing
{

}

public class IndirectXIndexed : IAddressing
{

}

public class IndirectYIndexed : IAddressing
{

}

public class Relative : IAddressing
{

}

public class ZeroPage : IAddressing
{

}

public class ZeroPageXIndexed : IAddressing
{

}

public class ZeroPageYIndexed : IAddressing
{

}

public class Addressings
{
    public IAddressing Accumulator { get; }
    public IAddressing Absolute { get; }
    public IAddressing AbsoluteXIndexed { get; }
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
        AbsoluteXIndexed = new AbsoluteXIndexed();
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