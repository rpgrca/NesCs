using NesCs.Logic.Cpu;
using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic.Tracing;

public class DummyTracer : ITracer
{
    public void Display(IInstruction instruction, byte[] operands, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles)
    {
    }

    public void Read(int address, byte value)
    {
    }

    public void Write(int address, byte value)
    {
    }
}