namespace NesCs.Logic.Cpu.Addressings;

public class Relative : IAddressing
{
    public byte[] PeekOperands(Cpu6502 cpu)
    {
        byte[] operands = { cpu.PeekMemory(cpu.ReadByteFromProgramCounter() + 1) };
        return operands;
    }
}
