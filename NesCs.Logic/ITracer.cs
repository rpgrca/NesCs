using NesCs.Logic.Cpu;
using NesCs.Logic.Cpu.Instructions;

namespace NesCs.Logic;

public interface ITracer
{
    void Write(int address, byte value);
    void Read(int address, byte value);
    void Display(IInstruction instruction, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles);
}
