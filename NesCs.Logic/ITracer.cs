using NesCs.Logic.Cpu;

namespace NesCs.Logic;

public interface ITracer
{
    void Write(int address, byte value);
    void Read(int address, byte value);
    void Display(byte opcode, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles);
}
