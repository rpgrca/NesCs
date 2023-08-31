using NesCs.Logic;
using NesCs.Logic.Cpu;
using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Common.Tests;

public class DisplayTracerSpy : ITracer
{
    public int Opcode { get; private set; } = 0;
    public string Name { get; private set; } = string.Empty;

    public void Display(IInstruction instruction, byte[] operands, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles)
    {
        Opcode = instruction.Opcode;
        Name = instruction.Name;
    }

    public void Read(int address, byte value)
    {
    }

    public void Write(int address, byte value)
    {
    }
}