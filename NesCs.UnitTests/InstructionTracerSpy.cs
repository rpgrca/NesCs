using NesCs.Logic.Cpu;
using NesCs.Logic.Cpu.Instructions;
using NesCs.Logic.Tracing;

namespace NesCs.UnitTests.Cpu;

public class InstructionTracerSpy : ITracer
{
    public IInstruction Instruction { get; private set; }

    public void Display(IInstruction instruction, byte[] operands, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles) =>
        Instruction = instruction;

    public void Read(int address, byte value)
    {
    }

    public void Write(int address, byte value)
    {
    }
}