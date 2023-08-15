using NesCs.Logic;
using NesCs.Logic.Cpu;
using NesCs.Logic.Cpu.Instructions;

public class TracerSpy : ITracer
{
    private readonly List<(int, byte, string)> _trace;

    public TracerSpy(List<(int, byte, string)> trace) => _trace = trace;

    public void Write(int address, byte value) => _trace.Add((address, value, "write"));

    public void Read(int address, byte value) => _trace.Add((address, value, "read"));

    public void Display(IInstruction instruction, int pc, byte a, byte x, byte y, ProcessorStatus p, byte s, int cycles)
    {
    }
}