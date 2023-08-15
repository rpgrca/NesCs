using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Cpu;

public class DummyTracer : ITracer
{
    public void Display(IInstruction instruction, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles)
    {
    }

    public void Read(int address, byte value)
    {
    }

    public void Write(int address, byte value)
    {
    }
}
